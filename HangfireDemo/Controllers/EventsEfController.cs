using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Database;
using System.Linq;
using System.Threading.Tasks;

namespace Playground.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/eventsef")]
    [ApiController]
    public class EventsEfController : ControllerBase
    {
        private EventContext _eventContext;

        public EventsEfController(EventContext eventContext)
        {
            _eventContext = eventContext;
        }

        [HttpGet("{eventId}")]
        public async Task<IActionResult> Get(int eventId)
        {
            var eventInfo = await _eventContext.Events.Where(o => o.Id == eventId).SingleOrDefaultAsync();
            return Ok(eventInfo);
        }

    }
}