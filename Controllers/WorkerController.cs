using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aloha.Dtos;
using Aloha.Model.Entities;
using Aloha.Models.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aloha.Controllers
{
    [Route("api/v1/[controller]")]
    public class WorkerController : Controller
    {
        private readonly AlohaContext alohaContext;
        private readonly IClassMapping<Worker, WorkerDto> workerToWorkerDtoMapping;
        private readonly IClassMapping<WorkerDto, Worker> workerDtoToWorkerMapping;

        public WorkerController(
            AlohaContext alohaContext,
            IClassMapping<Worker, WorkerDto> workerToWorkerDtoMapping,
            IClassMapping<WorkerDto, Worker> workerDtoToWorkerMapping)
        {
            this.alohaContext = alohaContext;
            this.workerToWorkerDtoMapping = workerToWorkerDtoMapping;
            this.workerDtoToWorkerMapping = workerDtoToWorkerMapping;
        }

        [HttpGet]
        public List<WorkerDto> List()
        {
            return alohaContext.Workers
                .Select(workerToWorkerDtoMapping.Map)
                .ToList();
        }

        [HttpGet("{id}")]
        public WorkerDto GetById(int id)
        {
            Worker worker = alohaContext.Workers.Find(id);

            return worker == null
                ? null
                : workerToWorkerDtoMapping.Map(worker);
        }

        [HttpPost]
        public WorkerDto Add(WorkerDto workerDto)
        {
            Worker worker = workerDtoToWorkerMapping.Map(workerDto);
            
            User user = alohaContext.Users.Find(worker.UserId);

            user.Worker = worker;
            worker.User = user;
            
            alohaContext.Add(worker).State = EntityState.Added;
            alohaContext.Entry(user).State = EntityState.Modified;

            alohaContext.SaveChanges();

            return workerToWorkerDtoMapping.Map(worker);
        }

        [HttpDelete("{id}")]
        public void Remove(int id)
        {
            Worker worker = alohaContext.Workers.Find(id);
            
            alohaContext.Workers.Remove(worker);

            alohaContext.SaveChanges();
        }
    }
}
