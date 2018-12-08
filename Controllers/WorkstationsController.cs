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
    [AllowAnonymous]
    [ApiController]
    [Route("api/v1/Floors/{floorId}/workstations")]
    public class WorkstationsController : Controller
    {
        private readonly AlohaContext dbContext;
        private readonly IClassMapping<Workstation, WorkstationDto> workstationToWorkstationDtoMapping;
        private readonly IClassMapping<WorkstationDto, Workstation> workstationDtoToWorkstationMapping;
        private readonly IEntityUpdater<Workstation> workstationUpdater;

        public WorkstationsController(
            AlohaContext dbContext,
            IClassMapping<Workstation, WorkstationDto> workstationToWorkstationDtoMapping,
            IClassMapping<WorkstationDto, Workstation> workstationDtoToWorkstationMapping,
            IEntityUpdater<Workstation> workstationUpdater)
        {
            this.dbContext = dbContext;
            this.workstationToWorkstationDtoMapping = workstationToWorkstationDtoMapping;
            this.workstationDtoToWorkstationMapping = workstationDtoToWorkstationMapping;
            this.workstationUpdater = workstationUpdater;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public List<WorkstationDto> List(int floorId)
        {
            return dbContext.Workstations
                .Include(w => w.Floor)
                .Where(w => w.Floor.Id == floorId)
                .Select(workstationToWorkstationDtoMapping.Map)
                .ToList();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<WorkstationDto> GetById(int floorId, int id)
        {
            Workstation workstation = dbContext.Workstations
                .Include(w => w.Floor)
                .Where(w => w.Floor.Id == floorId)
                .SingleOrDefault(w => w.Id == id);

            if (workstation == null)
            {
                return NotFound();
            }

            return workstationToWorkstationDtoMapping.Map(workstation);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        public ActionResult<WorkstationDto> Add(int floorId, [FromBody]WorkstationDto workstationDto)
        {
            Workstation workstation = workstationDtoToWorkstationMapping.Map(workstationDto);

            Floor floor = dbContext.Floors
                .Include(o => o.Workstations)
                .SingleOrDefault(o => o.Id == floorId);

            if (floor == null)
            {
                return NotFound();
            }

            floor.Workstations.Add(workstation);

            try
            {
                dbContext.SaveChanges();
            }
            catch
            {
                return Conflict();
            }

            return CreatedAtAction(nameof(GetById), new { floorId = floor.Id, Id = workstation.Id }, workstationToWorkstationDtoMapping.Map(workstation));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public ActionResult<WorkstationDto> Update(int floorId, int id, [FromBody]WorkstationDto workstationDto)
        {
            Workstation workstation = workstationDtoToWorkstationMapping.Map(workstationDto);

            Workstation actualWorkstation = dbContext.Workstations
                .Include(w => w.Floor)
                .Where(w => w.Floor.Id == floorId)
                .SingleOrDefault(w => w.Id == id);

            if (actualWorkstation == null)
            {
                return NotFound();
            }

            workstationUpdater.Update(actualWorkstation, workstation);

            try
            {
                dbContext.SaveChanges();
            }
            catch
            {
                return Conflict();
            }

            return workstationToWorkstationDtoMapping.Map(actualWorkstation);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public ActionResult Remove(int floorId, int id)
        {
            Workstation workstation = dbContext.Workstations
                .Include(w => w.Floor)
                .Where(w => w.Floor.Id == floorId)
                .SingleOrDefault(w => w.Id == id);

            if (workstation == null)
            {
                return NotFound();
            }

            dbContext.Workstations.Remove(workstation);

            dbContext.SaveChanges();

            return NoContent();
        }
    }
}
