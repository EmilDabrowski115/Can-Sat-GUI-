using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
using System.Windows.Forms;
//using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET;
using System.Windows.Forms.DataVisualization.Charting; // Chart
using GMap.NET.WindowsForms; // GMapControl
using SharpGL;
using GMap.NET.WindowsForms.Markers;
using ILNumerics;
using ILNumerics.Drawing;
using static ILNumerics.ILMath;
using ILNumerics.Drawing.Plotting;
using GlmSharp;
using ILNumerics.Toolboxes;
using ILNEditor;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;








namespace CanSatGUI
{
    class Upd
    {
        static GMarkerGoogle landingMarker = new GMarkerGoogle(new PointLatLng(0, 0), GMarkerGoogleType.green);
        static bool isStartingPointSet = false;
        public static void UpdateMap(GMapControl map, double latitude, double Longitude, double altitude, double fallingSpeed, double windSpeed, int course)
        {
            if (!isStartingPointSet)
            {
                map.Position = new PointLatLng(latitude, Longitude);
                isStartingPointSet = true;
            }
            GMapOverlay markersOverlay = map.Overlays[0];
            GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(latitude, Longitude), GMarkerGoogleType.green);
            markersOverlay.Markers.Add(marker);

            //double currentLatitude, double currentLongitude, double altitude, double fallingSpeed, double windSpeed, double course
            PointLatLng landingLocation = CalculateLandingLocation(latitude, Longitude, altitude, fallingSpeed, windSpeed, course);
            markersOverlay.Markers.Remove(landingMarker);
            landingMarker = new GMarkerGoogle(landingLocation, GMarkerGoogleType.red);
            markersOverlay.Markers.Add(landingMarker);


            //GMapOverlay markersOverlay = new GMapOverlay("markers");
            //GMarkerGoogleType.red);
        }

        public static void UpdateChart(Chart chart, double y, double x)
        {
            int MaxChartWidth = 50;

            int pointsCount = chart.Series[0].Points.Count;
            if (pointsCount >= MaxChartWidth)
            {
                chart.Series[0].Points.RemoveAt(0);
                chart.ResetAutoValues();
            }
            chart.Series[0].Points.AddXY(x, y);
            chart.Update();
        }

        public static void UpdateOpenGLControl(OpenGLControl control, uint shaderProgram, 
            int triangleCount, float pitch, float yaw, float roll)
        {
            OpenGL gl = control.OpenGL;

            // clear background 
            gl.ClearColor(11 / 255f, 18 / 255f, 34 / 255f, 1.0f);
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            // model
            mat4 model = mat4.Identity;

            vec3 pitchAxis = new vec3(1.0f, 0.0f, 0.0f);
            vec3 yawAxis = new vec3(0.0f, 1.0f, 0.0f);
            vec3 rollAxis = new vec3(0.0f, 0.0f, 1.0f);

            mat4 pitchMat = mat4.Rotate(glm.Radians(pitch), pitchAxis);
            mat4 yawMat = mat4.Rotate(glm.Radians(yaw), yawAxis);
            mat4 rollMat = mat4.Rotate(glm.Radians(roll), rollAxis);

            mat4 scaleMat = mat4.Scale(2.0f, 2.0f, 2.0f);

            model = pitchMat * yawMat * rollMat * scaleMat * model;
            int modelLoc = gl.GetUniformLocation(shaderProgram, "model");
            gl.UniformMatrix4(modelLoc, 1, false, model.Values1D);


            //// view
            //mat4 view = mat4.Identity;
            //// note that we're translating the scene in the reverse direction of where we want to move
            //vec3 translate_vec = { 0.0f, 0.0f, -3.0f };
            //glm_translate(view, translate_vec);
            //int viewLoc = glGetUniformLocation(shaderProgram, "view");
            //glUniformMatrix4fv(viewLoc, 1, GL_FALSE, (const GLfloat*)view);


            // projection
            // mat4 projection = GLM_MAT4_IDENTITY_INIT;
            //mat4 projection;
            //glm_perspective(glm_rad(45.0f), 800.0f / 600.0f, 0.1f, 100.0f, projection);
            //int projectionLoc = glGetUniformLocation(shaderProgram, "projection");
            //glUniformMatrix4fv(projectionLoc, 1, GL_FALSE, (const GLfloat*)projection);


            // draw triangles
            int colorLoc = gl.GetUniformLocation(shaderProgram, "materialColor");

            int[] part = { 1428, 5454, 6594, 7734, 8106, 9594, 9630, 10002, 10374, 10410, 10446, 10818 };

            float[] colors = {
                0.1f, 0.9f, 0.5f, 1.0f,
                0.2f, 0.8f, 0.3f, 1.0f,
                0.3f, 0.7f, 0.1f, 1.0f,
                0.4f, 0.6f, 0.9f, 1.0f,
                0.5f, 0.5f, 0.7f, 1.0f,
                0.6f, 0.4f, 0.5f, 1.0f,
                0.7f, 0.3f, 0.3f, 1.0f,
                0.8f, 0.2f, 0.1f, 1.0f,
                0.9f, 0.1f, 0.8f, 1.0f,
                0.1f, 0.2f, 0.6f, 1.0f,
                0.2f, 0.3f, 0.4f, 1.0f,
                0.3f, 0.4f, 0.2f, 1.0f,
            };

            int prev = 0;
            for (int i = 0; i < 12; i++)
            {
                int j = i * 4;
                gl.Uniform4(colorLoc, colors[j], colors[j + 1], colors[j + 2], colors[j + 3]);
                gl.DrawArrays(OpenGL.GL_TRIANGLES, prev, part[i]);
                prev = part[i];
            }

            control.Refresh();
        }
         public static void Updatewinddirection(Chart chart, double y, double x)
         {
             int MaxChartWidth = 50;

             int pointsCount = chart.Series[0].Points.Count;
             if (pointsCount >= MaxChartWidth)
             {
                 chart.Series[0].Points.RemoveAt(0);
                 chart.ResetAutoValues();
             }
             chart.Series[0].Points.AddXY(x, y);
             chart.Update();
         }
        
