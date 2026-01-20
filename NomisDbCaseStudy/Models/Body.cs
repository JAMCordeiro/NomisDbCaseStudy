namespace NomisDbCaseStudy.Models;
public class Body
{
    public int IDBody { get; set; }
    public Guid BodyGUID { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? FinishTime { get; set; }

    public ICollection<PersonBody> PersonBodies { get; set; } = new List<PersonBody>();
    public ICollection<BodyBodyTable> BodyBodyTables { get; set; } = new List<BodyBodyTable>();
}
