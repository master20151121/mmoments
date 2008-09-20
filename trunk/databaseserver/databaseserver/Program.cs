using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;

namespace databaseserver
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread listener = new Thread(ListenForConnections);
            listener.Name = "ConnectionListener";
            listener.IsBackground = true;
            Console.WriteLine("Mobile Musical Moments Server");
            Console.Write("Starting");
            //listener.Start();
            if (listener.ThreadState == ThreadState.Running)
            {
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
            else
            {
                throw new Exception("Listener could not be started");
            }
            Environment.Exit(0);
        }
        static void ListenForConnections()
        {
            TcpListener ss = new TcpListener(345);
            ss.Start();
            Random r = new Random();
            Socket s;
            while (true)
            {
                s = ss.AcceptSocket();
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
            sw.WriteLine("Hello I am " + Thread.CurrentThread.Name);
            Console.WriteLine(sr.ReadLine());
            s.Close();
        }
    }
}
