﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Aloha.Dtos
{
    public class FloorDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Url]
        public string ImageUrl { get; set; }

        public int WorkerCount { get; set; }

        public int OfficeId { get; set; }
    }
}