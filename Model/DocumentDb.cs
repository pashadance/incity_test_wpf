namespace incity_test.Model;

public class DocumentDb
{
    public Guid Id { get; set; }
   
    public Guid Guid { get; set; }
   
    public DateTime? RegistrationDate { get; set; }

    public string? Name { get; set; }
    
    public string? ActualAddress { get; set; }
   
    public string? Phone { get; set; }
}