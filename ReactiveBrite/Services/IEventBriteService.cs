using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ReactiveBrite.Models;
using Refit;

namespace ReactiveBrite.Services
{
    [Headers("Accept: application/json")]
    public interface IEventBriteService
    {
        [Get("/events/search/")]
        Task<IEnumerable<Event>> GetEvents([AliasAs("q")]string search);

        [Get("/events/:id/{id}")]
        Task<Event> GetEvent(string id);
    }

}
