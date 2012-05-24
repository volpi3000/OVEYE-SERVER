using System;
using System.Windows.Forms;
using System.Drawing;
using tcp;
using ovdatabase;
using System.Threading;
//*****************************************************************************
static class MyNotifyIconApplication
{
    private static NotifyIcon notico;

    //==========================================================================
    public static void Main(string[] astrArg)
    {
        ContextMenu cm;
        MenuItem miCurr;
        int iIndex = 0;
        string status = "running";

        // Kontextmenü erzeugen
        cm = new ContextMenu();

        // Kontextmenüeinträge erzeugen
        miCurr = new MenuItem();
        miCurr.Index = iIndex++;
        miCurr.Text = "&Aktion 1";           // Eigenen Text einsetzen
        miCurr.Click += new System.EventHandler(Action1Click);
        cm.MenuItems.Add(miCurr);

        // Kontextmenüeinträge erzeugen
        miCurr = new MenuItem();
        miCurr.Index = iIndex++;
        miCurr.Text = "&Beenden";
        miCurr.Click += new System.EventHandler(ExitClick);
        cm.MenuItems.Add(miCurr);

        // NotifyIcon selbst erzeugen
        notico = new NotifyIcon();
        notico.Icon = new Icon("git.ico"); // Eigenes Icon einsetzen
        notico.Text = "OVEye-Server | "+ status;   // Eigenen Text einsetzen
        notico.Visible = true;
        notico.ContextMenu = cm;
        



        Console.WriteLine("Erstelle Server");

        Server x = new Server();
        database y = new database();
        //Erstellt
        Thread prüfen = new Thread(delegate() { y.renewActiveConnections(x.clientList); });

        // Ohne Appplication.Run geht es nicht
        Application.Run();

            

      



    }

    //==========================================================================
    private static void ExitClick(Object sender, EventArgs e)
    {
        notico.Dispose();
        Application.Exit();
    }

    //==========================================================================
    private static void Action1Click(Object sender, EventArgs e)
    {
        // nur als Beispiel:
        // new MyForm ().Show ();
    }

<<<<<<< HEAD
    //==========================================================================
    private static void NotifyIconDoubleClick(Object sender, EventArgs e)
    {
        // Was immer du willst
    }
=======
 
    
    
>>>>>>> Working
}