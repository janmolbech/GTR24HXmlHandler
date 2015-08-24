namespace GTR24HXmlHandler.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QualificationModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Qualified = c.Boolean(nullable: false),
                        TenInARow = c.Boolean(nullable: false),
                        DriverName = c.String(),
                        Class = c.String(),
                        CarModel = c.String(),
                        CompletedLaps = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.QualificationModels");
        }
    }
}
