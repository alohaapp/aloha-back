using System.ComponentModel.DataAnnotations;

namespace Aloha.Dtos
{
    public class WorkstationDto
    {
        public int Id { get; set; }
        
        public decimal X { get; set; }

        public decimal Y { get; set; }

        public int FloorId { get; set; }

        public int? WorkerId { get; set; }
    }
}