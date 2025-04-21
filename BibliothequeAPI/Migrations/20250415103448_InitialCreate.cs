using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BibliothequeAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Livres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Auteur = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Categorie = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Annee = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    NombreExemplaires = table.Column<int>(type: "int", nullable: false),
                    ExemplairesDisponibles = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Utilisateurs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    MotDePasse = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Adresse = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DateInscription = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstActif = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilisateurs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Emprunts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LivreId = table.Column<int>(type: "int", nullable: false),
                    UtilisateurId = table.Column<int>(type: "int", nullable: false),
                    DateEmprunt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateRetourPrevue = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateRetourEffective = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EstRendu = table.Column<bool>(type: "bit", nullable: false),
                    Commentaire = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emprunts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Emprunts_Livres_LivreId",
                        column: x => x.LivreId,
                        principalTable: "Livres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Emprunts_Utilisateurs_UtilisateurId",
                        column: x => x.UtilisateurId,
                        principalTable: "Utilisateurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Livres",
                columns: new[] { "Id", "Annee", "Auteur", "Categorie", "Description", "ExemplairesDisponibles", "ISBN", "NombreExemplaires", "Titre" },
                values: new object[,]
                {
                    { 1, 1862, "Victor Hugo", "Roman", "Un chef-d'œuvre de la littérature française.", 5, "9782253096344", 5, "Les Misérables" },
                    { 2, 1943, "Antoine de Saint-Exupéry", "Conte", "Un conte poétique et philosophique.", 3, "9782070612758", 3, "Le Petit Prince" }
                });

            migrationBuilder.InsertData(
                table: "Utilisateurs",
                columns: new[] { "Id", "Adresse", "DateInscription", "Email", "EstActif", "MotDePasse", "Nom", "Prenom", "Telephone" },
                values: new object[] { 1, "123 Rue Exemple", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "bessem@gmail.com", true, "motdepasse123", "bessem", "ben mahmoud", "0123456789" });

            migrationBuilder.CreateIndex(
                name: "IX_Emprunts_LivreId",
                table: "Emprunts",
                column: "LivreId");

            migrationBuilder.CreateIndex(
                name: "IX_Emprunts_UtilisateurId",
                table: "Emprunts",
                column: "UtilisateurId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Emprunts");

            migrationBuilder.DropTable(
                name: "Livres");

            migrationBuilder.DropTable(
                name: "Utilisateurs");
        }
    }
}
