using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiProjeKampi.WebApi.Migrations
{
    public partial class mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReservationSibleCustomerName",
                table: "groupReservations",
                newName: "ResponSibleCustomerName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ResponSibleCustomerName",
                table: "groupReservations",
                newName: "ReservationSibleCustomerName");
        }
    }
}
