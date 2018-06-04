using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Practices.Unity;
using WasterWebAPI;

namespace WasteBatchConsole
{
    using System;

    class WasteStatsCounter
    {
        static void Main(string[] args)
        {
            DateTime periodStart = new DateTime();

            if (args.Length > 0)
            {
                if (!string.IsNullOrWhiteSpace(args[0]) &&
                    !DateTime.TryParseExact(args[0], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None,
                        out periodStart))
                {
                    periodStart = DateTime.Today.AddDays(-7);
                }
            }
            else
            {
                periodStart = DateTime.Today.AddDays(-7);
            }

            var container = Bootstrapper.Initialise();
            //TODO: Move batch to library and create console as Host project
            var batch = container.Resolve<BatchProcess>();

            batch.Start(periodStart);

            //Get each user


            do
            {
                batch.NextUser();

            } while (batch.InProgress());

            batch.Finish();
        }
    }
}
