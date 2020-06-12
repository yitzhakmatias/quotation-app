using FluentMigrator;

namespace Quotation.API.Migrations
{
    [Migration(20210630121800)]
    public class AddUserMigration : Migration
    {
        public override void Up()
        {
            Create.Table("Users")
                .WithColumn("Id").AsInt16().PrimaryKey().Identity()
                .WithColumn("Name").AsString()
                .WithColumn("DollarLimit").AsDouble()
                .WithColumn("RealLimit").AsDouble();

            Insert.IntoTable("Users").Row(new {Name = "John Smith", DollarLimit = "200", RealLimit = "300"});
            Insert.IntoTable("Users").Row(new {Name = "Sarah  Carter", DollarLimit = "200", RealLimit = "300"});
            Insert.IntoTable("Users").Row(new {Name = "Peter Sara", DollarLimit = "200", RealLimit = "300"});
        }

        public override void Down()
        {
            Delete.Table("Users");
        }
    }
}