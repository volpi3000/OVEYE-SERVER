using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using tcp;
using ovdatabase;




namespace TcpServer_LV
{
    class Program
    {
        static void Main(string[] args)
        {
           
            Console.WriteLine("Erstelle Server");
            
            Server x = new Server();
            database y = new database();
            Console.WriteLine("Erstellt");
            while (true)
            {
                Console.WriteLine("\nGeben sie die Nachricht für alle ein:");
                string g = Console.ReadLine();
                x.NachrichtAnAlle(x.clientList, g);
                y.renewActiveConnections(x.clientList);
                
            }
            
            
        }

        


       
    }
}
