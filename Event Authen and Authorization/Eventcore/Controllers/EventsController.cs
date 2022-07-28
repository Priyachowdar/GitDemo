using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eventcore.Models;
using Eventcore.Repositories;
using Eventcore.Specifications;
using Microsoft.AspNetCore.Authorization;

namespace Eventcore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IRepository<Event> eventRepository;
        private object context;

        public EventsController(IRepository<Event> eventRepository)
        {
            this.eventRepository = eventRepository;
        }
        [HttpGet]
        [ProducesResponseType(200,Type=typeof(List<Event>))]
        [AllowAnonymous]
        public IActionResult GetEvents()
        {
            var result = eventRepository.GetAll();
            return Ok(result);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(Event))]
        [AllowAnonymous]
        public async Task<IActionResult> GetEvent(int id)
        {
            var @event = await eventRepository.GetByIdAsync(id);
            if (@event == null)
                return NotFound();

            return Ok(@event);
        }
        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(201)]
        [Authorize(Roles = "Employees")]
        public async Task<IActionResult> SaveEvent(Event evn)
        {
            eventRepository.Add(evn);
            await eventRepository.SaveAsync();
            return StatusCode(201);
        }
        [HttpPut]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(Event))]
        [Authorize(Roles = "Admin,Employees")]
        public async Task<IActionResult> UpdateEvent(Event evn)
        {
            var @event = await eventRepository.GetByIdAsync(evn.Id); 
            if (@event == null)
                return NotFound();
            @event.Id = evn.Id;
            @event.StudentId = evn.StudentId;
            @event.StudentName = evn.StudentName;
            @event.EventId = evn.EventId;
            @event.EventName = evn.EventName;
            @event.DepartmentId = evn.DepartmentId;
            @event.RegisteredDate = evn.RegisteredDate;
            @event.EmailAddress = evn.EmailAddress;
            @event.Contact=evn.Contact;
            eventRepository.Update(@event);
            await eventRepository.SaveAsync();
            return Ok(@event);
            
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var @event = await eventRepository.GetByIdAsync(id);
            if (@event == null)
                return NotFound();
            eventRepository.Remove(@event);
            await eventRepository.SaveAsync();


            return NoContent();
        }
    }
}
