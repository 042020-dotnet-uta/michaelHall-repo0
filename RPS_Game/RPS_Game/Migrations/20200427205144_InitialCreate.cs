using Microsoft.EntityFrameworkCore.Migrations;

namespace RPS_Game.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    PlayerID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.PlayerID);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    GameID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    p1PlayerID = table.Column<int>(nullable: true),
                    p2PlayerID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.GameID);
                    table.ForeignKey(
                        name: "FK_Games_Players_p1PlayerID",
                        column: x => x.p1PlayerID,
                        principalTable: "Players",
                        principalColumn: "PlayerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Games_Players_p2PlayerID",
                        column: x => x.p2PlayerID,
                        principalTable: "Players",
                        principalColumn: "PlayerID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rounds",
                columns: table => new
                {
                    RoundID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    p1Choice = table.Column<string>(nullable: true),
                    p2Choice = table.Column<string>(nullable: true),
                    WinnerPlayerID = table.Column<int>(nullable: true),
                    GameID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rounds", x => x.RoundID);
                    table.ForeignKey(
                        name: "FK_Rounds_Games_GameID",
                        column: x => x.GameID,
                        principalTable: "Games",
                        principalColumn: "GameID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rounds_Players_WinnerPlayerID",
                        column: x => x.WinnerPlayerID,
                        principalTable: "Players",
                        principalColumn: "PlayerID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_p1PlayerID",
                table: "Games",
                column: "p1PlayerID");

            migrationBuilder.CreateIndex(
                name: "IX_Games_p2PlayerID",
                table: "Games",
                column: "p2PlayerID");

            migrationBuilder.CreateIndex(
                name: "IX_Rounds_GameID",
                table: "Rounds",
                column: "GameID");

            migrationBuilder.CreateIndex(
                name: "IX_Rounds_WinnerPlayerID",
                table: "Rounds",
                column: "WinnerPlayerID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rounds");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
