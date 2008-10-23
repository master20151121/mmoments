#define CSV
#define VERBOSE
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
        static string SAVEFILE = "songs.csv";
#if CSV
        static List<Song> ListSong;
#else
        static Database db;
#endif
        static void Main(string[] args)
        {
            Console.WriteLine("Mobile Musical Moments Server");
#if CSV
            ListSong = new List<Song>();
            loadcsv(ref ListSong);
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
                    savecsv();
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
#if VERBOSE
                Console.WriteLine("testing : {0} by {1} match with {2}", savedsong.title, savedsong.artest, c.ToString());

#endif
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
                string fingerprint;
                try
                {
                    fingerprint = calcfinger.generate(fileurl);
                }
                catch (NullReferenceException) { Console.WriteLine("file not found"); return; }

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

#if CSV
        static void loadcsv(ref List<Song> listsong)
        {   
            TextReader tr;
            try
            {
#if VERBOSE
                Console.WriteLine("opening savefile for loading");
#endif
                tr = new StreamReader(SAVEFILE);
                string line;
                string value;
                while ((line = tr.ReadLine()) != null) //break on end of file.
                {
                    Song newsong = new Song();

                    value = line.Substring(0, line.IndexOf(","));
                    line = line.Substring(line.IndexOf(",")+1);
                    newsong.artest = value;

                    value = line.Substring(0, line.IndexOf(","));
                    line = line.Substring(line.IndexOf(",")+1);
                    newsong.title = value;

                    value = line;
                    newsong.fingerprint = value;

                    bool trig = false;
                    foreach (Song tsong in listsong) //check its not already in the list.
                    {
                        if ((tsong.artest == newsong.artest) && (tsong.title == newsong.title))
                        {
                            trig = true;
                            break;
                        }
                    }
                    if (!trig) // if not in list, add.
                    {
#if VERBOSE
                        Console.WriteLine("loaded {0} by {1}", newsong.title, newsong.artest);
#endif
                        listsong.Add(newsong);
                    }
                }
                tr.Close();

            }
            catch (FileNotFoundException)
            { Console.WriteLine("FileNotFoundException, cannot load"); return; }
            catch (FileLoadException)
            {
                Console.WriteLine("cannot load, save file in use. FileLoadException");
                return;
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("NullReference, corrupt file");//or failed math.
                return;
            }
        }


        static void savecsv()
        {
            List<string> alreadysaved = new List<string>();

            TextReader tr;
            try
            {
#if VERBOSE
                Console.WriteLine("starting to read saved file");
#endif
                tr = new StreamReader(SAVEFILE);
                string line;
                while ((line = tr.ReadLine()) != null) //break on end of file.
                {
                    string value;
                    value = line.Substring(0, line.IndexOf(','));//could through error.
                    value += ",";
                    line = line.Substring(line.IndexOf(',')+1);//could through error.
                    value += line.Substring(0, line.IndexOf(','));//could through error.
                    alreadysaved.Add(value);
                }
                tr.Close();
            }
            catch (FileNotFoundException) { Console.WriteLine("old save file not found, creating new"); }//this fine.
            catch (FileLoadException)
            {
                Console.WriteLine("cannot save, save file in use. FileLoadException");
                return;
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("NullReferenc, corrupt file");//or failed math.
                return;
            }
#if VERBOSE
#if CSV
            Console.WriteLine("starting write");
#endif
#endif
            TextWriter tw;
            try
            {
#if VERBOSE
                Console.WriteLine("open file for append");
#endif
                tw = new StreamWriter(SAVEFILE, true); //append mode.

                foreach (Song ns in ListSong)
                {
                    string match = ns.artest + "," + ns.title;
                    bool trig = false;
                    foreach (string already in alreadysaved)
                    {
                        if (match == already)
                        {
                            trig = true;
                            break;
                        }
                    }
                    if (!trig)
                    {
                        tw.WriteLine("{0},{1},{2}",ns.artest, ns.title, ns.fingerprint);
                    }
                }
                tw.Close();
            }

            catch (FileLoadException)
            {
                Console.WriteLine("FileLoadException, file in use? At opening for append");
                return;
            }
        }
#endif  
    }
}
