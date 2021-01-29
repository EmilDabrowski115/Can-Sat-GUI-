using System.IO.Ports;
using System.Text.RegularExpressions;
using System;

namespace CanSatGUI
{
    class Utils
    {
        static Regex valid_packet_regex = new Regex(@"^([0-9\-;:\.]+\s+$)");

        public static string[] ParsePacket(string packet)
        {
            MatchCollection matches = valid_packet_regex.Matches(packet);
            if (matches.Count == 0) {
                Console.Write("skipping packet: invalid packet");
                return null;
            }
            //Console.Write("matched");
            /// "$$ZSM-Sat,997.27,1018,END$$$"
            string[] list = packet.Split(';');
            return list;
        }
        
        public static SerialPort InitSerialPort(string defaultPortName)
        {

            // init serial port
            //System.IO.IOException: 

            SerialPort port = new SerialPort();
            port.PortName = defaultPortName;
            port.BaudRate = 9600;
            port.Parity = Parity.None;
            port.DataBits = 8;
            port.StopBits = StopBits.One;
            port.NewLine = "\n";
            try
            {
                port.Open();
            }
            catch (System.IO.IOException)
            {
                // DataStream.AppendText("");
            }
            return port;
        }

        public static double WindSpeed(double hall)
        {
            double radius = 0.018; // in meters
            return Math.PI * radius * hall;
        }

        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssfff");
        }
    }
}


