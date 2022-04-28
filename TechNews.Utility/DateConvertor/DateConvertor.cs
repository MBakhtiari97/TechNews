using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechNews.Utility.DateConvertor
{
    public static class DateConvertor 
    {
        public static string ToShamsiDate(this DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(date) 
                   + "/" + pc.GetMonth(date).ToString("00")
                   + "/" + pc.GetDayOfMonth(date).ToString("00");
        }
        public static DateTime ToMiladi(this DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();
            DateTime dt = new DateTime(date.Year, date.Month, date.Day, pc);

            return dt;
        }
    }
}
