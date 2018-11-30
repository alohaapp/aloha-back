using System;
using Aloha.Dtos;
using Aloha.Helpers.FileHelper;
using Aloha.Model.Entities;

namespace Aloha.Mappers
{
    public class FloorDtoToFloorMapping : IClassMapping<FloorDto, Floor>
    {
        public Floor Map(FloorDto input)
        {
            return new Floor()
            {
                Name = input.Name,
                Image = input.ImageUrl == null ? null : FileHelper.GetFileFromBase64(input.ImageUrl)
            };
        }
    }
}
