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
    [Route("api/v1/Floors/{floorId}/[controller]")]
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
        public List<WorkstationDto> List(int floorId)
        {
            return dbContext.Set<Workstation>()
                .Include(w => w.Floor)
                .Where(w => w.Floor.Id == floorId)
                .Select(workstationToWorkstationDtoMapping.Map)
                .ToList();
        }

        [HttpGet("{id}")]
        public WorkstationDto Get(int floorId, int id)
        {
            Workstation workstation = dbContext.Set<Workstation>()
                .Include(w => w.Floor)
                .Where(w => w.Floor.Id == floorId)
                .SingleOrDefault(w => w.Id == id);

            return workstation == null
                ? null
                : workstationToWorkstationDtoMapping.Map(workstation);
        }

        [HttpPost]
        public WorkstationDto Add(int floorId, [FromBody]WorkstationDto workstationDto)
        {
            Workstation workstation = workstationDtoToWorkstationMapping.Map(workstationDto);

            Floor floor = dbContext.Floors
                .Include(o => o.Workstations)
                .SingleOrDefault(o => o.Id == floorId);

            floor.Workstations.Add(workstation);

            dbContext.SaveChanges();

            return workstationToWorkstationDtoMapping.Map(workstation);
        }

        [HttpPut("{id}")]
        public WorkstationDto Update(int floorId, int id, [FromBody]WorkstationDto workstationDto)
        {
            Workstation workstation = workstationDtoToWorkstationMapping.Map(workstationDto);

            Workstation actualWorkstation = dbContext.Workstations
                .Include(w => w.Floor)
                .Where(w => w.Floor.Id == floorId)
                .SingleOrDefault(w => w.Id == id);

            if (actualWorkstation == null)
            {
                return null;
            }

            workstationUpdater.Update(actualWorkstation, workstation);

            dbContext.SaveChanges();

            return workstationToWorkstationDtoMapping.Map(actualWorkstation);
        }

        [HttpDelete("{id}")]
        public void Remove(int floorId, int id)
        {
            Workstation workstation = dbContext.Workstations
                .Include(w => w.Floor)
                .Where(w => w.Floor.Id == floorId)
                .SingleOrDefault(w => w.Id == id);

            dbContext.Set<Workstation>().Remove(workstation);

            dbContext.SaveChanges();
        }
    }
}
