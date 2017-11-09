using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using CustomTypes;

namespace UDPServer
{
    class MessageProcesserXML
    {


        public static senderInfo ProcessAlarmResponseXML(string s)
        {

            XmlDocument doc = new XmlDocument();
            senderInfo si = new senderInfo();

            si.error = false;

            // Create the XmlDocument.
            try
            {
                doc.LoadXml(s);
            }
            catch (Exception)
            {
                si.errorCode = "Parsing xml error";
                si.error = true;
                return si;    
            }
            
            
            // extract information from document
            //get alarm name
            string tagText = doc.GetElementsByTagName("name")[0].InnerText;
            si.name = tagText;
            
            // get address ????
            string tagAddress = doc.GetElementsByTagName("address")[0].InnerText;
            si.address = Convert.ToInt32(tagAddress);

            // Get value
            string tagValue = doc.GetElementsByTagName("value")[0].InnerText;
            si.value = Convert.ToInt32(tagValue);

            //Get timestamp
            string tagTime = doc.GetElementsByTagName("time")[0].InnerText;
            si.time = tagTime;
            
            // extract from header
            XmlNodeList node = doc.GetElementsByTagName("header");
            // Get sequence number
            string seqString = node[0].Attributes["seq"].Value;
            si.seq = seqString;

            //Get MAC address
            string macString = node[0].Attributes["mac"].Value;
            si.mac = macString;

            // Others
            si.ip = node[0].Attributes["ip"].Value;
            si.senderError = node[0].Attributes["error"].Value;
            si.crc = node[0].Attributes["crc"].Value;
            si.valid = doc.GetElementsByTagName("valid")[0].InnerText;
            si.comment = doc.GetElementsByTagName("comment")[0].InnerText;


            // form ACK packet (with packet stamp number)
            string packetStamp = formPacketStamp(seqString);
            si.seq = packetStamp;

            si = extractTimeStampValues(si); // Fill extracted time struct


            return si;
        }

        private static senderInfo extractTimeStampValues(senderInfo si)
        {
            // 16-12-2016 13:28:35.000
            int currentPtrStart = 0;
            int currentPtrEnd = si.time.IndexOf('-', currentPtrStart);
            si.extractedTimestamp.day = si.time.Substring(currentPtrStart, currentPtrEnd - currentPtrStart); // Read day
            // Update
            currentPtrStart = currentPtrEnd + 1;
            currentPtrEnd = si.time.IndexOf('-', currentPtrStart);
            si.extractedTimestamp.month = si.time.Substring(currentPtrStart, currentPtrEnd - currentPtrStart); // Read month
            // Update
            currentPtrStart = currentPtrEnd + 1;
            currentPtrEnd = si.time.IndexOf(' ', currentPtrStart);
            si.extractedTimestamp.year = si.time.Substring(currentPtrStart, currentPtrEnd - currentPtrStart); // Read year
            // Update
            currentPtrStart = currentPtrEnd + 1;
            currentPtrEnd = si.time.IndexOf(':', currentPtrStart);
            si.extractedTimestamp.hour = si.time.Substring(currentPtrStart, currentPtrEnd - currentPtrStart); // Read hour
            // Update
            currentPtrStart = currentPtrEnd + 1;
            currentPtrEnd = si.time.IndexOf(':', currentPtrStart);
            si.extractedTimestamp.minute = si.time.Substring(currentPtrStart, currentPtrEnd - currentPtrStart); // Read minute
            // Update
            currentPtrStart = currentPtrEnd + 1;
            currentPtrEnd = si.time.IndexOf('.', currentPtrStart);
            si.extractedTimestamp.second = si.time.Substring(currentPtrStart, currentPtrEnd - currentPtrStart); // Read second
            // Update
            currentPtrStart = currentPtrEnd + 1;
            //currentPtrEnd = si.time.IndexOf('.', currentPtrStart);
            si.extractedTimestamp.milisecond = si.time.Substring(currentPtrStart, si.time.Length - currentPtrStart); // Read milisecond

            return si;
        }

        private static string formPacketStamp(string s)
        {
            int num = Convert.ToInt32(s);

            if (num >= 100)
            {
                return s;
            }
            else if (num >= 10)
            {
                return '0' + num.ToString();
            }

            return "00" + num.ToString();
        }
        
    }
}
