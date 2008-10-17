#define VERBOSE
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

//for stuff that doesent use sql. Once where happy with this we should 
//delete the sql stuff and give this a propper name.


namespace databaseserver
{
    class nakedsong
    {
        public string title;
        public string artest;
        public string fingerprint;
    }


    class scvloader
    {
        static public List<nakedsong> nsonglist = new List<nakedsong>();   
        static string SAVEFILE = "songs.csv";
        public static void load()
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
                while ((line = tr.ReadLine()) != "") //break on end of file.
                {
                    nakedsong ns = new nakedsong();
                    value = line.Substring(0, line.IndexOf(","));
                    line = line.Substring(line.IndexOf(","));
                    ns.artest = value;

                    value = line.Substring(0, line.IndexOf(","));
                    line = line.Substring(line.IndexOf(","));
                    ns.title = value;

                    value = line.Substring(0, line.IndexOf(","));
                    line = line.Substring(line.IndexOf(","));
                    ns.fingerprint = value;
                    nsonglist.Add(ns);
                }
            }
            catch (FileNotFoundException)
            { Console.WriteLine("FileNotFoundException, cannot load"); return; }
            catch (FileLoadException)
            {
                Console.WriteLine("cannot load, save file in use. FileLoadException");
                return;
            }
            catch (OverflowException)
            {
                Console.WriteLine("OverflowException, corrupt file");//or failed math.
                return;
            }


        }
        public static void save()
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
                while ((line = tr.ReadLine()) != "") //break on end of file.
                {
                    string value;
                    value = line.Substring(0, line.IndexOf(','));//could through error.
                    value += ",";
                    line = line.Substring(line.IndexOf(','));//could through error.
                    value += line.Substring(0, line.IndexOf(','));//could through error.
                    alreadysaved.Add(value);
                }
                tr.Close();
            }
            catch (FileNotFoundException) { }//this fine.
            catch (FileLoadException)
            {
                Console.WriteLine("cannot save, save file in use. FileLoadException");
                return;
            }
            catch (OverflowException)
            {
                Console.WriteLine("OverflowException, corrupt file");//or failed math.
                return;
            }
#if VERBOSE
            Console.WriteLine("starting write");
#endif
            TextWriter tw;
            try
            {
#if VERBOSE
                Console.WriteLine("open file for append");
#endif
                tw = new StreamWriter(SAVEFILE, true); //append mode.
            }

            catch (FileLoadException)
            {
                Console.WriteLine("FileLoadException, file in use? At opening for append");
                return;
            }
            
            foreach (nakedsong ns in nsonglist)
            {
                string line;
                line = ns.artest;
                line += ",";
                line += ns.title;

                if (alreadysaved.IndexOf(line) != -1)//not in list. -1?
                {
                    line += ",";
                    line += ns.fingerprint;
                    try
                    { tw.WriteLine(line); }
                    catch (IOException)
                    {
                        Console.WriteLine("IOException, while saving {1} by {0}", ns.artest, ns.title);
                        return;
                    }
                }
                tw.Close();
            }
        }

        /* save format. comma seprated values.
         * for now ..
         * artest, title, fingerprint
         * 
         * suggest anything else that gets added goes before the fingerprint. eg.
         * artest, title, newthing, fingerprint
         */
    }
}
