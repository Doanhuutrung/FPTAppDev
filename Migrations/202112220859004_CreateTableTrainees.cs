namespace FPTAppDev.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableTrainees : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Trainees",
                c => new
                    {
                        TraineeId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 255),
                        Age = c.Int(nullable: false),
                        BirthDate = c.DateTime(nullable: false),
                        Address = c.String(nullable: false),
                        Education = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.TraineeId)
                .ForeignKey("dbo.AspNetUsers", t => t.TraineeId)
                .Index(t => t.TraineeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trainees", "TraineeId", "dbo.AspNetUsers");
            DropIndex("dbo.Trainees", new[] { "TraineeId" });
            DropTable("dbo.Trainees");
        }
    }
}
