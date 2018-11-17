using System.Collections.Generic;
using System.Linq;
using Aloha.Dtos;
using Aloha.Mappers;
using Aloha.Model.Entities;
using Aloha.Model.Repositories;
using Aloha.Models.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace Aloha.Controllers
{
    [Route("api/v1/[controller]")]
    public class FloorController
    {
        private readonly AlohaContext dbContext;
        private readonly IClassMapping<Floor, FloorDto> floorToFloorDtoMapping;
        private readonly IClassMapping<FloorDto, Floor> floorDtoToFloorMapping;

        public FloorController(
            AlohaContext dbContext,
            IClassMapping<Floor, FloorDto> floorToFloorDtoMapping,
            IClassMapping<FloorDto, Floor> floorDtoToFloorMapping)
        {
            this.dbContext = dbContext;
            this.floorToFloorDtoMapping = floorToFloorDtoMapping;
            this.floorDtoToFloorMapping = floorDtoToFloorMapping;
        }

        [HttpGet]
        public List<FloorDto> List()
        {
            return dbContext.Set<Floor>()
                .AsEnumerable()
                .Select(floorToFloorDtoMapping.Map)
                .ToList();
        }

        [HttpGet("{id}")]
        public FloorDto Get(int id)
        {
            Floor floor = dbContext.Set<Floor>()
                .Find(id);

            return floor == null
                ? null
                : floorToFloorDtoMapping.Map(floor);
        }

        [HttpPost]
        public FloorDto Add(FloorDto floorDto)
        {
            
            Floor floor = floorDtoToFloorMapping.Map(floorDto);

            dbContext.Set<Floor>()
                .Add(floor);
                

            dbContext.SaveChanges();

            return floorToFloorDtoMapping.Map(floor);
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
