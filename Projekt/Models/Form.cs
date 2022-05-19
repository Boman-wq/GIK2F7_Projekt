using System;
using Microsoft.AspNetCore.Http;

namespace Catalog.Models
{
    public class Game
    {
        public Guid Id { get; init; }
        public string Name { get; set; }
        public string Description { get ; set; }
        public string Grade { get; set; }
        public string Image { get; set; }
    }
}