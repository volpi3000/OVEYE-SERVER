using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using tcp;

namespace ovdatabase
{
  public class database
    {

        string dataSource = "OVEye.db";

        public bool renewActiveConnections(ArrayList clients)
        {
            while (true)
            {
                SQLiteConnection connection = new SQLiteConnection();

                connection.ConnectionString = "Data Source=" + dataSource;
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(connection);

                // Erstellen der Tabelle, sofern diese noch nicht existiert.
                command.CommandText = "DROP TABLE IF EXISTS clients;";
                command.ExecuteNonQuery();

                command.CommandText = "CREATE TABLE clients ( id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, name VARCHAR(100) NOT NULL, IP VARCHAR(100) NOT NULL );";
                command.ExecuteNonQuery();

                // Das Kommando basteln
                string commandString = "INSERT INTO clients (name, IP) VALUES ";
                if (clients.Count != 0)
                {

                    int i = 0;
                    foreach (Server.extended item in clients)
                    {
                        i++;
                        commandString += "('" + item.Name + "', '" + item.IP + "')";
                        if (i != clients.Count)
                        {
                            commandString += ", ";
                        }




                    }

                    commandString += ";";
                }

                // Einfügen eines Test-Datensatzes.
                command.CommandText = commandString;
                command.ExecuteNonQuery();

                // Freigabe der Ressourcen.
                command.Dispose();



                System.Threading.Thread.Sleep(5000);


            }
            return true;
        }
    }
}
