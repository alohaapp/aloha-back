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
    [Route("api/v1/[controller]")]
    public class FloorsController : Controller
    {
        private readonly AlohaContext dbContext;
        private readonly IClassMapping<Floor, FloorDto> floorToFloorDtoMapping;
        private readonly IClassMapping<FloorDto, Floor> floorDtoToFloorMapping;
        private readonly IEntityUpdater<Floor> floorUpdater;

        public FloorsController(
            AlohaContext dbContext,
            IClassMapping<Floor, FloorDto> floorToFloorDtoMapping,
            IClassMapping<FloorDto, Floor> floorDtoToFloorMapping,
            IEntityUpdater<Floor> floorUpdater)
        {
            this.dbContext = dbContext;
            this.floorToFloorDtoMapping = floorToFloorDtoMapping;
            this.floorDtoToFloorMapping = floorDtoToFloorMapping;
            this.floorUpdater = floorUpdater;
        }

        [HttpGet]
        public List<FloorDto> List()
        {
            return dbContext.Set<Floor>()
                .Include(f => f.Office)
                .AsEnumerable()
                .Select(floorToFloorDtoMapping.Map)
                .ToList();
        }

        [HttpGet("{id}")]
        public FloorDto Get(int id)
        {
            Floor floor = dbContext.Set<Floor>()
                .Include(f => f.Office)
                .Single(f => f.Id == id);

            return floor == null
                ? null
                : floorToFloorDtoMapping.Map(floor);
        }

        [HttpPost]
        public FloorDto Add([FromBody]FloorDto floorDto)
        {
            Floor floor = floorDtoToFloorMapping.Map(floorDto);

            Office office = dbContext.Offices
                .Include(o => o.Floors)
                .SingleOrDefault(o => o.Id == floorDto.OfficeId);

            office.Floors.Add(floor);

            dbContext.SaveChanges();

            return floorToFloorDtoMapping.Map(floor);
        }

        [HttpPut]
        public FloorDto Update([FromBody]FloorDto floorDto)
        {
            Floor floor = floorDtoToFloorMapping.Map(floorDto);

            Floor actualFloor = dbContext.Floors
                .Include(f => f.Office)
                .SingleOrDefault(f => f.Id == floorDto.Id);

            floorUpdater.Update(actualFloor, floor);

            dbContext.SaveChanges();

            return floorToFloorDtoMapping.Map(actualFloor);
        }

        [HttpDelete("{id}")]
        public void Remove(int id)
        {
            // TODO: Remove its Workastations, either here or adding a on-delete-cascade.

            Floor floor = dbContext.Set<Floor>()
                .Find(id);

            dbContext.Set<Floor>().Remove(floor);

            dbContext.SaveChanges();
        }
    }
}
