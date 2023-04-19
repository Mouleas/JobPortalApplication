using JobPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace JobPortal.Controllers
{
    public class JobController : Controller
    {
        private IConfiguration configuration;
        public JobController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        
    }
}
