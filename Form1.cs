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
        string ComPort;

        


        public Form1()  //definiowanie ustawienia oraz port szeregowy
        {

             

            // sets double conversion separator to '.'
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;

            // init window
            InitializeComponent();

            // init timer
            timer.Start();
        }
        
        // poczatek czesci wykonawczej serial port txt box v1.0
        private void myPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            rxString = SerialPort1.ReadLine();
            Console.WriteLine(rxString);
            this.Invoke(new EventHandler(UpdateWidgets));
            //try
           // {
            //    this.Invoke(new EventHandler(UpdateWidgets));
           // }
           // catch ()
           // {
           // }
        }

        

        //private void UpdateWidgets(object sender, EventArgs e)
        private async void UpdateWidgets(object sender, EventArgs e)
        {

            // RSSI; framenr; xaccel; yaccel; zaccel; xtilt; ytilt; ztilt; xmag; ymag; zmag; pressure; temp; lat; long; alt; speed; course; h:m:s:ms; hall
            DataStream.AppendText(rxString);
            string[] packetElems = Utils.ParsePacket(rxString);
            if (packetElems == null)
                return;
            rssitxt.Text = packetElems[0];
            framenrtxt.Text = packetElems[1];
            xacceltxt.Text = packetElems[2];
            yacceltxt.Text = packetElems[3];
            zacceltxt.Text = packetElems[4];
            xtilttxt.Text = packetElems[5];
            ytilttxt.Text = packetElems[6];
            ztilttxt.Text = packetElems[7];
            xmagtxt.Text = packetElems[8];
            ymagtxt.Text = packetElems[9];
            zmagtxt.Text = packetElems[10];
            psrtxt.Text = packetElems[11];
            temptxt.Text = packetElems[12];
            lattxt.Text = packetElems[13];
            longtxt.Text = packetElems[14];
            alttxt.Text = packetElems[15];
            speedtxt.Text = packetElems[16];
            coursetxt.Text = packetElems[17];
            timetxt.Text = packetElems[18];
            halltxt.Text = packetElems[19];
            

            double time = Convert.ToInt32(timer.Elapsed.TotalSeconds);

            double temperature = Convert.ToDouble(packetElems[3]);
            Upd.UpdateTemperatureChart(chart1, temperature, time);

            double pressure = Convert.ToDouble(packetElems[2]);
            Upd.UpdatePressureChart(chart2, pressure, time);

            double Hall = Convert.ToDouble(packetElems[19]);
            double windSpeed = Utils.WindSpeed(Hall);
            Upd.UpdateHallChart(chart3, windSpeed, time);

            double Altitude = Convert.ToDouble(packetElems[15]);
            Upd.UpdateAltitudeChart(chart4, Altitude, time);

            double signal = Convert.ToDouble(packetElems[0]);
            Upd.UpdateSpeedChart(chart5, signal, time);

            double speed = Convert.ToDouble(packetElems[16]);
            Upd.UpdateSpeedChart(chart6, speed, time);
            
            double latitude = Convert.ToDouble(packetElems[5]);
            double longtitude = Convert.ToDouble(packetElems[6]);
            Upd.UpdateMap(map, latitude, longtitude);

            string json = JsonConvert.SerializeObject(new { psrtxt = psrtxt.Text, tmptxt = temptxt.Text, txtLat = lattxt.Text, txtLong = longtxt.Text, hghttext = alttxt.Text });
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

       

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Dropdown;
            
            Dropdown = ComboBox1.GetItemText(ComboBox1.SelectedItem);
            Console.WriteLine(Dropdown);

            // init COM
            SerialPort1 = Utils.InitSerialPort(Dropdown);     
            SerialPort1.DataReceived += myPort_DataReceived;

            // ComPort = Dropdown;



        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void DataStream_TextChanged(object sender, EventArgs e)
        {
            DataStream.ScrollToCaret();
          
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void chart3_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void chart4_Click(object sender, EventArgs e)
        {

        }

        private void chart6_Click(object sender, EventArgs e)
        {

        }
    }
}


