using System;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
//using SharpGL;
//using SharpGL.WinForms;
//using SharpGL.SceneGraph;
using System.Diagnostics;
using System.Threading;
using System.IO.Ports;
using System.Windows.Forms.DataVisualization.Charting;



namespace CanSatGUI
{
    public partial class Form1 : Form
    {
        Stopwatch timer = new Stopwatch();
        string DefaultPortName = "COM4";
        string rxString;

        public Form1()  //definiowanie ustawienia oraz port szeregowy
        {
            // sets double conversion separator to '.'
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;

            // init window
            InitializeComponent();

            // init COM
            SerialPort1 = Utils.InitSerialPort(DefaultPortName);
            SerialPort1.DataReceived += myPort_DataReceived;

            // init timer
            timer.Start();
        }


        private void UpdateMap(string lat, string longt)
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

        
        // poczatek czesci wykonawczej serial port txt box v1.0
        private void myPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            rxString = SerialPort1.ReadLine();
            Console.WriteLine(rxString);
            string[] args = new string[] { rxString };
            this.Invoke(new EventHandler(UpdateWidgets));
        }

        //private void UpdateWidgets(object sender, EventArgs e)
        private void UpdateWidgets(object sender, EventArgs e)
        {
            DataStream.AppendText(rxString);
            
            //Console.WriteLine(rxString);

            string[] packetElems = Utils.ParsePacket(rxString);

            psrtxt.Text = packetElems[2];
            tmptxt.Text = packetElems[3];
            // humtxt.Text = packetElems[2]; 
            txtLat.Text = packetElems[5];
            txtLong.Text = packetElems[6];
            hghttxt.Text = packetElems[7];
            UpdateTemperatureChart(packetElems[3]);
            UpdatePressureChart(packetElems[2]);
            UpdateMap(packetElems[5], packetElems[6]);


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

        private void openGLControl1_Load(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}


