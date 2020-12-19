using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleMovieSearch.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Video",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LongDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsFavorites = table.Column<bool>(type: "bit", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Video", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Video_Author_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Author",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserVideo",
                columns: table => new
                {
                    FavoriteVideosId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserVideo", x => new { x.FavoriteVideosId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_UserVideo_User_UsersId",
                        column: x => x.UsersId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserVideo_Video_FavoriteVideosId",
                        column: x => x.FavoriteVideosId,
                        principalTable: "Video",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VideoGenres",
                columns: table => new
                {
                    VideosId = table.Column<int>(type: "int", nullable: false),
                    GenresId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoGenres", x => new { x.GenresId, x.VideosId });
                    table.ForeignKey(
                        name: "FK_VideoGenres_Genre_GenresId",
                        column: x => x.GenresId,
                        principalTable: "Genre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VideoGenres_Video_VideosId",
                        column: x => x.VideosId,
                        principalTable: "Video",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Author",
                columns: new[] { "Id", "Age", "Description", "Name", "Surname" },
                values: new object[,]
                {
                    { 1, 55, "американский кинорежиссёр и кинопродюсер. Один из самых кассовых режиссёров планеты — его картины собрали в прокате более 5,7 млрд долларов США.", "Майкл", "Бенджамин Бэй" },
                    { 2, 52, "британский кинорежиссёр, сценарист, продюсер, чаще всего работающий в жанре криминальной комедии", "Гай", "Стюарт Ри́чи" },
                    { 3, 53, "франко-канадский кинорежиссёр и сценарист.", "Дени", "Вильнёв" }
                });

            migrationBuilder.InsertData(
                table: "Genre",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Фантастика" },
                    { 2, "Комедия" },
                    { 3, "Криминальная комедия" }
                });

            migrationBuilder.InsertData(
                table: "Video",
                columns: new[] { "Id", "AuthorId", "Image", "IsFavorites", "LongDescription", "ShortDescription", "Title" },
                values: new object[] { 1, 1, "https://i.pinimg.com/736x/4f/23/71/4f23717fbef818499a42c06de774bbc3.jpg", true, "Человечество расселилось по далёким планетам, а за власть над обитаемым пространством постоянно борются разные могущественные семьи. В центре противостояния оказывается пустынная планета Арракис. Там обитают гигантские песчаные черви, а в пещерах затаились скитальцы-фремены, но её главная ценность — спайс, самое важное вещество во Вселенной. Тот, кто контролирует Арракис, контролирует спайс, а тот, кто контролирует спайс, контролирует Вселенную.", "Предстоящий фантастический фильм режиссёра Дени Вильнёва. Экранизация одноимённого романа Фрэнка Герберта.", "Дюна" });

            migrationBuilder.InsertData(
                table: "Video",
                columns: new[] { "Id", "AuthorId", "Image", "IsFavorites", "LongDescription", "ShortDescription", "Title" },
                values: new object[] { 2, 2, "https://avatars.mds.yandex.net/get-kinopoisk-image/1599028/637271d5-61b4-4e46-ac83-6d07494c7645/600x900", true, "Талантливый выпускник Оксфорда, применив свой уникальный ум и невиданную дерзость, придумал нелегальную схему обогащения с использованием поместья обедневшей английской аристократии. Однако когда он решает продать свой бизнес влиятельному клану миллиардеров из США, на его пути встают не менее обаятельные, но жесткие джентльмены. Намечается обмен любезностями, который точно не обойдется без перестрелок и парочки несчастных случаев.", "Криминальная комедия режиссёра Гая Ричи по собственному сценарию.", "Джентельмены" });

            migrationBuilder.InsertData(
                table: "Video",
                columns: new[] { "Id", "AuthorId", "Image", "IsFavorites", "LongDescription", "ShortDescription", "Title" },
                values: new object[] { 3, 3, "https://www.film.ru/sites/default/files/movies/posters/37865107-1142009.jpg", false, "Инсценировав собственную смерть, сознательный миллиардер начинает новую жизнь — набирает отряд наёмников из разных стран, чтобы бороться со злом в этом мире. Все члены команды получают вместо имён цифры, и их целью становится свержение диктатора и освобождение народа страны Тургистан.", "«Призрачная шестерка» или «Шестеро вне закона» об этом тоже. О красоте разрушения во всех ее проявлениях.", "Шестеро вне закона" });

            migrationBuilder.InsertData(
                table: "VideoGenres",
                columns: new[] { "GenresId", "VideosId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 2, 2 },
                    { 3, 2 },
                    { 3, 3 },
                    { 1, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserVideo_UsersId",
                table: "UserVideo",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Video_AuthorId",
                table: "Video",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoGenres_VideosId",
                table: "VideoGenres",
                column: "VideosId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserVideo");

            migrationBuilder.DropTable(
                name: "VideoGenres");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropTable(
                name: "Video");

            migrationBuilder.DropTable(
                name: "Author");
        }
    }
}
