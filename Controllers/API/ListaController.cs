using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservaSala.Models;

namespace ReservaSala.Controllers.API
{
    [Route("api/Lista")]
    [ApiController]
    public class ListaSalaAPI : ControllerBase
    {
        [HttpGet]
        public List<Sala> Get()
        {
            return new List<Sala>
            {
                new Sala() { Nome = "Sala 01" },
                new Sala() { Nome = "Sala 02" }
            };
        }

        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
    }
}
