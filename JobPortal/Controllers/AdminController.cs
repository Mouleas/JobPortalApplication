using JobPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace JobPortal.Controllers
{
    public class AdminController : Controller
    {
        private IConfiguration configuration;
        public AdminController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IActionResult Admin()
        {
            List<CompanyViewModel> companyList = new List<CompanyViewModel>();
            try
            {
                String conn = configuration.GetConnectionString("JobPortal");
                SqlConnection connection = new SqlConnection(conn);
                connection.Open();

                string query = "SELECT * FROM COMPANY";
                SqlCommand sqlCommand = new SqlCommand(query, connection);

                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    CompanyViewModel obj = new CompanyViewModel();
                    obj.id = (int)reader[0];
                    obj.name = "" + reader[1];
                    obj.location = "" + reader[2];

                    companyList.Add(obj);
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            return View(companyList);
        }

        public IActionResult AddCompany()
        {
            return View();
        }

        public IActionResult DeleteCompany(int id)
        {
            try
            {
                String conn = configuration.GetConnectionString("JobPortal");
                SqlConnection connection = new SqlConnection(conn);
                connection.Open();
                string query = $"DELETE FROM COMPANY WHERE id={id}";
                SqlCommand sqlCommand = new SqlCommand(query, connection);
                sqlCommand.ExecuteNonQuery();
                connection.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("Admin");
        }

        [HttpPost]
        public IActionResult AddCompanyToTable() 
        {
            Console.WriteLine("In post");
            try
            {
                String conn = configuration.GetConnectionString("JobPortal");
                SqlConnection connection = new SqlConnection(conn);
                connection.Open();

                string companyName = Request.Form["name"];
                string location = Request.Form["location"];

                string query = $"INSERT INTO COMPANY VALUES('{companyName}','{location}')";
                SqlCommand sqlCommand = new SqlCommand(query, connection);

                sqlCommand.ExecuteNonQuery();

                connection.Close();

            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
            
            return RedirectToAction("Admin"); 
        }

        public IActionResult Job(int id, string companyName)
        {
            int Id = id;

            List<JobViewModel> JobList = new List<JobViewModel>();
            try
            {
                String conn = configuration.GetConnectionString("JobPortal");
                SqlConnection connection = new SqlConnection(conn);
                connection.Open();

                string query = $"SELECT * FROM Jobs where company_id = {Id};";
                SqlCommand sqlCommand = new SqlCommand(query, connection);

                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    JobViewModel obj = new JobViewModel();
                    obj.id = (int)reader[0];
                    obj.title = "" + reader[1];
                    obj.salary = (int)reader[2];

                    JobList.Add(obj);
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            ViewData["id"] = Id;
            ViewData["companyName"] = companyName;
            return View(JobList);
        }

        public IActionResult AddJob(int id)
        {

            ViewData["id"] = id;
            return View();
        }

        [HttpPost]
        public IActionResult AddJobToTable()
        {
            Console.WriteLine("In job post");
            try
            {
                String conn = configuration.GetConnectionString("JobPortal");
                SqlConnection connection = new SqlConnection(conn);
                connection.Open();

                string title = Request.Form["title"];
                int salary = Convert.ToInt32(Request.Form["salary"]);
                int id = Convert.ToInt32(Request.Form["id"]);

                string query = $"INSERT INTO jobs VALUES('{title}',{salary},{id})";
                SqlCommand sqlCommand = new SqlCommand(query, connection);

                sqlCommand.ExecuteNonQuery();

                connection.Close();

                return RedirectToAction("job","Admin", new {id=id});

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return RedirectToAction("Error","Home");
        }




        public IActionResult EditCompany(int id)
        {
            CompanyViewModel obj = new CompanyViewModel();
            try
            {
                String conn = configuration.GetConnectionString("JobPortal");
                SqlConnection connection = new SqlConnection(conn);
                connection.Open();

                string query = $"SELECT * FROM COMPANY where id = {id}";
                SqlCommand sqlCommand = new SqlCommand(query, connection);

                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    obj.id = (int)reader[0];
                    obj.name = "" + reader[1];
                    obj.location = "" + reader[2];

                    
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View(obj);
        }


        [HttpPost]
        public IActionResult EditCompanyToTable()
        {
            Console.WriteLine("In edit post");

            try
            {
                String conn = configuration.GetConnectionString("JobPortal");
                SqlConnection connection = new SqlConnection(conn);
                connection.Open();

                string company = Request.Form["company"];
                string location = Request.Form["location"];
                int id = Convert.ToInt32(Request.Form["id"]);

                string query = $"update company set name = '{company}' , location ='{location}' where id = {id};";
                SqlCommand sqlCommand = new SqlCommand(query, connection);

                sqlCommand.ExecuteNonQuery();

                connection.Close();

                return RedirectToAction("Admin");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return RedirectToAction("Error", "Home");
        }

      
        public IActionResult EditJob(int id)
        {
            JobViewModel obj = new JobViewModel();
            try
            {
                String conn = configuration.GetConnectionString("JobPortal");
                SqlConnection connection = new SqlConnection(conn);
                connection.Open();

                string query = $"SELECT * FROM Jobs where id = {id}";
                SqlCommand sqlCommand = new SqlCommand(query, connection);

                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    obj.id = (int)reader[0];
                    obj.title = "" + reader[1];
                    obj.salary = (int)reader[2];
                    obj.companyId = (int)reader[3];



                }
                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View(obj);
        }


        [HttpPost]
        public IActionResult EditJobToTable()
        {
            Console.WriteLine("In job post");
            try
            {
                String conn = configuration.GetConnectionString("JobPortal");
                SqlConnection connection = new SqlConnection(conn);
                connection.Open();

                string title = Request.Form["title"];
                int salary = Convert.ToInt32(Request.Form["salary"]);
                int id = Convert.ToInt32(Request.Form["id"]);
                int cid = Convert.ToInt32(Request.Form["cid"]);
                string query = $"update Jobs set title = '{title}' , salary = '{salary}' where id = {id}";
                SqlCommand sqlCommand = new SqlCommand(query, connection);

                sqlCommand.ExecuteNonQuery();

                connection.Close();

                return RedirectToAction("job", "Admin", new { id = cid });

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return RedirectToAction("Error", "Home");
        }


        public IActionResult DeleteJob(int id)
        {
            try
            {
                String conn = configuration.GetConnectionString("JobPortal");
                SqlConnection connection = new SqlConnection(conn);
                connection.Open();
                string query = $"DELETE FROM JOBS WHERE id={id}";
                SqlCommand sqlCommand = new SqlCommand(query, connection);
                sqlCommand.ExecuteNonQuery();
                connection.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("Admin");
        }

    }
}
