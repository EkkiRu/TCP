using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPServer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //записываем ip аддресс и порт
            var ipAddress = IPAddress.Parse("127.0.0.1");
            var port = 62227;

            //создаем TCP Listener
            var listemer = new TcpListener(ipAddress, port);

            try
            {
                //Начинаем слушать входящие соединения
                listemer.Start();

                while (true)
                {
                    var incomingConnection = await listemer.AcceptTcpClientAsync();
                  _ = Task.Run(() =>
                    {
                        var networkStream = incomingConnection.GetStream();
                        var stringBuilder = new StringBuilder();
                        do
                        {
                            var buffer = new byte[512];
                            networkStream.Read(buffer, 0, buffer.Length);
                            stringBuilder.Append(Encoding.UTF8.GetString(buffer));
                        } while (networkStream.DataAvailable);

                        Console.WriteLine($"Сообщение - {stringBuilder.ToString()}");

                        networkStream.Close();
                        incomingConnection.Close();
                    });
                }

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            Console.ReadLine();
        }
    }
}
