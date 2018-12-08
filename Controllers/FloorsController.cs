using System.Collections.Generic;
using System.Linq;
using Aloha.Dtos;
using Aloha.Helpers.FileHelper;
using Aloha.Mappers;
using Aloha.Model.Contexts;
using Aloha.Model.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aloha.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/v1/floors")]
    public class FloorsController : Controller
    {
        private readonly AlohaContext dbContext;
        private readonly IClassMapping<Floor, FloorDto> floorToFloorDtoMapping;
        private readonly IClassMapping<FloorDto, Floor> floorDtoToFloorMapping;
        private readonly IClassMapping<Workstation, WorkstationDto> workstationToWorkstationDtoMapping;
        private readonly IEntityUpdater<Floor> floorUpdater;

        public FloorsController(
            AlohaContext dbContext,
            IClassMapping<Floor, FloorDto> floorToFloorDtoMapping,
            IClassMapping<FloorDto, Floor> floorDtoToFloorMapping,
            IClassMapping<Workstation, WorkstationDto> workstationToWorkstationDtoMapping,
            IEntityUpdater<Floor> floorUpdater)
        {
            this.dbContext = dbContext;
            this.floorToFloorDtoMapping = floorToFloorDtoMapping;
            this.floorDtoToFloorMapping = floorDtoToFloorMapping;
            this.workstationToWorkstationDtoMapping = workstationToWorkstationDtoMapping;
            this.floorUpdater = floorUpdater;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public List<FloorDto> List()
        {
            return dbContext.Floors
                .Include(f => f.Office)
                .Include(f => f.Workstations)
                .Include(f => f.Image)
                .Select(floorToFloorDtoMapping.Map)
                .ToList();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<FloorDto> GetById(int id)
        {
            Floor floor = dbContext.Floors
                .Include(f => f.Office)
                .Include(f => f.Workstations)
                .Include(f => f.Image)
                .SingleOrDefault(f => f.Id == id);

            if (floor == null)
            {
                return NotFound();
            }

            return floorToFloorDtoMapping.Map(floor);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public ActionResult<FloorDto> Add([FromBody]FloorDto floorDto)
        {
            Floor floor = floorDtoToFloorMapping.Map(floorDto);

            Office office = dbContext.Offices
                .Include(o => o.Floors)
                .SingleOrDefault(o => o.Id == floorDto.OfficeId);

            if (office == null)
            {
                return NotFound();
            }

            office.Floors.Add(floor);

            try
            {
                dbContext.SaveChanges();
            }
            catch
            {
                return Conflict();
            }

            return CreatedAtAction(nameof(GetById), new { Id = floor.Id }, floorToFloorDtoMapping.Map(floor));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public ActionResult<FloorDto> Update(int id, [FromBody]FloorDto floorDto)
        {
            Floor floor = floorDtoToFloorMapping.Map(floorDto);

            Floor actualFloor = dbContext.Floors
                .Include(f => f.Office)
                .Include(f => f.Workstations)
                .Include(f => f.Image)
                .SingleOrDefault(f => f.Id == id);

            if (actualFloor == null)
            {
                return NotFound();
            }

            if (floorDto.ImageUrl != null && floorDto.ImageUrl != string.Empty)
            {
                if (actualFloor.Image != null)
                {
                    dbContext.Remove(actualFloor.Image);
                }

                actualFloor.Image = FileHelper.GetFileFromBase64(floorDto.ImageUrl);
            }

            floorUpdater.Update(actualFloor, floor);

            try
            {
                dbContext.SaveChanges();
            }
            catch
            {
                return Conflict();
            }

            return floorToFloorDtoMapping.Map(actualFloor);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public ActionResult Remove(int id)
        {
            Floor floor = dbContext.Floors
                .Include(f => f.Image)
                .SingleOrDefault(f => f.Id == id);

            if (floor == null)
            {
                return NotFound();
            }

            dbContext.Floors.Remove(floor);

            if (floor.Image != null)
            {
                dbContext.Files.Remove(floor.Image);
            }

            dbContext.SaveChanges();

            return NoContent();
        }
    }
}
