using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScubaAPI.Migrations
{
    public partial class finished : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kontakt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Navn = table.Column<string>(type: "TEXT", nullable: true),
                    Mail = table.Column<string>(type: "TEXT", nullable: true),
                    Content = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kontakt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Type",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DykType = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Type", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Steder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TypeID = table.Column<int>(type: "INTEGER", nullable: false),
                    Lat = table.Column<decimal>(type: "TEXT", nullable: true),
                    Lon = table.Column<decimal>(type: "TEXT", nullable: true),
                    Navn = table.Column<string>(type: "TEXT", nullable: true),
                    Dybde = table.Column<string>(type: "TEXT", nullable: true),
                    Content = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Steder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Steder_Type_TypeID",
                        column: x => x.TypeID,
                        principalTable: "Type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StedID = table.Column<int>(type: "INTEGER", nullable: false),
                    Image = table.Column<string>(type: "TEXT", nullable: true),
                    Default = table.Column<string>(type: "TEXT", nullable: true),
                    Cover = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Steder_StedID",
                        column: x => x.StedID,
                        principalTable: "Steder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Turer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StedID = table.Column<int>(type: "INTEGER", nullable: false),
                    Navn = table.Column<string>(type: "TEXT", nullable: true),
                    Dato = table.Column<string>(type: "TEXT", nullable: true),
                    Tid = table.Column<string>(type: "TEXT", nullable: true),
                    Beskrivelse = table.Column<string>(type: "TEXT", nullable: true),
                    Pladser = table.Column<int>(type: "INTEGER", nullable: false),
                    Tilmeldte = table.Column<int>(type: "INTEGER", nullable: false),
                    Pris = table.Column<int>(type: "INTEGER", nullable: false),
                    Rabat = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Turer_Steder_StedID",
                        column: x => x.StedID,
                        principalTable: "Steder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Signup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TurID = table.Column<int>(type: "INTEGER", nullable: false),
                    Navn = table.Column<string>(type: "TEXT", nullable: true),
                    Mail = table.Column<string>(type: "TEXT", nullable: true),
                    Tlfnr = table.Column<string>(type: "TEXT", nullable: true),
                    Hojde = table.Column<int>(type: "INTEGER", nullable: false),
                    Vaegt = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Signup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Signup_Turer_TurID",
                        column: x => x.TurID,
                        principalTable: "Turer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_StedID",
                table: "Images",
                column: "StedID");

            migrationBuilder.CreateIndex(
                name: "IX_Signup_TurID",
                table: "Signup",
                column: "TurID");

            migrationBuilder.CreateIndex(
                name: "IX_Steder_TypeID",
                table: "Steder",
                column: "TypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Turer_StedID",
                table: "Turer",
                column: "StedID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Kontakt");

            migrationBuilder.DropTable(
                name: "Signup");

            migrationBuilder.DropTable(
                name: "Turer");

            migrationBuilder.DropTable(
                name: "Steder");

            migrationBuilder.DropTable(
                name: "Type");
        }
    }
}
