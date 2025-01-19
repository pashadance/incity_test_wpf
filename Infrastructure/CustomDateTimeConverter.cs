using Newtonsoft.Json;
using System.Globalization;

namespace incity_test.Infrastructure;

public class CustomDateTimeConverter : JsonConverter
{
    private readonly string _dateFormat;

    public CustomDateTimeConverter(string dateFormat)
    {
        _dateFormat = dateFormat;
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(DateTime) || objectType == typeof(DateTime?);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var dateStr = reader.Value.ToString();
        if (DateTime.TryParseExact(dateStr, _dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
        {
            return result;
        }

        throw new JsonSerializationException($"не правильноый формат: {dateStr}");
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        writer.WriteValue(((DateTime)value).ToString(_dateFormat));
    }
}