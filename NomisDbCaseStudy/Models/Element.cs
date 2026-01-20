namespace NomisDbCaseStudy.Models;

// --------- Meta ---------
public class Element
{
    public int IDElement { get; set; }
    public Guid TableGUID { get; set; }              // tableGUID
    public string TableName { get; set; } = null!;   // tableName
    public string? Description { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? FinishTime { get; set; }
}
