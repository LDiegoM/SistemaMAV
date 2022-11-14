using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Web.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemMantenimiento",
                columns: table => new
                {
                    ItemMantenimientoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Detalle = table.Column<string>(type: "varchar(100)", nullable: false),
                    KilometrosPredeterminado = table.Column<int>(type: "INTEGER", nullable: true),
                    TiempoPredeterminado = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemMantenimiento", x => x.ItemMantenimientoId);
                });

            migrationBuilder.CreateTable(
                name: "Marca",
                columns: table => new
                {
                    MarcaId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Detalle = table.Column<string>(type: "varchar(50)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marca", x => x.MarcaId);
                });

            migrationBuilder.CreateTable(
                name: "TipoUnidad",
                columns: table => new
                {
                    TipoUnidadId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Detalle = table.Column<string>(type: "varchar(50)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoUnidad", x => x.TipoUnidadId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Modelo",
                columns: table => new
                {
                    ModeloId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Detalle = table.Column<string>(type: "varchar(250)", nullable: false),
                    MarcaId = table.Column<int>(type: "INTEGER", nullable: false),
                    TipoUnidadId = table.Column<int>(type: "INTEGER", nullable: false),
                    FechaAlta = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FechaBaja = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modelo", x => x.ModeloId);
                    table.ForeignKey(
                        name: "FK_Modelo_Marca_MarcaId",
                        column: x => x.MarcaId,
                        principalTable: "Marca",
                        principalColumn: "MarcaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Modelo_TipoUnidad_TipoUnidadId",
                        column: x => x.TipoUnidadId,
                        principalTable: "TipoUnidad",
                        principalColumn: "TipoUnidadId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Planilla",
                columns: table => new
                {
                    PlanillaId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ModeloId = table.Column<int>(type: "INTEGER", nullable: false),
                    Detalle = table.Column<string>(type: "varchar(270)", nullable: false),
                    AnioFabricacion = table.Column<int>(type: "INTEGER", nullable: true),
                    Version = table.Column<int>(type: "INTEGER", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planilla", x => x.PlanillaId);
                    table.ForeignKey(
                        name: "FK_Planilla_Modelo_ModeloId",
                        column: x => x.ModeloId,
                        principalTable: "Modelo",
                        principalColumn: "ModeloId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlanillaItem",
                columns: table => new
                {
                    PlanillaItemId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlanillaId = table.Column<int>(type: "INTEGER", nullable: false),
                    ItemMantenimientoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Kilometros = table.Column<int>(type: "INTEGER", nullable: true),
                    Meses = table.Column<int>(type: "INTEGER", nullable: true),
                    Recomendaciones = table.Column<string>(type: "TEXT", nullable: true),
                    Observaciones = table.Column<string>(type: "TEXT", nullable: true),
                    InfoExtra = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanillaItem", x => x.PlanillaItemId);
                    table.ForeignKey(
                        name: "FK_PlanillaItem_ItemMantenimiento_ItemMantenimientoId",
                        column: x => x.ItemMantenimientoId,
                        principalTable: "ItemMantenimiento",
                        principalColumn: "ItemMantenimientoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlanillaItem_Planilla_PlanillaId",
                        column: x => x.PlanillaId,
                        principalTable: "Planilla",
                        principalColumn: "PlanillaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "admin", null, "Administrador", "ADMINISTRADOR" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "admin", 0, "9250d0a9-2184-4ce1-80ac-73a6d3d811de", "admin@mav.com", true, true, null, "ADMIN@MAV.COM", "ADMINISTRADOR", "AQAAAAEAACcQAAAAEH7g5Hh0bEetkum/buwj0Y2cRVFHWuUe4UZec+vEugilfLt8KnFaPvpVDlvy8X05iw==", null, false, "OYWWGEEW7PAULQNQAM4DL5FERLEQLEPP", false, "Administrador" },
                    { "prop", 0, "e7513c25-eb2e-4101-9480-57c00b84d1e2", "propietario@mav.com", true, true, null, "PROPIETARIO@MAV.COM", "PROPIETARIO", "AQAAAAIAAYagAAAAEKnMEx8PAlltiKgEBDYvgoBQAG/ea2AdmcsjU7sf8tb/gq+tI064hbMkuhXs59a+Jw==", null, false, "EQXVLBMZVTIJMWVT6EDOUWZ3YFXA5FIP", false, "Propietario" }
                });

            migrationBuilder.InsertData(
                table: "ItemMantenimiento",
                columns: new[] { "ItemMantenimientoId", "Detalle", "KilometrosPredeterminado", "TiempoPredeterminado" },
                values: new object[,]
                {
                    { 1, "Cambio de aceite", 10000, 12 },
                    { 2, "Verificar presión de neumáticos", 2000, 2 },
                    { 3, "Correa de accesorios: sustituir", 50000, null },
                    { 4, "Control de fugas de fluidos", null, null },
                    { 5, "Prueba en ruta", null, null },
                    { 6, "Sustituir bujías", 30000, null },
                    { 7, "Correa de distribución: inspeccionar", null, null },
                    { 8, "Correa de distribución: sustituir", 50000, null },
                    { 9, "Aceite de transmisión", null, null },
                    { 10, "Líquido de frenos", null, null },
                    { 11, "Fluido de la dirección asistida", null, null },
                    { 12, "Pedal de embrague", null, null },
                    { 13, "Filtro de aire: inspeccionar", null, null },
                    { 14, "Filtro de aire: sustituir", null, null },
                    { 15, "Filtro de combustible: sustituir", null, null },
                    { 16, "Prefiltro de bomba de combustible", null, null },
                    { 17, "Sistema de aire acondicionado", null, null },
                    { 18, "Sistema de refrigeración de motor", null, null }
                });

            migrationBuilder.InsertData(
                table: "Marca",
                columns: new[] { "MarcaId", "Activo", "Detalle" },
                values: new object[,]
                {
                    { 1, true, "Ford" },
                    { 2, true, "Chevrolet" }
                });

            migrationBuilder.InsertData(
                table: "TipoUnidad",
                columns: new[] { "TipoUnidadId", "Activo", "Detalle" },
                values: new object[,]
                {
                    { 1, true, "Automóvil" },
                    { 2, true, "Motocicleta" },
                    { 3, true, "Vehículo pesado" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "admin", "admin" });

            migrationBuilder.InsertData(
                table: "Modelo",
                columns: new[] { "ModeloId", "Detalle", "FechaAlta", "FechaBaja", "MarcaId", "TipoUnidadId" },
                values: new object[,]
                {
                    { 1, "Chevrolet Agile", new DateTime(2005, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, 1 },
                    { 2, "Ford Fiesta Kinetic Design", new DateTime(2009, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "Planilla",
                columns: new[] { "PlanillaId", "Activo", "AnioFabricacion", "Detalle", "ModeloId", "Version" },
                values: new object[] { 1, true, 2015, "Chevrolet Agile", 1, 1 });

            migrationBuilder.InsertData(
                table: "PlanillaItem",
                columns: new[] { "PlanillaItemId", "InfoExtra", "ItemMantenimientoId", "Kilometros", "Meses", "Observaciones", "PlanillaId", "Recomendaciones" },
                values: new object[,]
                {
                    { 1, "Cambie el aceite del motor y el filtro de aceite conforme a los intervalos de tiempo o kilómetros recorridos, ya que los mismos pierden sus propiedades de lubricación no solo debido al funcionamiento del motor, sino también a su envejecimiento. Verificar el nivel de aceite semanalmente o antes de iniciar un viaje de más de 50 kilómetros. Tener en cuenta que el gasto promedio de aceite es de 0,8 litros cada 1000 km.", 1, 10000, 12, "Cambiar y verificar nivel con el motor a temperatura de operación normal.", 1, "Utilizar aceites Elaion F50 d1 (Dexos 1 API-SN ILSAC GF-5, grado SAE 5W30)." },
                    { 2, null, 2, 2000, 2, null, 1, "No esperar al siguiente servicio. Verificar frecuentemente en estaciones de servicio o talleres especializados en neumáticos." },
                    { 3, "Verificar el estado de los tensores.", 3, 50000, null, null, 1, null },
                    { 4, "Inspeccionar fugas de aceite, líquido refrigerante, de dirección, de freno, grasa de la caja de cambios y líquido lava-parabrisas.", 4, 10000, 12, null, 1, null },
                    { 5, "Inspeccionar si el vehículo presenta anomalías ocasionales. Realizar una prueba en ruta después de la inspección.", 5, 30000, null, null, 1, null },
                    { 6, null, 6, 30000, null, null, 1, null },
                    { 7, "Inspeccionar el estado de la correa y del tensor automático.", 7, null, null, "Primer control a los 20.000 Km, luego cada 50.000 Km.", 1, null },
                    { 8, null, 8, 50000, null, null, 1, null },
                    { 9, "Verificar el nivel y sustituir si fuera necesario.", 9, 10000, 12, "Caja de velocidades", 1, "Aceite mineral para cajas de cambios, SAE 75W85, engranajes helicoidales, color rojo." },
                    { 10, "Verificar el nivel y completar al nivel si hay fuga. Se debe corregir inmediatamente si hay fuga.", 10, 20000, 24, null, 1, "Líquido de frenos DOT 4 de ACDelco." },
                    { 11, "Verificar el nivel. No requiere cambio, excepto baja del nivel.", 11, 10000, null, null, 1, "Aceite Dexron II de ACDelco." },
                    { 12, "Comprobar el recorrido.", 12, 30000, null, null, 1, null },
                    { 13, "Limpiar el filtro si fuera necesario.", 13, null, null, "Primer control a los 20.000 Km, luego cada 30.000 Km.", 1, null },
                    { 14, null, 14, 30000, null, null, 1, null },
                    { 15, null, 15, 20000, null, null, 1, null },
                    { 16, "Colador de la bomba de combustible.", 16, 80000, null, null, 1, null },
                    { 17, "Controlar en cada inspección. No requiere sustitución, excepto que haya fuga.", 17, 10000, null, null, 1, "Gas R134a" },
                    { 18, "Cambiar el líquido refrigerante y reparar posibles fugas. Antes de cambiar se recomienda limpiar el sistema de refrigeración.", 18, null, null, null, 1, "Inspeccionar el nivel de líquido refrigerante mensualmente." }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Modelo_MarcaId",
                table: "Modelo",
                column: "MarcaId");

            migrationBuilder.CreateIndex(
                name: "IX_Modelo_TipoUnidadId",
                table: "Modelo",
                column: "TipoUnidadId");

            migrationBuilder.CreateIndex(
                name: "IX_Planilla_ModeloId",
                table: "Planilla",
                column: "ModeloId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanillaItem_ItemMantenimientoId",
                table: "PlanillaItem",
                column: "ItemMantenimientoId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanillaItem_PlanillaId",
                table: "PlanillaItem",
                column: "PlanillaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "PlanillaItem");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ItemMantenimiento");

            migrationBuilder.DropTable(
                name: "Planilla");

            migrationBuilder.DropTable(
                name: "Modelo");

            migrationBuilder.DropTable(
                name: "Marca");

            migrationBuilder.DropTable(
                name: "TipoUnidad");
        }
    }
}
