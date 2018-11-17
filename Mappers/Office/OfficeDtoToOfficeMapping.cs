using System;
using Aloha.Dtos;
using Aloha.Model.Entities;

namespace Aloha.Mappers
{
    public class OfficeToOfficeDtoMapping : IClassMapping<Office, OfficeDto>
    {
        public OfficeDto Map(Office office)
        {
            return new OfficeDto()
            {
                Id = office.Id,
                Name = office.Name
            };
        }
    }
}
