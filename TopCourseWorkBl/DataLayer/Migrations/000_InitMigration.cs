using FluentMigrator;
using JetBrains.Annotations;

namespace TopCourseWorkBl.DataLayer.Migrations
{
    [Migration(0)]
    [UsedImplicitly]
    public class InitMigration : ForwardOnlyMigration
    {
        public override void Up()
        {
            Create.Table("customers")
                .WithColumn("id").AsInt64().PrimaryKey()
                .WithColumn("gender").AsBoolean().Nullable();

            Create.Table("types")
                .WithColumn("type").AsInt32().PrimaryKey()
                .WithColumn("description").AsString(8000);

            Create.Table("codes")
                .WithColumn("code").AsInt32().PrimaryKey()
                .WithColumn("description").AsString(8000);

            Create.Table("transactions")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("customer_id").AsInt64() 
                .WithColumn("date").AsDateTime()
                .WithColumn("mcc_code").AsInt32()
                .WithColumn("type").AsInt32()
                .WithColumn("amount").AsDecimal()
                .WithColumn("terminal_id").AsString(8000).Nullable();
        }
    }
}