using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApiUnsecure.Model;

namespace WebApiUnsecure.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private TicketContext ticketContext;

        private readonly ILogger<TicketController> _logger;

        public TicketController(TicketContext context, ILogger<TicketController> logger)
        {


            ticketContext = context;
            _logger = logger;


        }

        [HttpGet]
        public IEnumerable<Ticket> GetAll()
        {
            _logger.LogDebug("GetAll");

            IEnumerable<Ticket> tickets = null;

            try
            {
                tickets = ticketContext.Tickets.AsNoTracking().ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return tickets;
        }


        [HttpGet("{id}", Name = "GetTicket")]
        public IActionResult GetById(long id)
        {
            Ticket ticket;
            try
            {
                ticket = ticketContext.Tickets.Single(t => t.Id == id);
            }
            catch (Exception)
            {
                return NotFound();//404
            }

            return new ObjectResult(ticket);//200
        }

        [HttpPost]
        public IActionResult Create([FromBody]Ticket newTicket)
        {
            if (newTicket == null)
            {
                return BadRequest();//400
            }

            try
            {
                ticketContext.Tickets.Add(newTicket);
                ticketContext.SaveChanges();
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
            //TODO: Solve this!
            //System.InvalidOperationException: No route matches the supplied values.
            return CreatedAtRoute("GetTicket", new { id = newTicket.Id, newTicket });
        }


        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody]Ticket updatedTicket)
        {
            if (updatedTicket == null)
                return BadRequest();


            Ticket ticket;
            try
            {
                ticket = ticketContext.Tickets.Single(t => t.Id == id);
                ticket.Concert = updatedTicket.Concert;
                ticket.Artist = updatedTicket.Artist;
                ticket.Available = updatedTicket.Available;


                ticketContext.Tickets.Update(ticket);
                ticketContext.SaveChanges();

                return new NoContentResult();

            }
            catch (Exception)
            {
                return NotFound();//404
            }

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            Ticket ticket;
            try
            {
                ticket = ticketContext.Tickets.Single(t => t.Id == id);

                ticketContext.Tickets.Remove(ticket);
                ticketContext.SaveChanges();

                return new NoContentResult();

            }
            catch (Exception)
            {
                return NotFound();//404
            }
        }

        //public async Task OnGetTaskAsync()
        //{

        //}


    }
}