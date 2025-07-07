using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleMarket.Api.Migrations
{
    /// <inheritdoc />
    public partial class addPriceType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
            name: "Price",
         table: "Products",
         type: "decimal(18,2)",
         nullable: false,
         oldClrType: typeof(decimal),
         oldType: "decimal(18,0)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
