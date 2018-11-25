using Aloha.Model.Entities;

namespace Aloha.Mappers
{
    public class WorkstationUpdater : IEntityUpdater<Workstation>
    {
        public void Update(Workstation target, Workstation source)
        {
            target.X = source.X;
            target.Y = source.Y;
            target.WorkerId = source.WorkerId;
        }
    }
}