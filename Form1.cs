using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SharpGL;
using SharpGL.WinForms;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Effects;
using SharpGL.SceneGraph.Primitives;
using SharpGL.Serialization.Wavefront;
using SharpGL.SceneGraph.Lighting;
using SharpGL.SceneGraph.Cameras;
using System.Diagnostics;
using System.Threading;
using System.IO.Ports;
using SocketIOClient;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Drawing;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms;
using GMap.NET.MapProviders;
using GMap.NET;
using GMap.NET.Internals;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing.Drawing2D;
using GMap.NET.CacheProviders;
using System.Threading.Tasks;



namespace CanSatGUI
{
    public partial class Form1 : Form
    {
        public SocketIO client = new SocketIO("http://77.55.213.87:3000");

        //private readonly ArcBallEffect arcBallEffect = new ArcBallEffect(); //DEBUG Mouse 3d object

        Stopwatch timer = new Stopwatch();
        string rxString;
        StreamWriter writer;
        PointLatLng lastPoint = new PointLatLng(0, 0);
        threedscatter chart3D;
        double previousAltitude = -1;
        float pitch = 00.0f;
        float yaw = 00.0f;
        float roll = 00.0f;
        GaugeChart gauge1;
        GaugeChart gauge2;


        public Form1()  //definiowanie ustawienia oraz port szeregowy
        {
            if (Debugger.IsAttached)
                CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.GetCultureInfo("en-US");

            // sets double conversion separator to '.'
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            // init window
            InitializeComponent();

            // enable keyboard input
            this.KeyPreview = true;

            // init timer
            timer.Start();

            // setup output file
            string date_time = Utils.GetTimestamp(DateTime.Now);
            System.IO.Directory.CreateDirectory("output");
            writer = new StreamWriter("output/output" + date_time + ".log");

            // setupChartGauge(40.0, 0.0, 100.0, 90.0f);
            gauge1 = new GaugeChart(chart7, veltxt, "{0} m/s");
            gauge2 = new GaugeChart(chart5, textBox3, "{0}%");
        }

        private async void ComPortConnetionRenewer()
        {
            while (true)
            {
                await Task.Delay(1000);
                if ((SerialPort1 == null || !SerialPort1.IsOpen) && ComboBox1.GetItemText(ComboBox1.SelectedItem) != "")
                {
                    string dropdownText = ComboBox1.GetItemText(ComboBox1.SelectedItem);
                    DataStream.AppendText("Connecting to " + dropdownText + "\n");
                    tryToConnectToCOM(dropdownText);
                }
            }
        }

        private bool AutoDetectComPort()
        {
            for (int i = 2; i < 8; i++)
            {
                string portName = "COM" + i;
                bool connected = tryToConnectToCOM(portName);
                DataStream.AppendText("Connecting to " + portName + "\n");

                if (connected)
                {
                    ComboBox1.SelectedIndex = ComboBox1.FindStringExact(portName);
                    return true;
                }
            }
            return true;
        }

        // poczatek czesci wykonawczej serial port txt box v1.0
        private void myPort_DataReceived(object sender1, SerialDataReceivedEventArgs _)
        {
            try
            {
                SerialPort sender = (SerialPort)sender1;
                rxString = sender.ReadLine();
                writer.Write(rxString);
                this.Invoke(new EventHandler(UpdateWidgets));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                writer.Flush();
            }
        }


