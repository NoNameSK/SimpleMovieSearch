using Microsoft.EntityFrameworkCore;
using SimpleMovieSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMovieSearch.Data
{
    public class AppDBContent : DbContext
    {
        public AppDBContent(DbContextOptions<AppDBContent> options) : base(options)
        {
        }

        public DbSet<Video> Video { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<Genre> Genre { get; set; }
        //public DbSet<Communication> Communications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Video>()
                .HasOne<Author>(s => s.Author)
                .WithMany(g => g.Videos)
                .HasForeignKey(s => s.AuthorId);

            modelBuilder.Entity<Video>()
                .HasMany(x => x.Genres)
                .WithMany(x => x.Videos)
                .UsingEntity<Dictionary<string, object>>(
                    "VideoGenre",
                    x => x.HasOne<Genre>().WithMany(),
                    x => x.HasOne<Video>().WithMany());

            //modelBuilder.Entity<Video>()
            //    .HasMany(c => c.Genres)
            //    .WithMany(s => s.Videos)
            //    .UsingEntity<Communication>(
            //    j => j
            //        .HasOne(pt => pt.Genre)
            //        .WithMany(t => t.Communications)
            //        .HasForeignKey(pt => pt.GenreId),
            //    j => j
            //        .HasOne(pt => pt.Video)
            //        .WithMany(p => p.Communications)
            //        .HasForeignKey(pt => pt.VideoId),
            //    j =>
            //    {
            //        j.HasKey(t => new { t.VideoId , t.GenreId });
            //        j.ToTable("Communication");
            //    });

            modelBuilder.Entity<Author>().HasData(
                new { Id = 1, Name = "Майкл", Surname = "Бенджамин Бэй", Age = 55, Description = "американский кинорежиссёр и кинопродюсер. Один из самых кассовых режиссёров планеты — его картины собрали в прокате более 5,7 млрд долларов США." },
                new { Id = 2, Name = "Гай", Surname = "Стюарт Ри́чи", Age = 52, Description = "британский кинорежиссёр, сценарист, продюсер, чаще всего работающий в жанре криминальной комедии" },
                new { Id = 3, Name = "Дени", Surname = "Вильнёв", Age = 53, Description = "франко-канадский кинорежиссёр и сценарист." });

            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Фантастика" },
                new Genre { Id = 2, Name = "Комедия" },
                new Genre { Id = 3, Name = "Криминальная комедия" }
                );

            //var dunaGanres = new List<Genre>
            //{
            //    new Genre { Id = 1, Name = "Фантастика" }
            //};
            //var djentelmensGanres = new List<Genre>
            //{
            //    new Genre { Id = 2, Name = "Комедия" }
            //};
            //var sixOutlaw = new List<Genre>
            //{
            //    new Genre { Id = 3, Name = "Криминальная комедия" }
            //};

            //modelBuilder.Entity<Genre>().HasData(
            //    dunaGanres, djentelmensGanres, sixOutlaw
            // );

            //var videoDune = new Video
            //{
            //    Id = 1,
            //    Image = "https://i.pinimg.com/736x/4f/23/71/4f23717fbef818499a42c06de774bbc3.jpg",
            //    Title = "Дюна",
            //    ShortDescription = "Предстоящий фантастический фильм режиссёра Дени Вильнёва. Экранизация одноимённого романа Фрэнка Герберта.",
            //    LongDescription = "Человечество расселилось по далёким планетам, а за власть над обитаемым пространством постоянно борются разные могущественные семьи. В центре противостояния оказывается пустынная планета Арракис. Там обитают гигантские песчаные черви, а в пещерах затаились скитальцы-фремены, но её главная ценность — спайс, самое важное вещество во Вселенной. Тот, кто контролирует Арракис, контролирует спайс, а тот, кто контролирует спайс, контролирует Вселенную.",
            //    IsFavorites = true,
            //    AuthorId = 1,
            //    Genres = dunaGanres,
            //};

            //var videoDjantelmens = new Video
            //{
            //    Id = 2,
            //    Image = "https://avatars.mds.yandex.net/get-kinopoisk-image/1599028/637271d5-61b4-4e46-ac83-6d07494c7645/600x900",
            //    Title = "Джентельмены",
            //    ShortDescription = "Криминальная комедия режиссёра Гая Ричи по собственному сценарию.",
            //    LongDescription = "Талантливый выпускник Оксфорда, применив свой уникальный ум и невиданную дерзость, придумал нелегальную схему обогащения с использованием поместья обедневшей английской аристократии. Однако когда он решает продать свой бизнес влиятельному клану миллиардеров из США, на его пути встают не менее обаятельные, но жесткие джентльмены. Намечается обмен любезностями, который точно не обойдется без перестрелок и парочки несчастных случаев.",
            //    IsFavorites = true,
            //    AuthorId = 2,
            //    //Genres = djentelmensGanres
            //};

            //var videoSixOutlaw = new Video
            //{
            //    Id = 3,
            //    Image = "https://www.film.ru/sites/default/files/movies/posters/37865107-1142009.jpg",
            //    Title = "Шестеро вне закона",
            //    ShortDescription = "«Призрачная шестерка» или «Шестеро вне закона» об этом тоже. О красоте разрушения во всех ее проявлениях.",
            //    LongDescription = "Инсценировав собственную смерть, сознательный миллиардер начинает новую жизнь — набирает отряд наёмников из разных стран, чтобы бороться со злом в этом мире. Все члены команды получают вместо имён цифры, и их целью становится свержение диктатора и освобождение народа страны Тургистан.",
            //    IsFavorites = false,
            //    AuthorId = 3,
            //    //Genres = sixOutlaw
            //};

            //modelBuilder.Entity<Video>()
            //    .HasData(videoDjantelmens, videoDune, videoSixOutlaw);

            modelBuilder.Entity<Video>()
                .HasData(
                new Video
                {
                    Id = 1,
                    Image = "https://i.pinimg.com/736x/4f/23/71/4f23717fbef818499a42c06de774bbc3.jpg",
                    Title = "Дюна",
                    ShortDescription = "Предстоящий фантастический фильм режиссёра Дени Вильнёва. Экранизация одноимённого романа Фрэнка Герберта.",
                    LongDescription = "Человечество расселилось по далёким планетам, а за власть над обитаемым пространством постоянно борются разные могущественные семьи. В центре противостояния оказывается пустынная планета Арракис. Там обитают гигантские песчаные черви, а в пещерах затаились скитальцы-фремены, но её главная ценность — спайс, самое важное вещество во Вселенной. Тот, кто контролирует Арракис, контролирует спайс, а тот, кто контролирует спайс, контролирует Вселенную.",
                    IsFavorites = true,
                    AuthorId = 1,
                    Genres = new List<Genre>() 
                },
                new Video
                {
                    Id = 2,
                    Image = "https://avatars.mds.yandex.net/get-kinopoisk-image/1599028/637271d5-61b4-4e46-ac83-6d07494c7645/600x900",
                    Title = "Джентельмены",
                    ShortDescription = "Криминальная комедия режиссёра Гая Ричи по собственному сценарию.",
                    LongDescription = "Талантливый выпускник Оксфорда, применив свой уникальный ум и невиданную дерзость, придумал нелегальную схему обогащения с использованием поместья обедневшей английской аристократии. Однако когда он решает продать свой бизнес влиятельному клану миллиардеров из США, на его пути встают не менее обаятельные, но жесткие джентльмены. Намечается обмен любезностями, который точно не обойдется без перестрелок и парочки несчастных случаев.",
                    IsFavorites = true,
                    AuthorId = 2,
                    Genres = new List<Genre>() 
                },
                new Video
                {
                    Id = 3,
                    Image = "https://www.film.ru/sites/default/files/movies/posters/37865107-1142009.jpg",
                    Title = "Шестеро вне закона",
                    ShortDescription = "«Призрачная шестерка» или «Шестеро вне закона» об этом тоже. О красоте разрушения во всех ее проявлениях.",
                    LongDescription = "Инсценировав собственную смерть, сознательный миллиардер начинает новую жизнь — набирает отряд наёмников из разных стран, чтобы бороться со злом в этом мире. Все члены команды получают вместо имён цифры, и их целью становится свержение диктатора и освобождение народа страны Тургистан.",
                    IsFavorites = false,
                    AuthorId = 3,
                    Genres = new List<Genre>() 
                }
                );

            modelBuilder.SharedTypeEntity<Dictionary<string, object>>("VideoGenre").HasData(
            new { VideosId = 1, GenresId = 1 },
            new { VideosId = 2, GenresId = 2 },
            new { VideosId = 3, GenresId = 3 }
            );
            modelBuilder.SharedTypeEntity<Dictionary<string, object>>("VideoGenre").ToTable("VideoGenres");
        }
        }
}
