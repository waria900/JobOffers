using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domains;

namespace Bl
{

    public interface IJobs 
    {
        public List<TbJob> GetAll();
        public List<VwJob> GetAllData();
        public TbJob GetById(int id);
        public List<VwJob> GetUserById(string userId);
        public VwJob GetJobById(int id);

        public bool Save(TbJob job);
        public bool Delete(int id);

    }

    public class ClsJobs : IJobs
    {

        JobOffersContext _ctx;
        public ClsJobs(JobOffersContext ctx)
        {
            _ctx = ctx; 
        }

        public List<TbJob> GetAll()
        {
            try
            {
                var job = _ctx.TbJobs.ToList();
                return job;
            }
            catch
            {
                return new List<TbJob>();
            }
        }

        public List<VwJob> GetAllData()
        {
            try
            {
                var job = _ctx.VwJobs.Where(x=>x.CurrentState==1).OrderByDescending(x=>x.PostedDate).ToList();
                return job;
            }
            catch
            {
                return new List<VwJob>();
            }
        }

        public TbJob GetById(int id)
        {
            try
            {
                var job = _ctx.TbJobs.FirstOrDefault(x => x.JobId == id && x.CurrentState == 1);
                return job;
            }
            catch
            {
                return new TbJob();
            }
        }


        public List<VwJob> GetUserById(string userId)
        {
            try
            {
                var user = _ctx.VwJobs.Where(x => x.UserId == userId && x.CurrentState == 1).OrderByDescending(x=>x.PostedDate).ToList();
                return user;
            }
            catch
            {
                return new List<VwJob>();
            }
        }

        public VwJob GetJobById(int id)
        {
            try
            {
                var job = _ctx.VwJobs.FirstOrDefault(x => x.JobId == id && x.CurrentState == 1);
                return job;
            }
            catch
            {
                return new VwJob();
            }
        }



        public bool Save(TbJob job)
        {

            try
            {
                if (job.JobId == 0)
                {
                    job.CurrentState = 1;
                    job.PostedDate = DateTime.Now;
                    _ctx.Add(job);
                }
                else
                {
                    _ctx.Entry(job).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
                var job = GetById(id);
                job.CurrentState = 0;
                _ctx.Entry(job).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

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
