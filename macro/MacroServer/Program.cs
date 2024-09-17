using System.Net;
using System.Net.Sockets;

ServerObject server = new ServerObject();// создаем сервер
await server.ListenAsync(); // запускаем сервер

public class ServerObject
{
    TcpListener tcpListener = new TcpListener(IPAddress.Any, 8888); // сервер для прослушивания
    public async Task ListenAsync()
    {
        try
        {
            tcpListener.Start();
            Console.WriteLine("Сервер запущен. Ожидание подключений...");
            while (true)
            {
                TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();
                ClientObject clientObject = new ClientObject(tcpClient);//, this);
                Task.Run(clientObject.ProcessAsync);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
public class ClientObject
{
    public StreamWriter Writer { get; set; }
    TcpClient client;
    public ClientObject(TcpClient tcpClient)
    {
        client = tcpClient;
        // получаем NetworkStream для взаимодействия с сервером
        var stream = client.GetStream();
        Writer = new StreamWriter(stream);
    }

    public async Task ProcessAsync()
    {
        string message="", OldMessage = "";
        while (true)
        {
            try
            {
                DateTime date = DateTime.Now;
                message = date.ToString();
                if (OldMessage != message)
                {
                    await Writer.WriteLineAsync(ClientObjectHelpers.StrokaZadaniya(date)); //передача данных
                    await Writer.FlushAsync();
                    OldMessage = message;
                }
            }
            catch
            {
                break;
            }
        }
    }
}
