using DE6ZVJ_ADT_2022_23_1.Logic;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DE6ZVJ_ADT_2022_23_1.Endpoint.Controllers
{
   
        [Route("[controller]/[action]")]
        [ApiController]
        public class NoncrudbookController : ControllerBase
        {
            IBookLogic BL;

            public NoncrudbookController(IBookLogic bL)
            {
                BL = bL;
            }

           

            
            [HttpGet]
            public int LongestBookQuery()
            {
                return BL.LongestBookQuery(BL.GetAllBooks());
            }

           
            [HttpGet]
            public int ShortestBookQuery()
            {
                return BL.ShortestBookQuery(BL.GetAllBooks());
            }
        }
    }

