using System;
using System.Windows.Forms;
using System.IO.Ports;
using GMap.NET; 
using GMap.NET.MapProviders;
using System.Diagnostics;


/*possible missions
 * magnetosphere reading
wind patterns
atmospheric data
soil ph level
soil composition
any signs of life
size/mass of the planet
microscopic data
if theirs water in the area
if the planet is habitable
weatherman data type shit
*/


namespace CanSatGUI
{
    public partial class Form1 : Form
    {
        string rxString;
        Stopwatch timer = new Stopwatch();

        public Form1()
        {
            // init window
            InitializeComponent();

            // init serial port
            SerialPort1 = new SerialPort();
            SerialPort1.PortName = "COM4";
            SerialPort1.BaudRate = 9600;
            SerialPort1.Parity = Parity.None;
            SerialPort1.DataBits = 8;
            SerialPort1.StopBits = StopBits.One;
            SerialPort1.DataReceived += myPort_DataReceived;
            SerialPort1.NewLine = "\n";
            try
            {
                SerialPort1.Open();
            }
            catch(System.IO.IOException)
            {
                DataStream.AppendText("Nie wykryto urządzenia w porcie " + SerialPort1.PortName);
            }

            // init timer
            timer.Start();

            // init map
            GMapProviders.GoogleMap.ApiKey = @"AIzaSyAZouhXULQgPGPckADOmiHqfCc_YvD5QzQ";
            map.DragButton = MouseButtons.Left;
            map.MapProvider = GMapProviders.GoogleMap;
            // map.Position = new PointLatLng(20, 30);
            map.MinZoom = 0;
            map.MaxZoom = 25;
            map.Zoom = 10;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // $$ZSM-Sat;24;980;25;74;50.71;14.01;2057;END$$
        // start; packet_num; pressure; temp; humidity; lat; long; height; end
        public void UpdateGUI(string packetString)
        {
            Console.WriteLine(packetString);
            DataStream.AppendText(packetString);

            TimeSpan elapsed = timer.Elapsed;
            double secondsElapsed = Convert.ToInt32(elapsed.TotalSeconds);

            string[] packetElems = Utils.ParsePacket(packetString);
            
            // pressure
            psrtxt.Text = packetElems[2];
            Utils.UpdateChart(chart2, secondsElapsed, Convert.ToDouble(packetElems[2]), maxChartWidth: 7);

            // temperature
            tmptxt.Text = packetElems[3];
            Utils.UpdateChart(chart1, secondsElapsed, Convert.ToDouble(packetElems[3]), maxChartWidth: 7);

            // humidity
            // humtxt.Text = packetElems[4]; 

            // latidude, longtitude
            txtLat.Text = packetElems[5];
            txtLong.Text = packetElems[6];
            Utils.UpdateMap(map, Convert.ToDouble(packetElems[5]), Convert.ToDouble(packetElems[6]));

            // height
            hghttxt.Text = packetElems[7];
        }

        //dzialajaca mapa v1.0
        
        // poczatek czesci wykonawczej serial port txt box v1.0
        private void myPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            rxString = SerialPort1.ReadLine();
            UpdateGUI(rxString);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string packet = Utils.RandomPacket();
            UpdateGUI(packet);
        }
        //koniec czesci wyckonawczej serial port 
    }


    class Utils
    {
        public static string[] ParsePacket(string packet)
        {
            // "$$ZSM-Sat,997.27,1018,END$$$"
            string[] list = packet.Split(';');
            return list;
        }

        public static void UpdateChart(
            System.Windows.Forms.DataVisualization.Charting.Chart chart,
            double x,
            double y,
            int maxChartWidth
            )
        {
            int pointsCount = chart.Series[0].Points.Count;

            if (pointsCount >= maxChartWidth)
            {
                chart.Series[0].Points.RemoveAt(0);
                chart.ResetAutoValues();
            }

            chart.Series[0].Points.AddXY(x, y);
            chart.Update();
        }

        public static void UpdateMap(GMap.NET.WindowsForms.GMapControl map, double lat, double longt)
        {
            map.Position = new PointLatLng(lat, longt);
        }

        public static string RandomPacket()
        {
            return "$$ZSM-Sat;24;980;25;74;50.71;14.01;2057;END$$";
        }
    }
}


