using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BeestjeOpJeFeestje.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accessories",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Price = table.Column<double>(nullable: false),
                    PicturePath = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accessories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Animal",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Type = table.Column<string>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    PicturePath = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animal", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Booking",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AnimalAccessories",
                columns: table => new
                {
                    AnimalId = table.Column<int>(nullable: false),
                    AccessoriesId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalAccessories", x => new { x.AnimalId, x.AccessoriesId });
                    table.ForeignKey(
                        name: "FK_AnimalAccessories_Accessories_AccessoriesId",
                        column: x => x.AccessoriesId,
                        principalTable: "Accessories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimalAccessories_Animal_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animal",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingAnimal",
                columns: table => new
                {
                    AnimalId = table.Column<int>(nullable: false),
                    BookingId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingAnimal", x => new { x.AnimalId, x.BookingId });
                    table.ForeignKey(
                        name: "FK_BookingAnimal_Animal_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animal",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingAnimal_Booking_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Booking",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Accessories",
                columns: new[] { "ID", "Name", "PicturePath", "Price" },
                values: new object[,]
                {
                    { 1, "Strikje", "/images/accessories/Picture 1.png", 15.0 },
                    { 2, "Strikje Rood", "/images/accessories/Picture 2.png", 15.0 },
                    { 3, "Hoge Hoed", "/images/accessories/Picture 3.png", 30.0 },
                    { 4, "Kerstmuts", "/images/accessories/Picture 4.png", 25.0 },
                    { 5, "Maracas", "/images/accessories/Picture 5.png", 10.0 },
                    { 6, "Hamer", "/images/accessories/Picture 6.png", 3.0 },
                    { 7, "Vleugels", "/images/accessories/Picture 7.png", 40.0 },
                    { 8, "Afro Haar", "/images/accessories/Picture 8.png", 30.0 },
                    { 9, "Wandelstok", "/images/accessories/Picture 9.png", 15.0 },
                    { 10, "Bot", "/images/accessories/Picture 10.png", 1.0 },
                    { 11, "Hengels", "/images/accessories/Picture 11.png", 25.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimalAccessories_AccessoriesId",
                table: "AnimalAccessories",
                column: "AccessoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingAnimal_BookingId",
                table: "BookingAnimal",
                column: "BookingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimalAccessories");

            migrationBuilder.DropTable(
                name: "BookingAnimal");

            migrationBuilder.DropTable(
                name: "Accessories");

            migrationBuilder.DropTable(
                name: "Animal");

            migrationBuilder.DropTable(
                name: "Booking");
        }
    }
}
