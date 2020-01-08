using Microsoft.EntityFrameworkCore.Migrations;

namespace BeestjeOpJeFeestje.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accessorieses",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    PicturePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accessorieses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Animal",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Type = table.Column<string>(maxLength: 30, nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Picture = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animal", x => x.ID);
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
                        name: "FK_AnimalAccessories_Accessorieses_AccessoriesId",
                        column: x => x.AccessoriesId,
                        principalTable: "Accessorieses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimalAccessories_Animal_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animal",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Accessorieses",
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimalAccessories");

            migrationBuilder.DropTable(
                name: "Accessorieses");

            migrationBuilder.DropTable(
                name: "Animal");
        }
    }
}
