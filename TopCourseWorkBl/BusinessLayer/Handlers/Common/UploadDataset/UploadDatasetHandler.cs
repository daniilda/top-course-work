using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TopCourseWorkBl.AuthenticationLayer.Exceptions;
using TopCourseWorkBl.BusinessLayer.CheckStrategy;
using TopCourseWorkBl.BusinessLayer.CsvParseStrategy;
using TopCourseWorkBl.BusinessLayer.CsvParseStrategy.Readers;
using TopCourseWorkBl.BusinessLayer.CsvReadChain.Readers;
using TopCourseWorkBl.BusinessLayer.Extensions;
using TopCourseWorkBl.DataLayer;
using TopCourseWorkBl.DataLayer.Cmds.Main;

namespace TopCourseWorkBl.BusinessLayer.Handlers.Common.UploadDataset
{
    [UsedImplicitly]
    public class UploadDatasetHandler : IRequestHandler<UploadDatasetCommand, EmptyResult>
    {
        private readonly MainRepository _repository;

        public UploadDatasetHandler(MainRepository repository)
            => _repository = repository;

        public async Task<EmptyResult> Handle(UploadDatasetCommand request, CancellationToken cancellationToken)
        {
            var dataSet = new CsvParserResponse();
            var parseChain = new CustomerCsvReader();
            parseChain
                .SetNext(new TransactionCsvReader())
                .SetNext(new TypeCsvReader())
                .SetNext(new MccCodeCsvReader());
            try
            {
                foreach (var file in request.Files)
                {
                    dataSet.Path = GetRandomPathHelper.GetRandomPath();
                    file.CreateTempFile(dataSet.Path);
                    (dataSet, _) = parseChain.Parse(dataSet);

                    File.Delete(dataSet!.Path);
                }
            }
            catch (Exception ex)
            {
                File.Delete(dataSet!.Path);
                throw new BadRequestException("Wrong data format!");
            }
            
            var checkChain = new CustomerCheck();
            checkChain
                .SetNext(new TransactionCheck())
                .SetNext(new TypeCheck())
                .SetNext(new MccCodeCheck());
            
            var (isOk, check) = checkChain.Check(dataSet);
            if (!isOk)
                throw new BadRequestException($"Check {check} failed");

            await _repository.CleanTablesWithDataAsync(cancellationToken);
            try
            {
                await _repository.InsertMccCodesAsync(new InsertMccCodesCmd(dataSet.MccCodes!), cancellationToken); //TODO: May consider using transactions
                await _repository.InsertTypesAsync(new InsertTypesCmd(dataSet.Types!), cancellationToken);
                await _repository.InsertCustomersAsync(new InsertCustomersCmd(dataSet.Customers!), cancellationToken);
                await _repository.InsertTransactionsAsync(new InsertTransactionsCmd(dataSet.Transactions!),
                    cancellationToken);
            }
            catch (Exception ex)
            {
                await _repository.CleanTablesWithDataAsync(cancellationToken);
                throw new Exception(ex.Message);
            }

            return new EmptyResult();
        }
    }
}