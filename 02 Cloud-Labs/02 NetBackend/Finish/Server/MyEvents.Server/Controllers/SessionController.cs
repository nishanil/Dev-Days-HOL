using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using MyEvents.Server.DataObjects;
using MyEvents.Server.Models;

namespace MyEvents.Server.Controllers
{
    public class SessionController : TableController<Session>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MyEventsContext context = new MyEventsContext();
            DomainManager = new EntityDomainManager<Session>(context, Request, enableSoftDelete:true);
        }

        // GET tables/Session
        public IQueryable<Session> GetAllSession()
        {
            return Query(); 
        }

        // GET tables/Session/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Session> GetSession(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Session/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Session> PatchSession(string id, Delta<Session> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/Session
        public async Task<IHttpActionResult> PostSession(Session item)
        {
            Session current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Session/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteSession(string id)
        {
             return DeleteAsync(id);
        }
    }
}
