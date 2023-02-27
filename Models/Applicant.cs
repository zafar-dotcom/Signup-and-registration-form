using Microsoft.Build.Framework;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Complete.Models
{
    public class Applicant
    {
        public int App_id { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [StringLength(150)]
        public string Name { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [StringLength(10)]
        public string Gender { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [Range(25,55, ErrorMessage ="NO Vacant post for your age,sorry")]
        [DisplayName("Age in years")]
        public int Age { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [StringLength(150)]
        public string Qualification { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [Range(5,25, ErrorMessage ="Experience dosnt match")]
        [DisplayName("Total Experience in years")]
        public string Total_experience { get; set; }

        public List<Experience_model> Experience { get; set; }
    }
}
