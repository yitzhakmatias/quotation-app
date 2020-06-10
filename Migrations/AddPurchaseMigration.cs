using FluentMigrator;

namespace Quotation.API.Migrations
{
    [Migration(20200630121800)]
    public class AddPurchaseMigration: Migration
    {
        public override void Up()
        {
            Create.Table("Purchases")
                .WithColumn("Id").AsInt16().PrimaryKey().Identity()
                .WithColumn("UserId").AsInt16()
                .WithColumn("Amount").AsDouble()
                .WithColumn("Currency").AsInt16();

            Create.ForeignKey() // You can give the FK a name or just let Fluent Migrator default to one
                .FromTable("Purchases").ForeignColumn("UserId")
                .ToTable("Users").PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.Table("Users");
        }
    }
}