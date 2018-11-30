using System;
using System.Linq;
using Aloha.Dtos;
using Aloha.Model.Entities;

namespace Aloha.Mappers
{
    public class FloorToFloorDtoMapping : IClassMapping<Floor, FloorDto>
    {
        public FloorDto Map(Floor input)
        {
            return new FloorDto()
            {
                Id = input.Id,
                Name = input.Name,
                ImageUrl = input.ImageURL,
                WorkerCount = input.Workstations == null ? 0 : input.Workstations.Count(w => w.WorkerId != null),
                OfficeId = input.Office.Id
            };
        }
    }
}
