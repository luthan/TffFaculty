using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

namespace WebApplication2.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRole", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "EntityFrameworkLog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Browser = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Exception = table.Column<string>(nullable: true),
                    HostAddress = table.Column<string>(nullable: true),
                    Level = table.Column<string>(nullable: false),
                    Logger = table.Column<string>(nullable: false),
                    Message = table.Column<string>(nullable: false),
                    Thread = table.Column<string>(nullable: false),
                    Url = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityFrameworkLog", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AirfareTrainAllowance = table.Column<string>(nullable: true),
                    ArriveHoursBefore = table.Column<int>(nullable: true),
                    DepartHoursAfter = table.Column<int>(nullable: true),
                    Enduring = table.Column<bool>(nullable: false),
                    EventCode = table.Column<string>(nullable: true),
                    EventEndDate = table.Column<DateTime>(nullable: true),
                    EventName = table.Column<string>(nullable: true),
                    EventStartDate = table.Column<DateTime>(nullable: true),
                    GroundTransfersAllowance = table.Column<string>(nullable: true),
                    HotelAllowance = table.Column<string>(nullable: true),
                    HotelBlockCode = table.Column<string>(nullable: true),
                    Live = table.Column<bool>(nullable: false),
                    VenueAddress1 = table.Column<string>(nullable: true),
                    VenueAddress2 = table.Column<string>(nullable: true),
                    VenueCity = table.Column<string>(nullable: true),
                    VenueName = table.Column<string>(nullable: true),
                    VenueState = table.Column<string>(nullable: true),
                    VenueZip = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "FacultyRole",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacultyRole", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Airline1MemberNumber = table.Column<string>(nullable: true),
                    Airline2MemberNumber = table.Column<string>(nullable: true),
                    Airline2Name = table.Column<string>(nullable: true),
                    Airlinel1Name = table.Column<string>(nullable: true),
                    AssistantEmail = table.Column<string>(nullable: true),
                    AssistantName = table.Column<string>(nullable: true),
                    AssistantPhone = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: true),
                    BusinessPhone = table.Column<string>(nullable: true),
                    CarRentalCompanyMemberNumber = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    Fax = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    Hotel1MemberNumber = table.Column<string>(nullable: true),
                    Hotel1Name = table.Column<string>(nullable: true),
                    Hotel2MemberNumber = table.Column<string>(nullable: true),
                    Hotel2Name = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    MobilePhone = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    OfficialName = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    PreferredAirport = table.Column<string>(nullable: true),
                    PreferredCarRentalCompany = table.Column<string>(nullable: true),
                    PreferredTrainStation = table.Column<string>(nullable: true),
                    Prefix = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    StreetAddress = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    Zip = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRoleClaim<string>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserClaim<string>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityUserClaim<string>_User_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserLogin<string>", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_IdentityUserLogin<string>_User_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserRole<string>", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_IdentityUserRole<string>_IdentityRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IdentityUserRole<string>_User_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "Disclosure",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CommercialInterest = table.Column<bool>(nullable: false),
                    ConsultingInfo = table.Column<string>(nullable: true),
                    ConsultingSelf = table.Column<bool>(nullable: false),
                    ConsultingSpouse = table.Column<bool>(nullable: false),
                    ContractedResearchInfo = table.Column<string>(nullable: true),
                    ContractedResearchSelf = table.Column<bool>(nullable: false),
                    ContractedResearchSpouse = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    DisclosedAllRelevantRelationships = table.Column<bool>(nullable: false),
                    DrugList = table.Column<string>(nullable: true),
                    EditDate = table.Column<DateTime>(nullable: false),
                    IpRightsPatentInfo = table.Column<string>(nullable: true),
                    IpRightsPatentSelf = table.Column<bool>(nullable: false),
                    IpRightsPatentSpouse = table.Column<bool>(nullable: false),
                    IsBackup = table.Column<bool>(nullable: false),
                    OtherDescription = table.Column<string>(nullable: true),
                    OtherInfo = table.Column<string>(nullable: true),
                    OtherSelf = table.Column<bool>(nullable: false),
                    OtherSpouse = table.Column<bool>(nullable: false),
                    OwnershipInfo = table.Column<string>(nullable: true),
                    OwnershipSelf = table.Column<bool>(nullable: false),
                    OwnershipSpouse = table.Column<bool>(nullable: false),
                    RoyaltyInfo = table.Column<string>(nullable: true),
                    RoyaltySelf = table.Column<bool>(nullable: false),
                    RoyaltySpouse = table.Column<bool>(nullable: false),
                    SalaryInfo = table.Column<string>(nullable: true),
                    SalarySelf = table.Column<bool>(nullable: false),
                    SalarySpouse = table.Column<bool>(nullable: false),
                    SignatureConfirm = table.Column<bool>(nullable: false),
                    SignatureDate = table.Column<DateTime>(nullable: false),
                    SignatureText = table.Column<string>(nullable: true),
                    SpeakersBureauInfo = table.Column<string>(nullable: true),
                    SpeakersBureauSelf = table.Column<bool>(nullable: false),
                    SpeakersBureauSpouse = table.Column<bool>(nullable: false),
                    UnapprovedDrugReference = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    WillImpactPresentation = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disclosure", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Disclosure_User_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "EventFaculty",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    EventId = table.Column<int>(nullable: false),
                    ArrivalDate = table.Column<DateTime>(nullable: true),
                    BookHotel = table.Column<bool>(nullable: false),
                    DepartureDate = table.Column<DateTime>(nullable: true),
                    HotelLocationPreference = table.Column<string>(nullable: true),
                    Id = table.Column<int>(nullable: false),
                    Inactive = table.Column<bool>(nullable: false),
                    InviteDate = table.Column<DateTime>(nullable: true),
                    Invited = table.Column<bool>(nullable: false),
                    RoomPreference = table.Column<string>(nullable: true),
                    SpecialAccess = table.Column<bool>(nullable: false),
                    TravelMethod = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventFaculty", x => new { x.UserId, x.EventId });
                    table.ForeignKey(
                        name: "FK_EventFaculty_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventFaculty_User_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "EventFacultyRole",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    EventId = table.Column<int>(nullable: false),
                    FacultyRoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventFacultyRole", x => new { x.UserId, x.EventId, x.FacultyRoleId });
                    table.ForeignKey(
                        name: "FK_EventFacultyRole_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventFacultyRole_FacultyRole_FacultyRoleId",
                        column: x => x.FacultyRoleId,
                        principalTable: "FacultyRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventFacultyRole_User_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName");
            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");
            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("AspNetRoleClaims");
            migrationBuilder.DropTable("AspNetUserClaims");
            migrationBuilder.DropTable("AspNetUserLogins");
            migrationBuilder.DropTable("AspNetUserRoles");
            migrationBuilder.DropTable("Disclosure");
            migrationBuilder.DropTable("EntityFrameworkLog");
            migrationBuilder.DropTable("EventFaculty");
            migrationBuilder.DropTable("EventFacultyRole");
            migrationBuilder.DropTable("AspNetRoles");
            migrationBuilder.DropTable("Event");
            migrationBuilder.DropTable("FacultyRole");
            migrationBuilder.DropTable("AspNetUsers");
        }
    }
}
