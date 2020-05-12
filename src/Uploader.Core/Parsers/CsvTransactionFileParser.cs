using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Uploader.Core.CsvMappers;
using Uploader.Core.CsvModels;
using Uploader.Core.Helpers;
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
                    csv.Configuration.HasHeaderRecord = false;

                    RegisterClassMaps(csv);
                    
                    try
                    {
                        while (csv.Read())
                        {
                            var record = csv.GetRecord<CsvTransaction>();
                            
                            var newTransaction = new Transaction()
                            {
                                Id = record.Id,
                                Amount = Decimal.Parse(record.Amount),
                                CurrencyCode = record.CurrencyCode,
                                TransactionDate = DateTime.ParseExact(record.TransactionDate, Constants.CsvDateTimeFormat, CultureInfo.InvariantCulture),
                                Status = record.Status
                            };
                            
                            transactions.Add(newTransaction);
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