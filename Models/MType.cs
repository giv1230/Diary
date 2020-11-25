using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace $safeprojectname$.Models
{
    public class MType
    {
        public int Id { get; set; }
        [Display(Name = "סוג הפגישה")]
        public string MeetingType { get; set; }

        public ICollection<Meeting> Meeting { get; set; }
    }
}