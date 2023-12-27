using System;
using Bogus;
using Bogus.DataSets;
using Microsoft.EntityFrameworkCore.Migrations;
using VTT.models;

#nullable disable

namespace CS_58_TichHop_EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class initdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "articles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Content = table.Column<string>(type: "ntext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_articles", x => x.Id);
                });

                // insert Data
                // migrationBuilder.InsertData(
                //     table:"articles",
                //     columns: new[] {"Title", "Created", "Content"},
                //     values: new object[] { 
                //         "Bai Viet 1",
                //         new DateTime(2023, 12,27),
                //         "Noi dung 1"
                //     }
                // );

                // Tao fake data: Bogus

                Randomizer.Seed = new Random(8675309);
                var fakerArticle = new Faker<Article>();
                fakerArticle.RuleFor(a=> a.Title, f=> f.Lorem.Sentence(5,5));
                fakerArticle.RuleFor(a=> a.Created, f=> f.Date.Between(new DateTime(2023, 1, 1), new DateTime(2023,12,30)));
                fakerArticle.RuleFor(a=> a.Content, f => f.Lorem.Paragraphs(1,4));

                for(int i=0; i<100; i++){
                    Article article = fakerArticle.Generate();
                    migrationBuilder.InsertData(
                        table:"articles",
                        columns: new[] {"Title", "Created", "Content"},
                        values: new object[] { 
                            article.Title,
                            article.Created,
                            article.Content
                        }
                    );
                }
                



        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "articles");
        }
    }
}
