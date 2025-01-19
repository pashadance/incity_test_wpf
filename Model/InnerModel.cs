using Newtonsoft.Json;

namespace incity_test.Model;

public class InnerModel
{
    public class TextData
    {
        [JsonProperty("ТекстовыеДанные")]
        public TextualData TextualData { get; set; }
    }

    public class TextualData
    {
        [JsonProperty("КонтактнаяИнформация")]
        public List<ContactInformation> ContactInformation { get; set; }
    }

    public class ContactInformation
    {
        [JsonProperty("ВидКИ")]
        public ContactKind ContactKind { get; set; }

        [JsonProperty("ПредставлениеКИ")]
        public string Presentation { get; set; }
    }
    
    public class ContactKind
    {
        [JsonProperty("Наименование")]
        public string Name { get; set; }
    }
}