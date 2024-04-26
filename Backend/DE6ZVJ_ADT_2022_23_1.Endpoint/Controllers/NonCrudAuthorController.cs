using DE6ZVJ_ADT_2022_23_1.Logic;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace DE6ZVJ_ADT_2022_23_1.Endpoint.Controllers
{
   

        [Route("[controller]/[action]")]
        [ApiController]
        public class NoncrudauthorController : ControllerBase
        {
            IAuthorLogic AL;

            public NoncrudauthorController(IAuthorLogic aL)
            {
                AL = aL;
            }




            [HttpGet]
            public string FirstName(int id)
            {
                return AL.FirstName(AL.GetAuthor(id).Name);
            }


            [HttpGet]
            public string SecondName(int id)
            {
                return AL.SecondName(AL.GetAuthor(id).Name);
            }
            [HttpGet]
            public string AllCaps(int id)
            {
                return AL.AllCaps(AL.GetAuthor(id).Name);
            }
        }
    }


