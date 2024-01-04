using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CS_58_TichHop_EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class listUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
for (int i = 1; i < 150; i++)
            {
                migrationBuilder.InsertData(
                    "Users",
                    columns: new[] {
                        "Id",
                        "UserName",
                        "Email",
                        "SecurityStamp",
                        "EmailConfirmed",
                        "PhoneNumberConfirmed",
                        "TwoFactorEnabled",
                        "LockoutEnabled",
                        "AccessFailedCount",
                        "HomeAdress"
                        

                     },
                    values: new object[] {
                        Guid.NewGuid().ToString(),
                        "User-"+i.ToString("D3"),
                        $"email{i.ToString("D3")}@example.com",
                        Guid.NewGuid().ToString(),
                        true,
                        false,
                        false,
                        false,
                        0,
                        "...@#%..." 
                    }
                );
            }

        }


//         SELECT TOP (1000) [Id]
//       ,[UserName]
//       ,[NormalizedUserName]
//       ,[Email]
//       ,[NormalizedEmail]
//       ,[EmailConfirmed]
//       ,[PasswordHash]
//       ,[SecurityStamp]
//       ,[ConcurrencyStamp]
//       ,[PhoneNumber]
//       ,[PhoneNumberConfirmed]
//       ,[TwoFactorEnabled]
//       ,[LockoutEnd]
//       ,[LockoutEnabled]
//       ,[AccessFailedCount]
//       ,[HomeAdress]
//       ,[BirthDate]
//   FROM [zaroWebDb1].[dbo].[Users]


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
