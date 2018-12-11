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
    [Route("api/v1/workers")]
    public class WorkersController : Controller
    {
        private readonly AlohaContext dbContext;
        private readonly IClassMapping<Worker, WorkerDto> workerToWorkerDtoMapping;
        private readonly IClassMapping<WorkerDto, Worker> workerDtoToWorkerMapping;
        private readonly IEntityUpdater<Worker> workerUpdater;
        private readonly ISecurityService securityService;

        public WorkersController(
            AlohaContext dbContext,
            IClassMapping<Worker, WorkerDto> workerToWorkerDtoMapping,
            IClassMapping<WorkerDto, Worker> workerDtoToWorkerMapping,
            IEntityUpdater<Worker> workerUpdater,
            ISecurityService securityService)
        {
            this.dbContext = dbContext;
            this.workerToWorkerDtoMapping = workerToWorkerDtoMapping;
            this.workerDtoToWorkerMapping = workerDtoToWorkerMapping;
            this.workerUpdater = workerUpdater;
            this.securityService = securityService;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public List<WorkerDto> List()
        {
            return dbContext.Workers
                .Include(w => w.User)
                .Include(w => w.Workstation)
                    .ThenInclude(w => w.Floor)
                .Include(f => f.Photo)
                .Select(workerToWorkerDtoMapping.Map)
                .ToList();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<WorkerDto> GetById(int id)
        {
            Worker worker = dbContext.Workers
                .Include(w => w.User)
                .Include(w => w.Workstation)
                    .ThenInclude(w => w.Floor)
                .Include(f => f.Photo)
                .SingleOrDefault(w => w.Id == id);

            if (worker == null)
            {
                return NotFound();
            }

            return workerToWorkerDtoMapping.Map(worker);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
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

            dbContext.Users.Add(user);

            try
            {
                dbContext.SaveChanges();
            }
            catch
            {
                return Conflict();
            }

            return CreatedAtAction(nameof(GetById), new { Id = worker.Id }, workerToWorkerDtoMapping.Map(worker));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public ActionResult<WorkerDto> Update(int id, [FromBody]WorkerDto workerDto)
        {
            Worker worker = workerDtoToWorkerMapping.Map(workerDto);

            Worker actualWorker = dbContext.Workers
                .Include(f => f.User)
                .Include(w => w.Workstation)
                    .ThenInclude(w => w.Floor)
                .Include(f => f.Photo)
                .SingleOrDefault(f => f.Id == id);

            if (actualWorker == null)
            {
                return NotFound();
            }

            if (actualWorker.ConcurrencyToken != workerDto.ConcurrencyToken)
            {
                return Conflict();
            }

            if (workerDto.PhotoUrl != null && workerDto.PhotoUrl != string.Empty)
            {
                if (actualWorker.Photo != null)
                {
                    dbContext.Remove(actualWorker.Photo);
                }

                actualWorker.Photo = FileHelper.GetFileFromBase64(workerDto.PhotoUrl);
            }

            workerUpdater.Update(actualWorker, worker);

            try
            {
                dbContext.SaveChanges();
            }
            catch
            {
                return Conflict();
            }

            return workerToWorkerDtoMapping.Map(actualWorker);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public ActionResult Remove(int id)
        {
            Worker worker = dbContext.Workers
                .Include(w => w.User)
                .Include(f => f.Photo)
                .SingleOrDefault(w => w.Id == id);

            if (worker == null)
            {
                return NotFound();
            }

            dbContext.Workers.Remove(worker);
            dbContext.Users.Remove(worker.User);

            if (worker.Photo != null)
            {
                dbContext.Files.Remove(worker.Photo);
            }

            dbContext.SaveChanges();

            return NoContent();
        }
    }
}
