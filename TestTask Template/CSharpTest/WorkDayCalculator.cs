using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTest
{
    public class WorkDayCalculator : IWorkDayCalculator
    {
        public DateTime Calculate(DateTime startDate, int dayCount, WeekEnd[] weekEnds)
        {
            if (dayCount < 0)
                throw new ArgumentException("dayCount can not be negative");
            DateTime result = startDate.AddDays(dayCount - 1);
            int daysOfWeekEnds = 0;
            if (weekEnds != null)
            {
                foreach (var weekEnd in weekEnds)
                {
                    if (weekEnd.StartDate <= result.AddDays(daysOfWeekEnds) && weekEnd.StartDate >= startDate)
                    {
                        TimeSpan time = weekEnd.EndDate - weekEnd.StartDate;
                        daysOfWeekEnds++;
                        daysOfWeekEnds += time.Days;
                    }
                    else if (weekEnd.StartDate < startDate && weekEnd.EndDate >= startDate)
                    {
                        TimeSpan time = weekEnd.EndDate - startDate;
                        daysOfWeekEnds++;
                        daysOfWeekEnds += time.Days == 0 ? 1 : time.Days;
                    }
                }
            }
            result = result.AddDays(daysOfWeekEnds);
            return result;
        }
    }
}
