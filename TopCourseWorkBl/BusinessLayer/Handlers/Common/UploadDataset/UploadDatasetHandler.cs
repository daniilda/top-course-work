using System.IO;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Http;
using TopCourseWorkBl.AuthenticationLayer.Exceptions;
using TopCourseWorkBl.BackgroundTasksService;
using TopCourseWorkBl.BusinessLayer.CheckStrategy;
using TopCourseWorkBl.BusinessLayer.CsvParseStrategy;
using TopCourseWorkBl.BusinessLayer.CsvParseStrategy.Readers;
using TopCourseWorkBl.BusinessLayer.Extensions;
using TopCourseWorkBl.DataLayer;
using TopCourseWorkBl.DataLayer.Cmds.Main;
using TopCourseWorkBl.Enums;

namespace TopCourseWorkBl.BusinessLayer.Handlers.Common.UploadDataset
{
    [UsedImplicitly]
    public class UploadDatasetHandler : IRequestHandler<UploadDatasetCommand>
    {
        private readonly MainRepository _repository;
        private readonly TasksProvider _tasks;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UploadDatasetHandler(MainRepository repository, TasksProvider tasks,
            IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _tasks = tasks;
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<Unit> Handle(UploadDatasetCommand request, CancellationToken cancellationToken)
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
            catch
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
            
            //TODO: Fix this govnokod
            var newTokenSource = new CancellationTokenSource();
             _tasks.RegisterNewTaskToExecute(
                 _repository.InsertMccCodesBulkAsync(new InsertMccCodesCmd(dataSet.MccCodes!), newTokenSource.Token),
                 OperationType.Insertion, _httpContextAccessor.HttpContext!.GetUserIdFromHttpContext(),"Вставка MccCodes в базу" ,newTokenSource);
            
             newTokenSource = new CancellationTokenSource();
             _tasks.RegisterNewTaskToExecute(
                 _repository.InsertTypesBulkAsync(new InsertTypesCmd(dataSet.Types!), newTokenSource.Token),
                 OperationType.Insertion, _httpContextAccessor.HttpContext!.GetUserIdFromHttpContext(),"Вставка Types в базу" ,newTokenSource);
            
             newTokenSource = new CancellationTokenSource();
             _tasks.RegisterNewTaskToExecute(
                 _repository.InsertCustomersBulkAsync(new InsertCustomersCmd(dataSet.Customers!), newTokenSource.Token),
                 OperationType.Insertion, _httpContextAccessor.HttpContext!.GetUserIdFromHttpContext(), "Вставка Customers в базу" ,newTokenSource);
            
             newTokenSource = new CancellationTokenSource();
             _tasks.RegisterNewTaskToExecute(
                 _repository.InsertTransactionsBulkAsync(new InsertTransactionsCmd(dataSet.Transactions!),
                     newTokenSource.Token),
                 OperationType.Insertion, _httpContextAccessor.HttpContext!.GetUserIdFromHttpContext(), "Вставка Transactions в базу" ,newTokenSource);

            return Unit.Task;
        }
    }
}