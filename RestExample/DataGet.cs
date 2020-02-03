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
        public static SqlString GetTopSubjects(string apiKey)
        {
            string url = "https://bdl.stat.gov.pl/api/v1/subjects?format=json";

            using (var client = new WebClient())
            {
                client.Headers["X-ClientId"] = apiKey;
                return new SqlString(client.DownloadString(url));
            }
        }
    }
}
