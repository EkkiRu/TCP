using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var random = new Random();
            var name = random.Next(500, 2500).ToString();
            while (true)
            {
                Console.WriteLine("Введите сообщение:");

                var text = $"name:{name} - {Console.ReadLine()}";
                //записываем ip аддресс и порт
                var ipAddress = IPAddress.Parse("127.0.0.1");
                var port = 62227;

                //создаем TCP Listener
                var client = new TcpClient();

                try
                {
                    await client.ConnectAsync(ipAddress, port);

                    var stream = client.GetStream();
                    var data = Encoding.UTF8.GetBytes(text);
                    await stream.WriteAsync(data, 0, data.Length);

                    stream.Close();
                    client.Close();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }
    }
}
