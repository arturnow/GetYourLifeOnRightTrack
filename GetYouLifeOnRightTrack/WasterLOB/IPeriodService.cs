using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WasterDAL.Model;

namespace WasterLOB
{
    public interface IPeriodService
    {
        Period GetDayPeriod(DateTime forDate);
        Period GetWeekPeriod(DateTime forDate);
        Period GetMonthPeriod(DateTime forDate);
        Period GetQuarterPeriod(DateTime forDate);

        long CreateDayPeriod(DateTime fordTime);
        long CreateWeekPeriod(DateTime fordTime);
        long CreateMonthPeriod(DateTime fordTime);
        long CreateQuarterPeriod(DateTime fordTime);
    }
}
