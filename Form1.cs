using System;
using System.Windows.Forms;
// using GMap.NET;
// using GMap.NET.MapProviders;
//using SharpGL;
//using SharpGL.WinForms;
//using SharpGL.SceneGraph;
using System.Diagnostics;
using System.Threading;
using System.IO.Ports;
// using System.Windows.Forms.DataVisualization.Charting; // Chart
// using GMap.NET.WindowsForms; // GMapControl



namespace CanSatGUI
{
    public partial class Form1 : Form
    {
        Stopwatch timer = new Stopwatch();
        string rxString;

        public Form1()  //definiowanie ustawienia oraz port szeregowy
        {
            // sets double conversion separator to '.'
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;

            // init window
            InitializeComponent();

            // init COM
            SerialPort1 = Utils.InitSerialPort("COM4");
            SerialPort1.DataReceived += myPort_DataReceived;

            // init timer
            timer.Start();
        }
        
        // poczatek czesci wykonawczej serial port txt box v1.0
        private void myPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            rxString = SerialPort1.ReadLine();
            Console.WriteLine(rxString);
            this.Invoke(new EventHandler(UpdateWidgets));
        }

        //private void UpdateWidgets(object sender, EventArgs e)
        private void UpdateWidgets(object sender, EventArgs e)
        {
            DataStream.AppendText(rxString);
            string[] packetElems = Utils.ParsePacket(rxString);
            if (packetElems == null)
                return;

            psrtxt.Text = packetElems[2];
            tmptxt.Text = packetElems[3];
            // humtxt.Text = packetElems[2]; 
            txtLat.Text = packetElems[5];
            txtLong.Text = packetElems[6];
            hghttxt.Text = packetElems[7];


            double time = Convert.ToInt32(timer.Elapsed.TotalSeconds);

            double temperature = Convert.ToDouble(packetElems[3]);
            Upd.UpdateTemperatureChart(chart1, temperature, time);

            double pressure = Convert.ToDouble(packetElems[2]);
            Upd.UpdatePressureChart(chart2, pressure, time);

            double latitude = Convert.ToDouble(packetElems[5]);
            double longtitude = Convert.ToDouble(packetElems[6]);
            Upd.UpdateMap(map, latitude, longtitude);
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


