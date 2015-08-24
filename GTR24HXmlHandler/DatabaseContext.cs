using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTR24HXmlHandler
{
    public class DatabaseContext:DbContext
    {
        static DatabaseContext()
        {

        }
        public DatabaseContext() : base("QualificationRepo") { }
        public DbSet<Qualification> Qualifications { get; set; }
    }

    public class Qualification
    {
        [Key]
        public int Id { get; set; }
        public bool Qualified { get; set; }
        public bool TenInARow { get; set; }
        public string DriverName { get; set; }
        public string Class { get; set; }
        public string CarModel { get; set; }

        public int CompletedLaps { get; set; }
    }
}
