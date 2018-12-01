using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aloha.Dtos;
using Aloha.Helpers.FileHelper;
using Aloha.Mappers;
using Aloha.Model.Contexts;
using Aloha.Model.Entities;
using Aloha.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aloha.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class WorkersController : Controller
    {
        private readonly AlohaContext alohaContext;
        private readonly IClassMapping<Worker, WorkerDto> workerToWorkerDtoMapping;
        private readonly IClassMapping<WorkerDto, Worker> workerDtoToWorkerMapping;
        private readonly IEntityUpdater<Worker> workerUpdater;
        private readonly ISecurityService securityService;

        public WorkersController(
            AlohaContext alohaContext,
            IClassMapping<Worker, WorkerDto> workerToWorkerDtoMapping,
            IClassMapping<WorkerDto, Worker> workerDtoToWorkerMapping,
            IEntityUpdater<Worker> workerUpdater,
            ISecurityService securityService)
        {
            this.alohaContext = alohaContext;
            this.workerToWorkerDtoMapping = workerToWorkerDtoMapping;
            this.workerDtoToWorkerMapping = workerDtoToWorkerMapping;
            this.workerUpdater = workerUpdater;
            this.securityService = securityService;
        }

        [HttpGet]
        public List<WorkerDto> List()
        {
            return alohaContext.Workers
                .Include(w => w.User)
                .Include(w => w.Workstation)
                .Include(f => f.Photo)
                .Select(workerToWorkerDtoMapping.Map)
                .ToList();
        }

        [HttpGet("{id}")]
        public WorkerDto GetById(int id)
        {
            Worker worker = alohaContext.Workers
                .Include(w => w.User)
                .Include(w => w.Workstation)
                .Include(f => f.Photo)
                .Single(w => w.Id == id);

            return worker == null
                ? null
                : workerToWorkerDtoMapping.Map(worker);
        }

        [HttpPost]
        public ActionResult<WorkerDto> Add([FromBody]WorkerDto workerDto)
        {
            if (workerDto.UserName == null)
            {
                return BadRequest(new { Password = "The UserName field is required." });
            }

            if (workerDto.Password == null)
            {
                return BadRequest(new { Password = "The Password field is required." });
            }

            Worker worker = workerDtoToWorkerMapping.Map(workerDto);

            User user = new User()
            {
                UserName = workerDto.UserName,
                Worker = worker,
                PasswordHash = securityService.HashPassword(workerDto.Password)
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
                .Include(f => f.Photo)
                .SingleOrDefault(f => f.Id == id);

            if (workerDto.PhotoUrl != null && workerDto.PhotoUrl != string.Empty)
            {
                if (actualWorker.Photo != null)
                {
                    alohaContext.Remove(actualWorker.Photo);
                }

                actualWorker.Photo = FileHelper.GetFileFromBase64(workerDto.PhotoUrl);
            }

            workerUpdater.Update(actualWorker, worker);

            alohaContext.SaveChanges();

            return workerToWorkerDtoMapping.Map(actualWorker);
        }

        [HttpDelete("{id}")]
        public void Remove(int id)
        {
            Worker worker = alohaContext.Workers
                .Include(w => w.User)
                .Include(f => f.Photo)
                .Single(w => w.Id == id);

            alohaContext.Workers.Remove(worker);
            alohaContext.Users.Remove(worker.User);

            if (worker.Photo != null)
            {
                alohaContext.Files.Remove(worker.Photo);
            }

            alohaContext.SaveChanges();
        }
    }
}
