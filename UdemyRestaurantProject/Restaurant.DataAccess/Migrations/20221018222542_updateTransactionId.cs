using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurant.DataAccess.Migrations
{
    public partial class updateTransactionId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TransactionId",
                table: "OrderHeader",
                newName: "SessionId");

            migrationBuilder.AddColumn<string>(
                name: "PaymentIntetnId",
                table: "OrderHeader",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentIntetnId",
                table: "OrderHeader");

            migrationBuilder.RenameColumn(
                name: "SessionId",
                table: "OrderHeader",
                newName: "TransactionId");
        }
    }
}
