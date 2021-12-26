namespace FPTAppDev.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class CreateTableCategorys : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 255),
                    Description = c.String(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "UniqueName");
        }

        public override void Down()
        {
            DropIndex("dbo.Categories", "UniqueName");
            DropTable("dbo.Categories");
        }
    }
}