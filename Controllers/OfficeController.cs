using System.Collections.Generic;
using System.Linq;
using Aloha.Dtos;
using Aloha.Mappers;
using Aloha.Model.Entities;
using Aloha.Model.Repositories;
using Aloha.Models.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aloha.Controllers
{
    [Route("api/v1/[controller]")]
    public class OfficeController
    {
        private readonly AlohaContext dbContext;
        private readonly IClassMapping<Office, OfficeDto> officeToOfficeDtoMapping;
        private readonly IClassMapping<OfficeDto, Office> officeDtoToOfficeMapping;

        public OfficeController(
            AlohaContext dbContext,
            IClassMapping<Office, OfficeDto> officeToOfficeDtoMapping,
            IClassMapping<OfficeDto, Office> officeDtoToOfficeMapping)
        {
            this.dbContext = dbContext;
            this.officeToOfficeDtoMapping = officeToOfficeDtoMapping;
            this.officeDtoToOfficeMapping = officeDtoToOfficeMapping;
        }

        [HttpGet]
        public List<OfficeDto> List()
        {
            return dbContext.Offices
                .Include(o => o.Floors)
                .Select(officeToOfficeDtoMapping.Map)
                .ToList();
        }

        [HttpGet("{id}")]
        public OfficeDto Get(int id)
        {
            Office office = dbContext.Offices
                .Include(o => o.Floors)
                .Single(o => o.Id == id);

            return office == null
                ? null
                : officeToOfficeDtoMapping.Map(office);
        }

        [HttpPost]
        public OfficeDto Add(OfficeDto officeDto)
        {
            Office office = officeDtoToOfficeMapping.Map(officeDto);

            dbContext.Set<Office>()
                .Add(office);

            dbContext.SaveChanges();

            return officeToOfficeDtoMapping.Map(office);
        }

        [HttpDelete("{id}")]
        public void Remove(int id)
        {
            Office office = dbContext.Set<Office>()
                .Find(id);

            dbContext.Set<Office>().Remove(office);

            dbContext.SaveChanges();
        }
    }
}
