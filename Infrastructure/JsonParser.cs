using incity_test.Model;
using Newtonsoft.Json;

namespace incity_test.Infrastructure;

public static class JsonParser
{
    public static Document Parse(this string json)
    {
        var data = JsonConvert.DeserializeObject<Document>(json);
        var textDate = JsonConvert.DeserializeObject<InnerModel.TextData>(data.TextData);

        if (textDate!=null)
        {
            data.Phone = textDate.TextualData.ContactInformation
                .FirstOrDefault(y => y.ContactKind.Name == "Телефон")?.Presentation;
            data.ActualAddress = textDate.TextualData.ContactInformation
                .FirstOrDefault(y => y.ContactKind.Name == "Фактический адрес")?.Presentation;
        }

        return data;
    }
}