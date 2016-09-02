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
    public class SpeakerController : TableController<Speaker>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MyEventsContext context = new MyEventsContext();
            DomainManager = new EntityDomainManager<Speaker>(context, Request, enableSoftDelete:true);
        }

        // GET tables/Speaker
        public IQueryable<Speaker> GetAllSpeaker()
        {
            return Query(); 
        }

        // GET tables/Speaker/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Speaker> GetSpeaker(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Speaker/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Speaker> PatchSpeaker(string id, Delta<Speaker> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/Speaker
        public async Task<IHttpActionResult> PostSpeaker(Speaker item)
        {
            Speaker current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Speaker/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteSpeaker(string id)
        {
             return DeleteAsync(id);
        }
    }
}
