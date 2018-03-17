using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MemoryGame.Business.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    GameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    DateUpdated = table.Column<DateTime>(nullable: false),
                    Descr = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.GameId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    DateUpdated = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "GameLevels",
                columns: table => new
                {
                    GameLevelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    DateUpdated = table.Column<DateTime>(nullable: false),
                    FailureSoundUrl = table.Column<string>(nullable: true),
                    GameId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NumWords = table.Column<int>(nullable: false),
                    SuccessSoundUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameLevels", x => x.GameLevelId);
                    table.ForeignKey(
                        name: "FK_GameLevels_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WordLists",
                columns: table => new
                {
                    WordListId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    DateUpdated = table.Column<DateTime>(nullable: false),
                    GameId = table.Column<int>(nullable: false),
                    Randomize = table.Column<bool>(nullable: false),
                    Words = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordLists", x => x.WordListId);
                    table.ForeignKey(
                        name: "FK_WordLists_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserState",
                columns: table => new
                {
                    UserStateId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CurrentScore = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    DateUpdated = table.Column<DateTime>(nullable: false),
                    GameLevelId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserState", x => x.UserStateId);
                    table.ForeignKey(
                        name: "FK_UserState_GameLevels_GameLevelId",
                        column: x => x.GameLevelId,
                        principalTable: "GameLevels",
                        principalColumn: "GameLevelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserState_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameLevels_GameId",
                table: "GameLevels",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_UserState_GameLevelId",
                table: "UserState",
                column: "GameLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_UserState_UserId",
                table: "UserState",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WordLists_GameId",
                table: "WordLists",
                column: "GameId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserState");

            migrationBuilder.DropTable(
                name: "WordLists");

            migrationBuilder.DropTable(
                name: "GameLevels");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Games");
        }
    }
}
