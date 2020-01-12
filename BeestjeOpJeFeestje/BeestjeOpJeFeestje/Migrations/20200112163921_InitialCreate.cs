using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BeestjeOpJeFeestje.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientInfo",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: false),
                    MiddleName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientInfo", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Booking",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    IsStillBooking = table.Column<bool>(nullable: false),
                    ClientInfoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Booking_ClientInfo_ClientInfoId",
                        column: x => x.ClientInfoId,
                        principalTable: "ClientInfo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingProcesses",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingId = table.Column<int>(nullable: false),
                    ClientInfoId = table.Column<int>(nullable: false),
                    TotalPrice = table.Column<double>(nullable: false),
                    TotalDiscount = table.Column<double>(nullable: false),
                    BookingIsConfirmed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingProcesses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BookingProcesses_Booking_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Booking",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingProcesses_ClientInfo_ClientInfoId",
                        column: x => x.ClientInfoId,
                        principalTable: "ClientInfo",
                        principalColumn: "ID");
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
                    PicturePath = table.Column<string>(nullable: false),
                    BookingProcessID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animal", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Animal_BookingProcesses_BookingProcessID",
                        column: x => x.BookingProcessID,
                        principalTable: "BookingProcesses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
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
                    AnimalId = table.Column<int>(nullable: false),
                    BookingProcessID = table.Column<int>(nullable: true)
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
                    table.ForeignKey(
                        name: "FK_Accessories_BookingProcesses_BookingProcessID",
                        column: x => x.BookingProcessID,
                        principalTable: "BookingProcesses",
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
                table: "Animal",
                columns: new[] { "ID", "BookingProcessID", "Name", "PicturePath", "Price", "Type" },
                values: new object[,]
                {
                    { 1, null, "Aap", "/images/animals/aap.png", 4.5, "Jungle" },
                    { 2, null, "Olifant", "/images/animals/olifant.png", 16.5, "Jungle" },
                    { 3, null, "Zebra", "/images/animals/zebra.png", 1.5, "Jungle" },
                    { 4, null, "Leeuw", "/images/animals/leeuw.png", 29.5, "Jungle" },
                    { 5, null, "Hond", "/images/animals/doggo.png", 7.5, "Boerderij" },
                    { 6, null, "Ezel", "/images/animals/donkey.png", 30.5, "Boerderij" },
                    { 7, null, "Koe", "/images/animals/koe.png", 1.75, "Boerderij" },
                    { 8, null, "Eend", "/images/animals/duck.png", 0.75, "Boerderij" },
                    { 9, null, "Kuiken", "/images/animals/kuiken.png", 3.75, "Boerderij" },
                    { 10, null, "Pinguin", "/images/animals/pingwing.png", 40.0, "Sneeuw" },
                    { 11, null, "Ijsbeer", "/images/animals/ijsbeer.png", 11.75, "Sneeuw" },
                    { 12, null, "Zeehond", "/images/animals/zeehond.png", 23.75, "Sneeuw" },
                    { 13, null, "Kameel", "/images/animals/kameel.gif", 55.200000000000003, "Woestijn" },
                    { 14, null, "Slang", "/images/animals/slang.png", 11.199999999999999, "Woestijn" }
                });

            migrationBuilder.InsertData(
                table: "ClientInfo",
                columns: new[] { "ID", "Address", "Email", "FirstName", "LastName", "MiddleName" },
                values: new object[] { 1, "Prins Mauritsstraat 11", "huseyincaliskan32@gmail.com", "Huseyin", "Caliskan", null });

            migrationBuilder.InsertData(
                table: "Accessories",
                columns: new[] { "ID", "AnimalId", "BookingProcessID", "Name", "PicturePath", "Price" },
                values: new object[,]
                {
                    { 1, 1, null, "Strikje", "/images/accessories/Picture 1.png", 15.0 },
                    { 2, 1, null, "Strikje Rood", "/images/accessories/Picture 2.png", 15.0 },
                    { 3, 2, null, "Hoge Hoed", "/images/accessories/Picture 3.png", 30.0 },
                    { 4, 3, null, "Kerstmuts", "/images/accessories/Picture 4.png", 25.0 },
                    { 5, 4, null, "Maracas", "/images/accessories/Picture 5.png", 10.0 },
                    { 6, 5, null, "Hamer", "/images/accessories/Picture 6.png", 3.0 },
                    { 7, 6, null, "Vleugels", "/images/accessories/Picture 7.png", 40.0 },
                    { 8, 7, null, "Afro Haar", "/images/accessories/Picture 8.png", 30.0 },
                    { 9, 8, null, "Wandelstok", "/images/accessories/Picture 9.png", 15.0 },
                    { 10, 9, null, "Bot", "/images/accessories/Picture 10.png", 1.0 },
                    { 11, 9, null, "Hengels", "/images/accessories/Picture 11.png", 25.0 }
                });

            migrationBuilder.InsertData(
                table: "Booking",
                columns: new[] { "ID", "ClientInfoId", "Date", "IsStillBooking" },
                values: new object[] { 1, 1, new DateTime(2020, 1, 13, 0, 0, 0, 0, DateTimeKind.Local), false });

            migrationBuilder.InsertData(
                table: "BookingAnimal",
                columns: new[] { "AnimalId", "BookingId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "BookingAnimal",
                columns: new[] { "AnimalId", "BookingId" },
                values: new object[] { 4, 1 });

            migrationBuilder.InsertData(
                table: "BookingAnimal",
                columns: new[] { "AnimalId", "BookingId" },
                values: new object[] { 9, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Accessories_AnimalId",
                table: "Accessories",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_Accessories_BookingProcessID",
                table: "Accessories",
                column: "BookingProcessID");

            migrationBuilder.CreateIndex(
                name: "IX_Animal_BookingProcessID",
                table: "Animal",
                column: "BookingProcessID");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_ClientInfoId",
                table: "Booking",
                column: "ClientInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingAnimal_BookingId",
                table: "BookingAnimal",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingProcesses_BookingId",
                table: "BookingProcesses",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingProcesses_ClientInfoId",
                table: "BookingProcesses",
                column: "ClientInfoId");
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
                name: "BookingProcesses");

            migrationBuilder.DropTable(
                name: "Booking");

            migrationBuilder.DropTable(
                name: "ClientInfo");
        }
    }
}
