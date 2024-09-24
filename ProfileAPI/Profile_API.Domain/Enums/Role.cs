using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profile_API.Domain.Enums
{
    public enum Role
    {
        [Display(Name = "Patients")]
        Patients = 0,

        [Display(Name = "Doctors")]
        Doctors = 1,

        [Display(Name = "Receptionists")]
        Receptionists =2
    }
}
