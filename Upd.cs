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
using GlmSharp;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Drawing;
// using System.Windows.Media;
using GMap.NET.WindowsForms;
using GMap.NET.MapProviders;
using GMap.NET;
using GMap.NET.Internals;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing.Drawing2D;

namespace CanSatGUI
{
    class Upd
    {
        static GMarkerGoogle landingMarker = new GMarkerGoogle(new PointLatLng(0, 0), GMarkerGoogleType.green);
        static bool isStartingPointSet = false;
        public static void UpdateMap(GMapControl map, double latitude, double Longitude, double altitude, double fallingSpeed, double windSpeed, int course)
        {
            if (latitude == 0 && Longitude == 0)
            {
                return;
            }
            if (!isStartingPointSet)
            {
                map.Position = new PointLatLng(latitude, Longitude);
                isStartingPointSet = true;
            }
            GMapOverlay markersOverlay = map.Overlays[0];
            GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(latitude, Longitude), GMarkerGoogleType.green);
            markersOverlay.Markers.Add(marker);

            // landing location prediction
            if (altitude > 10)
            {
                markersOverlay.Markers.Remove(landingMarker);
                PointLatLng landingLocation = CalculateLandingLocation(latitude, Longitude, altitude, fallingSpeed, windSpeed, course);
                landingMarker = new GMarkerGoogle(landingLocation, GMarkerGoogleType.red);
                markersOverlay.Markers.Add(landingMarker);
            }
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

            mat4 scaleMat = mat4.Scale(3.0f, 3.0f, 3.0f);

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
                180/255f, 180/255f, 180/255f, 1.0f, //os wirnika
                120/255f, 20/255f, 30/255f, 1.0f, // wirnik
                240/255f, 100/255f, 50/255f, 1.0f, //gorna pokrywa
                180/255f, 180/255f, 180/255f, 1.0f, // prawdopodobnie plytka
                180/255f, 180/255f, 180/255f, 1.0f, // prawdopodobnie plytka
                180/255f, 180/255f, 180/255f, 1.0f, // prety
                240/255f, 100/255f, 50/255f, 1.0f, //reszta
                240/255f, 100/255f, 50/255f, 1.0f,
                240/255f, 100/255f, 50/255f, 1.0f,
                240/255f, 100/255f, 50/255f, 1.0f,
                240/255f, 100/255f, 50/255f, 1.0f,
                240/255f, 100/255f, 50/255f, 1.0f,
            };

            int prev = 0;
            for (int i = 0; i < 12; i++)
            {
                int j = i * 4;
                gl.Uniform4(colorLoc, colors[j], colors[j + 1], colors[j + 2], colors[j + 3]);
                gl.DrawArrays(OpenGL.GL_TRIANGLES, prev, part[i]);
                prev = part[i];
            }

            //control.Refresh();
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
            double time = Convert.ToDouble(altitude / fallingSpeed); //Potencjalny blad dzielenia przez 0!!!
            double distance = time * windSpeed;
            double angle = course / 100;
            double valLong = -Math.Sin(ConvertToRadians(angle)) * distance / 0.111 / 1000000;
            double valLat = Math.Cos(ConvertToRadians(angle)) * distance / 0.111 / 1000000;
            double ZoneLat = currentLatitude + valLat;
            double ZoneLong = currentLongitude + valLong;

            return new PointLatLng(ZoneLat, ZoneLong);
        }

        //public static void UpdateChartGauge(Chart chart, TextBox textbox, double val)
        //{
        //    Series s = chart.Series[0];
        //    s.Points.Clear();
        //    s.Points.AddY(90);
        //    s.Points.AddY(0);
        //    s.Points.AddY(0);
        //    s.Points[0].Color = Color.Transparent;
        //    double range;
        //    if (val < 0)
        //    {
        //        range = 500;
        //        val = val * -1;
        //        s.Points[1].Color = Color.FromArgb(0, 179, 0);
        //    }
        //    else
        //    {
        //        range = 100;
        //        s.Points[1].Color = Color.FromArgb(240, 100, 80); // pomaranczowy (łososiowy)
        //    }
        //    s.Points[2].Color = Color.FromArgb(120, 190, 200); // j.niebieski

        //    //double range = valMax - valMin;
        //    //double aRange = 360 - angle;
        //    double aRange = 360 - 90; // 270
        //    double f = aRange / range;


        //    double v1 = val * f;
        //    double v2 = (range - val) * f;

        //    s.Points[1].YValues[0] = v1;
        //    s.Points[2].YValues[0] = v2;

        //    Console.WriteLine(s.Points[0].YValues[0]);
        //    Console.WriteLine(s.Points[1].YValues[0]);
        //    Console.WriteLine(s.Points[2].YValues[0]);
        //    Console.WriteLine("");

        //    textbox.Text = String.Format("{0} m/s", val);
        //    chart.Refresh();
        //}





    }
}






