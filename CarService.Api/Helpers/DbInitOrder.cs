
using System;
using System.Linq;
using System.Text;
using CarService.DbAccess.DAL;
using CarService.DbAccess.EF;
using CarService.DbAccess.Entities;

namespace CarService.Api.Helpers
{
    public static class InitOrderTable
    {
        public static void InitializeOrder(CarServiceDbContext context)
        {
            Random random = new Random();
            const string ABC = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            int ordersCount = 50;
            int[] autoRiaId = { 21352854, 18660881, 20454797, 20709355, 21280512, 21366060, 19454827, 21148041, 21397834, 21396045 };
            string[] markName = { "Volkswagen", "Mitsubishi", "Nissan", "Infiniti", "Suzuki", "Opel", "Kia", "Mercedes-Benz", "Audi" };
            string[] modelName = { "Touareg", "Lancer X", "Qashqai", "Q30", "Vitara", "Insignia", "Sportage", "E-Class", "Q5" };
            string[] city = { "Kyiv", "Odessa", "Lviv", "Kharkiv", "Poltava", "Pryluky", "Stryi", "Zhmerynka" };
            string[] photoLink = {
                                  "https://cdn.riastatic.com/photosnew/auto/photo/audi_q5__208448878sx.jpg",
                                  "https://cdn.riastatic.com/photosnew/auto/photo/mercedes-benz_e-class__189373766sx.jpg",
                                  "https://cdn.riastatic.com/photosnew/auto/photo/kia_sportage__195583415sx.jpg",
                                  "https://cdn.riastatic.com/photosnew/auto/photo/opel_insignia__207299768sx.jpg",
                                  "https://cdn.riastatic.com/photosnew/auto/photo/opel_corsa__207330356sx.jpg",
                                  "https://cdn.riastatic.com/photosnew/auto/photo/suzuki_vitara__208651532sx.jpg",
                                  "https://cdn.riastatic.com/photosnew/auto/photo/infiniti_q30__206379767sx.jpg",
                                  "https://cdn.riastatic.com/photosnew/auto/photo/nissan_qashqai__205340151sx.jpg",
                                  "https://cdn.riastatic.com/photosnew/auto/photo/mitsubishi_lancer-x__209154634sx.jpg",
                                  "https://cdn.riastatic.com/photosnew/auto/photo/volkswagen_touareg__209125093sx.jpg"
                                };

            if (!context.Orders.Any())
            {
                for (int i = 0; i < ordersCount; i++)
                {
                    var auto = new Auto
                    {
                        AutoRiaId = random.Next(1000000, 2000000),
                        MarkName = markName[random.Next(0, markName.Length)],
                        ModelName = modelName[random.Next(0, modelName.Length)],
                        Year = random.Next(2000, 2019),
                        City = city[random.Next(0, city.Length)],
                        PhotoLink = photoLink[random.Next(0, photoLink.Length)]
                    };
                    context.Autos.Add(auto);

                    var order = new Order
                    {
                        CustomerId = random.Next(5, 8),
                        AutoId = auto.Id,
                        Description = ABC.RandomString(random.Next(8, 20)),
                        Date = DateTime.Now.ToUniversalTime()
                    };
                    context.Orders.Add(order);

                    context.SaveChanges();
                }
            }
        }
        public static string RandomString(this string chars, int length = 10)
        {
            var randomString = new StringBuilder();
            var random = new Random();

            for (int i = 0; i < length; i++)
                randomString.Append(chars[random.Next(chars.Length)]);

            return randomString.ToString();
        }
    }
}