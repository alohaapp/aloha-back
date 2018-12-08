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
    [Controller]
    [Route("api/v1/files")]
    public class FilesController : Controller
    {
        private readonly AlohaContext dbContext;

        public FilesController(AlohaContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult GetById(int id)
        {
            File file = dbContext.Files
                .SingleOrDefault(f => f.Id == id);

            if (file == null)
            {
                return NotFound();
            }

            return File(file.Data, file.MediaType);
        }
    }
}
