using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionareFederatieTriatlon.Migrations
{
    /// <inheritdoc />
    public partial class AdaugaTabele : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Chaturi",
                columns: table => new
                {
                    codChat = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dataInfiintarii = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chaturi", x => x.codChat);
                });

            migrationBuilder.CreateTable(
                name: "Cluburi",
                columns: table => new
                {
                    codClub = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cluburi", x => x.codClub);
                });

            migrationBuilder.CreateTable(
                name: "Locatii",
                columns: table => new
                {
                    codLocatie = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tara = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    oras = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    strada = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    numarStrada = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locatii", x => x.codLocatie);
                });

            migrationBuilder.CreateTable(
                name: "Probe",
                columns: table => new
                {
                    codProba = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    numeProba = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    timpLimita = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Probe", x => x.codProba);
                });

            migrationBuilder.CreateTable(
                name: "Tipuri",
                columns: table => new
                {
                    codTip = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tipCompetitie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    numarMinimParticipanti = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipuri", x => x.codTip);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    nume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    prenume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    abonareStiri = table.Column<bool>(type: "bit", nullable: false),
                    urlPozaProfil = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClubcodClub = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gradPregatire = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    numarLegitimatie = table.Column<int>(type: "int", nullable: true),
                    dataNastere = table.Column<DateTime>(type: "datetime2", nullable: true),
                    gen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Cluburi_ClubcodClub",
                        column: x => x.ClubcodClub,
                        principalTable: "Cluburi",
                        principalColumn: "codClub",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Competitii",
                columns: table => new
                {
                    codCompetitie = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    numeCompetitie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    taxaParticipare = table.Column<float>(type: "real", nullable: false),
                    dataStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dataFinal = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipcodTip = table.Column<int>(type: "int", nullable: false),
                    LocatiecodLocatie = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competitii", x => x.codCompetitie);
                    table.ForeignKey(
                        name: "FK_Competitii_Locatii_LocatiecodLocatie",
                        column: x => x.LocatiecodLocatie,
                        principalTable: "Locatii",
                        principalColumn: "codLocatie",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Competitii_Tipuri_TipcodTip",
                        column: x => x.TipcodTip,
                        principalTable: "Tipuri",
                        principalColumn: "codTip",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "Conversatii",
                columns: table => new
                {
                    codConversatie = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    codChat = table.Column<int>(type: "int", nullable: false),
                    codUtilizator2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mesajConversatie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dataPrimireMesaj = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dataLivrareMesaj = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversatii", x => new { x.codConversatie, x.UserId, x.codChat });
                    table.ForeignKey(
                        name: "FK_Conversatii_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Conversatii_Chaturi_codChat",
                        column: x => x.codChat,
                        principalTable: "Chaturi",
                        principalColumn: "codChat",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fomulare",
                columns: table => new
                {
                    codFormular = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pozaProfil = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    avizMedical = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    buletinCertificatNastere = table.Column<string>(name: "buletin_CertificatNastere", type: "nvarchar(max)", nullable: false),
                    SportivId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fomulare", x => x.codFormular);
                    table.ForeignKey(
                        name: "FK_Fomulare_AspNetUsers_SportivId",
                        column: x => x.SportivId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notificari",
                columns: table => new
                {
                    codNotificare = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    mesaj = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UtilizatorId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificari", x => x.codNotificare);
                    table.ForeignKey(
                        name: "FK_Notificari_AspNetUsers_UtilizatorId",
                        column: x => x.UtilizatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Postari",
                columns: table => new
                {
                    codPostare = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    urlPoza = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    numarReactiiFericire = table.Column<int>(type: "int", nullable: false),
                    numarReactiiTristete = table.Column<int>(type: "int", nullable: false),
                    UtilizatorId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Postari", x => x.codPostare);
                    table.ForeignKey(
                        name: "FK_Postari_AspNetUsers_UtilizatorId",
                        column: x => x.UtilizatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Istorice",
                columns: table => new
                {
                    SportivId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    codProba = table.Column<int>(type: "int", nullable: false),
                    codCompetitie = table.Column<int>(type: "int", nullable: false),
                    categorie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    loc = table.Column<int>(type: "int", nullable: false),
                    timp = table.Column<int>(type: "int", nullable: false),
                    puncte = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Istorice", x => new { x.SportivId, x.codProba, x.codCompetitie });
                    table.ForeignKey(
                        name: "FK_Istorice_AspNetUsers_SportivId",
                        column: x => x.SportivId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Istorice_Competitii_codCompetitie",
                        column: x => x.codCompetitie,
                        principalTable: "Competitii",
                        principalColumn: "codCompetitie",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Istorice_Probe_codProba",
                        column: x => x.codProba,
                        principalTable: "Probe",
                        principalColumn: "codProba",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recenzii",
                columns: table => new
                {
                    codRecenzie = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    numarStele = table.Column<int>(type: "int", nullable: false),
                    text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SportivId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CompetitiecodCompetitie = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recenzii", x => x.codRecenzie);
                    table.ForeignKey(
                        name: "FK_Recenzii_AspNetUsers_SportivId",
                        column: x => x.SportivId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recenzii_Competitii_CompetitiecodCompetitie",
                        column: x => x.CompetitiecodCompetitie,
                        principalTable: "Competitii",
                        principalColumn: "codCompetitie",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Videoclipuri",
                columns: table => new
                {
                    codVideo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    urlVideo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tipExtensie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompetitiecodCompetitie = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videoclipuri", x => x.codVideo);
                    table.ForeignKey(
                        name: "FK_Videoclipuri_Competitii_CompetitiecodCompetitie",
                        column: x => x.CompetitiecodCompetitie,
                        principalTable: "Competitii",
                        principalColumn: "codCompetitie",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comentarii",
                columns: table => new
                {
                    codPostare = table.Column<int>(type: "int", nullable: false),
                    codComentariu = table.Column<int>(type: "int", nullable: false),
                    mesajComentariu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    codUtilizatorComentariu = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentarii", x => new { x.codPostare, x.codComentariu });
                    table.ForeignKey(
                        name: "FK_Comentarii_Postari_codPostare",
                        column: x => x.codPostare,
                        principalTable: "Postari",
                        principalColumn: "codPostare",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

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
                name: "IX_AspNetUsers_ClubcodClub",
                table: "AspNetUsers",
                column: "ClubcodClub");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Competitii_LocatiecodLocatie",
                table: "Competitii",
                column: "LocatiecodLocatie");

            migrationBuilder.CreateIndex(
                name: "IX_Competitii_TipcodTip",
                table: "Competitii",
                column: "TipcodTip");

            migrationBuilder.CreateIndex(
                name: "IX_Conversatii_codChat",
                table: "Conversatii",
                column: "codChat");

            migrationBuilder.CreateIndex(
                name: "IX_Conversatii_UserId",
                table: "Conversatii",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Fomulare_SportivId",
                table: "Fomulare",
                column: "SportivId");

            migrationBuilder.CreateIndex(
                name: "IX_Istorice_codCompetitie",
                table: "Istorice",
                column: "codCompetitie");

            migrationBuilder.CreateIndex(
                name: "IX_Istorice_codProba",
                table: "Istorice",
                column: "codProba");

            migrationBuilder.CreateIndex(
                name: "IX_Notificari_UtilizatorId",
                table: "Notificari",
                column: "UtilizatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Postari_UtilizatorId",
                table: "Postari",
                column: "UtilizatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Recenzii_CompetitiecodCompetitie",
                table: "Recenzii",
                column: "CompetitiecodCompetitie");

            migrationBuilder.CreateIndex(
                name: "IX_Recenzii_SportivId",
                table: "Recenzii",
                column: "SportivId");

            migrationBuilder.CreateIndex(
                name: "IX_Videoclipuri_CompetitiecodCompetitie",
                table: "Videoclipuri",
                column: "CompetitiecodCompetitie",
                unique: true);
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
                name: "Comentarii");

            migrationBuilder.DropTable(
                name: "Conversatii");

            migrationBuilder.DropTable(
                name: "Fomulare");

            migrationBuilder.DropTable(
                name: "Istorice");

            migrationBuilder.DropTable(
                name: "Notificari");

            migrationBuilder.DropTable(
                name: "Recenzii");

            migrationBuilder.DropTable(
                name: "Videoclipuri");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Postari");

            migrationBuilder.DropTable(
                name: "Chaturi");

            migrationBuilder.DropTable(
                name: "Probe");

            migrationBuilder.DropTable(
                name: "Competitii");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Locatii");

            migrationBuilder.DropTable(
                name: "Tipuri");

            migrationBuilder.DropTable(
                name: "Cluburi");
        }
    }
}
