using System.Net.Sockets;
using System.Text;
using System.Threading;
namespace MacroClient
{
    class MacroClient
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите адрес сервера");
            string Adres = Console.ReadLine();
            Console.WriteLine($"Подключение к {Adres}");
            Thread thread1 = new Thread(ThreadGetMessageFromServer);
            thread1.Start(Adres);
        }
        static void ThreadGetMessageFromServer(object Adres)
        {
            try
            {
                using TcpClient tcpClient = new TcpClient();
                string adres = Adres.ToString();
                tcpClient.Connect(adres, 8888);
                var stream = tcpClient.GetStream();
                // буфер для входящих данных
                var response = new List<byte>();
                int bytesRead = 255; // для считывания байтов из потока
                while (true)
                {
                    // считываем данные до конечного символа
                    while ((bytesRead = stream.ReadByte()) != '\n')
                    {
                        // добавляем в буфер
                        response.Add((byte)bytesRead);
                    }
                    var translation = Encoding.UTF8.GetString(response.ToArray());
                    Console.WriteLine($"{translation}");
                    response.Clear();
                }
            }
            catch
            {
                Console.WriteLine($"Подключение к {Adres} не доступно. Проверьте параметры подключения и достуность сервера по порту 8888 ");
                Console.ReadKey();
            }
        }
    }
}