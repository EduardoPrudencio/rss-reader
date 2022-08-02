using RssReader.Domain;

namespace console
{
   class Prgram
   {
        static void Main(string[] args) 
        {
            var rssReader = new Reader();

            string? uri;

            do {
                Console.WriteLine("Digite a url de um rss para ter seu conteúdo exibido na tela");
                uri = Console.ReadLine();

            } while (string.IsNullOrWhiteSpace(uri));


            var list = rssReader.Parse(uri);

            // var list = rssReader.Parse("https://www.omnycontent.com/d/playlist/42233656-1562-49af-98d5-acd100df7932/a3504d75-e95a-41c5-8dda-aced013a0cb9/343561ea-888c-4641-bd55-aced013a0cd5/podcast.rss");
            // var list = rssReader.Parse("https://www.spreaker.com/show/2987926/episodes/feed").Where(i => i.PublishDate > new DateTime(2020,1,1));

            int cont = 0;

            foreach (var item in list)
            {
                Console.WriteLine("_{0}", cont);
                WriteLabel("Título:"); WriteValueWithBreackLine($"*** {item.Title} ***");
                WriteLabel("Id:"); WriteValueWithBreackLine(item.Id);
                WriteLabel("Conteúdo:"); WriteValueWithBreackLine(item.Content);
                WriteLabel("Data de publicação:"); WriteValueWithBreackLine(item.PublishDate.ToString());
                WriteLabel("Imagem:"); WriteValueWithBreackLine(item.Image);
                WriteLabel("Autor:"); WriteValueWithBreackLine(item.Author);

                Console.WriteLine("------------------------------------------------");
                Console.WriteLine();
                cont++;
            }
            void WriteLabel(string? text)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(text);
            }

            void WriteValue(string? text)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
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



