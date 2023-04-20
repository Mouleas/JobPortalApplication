using JobPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Data.SqlClient;

namespace JobPortal.Controllers
{
    public class JobSeekerController : Controller
    {
        IConfiguration configuration;
        SqlConnection connection;
        public JobSeekerController(IConfiguration configuration)
        {
            this.configuration = configuration;
            connection = new SqlConnection(this.configuration.GetConnectionString("JobPortal"));
        }


        [HttpPost, HttpGet]
        public IActionResult Index()
        {
            List<JobViewModel> JobList = new List<JobViewModel>();


            if (Request.Method == "POST")
            {
                Console.WriteLine("hiss");
                string role = Request.Form["role"];
                string salary = Request.Form["salary"];
                string company = Request.Form["company"];
                string location = Request.Form["location"];

                Console.WriteLine(company+ " " + location);

                if (salary == "")
                {
                    salary = "0";
                }
                int wage = Convert.ToInt32(salary);
                try
                {
                    connection.Open();
                    string querry = $"Select j.title, j.salary, c.name, c.location from Jobs j join Company c on j.company_id = c.id where (j.title like '%{role}%') and (c.name like '%{company}%') and (c.location like '%{location}%') and (j.salary > {wage});";
                    SqlCommand cmd = new SqlCommand(querry, connection);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        JobViewModel obj = new JobViewModel();
                        CompanyViewModel comp = new CompanyViewModel();


                        obj.title = "" + reader[0];
                        obj.salary = (int)reader[1];
                        comp.name = "" + reader[2];
                        comp.location = "" + reader[3];
                        obj.company = comp;
                        JobList.Add(obj);
                    }


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return View(JobList);
            }

            try
            {
                Console.WriteLine("hhh");
                connection.Open();
                SqlCommand cmd = new SqlCommand("Select j.title, j.salary, c.name, c.location from Jobs j join Company c on j.company_id = c.id;", connection);
                
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    JobViewModel obj = new JobViewModel();
                    CompanyViewModel comp = new CompanyViewModel();


                    obj.title = "" + reader[0];
                    obj.salary = (int)reader[1];
                    comp.name = "" + reader[2];
                    comp.location = "" + reader[3];
                    obj.company = comp;
                    JobList.Add(obj);
                }


            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View(JobList);
        }


        [HttpPost]
        public IActionResult JobsSeeker()
        {
            List<JobViewModel> JobList = new List<JobViewModel>();
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("Select j.title, j.salary, c.name, c.location from Jobs j join Company c on j.company_id = c.id;", connection);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    JobViewModel obj = new JobViewModel();
                    CompanyViewModel comp = new CompanyViewModel();


                    obj.title = "" + reader[0];
                    obj.salary = (int)reader[1];
                    comp.name = "" + reader[2];
                    comp.location = "" + reader[3];
                    obj.company = comp;
                    JobList.Add(obj);
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View(JobList);
        }

    }
}
