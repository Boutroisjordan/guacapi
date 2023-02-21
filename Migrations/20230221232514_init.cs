using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GuacAPI.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "alcohol_type",
                columns: table => new
                {
                    AlcoholTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    label = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_alcohol_type", x => x.AlcoholTypeId);
                });

            migrationBuilder.CreateTable(
                name: "appellation",
                columns: table => new
                {
                    AppellationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_appellation", x => x.AppellationId);
                });

            migrationBuilder.CreateTable(
                name: "Domain",
                columns: table => new
                {
                    DomainId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Domain", x => x.DomainId);
                });

            migrationBuilder.CreateTable(
                name: "Furnisher",
                columns: table => new
                {
                    FurnisherId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Siret = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Furnisher", x => x.FurnisherId);
                });

            migrationBuilder.CreateTable(
                name: "Offer",
                columns: table => new
                {
                    OfferId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deadline = table.Column<DateTime>(type: "date", nullable: true),
                    isB2B = table.Column<bool>(type: "bit", nullable: false),
                    isDraft = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offer", x => x.OfferId);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatus",
                columns: table => new
                {
                    OrderStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderStatusName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatus", x => x.OrderStatusId);
                });

            migrationBuilder.CreateTable(
                name: "Region",
                columns: table => new
                {
                    RegionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.RegionID);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceFurnisher",
                columns: table => new
                {
                    InvoiceFurnisherId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FurnisherId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceFurnisher", x => x.InvoiceFurnisherId);
                    table.ForeignKey(
                        name: "FK_InvoiceFurnisher_Furnisher_FurnisherId",
                        column: x => x.FurnisherId,
                        principalTable: "Furnisher",
                        principalColumn: "FurnisherId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    RestockOption = table.Column<bool>(type: "bit", nullable: false),
                    Millesime = table.Column<int>(type: "int", nullable: false),
                    AlcoholDegree = table.Column<float>(type: "real", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FurnisherId = table.Column<int>(type: "int", nullable: false),
                    DomainId = table.Column<int>(type: "int", nullable: false),
                    RegionId = table.Column<int>(type: "int", nullable: false),
                    AlcoholTypeId = table.Column<int>(type: "int", nullable: false),
                    AppellationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Product_Domain_DomainId",
                        column: x => x.DomainId,
                        principalTable: "Domain",
                        principalColumn: "DomainId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_Furnisher_FurnisherId",
                        column: x => x.FurnisherId,
                        principalTable: "Furnisher",
                        principalColumn: "FurnisherId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_Region_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Region",
                        principalColumn: "RegionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_alcohol_type_AlcoholTypeId",
                        column: x => x.AlcoholTypeId,
                        principalTable: "alcohol_type",
                        principalColumn: "AlcoholTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_appellation_AppellationId",
                        column: x => x.AppellationId,
                        principalTable: "appellation",
                        principalColumn: "AppellationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VerifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InvoiceFurnisherProduct",
                columns: table => new
                {
                    InvoiceFurnisherId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    QuantityProduct = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceFurnisherProduct", x => new { x.InvoiceFurnisherId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_InvoiceFurnisherProduct_InvoiceFurnisher_InvoiceFurnisherId",
                        column: x => x.InvoiceFurnisherId,
                        principalTable: "InvoiceFurnisher",
                        principalColumn: "InvoiceFurnisherId");
                    table.ForeignKey(
                        name: "FK_InvoiceFurnisherProduct_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId");
                });

            migrationBuilder.CreateTable(
                name: "ProductOffer",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    OfferId = table.Column<int>(type: "int", nullable: false),
                    QuantityProduct = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOffer", x => new { x.ProductId, x.OfferId });
                    table.ForeignKey(
                        name: "FK_ProductOffer_Offer_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offer",
                        principalColumn: "OfferId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductOffer_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    CommentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rate = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreviousCommentId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    OfferId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_Comment_Offer_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offer",
                        principalColumn: "OfferId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comment_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false),
                    orderedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderStatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Order_OrderStatus_OrderStatusId",
                        column: x => x.OrderStatusId,
                        principalTable: "OrderStatus",
                        principalColumn: "OrderStatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TokenExpires = table.Column<DateTime>(type: "datetime2", nullable: false),
                    newToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    newTokenExpires = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderOffers",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    OfferId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderOffers", x => new { x.OrderId, x.OfferId });
                    table.ForeignKey(
                        name: "FK_OrderOffers_Offer_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offer",
                        principalColumn: "OfferId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderOffers_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Domain",
                columns: new[] { "DomainId", "Name" },
                values: new object[] { 1, "Domaine 1" });

            migrationBuilder.InsertData(
                table: "Furnisher",
                columns: new[] { "FurnisherId", "City", "Name", "PostalCode", "Siret", "Street" },
                values: new object[,]
                {
                    { 1, "West Nehaborough", "Gerhold Group", "37857-3192", "777882687108", "Shayne Ridges" },
                    { 2, "White Plains", "Bahringer, Hoeger and Schmidt", "79893", "668635164541", "Cruickshank Drive" },
                    { 3, "Hansenstad", "Runolfsdottir, Pollich and Leuschke", "69414", "608519234115", "Santa Ford" },
                    { 4, "Kissimmee", "Considine, Leuschke and Veum", "35694-4531", "898202718476", "O'Conner Flat" },
                    { 5, "West Sidney", "Littel Group", "00475-4444", "195898815118", "Rippin Drive" },
                    { 6, "Charlotte", "Quigley - Pfeffer", "40802-5108", "732774063940", "Hassan Port" },
                    { 7, "New Ezequielbury", "Jones - Crona", "79842-7277", "112436413622", "Cormier Ford" },
                    { 8, "Las Vegas", "Emmerich, Davis and McKenzie", "21616", "392543474289", "Amina Fall" },
                    { 9, "Jacobsshire", "Corwin, Considine and Hane", "89039", "364959358625", "Gerry Estate" },
                    { 10, "West Evert", "Volkman - Frami", "17739", "641177102433", "Kiehn Pines" },
                    { 11, "Stefaniebury", "Hermiston, Kutch and Vandervort", "31780", "933450980031", "Kuhlman Mill" },
                    { 12, "Jerdeworth", "Corkery and Sons", "42606", "489556724926", "Hoeger Land" },
                    { 13, "Kulasbury", "Kuhn - Heaney", "55898-4844", "588567648610", "Brandy Lake" },
                    { 14, "Cristianstead", "Boyer, Zieme and Boyer", "10873-5688", "962400736745", "Donnell Ramp" },
                    { 15, "Cummerataport", "Emmerich, Roob and Bailey", "39709-1993", "546796429516", "Leuschke Highway" },
                    { 16, "Thompsonville", "Lynch LLC", "61366", "871958088892", "Laurianne Fork" },
                    { 17, "Jaquanmouth", "Schneider LLC", "64590", "120265792694", "Cristal Viaduct" },
                    { 18, "West Carmine", "Daniel, Lockman and Yundt", "97741-2729", "994578250139", "Pacocha Row" },
                    { 19, "Lake Edgarbury", "Swaniawski, Hagenes and Sauer", "92158", "350262251499", "Josh Tunnel" },
                    { 20, "West Aldaworth", "Lehner, Reichel and Frami", "12033", "784089780004", "Lottie Unions" },
                    { 21, "Dearborn Heights", "Mills - Haley", "15908-0523", "747049226451", "Jalen Extensions" },
                    { 22, "Beckerboro", "Lynch and Sons", "06199-2690", "763771335612", "O'Keefe Lodge" },
                    { 23, "Lake Opheliaside", "Christiansen - Zieme", "63653-6867", "703487397458", "Filiberto Port" },
                    { 24, "New Marcia", "Grant - Ratke", "67696-0516", "957838491555", "Kitty Views" },
                    { 25, "East Derontown", "Mann, Funk and Jast", "10796", "877881137707", "Mann Ports" },
                    { 26, "West Jolieton", "Hegmann Group", "96021-4286", "263796604728", "Bogisich Locks" },
                    { 27, "Waltham", "Kirlin, Stroman and Baumbach", "61688", "293832653442", "Klein Forges" },
                    { 28, "Carrollberg", "Graham - Ernser", "11817-9656", "613090654847", "Cremin Manors" },
                    { 29, "Vancouver", "Welch Group", "73372-0715", "196336825822", "Chance Prairie" },
                    { 30, "Port Aurelia", "Monahan LLC", "55354", "996030453130", "Hettinger Land" },
                    { 31, "Lynchport", "Breitenberg Inc", "87664", "959290973480", "Mylene Estate" },
                    { 32, "Purdyberg", "Kerluke LLC", "82177-0935", "194634752100", "Hermann Lights" },
                    { 33, "Concepcionworth", "Wyman, Cruickshank and Schumm", "87519-6435", "143354522387", "Goldner Light" },
                    { 34, "New Caesar", "VonRueden, Beahan and D'Amore", "32128", "332440535262", "Hermann Hills" },
                    { 35, "North Deron", "Cormier, Koelpin and Connelly", "97262-0830", "782246492459", "Myles Flats" },
                    { 36, "Eddshire", "Toy, Adams and Sauer", "17455-1134", "754222747062", "Donnelly Hill" },
                    { 37, "East Haroldstad", "Cole Group", "58003-4951", "979269906961", "Jessica Lights" },
                    { 38, "East Samanthabury", "Hayes - Hettinger", "93408", "818856528117", "Rossie Mill" },
                    { 39, "Rennercester", "Hilpert - Cartwright", "03467-5084", "172071918538", "Sedrick Drives" },
                    { 40, "Schuppeburgh", "Rowe - Towne", "54940-1867", "645248189793", "Lebsack Fields" },
                    { 41, "Taraboro", "D'Amore Inc", "50016-2606", "739571662715", "Mueller Fort" },
                    { 42, "Louisaburgh", "Ryan, Emard and Yundt", "15978-2482", "997142334936", "Creola Meadows" },
                    { 43, "Port Mohammadmouth", "Cummerata LLC", "56846", "695194270589", "Trantow Union" },
                    { 44, "East Shawna", "Mayert, Johnson and Roberts", "30244-5644", "642338519954", "Ritchie Coves" },
                    { 45, "North Las Vegas", "Cartwright Inc", "58652-7445", "826268363756", "Maya Mills" },
                    { 46, "Fort Amely", "Greenholt, Bahringer and Goldner", "79182-5457", "334360440694", "Anahi Unions" },
                    { 47, "East Maudeberg", "Murphy - Bashirian", "06352", "473487405840", "Beer Lane" },
                    { 48, "Elenorabury", "Wunsch Inc", "55744", "779631889285", "Mayert Harbors" },
                    { 49, "East Aleenworth", "Considine - Champlin", "26690-3226", "892638472927", "Huel Path" },
                    { 50, "East Claudineport", "Lindgren, Hills and Glover", "92901", "821718435681", "Yundt Rapids" },
                    { 51, "Fort Tyree", "Braun - Heaney", "57129-4236", "821426938537", "Bradley Dale" },
                    { 52, "South Walter", "Herzog, Olson and Runte", "26730-3953", "463404328626", "Sarai Square" },
                    { 53, "Borerfield", "Vandervort - Cummerata", "13957-7836", "934682726602", "Sonya Island" },
                    { 54, "West Alexandraville", "Gulgowski, Schaefer and Collins", "20215", "534315408679", "McLaughlin Gateway" },
                    { 55, "Clarafield", "Koelpin, Lebsack and Schroeder", "38408", "341270012041", "Abigail Ranch" },
                    { 56, "East Tara", "Pollich Inc", "72415", "895315341210", "Padberg Fords" },
                    { 57, "Rodrickville", "Kovacek, Rosenbaum and Dare", "78918-0095", "787746716516", "Margie Lock" },
                    { 58, "New Davion", "Herzog and Sons", "03285-1198", "444168259387", "Fletcher Meadow" },
                    { 59, "Johnsmouth", "Marvin, Donnelly and Lindgren", "54057", "361836175072", "Clemens Ranch" }
                });

            migrationBuilder.InsertData(
                table: "OrderStatus",
                columns: new[] { "OrderStatusId", "OrderStatusName" },
                values: new object[,]
                {
                    { 1, "Non payer" },
                    { 2, "Payment refuser" },
                    { 3, "Payed" },
                    { 4, "En attente de Livraison" },
                    { 5, "Livré" },
                    { 6, "Annuler" }
                });

            migrationBuilder.InsertData(
                table: "Region",
                columns: new[] { "RegionID", "Name" },
                values: new object[] { 1, "region 1" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Administrator", "Admin" },
                    { 2, "Client web", "Client" },
                    { 3, "Fournisseur", "Furnisher" }
                });

            migrationBuilder.InsertData(
                table: "alcohol_type",
                columns: new[] { "AlcoholTypeId", "label" },
                values: new object[,]
                {
                    { 1, "red" },
                    { 2, "grand cru" },
                    { 3, "white" },
                    { 4, "sweet" },
                    { 5, "sparkling" }
                });

            migrationBuilder.InsertData(
                table: "appellation",
                columns: new[] { "AppellationId", "Name" },
                values: new object[,]
                {
                    { 1, "montrachet" },
                    { 2, "meursault" },
                    { 3, "corton-charlemagne" },
                    { 4, "chardonnay" },
                    { 5, "other" },
                    { 6, "blanc" },
                    { 7, "riesling" },
                    { 8, "corton" },
                    { 9, "chablis" },
                    { 10, "puligny-montrachet" },
                    { 11, "bordeaux" },
                    { 12, "albariño" },
                    { 13, "chassagne-montrachet" },
                    { 14, "châteauneuf-du-pape" },
                    { 15, "cuvée" },
                    { 16, "alsace" },
                    { 17, "rouge" },
                    { 18, "viognier" },
                    { 19, "hermitage" },
                    { 20, "bonnezeaux" },
                    { 21, "sancerre" },
                    { 22, "rioja" },
                    { 23, "roussanne" }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "AlcoholDegree", "AlcoholTypeId", "AppellationId", "DomainId", "FurnisherId", "ImageUrl", "Millesime", "Name", "Price", "Reference", "RegionId", "RestockOption", "Stock" },
                values: new object[] { 1, 2f, 1, 1, 1, 1, "", 2010, "product 1", 12, "jndijfndjn", 1, true, 155 });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_OfferId",
                table: "Comment",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_UserId",
                table: "Comment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceFurnisher_FurnisherId",
                table: "InvoiceFurnisher",
                column: "FurnisherId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceFurnisherProduct_ProductId",
                table: "InvoiceFurnisherProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_OrderStatusId",
                table: "Order",
                column: "OrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_UserId",
                table: "Order",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderOffers_OfferId",
                table: "OrderOffers",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_AlcoholTypeId",
                table: "Product",
                column: "AlcoholTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_AppellationId",
                table: "Product",
                column: "AppellationId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_DomainId",
                table: "Product",
                column: "DomainId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_FurnisherId",
                table: "Product",
                column: "FurnisherId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_RegionId",
                table: "Product",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOffer_OfferId",
                table: "ProductOffer",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UserId",
                table: "RefreshToken",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "InvoiceFurnisherProduct");

            migrationBuilder.DropTable(
                name: "OrderOffers");

            migrationBuilder.DropTable(
                name: "ProductOffer");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "InvoiceFurnisher");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Offer");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "OrderStatus");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Domain");

            migrationBuilder.DropTable(
                name: "Furnisher");

            migrationBuilder.DropTable(
                name: "Region");

            migrationBuilder.DropTable(
                name: "alcohol_type");

            migrationBuilder.DropTable(
                name: "appellation");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
