using FluentMigrator;

namespace Quotation.API.Migrations
{
    [Migration(20210630121800)]
    public class AddUserMigration: Migration
    {
        public override void Up()
        {
            Create.Table("Users")
                .WithColumn("Id").AsInt16().PrimaryKey().Identity()
                .WithColumn("Name").AsString();
                      
             Insert.IntoTable("Users").Row(new {Name = "John Smith"});
             Insert.IntoTable("Users").Row(new {Name = "Sarah  Carter"});
             Insert.IntoTable("Users").Row(new {Name = "Peter Sara"});
  
        }

        public override void Down()
        {
            Delete.Table("Users");
        }
    }
}