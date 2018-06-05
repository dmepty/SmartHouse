using System;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Windows;
using Newtonsoft.Json;

namespace SmartHouse
{
    public static class BackClient
    {
        private const string HOST = "http://h92761ae.beget.tech/";
        private static readonly Random Random = new Random();
        private static readonly HttpClient Client = new HttpClient();

        static BackClient()
        {
            Client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");
            Client.DefaultRequestHeaders.ExpectContinue = false;
        }

        public static bool SendRequest(string query)
        {
            query += "&random=" + Random.Next(int.MinValue, int.MaxValue);

            try
            {
                using (var webClient = new WebClient { Encoding = Encoding.UTF8 })
                {
                    webClient.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0");
                    webClient.DownloadString(query);
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка! " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        public static bool SendPostRequest(string query, string data)
        {
            query += "?random=" + Random.Next(int.MinValue, int.MaxValue);

            try
            {
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                Client.PostAsync(query, content).Wait();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка! " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        public static T GetEntities<T>(string query)
        {
            using (var webClient = new WebClient { Encoding = Encoding.UTF8 })
            {
                webClient.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0");


                var uri = new UriBuilder(HOST)
                {
                    Path = query,
                    Query = "random=" + Random.Next(int.MinValue, int.MaxValue)
                }.Uri;


                var sourceCode = webClient.DownloadString(uri);

                return JsonConvert.DeserializeObject<T>(sourceCode);
            }
        }
    }
}
