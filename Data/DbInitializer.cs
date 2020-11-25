using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using $safeprojectname$.Models;

namespace $safeprojectname$.Data
{
    public class DbInitializer
    {
        public static void Initialize(YomanContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Meeting.Any())
            {
                return;   // DB has been seeded
            }

            var meetings = new Meeting[]
            {
            new Meeting{Name="יוסי",MeetingDate=DateTime.Parse("2015-07-16"), StartTime=DateTime.Parse("08:00:00"), EndTime=DateTime.Parse("09:00:00"), MTypeID=1, Day="יום ראשון", Importancy="חשובה"},
            new Meeting{Name="מייק",MeetingDate=DateTime.Parse("2015-02-11"), StartTime=DateTime.Parse("08:00:00"), EndTime=DateTime.Parse("09:00:00"), MTypeID=2, Day="יום רביעי", Importancy="חשובה"},
            new Meeting{Name="בועז",MeetingDate=DateTime.Parse("2017-01-07"), StartTime=DateTime.Parse("08:00:00"), EndTime=DateTime.Parse("09:00:00"), MTypeID=4, Day="יום חמישי", Importancy="חשובה"},
            new Meeting{Name="רותי",MeetingDate=DateTime.Parse("2018-09-21"), StartTime=DateTime.Parse("08:00:00"), EndTime=DateTime.Parse("09:00:00"), MTypeID=4, Day="יום שישי", Importancy="חשובה מאוד"},
            new Meeting{Name="ירון",MeetingDate=DateTime.Parse("2020-09-22"), StartTime=DateTime.Parse("08:00:00"), EndTime=DateTime.Parse("09:00:00"), MTypeID=1, Day="יום ראשון", Importancy="לא חשובה"},
            new Meeting{Name="נעמה",MeetingDate=DateTime.Parse("2017-05-03"), StartTime=DateTime.Parse("08:00:00"), EndTime=DateTime.Parse("09:00:00"), MTypeID=3, Day="יום שני", Importancy="חשובה"},
            };
            foreach (Meeting s in meetings)
            {
                context.Meeting.Add(s);
            }
            context.SaveChanges();

            var meetingtypes = new MType[]
            {
            new MType{MeetingType="שיווק"},
            new MType{MeetingType="פרסום"},
            new MType{MeetingType="פיתוח"},
            new MType{MeetingType="הצעת מחיר"},
            new MType{MeetingType="מכירות"},
            };
            foreach (MType c in meetingtypes)
            {
                context.MType.Add(c);
            }
            context.SaveChanges();
        }
    }
}
