using Microsoft.EntityFrameworkCore;
using NomisDbCaseStudy.Models;

namespace NomisDbCaseStudy.Data
{
    public static class NomisSeed
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<NomisDbContext>();

            await db.Database.MigrateAsync();

            // Seed idempotente
            if (await db.Persons.AnyAsync())
                return;

            var now = DateTime.UtcNow;

            // =========================================================
            // 1) GUID inventado da tabela PersonInformation_<GUID>
            // =========================================================
            var tableGuidPI = Guid.Parse("11111111-1111-1111-1111-111111111111");

            // =========================================================
            // 2) Nome físico da tabela (TEM de bater certo com ToTable)
            //    Ex: "PersonInformation_11111111111111111111111111111111"
            // =========================================================
            var tableNamePI = $"PersonInformation_{tableGuidPI:N}";

            // =========================================================
            // 3) GUIDs das pessoas e dos bodies (determinísticos para exemplo)
            // =========================================================
            var person1Guid = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            var person2Guid = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");

            // BodyGUID identifica o "conteúdo" (a info da pessoa) guardado na body table
            var bodyGuidP1PI = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc");
            var bodyGuidP2PI = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd");

            // =========================================================
            // KERNEL: Element + BodyTable
            // =========================================================
            db.Elements.Add(new Element
            {
                TableGUID = tableGuidPI,
                TableName = tableNamePI,
                Description = "Tabela física de PersonInformation (nome = base + GUID).",
                StartTime = now,
                FinishTime = null
            });

            db.BodyTables.Add(new BodyTable
            {
                TableGUID = tableGuidPI,
                TableName = tableNamePI,
                Description = "BodyTable para PersonInformation.",
                StartTime = now,
                FinishTime = null
            });

            // =========================================================
            // Pessoas
            // =========================================================
            db.Persons.AddRange(
                new Person
                {
                    PersonGUID = person1Guid,
                    Name = "Ana Silva",
                    Description = "Pessoa 1",
                    StartTime = now,
                    FinishTime = null
                },
                new Person
                {
                    PersonGUID = person2Guid,
                    Name = "Bruno Costa",
                    Description = "Pessoa 2",
                    StartTime = now,
                    FinishTime = null
                }
            );

            // =========================================================
            // Bodies (um por conjunto de PersonInformation por pessoa)
            // =========================================================
            db.Bodies.AddRange(
                new Body
                {
                    BodyGUID = bodyGuidP1PI,
                    Name = "PersonInformation",
                    Description = "Body que identifica a informação da Ana.",
                    StartTime = now,
                    FinishTime = null
                },
                new Body
                {
                    BodyGUID = bodyGuidP2PI,
                    Name = "PersonInformation",
                    Description = "Body que identifica a informação do Bruno.",
                    StartTime = now,
                    FinishTime = null
                }
            );

            // =========================================================
            // BodyBodyTable: Body -> TableGUID onde vive o conteúdo
            // =========================================================
            db.BodyBodyTables.AddRange(
                new BodyBodyTable
                {
                    BodyGUID = bodyGuidP1PI,
                    TableGUID = tableGuidPI,
                    Description = "Ana -> PersonInformation_<GUID>",
                    StartTime = now,
                    FinishTime = null
                },
                new BodyBodyTable
                {
                    BodyGUID = bodyGuidP2PI,
                    TableGUID = tableGuidPI,
                    Description = "Bruno -> PersonInformation_<GUID>",
                    StartTime = now,
                    FinishTime = null
                }
            );

            // =========================================================
            // PersonBody: Person -> Body
            // =========================================================
            db.PersonBodies.AddRange(
                new PersonBody
                {
                    PersonGUID = person1Guid,
                    BodyGUID = bodyGuidP1PI,
                    Description = "Pessoa Ana ligada ao Body PersonInformation.",
                    StartTime = now,
                    FinishTime = null
                },
                new PersonBody
                {
                    PersonGUID = person2Guid,
                    BodyGUID = bodyGuidP2PI,
                    Description = "Pessoa Bruno ligada ao Body PersonInformation.",
                    StartTime = now,
                    FinishTime = null
                }
            );

            // Salva kernel primeiro
            await db.SaveChangesAsync();

            // =========================================================
            // CONTEÚDO: PersonInformation_<GUID> (já mapeada no contexto)
            //
            // IMPORTANTE:
            // Troque db.PersonInformation_... pelo nome real do teu DbSet.
            // =========================================================

            var oldStart = now.AddYears(-2);
            var oldFinish = now.AddMonths(-6);

            // Pessoa 1: registo antigo (fechado)
            db.PersonInformation_11111111111111111111111111111111.Add(new PersonInformation_11111111111111111111111111111111
            {
                BodyGUID = bodyGuidP1PI,
                FullName = "Ana Silva",
                BirthDate = new DateTime(1990, 5, 10),
                StartTime = oldStart,
                FinishTime = oldFinish
            });

            // Pessoa 1: registo novo (atual)
            db.PersonInformation_11111111111111111111111111111111.Add(new PersonInformation_11111111111111111111111111111111
            {
                BodyGUID = bodyGuidP1PI,
                FullName = "Ana Maria Silva",
                BirthDate = new DateTime(1990, 5, 10),
                StartTime = oldFinish.AddSeconds(1),
                FinishTime = null
            });

            // Pessoa 2: registo atual
            db.PersonInformation_11111111111111111111111111111111.Add(new PersonInformation_11111111111111111111111111111111
            {
                BodyGUID = bodyGuidP2PI,
                FullName = "Bruno Costa",
                BirthDate = new DateTime(1988, 11, 3),
                StartTime = now.AddYears(-1),
                FinishTime = null
            });

            await db.SaveChangesAsync();
        }
    }
}
