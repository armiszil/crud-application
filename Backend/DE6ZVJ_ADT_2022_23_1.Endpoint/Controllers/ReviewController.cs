using DE6ZVJ_ADT_2022_23_1.Endpoint.Services;
using DE6ZVJ_ADT_2022_23_1.Logic;
using DE6ZVJ_ADT_2022_23_1.Modells;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;

namespace DE6ZVJ_ADT_2022_23_1.Endpoint.Controllers
{
   
        [Route("[controller]")]
        [ApiController]
        public class ReviewControllers : ControllerBase
        {
            IReviewLogic RL;
            IHubContext<SignalerHub> hub;

            public ReviewControllers(IReviewLogic rL, IHubContext<SignalerHub> hub)
            {
                RL = rL;
                this.hub = hub;
            }

            // GET: /classes
            [HttpGet]
            public IEnumerable<Review> Get()
            {
                return RL.GetAllReview();
            }


            // GET /classes/5
            [HttpGet("{id}")]
            public Review Get(int id)
            {
                return RL.GetReview(id);
            }

            // POST /classes
            [HttpPost]
            public void Post([FromBody] Review value)
            {
                RL.AddNewReview(value);
                this.hub.Clients.All.SendAsync("Created", value);
            }


            // PUT /classes
            [HttpPut]
            public void Put([FromBody] Review value)
            {
                RL.UpdateReviewContent(value);
                this.hub.Clients.All.SendAsync("Updated", value);
            }


            // DELETE /classes/5
            [HttpDelete("{id}")]
            public void Delete(int id)
            {
                var classToDelete = this.RL.GetReview(id);
                RL.DeleteReview(id);
                this.hub.Clients.All.SendAsync("ClassDeleted", classToDelete);
            }
        }
    }

