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
        public class Authorcontroller : ControllerBase
        {
            IAuthorLogic AL;
            IHubContext<SignalerHub> hub;

            public Authorcontroller(IAuthorLogic aL, IHubContext<SignalerHub> hub)
            {
                AL =aL;
                this.hub = hub;
            }

            [HttpGet]
            public IEnumerable<Author> Get()
            {
                return AL.GetAllAuthors();
            }


         
            [HttpGet("{id}")]
            public Author Get(int id)
            {
                return AL.GetAuthor(id);
            }

           
            [HttpPost]
            public void Post([FromBody] Author value)
            {
                AL.AddNewAuthor(value);
                this.hub.Clients.All.SendAsync("AuthorCreated", value);
            }


   
            [HttpPut]
            public void Put([FromBody] Author value)
            {
                AL.UpdateAuthorName(value);
                this.hub.Clients.All.SendAsync("AuthorUpdated", value);
            }


          
            [HttpDelete("{id}")]
            public void Delete(int id)
            {
                var classToDelete = this.AL.GetAuthor(id);
                AL.DeleteAuthor(id);
                this.hub.Clients.All.SendAsync("AuthorDeleted", classToDelete);
            }
        }
    }
