using LangDetector.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;

namespace LangDetector.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //string userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            //ApplicationDbContext context = new ApplicationDbContext();
            //ViewBag.Test = context.RequestsInfo.Find(userId).AmountOfQueries;
            ViewBag.UserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            return View(new List<ComparableGridModel>());
        }
        
        [AllowAnonymous]
        public ActionResult TopTenRequests() 
         { 
             var context = new SQLiteContext();

            //string path = Server.MapPath("/App_Data/Detector.sqlite");
            //SQLiteContext.CreateDatabase(path);

            StringBuilder str = new StringBuilder();

             //Возвращает первые 10 записей из таблицы RequestsInfo и AspNetUsers, отсортированные по amountOfQueries.
             string queryString = "select UserName, amountOfQueries, registerDateTime, lastLoginDateTime" +
                 " from RequestsInfo inner join AspNetUsers on RequestsInfo.id=AspNetUsers.Id" +
                 " order by RequestsInfo.amountOfQueries desc limit 10";

            //context.LoadData(10) возвращает массив из 10-ти записей таблицы RequestsInfo.
             foreach (DataRow row in context.LoadData(queryString)) 
             { 
                 //Формируется таблица для bootstrap
                 str.Append("<div class='row'>"); 

                 str.Append("<div class = 'col-md-3'><p>Name - " 
                         + row["UserName"] + "</p></div>");

                 str.Append("<div class = 'col-md-3'><p>Amount of querys - "
                         + row[1] + "</p></div>"); 

                 str.Append("<div class = 'col-md-3'><p>Last visit " 
                         + row[3] + "</p></div>"); 

                 //Нахождение значения среднего времени между запросами.
                 TimeSpan elapsed = DateTime.Parse(row["lastLoginDateTime"].ToString()) - DateTime.Parse(row["registerDateTime"].ToString());
                 double timeBetweenRequests = elapsed.TotalSeconds/Convert.ToInt32(row["amountOfQueries"]);
                 str.Append("<div class = 'col-md-3'><p>Average time between querys "
                         + timeBetweenRequests + "с</p></div>");

                 str.Append("</div>");
             } 
 
             ViewBag.Grid = str;  

             return View(); 
         }

        [NonAction]
        public ActionResult Test()
        {

            //SQLiteConnection con = new SQLiteConnection(@"Data Source = D:\Detector.sqlite; Version = 3;");

            //string res = "";

            //using (con)
            //{
            //    con.Open();

            //    string sqlselect = "select * from requestsinfo order by amountofqueries limit 10;";
            //    SQLiteCommand command = new SQLiteCommand(sqlselect, con);
            //    SQLiteDataReader reader = command.ExecuteReader();
            //    while (reader.Read())
            //        res = reader["amountofqueries"].ToString();

            //    con.Close();
            //}

            //ViewBag.Grid = res;
            return View("Index");
        }

        [AllowAnonymous]
        public async Task<ActionResult> FillDB()
        {
            Stopwatch timer = Stopwatch.StartNew();
            DBInitializer initializer = new DBInitializer();
            initializer.FillDb();
            timer.Stop();

            ViewBag.Time = timer.Elapsed.Seconds;

            return View();
        }

        //Добавление строки о новом пользователе в таблицу RequestsInfo.
        public ActionResult InsertRowAboutNewUserIntoRequestsInfoAsync()
        {
            //Поиск id для текущего пользователя.
            string userId = System.Web.HttpContext.Current.User.Identity.GetUserId();

            SQLiteContext context = new SQLiteContext();

            string sqlInsert = string.Format(
                    @"INSERT INTO RequestsInfo(id, amountOfQueries, registerDateTime, lastLoginDateTime) VALUES ('{0}', {1}, '{2}', '{3}');"
                    , userId, 0, DateTime.Now.ToString(CultureInfo.InvariantCulture), DateTime.Now.ToString(CultureInfo.InvariantCulture));

            context.ExecuteQuery(sqlInsert);

            return RedirectToAction("Index", "Home");
        }
    }
}