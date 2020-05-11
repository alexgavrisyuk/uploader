using System;
using Uploader.Core.Interfaces;
using Uploader.Core.Parsers;

namespace Uploader.Core.Factories
{
    public class TransactionFileParserFactory
    {
        public ITransactionFileParser Create(string fileName)
        {
            var ext = fileName.Split(".")[1].ToLowerInvariant();
            if (ext.Equals("csv"))
            {
                return new CsvTransactionFileParser();
            }
            else if (ext.Equals("xml"))
            {
                return new XmlTransactionFileParser();
            }
            throw new Exception("Unknown format");
        }
    }
}