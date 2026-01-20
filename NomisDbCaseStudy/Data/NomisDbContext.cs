namespace NomisDbCaseStudy.Data
{
    using Microsoft.EntityFrameworkCore;
    using NomisDbCaseStudy.Models;

    public class NomisDbContext : DbContext
    {
        public NomisDbContext(DbContextOptions<NomisDbContext> options) : base(options) { }

        public DbSet<Element> Elements => Set<Element>();
        public DbSet<BodyTable> BodyTables => Set<BodyTable>();
        public DbSet<Person> Persons => Set<Person>();
        public DbSet<Body> Bodies => Set<Body>();

        public DbSet<BodyBodyTable> BodyBodyTables => Set<BodyBodyTable>();
        public DbSet<PersonBody> PersonBodies => Set<PersonBody>();

        public DbSet<Models.PersonInformation_11111111111111111111111111111111> PersonInformation_11111111111111111111111111111111 => Set<Models.PersonInformation_11111111111111111111111111111111>();
        protected override void OnModelCreating(ModelBuilder mb)
        {
            // Helpers
            static void Temporal<TEntity>(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TEntity> e)
                where TEntity : class
            {
                e.Property("StartTime").IsRequired();
                e.Property("FinishTime").IsRequired(false);
                e.Property("Description").HasMaxLength(2000);
            }

            // ---------- Element ----------
            mb.Entity<Element>(e =>
            {
                e.ToTable("Element");
                e.HasKey(x => x.IDElement);

                e.Property(x => x.TableGUID).IsRequired();
                e.Property(x => x.TableName).IsRequired().HasMaxLength(255);

                // Alternativa: pode garantir unicidade do GUID "válido" via regra/índice filtrado no SQL Server.
                e.HasIndex(x => x.TableGUID);
                Temporal(e);
            });

            // ---------- BodyTable ----------
            mb.Entity<BodyTable>(e =>
            {
                e.ToTable("BodyTable");
                e.HasKey(x => x.IDBodyTable);

                e.Property(x => x.TableGUID).IsRequired();
                e.Property(x => x.TableName).IsRequired().HasMaxLength(255);

                // Vamos permitir FK por GUID:
                e.HasAlternateKey(x => x.TableGUID);

                Temporal(e);
            });

            // ---------- Person ----------
            mb.Entity<Person>(e =>
            {
                e.ToTable("Person");
                e.HasKey(x => x.IDPerson);

                e.Property(x => x.PersonGUID).IsRequired();
                e.HasAlternateKey(x => x.PersonGUID);

                e.Property(x => x.Name).IsRequired().HasMaxLength(255);
                Temporal(e);
            });

            // ---------- Body ----------
            mb.Entity<Body>(e =>
            {
                e.ToTable("Body");
                e.HasKey(x => x.IDBody);

                e.Property(x => x.BodyGUID).IsRequired();
                e.HasAlternateKey(x => x.BodyGUID);

                e.Property(x => x.Name).IsRequired().HasMaxLength(255);
                Temporal(e);
            });

            // ---------- Relationship tables (FK por GUID => usa AlternateKey no principal) ----------

            mb.Entity<BodyBodyTable>(e =>
            {
                e.ToTable("BodyBodyTable");
                e.HasKey(x => x.IDBodyBodyTable);

                e.Property(x => x.BodyGUID).IsRequired();
                e.Property(x => x.TableGUID).IsRequired();
                Temporal(e);

                e.HasOne(x => x.Body)
                    .WithMany(x => x.BodyBodyTables)
                    .HasForeignKey(x => x.BodyGUID)
                    .HasPrincipalKey(p => p.BodyGUID);

                e.HasOne(x => x.BodyTable)
                    .WithMany()
                    .HasForeignKey(x => x.TableGUID)
                    .HasPrincipalKey(p => p.TableGUID);
            });

            mb.Entity<PersonBody>(e =>
            {
                e.ToTable("PersonBody");
                e.HasKey(x => x.IDPersonBody);

                e.Property(x => x.PersonGUID).IsRequired();
                e.Property(x => x.BodyGUID).IsRequired();
                Temporal(e);

                e.HasOne(x => x.Person)
                    .WithMany(x => x.PersonBodies)
                    .HasForeignKey(x => x.PersonGUID)
                    .HasPrincipalKey(p => p.PersonGUID);

                e.HasOne(x => x.Body)
                    .WithMany(x => x.PersonBodies)
                    .HasForeignKey(x => x.BodyGUID)
                    .HasPrincipalKey(p => p.BodyGUID);
            });
        }
    }
}
