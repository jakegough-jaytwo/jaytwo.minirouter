using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace jaytwo.MiniRouter.example.AspNet4_6_1.Controllers
{
    public class ValuesController : ApiController
    {
        public IEnumerable<string> Get()
        {
            yield return "Hello";
            yield return "World";
        }
    }
}
