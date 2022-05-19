using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Catalog.Dtos
{
    public record UpdateGameDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Grade { get; set; }

        public string Image { get; set; }
    }
}