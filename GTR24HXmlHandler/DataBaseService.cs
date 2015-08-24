using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace GTR24HXmlHandler
{
    public class DataBaseService
    {
        public static void AddQualificationInstance(Qualification model)
        {
            Qualification exists;
            using (DatabaseContext repo = new DatabaseContext())
            {
                exists = repo.Qualifications.FirstOrDefault(q => q.Id == model.Id);

                if (exists != null)
                {
                    exists.CompletedLaps = model.CompletedLaps;
                    exists.Qualified = model.Qualified;
                    exists.TenInARow = model.TenInARow;
                }
                else
                {
                    exists = model;
                    repo.Qualifications.Add(exists);
                }

                repo.SaveChanges();
            }

        }

        public static Qualification GetQualificationByNameAndModel(string name,string model)
        {
            using (DatabaseContext repo = new DatabaseContext())
            {
                var q = new Qualification();

                var result = repo.Qualifications.FirstOrDefault(ql => (ql.DriverName == name && ql.CarModel == model));
                if(result != null)
                {
                    return result;
                }
                return q;
            }
        }

        public static IQueryable<Qualification> GetAllQualifications()
        {
            using (DatabaseContext repo = new DatabaseContext())
            {
                return repo.Qualifications.OrderBy(q => q.Class);
            }
        }
    }
}
