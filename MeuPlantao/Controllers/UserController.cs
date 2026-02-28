using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MeuPlantao.Data;
using MeuPlantao.Entities;

namespace MeuPlantao.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public UserController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetProfissionais()
        {
            var resultado = await _appDbContext.Usuarios.ToListAsync();
            return Ok(resultado);
        }

        [HttpPost("users")]
        public async Task<IActionResult> PostUsers([FromBody]  UserModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _appDbContext.Usuarios.Add(user);
            await _appDbContext.SaveChangesAsync();

            return Created("setor adcionado", user);
        }
    }
}