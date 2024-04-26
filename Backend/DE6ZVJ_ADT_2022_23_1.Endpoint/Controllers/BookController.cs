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
        public class BookControllers : ControllerBase
        {
            IBookLogic BL;
            IHubContext<SignalerHub> hub;

            public BookControllers(IBookLogic bL, IHubContext<SignalerHub> hub)
            {
                BL = bL;
                this.hub = hub;
            }

          
            [HttpGet]
            public IEnumerable<Book> Get()
            {
                return BL.GetAllBooks();
            }


         
            [HttpGet("{id}")]
            public Book Get(int id)
            {
                return BL.GetBook(id);
            }

          
            [HttpPost]
            public void Post([FromBody] Book value)
            {
                BL.AddNewBook(value);
                this.hub.Clients.All.SendAsync("BookCreated", value);
            }


         
         


            [HttpDelete("{id}")]
            public void Delete(int id)
            {
                var classToDelete = this.BL.GetBook(id);
                BL.DeleteBook(id);
                this.hub.Clients.All.SendAsync("BookDeleted", classToDelete);
            }
        }
    }

