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
                name: "contents",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    body = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "now()"),
                    updated_date = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_date = table.Column<DateTime>(type: "timestamp", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_contents", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_contents_title",
                table: "contents",
                column: "title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_contents_user_id",
                table: "contents",
                column: "user_id");

            migrationBuilder.InsertData(
                table: "contents",
                columns: ["id", "title", "body", "user_id"],
                values: new object[,]
                {
                    { new Guid("64cb0e26-0749-4c9a-80d4-55eab6db6a9b"), "Title 1",  "Body 1",  new Guid("f6217d6c-e696-4d1f-8003-4e7f5599e057") },
                    { new Guid("3c6be25e-34c0-42c7-82d4-13c96dd6ccdd"), "Title 2",  "Body 2",  new Guid("18a1fea0-5f8d-49d8-b3d4-b04c69f3c61a") },
                    { new Guid("6cd6e857-dc4f-4669-a2aa-3ab7b6e2ec65"), "Title 3",  "Body 3",  new Guid("b09d8773-b5cb-4df8-8406-85afbd85121e") },
                    { new Guid("c7289f69-48ad-4f06-b2a9-d7552b34fba2"), "Title 4",  "Body 4",  new Guid("b5da513c-0d01-4463-a6ac-c7eafbc870b4") },
                    { new Guid("b2db3f51-3761-47ae-8d53-cf0b1741764d"), "Title 5",  "Body 5",  new Guid("01a40485-5da2-477b-b33d-752264222866") },
                    { new Guid("f64cf06d-4307-49d5-8e01-7d0a2e71d68f"), "Title 6",  "Body 6",  new Guid("4afe3edc-4710-4d4c-bbd2-ed76a47e000a") },
                    { new Guid("45c151a4-33c6-4f48-b21b-282789b30921"), "Title 7",  "Body 7",  new Guid("b23758b4-16e9-4043-adcd-b191eb128271") },
                    { new Guid("d1e58082-5ee3-44ac-b171-37d2601f91f5"), "Title 8",  "Body 8",  new Guid("bf06578f-5fa4-48f0-a8f6-77118a1086c9") },
                    { new Guid("a3f8d31c-fecb-45a9-b476-80a7869e8596"), "Title 9",  "Body 9",  new Guid("c5217f00-db2f-43b5-8e14-b9bec60c54e2") },
                    { new Guid("143fbda1-1f4b-4a74-9540-3611b013ace4"), "Title 10", "Body 10", new Guid("8831facf-ef52-4f50-8cb1-789224a5cfbb") },
                    { new Guid("a926be7a-0318-44c4-baf0-49fd189fda38"), "Title 11", "Body 11", new Guid("d9d938b5-2c2a-418b-8c07-fcf92b25beb6") },
                    { new Guid("3aaef0be-cae6-45af-8e39-bcd2c013d7a7"), "Title 12", "Body 12", new Guid("9b7d3d85-4163-4d35-8347-563310ef7624") },
                    { new Guid("cb7dbf93-24dd-4c11-ae7e-24f6a115b76d"), "Title 13", "Body 13", new Guid("17b221af-7592-46f9-abe5-0c7af6264e5e") },
                    { new Guid("93afbbd4-8a6c-4d79-a9c5-ff33b223d51b"), "Title 14", "Body 14", new Guid("8d36a0ce-9026-49e5-ba0b-c0e0fb96e6cb") },
                    { new Guid("8cf77c7a-b4ad-4182-8168-bfe48a0fef9d"), "Title 15", "Body 15", new Guid("eb5069c4-a376-492f-b0e5-75ff6ebb1794") },
                    { new Guid("c1340a08-795a-4b1c-93af-d250a4204fd8"), "Title 16", "Body 16", new Guid("66fab669-1551-4764-9ea2-9696c0760308") },
                    { new Guid("3b80a460-640c-4d08-8eb9-5f80f3f5c676"), "Title 17", "Body 17", new Guid("7761c43a-7980-4a1d-9d69-2c80375e731a") },
                    { new Guid("0c60dd87-2727-45c0-ae32-f319cc88c21f"), "Title 18", "Body 18", new Guid("61ae5b91-87f8-4c7c-bd64-f411e9cc0f24") },
                    { new Guid("c79d2d94-8702-48c9-88b1-74ff1e309c36"), "Title 19", "Body 19", new Guid("5443ce4b-2956-4e26-852e-612a557d510d") },
                    { new Guid("67258c86-f93d-456e-a8f6-9c295bd348b5"), "Title 20", "Body 20", new Guid("5a30f60b-9fdd-43f5-8b70-6d82810beadd") },
                    { new Guid("21a50d62-fd38-4a86-a128-4202d5ed2f73"), "Title 21", "Body 21", new Guid("f6217d6c-e696-4d1f-8003-4e7f5599e057") },
                    { new Guid("8b5f42c6-8a53-4760-b29f-5943f51c1a5b"), "Title 22", "Body 22", new Guid("18a1fea0-5f8d-49d8-b3d4-b04c69f3c61a") },
                    { new Guid("4494b163-3d69-46ae-9afc-c5d35b3dd063"), "Title 23", "Body 23", new Guid("b09d8773-b5cb-4df8-8406-85afbd85121e") },
                    { new Guid("5ad7cbd4-47ac-46c4-93c1-5fc6ee35fc48"), "Title 24", "Body 24", new Guid("b5da513c-0d01-4463-a6ac-c7eafbc870b4") },
                    { new Guid("e3077b16-1aa1-4099-91ee-b6b4f4626f11"), "Title 25", "Body 25", new Guid("01a40485-5da2-477b-b33d-752264222866") },
                    { new Guid("7c0db7c7-47be-45f8-975f-2a7c1a491bc0"), "Title 26", "Body 26", new Guid("4afe3edc-4710-4d4c-bbd2-ed76a47e000a") },
                    { new Guid("9a369d44-5675-4e4c-9f8f-72e6df33e4ff"), "Title 27", "Body 27", new Guid("b23758b4-16e9-4043-adcd-b191eb128271") },
                    { new Guid("47e893b2-b9ab-4aeb-8da4-fcba46b9f4a1"), "Title 28", "Body 28", new Guid("bf06578f-5fa4-48f0-a8f6-77118a1086c9") },
                    { new Guid("a6d3b36e-ecff-41ae-8f61-913c6a6d78ab"), "Title 29", "Body 29", new Guid("c5217f00-db2f-43b5-8e14-b9bec60c54e2") },
                    { new Guid("da0285aa-83f0-453d-bd5d-89ff64aa6379"), "Title 30", "Body 30", new Guid("8831facf-ef52-4f50-8cb1-789224a5cfbb") },
                    { new Guid("399ddc88-c2d9-4e0a-a60e-94d783c6a7f2"), "Title 31", "Body 31", new Guid("d9d938b5-2c2a-418b-8c07-fcf92b25beb6") },
                    { new Guid("a21bcd02-12ea-41b5-bcd3-3a149f4dd6b6"), "Title 32", "Body 32", new Guid("9b7d3d85-4163-4d35-8347-563310ef7624") },
                    { new Guid("e8a43f52-5012-4269-a21a-4e9ea01dbe2d"), "Title 33", "Body 33", new Guid("17b221af-7592-46f9-abe5-0c7af6264e5e") },
                    { new Guid("e1f7ff73-368b-4ecf-b8d5-ef9f8cb36c41"), "Title 34", "Body 34", new Guid("8d36a0ce-9026-49e5-ba0b-c0e0fb96e6cb") },
                    { new Guid("62293a5e-dccf-4ad0-9a71-0dc14a96ee9f"), "Title 35", "Body 35", new Guid("eb5069c4-a376-492f-b0e5-75ff6ebb1794") },
                    { new Guid("ac360a48-8908-40da-9a3f-64ff5d0c0a89"), "Title 36", "Body 36", new Guid("66fab669-1551-4764-9ea2-9696c0760308") },
                    { new Guid("e3a61b20-f8e3-409f-a51f-90c2d5235d5b"), "Title 37", "Body 37", new Guid("7761c43a-7980-4a1d-9d69-2c80375e731a") },
                    { new Guid("998b35c4-b0ba-49a2-8d1b-264e4f0d3a48"), "Title 38", "Body 38", new Guid("61ae5b91-87f8-4c7c-bd64-f411e9cc0f24") },
                    { new Guid("13efb1c4-bcd9-4824-9465-74e48d742217"), "Title 39", "Body 39", new Guid("5443ce4b-2956-4e26-852e-612a557d510d") },
                    { new Guid("d85bd28e-a40a-4062-8922-76e208b55b5a"), "Title 40", "Body 40", new Guid("5a30f60b-9fdd-43f5-8b70-6d82810beadd") },
                    { new Guid("c9b285c6-4ad1-4f32-9860-3f0e72e54677"), "Title 41", "Body 41", new Guid("f6217d6c-e696-4d1f-8003-4e7f5599e057") },
                    { new Guid("5d9ac696-45b1-48c9-8e2b-093e74b5c28d"), "Title 42", "Body 42", new Guid("18a1fea0-5f8d-49d8-b3d4-b04c69f3c61a") },
                    { new Guid("fcf3e55a-8304-41d4-a72f-b7a5c957db7d"), "Title 43", "Body 43", new Guid("b09d8773-b5cb-4df8-8406-85afbd85121e") },
                    { new Guid("04b40c5b-b2c4-4f07-9e4f-cde8c86d2b68"), "Title 44", "Body 44", new Guid("b5da513c-0d01-4463-a6ac-c7eafbc870b4") },
                    { new Guid("d08f3ab8-8af2-47ac-b2ab-fd9d08ac33f2"), "Title 45", "Body 45", new Guid("01a40485-5da2-477b-b33d-752264222866") },
                    { new Guid("df9dc4da-b0a3-49b0-afe9-d57f0f58a9b6"), "Title 46", "Body 46", new Guid("4afe3edc-4710-4d4c-bbd2-ed76a47e000a") },
                    { new Guid("49f2a42b-7c54-4ac0-9555-0c6204e5c623"), "Title 47", "Body 47", new Guid("b23758b4-16e9-4043-adcd-b191eb128271") },
                    { new Guid("cd621ac2-7d73-49bd-82ec-3eec79eec2b4"), "Title 48", "Body 48", new Guid("bf06578f-5fa4-48f0-a8f6-77118a1086c9") },
                    { new Guid("776faae2-9a36-4b7a-b2ae-1e5b2dc0bd05"), "Title 49", "Body 49", new Guid("c5217f00-db2f-43b5-8e14-b9bec60c54e2") },
                    { new Guid("e41f3f3d-3ea1-4ac9-944c-28909d79a1cb"), "Title 50", "Body 50", new Guid("8831facf-ef52-4f50-8cb1-789224a5cfbb") }
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contents");
        }
    }
}
