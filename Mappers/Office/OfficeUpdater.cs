using Aloha.Model.Entities;

namespace Aloha.Mappers
{
    public class OfficeUpdater : IEntityUpdater<Office>
    {
        public void Update(Office target, Office source)
        {
            target.Name = source.Name;
        }
    }
}