using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using DB2019.Backend.Api.Models;

namespace DB2019.Backend.Api.Controllers
{
    public class IssuesController : ApiController
    {
        public void Post(
            string sessionId,
            [ FromBody ] NewIssueData data )
        {

        }
    }
}
