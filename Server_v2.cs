#define CONSOLE_DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CustomTypes;



namespace UDPServer
{
    public class Server_v2
    {
        public static int defaultUDPSendingPort = 4023;
        public static int defaultUDPListeningPort = 4023;
        public static string defaultIpAddress = "192.168.2.254";
        public static Thread UDPThread = null;
        public static FormMain formMain = null;
        public static bool Kill = false;
        public static bool noError = false;
        public static bool isActive = false;

        //private static string connectionString = "DATA SOURCE=192.168.2.98:1521/M2CDatabase;PERSIST SECURITY INFO=True;USER ID=NTPM;PASSWORD=NeticoPassword123;";

        static IPAddress IPSendTo;
        static IPEndPoint ipep;
        static UdpClient socket;
        static IPEndPoint sender;

        public Server_v2(int listeningPort, int sendingPort)
        {
            isActive = true;
            defaultUDPListeningPort = listeningPort;
            defaultUDPSendingPort = sendingPort;

            //defaultIpAddress = defaultIpAddress.Trim();
            IPSendTo = IPAddress.Parse(defaultIpAddress);
            ipep = new IPEndPoint(IPAddress.Any, defaultUDPListeningPort);
#if CONSOLE_DEBUG
            Console.WriteLine("Phase 1 \n");
            FormCustomConsole.AddText("Phase 1" + Environment.NewLine);
#endif
            //ipep = new IPEndPoint(IPSendTo, defaultUDPListeningPort);
            socket = new UdpClient(ipep);
#if CONSOLE_DEBUG
            Console.WriteLine("Phase 2 \n");
            FormCustomConsole.AddText("Phase 2" + Environment.NewLine);
#endif
            sender = new IPEndPoint(IPSendTo, defaultUDPSendingPort);
#if CONSOLE_DEBUG
            Console.WriteLine("Phase 3 \n");
            FormCustomConsole.AddText("Phase 3" + Environment.NewLine);
#endif
            UDPThread = new Thread(new ThreadStart(StartReceive));
            UDPThread.IsBackground = true;
            UDPThread.Start();

        } // End constructor

        public void StartReceive()
        {
            try
            {
                using (socket)
                {
                    formMain.SetStatusText("Status: Running");
                    formMain.ChangeButtonState(true);
                    while (true)
                    {
                        byte[] data = new byte[1024];

                        if (socket.Available > 0)
                        {
                            IPEndPoint tempasd = new IPEndPoint(IPAddress.Any, 0);
                            data = socket.Receive(ref tempasd);
                            if (data.GetLength(0) == 4) // Is this get status response
                            {
                                MessageProcesser.ProcessStatusResponse(data);
                            }
                            else // For now it is only received Alarm 
                            {
                                string xmlData = System.Text.Encoding.Default.GetString(data);
                                senderInfo si = new senderInfo();

                                // Assign default value
                                si.errorCode = "none";

                                si = MessageProcesserXML.ProcessAlarmResponseXML(xmlData);
                                if (si.error)
                                {
                                    continue; // Something went wrong, abort
                                }

                                si.trueIPAddress = HashtableAndDatabaseClass.FetchIPFromMac(si.mac);
                                if (si.trueIPAddress == null)
                                {
                                    continue; // Hashtable doesn't countain that MAC, abort everything (writing in log and display too)
                                }
                                si.locationID = HashtableAndDatabaseClass.FetchLocationIDFromMac(si.mac);
                                // we know that we have that key, so dont need to check

                                if (!formMain.GetDebugStatus())
                                {   // If debug checkbox is not checked try to write to database
                                    try
                                    {
                                        HashtableAndDatabaseClass.InsertMessageInDatabase(si, Storage.ReadConnectionString());
                                        formMain.SetDBStatusText("DB Status: OK");
                                    }
                                    catch (Exception)
                                    {
                                        //WriteOnDisplayAndStorage(si);
                                        WriteOnDisplayAndStorageDBDown(si);
                                        formMain.SetDBStatusText("DB Status: DB is down"); // Database is down, update status
                                        continue;
                                    }

                                }
                                // Write log and to display
                                WriteOnDisplayAndStorage(si); // Will use information stored on si structure


                                // Form responce
                                byte[] finalMessage = Encoding.ASCII.GetBytes("ACK " + si.seq);

                                si.trueIPAddress = si.trueIPAddress.Trim(); // Some people use whitespaces infront of IP  :(
                                try
                                {
                                    sender = new IPEndPoint(IPAddress.Parse(si.trueIPAddress), defaultUDPSendingPort);
                                    socket.Send(finalMessage, finalMessage.Length, sender);
                                }
                                catch
                                {
                                    Console.WriteLine("IP from database is corrupted \n");
                                    FormCustomConsole.AddText("IP from database is corrupted" + Environment.NewLine);
                                }
                            } // End else from get data length

                        } // End  if (socket.Available > 0)

                        if (Kill == true)
                        {
                            if (socket.Available > 0)
                            {
                                Console.WriteLine("More data needs to be read first \n");
                                continue;
                            }
                            Kill = false;
                            Console.WriteLine("Phase Exit \n");
                            FormCustomConsole.AddText("Phase Exit" + Environment.NewLine);
                            formMain.SetStatusText("Status: Stopped");
                            isActive = false;
                            return;
                        }
                        Thread.Sleep(10);
                    } // While
                } // End using(socket)
            } // End try
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine("Something went wrong with server \n");
                FormCustomConsole.AddText("Something went wrong with server" + Environment.NewLine);
                formMain.SetStatusText("Status: Error");
                isActive = false;
            }

        } // StartReceive





