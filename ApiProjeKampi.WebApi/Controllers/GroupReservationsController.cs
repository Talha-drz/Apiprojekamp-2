using ApiProjeKampi.WebApi.Context;
using ApiProjeKampi.WebApi.Dtos.GroupReservationDtos;
using ApiProjeKampi.WebApi.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupReservationsController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;
        public GroupReservationsController(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GroupReservationList()
        {
            var list = _context.groupReservations.ToList();
            return Ok(list);
        }
        [HttpPost]
        public IActionResult CreateGroupReservation(CreateGroupReservationDto createGroupReservationDto)
        {
            var value = _mapper.Map<GroupReservation>(createGroupReservationDto);
            _context.groupReservations.Add(value);
            _context.SaveChanges();
            return Ok("Ekleme İşlei Başarılı");
        }
        [HttpDelete]
        public IActionResult DeleteGroupReservation(int id)
        {
            var value = _context.groupReservations.Find(id);
            _context.groupReservations.Remove(value);
            _context.SaveChanges();
            return Ok("Silme İşlei Başarılı");
        }
        [HttpGet("GetGroupReservation")]
        public IActionResult GetGroupReservation(int id)
        {
            var value = _context.groupReservations.Find(id);
            return Ok(value);
        }
        [HttpPut]
        public IActionResult UpdateGroupReservation(UpdateGroupReservationDto updateGroupReservationDto)
        {
            var value = _mapper.Map<GroupReservation>(updateGroupReservationDto);
            _context.groupReservations.Update(value);
            _context.SaveChanges();
            return Ok("güncelleme işlemi başarılı");
        }
    }
}
