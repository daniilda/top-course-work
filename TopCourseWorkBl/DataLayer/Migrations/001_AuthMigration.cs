using FluentMigrator;
using JetBrains.Annotations;

namespace TopCourseWorkBl.DataLayer.Migrations
{
    [Migration(1)]
    [UsedImplicitly]
    public class AuthMigration : ForwardOnlyMigration
    {
        public override void Up()
        {
            Create.Table("roles")
                .WithColumn("role").AsString(8000).PrimaryKey().Unique()
                .WithColumn("description").AsString(8000)
                .WithColumn("rules").AsCustom("jsonb").WithDefaultValue("{}");
            
            Create.Table("users")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("username").AsString(8000).Unique()
                .WithColumn("password").AsString(8000)
                .WithColumn("role").AsString(8000).ForeignKey("roles", "role");

            Create.Table("refresh_tokens")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("user_id").AsInt64().ForeignKey("users", "id")
                .WithColumn("token").AsString(8000)
                .WithColumn("expires_at").AsDateTime()
                .WithColumn("created_at").AsDateTime().WithDefault(SystemMethods.CurrentDateTime)
                .WithColumn("revoked_at").AsDateTime().Nullable()
                .WithColumn("revoked_by_ip").AsString().Nullable()
                .WithColumn("replaced_by_token").AsString().Nullable();

            Insert.IntoTable("roles").Row(new { role = "defaultAdmin", description = "SysAdmin" })
                .Row(new { role = "user", description = "User" });
        }
    }
}