using System.Collections.Generic;
using System.Linq;
using Aloha.Dtos;
using Aloha.Mappers;
using Aloha.Model.Entities;
using Aloha.Models.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aloha.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OfficesController : Controller
    {
        private readonly AlohaContext dbContext;
        private readonly IClassMapping<Office, OfficeDto> officeToOfficeDtoMapping;
        private readonly IClassMapping<OfficeDto, Office> officeDtoToOfficeMapping;
        private readonly IClassMapping<Floor, FloorDto> floorToFloorDtoMapping;
        private readonly IEntityUpdater<Office> officeUpdater;

        public OfficesController(
            AlohaContext dbContext,
            IClassMapping<Office, OfficeDto> officeToOfficeDtoMapping,
            IClassMapping<OfficeDto, Office> officeDtoToOfficeMapping,
            IClassMapping<Floor, FloorDto> floorToFloorDtoMapping,
            IEntityUpdater<Office> officeUpdater)
        {
            this.dbContext = dbContext;
            this.officeToOfficeDtoMapping = officeToOfficeDtoMapping;
            this.officeDtoToOfficeMapping = officeDtoToOfficeMapping;
            this.floorToFloorDtoMapping = floorToFloorDtoMapping;
            this.officeUpdater = officeUpdater;
        }

        [HttpGet]
        public List<OfficeDto> List()
        {
            return dbContext.Offices
                .Select(officeToOfficeDtoMapping.Map)
                .ToList();
        }

        [HttpGet("{id}")]
        public OfficeDto Get(int id)
        {
            Office office = dbContext.Offices
                .Single(o => o.Id == id);

            return office == null
                ? null
                : officeToOfficeDtoMapping.Map(office);
        }

        [HttpGet("{id}/Floors")]
        public List<FloorDto> ListFloors(int id)
        {
            return dbContext.Offices
                .Include(o => o.Floors)
                .Single(o => o.Id == id)?.Floors
                .Select(floorToFloorDtoMapping.Map)
                .ToList();
        }

        [HttpPost]
        public OfficeDto Add([FromBody]OfficeDto officeDto)
        {
            Office office = officeDtoToOfficeMapping.Map(officeDto);

            dbContext.Set<Office>()
                .Add(office);

            dbContext.SaveChanges();

            return officeToOfficeDtoMapping.Map(office);
        }

        [HttpPut("{id}")]
        public OfficeDto Update(int id, [FromBody]OfficeDto officeDto)
        {
            Office office = officeDtoToOfficeMapping.Map(officeDto);

            Office actualOffice = dbContext.Offices
                .SingleOrDefault(f => f.Id == id);

            officeUpdater.Update(actualOffice, office);

            dbContext.SaveChanges();

            return officeToOfficeDtoMapping.Map(actualOffice);
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
