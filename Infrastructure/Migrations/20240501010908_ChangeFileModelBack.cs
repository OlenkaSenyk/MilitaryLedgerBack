using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeFileModelBack : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE Files DROP CONSTRAINT [FK_Files_People_PersonId];");

            migrationBuilder.Sql("ALTER TABLE Files ADD ResidencePermitTemp VARBINARY(MAX);");
            migrationBuilder.Sql("UPDATE Files SET ResidencePermitTemp = CONVERT(VARBINARY(MAX), ResidencePermit);");
            migrationBuilder.Sql("ALTER TABLE Files DROP COLUMN ResidencePermit;");
            migrationBuilder.Sql("ALTER TABLE Files ADD ResidencePermit VARBINARY(MAX);");
            migrationBuilder.Sql("UPDATE Files SET ResidencePermit = ResidencePermitTemp;");
            migrationBuilder.Sql("ALTER TABLE Files DROP COLUMN ResidencePermitTemp;");

            migrationBuilder.Sql("ALTER TABLE Files ADD PhotoTemp VARBINARY(MAX);");
            migrationBuilder.Sql("UPDATE Files SET PhotoTemp = CONVERT(VARBINARY(MAX), Photo);");
            migrationBuilder.Sql("ALTER TABLE Files DROP COLUMN Photo;");
            migrationBuilder.Sql("ALTER TABLE Files ADD Photo VARBINARY(MAX);");
            migrationBuilder.Sql("UPDATE Files SET Photo = PhotoTemp;");
            migrationBuilder.Sql("ALTER TABLE Files DROP COLUMN PhotoTemp;");

            migrationBuilder.Sql("ALTER TABLE Files ADD PassportTemp VARBINARY(MAX);");
            migrationBuilder.Sql("UPDATE Files SET PassportTemp = CONVERT(VARBINARY(MAX), Passport);");
            migrationBuilder.Sql("ALTER TABLE Files DROP COLUMN Passport;");
            migrationBuilder.Sql("ALTER TABLE Files ADD Passport VARBINARY(MAX);");
            migrationBuilder.Sql("UPDATE Files SET Passport = PassportTemp;");
            migrationBuilder.Sql("ALTER TABLE Files DROP COLUMN PassportTemp;");

            migrationBuilder.Sql("ALTER TABLE Files ADD MedicalCertificateTemp VARBINARY(MAX);");
            migrationBuilder.Sql("UPDATE Files SET MedicalCertificateTemp = CONVERT(VARBINARY(MAX), MedicalCertificate);");
            migrationBuilder.Sql("ALTER TABLE Files DROP COLUMN MedicalCertificate;");
            migrationBuilder.Sql("ALTER TABLE Files ADD MedicalCertificate VARBINARY(MAX);");
            migrationBuilder.Sql("UPDATE Files SET MedicalCertificate = MedicalCertificateTemp;");
            migrationBuilder.Sql("ALTER TABLE Files DROP COLUMN MedicalCertificateTemp;");

            migrationBuilder.Sql("ALTER TABLE Files ADD IndividualTaxNumberTemp VARBINARY(MAX);");
            migrationBuilder.Sql("UPDATE Files SET IndividualTaxNumberTemp = CONVERT(VARBINARY(MAX), IndividualTaxNumber);");
            migrationBuilder.Sql("ALTER TABLE Files DROP COLUMN IndividualTaxNumber;");
            migrationBuilder.Sql("ALTER TABLE Files ADD IndividualTaxNumber VARBINARY(MAX);");
            migrationBuilder.Sql("UPDATE Files SET IndividualTaxNumber = IndividualTaxNumberTemp;");
            migrationBuilder.Sql("ALTER TABLE Files DROP COLUMN IndividualTaxNumberTemp;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE Files DROP CONSTRAINT [FK_Files_People_PersonId];");

            migrationBuilder.Sql("ALTER TABLE Files ADD ResidencePermitTemp NVARCHAR(MAX);");
            migrationBuilder.Sql("UPDATE Files SET ResidencePermitTemp = CONVERT(NVARCHAR(MAX), ResidencePermit);");
            migrationBuilder.Sql("ALTER TABLE Files DROP COLUMN ResidencePermit;");
            migrationBuilder.Sql("ALTER TABLE Files ADD ResidencePermit NVARCHAR(MAX);");
            migrationBuilder.Sql("UPDATE Files SET ResidencePermit = ResidencePermitTemp;");
            migrationBuilder.Sql("ALTER TABLE Files DROP COLUMN ResidencePermitTemp;");

            migrationBuilder.Sql("ALTER TABLE Files ADD PhotoTemp NVARCHAR(MAX);");
            migrationBuilder.Sql("UPDATE Files SET PhotoTemp = CONVERT(NVARCHAR(MAX), Photo);");
            migrationBuilder.Sql("ALTER TABLE Files DROP COLUMN Photo;");
            migrationBuilder.Sql("ALTER TABLE Files ADD Photo NVARCHAR(MAX);");
            migrationBuilder.Sql("UPDATE Files SET Photo = PhotoTemp;");
            migrationBuilder.Sql("ALTER TABLE Files DROP COLUMN PhotoTemp;");

            migrationBuilder.Sql("ALTER TABLE Files ADD PassportTemp NVARCHAR(MAX);");
            migrationBuilder.Sql("UPDATE Files SET PassportTemp = CONVERT(NVARCHAR(MAX), Passport);");
            migrationBuilder.Sql("ALTER TABLE Files DROP COLUMN Passport;");
            migrationBuilder.Sql("ALTER TABLE Files ADD Passport NVARCHAR(MAX);");
            migrationBuilder.Sql("UPDATE Files SET Passport = PassportTemp;");
            migrationBuilder.Sql("ALTER TABLE Files DROP COLUMN PassportTemp;");

            migrationBuilder.Sql("ALTER TABLE Files ADD MedicalCertificateTemp NVARCHAR(MAX);");
            migrationBuilder.Sql("UPDATE Files SET MedicalCertificateTemp = CONVERT(NVARCHAR(MAX), MedicalCertificate);");
            migrationBuilder.Sql("ALTER TABLE Files DROP COLUMN MedicalCertificate;");
            migrationBuilder.Sql("ALTER TABLE Files ADD MedicalCertificate NVARCHAR(MAX);");
            migrationBuilder.Sql("UPDATE Files SET MedicalCertificate = MedicalCertificateTemp;");
            migrationBuilder.Sql("ALTER TABLE Files DROP COLUMN MedicalCertificateTemp;");

            migrationBuilder.Sql("ALTER TABLE Files ADD IndividualTaxNumberTemp NVARCHAR(MAX);");
            migrationBuilder.Sql("UPDATE Files SET IndividualTaxNumberTemp = CONVERT(NVARCHAR(MAX), IndividualTaxNumber);");
            migrationBuilder.Sql("ALTER TABLE Files DROP COLUMN IndividualTaxNumber;");
            migrationBuilder.Sql("ALTER TABLE Files ADD IndividualTaxNumber NVARCHAR(MAX);");
            migrationBuilder.Sql("UPDATE Files SET IndividualTaxNumber = IndividualTaxNumberTemp;");
            migrationBuilder.Sql("ALTER TABLE Files DROP COLUMN IndividualTaxNumberTemp;");
        }

    }
}
