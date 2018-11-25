using System;
using System.Linq;
using Aloha.Dtos;
using Aloha.Model.Entities;

namespace Aloha.Mappers
{
    public class FloorToFloorDtoMapping : IClassMapping<Floor, FloorDto>
    {
        private readonly IClassMapping<Workstation, WorkstationDto> workstationToWorkstationDtoMapping;

        public FloorToFloorDtoMapping(IClassMapping<Workstation, WorkstationDto> workstationToWorkstationDtoMapping)
        {
            this.workstationToWorkstationDtoMapping = workstationToWorkstationDtoMapping;
        }

        public FloorDto Map(Floor input)
        {
            return new FloorDto()
            {
                Id = input.Id,
                Name = input.Name,
                ImageUrl = input.ImageURL,
                OfficeId = input.Office.Id,
                Workstations = input.Workstations.Select(workstationToWorkstationDtoMapping.Map).ToList()
            };
        }
    }
}
