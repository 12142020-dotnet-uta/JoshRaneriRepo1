﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RpsGame_NoDb.Migrations
{
    public partial class initmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "players",
                columns: table => new
                {
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_players", x => x.PlayerId);
                });

            migrationBuilder.CreateTable(
                name: "matches",
                columns: table => new
                {
                    MatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Player1PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Player2PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TiePlayerPlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MatchWon = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_matches", x => x.MatchId);
                    table.ForeignKey(
                        name: "FK_matches_players_Player1PlayerId",
                        column: x => x.Player1PlayerId,
                        principalTable: "players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_matches_players_Player2PlayerId",
                        column: x => x.Player2PlayerId,
                        principalTable: "players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_matches_players_TiePlayerPlayerId",
                        column: x => x.TiePlayerPlayerId,
                        principalTable: "players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "rounds",
                columns: table => new
                {
                    RoundId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Player1Choice = table.Column<int>(type: "int", nullable: false),
                    Player2Choice = table.Column<int>(type: "int", nullable: false),
                    WinningPlayerPlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rounds", x => x.RoundId);
                    table.ForeignKey(
                        name: "FK_rounds_players_WinningPlayerPlayerId",
                        column: x => x.WinningPlayerPlayerId,
                        principalTable: "players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_matches_Player1PlayerId",
                table: "matches",
                column: "Player1PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_matches_Player2PlayerId",
                table: "matches",
                column: "Player2PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_matches_TiePlayerPlayerId",
                table: "matches",
                column: "TiePlayerPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_rounds_WinningPlayerPlayerId",
                table: "rounds",
                column: "WinningPlayerPlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "matches");

            migrationBuilder.DropTable(
                name: "rounds");

            migrationBuilder.DropTable(
                name: "players");
        }
    }
}
