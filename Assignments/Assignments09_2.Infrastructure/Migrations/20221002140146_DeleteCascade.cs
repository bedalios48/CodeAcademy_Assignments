using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignments09_2.Infrastructure.Migrations
{
    public partial class DeleteCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_customers_employees_SupportRepId",
                table: "customers");

            migrationBuilder.DropForeignKey(
                name: "FK_invoice_items_invoices_InvoiceId",
                table: "invoice_items");

            migrationBuilder.DropForeignKey(
                name: "FK_invoice_items_tracks_TrackId",
                table: "invoice_items");

            migrationBuilder.DropForeignKey(
                name: "FK_invoices_customers_CustomerId",
                table: "invoices");

            migrationBuilder.AddForeignKey(
                name: "FK_customers_employees_SupportRepId",
                table: "customers",
                column: "SupportRepId",
                principalTable: "employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_invoice_items_invoices_InvoiceId",
                table: "invoice_items",
                column: "InvoiceId",
                principalTable: "invoices",
                principalColumn: "InvoiceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_invoice_items_tracks_TrackId",
                table: "invoice_items",
                column: "TrackId",
                principalTable: "tracks",
                principalColumn: "TrackId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_invoices_customers_CustomerId",
                table: "invoices",
                column: "CustomerId",
                principalTable: "customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_customers_employees_SupportRepId",
                table: "customers");

            migrationBuilder.DropForeignKey(
                name: "FK_invoice_items_invoices_InvoiceId",
                table: "invoice_items");

            migrationBuilder.DropForeignKey(
                name: "FK_invoice_items_tracks_TrackId",
                table: "invoice_items");

            migrationBuilder.DropForeignKey(
                name: "FK_invoices_customers_CustomerId",
                table: "invoices");

            migrationBuilder.AddForeignKey(
                name: "FK_customers_employees_SupportRepId",
                table: "customers",
                column: "SupportRepId",
                principalTable: "employees",
                principalColumn: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_invoice_items_invoices_InvoiceId",
                table: "invoice_items",
                column: "InvoiceId",
                principalTable: "invoices",
                principalColumn: "InvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_invoice_items_tracks_TrackId",
                table: "invoice_items",
                column: "TrackId",
                principalTable: "tracks",
                principalColumn: "TrackId");

            migrationBuilder.AddForeignKey(
                name: "FK_invoices_customers_CustomerId",
                table: "invoices",
                column: "CustomerId",
                principalTable: "customers",
                principalColumn: "CustomerId");
        }
    }
}
