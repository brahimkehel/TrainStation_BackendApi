using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainStation_.Models;

namespace TrainStation_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class TrainsController : ControllerBase
    {
        private readonly TrainStationContext _context;

        public TrainsController(TrainStationContext context)
        {
            _context = context;
        }

        // GET: api/Horaire/VilleDep/VilleArr/DateDep
        [HttpGet("Horaire/{VilleDep}/{VilleArr}/{DateDep}")]
        public async Task<ActionResult> Horaire(int VilleDep, int VilleArr, DateTime DateDep)
        {
            if(VilleDep!=0 && VilleArr!=0 && DateDep!=null)
            {
                List<Helper> list_aff = new List<Helper>();
                var depart_ = await (from g in _context.Gares where g.Id == VilleDep select g.NomGare).FirstOrDefaultAsync();
                var arriver_ = await (from g in _context.Gares where g.Id == VilleArr select g.NomGare).FirstOrDefaultAsync();
                var query = await (from a in _context.Affectations
                                   join t in _context.Trajets on a.IdTrajet equals t.Id 
                                   where t.GareDep == VilleDep && t.GareArr == VilleArr && a.DateDep.Value == DateDep
                                   select new
                                   {
                                       id=a.Id,
                                       idTrain=a.IdTrain,
                                       idTrajet=a.IdTrajet,
                                       depart=depart_,
                                       arriver=arriver_,
                                       dateDep=a.DateDep,
                                       dateArr=a.DateArr,
                                       HeureDep=a.HeureDep,
                                       HeureArr=a.HeureArr
                                   }).ToListAsync();
                if (query.Count()>0)
                {
                    return Ok(query);
                }
                else
                {
                    var query2 = await (from a in _context.Affectations join t in _context.Trajets
                                  on a.IdTrajet equals t.Id
                                  where t.GareDep==VilleDep select a).ToListAsync();
                    foreach(var a in query2)
                    {
                        var exist = await (from aff in _context.Affectations join t in _context.Trajets
                                     on aff.IdTrajet equals t.Id
                                     where t.GareDep==(from aff in _context.Affectations join t in _context.Trajets on aff.IdTrajet equals t.Id where aff.Id==a.Id select t.GareArr).FirstOrDefault()
                                     && t.GareArr==VilleArr && a.DateArr==aff.DateDep && aff.HeureDep>a.HeureArr
                                           select new
                                           {
                                               id = aff.Id,
                                               idTrain = aff.IdTrain,
                                               idTrajet = aff.IdTrajet,
                                               depart = t.GareDep,
                                               arriver = t.GareArr,
                                               dateDep = aff.DateDep,
                                               dateArr = aff.DateArr,
                                               HeureDep = aff.HeureDep,
                                               HeureArr = aff.HeureArr
                                           }).FirstOrDefaultAsync();
                        if(exist!=null)
                        {
                            list_aff.Add(new Helper()
                            {
                            idAff_1=a.Id,
                            idAff_2=exist.id,
                            idTrain_1=(int)a.IdTrain,
                            idTrain_2=(int)exist.idTrain,
                            depart_1=await(from g in _context.Gares where g.Id==(from t in _context.Trajets 
                                                                                 where t.Id==a.IdTrajet select t.GareDep)
                                                                                 .FirstOrDefault() select g.NomGare).FirstOrDefaultAsync(),
                            arriver_1= await (from g in _context.Gares
                                              where g.Id == (from t in _context.Trajets
                                                             where t.Id == a.IdTrajet
                                                             select t.GareArr)
                                                          .FirstOrDefault()
                                              select g.NomGare).FirstOrDefaultAsync(),
                            depart_2 =await(from g in _context.Gares where g.Id==exist.depart select g.NomGare).FirstOrDefaultAsync(),
                            arriver_2= await (from g in _context.Gares where g.Id == exist.arriver select g.NomGare).FirstOrDefaultAsync(),
                            dateDep_1=(DateTime)a.DateDep,
                            dateArr_1=(DateTime)a.DateArr,
                            dateDep_2=(DateTime)exist.dateDep,
                            dateArr_2=(DateTime)exist.dateArr,
                            HeureDep_1=(int)a.HeureDep,
                            HeureArr_1 = (int)a.HeureArr,
                            HeureDep_2 = (int)exist.HeureDep,
                            HeureArr_2 = (int)exist.HeureArr,
                            });
                        }
                    }
                    if(list_aff.Count()>=0)
                    {
                        return Accepted(list_aff);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
            }
            
            else
            {
                return NoContent();
            }
        }

        // GET: api/Trains
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Train>>> GetTrains()
        {
            return await _context.Trains.ToListAsync();
        }

        // GET: api/Trains/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Train>> GetTrain(int id)
        {
            var train = await _context.Trains.FindAsync(id);

            if (train == null)
            {
                return NotFound();
            }

            return train;
        }

        // PUT: api/Trains/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrain(int id, Train train)
        {
            if (id != train.Id)
            {
                return BadRequest();
            }

            _context.Entry(train).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Trains
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Train>> PostTrain(Train train)
        {
            _context.Trains.Add(train);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrain", new { id = train.Id }, train);
        }

        // DELETE: api/Trains/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Train>> DeleteTrain(int id)
        {
            var train = await _context.Trains.FindAsync(id);
            if (train == null)
            {
                return NotFound();
            }

            _context.Trains.Remove(train);
            await _context.SaveChangesAsync();

            return train;
        }

        private bool TrainExists(int id)
        {
            return _context.Trains.Any(e => e.Id == id);
        }
    }
}
