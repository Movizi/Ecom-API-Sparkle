using Microsoft.EntityFrameworkCore;
using ReportApp_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportApp_Repositories
{
    public interface IDbContext
    {
        DbSet<Category> Categories { get; set; }
        DbSet<Product> Products { get; set; }

        void SaveChanges();
        void SetEntityState<T>(T entity, EntityState state) where T : class;
    }
}
