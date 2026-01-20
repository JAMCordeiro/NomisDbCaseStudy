namespace NomisDbCaseStudy.Models;

public class PersonBody
{
    public int IDPersonBody { get; set; }
    public Guid PersonGUID { get; set; }
    public Guid BodyGUID { get; set; }
    public string? Description { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? FinishTime { get; set; }

    public Person? Person { get; set; }
    public Body? Body { get; set; }
}
