using System.Data;
using System.Data.SQLite;
using System.Threading.Tasks;
using System.Web;

namespace LangDetector.Models
{
    public class SQLiteContext
    {
        private SQLiteConnection _con;
        private SQLiteCommand _sqlCmd;
        private SQLiteDataAdapter _db;
        private readonly DataSet _ds = new DataSet();
        private DataTable _dt = new DataTable();


        private void SetConnection()
        {
            string path = HttpContext.Current.Server.MapPath("/App_Data/Detector.sqlite");
            SetConnection(@"Data Source =" + path + "; Version = 3;");
        }
        private void SetConnection(string connectionString)
        {
            _con = new SQLiteConnection(connectionString);
        }

        //Ctors
        public SQLiteContext()
        {
            SetConnection();
        }
        public SQLiteContext(string connectionString)
        {
            SetConnection(connectionString);
        }

        public void ExecuteQuery(string queryString)
        {
            using (_con)
            {
                _con.Open();
                _sqlCmd = _con.CreateCommand();
                _sqlCmd.CommandText = queryString;
                _sqlCmd.ExecuteNonQuery();
                _con.Close();
            }
        }

        public void ExecuteQuery(SQLiteCommand cmd)
        {
            using (_con)
            {
                _con.Open();
                cmd.Connection = _con;
                cmd.ExecuteNonQuery();
                _con.Close();
            }
        }

        public async Task ExecuteQueryAsync(string queryString)
        {
            _con.Open();
            _sqlCmd = _con.CreateCommand();
            _sqlCmd.CommandText = queryString;
            await _sqlCmd.ExecuteNonQueryAsync();
            //sql_con.Dispose();
            _con.Close();
        }
        public DataRowCollection LoadData(string queryString)
        {
            using (_con)
            {
                _con.Open();
                _sqlCmd = _con.CreateCommand();

                _db = new SQLiteDataAdapter(queryString, _con);
                _ds.Reset();
                _db.Fill(_ds);
                _dt = _ds.Tables[0];
                DataRowCollection res = _dt.Rows;
                //Grid.DataSource = DT;
                _con.Close();
                return res;
            }
        }

        public static void CreateDatabase(string path)
        {
            SQLiteConnection.CreateFile(path);

            SQLiteConnection con = new SQLiteConnection(@"Data Source = " + path + ";Version = 3;");

            using (con)
            {
                con.Open();

                string sqlCreateTable = @"CREATE TABLE AspNetRoles(Id varchar(128) primary key, Name varchar);";
                SQLiteCommand command = new SQLiteCommand(sqlCreateTable, con);
                command.ExecuteNonQuery();

                sqlCreateTable = @"CREATE TABLE AspNetUserClaims(Id integer primary key, User_Id varchar(128), ClaimType varchar, ClaimValue varchar);";
                command = new SQLiteCommand(sqlCreateTable, con);
                command.ExecuteNonQuery();

                sqlCreateTable = @"CREATE TABLE AspNetUserLogins(LoginProvider varchar(128) primary key, UserId varchar(128), ProviderKey varchar(128));";
                command = new SQLiteCommand(sqlCreateTable, con);
                command.ExecuteNonQuery();

                sqlCreateTable = @"CREATE TABLE AspNetUserRoles(UserId varchar(128) primary key, RoleId varchar(128));";
                command = new SQLiteCommand(sqlCreateTable, con);
                command.ExecuteNonQuery();

                sqlCreateTable = @"CREATE TABLE AspNetUsers(Id varchar(128) primary key, Discriminator varchar(128)
                                    , PasswordHash varchar, SecurityStamp varchar, UserName varchar);";
                command = new SQLiteCommand(sqlCreateTable, con);
                command.ExecuteNonQuery();

                sqlCreateTable = @"CREATE TABLE RequestsInfo(UserId varchar(128) primary key, AmountOfQueries integer, RegisterDateTime varchar(50) , LastLoginDateTime varchar(50));";
                command = new SQLiteCommand(sqlCreateTable, con);
                command.ExecuteNonQuery();

                sqlCreateTable = @"CREATE TABLE Requests(Id integer primary key, UserId varchar(128), QueryResult varchar, QueryString varchars);";
                command = new SQLiteCommand(sqlCreateTable, con);
                command.ExecuteNonQuery();

                con.Close();
            }
        }        
    }
}