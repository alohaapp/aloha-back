using System;
using System.Collections.Generic;
using System.Linq;
using Aloha.Dtos;
using Aloha.Model.Entities;

namespace Aloha.Mappers
{
    public class OfficeToOfficeDtoMapping : IClassMapping<Office, OfficeDto>
    {
        private readonly IClassMapping<Floor, FloorDto> floorToFloorDtoMapping;

        public OfficeToOfficeDtoMapping(IClassMapping<Floor, FloorDto> floorToFloorDtoMapping)
        {
            this.floorToFloorDtoMapping = floorToFloorDtoMapping;
        }

        public OfficeDto Map(Office office)
        {
            return new OfficeDto()
            {
                Id = office.Id,
                Name = office.Name,
                Floors = office.Floors.Select(floorToFloorDtoMapping.Map).ToList()
            };
        }
    }
}
