using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Data.SQLite;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;

namespace databaseserver
{
    class Program
    {
        static void Main(string[] args)
        {
            SQLiteConnection sqlcon = new SQLiteConnection(@"data source=I:\COMP134Project\mmoments\databaseserver\songdatabase.mmd");
            sqlcon.Open();
            //SQLiteCommand com = new SQLiteCommand("CREATE TABLE `songs` (" + 
            //        "`id` INT NOT NULL ," +
            //        "`title` CHAR( 32 ) NOT NULL ," +
            //        "`artist` CHAR( 32 ) NOT NULL ," +
            //        "`fingerprint` BLOB NOT NULL ," +
            //        "PRIMARY KEY (  `id` )" +
            //        ")", sqlcon);
            SQLiteCommand com = new SQLiteCommand("SELECT * FROM songs", sqlcon);
            com.Prepare();
            com.ExecuteNonQuery();
            Console.WriteLine(sqlcon.ServerVersion.ToString());

            Thread listener = new Thread(ListenForConnections);
            listener.Name = "ConnectionListener";
            listener.IsBackground = true;
            Console.WriteLine("Mobile Musical Moments Server");
            Console.Write("Starting");
            listener.Start();
            Console.WriteLine("\r\n" + "Q: Quit");
            string line;
            while (true)
            {
                line = Console.In.ReadLine().ToUpper();
                if (line == "Q")
                {
                    Environment.Exit(0);
                }
            }
        }
        static void ListenForConnections()
        {
            TcpListener ss = new TcpListener(345);
            ss.Start();
            Console.WriteLine("Listening for new connections");
            Random r = new Random();
            Socket s;
            while (true)
            {
                s = ss.AcceptSocket();
                Console.WriteLine("New connection received");
                Thread conn = new Thread(new ParameterizedThreadStart(Connection));
                conn.Name = "mmomentsconnection" + conn.ManagedThreadId;
                conn.IsBackground = true;
                conn.Start(s);
            }
        }
        static void Connection(object s_)
        {
            Socket s = (Socket)s_;
            NetworkStream ns = new NetworkStream(s);
            BinaryWriter bw = new BinaryWriter(ns);
            BinaryReader br = new BinaryReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            StreamReader sr = new StreamReader(ns);
            sw.AutoFlush = true;
            Random r = new Random();
            string challengeCode = r.Next(9999999).ToString("X");
            string responseCode = "";
            for (int i = challengeCode.Length - 1; i >= 0; i--)
            {
                responseCode += challengeCode[i];
            }
            sw.WriteLine("Challenge: " + challengeCode);
            string clientResponse = sr.ReadLine();
            if (clientResponse != responseCode)
            {
                sw.WriteLine("Challenge failed. Connection terminated");
                s.Close();
                return;
            }
            else
            {
                sw.WriteLine("Challenge code accepted");
            }
            SongList songs = new SongList();
            songs.Add(new Song("All Summer Long", "Kid Rock", 98));
            XmlSerializer ser = new XmlSerializer(typeof(SongList));
            ser.Serialize(sw, songs);
            //sw.WriteLine("Song: " + songs[0].Title);
            //sw.WriteLine("Artist: " + songs[0].Artist);
            //sw.WriteLine("Match: " + songs[0].Match);
            sw.WriteLine(".");
            s.Close();
        }
    }
}
