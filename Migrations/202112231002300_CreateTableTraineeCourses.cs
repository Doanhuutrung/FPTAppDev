namespace FPTAppDev.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableTraineeCourses : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TraineeCourses",
                c => new
                    {
                        TraineeId = c.String(nullable: false, maxLength: 128),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TraineeId, t.CourseId })
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Trainees", t => t.TraineeId, cascadeDelete: true)
                .Index(t => t.TraineeId)
                .Index(t => t.CourseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TraineeCourses", "TraineeId", "dbo.Trainees");
            DropForeignKey("dbo.TraineeCourses", "CourseId", "dbo.Courses");
            DropIndex("dbo.TraineeCourses", new[] { "CourseId" });
            DropIndex("dbo.TraineeCourses", new[] { "TraineeId" });
            DropTable("dbo.TraineeCourses");
        }
    }
}
