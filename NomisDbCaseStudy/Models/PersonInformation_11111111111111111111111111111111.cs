namespace NomisDbCaseStudy.Models
{
    public class PersonInformation_11111111111111111111111111111111
    {
        
        // PK técnica (implementação)
        public int ID { get; set; }

        // Identidade lógica do conteúdo (liga ao Body)
        public Guid BodyGUID { get; set; }

        // Dados de negócio
        public string FullName { get; set; } = null!;
        public DateTime? BirthDate { get; set; }

        // Temporalidade (NOMIS / STDB-style)
        public DateTime StartTime { get; set; }
        public DateTime? FinishTime { get; set; }
    }
}

