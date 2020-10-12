using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using GMap.NET; 
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using System.Threading;
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

        public Form1()  //definiowanie ustawienia oraz port szeregowy
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
            SerialPort1.Open();
            SerialPort1.DataReceived += myPort_DataReceived;
            SerialPort1.NewLine = "\n";

            // init timer
            timer.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void mapupdate(string lat, string longt)
        {
            GMapProviders.GoogleMap.ApiKey = @"AIzaSyAZouhXULQgPGPckADOmiHqfCc_YvD5QzQ";
            map.DragButton = MouseButtons.Left;
            map.MapProvider = GMapProviders.GoogleMap;
            double latitude = Convert.ToDouble(lat);
            double longtitude = Convert.ToDouble(longt);
            map.Position = new PointLatLng(latitude, longtitude);
            map.MinZoom = 0;
            map.MaxZoom = 25;
            map.Zoom = 10;
        }


        //aktualizacja GUI    //$$ZSM-Sat;24;980;25;74;50.71;14.01;2057;END$$
        public void UpdateGUI(string packet)
        {
            Console.WriteLine(packet);
            
            string[] packetElems = Utils.ParsePacket(packet);
            
           
            psrtxt.Text = packetElems[2];
            tmptxt.Text = packetElems[3];
            // humtxt.Text = packetElems[2]; 
            txtLat.Text = packetElems[5];
            txtLong.Text = packetElems[6];
            hghttxt.Text = packetElems[7];
            UpdateTemperatureChart(packetElems[3]);
            UpdatePressureChart(packetElems[2]);
            mapupdate(packetElems[5],packetElems[6]);

        }

        private void UpdateTemperatureChart(string temp)
        {

            int MaxChartWidth = 7;
            TimeSpan elapsed = timer.Elapsed;
            double secondsElapsed = Convert.ToInt32(elapsed.TotalSeconds);
            double temperature = Convert.ToDouble(temp);
           

            int pointsCount = chart1.Series[0].Points.Count;
            Console.WriteLine(pointsCount);
            if(pointsCount >= MaxChartWidth)
            {
                chart1.Series[0].Points.RemoveAt(0);
                chart1.ResetAutoValues();
            }

            chart1.Series[0].Points.AddXY(secondsElapsed, temperature);

            chart1.Update();
        }

        private void UpdatePressureChart(string psr)
        {
            int MaxChartWidth = 7;
            TimeSpan elapsed = timer.Elapsed;
            double secondsElapsed = Convert.ToInt32(elapsed.TotalSeconds);
            double pressure = Convert.ToDouble(psr);


            int pointsCount = chart2.Series[0].Points.Count;
            Console.WriteLine(pointsCount);
            if (pointsCount >= MaxChartWidth)
            {
                
                chart2.Series[0].Points.RemoveAt(0);
                chart2.ResetAutoValues();
            }

            chart2.Series[0].Points.AddXY(secondsElapsed,pressure);

            chart2.Update();
        }

        //dzialajaca mapa v1.0
        
        // poczatek czesci wykonawczej serial port txt box v1.0
        private void myPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            rxString = SerialPort1.ReadLine();
            Console.WriteLine(rxString);
            this.Invoke(new EventHandler(UpdateWidgets));
        }

        private void UpdateWidgets(object sender, EventArgs e)
        {
            DataStream.AppendText(rxString);
            
            UpdateGUI(rxString);
           
            
        }

        private void psrtxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void chart2_Click(object sender, EventArgs e)
        {

        }

        private void map_Load(object sender, EventArgs e)
        {

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
        
        
    }
}


