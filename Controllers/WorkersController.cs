using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aloha.Dtos;
using Aloha.Mappers;
using Aloha.Model.Contexts;
using Aloha.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aloha.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class WorkersController : Controller
    {
        private readonly AlohaContext alohaContext;
        private readonly IClassMapping<Worker, WorkerDto> workerToWorkerDtoMapping;
        private readonly IClassMapping<WorkerDto, Worker> workerDtoToWorkerMapping;
        private readonly IEntityUpdater<Worker> workerUpdater;

        public WorkersController(
            AlohaContext alohaContext,
            IClassMapping<Worker, WorkerDto> workerToWorkerDtoMapping,
            IClassMapping<WorkerDto, Worker> workerDtoToWorkerMapping,
            IEntityUpdater<Worker> workerUpdater)
        {
            this.alohaContext = alohaContext;
            this.workerToWorkerDtoMapping = workerToWorkerDtoMapping;
            this.workerDtoToWorkerMapping = workerDtoToWorkerMapping;
            this.workerUpdater = workerUpdater;
        }

        [HttpGet]
        public List<WorkerDto> List()
        {
            return alohaContext.Workers
                .Include(w => w.User)
                .Include(w => w.Workstation)
                .Select(workerToWorkerDtoMapping.Map)
                .ToList();
        }

        [HttpGet("{id}")]
        public WorkerDto GetById(int id)
        {
            Worker worker = alohaContext.Workers
                .Include(w => w.User)
                .Include(w => w.Workstation)
                .Single(w => w.Id == id);

            return worker == null
                ? null
                : workerToWorkerDtoMapping.Map(worker);
        }

        [HttpPost]
        public WorkerDto Add([FromBody]WorkerDto workerDto)
        {
            Worker worker = workerDtoToWorkerMapping.Map(workerDto);

            User user = new User()
            {
                UserName = workerDto.UserName,
                Worker = worker
            };

            alohaContext.Users.Add(user);
            alohaContext.SaveChanges();

            return workerToWorkerDtoMapping.Map(worker);
        }

        [HttpPut("{id}")]
        public WorkerDto Update(int id, [FromBody]WorkerDto workerDto)
        {
            Worker worker = workerDtoToWorkerMapping.Map(workerDto);

            Worker actualWorker = alohaContext.Workers
                .Include(f => f.User)
                .Include(w => w.Workstation)
                .SingleOrDefault(f => f.Id == id);

            workerUpdater.Update(actualWorker, worker);

            alohaContext.SaveChanges();

            return workerToWorkerDtoMapping.Map(actualWorker);
        }

        [HttpDelete("{id}")]
        public void Remove(int id)
        {
            Worker worker = alohaContext.Workers
                .Include(w => w.User)
                .Single(w => w.Id == id);

            alohaContext.Workers.Remove(worker);
            alohaContext.Users.Remove(worker.User);

            alohaContext.SaveChanges();
        }
    }
}
