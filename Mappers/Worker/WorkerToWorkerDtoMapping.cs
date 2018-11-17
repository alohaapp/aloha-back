using Aloha.Dtos;
using Aloha.Mappers;
using Aloha.Model.Entities;

namespace Aloha.Mappers
{
    public class WorkerToWorkerDtoMapping : IClassMapping<Worker, WorkerDto>
    {
        public WorkerDto Map(Worker worker)
        {
            return new WorkerDto()
            {
                Id = worker.Id,
                Name = worker.Name,
                Surname = worker.Surname,
                PhotoUrl = worker.PhotoUrl,
                Email = worker.Email,
                Notes = worker.Notes,
                UserId = worker.User.Id,
                WorkstationId = worker.Workstation == null ? null : (int?)worker.Workstation.Id
            };
        }
    }
}
