using Microsoft.SqlServer.Server;
using System.Collections;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;

namespace RestExample
{
    public class BDL
    {
        [SqlFunction(FillRowMethodName = "FetchDataFillRow", TableDefinition = "Id int, Year int, Value float")]
        public static IEnumerable FetchData(string apiKey, int variableId, string unitParentId, int yearFrom, int yearTo, int level)
        {
            string url = $"https://bdl.stat.gov.pl/api/v1/data/by-variable/{variableId}?format=xml&";
            url += string.Join("&", Enumerable.Range(yearFrom, yearTo - yearFrom + 1).Select(yr => "year=" + yr));
            url += "&page-size=100";
            url += $"&unit-level={level}";
            url += unitParentId != null ? $"&unit-parent-id={unitParentId}" : "";
            
            using (WebClient client = new WebClient())
            {
                client.Headers["X-ClientId"] = apiKey;
                
                XDocument doc = XDocument.Parse(Encoding.UTF8.GetString(client.DownloadData(url)));
                return doc.Descendants("yearVal").Select((yearVal, id) => new Item()
                {
                    Id = id + 1,
                    Year = int.Parse(yearVal.Element("year").Value),
                    Value = double.Parse(yearVal.Element("val").Value)
                });
            }
        }

        public static void FetchDataFillRow(object ob, out int Id, out int Year, out double Value)
        {
            var row = ob as Item;
            Id = row.Id;
            Year = row.Year;
            Value = row.Value;
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

    public class Item
    {
        public int Id { get; set; }

        public int Year { get; set; }

        public double Value { get; set; }

    }

}
