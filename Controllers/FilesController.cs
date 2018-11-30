using System.Collections.Generic;
using System.Linq;
using Aloha.Dtos;
using Aloha.Mappers;
using Aloha.Model.Contexts;
using Aloha.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aloha.Controllers
{
    [Controller]
    [Route("api/v1/[controller]")]
    public class FilesController : Controller
    {
        private readonly AlohaContext dbContext;

        public FilesController(
            AlohaContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("{id}")]
        public FileContentResult Get(int id)
        {
            File file = dbContext.Files
                .Single(f => f.Id == id);

            if (file != null)
            {
                return File(file.Data, file.MediaType);
            }

            return null;
        }
    }
}
