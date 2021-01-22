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
using SocketIOClient;
using Newtonsoft.Json;


namespace CanSatGUI
{
    public partial class Form1 : Form
    {
        public SocketIO client = new SocketIO("http://77.55.213.87:3000");

        Stopwatch timer = new Stopwatch();
        string rxString;

        public Form1()  //definiowanie ustawienia oraz port szeregowy
        {
            // sets double conversion separator to '.'
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;

            // init window
            InitializeComponent();

            // init COM
            SerialPort1 = Utils.InitSerialPort("COM5");
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
        private async void UpdateWidgets(object sender, EventArgs e)
        {
            // framenr; xmss; ymss; zmss; xrads; yrads; zrads; magx; magy; magz; preassure; temp;
            // RSSI; framenr; xmss; ymss; zmss; xrads; yrads; zrads; magx; magy; magz; preassure; temp; lat; lng; alt; speed; crouze; h: m: s: cs; Hall
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

            string json = JsonConvert.SerializeObject(new { psrtxt = psrtxt.Text, tmptxt = tmptxt.Text, txtLat = txtLat.Text, txtLong = txtLong.Text, hghttext = hghttxt.Text });
            try
            {
                await client.EmitAsync("data", json);
            }
            catch (SocketIOClient.Exceptions.InvalidSocketStateException)
            {

            }
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

        private async void Form1_Load(object sender, EventArgs e)
        {
            client.OnConnected += async (sender_socket, e_socket) => {
                //await client.EmitAsync("data", ".net core");
            };
            try
            {
                await client.ConnectAsync();
            }
            catch (System.Net.WebSockets.WebSocketException)
            {

            }
        }
    }
}


