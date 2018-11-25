using System;
using Aloha.Dtos;
using Aloha.Model.Entities;

namespace Aloha.Mappers
{
    public class WorkstationDtoToWorkstationMapping : IClassMapping<WorkstationDto, Workstation>
    {
        public Workstation Map(WorkstationDto input)
        {
            return new Workstation()
            {
                X = input.X,
                Y = input.Y,
                WorkerId = input.WorkerId
            };
        }
    }
}
