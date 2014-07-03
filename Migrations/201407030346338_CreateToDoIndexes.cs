namespace ToDo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateToDoIndexes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ToDoItems", "Todo", c => c.String(maxLength: 800));

            foreach (string col in new[] { "Todo", "Priority", "DueDate" }) 
            {
                CreateIndex("TodoItems", col);
            }
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ToDoItems", "Todo", c => c.String());
        }
    }
}
