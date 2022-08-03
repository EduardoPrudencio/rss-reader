using RssReader.Domain;
using System.Net;

namespace console
{
    class Prgram
    {
        static async Task Main(string[] args)
        {
            var rssReader = new Reader();

            string? uri;

            do
            {
                Console.WriteLine("Digite a url de um rss para ter seu conteúdo exibido na tela");
                uri = Console.ReadLine();

            } while (string.IsNullOrWhiteSpace(uri));


            var list = rssReader.Parse(uri);

            // var list = rssReader.Parse("https://www.omnycontent.com/d/playlist/42233656-1562-49af-98d5-acd100df7932/a3504d75-e95a-41c5-8dda-aced013a0cb9/343561ea-888c-4641-bd55-aced013a0cd5/podcast.rss");
            // var list = rssReader.Parse("https://www.spreaker.com/show/2987926/episodes/feed").Where(i => i.PublishDate > new DateTime(2020,1,1));

            int cont = 0;
            string urlImage = "https://www.omnycontent.com/d/clips/42233656-1562-49af-98d5-acd100df7932/a3504d75-e95a-41c5-8dda-aced013a0cb9/82d8df4e-f6cd-4160-921d-aced015785e3/image.jpg?t=1615927850&size=Large";

            foreach (var item in list)
            {
                Console.WriteLine("_{0}", cont);
                WriteLabel("Título:"); WriteValueWithBreackLine($"*** {item.Title} ***");
                WriteLabel("Id:"); WriteValueWithBreackLine(item.Id);
                WriteLabel("Conteúdo:"); WriteValueWithBreackLine(item.Content);
                WriteLabel("Data de publicação:"); WriteValueWithBreackLine(item.PublishDate.ToString());
                WriteLabel("Imagem:"); WriteValueWithBreackLine(item.Image);
                WriteLabel("Autor:"); WriteValueWithBreackLine(item.Author);

                if (cont < 10) await SaveImageAsync(item.Image, $"{item.Id}.png");

                Console.WriteLine("------------------------------------------------");
                Console.WriteLine();
                cont++;
            }

            async Task SaveImageAsync(string url, string name)
            {
                await Task.Run(() =>
                {
                    if (!string.IsNullOrWhiteSpace(url))
                    {
                        using (WebClient client = new())
                        {
                            try
                            {
                                if (!Directory.Exists("images"))
                                    Directory.CreateDirectory("images");

                                client.DownloadFile(new Uri(url), $"images/{name}");
                            }
                            catch (Exception exp)
                            {
                                WriteError($"Ocorreu o seguinte erro: {exp.Message} - {exp.StackTrace}");
                            }
                        }
                    }
                });


            }

            void WriteLabel(string? text)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(text);
            }

            void WriteError(string? text)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(text);
            }

            void WriteValueWithBreackLine(string? text)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(text + "\n");
            }
        }
    }
}



