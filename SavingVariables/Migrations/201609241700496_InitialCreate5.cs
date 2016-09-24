namespace SavingVariables.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Variables", "Variable", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Variables", "Variable");
        }
    }
}
