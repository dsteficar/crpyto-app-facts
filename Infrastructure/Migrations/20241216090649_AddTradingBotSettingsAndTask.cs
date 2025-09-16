using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTradingBotSettingsAndTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TradingBotTask",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradingBotTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TradingBotTask_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TradingBotSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TradingBotTaskId = table.Column<int>(type: "int", nullable: false),
                    BotStatus = table.Column<byte>(type: "tinyint", nullable: false),
                    MarketType = table.Column<byte>(type: "tinyint", nullable: false),
                    MarketDirection = table.Column<byte>(type: "tinyint", nullable: false),
                    ChannelStructureType = table.Column<byte>(type: "tinyint", nullable: false),
                    ChannelInfinityType = table.Column<byte>(type: "tinyint", nullable: false),
                    BaseAsset = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    QuoteAsset = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    PricePrecision = table.Column<int>(type: "int", nullable: false),
                    QuantityPrecision = table.Column<int>(type: "int", nullable: false),
                    SlotType = table.Column<byte>(type: "tinyint", nullable: false),
                    SlotSize = table.Column<decimal>(type: "decimal(20,8)", precision: 20, scale: 8, nullable: false),
                    StartingChannelPrice = table.Column<decimal>(type: "decimal(20,8)", precision: 20, scale: 8, nullable: false),
                    EndingChannelPrice = table.Column<decimal>(type: "decimal(20,8)", precision: 20, scale: 8, nullable: true),
                    StopLossPrice = table.Column<decimal>(type: "decimal(20,8)", precision: 20, scale: 8, nullable: false),
                    TriggerPrice = table.Column<decimal>(type: "decimal(20,8)", precision: 20, scale: 8, nullable: false),
                    QuantityType = table.Column<byte>(type: "tinyint", nullable: false),
                    QuantityPerSlot = table.Column<decimal>(type: "decimal(20,8)", precision: 20, scale: 8, nullable: false),
                    NumberOfSlots = table.Column<int>(type: "int", nullable: false),
                    OrdersCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradingBotSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TradingBotSettings_TradingBotTask_TradingBotTaskId",
                        column: x => x.TradingBotTaskId,
                        principalTable: "TradingBotTask",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TradingBotSettings_TradingBotTaskId",
                table: "TradingBotSettings",
                column: "TradingBotTaskId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TradingBotTask_UserId",
                table: "TradingBotTask",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TradingBotSettings");

            migrationBuilder.DropTable(
                name: "TradingBotTask");
        }
    }
}
