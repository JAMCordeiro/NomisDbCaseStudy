namespace NomisDbCaseStudy.Models;

// --------- Elements ---------
public class BodyTable
{
    public int IDBodyTable { get; set; }
    public Guid TableGUID { get; set; }              // tableGUID (refere uma tabela/elemento)
    public string TableName { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? FinishTime { get; set; }
}
