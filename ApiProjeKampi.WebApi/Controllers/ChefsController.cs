using ApiProjeKampi.WebApi.Context;
using ApiProjeKampi.WebApi.Dtos.ChefDtos;
using ApiProjeKampi.WebApi.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;

namespace ApiProjeKampi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChefsController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;
        public ChefsController(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
      
        [HttpGet]
        public IActionResult ChefList()
        {
            var Values = _context.Chefs.ToList();
            return Ok(Values);
        }
        [HttpPost]
        public IActionResult CreateChef(CreateChefDto chef)
        {
            var value = _mapper.Map<Chef>(chef);
            _context.Chefs.Add(value);
            _context.SaveChanges();
            return Ok("Şef sisteme Başarılı Eklendi");
        }
        [HttpDelete]
        public IActionResult DeletChef(int id)
        {
            var value = _context.Chefs.Find(id);
            _context.Chefs.Remove(value);
            _context.SaveChanges();
            return Ok("Şef Silme İşlemi Başarılı");

        }
        [HttpGet("GetChef")]
        public IActionResult GetChef(int id)
        { 
            return Ok(_context.Chefs.Find(id));
        }
        [HttpPut]
        public IActionResult UpdateChef(GetChefByIdDto chef)
        {
            var value = _mapper.Map<Chef>(chef);
            _context.Chefs.Update(value);
            _context.SaveChanges();
            return Ok("Şef Güncelleme İşlemi Başarılı");
        }

    }
}
