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
            listener.Start();
            Console.In.ReadLine();
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
            StreamReader sr = new StreamReader(ns);
            bw.Write("Hello I am " + Thread.CurrentThread.Name + '\n');
            bw.Flush();
            System.Diagnostics.Debug.WriteLine(sr.ReadLine());
        }
    }
}
