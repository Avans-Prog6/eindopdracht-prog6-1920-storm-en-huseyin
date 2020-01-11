using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BeestjeOpJeFeestje.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "Accessories",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Price = table.Column<double>(nullable: false),
                    PicturePath = table.Column<string>(nullable: false),
                    AnimalId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accessories", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Accessories_Animal_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animal",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
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
                columns: new[] { "ID", "AnimalId", "Name", "PicturePath", "Price" },
                values: new object[,]
                {
                    { 1, null, "Strikje", "/images/accessories/Picture 1.png", 15.0 },
                    { 11, null, "Hengels", "/images/accessories/Picture 11.png", 25.0 },
                    { 10, null, "Bot", "/images/accessories/Picture 10.png", 1.0 },
                    { 8, null, "Afro Haar", "/images/accessories/Picture 8.png", 30.0 },
                    { 7, null, "Vleugels", "/images/accessories/Picture 7.png", 40.0 },
                    { 9, null, "Wandelstok", "/images/accessories/Picture 9.png", 15.0 },
                    { 5, null, "Maracas", "/images/accessories/Picture 5.png", 10.0 },
                    { 4, null, "Kerstmuts", "/images/accessories/Picture 4.png", 25.0 },
                    { 3, null, "Hoge Hoed", "/images/accessories/Picture 3.png", 30.0 },
                    { 2, null, "Strikje Rood", "/images/accessories/Picture 2.png", 15.0 },
                    { 6, null, "Hamer", "/images/accessories/Picture 6.png", 3.0 }
                });

            migrationBuilder.InsertData(
                table: "Animal",
                columns: new[] { "ID", "Name", "PicturePath", "Price", "Type" },
                values: new object[,]
                {
                    { 9, "kuiken", "/images/animals/kuiken.png", 10.0, "Boerderij" },
                    { 13, "varken", "/images/animals/varken.png", 30.0, "Boerderij" },
                    { 12, "pingwing", "/images/animals/pingwing.png", 50.0, "Sneeuw" },
                    { 11, "olifant", "/images/animals/olifant.png", 90.0, "Jungle" },
                    { 10, "leeuw", "/images/animals/leeuw.png", 40.0, "Jungle" },
                    { 8, "koe", "/images/animals/koe.png", 50.0, "Boerderij" },
                    { 2, "bever", "/images/animals/bever.png", 20.0, "Boerderij" },
                    { 6, "ijsbeer", "/images/animals/ijsbeer.png", 90.0, "Sneeuw" },
                    { 5, "duck", "/images/animals/duck.png", 20.0, "Boerderij" },
                    { 4, "donkey", "/images/animals/donkey.png", 30.0, "Boerderij" },
                    { 3, "doggo", "/images/animals/doggo.png", 100.0, "Boerderij" },
                    { 14, "zebra", "/images/animals/zebra.png", 40.0, "Jungle" },
                    { 1, "aap", "/images/animals/aap.png", 50.0, "Jungle" },
                    { 7, "kat", "/images/animals/kat.png", 50.0, "Boerderij" },
                    { 15, "zeehond", "/images/animals/zeehond.png", 70.0, "Sneeuw" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accessories_AnimalId",
                table: "Accessories",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingAnimal_BookingId",
                table: "BookingAnimal",
                column: "BookingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accessories");

            migrationBuilder.DropTable(
                name: "BookingAnimal");

            migrationBuilder.DropTable(
                name: "Animal");

            migrationBuilder.DropTable(
                name: "Booking");
        }
    }
}
