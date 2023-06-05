using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROG7311.FarmProducts.ST10153536.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create the admin user
            migrationBuilder.Sql("INSERT INTO AspNetUsers (Id, FirstName, LastName, Username, Email, NormalizedUserName, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount) " +
                                 "VALUES ('1', 'Admin', '', 'admin', 'admin@example.com', 'ADMIN', 'ADMIN@EXAMPLE.COM', 1, 'Admin123!', 'Y4XEW3C7LP3LVE5SFMNFEJEBEMLC7BMV', '3a04c0f4-dc94-4e0b-a6f5-6a308b04e1f6', 0, 0, 1, 0)");

            // Set the password hash for the admin user
            migrationBuilder.Sql("UPDATE AspNetUsers SET PasswordHash = 'Admin123!' WHERE Id = '1'");

            // Create the farmer user
            migrationBuilder.Sql("INSERT INTO AspNetUsers (Id, FirstName, LastName, Username, Email, NormalizedUserName, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount) " +
                                 "VALUES ('2', 'John', 'Doe', 'farmer', 'john.doe@example.com', 'FARMER', 'JOHN.DOE@EXAMPLE.COM', 1, 'Farmer123!', 'Y4XEW3C7LP3LVE5SFMNFEJEBEMLC7BMV', '3a04c0f4-dc94-4e0b-a6f5-6a308b04e1f6', 0, 0, 1, 0)");

            // Set the password hash for the farmer user
            migrationBuilder.Sql("UPDATE AspNetUsers SET PasswordHash = 'Farmer123!' WHERE Id = '2'");

            // Create the employee user
            migrationBuilder.Sql("INSERT INTO AspNetUsers (Id, FirstName, LastName, Username, Email, NormalizedUserName, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount) " +
                                 "VALUES ('3', 'Jane', 'Smith', 'employee', 'jane.smith@example.com', 'EMPLOYEE', 'JANE.SMITH@EXAMPLE.COM', 1, 'Employee123!', 'Y4XEW3C7LP3LVE5SFMNFEJEBEMLC7BMV', '3a04c0f4-dc94-4e0b-a6f5-6a308b04e1f6', 0, 0, 1, 0)");

            // Set the password hash for the employee user
            migrationBuilder.Sql("UPDATE AspNetUsers SET PasswordHash = 'Employee123!' WHERE Id = '3'");

            // Add the role names
            migrationBuilder.Sql("INSERT INTO AspNetRoles (Id, Name, NormalizedName) VALUES ('1', 'Admin', 'ADMIN')");
            migrationBuilder.Sql("INSERT INTO AspNetRoles (Id, Name, NormalizedName) VALUES ('2', 'Farmer', 'FARMER')");
            migrationBuilder.Sql("INSERT INTO AspNetRoles (Id, Name, NormalizedName) VALUES ('3', 'Employee', 'EMPLOYEE')");

            // Assign roles to users
            migrationBuilder.Sql("INSERT INTO AspNetUserRoles (UserId, RoleId) SELECT '1', '1'"); // Admin
            migrationBuilder.Sql("INSERT INTO AspNetUserRoles (UserId, RoleId) SELECT '2', '2'"); // Farmer
            migrationBuilder.Sql("INSERT INTO AspNetUserRoles (UserId, RoleId) SELECT '3', '3'"); // Employee

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
            {
                b.Property<string>("Id")
                    .HasColumnType("nvarchar(450)");

                b.Property<string>("ConcurrencyStamp")
                    .IsConcurrencyToken()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Name")
                    .HasMaxLength(256)
                    .HasColumnType("nvarchar(256)");

                b.Property<string>("NormalizedName")
                    .HasMaxLength(256)
                    .HasColumnType("nvarchar(256)");

                b.HasKey("Id");

                b.HasIndex("NormalizedName")
                    .IsUnique()
                    .HasDatabaseName("RoleNameIndex")
                    .HasFilter("[NormalizedName] IS NOT NULL");

                b.ToTable("AspNetRoles", (string)null);
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<string>("ClaimType")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("ClaimValue")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("RoleId")
                    .IsRequired()
                    .HasColumnType("nvarchar(450)");

                b.HasKey("Id");

                b.HasIndex("RoleId");

                b.ToTable("AspNetRoleClaims", (string)null);
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<string>("ClaimType")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("ClaimValue")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("UserId")
                    .IsRequired()
                    .HasColumnType("nvarchar(450)");

                b.HasKey("Id");

                b.HasIndex("UserId");

                b.ToTable("AspNetUserClaims", (string)null);
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
            {
                b.Property<string>("LoginProvider")
                    .HasMaxLength(128)
                    .HasColumnType("nvarchar(128)");

                b.Property<string>("ProviderKey")
                    .HasMaxLength(128)
                    .HasColumnType("nvarchar(128)");

                b.Property<string>("ProviderDisplayName")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("UserId")
                    .IsRequired()
                    .HasColumnType("nvarchar(450)");

                b.HasKey("LoginProvider", "ProviderKey");

                b.HasIndex("UserId");

                b.ToTable("AspNetUserLogins", (string)null);
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<string>", b =>
            {
                b.Property<string>("UserId")
                    .HasColumnType("nvarchar(450)");

                b.Property<string>("RoleId")
                    .HasColumnType("nvarchar(450)");

                b.Property<string>("ApplicationRole")
                    .HasColumnType("nvarchar(450)");

                b.HasKey("UserId", "RoleId");

                b.HasIndex("RoleId");

                b.HasIndex("ApplicationRole");

                b.ToTable("AspNetUserRoles", (string)null);
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
            {
                b.Property<string>("UserId")
                    .HasColumnType("nvarchar(450)");

                b.Property<string>("LoginProvider")
                    .HasMaxLength(128)
                    .HasColumnType("nvarchar(128)");

                b.Property<string>("Name")
                    .HasMaxLength(128)
                    .HasColumnType("nvarchar(128)");

                b.Property<string>("Value")
                    .HasColumnType("nvarchar(max)");

                b.HasKey("UserId", "LoginProvider", "Name");

                b.ToTable("AspNetUserTokens", (string)null);
            });

            modelBuilder.Entity("PROG7311.FarmProducts.ST10153536.Models.Domain.ApplicationUser", b =>
            {
                b.Property<string>("Id")
                    .HasColumnType("nvarchar(450)");

                b.Property<int>("AccessFailedCount")
                    .HasColumnType("int");

                b.Property<string>("ConcurrencyStamp")
                    .IsConcurrencyToken()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Email")
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnType("nvarchar(256)");

                b.Property<bool>("EmailConfirmed")
                    .HasColumnType("bit");

                b.Property<string>("FirstName")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("LastName")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<bool>("LockoutEnabled")
                    .HasColumnType("bit");

                b.Property<DateTimeOffset?>("LockoutEnd")
                    .HasColumnType("datetimeoffset");

                b.Property<string>("NormalizedEmail")
                    .HasMaxLength(256)
                    .HasColumnType("nvarchar(256)");

                b.Property<string>("NormalizedUserName")
                    .HasMaxLength(256)
                    .HasColumnType("nvarchar(256)");

                b.Property<string>("PasswordHash")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("PhoneNumber")
                    .HasColumnType("nvarchar(max)");

                b.Property<bool>("PhoneNumberConfirmed")
                    .HasColumnType("bit");

                b.Property<string>("SecurityStamp")
                    .HasColumnType("nvarchar(max)");

                b.Property<bool>("TwoFactorEnabled")
                    .HasColumnType("bit");

                b.Property<string>("UserName")
                    .HasMaxLength(256)
                    .HasColumnType("nvarchar(256)");

                b.Property<string>("Username")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.HasKey("Id");

                b.HasIndex("NormalizedEmail")
                    .HasDatabaseName("EmailIndex");

                b.HasIndex("NormalizedUserName")
                    .IsUnique()
                    .HasDatabaseName("UserNameIndex")
                    .HasFilter("[NormalizedUserName] IS NOT NULL");

                b.ToTable("AspNetUsers", (string)null);
            });

            modelBuilder.Entity("PROG7311.FarmProducts.ST10153536.Models.Product", b =>
            {
                b.Property<int?>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                b.Property<string>("Name")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Type")
                    .HasColumnType("nvarchar(max)");

                b.HasKey("Id");

                b.ToTable("Products");
            });

            modelBuilder.Entity("PROG7311.FarmProducts.ST10153536.Models.ProductFarmer", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<string>("ApplicationUserId")
                    .HasColumnType("nvarchar(450)");

                b.Property<string>("FirstName")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Name")
                    .HasColumnType("nvarchar(max)");

                b.Property<int>("ProductId")
                    .HasColumnType("int");

                b.Property<DateTime>("ReceivedDate")
                    .HasColumnType("datetime2");

                b.Property<string>("Type")
                    .HasColumnType("nvarchar(max)");

                b.HasKey("Id");

                b.HasIndex("ApplicationUserId");

                b.HasIndex("ProductId");

                b.ToTable("ProductFarmers");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
            {
                b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                    .WithMany()
                    .HasForeignKey("RoleId")
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
            {
                b.HasOne("PROG7311.FarmProducts.ST10153536.Models.Domain.ApplicationUser", null)
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
            {
                b.HasOne("PROG7311.FarmProducts.ST10153536.Models.Domain.ApplicationUser", null)
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<string>", b =>
            {
                b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                    .WithMany()
                    .HasForeignKey("RoleId")
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                b.HasOne("PROG7311.FarmProducts.ST10153536.Models.Domain.ApplicationUser", null)
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                b.HasOne("PROG7311.FarmProducts.ST10153536.Models.Domain.ApplicationUser", null)
                    .WithMany("Roles")
                    .HasForeignKey("ApplicationRole")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
            {
                b.HasOne("PROG7311.FarmProducts.ST10153536.Models.Domain.ApplicationUser", null)
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
            });

            modelBuilder.Entity("PROG7311.FarmProducts.ST10153536.Models.ProductFarmer", b =>
            {
                b.HasOne("PROG7311.FarmProducts.ST10153536.Models.Domain.ApplicationUser", "ApplicationUser")
                    .WithMany("ProductFarmers")
                    .HasForeignKey("ApplicationUserId")
                    .OnDelete(DeleteBehavior.Restrict);

                b.HasOne("PROG7311.FarmProducts.ST10153536.Models.Product", "Product")
                    .WithMany("ProductFarmers")
                    .HasForeignKey("ProductId")
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                b.Navigation("ApplicationUser");

                b.Navigation("Product");
            });

            modelBuilder.Entity("PROG7311.FarmProducts.ST10153536.Models.Domain.ApplicationUser", b =>
            {
                b.Navigation("ProductFarmers");

                b.Navigation("Roles");
            });

            modelBuilder.Entity("PROG7311.FarmProducts.ST10153536.Models.Product", b =>
            {
                b.Navigation("ProductFarmers");
            });
#pragma warning restore 612, 618
        }
    }
}

