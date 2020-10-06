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
            this.txtLat = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLong = new System.Windows.Forms.TextBox();
            this.MapSearch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.map = new GMap.NET.WindowsForms.GMapControl();
            this.SerialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.DataStream = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tmptxt = new System.Windows.Forms.TextBox();
            this.psrtxt = new System.Windows.Forms.TextBox();
            this.hghttxt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtLat
            // 
            this.txtLat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLat.Location = new System.Drawing.Point(233, 27);
            this.txtLat.Name = "txtLat";
            this.txtLat.Size = new System.Drawing.Size(100, 20);
            this.txtLat.TabIndex = 1;
            this.txtLat.TextChanged += new System.EventHandler(this.txtLat_TextChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(230, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Longitude";
            // 
            // txtLong
            // 
            this.txtLong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLong.Location = new System.Drawing.Point(233, 75);
            this.txtLong.Name = "txtLong";
            this.txtLong.Size = new System.Drawing.Size(100, 20);
            this.txtLong.TabIndex = 3;
            this.txtLong.TextChanged += new System.EventHandler(this.txtLong_TextChanged);
            // 
            // MapSearch
            // 
            this.MapSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MapSearch.Location = new System.Drawing.Point(247, 119);
            this.MapSearch.Name = "MapSearch";
            this.MapSearch.Size = new System.Drawing.Size(75, 23);
            this.MapSearch.TabIndex = 5;
            this.MapSearch.Text = "Find Me";
            this.MapSearch.UseVisualStyleBackColor = true;
            this.MapSearch.Click += new System.EventHandler(this.MapSearch_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(230, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Latitude";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // map
            // 
            this.map.Bearing = 0F;
            this.map.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.map.CanDragMap = true;
            this.map.EmptyTileColor = System.Drawing.Color.Navy;
            this.map.GrayScaleMode = false;
            this.map.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.map.LevelsKeepInMemmory = 5;
            this.map.Location = new System.Drawing.Point(0, 0);
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
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // tmptxt
            // 
            this.tmptxt.Location = new System.Drawing.Point(781, 23);
            this.tmptxt.Name = "tmptxt";
            this.tmptxt.Size = new System.Drawing.Size(100, 20);
            this.tmptxt.TabIndex = 10;
            // 
            // psrtxt
            // 
            this.psrtxt.Location = new System.Drawing.Point(781, 75);
            this.psrtxt.Name = "psrtxt";
            this.psrtxt.Size = new System.Drawing.Size(100, 20);
            this.psrtxt.TabIndex = 11;
            // 
            // hghttxt
            // 
            this.hghttxt.Location = new System.Drawing.Point(781, 49);
            this.hghttxt.Name = "hghttxt";
            this.hghttxt.Size = new System.Drawing.Size(100, 20);
            this.hghttxt.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(707, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Temperature";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(707, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Height";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(707, 81);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Pressure";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1107, 648);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.hghttxt);
            this.Controls.Add(this.psrtxt);
            this.Controls.Add(this.tmptxt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DataStream);
            this.Controls.Add(this.map);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MapSearch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtLong);
            this.Controls.Add(this.txtLat);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtLat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLong;
        private System.Windows.Forms.Button MapSearch;
        private System.Windows.Forms.Label label1;
        private GMap.NET.WindowsForms.GMapControl map;
        private System.IO.Ports.SerialPort SerialPort1;
        private System.Windows.Forms.RichTextBox DataStream;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tmptxt;
        private System.Windows.Forms.TextBox psrtxt;
        private System.Windows.Forms.TextBox hghttxt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}

