using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using Microsoft.AspNetCore.Http;
using Uploader.Core.Helpers;
using Uploader.Core.Interfaces;
using Uploader.Domain.Entities;

namespace Uploader.Core.Parsers
{
    public class XmlTransactionFileParser : ITransactionFileParser
    {
        public IEnumerable<Transaction> ParseAll(IFormFile file)
        {
            var transactions = new List<Transaction>();
            
            XmlDocument doc = new XmlDocument();
            doc.Load(file.OpenReadStream());
            XmlNodeList nodes = doc.DocumentElement.SelectNodes("/Transactions/Transaction");

            foreach (XmlNode node in nodes)
            {
                var id = node.Attributes[0].InnerText;

                var details = node.SelectSingleNode("PaymentDetails");

                var textAmount = details.SelectSingleNode("Amount").InnerText;
                var currencyCode = details.SelectSingleNode("CurrencyCode").InnerText;
                
                var status = node.SelectSingleNode("Status").InnerText;

                var textTransactionDate = node.SelectSingleNode("TransactionDate").InnerText;

                DateTime transactionDate = DateTime.Now;
                if (DateTime.TryParse(textTransactionDate, out var parsedValue))
                {
                    transactionDate = parsedValue;
                }
                
                transactions.Add(new Transaction
                {
                    Id = id,
                    Amount = decimal.Parse(textAmount),
                    CurrencyCode = currencyCode,
                    TransactionDate = transactionDate,
                    Status = status
                });
            }
            
            return transactions;
        }
    }
}