using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using MyEvents.Server.DataObjects;
using MyEvents.Server.Models;
using System.Security.Claims;

namespace MyEvents.Server.Controllers
{
   
    public class FeedbackController : TableController<Feedback>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MyEventsContext context = new MyEventsContext();
            DomainManager = new EntityDomainManager<Feedback>(context, Request);
        }

        // GET tables/Feedback
        public IQueryable<Feedback> GetAllFeedback()
        {
            var claimsPrincipal = this.User as ClaimsPrincipal;
            string sid = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier).Value;

            // Only return data rows that belong to the current user.
            return Query().Where(t => t.UserId == sid);
        }

        [Authorize]
        // GET tables/Feedback/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Feedback> GetFeedback(string id)
        {
            return Lookup(id);
        }

        [Authorize]
        // PATCH tables/Feedback/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Feedback> PatchFeedback(string id, Delta<Feedback> patch)
        {
             return UpdateAsync(id, patch);
        }

        [Authorize]
        // POST tables/Feedback
        public async Task<IHttpActionResult> PostFeedback(Feedback item)
        {
            Feedback current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        [Authorize]
        // DELETE tables/Feedback/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteFeedback(string id)
        {
             return DeleteAsync(id);
        }
    }
}
