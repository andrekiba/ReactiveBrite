using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveBrite.Models;
using Refit;

namespace ReactiveBrite.Services
{
    [Headers("Accept: application/json")]
    public interface IEventBriteService
    {
        [Get("/events")]
        Task<IEnumerable<Event>> GetEvents(string search);

        [Get("/events/{name}")]
        Task<Event> GetEvent(string name);
    }

}
