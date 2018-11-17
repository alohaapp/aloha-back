using System;
using Aloha.Dtos;
using Aloha.Model.Entities;

namespace Aloha.Mappers
{
    public class OfficeDtoToOfficeMapping : IClassMapping<OfficeDto, Office>
    {
        public Office Map(OfficeDto officeDto)
        {
            return new Office()
            {
                Name = officeDto.Name
            };
        }
    }
}
