using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl
{
    public interface IjobLocations
    {
        public List<TbJobLocation> GetAll();
        public TbJobLocation GetById(int id);
        public bool Save(TbJobLocation jobLocation);
        public bool Delete(int id);
    }
    public class ClsJobLocations : IjobLocations
    {

        JobOffersContext _ctx;
        public ClsJobLocations(JobOffersContext ctx)
        {
            _ctx = ctx;
        }


        public List<TbJobLocation> GetAll()
        {
            try
            {
                var jobLocation = _ctx.TbJobLocations.Where(x => x.CurrentState == 1).ToList();
                return jobLocation;
            }
            catch
            {
                return new List<TbJobLocation>();
            }
        }

        public TbJobLocation GetById(int id)
        {
            var jobLocation = _ctx.TbJobLocations.FirstOrDefault(x=>x.JobLocationId== id);
            return jobLocation;
        }

        public bool Save(TbJobLocation jobLocation)
        {
            try
            {


                if (jobLocation.JobLocationId == 0)
                {
                    jobLocation.CurrentState = 1;
                    _ctx.TbJobLocations.Add(jobLocation);
                }
                else
                {
                    _ctx.Entry(jobLocation).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                _ctx.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var jobLocation = GetById(id);
                jobLocation.CurrentState = 0;

                _ctx.Entry(jobLocation).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _ctx.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
