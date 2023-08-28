using Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl
{

    public interface ICategories
    {
        public List<TbCategory> GetAll();
        public TbCategory GetById(int id);
        public bool Save(TbCategory category);
        public bool Delete(int id);
    }
    public class ClsCategories : ICategories
    {

        JobOffersContext _ctx;
        public ClsCategories(JobOffersContext ctx)
        {
            _ctx = ctx;
        }


        public List<TbCategory> GetAll()
        {
            try
            {
                var category = _ctx.TbCategories.Where(x => x.CurrentState == 1).ToList();
                return category;
            }
            catch
            {
                return new List<TbCategory>();
            }
        }

        public TbCategory GetById(int id)
        {
            var category = _ctx.TbCategories.FirstOrDefault(x => x.CategoryId == id);
            return category;
        }

        public bool Save(TbCategory category)
        {
            try
            {

                if (category.CategoryId == 0)
                {
                    category.CurrentState = 1;
                    _ctx.TbCategories.Add(category);
                }
                else
                {
                    _ctx.Entry(category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
                var category = GetById(id);
                category.CurrentState = 0;

                _ctx.Entry(category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
