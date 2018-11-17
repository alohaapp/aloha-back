using System;
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
                Workstations = input.Workstations
            };
        }
    }
}
