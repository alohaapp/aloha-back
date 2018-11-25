using System;
using Aloha.Dtos;
using Aloha.Model.Entities;

namespace Aloha.Mappers
{
    public class WorkstationToWorkstationDtoMapping : IClassMapping<Workstation, WorkstationDto>
    {
        public WorkstationDto Map(Workstation input)
        {
            return new WorkstationDto()
            {
                X = input.X,
                Y = input.Y,
                WorkerId = input.WorkerId,
                FloorId = input.Floor.Id
            };
        }
    }
}
