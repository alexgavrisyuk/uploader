using System.Collections.Generic;

namespace Uploader.Core.Helpers
{
    public class Constants
    {
        public const string CsvDateTimeFormat = "dd/MM/yyyy hh:mm:ss";
        public const string XmlDateTimeFormat = "yyyy-MM-ddThh:mm:ss";

        public static Dictionary<List<string>, string> StatusMap = new Dictionary<List<string>, string>
        {
            { new List<string>{"Approved"}, "A" },
            { new List<string>{"Failed", "Rejected"}, "R" },
            { new List<string>{"Finished", "Done"}, "D" },
        };
    }
}