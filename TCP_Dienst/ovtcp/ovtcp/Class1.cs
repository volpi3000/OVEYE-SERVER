using System;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Collections;
using System.IO;
namespace tcp
{
    public class Server
    {
        private TcpListener tcpListener;
        private Thread listenThread;
        private Thread scanner;
        public ArrayList clientList = new ArrayList();

        public Server()
        {
            this.tcpListener = new TcpListener(IPAddress.Any, 3000);
            this.listenThread = new Thread(new ThreadStart(ListenForClients));
            this.listenThread.Start();
            this.scanner = new Thread(new ThreadStart(print));
            this.AlleÜberprüfen();

        }

        public struct extended
        {
            public TcpClient tcp;
            public string IP;
            public string Name;
        }

        private void ListenForClients()
        {
            this.tcpListener.Start();

            while (true)
            {
                lock (clientList)
                {
                    //blocks until a client has connected to the server
                    TcpClient client = this.tcpListener.AcceptTcpClient();
                    extended y = new extended();

                    //Packe alle ins struct und dann in eine Liste zu späteren vewendung

                    y.IP = client.Client.RemoteEndPoint.ToString();
                    y.tcp = client;
                    clientList.Add(y);

                    //create a thread to handle communication
                    //with connected client
                    Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                    clientThread.Start(client);
                }
            }
        }

        private void HandleClientComm(object client)
        {
            TcpClient tcpClient = (TcpClient)client;
            NetworkStream clientStream = tcpClient.GetStream();

            byte[] message = new byte[4096];
            int bytesRead;

            while (true)
            {
                bytesRead = 0;

                try
                {
                    //blocks until a client sends a message
                    bytesRead = clientStream.Read(message, 0, 4096);
                }
                catch
                {
                    //a socket error has occured
                    break;
                }

                if (bytesRead == 0)
                {
                    //the client has disconnected from the server
                    break;
                }

                //message has successfully been received
                ASCIIEncoding encoder = new ASCIIEncoding();

                extended y = new extended();

                y = ((extended)clientList[clientList.Count - 1]);

                y.Name = encoder.GetString(message, 0, bytesRead);

                clientList.RemoveAt(clientList.Count - 1);
                clientList.Add(y);
                
                //Console.WriteLine("Client: " + encoder.GetString(message, 0, bytesRead));
                
            }

            tcpClient.Close();
        }

        void print()
        {
            scan x = new scan();
            while (true)
            {
                System.Threading.Thread.Sleep(5000);
                StreamWriter w = new StreamWriter("online.txt");
                w.Write(x.online(clientList));
                w.Close();

            }
        }

        public void NachrichtAnAlle(ArrayList b, string Nachricht)
        {
            //dringend bearbeiten

            while (true)
            {
                try
                {
                    foreach (Server.extended y in b)
                    {
                        try
                        {
                            NetworkStream clientStream = y.tcp.GetStream();
                            ASCIIEncoding encoder = new ASCIIEncoding();
                            byte[] buffer = encoder.GetBytes(Nachricht);

                            clientStream.Write(buffer, 0, buffer.Length);
                            clientStream.Flush();


                        }
                        catch
                        {
                            b.Remove(y);
                        }

                    }
                    break;
                }
                catch
                {
                }
            }


        }

        private void AlleÜberprüfen()
        {
            Thread prüfen = new Thread(tuwas);

        }

        private void tuwas()
        {
            while (true)
            {
                try
                {
                    foreach (Server.extended y in clientList)
                    {
                        try
                        {
                            NetworkStream clientStream = y.tcp.GetStream();
                            ASCIIEncoding encoder = new ASCIIEncoding();
                            byte[] buffer = encoder.GetBytes("test");

                            clientStream.Write(buffer, 0, buffer.Length);
                            clientStream.Flush();


                        }
                        catch
                        {
                            clientList.Remove(y);
                        }

                    }
                    System.Threading.Thread.Sleep(5000);
                }
                catch
                {
                }
            }
        }
    }
    class scan
    {
        public string online(ArrayList x)
        {
            string on = string.Empty;
            foreach (Server.extended y in x)
            {
                try
                {
                    NetworkStream clientStream = y.tcp.GetStream();
                    ASCIIEncoding encoder = new ASCIIEncoding();
                    byte[] buffer = encoder.GetBytes("check");

                    clientStream.Write(buffer, 0, buffer.Length);
                    clientStream.Flush();
                    on += y.IP + "\n";


                }
                catch
                {

                }

            }
            return on;
        }


    }
}
