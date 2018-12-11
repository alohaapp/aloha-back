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
                ConcurrencyToken = worker.ConcurrencyToken,
                Name = worker.Name,
                Surname = worker.Surname,
                Email = worker.Email,
                Notes = worker.Notes,
                PhotoId = worker.Photo == null ? null : (int?)worker.Photo.Id,
                WorkstationId = worker.Workstation == null ? null : (int?)worker.Workstation.Id,

                FloorId = worker.Workstation == null ? null : (int?)worker.Workstation.Floor.Id,
                UserName = worker.User.UserName
            };
        }
    }
}
