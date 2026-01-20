using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NomisDbCaseStudy.Data;
using NomisDbCaseStudy.Models;
using NomisDbCaseStudy.Models.ViewModels;

namespace NomisDbCaseStudy.Controllers
{
    public class NomisExplorerController : Controller
    {
        private readonly NomisDbContext _db;

        public NomisExplorerController(NomisDbContext db) => _db = db;

        public async Task<IActionResult> Index()
        {
            // 1) carregar kernel
            var persons = await _db.Persons
                .Where(p => p.FinishTime == null)
                .Select(p => new { p.PersonGUID, p.Name })
                .ToListAsync();

            var personBodies = await _db.PersonBodies
                .Where(pb => pb.FinishTime == null)
                .Select(pb => new { pb.PersonGUID, pb.BodyGUID })
                .ToListAsync();

            var bodies = await _db.Bodies
                .Where(b => b.FinishTime == null)
                .Select(b => new { b.BodyGUID, b.Name })
                .ToListAsync();

            var bodyBodyTables = await _db.BodyBodyTables
                .Where(bbt => bbt.FinishTime == null)
                .Select(bbt => new { bbt.BodyGUID, bbt.TableGUID })
                .ToListAsync();

            var bodyTables = await _db.BodyTables
                .Where(bt => bt.FinishTime == null)
                .Select(bt => new { bt.TableGUID, bt.TableName })
                .ToListAsync();

            // 2) conteúdo: PersonInformation_<GUID> (DbSet fixo do exemplo)
            // Troque pelo teu DbSet real (nome amigável, de preferência)
            var piRows = await _db.PersonInformation_11111111111111111111111111111111
                .Select(x => new { x.BodyGUID, x.FullName, x.StartTime, x.FinishTime })
                .ToListAsync();

            // 3) montar ViewModel em memória (didático e simples)
            var vm = new NomisOverviewVm();

            foreach (var p in persons)
            {
                var pNode = new PersonNode { PersonGuid = p.PersonGUID, Name = p.Name };

                var bodyGuids = personBodies
                    .Where(pb => pb.PersonGUID == p.PersonGUID)
                    .Select(pb => pb.BodyGUID)
                    .Distinct()
                    .ToList();

                foreach (var bg in bodyGuids)
                {
                    var b = bodies.FirstOrDefault(x => x.BodyGUID == bg);
                    if (b == null) continue;

                    var tableGuid = bodyBodyTables.FirstOrDefault(x => x.BodyGUID == bg)?.TableGUID ?? Guid.Empty;
                    var tableName = bodyTables.FirstOrDefault(x => x.TableGUID == tableGuid)?.TableName ?? "";

                    var bNode = new BodyNode
                    {
                        BodyGuid = bg,
                        BodyName = b.Name,
                        TableGuid = tableGuid,
                        TableName = tableName,
                        PersonInformationRows = piRows
                            .Where(r => r.BodyGUID == bg)
                            .OrderByDescending(r => r.StartTime)
                            .Select(r => new PersonInfoRow
                            {
                                FullName = r.FullName,
                                StartTime = r.StartTime,
                                FinishTime = r.FinishTime
                            })
                            .ToList()
                    };

                    pNode.Bodies.Add(bNode);
                }

                vm.Persons.Add(pNode);
            }

            return View(vm);
        }
    }

}