        public void SendCommand(string s) // Send command to IP from last arrived event
        {
            byte[] data = Encoding.ASCII.GetBytes(s);
            try
            {
                socket.Send(data, data.Length, sender);
                Console.WriteLine("Sending command: {0}  = success \n", s);
            }
            catch (Exception)
            {

                Console.WriteLine("Sending command: {0}  = fail \n", s);
            }
        }

        private void WriteOnDisplayAndStorage(senderInfo si)
        {
            string MAC = si.mac;
            string alarmName = si.name;
            string status = si.value.ToString();
            string timeStamp = si.time;
            string packetNum = si.seq;

            string messageToAppend = MAC + " " + si.trueIPAddress + " " + alarmName + " " + status + " " + timeStamp + " " + packetNum + Environment.NewLine;
            formMain.SetText(messageToAppend);            
            Storage.UpdateFolderTimeStamp(); // Update file, in case it is new day
            string messageToAppendTextFile = MAC + " " + si.trueIPAddress + " " + alarmName + " " + status + " " + timeStamp + " " + packetNum;
            Storage.AppendTextToFile(messageToAppendTextFile);

        }

        private void WriteOnDisplayAndStorageDBDown(senderInfo si)
        {
            string MAC = si.mac;
            string alarmName = si.name;
            string status = si.value.ToString();
            string timeStamp = si.time;
            string packetNum = si.seq;

            string messageToAppend = MAC + " " + si.trueIPAddress + " " + alarmName + " " + status + " " + timeStamp + " " + packetNum + " DB is down\r\n";
            formMain.SetText(messageToAppend);
            Storage.UpdateFolderTimeStamp(); // Update file, in case it is new day
            string messageToAppendTextfile = MAC + " " + si.trueIPAddress + " " + alarmName + " " + status + " " + timeStamp + " " + packetNum + " DB is down";
            Storage.AppendTextToFile(messageToAppendTextfile);

        }

        public void updateSender(string ip) // This is called to update sender for manualy added devices so you can send commands and not wait for them to send first
        {
            try
            {
                IPAddress tempIPSendTo = IPAddress.Parse(ip);
                sender = new IPEndPoint(tempIPSendTo, defaultUDPSendingPort); // Update current ip with default port

            }
            catch (Exception)
            {
                return;
            }
        }

    }
}
