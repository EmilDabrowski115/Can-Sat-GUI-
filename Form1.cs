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

        // poczatek czesci wykonawczej serial port txt box v1.0
        private void myPort_DataReceived(object sender, SerialDataReceivedEventArgs _)
        {
            try
            {
                rxString = SerialPort1.ReadLine();
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
            xacceltxt.Text = packetElems[2]+ " G";
            yacceltxt.Text = packetElems[3] + " G";
            zacceltxt.Text = packetElems[4] + " G";
            xtilttxt.Text = packetElems[5]+ " °/s";
            ytilttxt.Text = packetElems[6] + " °/s";
            ztilttxt.Text = packetElems[7] + " °/s";
            xmagtxt.Text = packetElems[8] + " µT";
            ymagtxt.Text = packetElems[9] + " µT";
            zmagtxt.Text = packetElems[10] + " µT";
            psrtxt.Text = packetElems[11]+ " hPa";
            temptxt.Text = packetElems[12]+" C";
            lattxt.Text = packetElems[13];
            longtxt.Text = packetElems[14];
            alttxt.Text = packetElems[15] + " m";
            speedtxt.Text = packetElems[16] + "m/s";
            coursetxt.Text = packetElems[17]+ " °";
            timetxt.Text = packetElems[18];
            halltxt.Text = packetElems[19]+ " Hz";
            pitchtxt.Text = packetElems[20] + " °";
            rolltxt.Text = packetElems[21] + " °";
            yawtxt.Text = packetElems[22] + " °";
            


            pitch = float.Parse(packetElems[20], CultureInfo.InvariantCulture.NumberFormat);
            roll = float.Parse(packetElems[21], CultureInfo.InvariantCulture.NumberFormat);
            yaw = float.Parse(packetElems[22], CultureInfo.InvariantCulture.NumberFormat);

            double course = (Convert.ToDouble(packetElems[17]));
            pictureBox1.Image = Compass.DrawCompass(course / 100, 0, 80, 0, 80, pictureBox1.Size);

            double temperature = Convert.ToDouble(packetElems[12]);
            Upd.UpdateChart(chart1, temperature, time);

            double pressure = Convert.ToDouble(packetElems[11]);
            Upd.UpdateChart(chart2, pressure, time);

            // double course = (Convert.ToDouble(packetElems[17])/100);
            //Upd.UpdateChart(chart7, course, time);

            double Hall = Convert.ToDouble(packetElems[19]);
            double windSpeed = Utils.WindSpeed(Hall);
            Upd.UpdateChart(chart3, windSpeed, time);

            double signal = Convert.ToDouble(packetElems[0]);
            //gauge2.Update(chart5, signal, time);
            double signalPercent = Utils.SignalStrengthInPercent(signal);
            gauge2.Update(signalPercent);

            double speed = Convert.ToDouble(packetElems[16]);
            Upd.UpdateChart(chart6, speed, time);

            double latitude = Convert.ToDouble(packetElems[13]);
            double Longitude = Convert.ToDouble(packetElems[14]);
            // double altitude, double fallingSpeed, double windSpeed, int course

            double Altitude = Convert.ToDouble(packetElems[15]);

            double fallingSpeed;
            if (previousAltitude == -1)
            {
                fallingSpeed = 0;
            }
            else
            {
                fallingSpeed = previousAltitude - Altitude;
            }
            //Console.WriteLine(previousAltitude);
            //Console.WriteLine(Altitude);
            //Console.WriteLine(fallingSpeed);
            vertveltxt.Text = fallingSpeed.ToString() + " m/s";

            previousAltitude = Altitude;
            Upd.UpdateChart(chart4, Altitude, time);

            // Upd.UpdateChartGauge(chart7, veltxt, fallingSpeed);


            // Upd.UpdateChart(chart7, fallingSpeed, time);
            gauge1.Update(fallingSpeed);


            Upd.UpdateMap(map, latitude, Longitude, Altitude, fallingSpeed, speed, (int)course);
            lastPoint = new PointLatLng(latitude, Longitude);

            chart3D.Update(latitude, Longitude, Altitude);

            //openGLControl1.DoRender();

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
            map.CacheLocation = System.IO.Path.GetDirectoryName(Application.ExecutablePath); // ta linijka jest ok, przyda sie 
            //https://stackoverflow.com/questions/40847505/gmap-net-explicit-load-cache <^ tu masz link to tego // ta widziałem
            // teraz patrze program do zapisywania cache https://github.com/williamwdu/GMap.NETChacher
            map.CacheLocation = @"E:/Visual Studios/Can-Sat-GUI-/cache";
        }

        /*
         *  RectLatLng area = mapView.SelectedArea;

        if (!area.IsEmpty)
        {
            for (int i = (int)mapView.Zoom; i <= mapView.MaxZoom; i++)
            {
                TilePrefetcher obj = new TilePrefetcher();
                obj.Title = "Prefetching Tiles";
                obj.Icon = this.Icon;
                obj.Owner = this;
                obj.ShowCompleteMessage = false;
                obj.Start(area, i, mapView.MapProvider, 100);
            }

            DialogResult = true;
            Close();
        }
        else
        {
            MessageBox.Show("No Area Chosen", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        */


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

            /////// heree
            ///// Set the center of the plot region at (350, 280), and set width x depth x height to
            // 360 x 360 x 270 pixels
            //chart3D.scale(scale);
            chart3D = new threedscatter();
            chart3D.createChart(winChartViewer1, scale);

            WindowState = FormWindowState.Normal;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs _)
        {
            // init COM
            string Dropdown = ComboBox1.GetItemText(ComboBox1.SelectedItem);
            try
            {
                SerialPort1 = Utils.InitSerialPort(Dropdown);
                SerialPort1.DataReceived += myPort_DataReceived;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
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

        
        /* //DEBUG Mouse 3d object
        private void openGLControl1_MouseDown(object sender, MouseEventArgs e)
        {
            arcBallEffect.ArcBall.SetBounds(openGLControl1.Width, openGLControl1.Height);
            arcBallEffect.ArcBall.MouseDown(e.X, e.Y);
        }

        private void openGLControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                arcBallEffect.ArcBall.MouseMove(e.X, e.Y);
        }

        private void openGLControl1_MouseUp(object sender, MouseEventArgs e)
        {
            arcBallEffect.ArcBall.MouseUp(e.X, e.Y);
        } */


        private void openGLControl1_Load(object sender, EventArgs e)
        {
            //openGLControl1.MouseDown += new MouseEventHandler(openGLControl1_MouseDown); //DEBUG Mouse 3d object
            //openGLControl1.MouseMove += new MouseEventHandler(openGLControl1_MouseMove); //DEBUG Mouse 3d object
            //openGLControl1.MouseUp += new MouseEventHandler(openGLControl1_MouseUp); //DEBUG Mouse 3d object

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
            Environment.Exit(0);
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

       

        private void chart7_Load(object sender, EventArgs e)
        {

        }

        private void chart7_Load(object sender, PaintEventArgs e)
        {
            //e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            //Rectangle r = chart1.ClientRectangle;
            //r.Inflate(-10, -10);
            //using (SolidBrush brush = new SolidBrush(Color.FromArgb(55, Color.Beige)))
            //e.Graphics.FillEllipse(brush, r);
        }

        //private void setupChartGauge(Chart chart, double val, double vMin, double vMax, float a)
        //{
        //    Series s = chart.Series[0];
        //    // s.ChartType = SeriesChartType.Doughnut;
        //    //s.SetCustomProperty("PieStartAngle", (90 - angle / 2) + "");
        //    s.SetCustomProperty("PieStartAngle", 0 + "");
        //    s.SetCustomProperty("DoughnutRadius", "30");
        //    //s.Points.Clear();
        //    //s.Points.AddY(90);
        //    //s.Points.AddY(0);
        //    //s.Points.AddY(0);
        //    ////setChartGauge(0);
        //    //s.Points[0].Color = Color.Transparent;
        //    //s.Points[1].Color = Color.Chartreuse;
        //    //s.Points[2].Color = Color.Tomato;
        //}

        private void rssitxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void vertveltxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void psrtxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void veltxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void longtxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        //private void Form1_Resize(object sender, EventArgs e)
        //{

        //    //float widthRatio = this.Width / Screen.PrimaryScreen.Bounds.Width;
        //    //float heightRatio = this.Height / Screen.PrimaryScreen.Bounds.Height;
        //    //SizeF scale = new SizeF(widthRatio, heightRatio);
        //    //this.Scale(scale);
        //    //foreach (Control control in this.Controls)
        //    //{
        //    //Screen.PrimaryScreen.Bounds.Height;

        //    //    control.Font = new Font("Verdana", control.Font.SizeInPoints * heightRatio * widthRatio);
        //    //}
        //}

        //private void Form1_ResizeEnd(object sender, EventArgs e)
        //{
        //    float widthRatio = Screen.PrimaryScreen.Bounds.Width / this.Size.Width;
        //    float heightRatio = Screen.PrimaryScreen.Bounds.Height / this.Size.Height;
        //    //this.Size = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

        //    SizeF scale_ = new SizeF(widthRatio / scale.Width, heightRatio / scale.Height);
        //    this.Scale(scale_);
        //    Console.WriteLine("resize");
        //}
    }
}


