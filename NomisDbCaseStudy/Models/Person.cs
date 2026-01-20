namespace NomisDbCaseStudy.Models;

public class Person
{
    public int IDPerson { get; set; }
    public Guid PersonGUID { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? FinishTime { get; set; }

    public ICollection<PersonBody> PersonBodies { get; set; } = new List<PersonBody>();
    
}
