using System.IO.Ports;


namespace CanSatGUI
{
    class Utils
    {
        public static string[] ParsePacket(string packet)
        {
            // "$$ZSM-Sat,997.27,1018,END$$$"
            string[] list = packet.Split(';');
            return list;
        }
        
        public static SerialPort InitSerialPort(string defaultPortName)
        {
            // init serial port
            SerialPort port = new SerialPort();
            port.PortName = defaultPortName;
            port.BaudRate = 9600;
            port.Parity = Parity.None;
            port.DataBits = 8;
            port.StopBits = StopBits.One;
            port.Open();
            port.NewLine = "\n";
            return port;
        }
    }
}


