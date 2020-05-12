using System;
using System.Globalization;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Uploader.Core.CsvModels;
using Uploader.Domain.Entities;

namespace Uploader.Core.CsvMappers
{
    public class TransactionMap : ClassMap<CsvTransaction>
    {
        public TransactionMap()
        {
            Map(s => s.Id).TypeConverter<CustomStringConverter>();
            Map(s => s.Amount).TypeConverter<CustomStringConverter>();
            Map(s => s.CurrencyCode).TypeConverter<CustomStringConverter>();
            Map(s => s.TransactionDate).TypeConverter<CustomStringConverter>();
            Map(s => s.Status).TypeConverter<CustomStringConverter>();
        }
    }


    public class CustomStringConverter : StringConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            return text;
        }

        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            return value.ToString();
        }
    }
    
    public class PricingConverter : DecimalConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            NumberStyles? numberStyle = memberMapData.TypeConverterOptions.NumberStyle;
            NumberStyles style = numberStyle.HasValue ? numberStyle.GetValueOrDefault() : NumberStyles.Float;
            Decimal result;

            text = text.Trim();
            var price = text.Replace('$', ' ');
            if (price.Contains(','))
            {
                price = price.Replace(',', '.');
            }

            var value = string.Concat(price.Where(c => !char.IsWhiteSpace(c)));
            if (value[1] == '.')
            {
                value = value.Remove(1, 1);
            }

            if (Decimal.TryParse(value, style, (IFormatProvider) memberMapData.TypeConverterOptions.CultureInfo,
                out result))
                return (object) result;
            return base.ConvertFromString(text, row, memberMapData);
        }
    }
}