using RssReader.Domain;

namespace RssReader.Tests
{
    [TestClass]
    public class Ao_Ler_Arquivos_Sem_Todos_Os_Parametros
    {
        [TestMethod]
        public void Deve_Carregar_Todos_Os_Itens()
        {
            string path = Directory.GetCurrentDirectory().Split("bin")[0];
            string filePath = $"{path}feed_test_two.xml";

            var rssReader = new Reader();
            var list = rssReader.Parse(filePath);

            Assert.IsTrue(list.Count == 2);
        }
    }
}