        private async void UpdateWidgets(object sender, EventArgs e)
        {
            //162;-0.02;-0.12;0.88;0.06;0.12;0.00;277.36;247.41;384.70;1028.69;25.00;X;X;X;X;X;1:0:0;0.00;1.57;-7.42;-43.99
            // RSSI; framenr; xaccel; yaccel; zaccel; xtilt; ytilt; ztilt; xmag; ymag; zmag; pressure; temp; lat; long; alt; speed; course; h:m:s:ms; hall
            DataStream.AppendText(rxString);
            string[] packetElems = Utils.ParsePacket(rxString);
            double time = Convert.ToInt32(timer.Elapsed.TotalSeconds);

            if (packetElems == null || packetElems.Length != 23)
            {
                Console.WriteLine("Skipped corrupted packet");
                return;
            }

            rssitxt.Text = packetElems[0] + " dBm";
            framenrtxt.Text = packetElems[1];
            xacceltxt.Text = packetElems[2] + " G";
            yacceltxt.Text = packetElems[3] + " G";
            zacceltxt.Text = packetElems[4] + " G";
            xtilttxt.Text = packetElems[5] + " °/s";
            ytilttxt.Text = packetElems[6] + " °/s";
            ztilttxt.Text = packetElems[7] + " °/s";
            xmagtxt.Text = packetElems[8] + " µT";
            ymagtxt.Text = packetElems[9] + " µT";
            zmagtxt.Text = packetElems[10] + " µT";
            psrtxt.Text = packetElems[11] + " hPa";
            temptxt.Text = packetElems[12] + " C";
       
            alttxt.Text = packetElems[15] + " m";
            speedtxt.Text = packetElems[16] + "m/s";
            coursetxt.Text = packetElems[17] + " °";
            timetxt.Text = packetElems[18];
            halltxt.Text = packetElems[19] + " Hz";
            pitchtxt.Text = packetElems[20] + " °";
            rolltxt.Text = packetElems[21] + " °";
            yawtxt.Text = packetElems[22] + " °";
            if (packetElems[13] == "x" || packetElems[13] == "X")
            {
                lattxt.Text = "NO GPS";
                longtxt.Text = "NO GPS";
                previousAltitude = -1;
            }
            else
            {
                // lat; long; alt; speed; course
                lattxt.Text = packetElems[13];
                longtxt.Text = packetElems[14];

                double latitude = Convert.ToDouble(packetElems[13]);
                double Longitude = Convert.ToDouble(packetElems[14]);
                double Altitude = Convert.ToDouble(packetElems[15]);
                double speed = Convert.ToDouble(packetElems[16]);
                double course = Convert.ToDouble(packetElems[17]);

                double fallingSpeed;
                if (previousAltitude == -1)
                {
                    fallingSpeed = 0;
                }
                else
                {
                    fallingSpeed = previousAltitude - Altitude;
                }
                gauge1.Update(fallingSpeed);


                Upd.UpdateMap(map, latitude, Longitude, Altitude, fallingSpeed, speed, (int)course);
                lastPoint = new PointLatLng(latitude, Longitude);
                chart3D.Update(latitude, Longitude, Altitude);

                vertveltxt.Text = fallingSpeed.ToString() + " m/s";

                previousAltitude = Altitude;
                Upd.UpdateChart(chart4, Altitude, time);

                Upd.UpdateChart(chart6, speed, time);

                pictureBox1.Image = Compass.DrawCompass(course / 100, 0, 80, 0, 80, pictureBox1.Size);
            }

            pitch = float.Parse(packetElems[20], CultureInfo.InvariantCulture.NumberFormat);
            roll = float.Parse(packetElems[21], CultureInfo.InvariantCulture.NumberFormat);
            yaw = float.Parse(packetElems[22], CultureInfo.InvariantCulture.NumberFormat);

            double temperature = Convert.ToDouble(packetElems[12]);
            Upd.UpdateChart(chart1, temperature, time);

            double pressure = Convert.ToDouble(packetElems[11]);
            Upd.UpdateChart(chart2, pressure, time);

            // double course = (Convert.ToDouble(packetElems[17])/100);
            //Upd.UpdateChart(chart7, course, time);

            double Hall = Convert.ToDouble(packetElems[19]);
            double windSpeed = Utils.WindSpeed(Hall);
            Upd.UpdateChart(chart3, windSpeed, time);

            
            windtxt.Text = Convert.ToString(windSpeed); ;


            double signal = Convert.ToDouble(packetElems[0]);
            //gauge2.Update(chart5, signal, time);
            double signalPercent = Utils.SignalStrengthInPercent(signal);
            gauge2.Update(signalPercent);

            
            

            // socketio
            string json = JsonConvert.SerializeObject(new { psrtxt = psrtxt.Text, tmptxt = temptxt.Text, txtLat = lattxt.Text, txtLong = longtxt.Text, hghttext = alttxt.Text });
            try
            {
                _ = client.EmitAsync("data", json);
            }
            catch { }
        }

        private void InitWidgets()
        {
            // DataStream.AppendText("Initalize");
        }


        private void map_Load(object sender, EventArgs e)
        {
            GMapProviders.GoogleMap.ApiKey = @"AIzaSyAZouhXULQgPGPckADOmiHqfCc_YvD5QzQ";
            map.DragButton = MouseButtons.Left;
            map.MapProvider = GMapProviders.GoogleHybridMap;

            // map.Position = new PointLatLng(0, 0);
            map.MinZoom = 0;
            map.MaxZoom = 25;
            map.Zoom = 20;

            GMapOverlay markersOverlay = new GMapOverlay("markers");
            map.Overlays.Add(markersOverlay);
            GMaps.Instance.Mode = AccessMode.CacheOnly; //cache only means offline , server only online , cache and server online but loads offline files to folder
            //map.CacheLocation = System.IO.Path.GetDirectoryName(Application.ExecutablePath); // ta linijka jest ok, przyda sie 
            //https://stackoverflow.com/questions/40847505/gmap-net-explicit-load-cache <^ tu masz link to tego // ta widziałem
            // teraz patrze program do zapisywania cache https://github.com/williamwdu/GMap.NETChacher
            try
            {
                map.CacheLocation = @"E:/Visual Studios/Can-Sat-GUI-/cache";
            }
            catch
            {
                DataStream.AppendText("Couldn't load map cache. Using online map.\n");
                GMaps.Instance.Mode = AccessMode.ServerAndCache; //cache only means offline , server only online , cache and server online but loads offline files to folder
            }

        }




