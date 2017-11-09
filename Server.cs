﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace UDPServer
{


    public class Server
    {
        public int defaultUDPSendingPort = 4023;
        public int defaultUDPListeningPort = 4023;
        public string defaultIpAddress = "192.168.2.22";
        public Thread UDPThread = null;
        public FormMain formMain = null;
        public bool Kill = false;
        public static bool noError = false;

        public Server(string s, int listeningPort, int sendingPort)
        {
            defaultUDPListeningPort = listeningPort;
            defaultUDPSendingPort = sendingPort;
            defaultIpAddress = s;
            try
            {
                noError = false;
                //Starting the UDP Server thread.
                UDPThread = new Thread(new ThreadStart(StartReceive));
                UDPThread.IsBackground = true;
                UDPThread.Start();
                Console.WriteLine("Started SampleTcpUdpServer's UDP Receiver Thread!\n");
            }
            catch (Exception e)
            {
                Console.WriteLine("An UDP Exception has occurred!" + e.ToString());
                //UDPThread.Abort();
            }
        }

        public void StartReceive()
        {
            byte[] data = new byte[1024];
            IPAddress IPSendTo = IPAddress.Parse(defaultIpAddress);

            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, defaultUDPListeningPort);
            UdpClient socket;
            try
            {
                socket = new UdpClient(ipep);
            }
            catch (SocketException)
            {
                formMain.SetStatusText("Status: ERROR");
                Console.WriteLine("Couldn't create socket :( \n");
                return; // Abort all
            }

            using (socket)
            {
                Console.WriteLine("Waiting for a client...");
                IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
                noError = true;
                formMain.SetStatusText("Status: OK");
                while (true)
                {
                    if (socket.Available > 0)
                    {
                        try
                        {
                            data = socket.Receive(ref sender);
                        }
                        catch (Exception)
                        {
                            noError = false;
                            Console.WriteLine("Couldn't create socket 2 :( \n");
                            formMain.SetStatusText("Status: ERROR");
                            return;
                        } // Crashing here
                          //if (sender.Address.Equals(IPAdressToCompare)) // Is this IP we listen to 
                          //{
                        byte[] temp_data = new byte[100];
                        int y = 0;
                        for (int i = 0; i < data.Length; i++) // Replacing \n with \r\n
                        {
                            if (data[i] == 0xA)
                            {
                                temp_data[y] = 0xD; // carriage return
                                y++;
                                temp_data[y] = 0xA; // new line
                                y++;
                            }
                            else
                            {
                                temp_data[y] = data[i];
                                y++;
                            }
                        }

                        // Process message
                        int endPtr = 0;
                        for (int i = 0; i < temp_data.Length; i++) // Find actual length
                        {
                            if (temp_data[i] == 0)
                            {
                                endPtr = i;
                                break;
                            }
                        }

                        byte[] temp_data2 = new byte[endPtr]; // Create new array with precise length

                        for (int i = 0; i < temp_data2.Length; i++)  // And fill it
                        {
                            temp_data2[i] = temp_data[i];
                        }
                        
                        // Send ACK
                        data = Encoding.ASCII.GetBytes("ACK");
                        sender.Port = defaultUDPSendingPort;
                        sender.Address = IPSendTo;
                        socket.Send(data, data.Length, sender);
                        //} // end if
                    } // end if
                    if (Kill == true)
                    {
                        if (socket.Available > 0)
                        {
                            continue;
                        }
                        return;
                    }
                    Thread.Sleep(10);
                }
            } // while
        } // StartReceive()
        


        public string ReadTo(string s, char c)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == c)
                {
                    return sb.ToString();
                }
                else
                {
                    sb.Append(s[i]);
                }
            }

            return null;
        }


    }//end of class
}//end of namespace
