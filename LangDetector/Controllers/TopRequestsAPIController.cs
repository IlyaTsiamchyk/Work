using LangDetector.Models;
using System;
using System.Web.Http;

namespace LangDetector.Controllers
{
    public class TopRequestsApiController : ApiController
    {
        // PUT: api/TopRequestsAPI
        public void Put([FromBody]string id)
        {
            if (id == null)
            {
                return;
            }
            SQLiteContext context = new SQLiteContext();

            string queryString = $@"Update RequestsInfo Set amountOfQueries = amountOfQueries+1, lastLoginDateTime='{DateTime.Now}'
                                    Where UserId='{id}';";

            context.ExecuteQuery(queryString);
        }

    }
}
