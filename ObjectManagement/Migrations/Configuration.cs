using ObjectManagement.Models;
using ObjectManagement.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace ObjectManagement.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ObjectManagement.DAL.ObjectManagementContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ObjectManagement.DAL.ObjectManagementContext context)
        {
            context.Database.ExecuteSqlCommand("DELETE FROM Principals");

            var principals = new List<Principal>
            {
                new Principal { PrincipalID = 1, Username = "TestUser1", Password = SecurityManager.Hash("TestUser") },
                new Principal { PrincipalID = 2, Username = "TestUser2", Password = SecurityManager.Hash("TestUser") },
                new Principal { PrincipalID = 3, Username = "TestUser3", Password = SecurityManager.Hash("TestUser") },
                new Principal { PrincipalID = 4, Username = "TestUser4", Password = SecurityManager.Hash("TestUser") },
                new Principal { PrincipalID = 5, Username = "TestUser5", Password = SecurityManager.Hash("TestUser") }
            };

            var items = new List<Item>
            {
                new Item { ItemID = 1, Name = "Item1", Due = DateTime.Now.AddDays(7), Completed = false, Comment = "Default comment", PrincipalID = 1},
                new Item { ItemID = 2, Name = "Item2", Due = DateTime.Now.AddDays(7), Completed = false, Comment = "Default comment", PrincipalID = 1},
                new Item { ItemID = 3, Name = "Item3", Due = DateTime.Now.AddDays(7), Completed = false, Comment = "Default comment", PrincipalID = 2},
                new Item { ItemID = 4, Name = "Item4", Due = DateTime.Now.AddDays(7), Completed = false, Comment = "Default comment", PrincipalID = 2},
                new Item { ItemID = 5, Name = "Item5", Due = DateTime.Now.AddDays(7), Completed = false, Comment = "Default comment", PrincipalID = 2},
                new Item { ItemID = 6, Name = "Item6", Due = DateTime.Now.AddDays(7), Completed = false, Comment = "Default comment", PrincipalID = 3},
                new Item { ItemID = 7, Name = "Item7", Due = DateTime.Now.AddDays(7), Completed = false, Comment = "Default comment", PrincipalID = 4}
            };

            principals.ForEach(p => context.Principals.Add(p));
            items.ForEach(i => context.Items.Add(i));
            context.SaveChanges();
        }
    }
}
