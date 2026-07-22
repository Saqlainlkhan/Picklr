using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Picklr.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clubs",
                columns: table => new
                {
                    ClubID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Location = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clubs", x => x.ClubID);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    ReservationID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProgramID = table.Column<int>(type: "INTEGER", nullable: false),
                    ProgramName = table.Column<string>(type: "TEXT", nullable: false),
                    ClubName = table.Column<string>(type: "TEXT", nullable: false),
                    Fee = table.Column<decimal>(type: "TEXT", nullable: false),
                    SelectedDate = table.Column<string>(type: "TEXT", nullable: false),
                    BookedOn = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.ReservationID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Programs",
                columns: table => new
                {
                    ProgramID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Fee = table.Column<decimal>(type: "TEXT", nullable: false),
                    AvailableDays = table.Column<string>(type: "TEXT", nullable: false),
                    ClubID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programs", x => x.ProgramID);
                    table.ForeignKey(
                        name: "FK_Programs_Clubs_ClubID",
                        column: x => x.ClubID,
                        principalTable: "Clubs",
                        principalColumn: "ClubID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Clubs",
                columns: new[] { "ClubID", "Description", "Location", "Name" },
                values: new object[,]
                {
                    { 1, "Our flagship downtown club with 10 indoor courts.", "123 Main St, Chicago, IL", "Picklr Downtown" },
                    { 2, "A vibrant outdoor facility with 8 courts and a pro shop.", "456 Oak Ave, Evanston, IL", "Picklr Northside" },
                    { 3, "Our newest club in the heart of the city.", "789 Broadway, New York, NY", "Picklr New York" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Email", "FirstName", "LastName", "Role" },
                values: new object[,]
                {
                    { 1, "alice@picklr.com", "Alice", "Smith", "Admin" },
                    { 2, "bob@picklr.com", "Bob", "Jones", "Client" }
                });

            migrationBuilder.InsertData(
                table: "Programs",
                columns: new[] { "ProgramID", "AvailableDays", "ClubID", "Description", "Fee", "Name" },
                values: new object[,]
                {
                    { 1, "Monday,Wednesday,Friday", 1, "Drop-in open play for new players. No experience needed.", 10.00m, "Beginner Open Play" },
                    { 2, "Tuesday,Thursday", 1, "Weekly skill-building clinic led by a certified coach.", 25.00m, "Intermediate Clinic" },
                    { 3, "Saturday,Sunday", 2, "Competitive round-robin tournament for rated players.", 40.00m, "Advanced Tournament" },
                    { 4, "Saturday", 2, "Casual weekend social play, all levels welcome.", 0.00m, "Picklr Social" },
                    { 5, "Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday", 3, "The program is designed for the beginners.", 10.00m, "Picklr 101" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Programs_ClubID",
                table: "Programs",
                column: "ClubID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Programs");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Clubs");
        }
    }
}
