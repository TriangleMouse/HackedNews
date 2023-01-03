using System;
using System.Collections.Generic;
using System.Linq;
using HackedNews.Data.Models.NewsModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HackedNews.Data
{
    public static class SeedData
    {
        private static Dictionary<string, Category> category;

        public static Dictionary<string, Category> Categories
        {
            get
            {
                if (category == null)
                {
                    var list = new[]
                    {
                        new Category
                        {
                            Name = "Культура",
                            Desc =
                                "Читайте свежие новости культуры: о новинках кино, культурных событиях в регионах, фестивалях и литературных премиях..."
                        },
                        new Category
                        {
                            Name = "Наука",
                            Desc = "Актуальные новости научных открытий, высоких технологий, электроники и космоса... "
                        },
                        new Category
                        {
                            Name = "Политика", Desc = "Актуальные новости о политике, политических преступлениях..."
                        },
                        new Category { Name = "Спорт", Desc = "Последние новости спорта, самые свежие событии..." },
                        new Category
                        {
                            Name = "Экономика", Desc = "Самые актуальные новости об экономики во всех странах мир..."
                        },
                        new Category { Name = "Происшествия", Desc = "Происшествия и криминальные новости..." }
                    };

                    category = new Dictionary<string, Category>();
                    foreach (var el in list) category.Add(el.Name, el);
                }

                return category;
            }
        }

        private static int GetCategoryId(string CategoryName, AppDBContext context)
        {
            return context.Category.FirstOrDefault(p => p.Name == CategoryName).Id;
        }

        public static void EnsurePopulated(IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDBContext>();
            context.Database.Migrate();

            if (!context.Category.Any())
            {
                context.Category.AddRange(Categories.Select(c => c.Value));
                context.SaveChanges();
            }

            if (!context.News.Any())
            {
                context.News.AddRange(
                    new News
                    {
                        Title = "Слова Лаврова о Тайване привели китайцев в восторг",
                        NewsIntroduction = null,
                        ImgLink =
                            "https://cdnn21.img.ria.ru/images/07e5/0a/06/1753387486_0:0:3106:2047_1440x900_80_1_1_373f14d7dd32e06c2779f98ff9e022f5.jpg.webp?source-sid=rian_photo",
                        SwitchLoadImg = false,
                        ImgLoad = null,
                        AuthorNews = "Риа Новости",
                        CategoryId = GetCategoryId("Политика", context),
                        ShorttextCard =
                            "Читатели китайского издания \"Гуаньча\" положительно оценили слова главы российского МИД Сергея Лаврова, заявившего...",
                        Date = DateTime.Now.ToString(),
                        ListNewsDatas = new List<NewsData>
                        {
                            new NewsData
                            {
                                Subtitle =
                                    @"Читатели ""Гуанча"": слова Лаврова о принадлежности Тайваня к КНР свидетельствуют о дружбе",
                                ImgLink = null,
                                SwitchLoadImg = false,
                                ImgLoad = null,
                                Txt =
                                    "МОСКВА, 14 окт — РИА Новости. Читатели китайского издания \"Гуаньча\" положительно оценили слова главы российского МИД Сергея Лаврова, заявившего, что Россия, как и подавляющее большинство стран мира," +
                                    " считает Тайвань частью Китайской Народной Республики.\"Это основа китайско-российской дружбы и основа отношений любой страны с Китаем\", — написал пользователь под ником Xiaoxia.\"Россия — хороший партнер, " +
                                    "хороший сосед и отличный друг\", — добавил другой комментатор Некоторые читатели посчитали, что позиция Лаврова точнее выражает отношение Пекина к Тайваню, чем заявления официальных лиц, называющих остров \"частью Китая\" (а не КНР)."
                            }
                        }
                    },
                    new News
                    {
                        Title = "3 лучших видеорегистраторов с Aliexpress",
                        NewsIntroduction =
                            "Если вам понадобился видеорегистратор, то проще всего будет купить его на Aliexpress. Есть ли смысл покупать его там и ждать посылку из Китая? На практике, если вы готовы подождать от 2 до 4 недель (большинство посылок приходит в эти сроки), то можно получить хорошее устройство (все видеорегистраторы, так или иначе производятся в Китае), сэкономив при этом до половины стоимости того же регистратора в нашей рознице. ",
                        ImgLink = "https://gagadget.com/media/cache/0b/db/0bdb67ffa987ff2abf72f173369555e7.jpg",
                        SwitchLoadImg = false,
                        ImgLoad = null,
                        AuthorNews = "HackedNews",
                        CategoryId = GetCategoryId("Наука", context),
                        ShorttextCard =
                            "Если вам понадобился видеорегистратор, то проще всего будет купить его на Aliexpres...",
                        Date = DateTime.Now.ToString(),
                        ListNewsDatas = new List<NewsData>
                        {
                            new NewsData
                            {
                                Subtitle = @"Видеорегистратор 70MAI Dash Cam 4k A800S",
                                ImgLink =
                                    "https://gagadget.com/media/cache/c9/10/c9100874fb49e01ce0b9b9e9548f3728.jpg",
                                SwitchLoadImg = false,
                                ImgLoad = null,
                                Txt =
                                    "Компания Xiaomi вместе с брендом 70MAI выпустила новый видеорегистратор A800S, который можно назвать флагманом во всей линейке." +
                                    " По качеству сборки всё очень неплохо, хороший матовый пластик, сама камера держится очень жёстко и тряски не будет даже на сложной дороге. " +
                                    "В отличие от его предшественников у него встроенный GPS. Управление через ваш смартфон через официальное приложение + 4 физические кнопки на передней панели." +
                                    " Пользователи недолюбливают стандартную озвучку у видеорегистратора, но есть возможно через 4PDA скачать прошивку и поставить нормальную озвучку. " +
                                    "Также вам должен понравится ночной режим, у гаджета стоит 7 линзовый объектив от Sony.Цена: $103-152 (В зависимости от комплектации). Хочу купить!"
                            },
                            new NewsData
                            {
                                Subtitle = @"Видеорегистратор 70MAI A500S Pro Plus",
                                ImgLink =
                                    "https://gagadget.com/media/uploads/%D0%92%D0%B8%D0%B4%D0%B5%D0%BE%D1%80%D0%B5%D0%B3%D0%B8%D1%81%D1%82%D1%80%D0%B0%D1%82%D0%BE%D1%80%2070MAI%20A500S%20Pro%20Plus.jpg",
                                SwitchLoadImg = false,
                                ImgLoad = null,
                                Txt =
                                    "Перечислять характеристике уже нет смысла, так как они сильно совпадают с предыдущими, можно выделить только одну вещь: запись будет в 2592x1944, что означает режим 4:3 (это не очень хорошо)." +
                                    " Задняя камера достаточно плоха (пользователи выделают туманность картинки и факт, что номера видно только вблизи). Но есть плюс, которого не было у других оппонентов: этот видеорегистратор фиксирует движение вашего автомобиля." +
                                    " В приложении можно рассмотреть точный путь, скорость и сколько проехал всего. В ночном режиме основная камера вас порадует, видно хорошо номера машин из-за встроенных технологий (антитуман и HDR)." +
                                    " Цена: $66-110 (в зависимости от комплектации)"
                            },
                            new NewsData
                            {
                                Subtitle = @"Видеорегистратор VVCAR D530",
                                ImgLink =
                                    "https://gagadget.com/media/uploads/%D0%92%D0%B8%D0%B4%D0%B5%D0%BE%D1%80%D0%B5%D0%B3%D0%B8%D1%81%D1%82%D1%80%D0%B0%D1%82%D0%BE%D1%80%20VVCAR%20D530.jpg",
                                SwitchLoadImg = false,
                                ImgLoad = null,
                                Txt =
                                    "Характеристики этого гаджета можно назвать лучшими в нашем топе: 4К запись у основной камеры, у задней 1920x1080." +
                                    " Само собой, режим парковки, циклическая запись, GPS, Wi-Fi и т.д. Но качество сборки не соответствует цене: недорогой пластик." +
                                    " Номера очень хорошо читаются при любом освещении и даже ночью. Пользователи отметили недоработку в креплении: на сложной дороге камеру потряхивает из-за хилого крепления на шарнире. " +
                                    "Задняя камера разворачивает картинку на 180º по горизонтали (проще сказать, зеркалит). Управление кнопками на устройстве и через мобильное приложение." +
                                    "Цена: $50-112 (в зависимости от комплектации)"
                            }
                        }
                    },
                    new News
                    {
                        Title = "Стабильная прошивка One UI 4.0 для смартфонов Samsung задерживается",
                        NewsIntroduction =
                            "Компания Samsung уже приступила к тестированию фирменной оболочки One UI 4.0. Бета-версия программного обеспечения доступна для флагманских смартфонов Galaxy S21, а стабильную сборку придётся ждать до зимы.",
                        ImgLink = "https://gagadget.com/media/cache/40/af/40af5a68db3ea30dafc0751c3e7402b8.jpg",
                        SwitchLoadImg = false,
                        ImgLoad = null,
                        AuthorNews = "HackedNews",
                        CategoryId = GetCategoryId("Наука", context),
                        Date = DateTime.Now.ToString(),
                        ShorttextCard =
                            "Компания Samsung уже приступила к тестированию фирменной оболочки One UI 4.0. Бета-версия...",
                        ListNewsDatas = new List<NewsData>
                        {
                            new NewsData
                            {
                                Subtitle = @"Что известно",
                                ImgLink = null,
                                SwitchLoadImg = false,
                                ImgLoad = null,
                                Txt =
                                    "Прошивка основана на Android 12. Вторая сборка принесла поддержку RAM Plus и темы Material You. Она на прошлой неделе стала доступна для владельцев Samsung Galaxy S21, Galaxy S21+ и Galaxy S21 Ultra в Южной Корее, Китае, Германии, Индии, США, Польше и Великобритании. Модератор на официальном форуме Samsung заявил, что стабильная версия прошивки One UI 4.0 с операционной системой Android 12 появится только в декабре. Первыми получат обновление пользователи в Южной Корее. Когда Samsung начнёт рассылать апдейт программного обеспечения в других регионах – неизвестно. Нет информации и о том, когда One UI 4.0 станет доступна для остальных смартфонов компании. Напомним, что годом ранее One UI 3.0 вышла сразу в Южной Корее и в США."
                            }
                        }
                    },
                    new News
                    {
                        Title = "Дайджест: лидеры \"двадцатки\" хотят помочь афганцам - но не талибам",
                        ImgLink =
                            "https://ichef.bbci.co.uk/news/800/cpsprodpb/3B02/production/_121060151_gettyimages-1235719365.jpg",
                        SwitchLoadImg = false,
                        ImgLoad = null,
                        AuthorNews = "BBC",
                        CategoryId = GetCategoryId("Политика", context),
                        Date = DateTime.Now.ToString(),
                        ShorttextCard =
                            "Лидеры самых развитых экономик мира во вторник встретились на внеочередном виртуальном саммите, чтобы решить...",
                        ListNewsDatas = new List<NewsData>
                        {
                            new NewsData
                            {
                                Subtitle = "Лидеры \"двадцатки\" хотят помочь афганцам - но не талибам",
                                ImgLink = null,
                                SwitchLoadImg = false,
                                ImgLoad = null,
                                Txt =
                                    "Лидеры самых развитых экономик мира во вторник встретились на внеочередном виртуальном саммите, чтобы решить, как оказать помощь Афганистану, который стремительно близится к экономической и гуманитарной катастрофе."
                            }
                        }
                    }
                );
                context.SaveChanges();
            }
        }
    }
}