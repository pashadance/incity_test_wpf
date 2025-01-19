using incity_test.Infrastructure;
using Newtonsoft.Json;

namespace incity_test.Model;

public class Document
{
    public Guid Id { get; set; }

    [JsonProperty("ГУИД")]
    public Guid Guid { get; set; }

    [JsonProperty("ДатаРегистрации")]
    [JsonConverter(typeof(CustomDateTimeConverter), "dd.MM.yyyy HH:mm:ss")]
    public DateTime? RegistrationDate { get; set; }

    [JsonProperty("Наименование")]
    public string? Name { get; set; }

    [JsonProperty("ТекстовыеДанные")]
    public string? TextData { get; set; }

    [JsonIgnore]
    public string? ActualAddress { get; set; }

    [JsonIgnore]
    public string? Phone { get; set; }
}