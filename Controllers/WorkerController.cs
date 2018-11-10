using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aloha.Dtos;
using Aloha.Model.Entities;
using Aloha.Model.Repositories;
using Aloha.Models.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aloha.Controllers
{
    [Route("api/v1/[controller]")]
    public class WorkerController : Controller
    {
        private readonly IRepository<Worker> workerRepository;

        public WorkerController(IRepository<Worker> workerRepository)
        {
            this.workerRepository = workerRepository;
        }

        [HttpGet]
        public List<WorkerDto> List()
        {
            return workerRepository.List()
                .Select(worker => new WorkerDto() {
                    Id = worker.Id,
                    Name = worker.Name,
                    Surname = worker.Surname,
                    PhotoUrl = worker.PhotoUrl,
                    Email = worker.Email,
                    Notes = worker.Notes,
                    UserId = worker.User == null ? null : (int?) worker.User.Id,
                    WorkstationId = worker.Workstation == null ? null : (int?) worker.Workstation.Id
                })
                .ToList();
        }

        [HttpGet("{id}")]
        public WorkerDto GetById(int id)
        {
            Worker worker = workerRepository.GetById(id);

            return worker == null ? null : new WorkerDto() {
                Id = worker.Id,
                Name = worker.Name,
                Surname = worker.Surname,
                PhotoUrl = worker.PhotoUrl,
                Email = worker.Email,
                Notes = worker.Notes,
                UserId = worker.User == null ? null : (int?) worker.User.Id,
                WorkstationId = worker.Workstation == null ? null : (int?) worker.Workstation.Id
            };
        }

        [HttpPost]
        public void Add(WorkerDto workerDto)
        {
            Worker worker = new Worker() {
                Name = workerDto.Name,
                Surname = workerDto.Surname,
                PhotoUrl = workerDto.PhotoUrl,
                Email = workerDto.Email,
                Notes = workerDto.Notes
            };

            workerRepository.Add(worker);
        }

        [HttpDelete("{id}")]
        public void Remove(int id)
        {
            workerRepository.Remove(id);
        }
    }
}
