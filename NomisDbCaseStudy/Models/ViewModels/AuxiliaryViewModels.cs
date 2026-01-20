namespace NomisDbCaseStudy.Models.ViewModels
{
    public class NomisOverviewVm
    {
        public List<PersonNode> Persons { get; set; } = new();
    }

    public class PersonNode
    {
        public Guid PersonGuid { get; set; }
        public string Name { get; set; } = "";
        public List<BodyNode> Bodies { get; set; } = new();
    }

    public class BodyNode
    {
        public Guid BodyGuid { get; set; }
        public string BodyName { get; set; } = "";
        public Guid TableGuid { get; set; }
        public string TableName { get; set; } = "";
        public List<PersonInfoRow> PersonInformationRows { get; set; } = new();
    }

    public class PersonInfoRow
    {
        public string FullName { get; set; } = "";
        public DateTime StartTime { get; set; }
        public DateTime? FinishTime { get; set; }
    }

}
