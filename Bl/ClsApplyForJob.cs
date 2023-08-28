using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl
{

    public interface IApplyForJob
    {
        public List<TbApplyForAJob> GetAll();
        public List<VwApplyForAJob> GetAllData();
        public TbApplyForAJob GetById(int id);
        public List<VwApplyForAJob> GetUserById(string userId);
        public List<VwApplyForAJob> GetJobById(int id);

      

        public bool Save(TbApplyForAJob job);
        public bool Delete(int id);
    }
    public class ClsApplyForJob : IApplyForJob
    {

        JobOffersContext _ctx;
        public ClsApplyForJob(JobOffersContext ctx)
        {
            _ctx = ctx;
        }

        public List<TbApplyForAJob> GetAll()
        {
            try
            {
                var job = _ctx.TbApplyForAJobs.ToList();
                return job;
            }
            catch
            {
                return new List<TbApplyForAJob>();
            }
        }

        public List<VwApplyForAJob> GetAllData()
        {
            try
            {
                var job = _ctx.VwApplyForAJobs.Where(x => x.CurrentState == 1).OrderByDescending(x => x.ApplyDate).ToList();
                return job;
            }
            catch
            {
                return new List<VwApplyForAJob>();
            }
        }

        public TbApplyForAJob GetById(int id)
        {
            try
            {
                var job = _ctx.TbApplyForAJobs.FirstOrDefault(x => x.ApplyId == id && x.CurrentState == 1);
                return job;
            }
            catch
            {
                return new TbApplyForAJob();
            }
        }

        public List<VwApplyForAJob> GetJobById(int id)
        {
            try
            {
                var apply = _ctx.VwApplyForAJobs.Where(x => x.JobId == id && x.CurrentState == 1).ToList();
                return apply;
            }
            catch
            {
                return new List<VwApplyForAJob>();
            }
        }

        public List<VwApplyForAJob> GetUserById(string userId)
        {
            try
            {
                var apply = _ctx.VwApplyForAJobs.Where(x => x.UserId == userId && x.CurrentState == 1).ToList();
                return apply;
            }
            catch
            {
                return new List<VwApplyForAJob>();
            }
        }

        public bool Save(TbApplyForAJob apply)
        {
            try
            {
                if (apply.ApplyId == 0)
                {
                    apply.CurrentState = 1;
                    apply.ApplyDate = DateTime.Now;
                    _ctx.Add(apply);
                }
                else
                {
                    _ctx.Entry(apply).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
                var apply = GetById(id);
                apply.CurrentState = 0;
                _ctx.Entry(apply).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

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
