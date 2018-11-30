using Aloha.Model.Entities;

namespace Aloha.Mappers
{
    public class WorkerUpdater : IEntityUpdater<Worker>
    {
        public void Update(Worker target, Worker source)
        {
            target.Name = source.Name;
            target.Surname = source.Surname;
            target.Email = source.Email;
            target.Notes = source.Notes;
        }
    }
}