using Aloha.Dtos;
using Aloha.Model.Entities;

public class WorkerDtoToWorkerMapping : IClassMapping<WorkerDto, Worker>
{
    public Worker Map(WorkerDto workerDto)
    {
        return new Worker()
        {
            Name = workerDto.Name,
            Surname = workerDto.Surname,
            PhotoUrl = workerDto.PhotoUrl,
            Email = workerDto.Email,
            Notes = workerDto.Notes
        };
    }
}