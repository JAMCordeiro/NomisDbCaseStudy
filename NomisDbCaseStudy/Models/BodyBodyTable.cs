namespace NomisDbCaseStudy.Models;

// --------- Relationships ---------
public class BodyBodyTable
{
    public int IDBodyBodyTable { get; set; }
    public Guid BodyGUID { get; set; }
    public Guid TableGUID { get; set; }              // tabela onde está o "conteúdo" do Body
    public string? Description { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? FinishTime { get; set; }

    public Body? Body { get; set; }
    public BodyTable? BodyTable { get; set; }
}
