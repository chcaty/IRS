using System;
using System.Collections.Generic;
using System.Text;

namespace IRS.Common.Time
{
    public class TimeMethod
    {
        public static List<string> GetDateList(string startAt,string endAt)
        {
            List<String> dates = new List<string>();
            try
            {
                DateTime startDate = Convert.ToDateTime(startAt);
                DateTime endDate = Convert.ToDateTime(endAt);
                for (DateTime t = startDate; t <= endDate; t = t.AddDays(1))
                {
                    dates.Add(t.ToString("yyyy-MM-dd"));
                }

            }
            catch (Exception e)
            {
                return null;
            }
            return dates;
        }
    }
}
