using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend.Infrastructure.Migrations.AppDbMigrations
{
    /// <inheritdoc />
    public partial class APPSNAPSHOT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressId = table.Column<Guid>(type: "uuid", nullable: false),
                    AddressTypeId = table.Column<int>(type: "integer", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CountryName = table.Column<string>(type: "text", nullable: false),
                    CountryAlias = table.Column<string>(type: "text", nullable: false),
                    StateAlias = table.Column<string>(type: "text", nullable: false),
                    StateName = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    Region = table.Column<string>(type: "text", nullable: false),
                    StreetName = table.Column<string>(type: "text", nullable: false),
                    StreetNumber = table.Column<string>(type: "text", nullable: false),
                    PostalCode = table.Column<string>(type: "text", nullable: false),
                    Primary = table.Column<bool>(type: "boolean", nullable: false),
                    AgentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                });

            migrationBuilder.CreateTable(
                name: "AgentType",
                columns: table => new
                {
                    AgentTypeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AgentTypeName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentType", x => x.AgentTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryName = table.Column<string>(type: "text", nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "ProductType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    ProfileId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    DisplayName = table.Column<string>(type: "text", nullable: true),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    CNAE = table.Column<string>(type: "text", nullable: true),
                    CNPJ = table.Column<string>(type: "text", nullable: true),
                    CPF = table.Column<string>(type: "text", nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.ProfileId);
                });

            migrationBuilder.CreateTable(
                name: "SubCategory",
                columns: table => new
                {
                    SubCategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    SubCategoryName = table.Column<string>(type: "text", nullable: true),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategory", x => x.SubCategoryId);
                    table.ForeignKey(
                        name: "FK_SubCategory_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Agent",
                columns: table => new
                {
                    AgentId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    AgentTypeId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    ProfileId = table.Column<Guid>(type: "uuid", nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agent", x => x.AgentId);
                    table.ForeignKey(
                        name: "FK_Agent_AgentType_AgentTypeId",
                        column: x => x.AgentTypeId,
                        principalTable: "AgentType",
                        principalColumn: "AgentTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Agent_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "ProfileId");
                });

            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    EmailAddressId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    AgentId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmailAddress = table.Column<string>(type: "text", nullable: true),
                    Primary = table.Column<bool>(type: "boolean", nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.EmailAddressId);
                    table.ForeignKey(
                        name: "FK_Emails_Agent_AgentId",
                        column: x => x.AgentId,
                        principalTable: "Agent",
                        principalColumn: "AgentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Phones",
                columns: table => new
                {
                    PhoneId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    AgentId = table.Column<Guid>(type: "uuid", nullable: false),
                    AreaCode = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    Primary = table.Column<bool>(type: "boolean", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phones", x => x.PhoneId);
                    table.ForeignKey(
                        name: "FK_Phones_Agent_AgentId",
                        column: x => x.AgentId,
                        principalTable: "Agent",
                        principalColumn: "AgentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    SKU = table.Column<string>(type: "text", nullable: false),
                    GTIN = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ColorName = table.Column<string>(type: "text", nullable: true),
                    MetricUnitName = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<double>(type: "double precision", nullable: false),
                    TotalWeight = table.Column<double>(type: "double precision", nullable: true),
                    LiquidWeight = table.Column<double>(type: "double precision", nullable: true),
                    ProductTypeId = table.Column<int>(type: "integer", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: true),
                    SubCategoryId = table.Column<Guid>(type: "uuid", nullable: true),
                    AgentId = table.Column<Guid>(type: "uuid", nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Product_Agent_AgentId",
                        column: x => x.AgentId,
                        principalTable: "Agent",
                        principalColumn: "AgentId");
                    table.ForeignKey(
                        name: "FK_Product_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId");
                    table.ForeignKey(
                        name: "FK_Product_ProductType_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_SubCategory_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategory",
                        principalColumn: "SubCategoryId");
                });

            migrationBuilder.CreateTable(
                name: "ProductMedia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    MediaType = table.Column<int>(type: "integer", nullable: false),
                    MediaURL = table.Column<string>(type: "text", nullable: false),
                    Priority = table.Column<long>(type: "bigint", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductMedia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductMedia_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductVariants",
                columns: table => new
                {
                    VariantId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    SKU = table.Column<string>(type: "text", nullable: false),
                    GTIN = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ColorName = table.Column<string>(type: "text", nullable: true),
                    LiquidWeight = table.Column<double>(type: "double precision", nullable: true),
                    TotalWeight = table.Column<double>(type: "double precision", nullable: true),
                    Value = table.Column<double>(type: "double precision", nullable: true),
                    OverwriteValue = table.Column<bool>(type: "boolean", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVariants", x => x.VariantId);
                    table.ForeignKey(
                        name: "FK_ProductVariants_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "AddressId", "Active", "AddressTypeId", "AgentId", "City", "CountryAlias", "CountryName", "Created", "CreatedBy", "PostalCode", "Primary", "Region", "StateAlias", "StateName", "StreetName", "StreetNumber", "TenantId", "Updated", "UpdatedBy" },
                values: new object[] { new Guid("6ac52fbb-db9b-4210-9160-aac4a39285c3"), true, 1, new Guid("ca7f59ef-02aa-45f0-af27-91da78da253f"), "Atibaia", "BR", "Brazil", null, null, "12947320", true, "Sao Paulo/Braganca", "SP", "Sao Paulo", "Rua daora", "1337", new Guid("cabaa57a-37ff-4871-be7d-0187ed3534a5"), null, null });

            migrationBuilder.InsertData(
                table: "AgentType",
                columns: new[] { "AgentTypeId", "AgentTypeName" },
                values: new object[,]
                {
                    { 1, "Company" },
                    { 2, "Customer" },
                    { 3, "Employee" },
                    { 4, "Vendor" },
                    { 5, "Physical Store" }
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "CategoryId", "Active", "CategoryName", "Created", "CreatedBy", "TenantId", "Updated", "UpdatedBy" },
                values: new object[] { new Guid("63cf51c6-e90e-4725-b6c3-1c40986d6847"), true, "Eletronic", new DateTime(2024, 3, 9, 19, 49, 26, 327, DateTimeKind.Utc).AddTicks(4721), null, new Guid("cabaa57a-37ff-4871-be7d-0187ed3534a5"), null, null });

            migrationBuilder.InsertData(
                table: "ProductType",
                columns: new[] { "Id", "Active", "Description", "Name" },
                values: new object[,]
                {
                    { 1, true, "Crafting material", "Feedstock" },
                    { 2, true, "Intermediate Product/Crafting material", "Intermediate Component" },
                    { 3, true, "Final Product", "Product" }
                });

            migrationBuilder.InsertData(
                table: "Profiles",
                columns: new[] { "ProfileId", "Active", "CNAE", "CNPJ", "CPF", "Created", "CreatedBy", "DisplayName", "FirstName", "LastName", "TenantId", "Updated", "UpdatedBy" },
                values: new object[] { new Guid("bf489dd5-c2d6-444d-a761-4184b6471b96"), false, null, "1234556789", "9876544321", null, null, "Leonardo B.", "Leonardo", "Bruni", new Guid("cabaa57a-37ff-4871-be7d-0187ed3534a5"), null, null });

            migrationBuilder.InsertData(
                table: "Agent",
                columns: new[] { "AgentId", "Active", "AgentTypeId", "Created", "CreatedBy", "Name", "ProfileId", "TenantId", "Updated", "UpdatedBy", "UserId" },
                values: new object[,]
                {
                    { new Guid("4c223cf3-a4ee-4bc3-82a0-763a73673114"), true, 4, null, null, "Fastcar AutoParts", null, new Guid("cabaa57a-37ff-4871-be7d-0187ed3534a5"), null, null, null },
                    { new Guid("a5c4423a-4e92-4f3d-a4eb-89f1cd1a03d7"), true, 2, null, null, "Simas Turbo Mecanica e Performance", null, new Guid("cabaa57a-37ff-4871-be7d-0187ed3534a5"), null, null, null },
                    { new Guid("ca7f59ef-02aa-45f0-af27-91da78da253f"), true, 3, null, null, "Leo", new Guid("bf489dd5-c2d6-444d-a761-4184b6471b96"), new Guid("cabaa57a-37ff-4871-be7d-0187ed3534a5"), null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "Active", "AgentId", "CategoryId", "ColorName", "Created", "CreatedBy", "Description", "GTIN", "LiquidWeight", "MetricUnitName", "Name", "ProductTypeId", "SKU", "SubCategoryId", "TenantId", "TotalWeight", "Updated", "UpdatedBy", "Value" },
                values: new object[] { new Guid("dae5787c-b3af-4bbd-849f-bcbc3c0edde8"), true, null, null, "Azul-Marinho", new DateTime(2024, 3, 9, 19, 49, 26, 327, DateTimeKind.Utc).AddTicks(4871), null, "Produto de teste gerado na migration - SampleCompany", "012345678910111213", 0.0, "G", "Motorola Moto E", 3, "202401", null, new Guid("ae100414-8fbb-4286-839a-5bafc51a84fb"), 0.0, null, null, 100.0 });

            migrationBuilder.InsertData(
                table: "SubCategory",
                columns: new[] { "SubCategoryId", "Active", "CategoryId", "Created", "CreatedBy", "SubCategoryName", "TenantId", "Updated", "UpdatedBy" },
                values: new object[] { new Guid("cb1dd75f-6cf2-4c6e-b050-ee80444ad1c6"), true, new Guid("63cf51c6-e90e-4725-b6c3-1c40986d6847"), new DateTime(2024, 3, 9, 19, 49, 26, 327, DateTimeKind.Utc).AddTicks(4824), null, "Smartphone", new Guid("cabaa57a-37ff-4871-be7d-0187ed3534a5"), null, null });

            migrationBuilder.InsertData(
                table: "Emails",
                columns: new[] { "EmailAddressId", "Active", "AgentId", "Created", "CreatedBy", "EmailAddress", "Primary", "TenantId", "Updated", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("5f26a4d5-e05f-4d07-ba3d-25324b567b00"), false, new Guid("ca7f59ef-02aa-45f0-af27-91da78da253f"), null, null, "lbruni10@gmail.com", true, new Guid("cabaa57a-37ff-4871-be7d-0187ed3534a5"), null, null },
                    { new Guid("709d8de8-bdb0-4703-81d0-e8d3764ed263"), false, new Guid("ca7f59ef-02aa-45f0-af27-91da78da253f"), null, null, "leo.bruni130@gmail.com", false, new Guid("cabaa57a-37ff-4871-be7d-0187ed3534a5"), null, null }
                });

            migrationBuilder.InsertData(
                table: "Phones",
                columns: new[] { "PhoneId", "Active", "AgentId", "AreaCode", "Created", "CreatedBy", "PhoneNumber", "Primary", "TenantId", "Updated", "UpdatedBy" },
                values: new object[] { new Guid("7c6a7504-6747-4a5d-b2df-c43484371138"), false, new Guid("ca7f59ef-02aa-45f0-af27-91da78da253f"), "55", null, null, "11955506737", true, new Guid("cabaa57a-37ff-4871-be7d-0187ed3534a5"), null, null });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "Active", "AgentId", "CategoryId", "ColorName", "Created", "CreatedBy", "Description", "GTIN", "LiquidWeight", "MetricUnitName", "Name", "ProductTypeId", "SKU", "SubCategoryId", "TenantId", "TotalWeight", "Updated", "UpdatedBy", "Value" },
                values: new object[] { new Guid("d022496e-cbc3-4ca5-933d-85d032b2adf1"), true, null, new Guid("63cf51c6-e90e-4725-b6c3-1c40986d6847"), "Preto", new DateTime(2024, 3, 9, 19, 49, 26, 327, DateTimeKind.Utc).AddTicks(4863), null, "Produto de teste gerado na migration - Aurora", "012345678910111213", 0.13, "G", "Samsung Galaxy S4", 3, "202401", new Guid("cb1dd75f-6cf2-4c6e-b050-ee80444ad1c6"), new Guid("cabaa57a-37ff-4871-be7d-0187ed3534a5"), 0.13, null, null, 604.99000000000001 });

            migrationBuilder.CreateIndex(
                name: "IX_Agent_AgentTypeId",
                table: "Agent",
                column: "AgentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Agent_ProfileId",
                table: "Agent",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Emails_AgentId",
                table: "Emails",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_Phones_AgentId",
                table: "Phones",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_AgentId",
                table: "Product",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryId",
                table: "Product",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductTypeId",
                table: "Product",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_SubCategoryId",
                table: "Product",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductMedia_ProductId",
                table: "ProductMedia",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariants_ProductId",
                table: "ProductVariants",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategory_CategoryId",
                table: "SubCategory",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Emails");

            migrationBuilder.DropTable(
                name: "Phones");

            migrationBuilder.DropTable(
                name: "ProductMedia");

            migrationBuilder.DropTable(
                name: "ProductVariants");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Agent");

            migrationBuilder.DropTable(
                name: "ProductType");

            migrationBuilder.DropTable(
                name: "SubCategory");

            migrationBuilder.DropTable(
                name: "AgentType");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
