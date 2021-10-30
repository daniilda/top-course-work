using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TopCourseWorkBl.Infrastructure.Handlers.Common.UploadDataset
{
    [UsedImplicitly]
    public class UploadDatasetHandler : IRequestHandler<UploadDatasetCommand, IActionResult>
    {
        public async Task<IActionResult> Handle(UploadDatasetCommand request, CancellationToken cancellationToken)
        {
            await Task.Delay(1000, cancellationToken); //TODO: Work imitation
            
            if (request.Files.Count != 4)
                return new BadRequestObjectResult("Wrong amount of files. Should be 4!");
            
            return new AcceptedResult(); //TODO: Add DB workflow
        }
    }
}