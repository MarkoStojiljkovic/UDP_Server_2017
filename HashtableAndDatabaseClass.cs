using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using CustomTypes;

namespace UDPServer
{
    class HashtableAndDatabaseClass
    {

        static Hashtable ht = new Hashtable();
        //private static HashTableValues dummy = new HashTableValues();


        public static string FetchIPFromMac(string s) // Fetch IP from hashtable
        {
            if (!ht.ContainsKey(s))
            {
                return null;
            }
            return ((HashTableValues)ht[s]).ip;
        }

        public static string FetchLocationIDFromMac(string s) // Fetch LocationID from hashtable
        {
            if (!ht.ContainsKey(s))
            {
                return null;
            }
            return ((HashTableValues)ht[s]).locationID;
        }

        public static void AddToHashTable(string key, HashTableValues value)
        {
            if (ht.ContainsKey(key))
            {
                return; // Already have that MAC address, ignore
            } 

            ht.Add(key, value);
            return;
        }

        public static void FetchValuesFromDatabase(string connectionString)
        {

            // Command to get MAC, IP and location ID
            string command1 = "SELECT device.MAC,device.IP,location.id FROM device INNER JOIN location ON device.ID=location.DEVICE_ID ORDER BY location.id";
            //string command2 = "SELECT * FROM device";

            OracleConnection conn = new OracleConnection(connectionString);
            conn.Open();
            OracleCommand cmd = new OracleCommand(command1);

            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();

            //StringBuilder sMAC = new StringBuilder();
            //StringBuilder sIP = new StringBuilder();

            //int bufferSize = 0;

            HashTableValues htv = new HashTableValues();
            htv.valid = true;

            StringBuilder MACsb = new StringBuilder(); // Those are for testing purposes
            StringBuilder IPsb = new StringBuilder();
            StringBuilder locIDsb = new StringBuilder();

            while (dr.Read()) // Read one row
            {
                MACsb.Append(dr.GetValue(0) + Environment.NewLine);
                htv.ip = dr.GetValue(1).ToString();
                IPsb.Append(htv.ip + Environment.NewLine);
                htv.locationID = dr.GetValue(2).ToString();
                locIDsb.Append(htv.locationID + Environment.NewLine);

                // Add read values to hashtable, MAC is key, others are values
                AddToHashTable(dr.GetValue(0).ToString(), htv);
                
            }
            conn.Close(); 
        }
        
        public static void InsertMessageInDatabase(senderInfo s, string connectionString)
        {
            string tempTimeStamp = "TO_TIMESTAMP('"+ s.extractedTimestamp.year +"."+ s.extractedTimestamp.month +
                "."+ s.extractedTimestamp.day +" "+ s.extractedTimestamp.hour +
                ":"+ s.extractedTimestamp.minute +":"+ s.extractedTimestamp.second +"."+ s.extractedTimestamp.milisecond +"','yyyy.MM.dd HH24:mi.ss.ff')";
            string command1 = "INSERT INTO EVENT_HISTORY (LOCATION_ID, EVENT_ID, NAME, EVENT_COMMENT, VALUE, VALID, TIME) VALUES('"+ s.locationID +"','"+ s.address +"','"+ s.name +"','"+
               s.comment +"','"+ s.value.ToString() +"','"+ s.valid +"',"+ tempTimeStamp + ")";

            OracleConnection conn = new OracleConnection(connectionString);
            conn.Open();
            OracleCommand cmd1 = new OracleCommand(command1);

            cmd1.Connection = conn;
            cmd1.CommandType = System.Data.CommandType.Text;
            OracleDataReader dr = cmd1.ExecuteReader();

            // Second task, delete row

            string command2 = "DELETE FROM EVENT_PORTRAIT WHERE LOCATION_ID = " + s.locationID + " AND EVENT_ID = " + s.address;
            
            OracleCommand cmd2 = new OracleCommand(command2);
            cmd2.Connection = conn;
            cmd2.CommandType = System.Data.CommandType.Text;
            dr = cmd2.ExecuteReader();

            // Third task, write row, agian
            string command3 = "INSERT INTO EVENT_PORTRAIT (LOCATION_ID, EVENT_ID, NAME, EVENT_COMMENT, VALUE, VALID, TIME) VALUES('" + s.locationID + "','" + s.address + "','" + s.name + "','" +
               s.comment + "','" + s.value.ToString() + "','" + s.valid + "'," + tempTimeStamp + ")";
            
            OracleCommand cmd3 = new OracleCommand(command3);
            cmd3.Connection = conn;
            cmd3.CommandType = System.Data.CommandType.Text;
            dr = cmd3.ExecuteReader();
            
            conn.Close();
        }
        
    }
}
