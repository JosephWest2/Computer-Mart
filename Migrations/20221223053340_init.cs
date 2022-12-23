using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComputerMart.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CPU",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    pictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CPU", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GPU",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    pictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GPU", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RAM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    pictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RAM", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SSD",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    pictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SSD", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Admin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Computer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CPUId = table.Column<int>(type: "int", nullable: true),
                    GPUId = table.Column<int>(type: "int", nullable: true),
                    RAMId = table.Column<int>(type: "int", nullable: true),
                    SSDId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    pictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Computer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Computer_CPU_CPUId",
                        column: x => x.CPUId,
                        principalTable: "CPU",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Computer_GPU_GPUId",
                        column: x => x.GPUId,
                        principalTable: "GPU",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Computer_RAM_RAMId",
                        column: x => x.RAMId,
                        principalTable: "RAM",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Computer_SSD_SSDId",
                        column: x => x.SSDId,
                        principalTable: "SSD",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Computer_CPUId",
                table: "Computer",
                column: "CPUId");

            migrationBuilder.CreateIndex(
                name: "IX_Computer_GPUId",
                table: "Computer",
                column: "GPUId");

            migrationBuilder.CreateIndex(
                name: "IX_Computer_RAMId",
                table: "Computer",
                column: "RAMId");

            migrationBuilder.CreateIndex(
                name: "IX_Computer_SSDId",
                table: "Computer",
                column: "SSDId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Computer");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "CPU");

            migrationBuilder.DropTable(
                name: "GPU");

            migrationBuilder.DropTable(
                name: "RAM");

            migrationBuilder.DropTable(
                name: "SSD");
        }
    }
}
