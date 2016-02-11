using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace LangDetector.Models
{
    public class DBInitializer
    {
        readonly StringBuilder _builder = new StringBuilder();
        readonly Random _random = new Random();
        readonly SQLiteContext _context = new SQLiteContext();
        public async void FillDb()
        {
            for (var i = 0; i < 5; i++)
            {
                var userId = CreateUser();
                CreateRequestsInfoRow(userId);
                
                for (int k = 0, requestAmount = _random.Next(1000, 10000); k < requestAmount; k++)
                {
                    await CreateRequest(userId);
                }
            }
        }

        public string CreateUser()
        {
            var model = new RegisterViewModel();
            for (int j = 0; j < 9; j++)
            {
                _builder.Append((char)_random.Next(0x041, 0x005A));
            }

            model.UserName = _builder.ToString();
            model.Password = "qwe123";
            model.ConfirmPassword = "qwe123";

            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = new ApplicationUser() { UserName = model.UserName };
            userManager.Create(user, model.Password);
            return userManager.FindByName(_builder.ToString()).Id;

        }

        public void CreateRequestsInfoRow(string userId)
        {
            string sqlInsert = string.Format(
                    @"INSERT INTO RequestsInfo(id, amountOfQueries, registerDateTime, lastLoginDateTime) VALUES ('{0}', {1}, '{2}', '{3}');"
                    , userId, 0, DateTime.Now.ToString(CultureInfo.InvariantCulture), DateTime.Now.ToString(CultureInfo.InvariantCulture));

            _context.ExecuteQuery(sqlInsert);
        }

        public async Task CreateRequest(string userId)
        {
            List<Language> languages = new List<Language>() { new EnglishLanguage(), new SpanishLanguage(), new PortugalLanguage(), new RussianLanguage(), new BulgarianLanguage() };
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            LangDetector detector = new LangDetector();

            var gridModel = new List<ComparableGridModel>();
            _builder.Clear();

            string queryString = string.Format(@"Update RequestsInfo Set amountOfQueries = amountOfQueries+1, lastLoginDateTime='{0}'
                                    Where id='{1}';", DateTime.Now, userId);

            await _context.ExecuteQueryAsync(queryString);

            for (int z = 0; z < 10; z++)
            {
                _builder.Append((char)_random.Next(0x041, 0x015A));
            }
            foreach (Language language in languages)
            {
                gridModel.Add(detector.Detect(_builder.ToString(), language));
            }
            string JSonString = serializer.Serialize(gridModel);

            queryString = string.Format(@"INSERT INTO Requests(UserId, QueryString, QueryResult) VALUES ('{0}', '{1}', '{2}');"
                    , userId, _builder, JSonString);

            //context.ExecuteQuery(queryString);
            await _context.ExecuteQueryAsync(queryString);

            _builder.Clear();
        }
    }
}