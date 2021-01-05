﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SimpleMovieSearch.Data;

namespace SimpleMovieSearch.Migrations
{
    [DbContext(typeof(AppContext))]
    partial class AppDBContentModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("CommunicationsVideoGenres", b =>
                {
                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.Property<int>("VideoId")
                        .HasColumnType("int");

                    b.HasKey("GenreId", "VideoId");

                    b.HasIndex("VideoId");

                    b.ToTable("CommunicationsVideoGenres");

                    b.HasData(
                        new
                        {
                            GenreId = 1,
                            VideoId = 1
                        },
                        new
                        {
                            GenreId = 2,
                            VideoId = 1
                        },
                        new
                        {
                            GenreId = 2,
                            VideoId = 2
                        },
                        new
                        {
                            GenreId = 3,
                            VideoId = 2
                        },
                        new
                        {
                            GenreId = 3,
                            VideoId = 3
                        },
                        new
                        {
                            GenreId = 1,
                            VideoId = 3
                        });
                });

            modelBuilder.Entity("SimpleMovieSearch.Models.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Author");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Age = 55,
                            Description = "американский кинорежиссёр и кинопродюсер. Один из самых кассовых режиссёров планеты — его картины собрали в прокате более 5,7 млрд долларов США.",
                            Name = "Майкл",
                            Surname = "Бенджамин Бэй"
                        },
                        new
                        {
                            Id = 3,
                            Age = 53,
                            Description = "франко-канадский кинорежиссёр и сценарист.",
                            Name = "Дени",
                            Surname = "Вильнёв"
                        },
                        new
                        {
                            Id = 2,
                            Age = 52,
                            Description = "британский кинорежиссёр, сценарист, продюсер, чаще всего работающий в жанре криминальной комедии",
                            Name = "Гай",
                            Surname = "Стюарт Ри́чи"
                        });
                });

            modelBuilder.Entity("SimpleMovieSearch.Models.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Genre");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Фантастика"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Комедия"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Криминальная комедия"
                        });
                });

            modelBuilder.Entity("SimpleMovieSearch.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("SimpleMovieSearch.Models.Video", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsFavorites")
                        .HasColumnType("bit");

                    b.Property<string>("LongDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShortDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Video");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AuthorId = 1,
                            Image = "https://i.pinimg.com/736x/4f/23/71/4f23717fbef818499a42c06de774bbc3.jpg",
                            IsFavorites = true,
                            LongDescription = "Человечество расселилось по далёким планетам, а за власть над обитаемым пространством постоянно борются разные могущественные семьи. В центре противостояния оказывается пустынная планета Арракис. Там обитают гигантские песчаные черви, а в пещерах затаились скитальцы-фремены, но её главная ценность — спайс, самое важное вещество во Вселенной. Тот, кто контролирует Арракис, контролирует спайс, а тот, кто контролирует спайс, контролирует Вселенную.",
                            ShortDescription = "Предстоящий фантастический фильм режиссёра Дени Вильнёва. Экранизация одноимённого романа Фрэнка Герберта.",
                            Title = "Дюна"
                        },
                        new
                        {
                            Id = 2,
                            AuthorId = 2,
                            Image = "https://avatars.mds.yandex.net/get-kinopoisk-image/1599028/637271d5-61b4-4e46-ac83-6d07494c7645/600x900",
                            IsFavorites = true,
                            LongDescription = "Талантливый выпускник Оксфорда, применив свой уникальный ум и невиданную дерзость, придумал нелегальную схему обогащения с использованием поместья обедневшей английской аристократии. Однако когда он решает продать свой бизнес влиятельному клану миллиардеров из США, на его пути встают не менее обаятельные, но жесткие джентльмены. Намечается обмен любезностями, который точно не обойдется без перестрелок и парочки несчастных случаев.",
                            ShortDescription = "Криминальная комедия режиссёра Гая Ричи по собственному сценарию.",
                            Title = "Джентельмены"
                        },
                        new
                        {
                            Id = 3,
                            AuthorId = 3,
                            Image = "https://www.film.ru/sites/default/files/movies/posters/37865107-1142009.jpg",
                            IsFavorites = false,
                            LongDescription = "Инсценировав собственную смерть, сознательный миллиардер начинает новую жизнь — набирает отряд наёмников из разных стран, чтобы бороться со злом в этом мире. Все члены команды получают вместо имён цифры, и их целью становится свержение диктатора и освобождение народа страны Тургистан.",
                            ShortDescription = "«Призрачная шестерка» или «Шестеро вне закона» об этом тоже. О красоте разрушения во всех ее проявлениях.",
                            Title = "Шестеро вне закона"
                        });
                });

            modelBuilder.Entity("UserVideo", b =>
                {
                    b.Property<int>("FavoriteVideosId")
                        .HasColumnType("int");

                    b.Property<int>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("FavoriteVideosId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("UserVideo");
                });

            modelBuilder.Entity("CommunicationsVideoGenres", b =>
                {
                    b.HasOne("SimpleMovieSearch.Models.Genre", null)
                        .WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SimpleMovieSearch.Models.Video", null)
                        .WithMany()
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SimpleMovieSearch.Models.Video", b =>
                {
                    b.HasOne("SimpleMovieSearch.Models.Author", "Author")
                        .WithMany("Videos")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("UserVideo", b =>
                {
                    b.HasOne("SimpleMovieSearch.Models.Video", null)
                        .WithMany()
                        .HasForeignKey("FavoriteVideosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SimpleMovieSearch.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SimpleMovieSearch.Models.Author", b =>
                {
                    b.Navigation("Videos");
                });
#pragma warning restore 612, 618
        }
    }
}
