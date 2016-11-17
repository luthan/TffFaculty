using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using WebApplication2.Models;

namespace WebApplication2.Migrations
{
    [DbContext(typeof(TffFacultyContext))]
    [Migration("20161107181528_disclosure relationships2")]
    partial class disclosurerelationships2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasAnnotation("Relational:Name", "RoleNameIndex");

                    b.HasAnnotation("Relational:TableName", "AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasAnnotation("Relational:TableName", "AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasAnnotation("Relational:TableName", "AspNetUserRoles");
                });

            modelBuilder.Entity("WebApplication2.Models.Disclosure", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("CommercialInterest");

                    b.Property<string>("ConsultingInfo");

                    b.Property<bool>("ConsultingSelf");

                    b.Property<bool>("ConsultingSpouse");

                    b.Property<string>("ContractedResearchInfo");

                    b.Property<bool>("ContractedResearchSelf");

                    b.Property<bool>("ContractedResearchSpouse");

                    b.Property<DateTime>("CreateDate");

                    b.Property<bool>("DisclosedAllRelevantRelationships");

                    b.Property<string>("DrugList");

                    b.Property<DateTime>("EditDate");

                    b.Property<string>("IpRightsPatentInfo");

                    b.Property<bool>("IpRightsPatentSelf");

                    b.Property<bool>("IpRightsPatentSpouse");

                    b.Property<bool>("IsBackup");

                    b.Property<string>("OtherDescription");

                    b.Property<string>("OtherInfo");

                    b.Property<bool>("OtherSelf");

                    b.Property<bool>("OtherSpouse");

                    b.Property<string>("OwnershipInfo");

                    b.Property<bool>("OwnershipSelf");

                    b.Property<bool>("OwnershipSpouse");

                    b.Property<string>("RoyaltyInfo");

                    b.Property<bool>("RoyaltySelf");

                    b.Property<bool>("RoyaltySpouse");

                    b.Property<string>("SalaryInfo");

                    b.Property<bool>("SalarySelf");

                    b.Property<bool>("SalarySpouse");

                    b.Property<bool>("SignatureConfirm");

                    b.Property<DateTime>("SignatureDate");

                    b.Property<string>("SignatureText");

                    b.Property<string>("SpeakersBureauInfo");

                    b.Property<bool>("SpeakersBureauSelf");

                    b.Property<bool>("SpeakersBureauSpouse");

                    b.Property<string>("UnapprovedDrugReference");

                    b.Property<string>("UserId");

                    b.Property<bool>("WillImpactPresentation");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("WebApplication2.Models.EntityFrameworkLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Browser")
                        .HasAnnotation("MaxLength", 200);

                    b.Property<DateTime>("Date");

                    b.Property<string>("Exception")
                        .HasAnnotation("MaxLength", 2000);

                    b.Property<string>("HostAddress")
                        .HasAnnotation("MaxLength", 20);

                    b.Property<string>("Level")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("Logger")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 255);

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 4000);

                    b.Property<string>("Thread")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 255);

                    b.Property<string>("Url")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("Username")
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");
                });

            modelBuilder.Entity("WebApplication2.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AirfareTrainAllowance");

                    b.Property<int?>("ArriveHoursBefore");

                    b.Property<int?>("DepartHoursAfter");

                    b.Property<string>("EventCode");

                    b.Property<DateTime?>("EventEndDate");

                    b.Property<string>("EventName");

                    b.Property<DateTime?>("EventStartDate");

                    b.Property<string>("GroundTransfersAllowance");

                    b.Property<string>("HotelAllowance");

                    b.Property<string>("HotelBlockCode");

                    b.Property<string>("ManagerId");

                    b.Property<string>("Type");

                    b.Property<string>("VenueAddress1");

                    b.Property<string>("VenueAddress2");

                    b.Property<string>("VenueCity");

                    b.Property<string>("VenueName");

                    b.Property<string>("VenueState");

                    b.Property<string>("VenueZip");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("WebApplication2.Models.EventFaculty", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<int>("EventId");

                    b.Property<bool>("BookHotel");

                    b.Property<DateTime?>("CheckIn");

                    b.Property<DateTime?>("CheckOut");

                    b.Property<bool>("Completed");

                    b.Property<DateTime?>("DepartureDate");

                    b.Property<string>("DepartureLocation");

                    b.Property<string>("DepartureTime");

                    b.Property<bool>("FilledOut");

                    b.Property<string>("Id");

                    b.Property<bool>("Inactive");

                    b.Property<DateTime?>("InviteDate");

                    b.Property<bool>("Invited");

                    b.Property<DateTime?>("ReturnDate");

                    b.Property<string>("ReturnLocation");

                    b.Property<string>("ReturnTime");

                    b.Property<string>("TravelMethod");

                    b.HasKey("UserId", "EventId");
                });

            modelBuilder.Entity("WebApplication2.Models.EventFacultyRole", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<int>("EventId");

                    b.Property<int>("FacultyRoleId");

                    b.Property<string>("EventFacultyId");

                    b.HasKey("UserId", "EventId", "FacultyRoleId");
                });

            modelBuilder.Entity("WebApplication2.Models.FacultyRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("WebApplication2.Models.Specialty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("SpecialtyTitle");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("WebApplication2.Models.User", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("Airline1MemberNumber");

                    b.Property<string>("Airline2MemberNumber");

                    b.Property<string>("Airline2Name");

                    b.Property<string>("AirlineSeatingPreference");

                    b.Property<string>("Airlinel1Name");

                    b.Property<string>("AssistantEmail");

                    b.Property<string>("AssistantName");

                    b.Property<string>("AssistantPhone");

                    b.Property<DateTime?>("BirthDate");

                    b.Property<string>("BusinessPhone");

                    b.Property<string>("City");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Country");

                    b.Property<string>("Degree");

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("Fax");

                    b.Property<string>("FirstName");

                    b.Property<string>("Hotel1MemberNumber");

                    b.Property<string>("Hotel1Name");

                    b.Property<string>("Hotel2MemberNumber");

                    b.Property<string>("Hotel2Name");

                    b.Property<string>("HotelRoomPreference");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("MobilePhone");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("OfficialName");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("Prefix");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("Specialty");

                    b.Property<string>("State");

                    b.Property<string>("StreetAddress");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("Zip");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasAnnotation("Relational:Name", "EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .HasAnnotation("Relational:Name", "UserNameIndex");

                    b.HasAnnotation("Relational:TableName", "AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNet.Identity.EntityFramework.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("WebApplication2.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("WebApplication2.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNet.Identity.EntityFramework.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.HasOne("WebApplication2.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("WebApplication2.Models.Disclosure", b =>
                {
                    b.HasOne("WebApplication2.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("WebApplication2.Models.Event", b =>
                {
                    b.HasOne("WebApplication2.Models.User")
                        .WithMany()
                        .HasForeignKey("ManagerId");
                });

            modelBuilder.Entity("WebApplication2.Models.EventFaculty", b =>
                {
                    b.HasOne("WebApplication2.Models.Event")
                        .WithMany()
                        .HasForeignKey("EventId");

                    b.HasOne("WebApplication2.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("WebApplication2.Models.EventFacultyRole", b =>
                {
                    b.HasOne("WebApplication2.Models.Event")
                        .WithMany()
                        .HasForeignKey("EventId");

                    b.HasOne("WebApplication2.Models.FacultyRole")
                        .WithMany()
                        .HasForeignKey("FacultyRoleId");

                    b.HasOne("WebApplication2.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.HasOne("WebApplication2.Models.EventFaculty")
                        .WithMany()
                        .HasForeignKey("UserId", "EventId");
                });
        }
    }
}