        public static double ConvertToRadians(double angle)
        {
            return Math.PI / 180 * angle;
        }

        public static PointLatLng CalculateLandingLocation(double currentLatitude, double currentLongitude, double altitude, double fallingSpeed, double windSpeed, int course)
        {
            int time = Convert.ToInt32(altitude / fallingSpeed); //Potencjalny blad dzielenia przez 0!!!
            double distance = time * windSpeed;
            double angle = course / 100;
            double valLong = -Math.Sin(ConvertToRadians(angle)) * distance / 0.111 / 1000000;
            double valLat = Math.Cos(ConvertToRadians(angle)) * distance / 0.111 / 1000000;
            double ZoneLat = currentLatitude + valLat;
            double ZoneLong = currentLongitude + valLong;

            return new PointLatLng(ZoneLat, ZoneLong);
        }

        
        
        //public static void Update3DChart()
        //{
        //    var colors = new[] { Color.Red, Color.Black, Color.Blue, Color.Green};

        //    ILArray<float> data = ILMath.zeros<float>(
        //      3,
        //      colors.Length);

        //    ILArray<float> colorData = ILMath.zeros<float>(
        //      3,
        //      colors.Length);

        //    int index = 0;
        //    foreach (var p in colors)
        //    {
        //        data[0, index] = p.GetHue();
        //        data[1, index] = p.GetSaturation();
        //        data[2, index] = p.GetBrightness();
        //        colorData[0, index] = p.R / 255.0f;
        //        colorData[1, index] = p.G / 255.0f;
        //        colorData[2, index] = p.B / 255.0f;
        //        index++;
        //    }

        //    var points = new ILPoints()
        //    {
        //        Positions = data,
        //        Colors = colorData
        //    };

        //    points.Color = null;

        //    var plot = new ILPlotCube(twoDMode: false)
        //    {
        //        Rotation = Matrix4.Rotation(new Vector3(1, 1, 0.1f), 0.4f),
        //        Projection = Projection.Orthographic,
        //        Children = { points }
        //    };

        //    plot.Axes[0].Label.Text = "Hue";
        //    plot.Axes[1].Label.Text = "Saturation";
        //    plot.Axes[2].Label.Text = "Brightness";

        //    ilPanel1.Scene = new ILScene { plot };
        //}
        
    }
}






