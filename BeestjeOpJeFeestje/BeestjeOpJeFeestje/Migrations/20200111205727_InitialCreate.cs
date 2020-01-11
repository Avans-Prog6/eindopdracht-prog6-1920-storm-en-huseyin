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
                    AnimalId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accessories", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Accessories_Animal_AnimalId",
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
                table: "Animal",
                columns: new[] { "ID", "Name", "PicturePath", "Price", "Type" },
                values: new object[,]
                {
                    { 1, "Aap", "/images/animals/aap.png", 4.5, "Jungle" },
                    { 2, "Olifant", "/images/animals/olifant.png", 16.5, "Jungle" },
                    { 3, "Zebra", "/images/animals/zebra.png", 1.5, "Jungle" },
                    { 4, "Leeuw", "/images/animals/leeuw.png", 29.5, "Jungle" },
                    { 5, "Hond", "/images/animals/doggo.png", 7.5, "Boerderij" },
                    { 6, "Ezel", "/images/animals/donkey.png", 30.5, "Boerderij" },
                    { 7, "Koe", "/images/animals/koe.png", 1.75, "Boerderij" },
                    { 8, "Eend", "/images/animals/duck.png", 0.75, "Boerderij" },
                    { 9, "Kuiken", "/images/animals/kuiken.png", 3.75, "Boerderij" },
                    { 10, "Pinguin", "/images/animals/pingwing.png", 40.0, "Sneeuw" },
                    { 11, "Ijsbeer", "/images/animals/ijsbeer.png", 11.75, "Sneeuw" },
                    { 12, "Zeehond", "/images/animals/zeehond.png", 23.75, "Sneeuw" },
                    { 13, "Kameel", "/images/animals/kameel.gif", 55.200000000000003, "Woestijn" },
                    { 14, "Slang", "/images/animals/slang.png", 11.199999999999999, "Woestijn" }
                });

            migrationBuilder.InsertData(
                table: "Booking",
                columns: new[] { "ID", "Date" },
                values: new object[] { 1, new DateTime(2020, 1, 12, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.InsertData(
                table: "Accessories",
                columns: new[] { "ID", "AnimalId", "Name", "PicturePath", "Price" },
                values: new object[,]
                {
                    { 1, 1, "Strikje", "/images/accessories/Picture 1.png", 15.0 },
                    { 2, 1, "Strikje Rood", "/images/accessories/Picture 2.png", 15.0 },
                    { 3, 2, "Hoge Hoed", "/images/accessories/Picture 3.png", 30.0 },
                    { 4, 3, "Kerstmuts", "/images/accessories/Picture 4.png", 25.0 },
                    { 5, 4, "Maracas", "/images/accessories/Picture 5.png", 10.0 },
                    { 6, 5, "Hamer", "/images/accessories/Picture 6.png", 3.0 },
                    { 7, 6, "Vleugels", "/images/accessories/Picture 7.png", 40.0 },
                    { 8, 7, "Afro Haar", "/images/accessories/Picture 8.png", 30.0 },
                    { 9, 8, "Wandelstok", "/images/accessories/Picture 9.png", 15.0 },
                    { 10, 9, "Bot", "/images/accessories/Picture 10.png", 1.0 },
                    { 11, 9, "Hengels", "/images/accessories/Picture 11.png", 25.0 }
                });

            migrationBuilder.InsertData(
                table: "BookingAnimal",
                columns: new[] { "AnimalId", "BookingId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 4, 1 },
                    { 9, 1 }
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
