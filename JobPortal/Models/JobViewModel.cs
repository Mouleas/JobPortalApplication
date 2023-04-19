namespace JobPortal.Models
{
    public class JobViewModel
    {
        public int id { get; set; }
        public string title { get; set; }
        public int salary { get; set; }
        public int companyId { get; set; }
        public CompanyViewModel? company;
    }
}
