using System.Collections;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CsvHelper;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TopCourseWorkBl.DataLayer;

namespace TopCourseWorkBl.BusinessLayer.Handlers.Common.GetGroupedAverageCsv
{
    [UsedImplicitly]
    public class GetGroupedAverageCsvHandler : IRequestHandler<GetGroupedAverageCsvCommand, FileContentResult>
    {
        private readonly MainRepository _repository;

        public GetGroupedAverageCsvHandler(MainRepository repository)
            => _repository = repository;
        
        public async Task<FileContentResult> Handle(GetGroupedAverageCsvCommand request, CancellationToken cancellationToken)
        {
            var records = await _repository.GetGroupedTransactionsByDay(cancellationToken);
            var path = GetRandomPathHelper.GetRandomPath() + ".csv";
            await using var writer = new StreamWriter(path);
            await using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            await csv.WriteRecordsAsync((IEnumerable)records, cancellationToken);
            await csv.DisposeAsync();
            await writer.DisposeAsync();
            var bytes = await File.ReadAllBytesAsync(path, cancellationToken);
            File.Delete(path);
            return new FileContentResult(bytes, "application/csv")
            {
                FileDownloadName = "result.csv"
            };
        }
    }
}