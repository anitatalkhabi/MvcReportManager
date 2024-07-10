using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Helpers
{
    public class PersianCalenderHelper 
    {
        public string ConvertToPersianDate(DateTime date)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            string persianDate = string.Format("{0}/{1}/{2}",
                persianCalendar.GetYear(date),
                persianCalendar.GetMonth(date).ToString("00"),
                persianCalendar.GetDayOfMonth(date).ToString("00")


                );

            return persianDate;
        }
    }
}