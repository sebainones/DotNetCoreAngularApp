using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiUnsecure.Model;

namespace WebApiUnsecure.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private TicketContext ticketContext;

        public TicketController(TicketContext context)
        {
            ticketContext = context;
        }

        [HttpGet]
        public IEnumerable<Ticket> GetAll()
        {
            return ticketContext.Tickets.AsNoTracking().ToList();
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