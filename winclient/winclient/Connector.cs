using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Xml;
using System.Xml.Serialization;
using System.Text;

namespace winclient
{
    class Connector
    {
        private NetworkStream ns;
        private StreamReader sr;
        private StreamWriter sw;
        private TcpClient client;

        public bool Connect(string IP)
        {
            try
            {
                client = new TcpClient(IP, 345);
            }
            catch (Exception e)
            {
                return false;
            }
            if (client.Client.Connected)
            {
                ns = client.GetStream();
                sr = new StreamReader(ns);
                sw = new StreamWriter(ns);
                string challengeCode = sr.ReadLine();
                challengeCode = challengeCode.Substring(challengeCode.IndexOf(' ') + 1);
                string responseCode = "";
                for (int i = challengeCode.Length - 1; i >= 0; i--)
                {
                    responseCode += challengeCode[i];
                }
                sw.AutoFlush = true;
                sw.WriteLine(responseCode);
                if (sr.ReadLine() == "Challenge code accepted")
                {

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public SongList SendFingerprint(string fingerprint)
        {
            sw.WriteLine(fingerprint);
            if (sr.ReadLine() == "Matches")
            {
                SongList songs;
                XmlSerializer ser = new XmlSerializer(typeof(SongList));
                songs = (SongList)ser.Deserialize(sr);
                return songs;
            }
            else
            {
                return new SongList();
            }
        }
    }
}
