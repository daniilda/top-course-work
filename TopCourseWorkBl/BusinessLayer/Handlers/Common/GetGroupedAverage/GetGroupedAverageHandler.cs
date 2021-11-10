using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using TopCourseWorkBl.DataLayer;
using TopCourseWorkBl.DataLayer.Cmds.Main;

namespace TopCourseWorkBl.BusinessLayer.Handlers.Common.GetGroupedAverage
{
    [UsedImplicitly]
    public class GetGroupedAverageHandler : IRequestHandler<GetGroupedAverageCommand, GetGroupedAverageResponse>
    {
        private readonly MainRepository _repository;

        public GetGroupedAverageHandler(MainRepository repository)
            => _repository = repository;

        public async Task<GetGroupedAverageResponse> Handle(GetGroupedAverageCommand request,
            CancellationToken cancellationToken)
        {
            var result =
                await _repository.GetGroupedTransactionsByDay(
                    new GetGroupedTransactionsByDayCmd(request.PaginationRequest), cancellationToken);

            return new GetGroupedAverageResponse(result.Transactions, result.PaginationResponse);
        }
    }
}