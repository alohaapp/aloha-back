using Aloha.Model.Entities;

namespace Aloha.Mappers
{
    public class FloorUpdater : IEntityUpdater<Floor>
    {
        public void Update(Floor target, Floor source)
        {
            target.Name = source.Name;
        }
    }
}