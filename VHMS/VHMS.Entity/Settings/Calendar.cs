using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VHMS.Entity
{
    public class Calendar
    {
        public string HtmlCalendar { get; set; }
        public string DisplayDate { get; set; }
        public string Date { get; set; }
        public string AddAll { get; set; }
        public int Count { get; set; }
        public int WeekNo { get; set; }
        public int DineTimeCount { get; set; }
        public string CalendarTheme { get; set; }
        public string CalendarThemeClass { get; set; }
        public string SessionState { get; set; }
    }
}
