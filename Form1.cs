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
        float pitch = 30.0f;
        float yaw = 0.0f;
        float roll = 50.0f;


        public Form1()  //definiowanie ustawienia oraz port szeregowy
        {
            if (Debugger.IsAttached)
                CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.GetCultureInfo("en-US");

            // sets double conversion separator to '.'
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            // init window
            InitializeComponent();

            // init widgets
            

            // init timer
            timer.Start();

            // setup output file
            string date_time = Utils.GetTimestamp(DateTime.Now);
            System.IO.Directory.CreateDirectory("output");
            writer = new StreamWriter("output/output" + date_time + ".log");

            // 
            chart3D = new threedscatter();
            chart3D.createChart(winChartViewer1);
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

            if (packetElems == null || packetElems.Length != 20)
            {
                Console.WriteLine("Skipped corrupted packet");
                return;
            }

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

            double course = (Convert.ToDouble(packetElems[17]));
            pictureBox1.Image = Compass.DrawCompass(course/100 , 0, 80, 0, 80, pictureBox1.Size);

            double temperature = Convert.ToDouble(packetElems[12]);
            Upd.UpdateChart(chart1, temperature, time);

            double pressure = Convert.ToDouble(packetElems[11]);
            Upd.UpdateChart(chart2, pressure, time);

           // double course = (Convert.ToDouble(packetElems[17])/100);
            //Upd.UpdateChart(chart7, course, time);

            double Hall = Convert.ToDouble(packetElems[19]);
            double windSpeed = Utils.WindSpeed(Hall);
            Upd.UpdateChart(chart3, windSpeed, time);

            double Altitude = Convert.ToDouble(packetElems[15]);
            Upd.UpdateChart(chart4, Altitude, time);

            double signal = Convert.ToDouble(packetElems[0]);
            Upd.UpdateChart(chart5, signal, time);

            double speed = Convert.ToDouble(packetElems[16]);
            Upd.UpdateChart(chart6, speed, time);
            
            double latitude = Convert.ToDouble(packetElems[13]);
            double Longitude = Convert.ToDouble(packetElems[14]);
            // double altitude, double fallingSpeed, double windSpeed, int course

            double fallingSpeed = 9; ///////////
            Upd.UpdateMap(map, latitude, Longitude, Altitude, fallingSpeed, speed, (int)course);
            lastPoint = new PointLatLng(latitude, Longitude);

            chart3D.Update(latitude, Longitude, Altitude);



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

            DataStream.AppendText("Initalize");

            
        }

        private void psrtxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void chart2_Click(object sender, EventArgs e)
        {

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
        }



        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            InitWidgets();
            client.OnConnected += async (sender_socket, e_socket) => {
                //await client.EmitAsync("data", ".net core");
            };
            try
            {
                await client.ConnectAsync();
            }
            catch
            { }
            //starts app in fullscreen

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

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void DataStream_TextChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                DataStream.ScrollToCaret();
            }

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


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            

        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            map.Position = lastPoint;
        }

        private void winChartViewer1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

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

            var gl = this.openGLControl1.OpenGL;


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
            GLColor background = new GLColor(11/255f, 18/255f, 34/255f, 1);
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
    }
}


