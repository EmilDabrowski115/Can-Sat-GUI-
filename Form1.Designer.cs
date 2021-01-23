namespace CanSatGUI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.lattxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.longtxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.map = new GMap.NET.WindowsForms.GMapControl();
            this.SerialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.DataStream = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.temptxt = new System.Windows.Forms.TextBox();
            this.psrtxt = new System.Windows.Forms.TextBox();
            this.alttxt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.asdasdasda = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.openGLControl1 = new SharpGL.OpenGLControl();
            this.framenrtxt = new System.Windows.Forms.TextBox();
            this.zacceltxt = new System.Windows.Forms.TextBox();
            this.yacceltxt = new System.Windows.Forms.TextBox();
            this.xacceltxt = new System.Windows.Forms.TextBox();
            this.ytilttxt = new System.Windows.Forms.TextBox();
            this.ztilttxt = new System.Windows.Forms.TextBox();
            this.xmagtxt = new System.Windows.Forms.TextBox();
            this.xtilttxt = new System.Windows.Forms.TextBox();
            this.zmagtxt = new System.Windows.Forms.TextBox();
            this.rssitxt = new System.Windows.Forms.TextBox();
            this.speedtxt = new System.Windows.Forms.TextBox();
            this.ymagtxt = new System.Windows.Forms.TextBox();
            this.coursetxt = new System.Windows.Forms.TextBox();
            this.timetxt = new System.Windows.Forms.TextBox();
            this.halltxt = new System.Windows.Forms.TextBox();
            this.frametxt = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.ComboBox1 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.chart3 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart3)).BeginInit();
            this.SuspendLayout();
            // 
            // lattxt
            // 
            this.lattxt.Location = new System.Drawing.Point(233, 27);
            this.lattxt.Name = "lattxt";
            this.lattxt.Size = new System.Drawing.Size(100, 20);
            this.lattxt.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(230, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Longitude";
            // 
            // longtxt
            // 
            this.longtxt.Location = new System.Drawing.Point(233, 75);
            this.longtxt.Name = "longtxt";
            this.longtxt.Size = new System.Drawing.Size(100, 20);
            this.longtxt.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(230, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Latitude";
            // 
            // map
            // 
            this.map.BackColor = System.Drawing.SystemColors.Control;
            this.map.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("map.BackgroundImage")));
            this.map.Bearing = 0F;
            this.map.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.map.CanDragMap = true;
            this.map.EmptyTileColor = System.Drawing.Color.Navy;
            this.map.ForeColor = System.Drawing.SystemColors.Control;
            this.map.GrayScaleMode = false;
            this.map.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.map.LevelsKeepInMemmory = 5;
            this.map.Location = new System.Drawing.Point(-3, 8);
            this.map.MarkersEnabled = true;
            this.map.MaxZoom = 2;
            this.map.MinZoom = 2;
            this.map.MouseWheelZoomEnabled = true;
            this.map.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.map.Name = "map";
            this.map.NegativeMode = false;
            this.map.PolygonsEnabled = true;
            this.map.RetryLoadTile = 0;
            this.map.RoutesEnabled = true;
            this.map.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.map.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.map.ShowTileGridLines = false;
            this.map.Size = new System.Drawing.Size(227, 236);
            this.map.TabIndex = 7;
            this.map.Zoom = 0D;
            this.map.Load += new System.EventHandler(this.map_Load);
            // 
            // SerialPort1
            // 
            this.SerialPort1.PortName = "COM3";
            // 
            // DataStream
            // 
            this.DataStream.Location = new System.Drawing.Point(369, 27);
            this.DataStream.Name = "DataStream";
            this.DataStream.Size = new System.Drawing.Size(291, 68);
            this.DataStream.TabIndex = 8;
            this.DataStream.Text = "";
            this.DataStream.TextChanged += new System.EventHandler(this.DataStream_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(484, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Data Stream";
            // 
            // temptxt
            // 
            this.temptxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.temptxt.Location = new System.Drawing.Point(1417, 90);
            this.temptxt.Name = "temptxt";
            this.temptxt.Size = new System.Drawing.Size(100, 20);
            this.temptxt.TabIndex = 10;
            // 
            // psrtxt
            // 
            this.psrtxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.psrtxt.Location = new System.Drawing.Point(1417, 142);
            this.psrtxt.Name = "psrtxt";
            this.psrtxt.Size = new System.Drawing.Size(100, 20);
            this.psrtxt.TabIndex = 11;
            this.psrtxt.TextChanged += new System.EventHandler(this.psrtxt_TextChanged);
            // 
            // alttxt
            // 
            this.alttxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.alttxt.Location = new System.Drawing.Point(1417, 116);
            this.alttxt.Name = "alttxt";
            this.alttxt.Size = new System.Drawing.Size(100, 20);
            this.alttxt.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1332, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Temperature";
            // 
            // asdasdasda
            // 
            this.asdasdasda.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.asdasdasda.AutoSize = true;
            this.asdasdasda.Location = new System.Drawing.Point(1332, 119);
            this.asdasdasda.Name = "asdasdasda";
            this.asdasdasda.Size = new System.Drawing.Size(42, 13);
            this.asdasdasda.TabIndex = 14;
            this.asdasdasda.Text = "Altitude";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1332, 145);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Pressure";
            // 
            // chart1
            // 
            chartArea1.BackSecondaryColor = System.Drawing.Color.Transparent;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Cursor = System.Windows.Forms.Cursors.Arrow;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(856, 173);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series1.XValueMember = "tmptxt";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(212, 150);
            this.chart1.TabIndex = 16;
            this.chart1.Text = "chart1";
            this.chart1.Click += new System.EventHandler(this.chart1_Click);
            // 
            // chart2
            // 
            chartArea2.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea2);
            this.chart2.Cursor = System.Windows.Forms.Cursors.Arrow;
            legend2.Name = "Legend1";
            this.chart2.Legends.Add(legend2);
            this.chart2.Location = new System.Drawing.Point(856, 342);
            this.chart2.Name = "chart2";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            series2.XValueMember = "tmptxt";
            this.chart2.Series.Add(series2);
            this.chart2.Size = new System.Drawing.Size(212, 150);
            this.chart2.TabIndex = 17;
            this.chart2.Text = "chart2";
            this.chart2.Click += new System.EventHandler(this.chart2_Click);
            // 
            // openGLControl1
            // 
            this.openGLControl1.DrawFPS = false;
            this.openGLControl1.Location = new System.Drawing.Point(325, 367);
            this.openGLControl1.Name = "openGLControl1";
            this.openGLControl1.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.openGLControl1.RenderContextType = SharpGL.RenderContextType.DIBSection;
            this.openGLControl1.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.openGLControl1.Size = new System.Drawing.Size(166, 153);
            this.openGLControl1.TabIndex = 18;
            this.openGLControl1.Load += new System.EventHandler(this.openGLControl1_Load);
            // 
            // framenrtxt
            // 
            this.framenrtxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.framenrtxt.Location = new System.Drawing.Point(1417, 12);
            this.framenrtxt.Name = "framenrtxt";
            this.framenrtxt.Size = new System.Drawing.Size(100, 20);
            this.framenrtxt.TabIndex = 20;
            // 
            // zacceltxt
            // 
            this.zacceltxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.zacceltxt.Location = new System.Drawing.Point(1417, 249);
            this.zacceltxt.Name = "zacceltxt";
            this.zacceltxt.Size = new System.Drawing.Size(100, 20);
            this.zacceltxt.TabIndex = 21;
            // 
            // yacceltxt
            // 
            this.yacceltxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.yacceltxt.Location = new System.Drawing.Point(1417, 223);
            this.yacceltxt.Name = "yacceltxt";
            this.yacceltxt.Size = new System.Drawing.Size(100, 20);
            this.yacceltxt.TabIndex = 22;
            // 
            // xacceltxt
            // 
            this.xacceltxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xacceltxt.Location = new System.Drawing.Point(1417, 197);
            this.xacceltxt.Name = "xacceltxt";
            this.xacceltxt.Size = new System.Drawing.Size(100, 20);
            this.xacceltxt.TabIndex = 23;
            // 
            // ytilttxt
            // 
            this.ytilttxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ytilttxt.Location = new System.Drawing.Point(1417, 303);
            this.ytilttxt.Name = "ytilttxt";
            this.ytilttxt.Size = new System.Drawing.Size(100, 20);
            this.ytilttxt.TabIndex = 27;
            // 
            // ztilttxt
            // 
            this.ztilttxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ztilttxt.Location = new System.Drawing.Point(1417, 329);
            this.ztilttxt.Name = "ztilttxt";
            this.ztilttxt.Size = new System.Drawing.Size(100, 20);
            this.ztilttxt.TabIndex = 26;
            // 
            // xmagtxt
            // 
            this.xmagtxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xmagtxt.Location = new System.Drawing.Point(1417, 355);
            this.xmagtxt.Name = "xmagtxt";
            this.xmagtxt.Size = new System.Drawing.Size(100, 20);
            this.xmagtxt.TabIndex = 25;
            // 
            // xtilttxt
            // 
            this.xtilttxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xtilttxt.Location = new System.Drawing.Point(1417, 277);
            this.xtilttxt.Name = "xtilttxt";
            this.xtilttxt.Size = new System.Drawing.Size(100, 20);
            this.xtilttxt.TabIndex = 24;
            // 
            // zmagtxt
            // 
            this.zmagtxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.zmagtxt.Location = new System.Drawing.Point(1417, 408);
            this.zmagtxt.Name = "zmagtxt";
            this.zmagtxt.Size = new System.Drawing.Size(100, 20);
            this.zmagtxt.TabIndex = 31;
            // 
            // rssitxt
            // 
            this.rssitxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rssitxt.Location = new System.Drawing.Point(1417, 64);
            this.rssitxt.Name = "rssitxt";
            this.rssitxt.Size = new System.Drawing.Size(100, 20);
            this.rssitxt.TabIndex = 30;
            // 
            // speedtxt
            // 
            this.speedtxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.speedtxt.Location = new System.Drawing.Point(1417, 168);
            this.speedtxt.Name = "speedtxt";
            this.speedtxt.Size = new System.Drawing.Size(100, 20);
            this.speedtxt.TabIndex = 29;
            // 
            // ymagtxt
            // 
            this.ymagtxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ymagtxt.Location = new System.Drawing.Point(1417, 382);
            this.ymagtxt.Name = "ymagtxt";
            this.ymagtxt.Size = new System.Drawing.Size(100, 20);
            this.ymagtxt.TabIndex = 28;
            // 
            // coursetxt
            // 
            this.coursetxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.coursetxt.Location = new System.Drawing.Point(1417, 434);
            this.coursetxt.Name = "coursetxt";
            this.coursetxt.Size = new System.Drawing.Size(100, 20);
            this.coursetxt.TabIndex = 34;
            // 
            // timetxt
            // 
            this.timetxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.timetxt.Location = new System.Drawing.Point(1417, 38);
            this.timetxt.Name = "timetxt";
            this.timetxt.Size = new System.Drawing.Size(100, 20);
            this.timetxt.TabIndex = 33;
            // 
            // halltxt
            // 
            this.halltxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.halltxt.Location = new System.Drawing.Point(1417, 460);
            this.halltxt.Name = "halltxt";
            this.halltxt.Size = new System.Drawing.Size(100, 20);
            this.halltxt.TabIndex = 32;
            // 
            // frametxt
            // 
            this.frametxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.frametxt.AutoSize = true;
            this.frametxt.Location = new System.Drawing.Point(1332, 15);
            this.frametxt.Name = "frametxt";
            this.frametxt.Size = new System.Drawing.Size(50, 13);
            this.frametxt.TabIndex = 35;
            this.frametxt.Text = "Frame Nr";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(1332, 385);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 13);
            this.label8.TabIndex = 39;
            this.label8.Text = "Y-Mag";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(1332, 358);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(38, 13);
            this.label9.TabIndex = 38;
            this.label9.Text = "X-Mag";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(1332, 332);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(31, 13);
            this.label10.TabIndex = 37;
            this.label10.Text = "Z-Tilt";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(1332, 306);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(31, 13);
            this.label11.TabIndex = 36;
            this.label11.Text = "Y-Tilt";
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(1332, 226);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(44, 13);
            this.label14.TabIndex = 41;
            this.label14.Text = "Y-Accel";
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(1332, 200);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(44, 13);
            this.label15.TabIndex = 40;
            this.label15.Text = "X-Accel";
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(1332, 437);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(39, 13);
            this.label16.TabIndex = 47;
            this.label16.Text = "course";
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(1332, 168);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(38, 13);
            this.label17.TabIndex = 46;
            this.label17.Text = "Speed";
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(1332, 67);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(0, 13);
            this.label18.TabIndex = 45;
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(1332, 411);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(38, 13);
            this.label19.TabIndex = 44;
            this.label19.Text = "Z-Mag";
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(1332, 463);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(47, 13);
            this.label20.TabIndex = 49;
            this.label20.Text = "Hall (Hz)";
            // 
            // label21
            // 
            this.label21.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(1332, 41);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(30, 13);
            this.label21.TabIndex = 48;
            this.label21.Text = "Time";
            // 
            // label22
            // 
            this.label22.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(1332, 67);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(79, 13);
            this.label22.TabIndex = 50;
            this.label22.Text = "Signal Strength";
            // 
            // ComboBox1
            // 
            this.ComboBox1.AllowDrop = true;
            this.ComboBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ComboBox1.FormattingEnabled = true;
            this.ComboBox1.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7"});
            this.ComboBox1.Location = new System.Drawing.Point(707, 38);
            this.ComboBox1.Name = "ComboBox1";
            this.ComboBox1.Size = new System.Drawing.Size(121, 21);
            this.ComboBox1.TabIndex = 51;
            this.ComboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(727, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 52;
            this.label5.Text = "Select COM Port";
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(1332, 280);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(31, 13);
            this.label13.TabIndex = 53;
            this.label13.Text = "X-Tilt";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1332, 252);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 13);
            this.label7.TabIndex = 54;
            this.label7.Text = "Z-Accel";
            // 
            // chart3
            // 
            chartArea3.BackSecondaryColor = System.Drawing.Color.Transparent;
            chartArea3.Name = "ChartArea1";
            this.chart3.ChartAreas.Add(chartArea3);
            this.chart3.Cursor = System.Windows.Forms.Cursors.Arrow;
            legend3.Name = "Legend1";
            this.chart3.Legends.Add(legend3);
            this.chart3.Location = new System.Drawing.Point(588, 173);
            this.chart3.Name = "chart3";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Radar;
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            series3.XValueMember = "halltxt";
            this.chart3.Series.Add(series3);
            this.chart3.Size = new System.Drawing.Size(212, 150);
            this.chart3.TabIndex = 55;
            this.chart3.Text = "chart3";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1545, 852);
            this.Controls.Add(this.chart3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ComboBox1);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.frametxt);
            this.Controls.Add(this.coursetxt);
            this.Controls.Add(this.timetxt);
            this.Controls.Add(this.halltxt);
            this.Controls.Add(this.zmagtxt);
            this.Controls.Add(this.rssitxt);
            this.Controls.Add(this.speedtxt);
            this.Controls.Add(this.ymagtxt);
            this.Controls.Add(this.ytilttxt);
            this.Controls.Add(this.ztilttxt);
            this.Controls.Add(this.xmagtxt);
            this.Controls.Add(this.xtilttxt);
            this.Controls.Add(this.xacceltxt);
            this.Controls.Add(this.yacceltxt);
            this.Controls.Add(this.zacceltxt);
            this.Controls.Add(this.framenrtxt);
            this.Controls.Add(this.openGLControl1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.asdasdasda);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.alttxt);
            this.Controls.Add(this.psrtxt);
            this.Controls.Add(this.temptxt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DataStream);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.longtxt);
            this.Controls.Add(this.lattxt);
            this.Controls.Add(this.map);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.chart2);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox lattxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox longtxt;
        private System.Windows.Forms.Label label1;
        private GMap.NET.WindowsForms.GMapControl map;
        private System.IO.Ports.SerialPort SerialPort1;
        private System.Windows.Forms.RichTextBox DataStream;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox temptxt;
        private System.Windows.Forms.TextBox psrtxt;
        private System.Windows.Forms.TextBox alttxt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label asdasdasda;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        private SharpGL.OpenGLControl openGLControl1;
        private System.Windows.Forms.TextBox framenrtxt;
        private System.Windows.Forms.TextBox zacceltxt;
        private System.Windows.Forms.TextBox yacceltxt;
        private System.Windows.Forms.TextBox xacceltxt;
        private System.Windows.Forms.TextBox ytilttxt;
        private System.Windows.Forms.TextBox ztilttxt;
        private System.Windows.Forms.TextBox xmagtxt;
        private System.Windows.Forms.TextBox xtilttxt;
        private System.Windows.Forms.TextBox zmagtxt;
        private System.Windows.Forms.TextBox rssitxt;
        private System.Windows.Forms.TextBox speedtxt;
        private System.Windows.Forms.TextBox ymagtxt;
        private System.Windows.Forms.TextBox coursetxt;
        private System.Windows.Forms.TextBox timetxt;
        private System.Windows.Forms.TextBox halltxt;
        private System.Windows.Forms.Label frametxt;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.ComboBox ComboBox1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart3;
    }

}











