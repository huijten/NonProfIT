using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NonProfIT.Migrations
{
    public partial class SeedRoles : Migration
    {
        private string StudentRoleId = Guid.NewGuid().ToString();
        private string EmployeeRoleId = Guid.NewGuid().ToString();

        private string User1Id = Guid.NewGuid().ToString();
        private string User2Id = Guid.NewGuid().ToString();
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            SeedRolesSQL(migrationBuilder);
            SeedUser(migrationBuilder);
            SeedUserRoles(migrationBuilder);
        }

        private void SeedRolesSQL(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@$"INSERT INTO [dbo].[AspNetRoles] ([Id],[Name],[NormalizedName],[ConcurrencyStamp]) 
            VALUES ('{StudentRoleId}', 'Student', 'STUDENT', null);");
            migrationBuilder.Sql(@$"INSERT INTO [dbo].[AspNetRoles] ([Id],[Name],[NormalizedName],[ConcurrencyStamp]) 
            VALUES ('{EmployeeRoleId}', 'Employee', 'EMPLOYEE', null);");
        }

        private void SeedUser(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @$"INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp],
                [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount])
                VALUES
                (N'{User1Id}', N'Kevin', N'Huijten', N'1844067huijten@zuyd.nl', N'1844067HUIJTEN@ZUYD.NL', N'1844067huijten@zuyd.nl', N'1844067HUIJTEN@ZUYD.NL', 0, 
                N'AQAAAAEAACcQAAAAEAf3LrX6v5Hn5Youb9v2G2WIkkyuSYfQJDsdqdPMNNaf1DxmhYV98qMsR2WxJmnO5g==', 
                '5OPVGWREE5TPW4EVMCMIBUQ7D5BPRM6N', '44e1fdde-46f5-4b68-a814-7823b8212ccb', NULL, 0, 0, NULL, 1, 0)");

            migrationBuilder.Sql(
               @$"INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp],
                [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount])
                VALUES
                (N'{User2Id}', N'Miel', N'Noelanders', N'miel.noelanders@zuyd.nl', N'MIEL.NOELANDERS@ZUYD.NL', N'miel.noelanders@zuyd.nl', N'MIEL.NOELANDERS@ZUYD.NL', 0, 
                N'AQAAAAEAACcQAAAAEFW+P+ZsTw6uWGbLNsp/p6pAiBYa6epX+WFVFVVJr19HH8pPoWENemhl2Zju2PkzYQ==', 
                'COE6JBC7NLLHUWWJX4SWL33TNFWBPXMS', '19e42160-b031-4bc3-982b-8be364f63648', NULL, 0, 0, NULL, 1, 0)");
        }

        private void SeedUserRoles(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@$"
            INSERT INTO [dbo].[AspNetUserRoles]
            ([UserId],[RoleId])
            VALUES
            ('{User1Id}', '{StudentRoleId}');");

            migrationBuilder.Sql(@$"
            INSERT INTO [dbo].[AspNetUserRoles]
            ([UserId],[RoleId])
            VALUES
            ('{User2Id}', '{EmployeeRoleId}');");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