        private async void Form1_Load(object sender, EventArgs e)
        {
            InitWidgets();
            client.OnConnected += async (sender_socket, e_socket) =>
            {
                //await client.EmitAsync("data", ".net core");
            };
            try
            {
                await client.ConnectAsync();
            }
            catch
            { }
            //starts app in fullscreen

            float widthRatio = Screen.PrimaryScreen.Bounds.Width / 1920f;
            float heightRatio = Screen.PrimaryScreen.Bounds.Height / 1080f;
            //this.Size = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            SizeF scale = new SizeF(widthRatio, heightRatio);
            this.Scale(scale);

            foreach (Control control in Utils.GetAllControls(this))
            {
                //control.Font = new Font("Calibri", control.Font.SizeInPoints * ((heightRatio + widthRatio) / 5));
                control.Font = new Font("Calibri", control.Font.SizeInPoints * heightRatio);
                Console.WriteLine(control.ToString());
            }

            
            ///// Set the center of the plot region at (350, 280), and set width x depth x height to
            // 360 x 360 x 270 pixels
            //chart3D.scale(scale);
            chart3D = new threedscatter();
            chart3D.createChart(winChartViewer1, scale);

            WindowState = FormWindowState.Normal;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;

            AutoDetectComPort();

            // com port background task
            ComPortConnetionRenewer();
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs _)
        {
            string dropdownText = ComboBox1.GetItemText(ComboBox1.SelectedItem);
            tryToConnectToCOM(dropdownText);
        }

        private bool tryToConnectToCOM(string portName)
        {
            // init COM
            if (SerialPort1 != null)
            {
                SerialPort1.Close();
            }
            SerialPort1 = null;
            try
            {
                SerialPort1 = Utils.InitSerialPort(portName);
                SerialPort1.DataReceived += myPort_DataReceived;
                DataStream.AppendText("Connected\n");
                //lastSerialPortName = portName;
            }
            catch (System.UnauthorizedAccessException) { 
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                DataStream.AppendText("Connection failed\n");
                return false;
            }
            return true;
        }


        private void DataStream_TextChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                DataStream.ScrollToCaret();
            }

        }


        private void button1_Click(object sender, EventArgs e)
        {
            map.Position = lastPoint;
        }

        
        


        private void openGLControl1_Load(object sender, EventArgs e)
        {
            

            var obj = new ObjFileFormat();
            var objScene = obj.LoadData("Assets/cansat_gotowy.obj");

            foreach (var asset in objScene.Assets)
            {
                openGLControl1.Scene.Assets.Add(asset);
            }

            openGLControl1.Scene.RenderBoundingVolumes = false;

            var polygons = objScene.SceneContainer.Traverse<Polygon>().ToList();


            foreach (Polygon polygon in polygons)
            {
                polygon.Transformation.RotateX = 90f; // rotate to right direction

                polygon.Parent.RemoveChild(polygon);
                polygon.Transformation.ScaleX = 8f;
                polygon.Transformation.ScaleY = 8f;
                polygon.Transformation.ScaleZ = 8f;

                polygon.Freeze(openGLControl1.OpenGL);

                openGLControl1.Scene.SceneContainer.AddChild(polygon);

                // Add effects.
                polygon.AddEffect(new OpenGLAttributesEffect());
                //polygon.AddEffect(arcBallEffect); //DEBUG Mouse 3d object
            }

        }

        private void openGLControl1_OpenGLDraw(object sender, RenderEventArgs e)
        {
            var gl = this.openGLControl1.OpenGL;
            //Reset position
            gl.LoadIdentity();
            gl.Rotate(pitch, roll, yaw);
        }


        private void openGLControl1_OpenGLInitialized(object sender, EventArgs e)
        {


            //clear grid and axis
            openGLControl1.Scene.SceneContainer.Children.Clear();
            //set background color
            GLColor background = new GLColor(11 / 255f, 18 / 255f, 34 / 255f, 1);
            openGLControl1.Scene.ClearColour = background;

            //  Create some lights.
            Light light1 = new Light()
            {
                Name = "Light 1",
                On = true,
                Position = new Vertex(-9, -9, 11),
                GLCode = OpenGL.GL_LIGHT0
            };
            Light light2 = new Light()
            {
                Name = "Light 2",
                On = true,
                Position = new Vertex(9, -9, 11),
                GLCode = OpenGL.GL_LIGHT1
            };
            Light light3 = new Light()
            {
                Name = "Light 3",
                On = true,
                Position = new Vertex(0, 15, 15),
                GLCode = OpenGL.GL_LIGHT2
            };

            //  Add the lights.
            var folder = new Folder() { Name = "Lights" };
            folder.AddChild(light1);
            folder.AddChild(light2);
            folder.AddChild(light3);
            openGLControl1.Scene.SceneContainer.AddChild(folder);


            var lookAtCamera = new LookAtCamera()
            {
                Position = new Vertex(0f, -20f, 2f),
                Target = new Vertex(0f, 0f, 0f),
                UpVector = new Vertex(0f, 0f, 1f)
            };

            //  Set the look at camera as the current camera.
            openGLControl1.Scene.CurrentCamera = lookAtCamera;
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

      

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F11)
            {
                WindowState = FormWindowState.Normal;
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                WindowState = FormWindowState.Normal;
                FormBorderStyle = FormBorderStyle.Sizable;
                this.Size = new Size(1280, 800);
            }

        }

       

        
        private void button3_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        
    }
}


