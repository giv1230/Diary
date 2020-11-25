using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace $safeprojectname$.Models
{
    public class Meeting
    {
        public int Id { get; set; }
        [Display(Name = "שם הלקוח")]
        public string Name { get; set; }

        [Display(Name = "תאריך הפגישה")]
        [DataType(DataType.Date)]
        public DateTime MeetingDate { get; set; }

        [Display(Name = "שעת התחלה")]
        [DataType(DataType.Time)]
        public DateTime StartTime { get; set; }

        [Display(Name = "שעת סיום")]
        [DataType(DataType.Time)]
        public DateTime EndTime { get; set; }

        [Display(Name = "היום בשבוע")]
        public string Day { get; set; }

        [Display(Name = "סוג הפגישה")]
        public int MTypeID { get; set; }

        [Display(Name = "חשיבות")]
        public string Importancy { get; set; }

        [Display(Name = "סוג הפגישה")]
        public MType MType { get; set; }
    }
}