using Microsoft.EntityFrameworkCore.Migrations;

namespace P0_JoshRaneri.Migrations
{
    public partial class migration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_InventoryLine",
                table: "InventoryLine");

            migrationBuilder.RenameTable(
                name: "InventoryLine",
                newName: "InventoryLines");

            migrationBuilder.RenameColumn(
                name: "QuantityOnHand",
                table: "InventoryLines",
                newName: "Quantity");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Customers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "CartId",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_InventoryLines",
                table: "InventoryLines",
                columns: new[] { "LocationId", "ProductId" });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UserName",
                table: "Customers",
                column: "UserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customers_UserName",
                table: "Customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InventoryLines",
                table: "InventoryLines");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "Customers");

            migrationBuilder.RenameTable(
                name: "InventoryLines",
                newName: "InventoryLine");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "InventoryLine",
                newName: "QuantityOnHand");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InventoryLine",
                table: "InventoryLine",
                columns: new[] { "LocationId", "ProductId" });
        }
    }
}
