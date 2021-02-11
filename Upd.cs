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


namespace CanSatGUI
{
    class Upd
    {
        static GMarkerGoogle landingMarker = new GMarkerGoogle(new PointLatLng(0, 0), GMarkerGoogleType.green);

        public static void UpdateMap(GMapControl map, double latitude, double Longitude, double altitude, double fallingSpeed, double windSpeed, int course)
        {
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

        public static void UpdateOpenGLControl(OpenGLControl control, int triangleCount)
        {
            OpenGL gl = control.OpenGL;
            //processInput(window);

            // clear background 
            gl.ClearColor(11/255f, 18/255f, 34/255f, 1.0f);
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            /*// model
            mat4 model = GLM_MAT4_IDENTITY_INIT;
            float rads = glm_rad(-55.0f);
            vec3 axis = { 1.0f, 0.0f, 0.0f };
            glm_rotate(model, rads, axis);
            int modelLoc = glGetUniformLocation(shaderProgram, "model");
            glUniformMatrix4fv(modelLoc, 1, GL_FALSE, (const GLfloat*)model);

            // view
            mat4 view = GLM_MAT4_IDENTITY_INIT;
            // note that we're translating the scene in the reverse direction of where we want to move
            vec3 translate_vec = { 0.0f, 0.0f, -3.0f };
            glm_translate(view, translate_vec);
            int viewLoc = glGetUniformLocation(shaderProgram, "view");
            glUniformMatrix4fv(viewLoc, 1, GL_FALSE, (const GLfloat*)view);

            // projection
            // mat4 projection = GLM_MAT4_IDENTITY_INIT;
            mat4 projection;
            glm_perspective(glm_rad(45.0f), 800.0f / 600.0f, 0.1f, 100.0f, projection);
            int projectionLoc = glGetUniformLocation(shaderProgram, "projection");
            glUniformMatrix4fv(projectionLoc, 1, GL_FALSE, (const GLfloat*)projection);*/


            // draw triangles
            gl.DrawArrays(OpenGL.GL_TRIANGLES, 0, triangleCount);
            control.Refresh();

            // swap buffers


            
        }
        /* public static void Updatewinddirection(Chart chart, double y, double x)
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
        */
        public static double ConvertToRadians(double angle)
        {
            return Math.PI / 180 * angle;
        }

        public static PointLatLng CalculateLandingLocation(double currentLatitude, double currentLongitude, double altitude, double fallingSpeed, double windSpeed, int course)
        {
            int time = Convert.ToInt32(altitude / fallingSpeed);
            double distance = time * windSpeed;
            double angle = course / 100;
            double valLong = -Math.Sin(ConvertToRadians(angle)) * distance / 0.111 / 1000000;
            double valLat = Math.Cos(ConvertToRadians(angle)) * distance / 0.111 / 1000000;
            double ZoneLat = currentLatitude + valLat;
            double ZoneLong = currentLongitude + valLong;

            return new PointLatLng(ZoneLat, ZoneLong);
        }
    }
}






