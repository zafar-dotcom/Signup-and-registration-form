using Microsoft.Build.Framework;

namespace Complete.Models
{
    public class Experience_model
    {
        public int Exp_id { get; set; }
        public int App_id { get; set; }
        public int MyProperty { get; set; }
        public string Company_name { get; set; }
        public string Designation { get; set; }
        [Required]
        public int Years_worked { get; set; }
        public Applicant Applicatn { get; set; }
    }
}
