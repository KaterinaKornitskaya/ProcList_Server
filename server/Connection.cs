using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace server
{
    internal class MyConnection
    {
        // метод Соединение с клиентом
        public async void ConnectWithClient()
        {
            await Task.Run(() =>
            {
                try
                {
                    TcpListener listener = new TcpListener(IPAddress.Any, 49154);
                    listener.Start();
                    MessageBox.Show("Сервер запущен. Ожидание подключений... ");
                    while (true)
                    {
                        TcpClient client = listener.AcceptTcpClient();
                        MessageBox.Show($"Server say: Client connected");
                        
                        // вызываем метод Читать сообщение от клиента
                        string str = ReadMessage(client).Result;
                        MessageBox.Show($"Server say: Client send {str}");
                        // если от клиента пришло Hi , вызываем метод Отправить процессы
                        if (str == "Hi")
                        {
                            SendProc(client);
                        }
                       
                        client.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Server say: " +  ex.Message);
                }
            });
            
        }

        // метод Читаем сообщение от клиента - сдесь ловим Hi от клиента
        public async Task<string> ReadMessage(TcpClient client)
        {
            var response = new StringBuilder();
            await Task.Run(() =>
            {
                try
                {
                    NetworkStream netstream = client.GetStream();
                    byte[] arr = new byte[client.ReceiveBufferSize];

                    int len;// = netstream.Read(arr, 0, client.ReceiveBufferSize);
                    do
                    {
                        len = netstream.Read(arr, 0, arr.Length);
                        response.Append(Encoding.UTF8.GetString(arr, 0, len));
                    } while(len > 0);
                    string s = response.ToString();
                    netstream.Close();
                }
                catch (Exception ex)
                {
                    //client.Close(); // закрываем TCP-подключение и освобождаем все ресурсы, связанные с объектом TcpClient.
                    MessageBox.Show("Server say: " + ex.Message);
                }
            });
            return response.ToString();

        }
        // метод Отправить список процессов клиенту
        public async void SendProc(TcpClient client)
        {
            await Task.Run(() =>
            {
                try
                {
                    NetworkStream netstream = client.GetStream();
                    MemoryStream stream = new MemoryStream();
                    BinaryFormatter formatter = new BinaryFormatter();
                    MyList list = new MyList();
                    formatter.Serialize(stream, list.CreateList());
                    byte[] arr = stream.ToArray();
                    stream.Close();
                    netstream.Write(arr, 0, arr.Length);
                    netstream.Close();
                }
                catch (Exception ex)
                {
                    //client.Close();
                    MessageBox.Show("Server say: " + ex.Message);
                }
            });
        }
    }
}
