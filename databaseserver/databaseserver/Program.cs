#define CSV
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Data.SQLite;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;

namespace databaseserver
{
    class Program
    {
        static int WORTHYMATCH = 50; //percentage
#if CSV
        static List<Song> ListSong;
#else
        static Database db;
#endif
        static void Main(string[] args)
        {
#if CSV
            ListSong = new List<Song>();
#else       
            db = new Database();
#endif
            //SQLiteCommand com = new SQLiteCommand("CREATE TABLE `songs` (" + 
            //        "`id` INT NOT NULL ," +
            //        "`title` CHAR( 32 ) NOT NULL ," +
            //        "`artist` CHAR( 32 ) NOT NULL ," +
            //        "`fingerprint` BLOB NOT NULL ," +
            //        "PRIMARY KEY (  `id` )" +
            //        ")", sqlcon);

            Thread listener = new Thread(ListenForConnections);
            listener.Name = "ConnectionListener";
            listener.IsBackground = true;
            Console.WriteLine("Mobile Musical Moments Server");
            Console.Write("Starting");
            listener.Start();
            Console.WriteLine("\r\n" + "Q: Quit");

            Console.WriteLine("\"i\" for insert song.");

            string line;
            while (true)
            {
                line = Console.In.ReadLine().ToUpper();
                if (line == "Q")
                {
                    Environment.Exit(0);
                }
                if (line == "I")
                { addsong(); }
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
            string fingerprint = sr.ReadLine();
            SongList songs = new SongList();
#if CSV
            foreach (Song savedsong in ListSong)
            {
                int c = fingerprintcompare.strings(savedsong.fingerprint, fingerprint);
                if (c > WORTHYMATCH)
                {
                    songs.Add(savedsong);
                }
            }
#else
            DataTable dt = db.GetData("SELECT * FROM songs");
            for(int i=0; i < dt.Rows.Count; i++)
            {
                //sw.WriteLine("Matches");
                int c = fingerprintcompare.strings(System.Text.ASCIIEncoding.ASCII.GetString(dt.Rows[i]["Fingerprint"]), fingerprint); // this doesnt work.
        	if (c > 50)
         	{
         	      songs.Add(new Song(dt.Rows[i]["Title"].ToString(), dt.Rows[i]["Artist"].ToString(), c));
         	}
            }
#endif
            /*else
            {
                sw.WriteLine("No matches");
            }*/
            XmlSerializer ser = new XmlSerializer(typeof(SongList));
            ser.Serialize(sw, songs);
            sw.Close();
            sr.Close();
            s.Close();
        }

        static void addsong()
        {
            string ans = getans("Procced with adding song? y/n");
            if ((ans == "y") || (ans == "yes"))
            {
                string fileurl = getans("file url"); // might handle true cascaded directories.
                string fingerprint = calcfinger.generate(fileurl);
                string artest = getans("artest?");
                string title = getans("song title?");
                //could get more info, but not important now.
#if CSV
                Song s = new Song();
                s.artest = artest;
                s.title = title;
                s.fingerprint = fingerprint;
                ListSong.Add(s);
#else
                // isnt completed for sql.
#endif
                Console.WriteLine("song added");
            }
            else
            {
                Console.WriteLine("not adding song");
            }
        }

        static string getans(string msg)
        { // going to do this lots so..
            Console.WriteLine(msg);
            return Console.ReadLine();
        }

        
    }
}
