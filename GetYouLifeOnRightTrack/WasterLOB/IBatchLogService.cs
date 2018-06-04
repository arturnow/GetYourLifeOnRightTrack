using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WasterLOB
{
    public interface IBatchLogService
    {
        long StartBatch(string name);

        void StopBatch(string name);

        void StopBatch(long id);
        DateTime? GetLastRun(string name);
    }
}
