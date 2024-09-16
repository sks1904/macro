using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Transactions;

var tcpListener = new TcpListener(IPAddress.Any, 8888);

try
{
    tcpListener.Start();    // запускаем сервер
    Console.WriteLine("Сервер запущен. Ожидание подключений... ");

    while (true)
    {
        // получаем подключение в виде TcpClient
        var tcpClient = await tcpListener.AcceptTcpClientAsync();

        // создаем новую задачу для обслуживания нового клиента
        Task.Run(async () => await ProcessClientAsync(tcpClient));

        // вместо задач можно использовать стандартный Thread
        // new Thread(async ()=>await ProcessClientAsync(tcpClient)).Start();
    }
}
finally
{
    tcpListener.Stop();
}

// обрабатываем клиент
async Task ProcessClientAsync(TcpClient tcpClient)
{
    var stream = tcpClient.GetStream();
    while (true)
    {
        DateTime currentTime = DateTime.Now;
        
        string translation = await GetChet(currentTime) + Environment.NewLine;
        // отправляем перевод слова из словаря
        await stream.WriteAsync(Encoding.UTF8.GetBytes(translation));
        Task.Delay(1000);
    }
    tcpClient.Close();
}

async Task<string> GetChet(DateTime dateTime)
{
    int ChetCountSymbol = 0, NeChetCountSymbol = 0;
    string Chet = "02468", NeChet = "13579";
    string Result = "";
    string Str = dateTime.ToString();
    for (int i = 0; i < Str.Length; i++)
    {
        if (Chet.Contains(Str[i]))
        {
            ChetCountSymbol++;
        }
        else if (NeChet.Contains(Str[i]))
        {
            NeChetCountSymbol++;
        }
    }
    if (ChetCountSymbol > NeChetCountSymbol)
    {
        Result = ">!";
    }else if(ChetCountSymbol < NeChetCountSymbol)
    {
        Result = "<!";
    }else if(ChetCountSymbol == NeChetCountSymbol)
    {
        Result = "=!";
    }
    return Result;
}    