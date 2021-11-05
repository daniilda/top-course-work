using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TopCourseWorkBl.BusinessLayer.Handlers.Common.UploadDataset
{
    [UsedImplicitly]
    public class UploadDatasetHandler : IRequestHandler<UploadDatasetCommand, EmptyResult>
    {
        public async Task<EmptyResult> Handle(UploadDatasetCommand request, CancellationToken cancellationToken)
        {
            await Task.Delay(1000, cancellationToken); //TODO: Work imitation

            if (request.Files.Count != 4)
                throw new Exception("Must be 4 files");
            
            return new EmptyResult(); //TODO: Add DB workflow
        }
    }
}