using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl
{
    public interface IJobTypes
    {
        public List<TbJobType> GetAll();
        public TbJobType GetById(int id);
        public bool Save(TbJobType jobType);
        public bool Delete(int id);
    }
    public class ClsJobTypes : IJobTypes
    {

        JobOffersContext _ctx;
        public ClsJobTypes(JobOffersContext ctx)
        {
            _ctx = ctx;
        }

        public List<TbJobType> GetAll()
        {
            try
            {
                var JobType = _ctx.TbJobTypes.Where(x => x.CurrentState == 1).ToList();
                return JobType;
            }
            catch
            {
                return new List<TbJobType>();
            }
        }

        public TbJobType GetById(int id)
        {
            var JobType = _ctx.TbJobTypes.FirstOrDefault(x => x.JobTypeId == id);
            return JobType;
        }

        public bool Save(TbJobType JobType)
        {
            try
            {


                if (JobType.JobTypeId == 0)
                {
                    JobType.CurrentState = 1;
                    _ctx.TbJobTypes.Add(JobType);
                }
                else
                {
                    _ctx.Entry(JobType).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
                var JobType = GetById(id);
                JobType.CurrentState = 0;

                _ctx.Entry(JobType).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
