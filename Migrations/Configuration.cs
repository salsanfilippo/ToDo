namespace ToDo.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ToDo.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ToDo.Models.ToDoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ToDo.Models.ToDoContext context)
        {
            //  This method will be called after migrating to the latest version.

            var r = new Random();
            var items = Enumerable.Range(1, 50).Select(o => new ToDoItem
            {
                DueDate = new DateTime(2012, r.Next(1, 12), r.Next(1, 28)),
                Priority = (byte)r.Next(10),
                Todo = o.ToString()
            }).ToArray();
            context.ToDoItems.AddOrUpdate(item => new { item.Todo }, items);
        }
    }
}
