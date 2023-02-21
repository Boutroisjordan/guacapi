using Microsoft.EntityFrameworkCore;
using GuacAPI.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuacAPI.Context.TypeConfigurations;

class FurnisherEntityConfiguration : IEntityTypeConfiguration<Furnisher>
{
    #region Public methods
    public void Configure(EntityTypeBuilder<Furnisher> builder)
    {
        //Nom de la tabmle
        builder.ToTable("Furnisher");
        //Primary key de la table
        builder.HasKey(item => item.FurnisherId);

        builder.HasMany(f => f.Invoices)
            .WithOne(f => f.Furnisher)
            .HasForeignKey(f => f.FurnisherId);

        builder.HasData(
    new Furnisher {
      FurnisherId = 1,
      Name = "Gerhold Group",
      City = "West Nehaborough",
      Street = "Shayne Ridges",
      PostalCode = "37857-3192",
      Siret = "777882687108"
    },
    new Furnisher{
      FurnisherId = 2,
      Name = "Bahringer, Hoeger and Schmidt",
      City = "White Plains",
      Street = "Cruickshank Drive",
      PostalCode = "79893",
      Siret = "668635164541"
    },
    new Furnisher{
      FurnisherId = 3,
      Name = "Runolfsdottir, Pollich and Leuschke",
      City = "Hansenstad",
      Street = "Santa Ford",
      PostalCode = "69414",
      Siret = "608519234115"
    },
    new Furnisher{
      FurnisherId = 4,
      Name = "Considine, Leuschke and Veum",
      City = "Kissimmee",
      Street = "O'Conner Flat",
      PostalCode = "35694-4531",
      Siret = "898202718476"
    },
    new Furnisher{
      FurnisherId = 5,
      Name = "Littel Group",
      City = "West Sidney",
      Street = "Rippin Drive",
      PostalCode = "00475-4444",
      Siret = "195898815118"
    },
    new Furnisher{
      FurnisherId = 6,
      Name = "Quigley - Pfeffer",
      City = "Charlotte",
      Street = "Hassan Port",
      PostalCode = "40802-5108",
      Siret = "732774063940"
    },
    new Furnisher{
      FurnisherId = 7,
      Name = "Jones - Crona",
      City = "New Ezequielbury",
      Street = "Cormier Ford",
      PostalCode = "79842-7277",
      Siret = "112436413622"
    },
    new Furnisher{
      FurnisherId = 8,
      Name = "Emmerich, Davis and McKenzie",
      City = "Las Vegas",
      Street = "Amina Fall",
      PostalCode = "21616",
      Siret = "392543474289"
    },
    new Furnisher{
      FurnisherId = 9,
      Name = "Corwin, Considine and Hane",
      City = "Jacobsshire",
      Street = "Gerry Estate",
      PostalCode = "89039",
      Siret = "364959358625"
    },
    new Furnisher{
      FurnisherId = 10,
      Name = "Volkman - Frami",
      City = "West Evert",
      Street = "Kiehn Pines",
      PostalCode = "17739",
      Siret = "641177102433"
    },
    new Furnisher{
      FurnisherId = 11,
      Name = "Hermiston, Kutch and Vandervort",
      City = "Stefaniebury",
      Street = "Kuhlman Mill",
      PostalCode = "31780",
      Siret = "933450980031"
    },
    new Furnisher{
      FurnisherId = 12,
      Name = "Corkery and Sons",
      City = "Jerdeworth",
      Street = "Hoeger Land",
      PostalCode = "42606",
      Siret = "489556724926"
    },
    new Furnisher{
      FurnisherId = 13,
      Name = "Kuhn - Heaney",
      City = "Kulasbury",
      Street = "Brandy Lake",
      PostalCode = "55898-4844",
      Siret = "588567648610"
    },
    new Furnisher{
      FurnisherId = 14,
      Name = "Boyer, Zieme and Boyer",
      City = "Cristianstead",
      Street = "Donnell Ramp",
      PostalCode = "10873-5688",
      Siret = "962400736745"
    },
    new Furnisher{
      FurnisherId = 15,
      Name = "Emmerich, Roob and Bailey",
      City = "Cummerataport",
      Street = "Leuschke Highway",
      PostalCode = "39709-1993",
      Siret = "546796429516"
    },
    new Furnisher{
      FurnisherId = 16,
      Name = "Lynch LLC",
      City = "Thompsonville",
      Street = "Laurianne Fork",
      PostalCode = "61366",
      Siret = "871958088892"
    },
   new Furnisher {
      FurnisherId = 17,
      Name = "Schneider LLC",
      City = "Jaquanmouth",
      Street = "Cristal Viaduct",
      PostalCode = "64590",
      Siret = "120265792694"
    },
    new Furnisher{
      FurnisherId = 18,
      Name = "Daniel, Lockman and Yundt",
      City = "West Carmine",
      Street = "Pacocha Row",
      PostalCode = "97741-2729",
      Siret = "994578250139"
    },
    new Furnisher{
      FurnisherId = 19,
      Name = "Swaniawski, Hagenes and Sauer",
      City = "Lake Edgarbury",
      Street = "Josh Tunnel",
      PostalCode = "92158",
      Siret = "350262251499"
    },
    new Furnisher{
      FurnisherId = 20,
      Name = "Lehner, Reichel and Frami",
      City = "West Aldaworth",
      Street = "Lottie Unions",
      PostalCode = "12033",
      Siret = "784089780004"
    },
    new Furnisher{
      FurnisherId = 21,
      Name = "Mills - Haley",
      City = "Dearborn Heights",
      Street = "Jalen Extensions",
      PostalCode = "15908-0523",
      Siret = "747049226451"
    },
    new Furnisher{
      FurnisherId = 22,
      Name = "Lynch and Sons",
      City = "Beckerboro",
      Street = "O'Keefe Lodge",
      PostalCode = "06199-2690",
      Siret = "763771335612"
    },
    new Furnisher{
      FurnisherId = 23,
      Name = "Christiansen - Zieme",
      City = "Lake Opheliaside",
      Street = "Filiberto Port",
      PostalCode = "63653-6867",
      Siret = "703487397458"
    },
    new Furnisher{
      FurnisherId = 24,
      Name = "Grant - Ratke",
      City = "New Marcia",
      Street = "Kitty Views",
      PostalCode = "67696-0516",
      Siret = "957838491555"
    },
    new Furnisher{
      FurnisherId = 25,
      Name = "Mann, Funk and Jast",
      City = "East Derontown",
      Street = "Mann Ports",
      PostalCode = "10796",
      Siret = "877881137707"
    },
    new Furnisher{
      FurnisherId = 26,
      Name = "Hegmann Group",
      City = "West Jolieton",
      Street = "Bogisich Locks",
      PostalCode = "96021-4286",
      Siret = "263796604728"
    },
    new Furnisher{
      FurnisherId = 27,
      Name = "Kirlin, Stroman and Baumbach",
      City = "Waltham",
      Street = "Klein Forges",
      PostalCode = "61688",
      Siret = "293832653442"
    },
    new Furnisher{
      FurnisherId = 28,
      Name = "Graham - Ernser",
      City = "Carrollberg",
      Street = "Cremin Manors",
      PostalCode = "11817-9656",
      Siret = "613090654847"
    },
    new Furnisher{
      FurnisherId = 29,
      Name = "Welch Group",
      City = "Vancouver",
      Street = "Chance Prairie",
      PostalCode = "73372-0715",
      Siret = "196336825822"
    },
    new Furnisher{
      FurnisherId = 30,
      Name = "Monahan LLC",
      City = "Port Aurelia",
      Street = "Hettinger Land",
      PostalCode = "55354",
      Siret = "996030453130"
    },
    new Furnisher{
      FurnisherId = 31,
      Name = "Breitenberg Inc",
      City = "Lynchport",
      Street = "Mylene Estate",
      PostalCode = "87664",
      Siret = "959290973480"
    },
    new Furnisher{
      FurnisherId = 32,
      Name = "Kerluke LLC",
      City = "Purdyberg",
      Street = "Hermann Lights",
      PostalCode = "82177-0935",
      Siret = "194634752100"
    },
    new Furnisher{
      FurnisherId = 33,
      Name = "Wyman, Cruickshank and Schumm",
      City = "Concepcionworth",
      Street = "Goldner Light",
      PostalCode = "87519-6435",
      Siret = "143354522387"
    },
    new Furnisher{
      FurnisherId = 34,
      Name = "VonRueden, Beahan and D'Amore",
      City = "New Caesar",
      Street = "Hermann Hills",
      PostalCode = "32128",
      Siret = "332440535262"
    },
    new Furnisher{
      FurnisherId = 35,
      Name = "Cormier, Koelpin and Connelly",
      City = "North Deron",
      Street = "Myles Flats",
      PostalCode = "97262-0830",
      Siret = "782246492459"
    },
    new Furnisher{
      FurnisherId = 36,
      Name = "Toy, Adams and Sauer",
      City = "Eddshire",
      Street = "Donnelly Hill",
      PostalCode = "17455-1134",
      Siret = "754222747062"
    },
    new Furnisher{
      FurnisherId = 37,
      Name = "Cole Group",
      City = "East Haroldstad",
      Street = "Jessica Lights",
      PostalCode = "58003-4951",
      Siret = "979269906961"
    },
    new Furnisher{
      FurnisherId = 38,
      Name = "Hayes - Hettinger",
      City = "East Samanthabury",
      Street = "Rossie Mill",
      PostalCode = "93408",
      Siret = "818856528117"
    },
    new Furnisher{
      FurnisherId = 39,
      Name = "Hilpert - Cartwright",
      City = "Rennercester",
      Street = "Sedrick Drives",
      PostalCode = "03467-5084",
      Siret = "172071918538"
    },
    new Furnisher{
      FurnisherId = 40,
      Name = "Rowe - Towne",
      City = "Schuppeburgh",
      Street = "Lebsack Fields",
      PostalCode = "54940-1867",
      Siret = "645248189793"
    },
    new Furnisher{
      FurnisherId = 41,
      Name = "D'Amore Inc",
      City = "Taraboro",
      Street = "Mueller Fort",
      PostalCode = "50016-2606",
      Siret = "739571662715"
    },
    new Furnisher{
      FurnisherId = 42,
      Name = "Ryan, Emard and Yundt",
      City = "Louisaburgh",
      Street = "Creola Meadows",
      PostalCode = "15978-2482",
      Siret = "997142334936"
    },
    new Furnisher{
      FurnisherId = 43,
      Name = "Cummerata LLC",
      City = "Port Mohammadmouth",
      Street = "Trantow Union",
      PostalCode = "56846",
      Siret = "695194270589"
    },
    new Furnisher{
      FurnisherId = 44,
      Name = "Mayert, Johnson and Roberts",
      City = "East Shawna",
      Street = "Ritchie Coves",
      PostalCode = "30244-5644",
      Siret = "642338519954"
    },
    new Furnisher{
      FurnisherId = 45,
      Name = "Cartwright Inc",
      City = "North Las Vegas",
      Street = "Maya Mills",
      PostalCode = "58652-7445",
      Siret = "826268363756"
    },
    new Furnisher{
      FurnisherId = 46,
      Name = "Greenholt, Bahringer and Goldner",
      City = "Fort Amely",
      Street = "Anahi Unions",
      PostalCode = "79182-5457",
      Siret = "334360440694"
    },
    new Furnisher{
      FurnisherId = 47,
      Name = "Murphy - Bashirian",
      City = "East Maudeberg",
      Street = "Beer Lane",
      PostalCode = "06352",
      Siret = "473487405840"
    },
    new Furnisher{
      FurnisherId = 48,
      Name = "Wunsch Inc",
      City = "Elenorabury",
      Street = "Mayert Harbors",
      PostalCode = "55744",
      Siret = "779631889285"
    },
    new Furnisher{
      FurnisherId = 49,
      Name = "Considine - Champlin",
      City = "East Aleenworth",
      Street = "Huel Path",
      PostalCode = "26690-3226",
      Siret = "892638472927"
    },
    new Furnisher{
      FurnisherId = 50,
      Name = "Lindgren, Hills and Glover",
      City = "East Claudineport",
      Street = "Yundt Rapids",
      PostalCode = "92901",
      Siret = "821718435681"
    },
    new Furnisher{
      FurnisherId = 51,
      Name = "Braun - Heaney",
      City = "Fort Tyree",
      Street = "Bradley Dale",
      PostalCode = "57129-4236",
      Siret = "821426938537"
    },
   new Furnisher {
      FurnisherId = 52,
      Name = "Herzog, Olson and Runte",
      City = "South Walter",
      Street = "Sarai Square",
      PostalCode = "26730-3953",
      Siret = "463404328626"
    },
    new Furnisher{
      FurnisherId = 53,
      Name = "Vandervort - Cummerata",
      City = "Borerfield",
      Street = "Sonya Island",
      PostalCode = "13957-7836",
      Siret = "934682726602"
    },
    new Furnisher{
      FurnisherId = 54,
      Name = "Gulgowski, Schaefer and Collins",
      City = "West Alexandraville",
      Street = "McLaughlin Gateway",
      PostalCode = "20215",
      Siret = "534315408679"
    },
    new Furnisher{
      FurnisherId = 55,
      Name = "Koelpin, Lebsack and Schroeder",
      City = "Clarafield",
      Street = "Abigail Ranch",
      PostalCode = "38408",
      Siret = "341270012041"
    },
    new Furnisher{
      FurnisherId = 56,
      Name = "Pollich Inc",
      City = "East Tara",
      Street = "Padberg Fords",
      PostalCode = "72415",
      Siret = "895315341210"
    },
   new Furnisher {
      FurnisherId = 57,
      Name = "Kovacek, Rosenbaum and Dare",
      City = "Rodrickville",
      Street = "Margie Lock",
      PostalCode = "78918-0095",
      Siret = "787746716516"
    },
   new Furnisher {
      FurnisherId = 58,
      Name = "Herzog and Sons",
      City = "New Davion",
      Street = "Fletcher Meadow",
      PostalCode = "03285-1198",
      Siret = "444168259387"
    },
    new Furnisher{
      FurnisherId = 59,
      Name = "Marvin, Donnelly and Lindgren",
      City = "Johnsmouth",
      Street = "Clemens Ranch",
      PostalCode = "54057",
      Siret = "361836175072"
    }
        );
    }


    // protected override void OnModelCreating()
    #endregion

}

