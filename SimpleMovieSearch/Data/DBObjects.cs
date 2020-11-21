using SimpleMovieSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMovieSearch.Data
{
    public class DBObjects
    {
        public static void Initial(AppDBContent content)
        {
            if (!content.Author.Any())
                content.Author.AddRange(Authors.Select(c => c.Value));

            if (!content.Video.Any())
            {
                content.AddRange(
                    new Video
                    {
                        Image = "https://i.pinimg.com/736x/4f/23/71/4f23717fbef818499a42c06de774bbc3.jpg",
                        Title = "Дюна",
                        ShortDescription = "Предстоящий фантастический фильм режиссёра Дени Вильнёва. Экранизация одноимённого романа Фрэнка Герберта.",
                        LongDescription = "Человечество расселилось по далёким планетам, а за власть над обитаемым пространством постоянно борются разные могущественные семьи. В центре противостояния оказывается пустынная планета Арракис. Там обитают гигантские песчаные черви, а в пещерах затаились скитальцы-фремены, но её главная ценность — спайс, самое важное вещество во Вселенной. Тот, кто контролирует Арракис, контролирует спайс, а тот, кто контролирует спайс, контролирует Вселенную.",
                        IsFavorites = true,
                        Author = Authors["Дени"],
                    },
                    new Video
                    {
                        Image = "https://avatars.mds.yandex.net/get-kinopoisk-image/1599028/637271d5-61b4-4e46-ac83-6d07494c7645/600x900",
                        Title = "Джентельмены",
                        ShortDescription = "Криминальная комедия режиссёра Гая Ричи по собственному сценарию.",
                        LongDescription = "Талантливый выпускник Оксфорда, применив свой уникальный ум и невиданную дерзость, придумал нелегальную схему обогащения с использованием поместья обедневшей английской аристократии. Однако когда он решает продать свой бизнес влиятельному клану миллиардеров из США, на его пути встают не менее обаятельные, но жесткие джентльмены. Намечается обмен любезностями, который точно не обойдется без перестрелок и парочки несчастных случаев.",
                        IsFavorites = true,
                        Author = Authors["Гай"],
                    },
                    new Video
                    {
                        Image = "https://www.film.ru/sites/default/files/movies/posters/37865107-1142009.jpg",
                        Title = "Шестеро вне закона",
                        ShortDescription = "«Призрачная шестерка» или «Шестеро вне закона» об этом тоже. О красоте разрушения во всех ее проявлениях.",
                        LongDescription = "Инсценировав собственную смерть, сознательный миллиардер начинает новую жизнь — набирает отряд наёмников из разных стран, чтобы бороться со злом в этом мире. Все члены команды получают вместо имён цифры, и их целью становится свержение диктатора и освобождение народа страны Тургистан.",
                        IsFavorites = false,
                        Author = Authors["Майкл"],
                    }
                    );
            }
            content.SaveChanges();
        }

        private static Dictionary<string, Author> author;
        public static Dictionary<string, Author> Authors
        {
            get
            {
                if(author == null)
                {
                    var list = new Author[]
                    {
                        new Author {Name ="Майкл", Surname="Бенджамин Бэй", Age= 55, Description="американский кинорежиссёр и кинопродюсер. Один из самых кассовых режиссёров планеты — его картины собрали в прокате более 5,7 млрд долларов США."},
                        new Author {Name ="Гай", Surname="Стюарт Ри́чи", Age= 52, Description="британский кинорежиссёр, сценарист, продюсер, чаще всего работающий в жанре криминальной комедии"},
                        new Author {Name ="Дени", Surname="Вильнёв", Age= 53, Description="франко-канадский кинорежиссёр и сценарист."}
                    };

                    author = new Dictionary<string, Author>();
                    foreach (Author el in list)
                        author.Add(el.Name, el);
                }
                return author;
            }
        }

        //private static Dictionary<string, List<Genre>> genre;
        //public static Dictionary<string, List<Genre>> Genres
        //{
        //    get
        //    {
        //        if (genre == null)
        //        {
        //            var list = new List<Genre>
        //            {
        //                new Genre {Name = "фантастика"},
        //                new Genre {Name = "криминальная ‎комедия"},
        //                new Genre {Name = "комедия"}
        //            };

        //            genre = new Dictionary<string, List<Genre>>();
        //            foreach (Genre el in list)
        //                genre.Add(el.Name, el);
        //        }
        //        return genre;
        //    }
        //}
    }
}
