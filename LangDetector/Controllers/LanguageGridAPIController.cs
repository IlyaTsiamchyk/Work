using System;
using LangDetector.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace LangDetector.Controllers
{
    public class LanguageGridApiController : ApiController
    {
        readonly List<Language> _languages = new List<Language>() { new EnglishLanguage(), new SpanishLanguage(), new PortugalLanguage(), new RussianLanguage(), new BulgarianLanguage() };
        readonly Models.LangDetector _detector = new Models.LangDetector();
        private readonly List<ComparableGridModel> _gridModel = new List<ComparableGridModel>();

        public string Get()
        {
            return "Hello";
        }

        [HttpPost]
        public HttpResponseMessage LanguageGrid([FromUri]string id, [FromBody]string requestWord)
        {
            if (string.IsNullOrEmpty(requestWord)) return Request.CreateResponse(HttpStatusCode.BadRequest);

            foreach (Language language in _languages)
            {
                _gridModel.Add(_detector.Detect(requestWord, language));
            }

            if (string.IsNullOrEmpty(id))
                return Request.CreateResponse(HttpStatusCode.NoContent); //TODO: Found out a right status code.

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string JSonString = serializer.Serialize(_gridModel);

            Models.SQLiteContext context = new Models.SQLiteContext();
            //Parametrized query.
            string queryString = "INSERT INTO Requests(UserId, QueryString, QueryResult) VALUES ('@id', '@requestWord', '@JSonString');";

            //Add params.
            //TODO: Move it to model.
            SQLiteCommand _cmd = new SQLiteCommand(queryString);
            _cmd.Parameters.Add(new SQLiteParameter("@id", SqlDbType.VarChar) { Value = id });
            _cmd.Parameters.Add(new SQLiteParameter("@requestWord", SqlDbType.VarChar) { Value = requestWord });
            _cmd.Parameters.Add(new SQLiteParameter("@JSonString", SqlDbType.VarChar) { Value = JSonString });

            try
            {
                context.ExecuteQuery(_cmd);
                return Request.CreateResponse(HttpStatusCode.Created, _gridModel);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, _gridModel); //TODO: Found out a right status code.
            }

        }
    }
}
