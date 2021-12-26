namespace FPTAppDev.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class CreateTabeTrainers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Trainers",
                c => new
                {
                    TrainerId = c.String(nullable: false, maxLength: 128),
                    Name = c.String(nullable: false, maxLength: 255),
                    Age = c.Int(nullable: false),
                    Address = c.String(nullable: false),
                    Specialty = c.String(nullable: false),
                })
                .PrimaryKey(t => t.TrainerId)
                .ForeignKey("dbo.AspNetUsers", t => t.TrainerId)
                .Index(t => t.TrainerId);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Trainers", "TrainerId", "dbo.AspNetUsers");
            DropIndex("dbo.Trainers", new[] { "TrainerId" });
            DropTable("dbo.Trainers");
        }
    }
}