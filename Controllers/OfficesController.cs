using System.Collections.Generic;
using System.Linq;
using Aloha.Dtos;
using Aloha.Mappers;
using Aloha.Model.Contexts;
using Aloha.Model.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aloha.Controllers
{
    [ApiController]
    [Route("api/v1/offices")]
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

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(200)]
        public List<OfficeDto> List()
        {
            return dbContext.Offices
                .Select(officeToOfficeDtoMapping.Map)
                .ToList();
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<OfficeDto> GetById(int id)
        {
            Office office = dbContext.Offices
                .SingleOrDefault(o => o.Id == id);

            if (office == null)
            {
                return NotFound();
            }

            return officeToOfficeDtoMapping.Map(office);
        }

        [AllowAnonymous]
        [HttpGet("{id}/Floors")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<List<FloorDto>> ListFloors(int id)
        {
            Office office = dbContext.Offices
                .Include(o => o.Floors)
                .SingleOrDefault(o => o.Id == id);

            if (office == null)
            {
                return NotFound();
            }

            return office.Floors
                .Select(floorToFloorDtoMapping.Map)
                .ToList();
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        public ActionResult<OfficeDto> Add([FromBody]OfficeDto officeDto)
        {
            Office office = officeDtoToOfficeMapping.Map(officeDto);

            dbContext.Offices
                .Add(office);

            try
            {
                dbContext.SaveChanges();
            }
            catch
            {
                return Conflict();
            }

            return CreatedAtAction(nameof(GetById), new { Id = office.Id }, officeToOfficeDtoMapping.Map(office));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public ActionResult<OfficeDto> Update(int id, [FromBody]OfficeDto officeDto)
        {
            Office office = officeDtoToOfficeMapping.Map(officeDto);

            Office actualOffice = dbContext.Offices
                .SingleOrDefault(f => f.Id == id);

            if (actualOffice == null)
            {
                return NotFound();
            }

            officeUpdater.Update(actualOffice, office);

            try
            {
                dbContext.SaveChanges();
            }
            catch
            {
                return Conflict();
            }

            return officeToOfficeDtoMapping.Map(actualOffice);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public ActionResult Remove(int id)
        {
            Office office = dbContext.Offices
                .SingleOrDefault(o => o.Id == id);

            if (office == null)
            {
                return NotFound();
            }

            dbContext.Offices.Remove(office);

            dbContext.SaveChanges();

            return NoContent();
        }
    }
}
