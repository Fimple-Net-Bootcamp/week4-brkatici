using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestfulPetApi.Migrations
{
    /// <inheritdoc />
    public partial class NewClassesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SocialInteractions",
                columns: table => new
                {
                    SocialInteractionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Pet1 = table.Column<int>(type: "int", nullable: false),
                    Pet2 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialInteractions", x => x.SocialInteractionId);
                });

            migrationBuilder.CreateTable(
                name: "Trainings",
                columns: table => new
                {
                    TrainingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PetId = table.Column<int>(type: "int", nullable: false),
                    TrainingId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainings", x => x.TrainingId);
                    table.ForeignKey(
                        name: "FK_Trainings_Trainings_TrainingId1",
                        column: x => x.TrainingId1,
                        principalTable: "Trainings",
                        principalColumn: "TrainingId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_TrainingId1",
                table: "Trainings",
                column: "TrainingId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SocialInteractions");

            migrationBuilder.DropTable(
                name: "Trainings");
        }
    }
}
