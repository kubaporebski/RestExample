using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RestExample
{
    public class BDL
    {
        [SqlFunction]
        public static SqlString FetchTopSubjects(string apiKey)
        {
            string url = "https://bdl.stat.gov.pl/api/v1/subjects?format=json&page-size=100";

            using (var client = new WebClient())
            {
                client.Headers["X-ClientId"] = apiKey;

                var utf8string = Encoding.UTF8.GetString(client.DownloadData(url));
                return new SqlString(utf8string);
            }
        }

        [SqlFunction]
        public static SqlString FetchMeasureUnits(string apiKey)
        {
            string url = "https://bdl.stat.gov.pl/api/v1/measures?format=json&page-size=100";

            using (var client = new WebClient())
            {
                client.Headers["X-ClientId"] = apiKey;

                var utf8string = Encoding.UTF8.GetString(client.DownloadData(url));
                return new SqlString(utf8string);
            }
        }
    }
}
