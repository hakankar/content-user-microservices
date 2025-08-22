using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    full_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "now()"),
                    updated_date = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "timestamp", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_users_email",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.InsertData(
                table: "users",
                columns: ["id", "full_name", "email", "password"],
                values: new object[,]
                {
                    { new Guid("f6217d6c-e696-4d1f-8003-4e7f5599e057"), "Test User 1",  "test1@example.com",  "$2a$11$QkUk5GpbEimnbc0PLsCzMeKnBRpmiFt2aHdW2obD9E9NPi1EJdHGi" },
                    { new Guid("18a1fea0-5f8d-49d8-b3d4-b04c69f3c61a"), "Test User 2",  "test2@example.com",  "$2a$11$QkUk5GpbEimnbc0PLsCzMeKnBRpmiFt2aHdW2obD9E9NPi1EJdHGi" },
                    { new Guid("b09d8773-b5cb-4df8-8406-85afbd85121e"), "Test User 3",  "test3@example.com",  "$2a$11$QkUk5GpbEimnbc0PLsCzMeKnBRpmiFt2aHdW2obD9E9NPi1EJdHGi" },
                    { new Guid("b5da513c-0d01-4463-a6ac-c7eafbc870b4"), "Test User 4",  "test4@example.com",  "$2a$11$QkUk5GpbEimnbc0PLsCzMeKnBRpmiFt2aHdW2obD9E9NPi1EJdHGi" },
                    { new Guid("01a40485-5da2-477b-b33d-752264222866"), "Test User 5",  "test5@example.com",  "$2a$11$QkUk5GpbEimnbc0PLsCzMeKnBRpmiFt2aHdW2obD9E9NPi1EJdHGi" },
                    { new Guid("4afe3edc-4710-4d4c-bbd2-ed76a47e000a"), "Test User 6",  "test6@example.com",  "$2a$11$QkUk5GpbEimnbc0PLsCzMeKnBRpmiFt2aHdW2obD9E9NPi1EJdHGi" },
                    { new Guid("b23758b4-16e9-4043-adcd-b191eb128271"), "Test User 7",  "test7@example.com",  "$2a$11$QkUk5GpbEimnbc0PLsCzMeKnBRpmiFt2aHdW2obD9E9NPi1EJdHGi" },
                    { new Guid("bf06578f-5fa4-48f0-a8f6-77118a1086c9"), "Test User 8",  "test8@example.com",  "$2a$11$QkUk5GpbEimnbc0PLsCzMeKnBRpmiFt2aHdW2obD9E9NPi1EJdHGi" },
                    { new Guid("c5217f00-db2f-43b5-8e14-b9bec60c54e2"), "Test User 9",  "test9@example.com",  "$2a$11$QkUk5GpbEimnbc0PLsCzMeKnBRpmiFt2aHdW2obD9E9NPi1EJdHGi" },
                    { new Guid("8831facf-ef52-4f50-8cb1-789224a5cfbb"), "Test User 10", "test10@example.com", "$2a$11$QkUk5GpbEimnbc0PLsCzMeKnBRpmiFt2aHdW2obD9E9NPi1EJdHGi" },
                    { new Guid("d9d938b5-2c2a-418b-8c07-fcf92b25beb6"), "Test User 11", "test11@example.com", "$2a$11$QkUk5GpbEimnbc0PLsCzMeKnBRpmiFt2aHdW2obD9E9NPi1EJdHGi" },
                    { new Guid("9b7d3d85-4163-4d35-8347-563310ef7624"), "Test User 12", "test12@example.com", "$2a$11$QkUk5GpbEimnbc0PLsCzMeKnBRpmiFt2aHdW2obD9E9NPi1EJdHGi" },
                    { new Guid("17b221af-7592-46f9-abe5-0c7af6264e5e"), "Test User 13", "test13@example.com", "$2a$11$QkUk5GpbEimnbc0PLsCzMeKnBRpmiFt2aHdW2obD9E9NPi1EJdHGi" },
                    { new Guid("8d36a0ce-9026-49e5-ba0b-c0e0fb96e6cb"), "Test User 14", "test14@example.com", "$2a$11$QkUk5GpbEimnbc0PLsCzMeKnBRpmiFt2aHdW2obD9E9NPi1EJdHGi" },
                    { new Guid("eb5069c4-a376-492f-b0e5-75ff6ebb1794"), "Test User 15", "test15@example.com", "$2a$11$QkUk5GpbEimnbc0PLsCzMeKnBRpmiFt2aHdW2obD9E9NPi1EJdHGi" },
                    { new Guid("66fab669-1551-4764-9ea2-9696c0760308"), "Test User 16", "test16@example.com", "$2a$11$QkUk5GpbEimnbc0PLsCzMeKnBRpmiFt2aHdW2obD9E9NPi1EJdHGi" },
                    { new Guid("7761c43a-7980-4a1d-9d69-2c80375e731a"), "Test User 17", "test17@example.com", "$2a$11$QkUk5GpbEimnbc0PLsCzMeKnBRpmiFt2aHdW2obD9E9NPi1EJdHGi" },
                    { new Guid("61ae5b91-87f8-4c7c-bd64-f411e9cc0f24"), "Test User 18", "test18@example.com", "$2a$11$QkUk5GpbEimnbc0PLsCzMeKnBRpmiFt2aHdW2obD9E9NPi1EJdHGi" },
                    { new Guid("5443ce4b-2956-4e26-852e-612a557d510d"), "Test User 19", "test19@example.com", "$2a$11$QkUk5GpbEimnbc0PLsCzMeKnBRpmiFt2aHdW2obD9E9NPi1EJdHGi" },
                    { new Guid("5a30f60b-9fdd-43f5-8b70-6d82810beadd"), "Test User 20", "test20@example.com", "$2a$11$QkUk5GpbEimnbc0PLsCzMeKnBRpmiFt2aHdW2obD9E9NPi1EJdHGi" }
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
