using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class WorkersController : Controller
    {
        private readonly IRepository<Worker> workerRepository;
        private readonly IClassMapping<Worker, WorkerDto> workerToWorkerDtoMapping;
        private readonly IClassMapping<WorkerDto, Worker> workerDtoToWorkerMapping;

        public WorkersController(
            IRepository<Worker> workerRepository,
            IClassMapping<Worker, WorkerDto> workerToWorkerDtoMapping,
            IClassMapping<WorkerDto, Worker> workerDtoToWorkerMapping)
        {
            this.workerRepository = workerRepository;
            this.workerToWorkerDtoMapping = workerToWorkerDtoMapping;
            this.workerDtoToWorkerMapping = workerDtoToWorkerMapping;
        }

        [HttpGet]
        public List<WorkerDto> List()
        {
            return workerRepository.List()
                .Select(workerToWorkerDtoMapping.Map)
                .ToList();
        }

        [HttpGet("{id}")]
        public WorkerDto GetById(int id)
        {
            Worker worker = workerRepository.GetById(id);

            return worker == null
                ? null
                : workerToWorkerDtoMapping.Map(worker);
        }

        [HttpPost]
        public WorkerDto Add([FromBody]WorkerDto workerDto)
        {
            Worker worker = workerDtoToWorkerMapping.Map(workerDto);

            workerRepository.Add(worker);

            return workerToWorkerDtoMapping.Map(worker);
        }

        [HttpDelete("{id}")]
        public void Remove(int id)
        {
            workerRepository.Remove(id);
        }
    }
}
