using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;


namespace RdpRealTimePricing.Extensions
{
    public static class ObjectExtensions
    {
        public static T ToObject<T>(this IDictionary<string, object> source)
            where T : class, new()
        {
            var tmpObject = new T();
            var tmpObjectType = tmpObject.GetType();

            foreach (var item in source)
            {
                try
                {
                    var property = tmpObjectType.GetProperty(item.Key);
                    if (property == null) continue;
                    if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        var genericType = property.PropertyType.GetGenericArguments()[0];
                        if (item.Value != null)
                            property.SetValue(tmpObject, Convert.ChangeType(item.Value, genericType), null);
                        else
                            property.SetValue(tmpObject, item.Value, null);
                    }
                    else
                    {
                        property.SetValue(tmpObject, Convert.ChangeType(item.Value, property.PropertyType), null);
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception($" Error in ToObject method {ex.Message}");
                }
            }

            return tmpObject;
        }

        public static IDictionary<string, object> AsDictionary(this object source, BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
        {
            return source.GetType().GetProperties(bindingAttr).ToDictionary
            (
                propInfo => propInfo.Name,
                propInfo => propInfo.GetValue(source, null)
            );

        }
        private static readonly Dictionary<string, CultureInfo> IsoCurrenciesToACultureMap =
            CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                .Select(c => new { c, new RegionInfo(c.Name).ISOCurrencySymbol })
                .GroupBy(x => x.ISOCurrencySymbol)
                .ToDictionary(g => g.Key, g => g.First().c, StringComparer.OrdinalIgnoreCase);

        public static string FormatCurrency(dynamic amount, string currencyCode,bool showdigit=true)
        {
            try
            {
                if (amount == null) return string.Empty;
                if (string.IsNullOrEmpty(currencyCode)) return $"{amount}";
                if (showdigit)
                {
                    return IsoCurrenciesToACultureMap.TryGetValue(currencyCode, out var culture)
                        ? (string) string.Format(culture, "{0:C4}", amount)
                        : (string) amount.ToString();
                }
                else
                {
                    return IsoCurrenciesToACultureMap.TryGetValue(currencyCode, out var culture)
                        ? (string) string.Format(culture, "{0:C}", amount)
                        : (string) amount.ToString();
                }
            }
            catch
            {
            }

         
            return amount.ToString();
        }
      

    }
}
