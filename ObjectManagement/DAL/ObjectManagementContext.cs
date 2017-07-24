using ObjectManagement.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ObjectManagement.DAL
{
    public class ObjectManagementContext : DbContext
    {
        public ObjectManagementContext() : base("ObjectManagementContext")
        {
        }

        public DbSet<Principal> Principals { get; set; }
        public DbSet<Item> Items { get; set; }
    }
}