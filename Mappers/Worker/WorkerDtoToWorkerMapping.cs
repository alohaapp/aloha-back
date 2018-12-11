using Aloha.Dtos;
using Aloha.Helpers.FileHelper;
using Aloha.Model.Entities;

namespace Aloha.Mappers
{
    public class WorkerDtoToWorkerMapping : IClassMapping<WorkerDto, Worker>
    {
        public Worker Map(WorkerDto workerDto)
        {
            return new Worker()
            {
                ConcurrencyToken = workerDto.ConcurrencyToken,
                Name = workerDto.Name,
                Surname = workerDto.Surname,
                Email = workerDto.Email,
                Notes = workerDto.Notes,
                Photo = workerDto.PhotoUrl == null ? null : FileHelper.GetFileFromBase64(workerDto.PhotoUrl)
            };
        }
    }
}