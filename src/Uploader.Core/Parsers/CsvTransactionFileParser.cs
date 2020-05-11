using System;
using System.Collections.Generic;
using System.IO;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Uploader.Core.CsvMappers;
using Uploader.Core.Interfaces;
using Uploader.Domain.Entities;

namespace Uploader.Core.Parsers
{
    public class CsvTransactionFileParser : ITransactionFileParser
    {
        public IEnumerable<Transaction> ParseAll(IFormFile file)
        {
            var transactions = new List<Transaction>();

            var stream = file.OpenReadStream();
            using (var reader = new StreamReader(stream))
            {
                using (var csv = new CsvReader(reader))
                {
                    RegisterClassMaps(csv);
                    try
                    {
                        csv.Read();
                        while (csv.Read())
                        {
                            var record = csv.GetRecord<Transaction>();
                            transactions.Add(record);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("File contains invalid data");
                    }
                }
            }

            return transactions;
        }
        
        /// <summary>
        /// Put you map class here
        /// </summary>
        /// <param name="csv"></param>
        private static void RegisterClassMaps(CsvReader csv)
        {
            csv.Configuration.RegisterClassMap<TransactionMap>();
        }
    }
}