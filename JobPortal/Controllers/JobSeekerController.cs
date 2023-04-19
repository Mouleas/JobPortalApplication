using JobPortal.Models;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Index()
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
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return View(JobList);
        }


    }
}
