using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeCooking.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Other",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Amount = table.Column<int>(nullable: false),
                    AmountType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Other", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Method = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Volume",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Millilitres = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Volume", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Weight",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Amount = table.Column<int>(nullable: false),
                    Grams = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weight", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FoodType = table.Column<string>(nullable: true),
                    WeightId = table.Column<int>(nullable: true),
                    VolumeId = table.Column<int>(nullable: true),
                    OtherId = table.Column<int>(nullable: true),
                    RecipeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ingredients_Other_OtherId",
                        column: x => x.OtherId,
                        principalTable: "Other",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ingredients_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ingredients_Volume_VolumeId",
                        column: x => x.VolumeId,
                        principalTable: "Volume",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ingredients_Weight_WeightId",
                        column: x => x.WeightId,
                        principalTable: "Weight",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_OtherId",
                table: "Ingredients",
                column: "OtherId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_RecipeId",
                table: "Ingredients",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_VolumeId",
                table: "Ingredients",
                column: "VolumeId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_WeightId",
                table: "Ingredients",
                column: "WeightId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Other");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "Volume");

            migrationBuilder.DropTable(
                name: "Weight");
        }
    }
}
