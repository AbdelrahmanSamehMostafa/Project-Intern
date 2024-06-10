using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public record AdminForUpdateDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}