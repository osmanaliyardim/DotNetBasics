using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ORMFundamentals.Migrations
{
    /// <inheritdoc />
    public partial class GetOrdersByFilter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var getOrdersByFilter = @"CREATE OR ALTER PROCEDURE [dbo].[GetOrdersByFilter]
                                        @Month INT = NULL,
                                        @Year INT = NULL,
                                        @Status NVARCHAR(50) = NULL,
                                        @ProductId INT = NULL
                                    AS
                                    BEGIN
                                        SELECT *
                                        FROM [Orders]
                                        WHERE
                                            (@Month IS NULL OR MONTH(CreatedDate) = @Month) AND
                                            (@Year IS NULL OR YEAR(CreatedDate) = @Year) AND
                                            (@Status IS NULL OR Status = @Status) AND
                                            (@ProductId IS NULL OR ProductId = @ProductId)
                                    END
                                    GO";

            migrationBuilder.Sql(getOrdersByFilter);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
