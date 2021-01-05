using Microsoft.EntityFrameworkCore;
using SimpleMovieSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMovieSearch.Data
{
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions<AppContext> options) : base(options)
        {
        }

        public DbSet<Video> Video { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionbuilder)
        {
            optionbuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Video>()
                .HasOne<Author>(s => s.Author)
                .WithMany(g => g.Videos)
                .HasForeignKey(s => s.AuthorId);

            modelBuilder
                .Entity<Video>()
                .HasMany(e => e.Genres)
                .WithMany(e => e.Videos)
                .UsingEntity<Dictionary<string, object>>(
                "CommunicationsVideoGenres",
                    b => b.HasOne<Genre>().WithMany().HasForeignKey("GenreId"),
                    b => b.HasOne<Video>().WithMany().HasForeignKey("VideoId"),
                    b => b.HasData(
                        new { VideoId = 1, GenreId = 1 },
                        new { VideoId = 1, GenreId = 2 },
                        new { VideoId = 2, GenreId = 2 },
                        new { VideoId = 2, GenreId = 3 },
                        new { VideoId = 3, GenreId = 3 },
                        new { VideoId = 3, GenreId = 1 }
                    )
                    );
                
            var authorMikle = new Author { Id = 1, Name = "Майкл", Surname = "Бенджамин Бэй", Age = 55, Description = "американский кинорежиссёр и кинопродюсер. Один из самых кассовых режиссёров планеты — его картины собрали в прокате более 5,7 млрд долларов США." };
            var authorGie = new Author { Id = 2, Name = "Гай", Surname = "Стюарт Ри́чи", Age = 52, Description = "британский кинорежиссёр, сценарист, продюсер, чаще всего работающий в жанре криминальной комедии" };
            var authorDeni = new Author { Id = 3, Name = "Дени", Surname = "Вильнёв", Age = 53, Description = "франко-канадский кинорежиссёр и сценарист." };

            var genreFantastic = new Genre { Id = 1, Name = "Фантастика", };
            var genreComedian = new Genre { Id = 2, Name = "Комедия" };
            var genreCrimalComedian = new Genre { Id = 3, Name = "Криминальная комедия" };

            modelBuilder.Entity<Author>().HasData(authorMikle, authorDeni, authorGie);
            modelBuilder.Entity<Genre>().HasData(genreFantastic, genreComedian, genreCrimalComedian);

            var dune = new Video
            {
                    Id = 1,
                    Image = "https://i.pinimg.com/736x/4f/23/71/4f23717fbef818499a42c06de774bbc3.jpg",
                    Title = "Дюна",
                    ShortDescription = "Предстоящий фантастический фильм режиссёра Дени Вильнёва. Экранизация одноимённого романа Фрэнка Герберта.",
                    LongDescription = "Человечество расселилось по далёким планетам, а за власть над обитаемым пространством постоянно борются разные могущественные семьи. В центре противостояния оказывается пустынная планета Арракис. Там обитают гигантские песчаные черви, а в пещерах затаились скитальцы-фремены, но её главная ценность — спайс, самое важное вещество во Вселенной. Тот, кто контролирует Арракис, контролирует спайс, а тот, кто контролирует спайс, контролирует Вселенную.",
                    IsFavorites = true,
                    AuthorId = 1,
            };

            var jentelmens = new Video
            {
                    Id = 2,
                    Image = "https://avatars.mds.yandex.net/get-kinopoisk-image/1599028/637271d5-61b4-4e46-ac83-6d07494c7645/600x900",
                    Title = "Джентельмены",
                    ShortDescription = "Криминальная комедия режиссёра Гая Ричи по собственному сценарию.",
                    LongDescription = "Талантливый выпускник Оксфорда, применив свой уникальный ум и невиданную дерзость, придумал нелегальную схему обогащения с использованием поместья обедневшей английской аристократии. Однако когда он решает продать свой бизнес влиятельному клану миллиардеров из США, на его пути встают не менее обаятельные, но жесткие джентльмены. Намечается обмен любезностями, который точно не обойдется без перестрелок и парочки несчастных случаев.",
                    IsFavorites = true,
                    AuthorId = 2,
            };

            var sixInstage = new Video
            {
                Id = 3,
                Image = "https://www.film.ru/sites/default/files/movies/posters/37865107-1142009.jpg",
                Title = "Шестеро вне закона",
                ShortDescription = "«Призрачная шестерка» или «Шестеро вне закона» об этом тоже. О красоте разрушения во всех ее проявлениях.",
                LongDescription = "Инсценировав собственную смерть, сознательный миллиардер начинает новую жизнь — набирает отряд наёмников из разных стран, чтобы бороться со злом в этом мире. Все члены команды получают вместо имён цифры, и их целью становится свержение диктатора и освобождение народа страны Тургистан.",
                IsFavorites = false,
                AuthorId = 3,
            };

            modelBuilder.Entity<Video>()
                .HasData(dune, jentelmens, sixInstage);
        }
        }
}
