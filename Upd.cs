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

namespace CanSatGUI
{
    class Upd
    {
        public static void UpdateMap(GMapControl map, double latitude, double longtitude)
        {
            GMapProviders.GoogleMap.ApiKey = @"AIzaSyAZouhXULQgPGPckADOmiHqfCc_YvD5QzQ";
            map.DragButton = MouseButtons.Left;
            map.MapProvider = GMapProviders.GoogleMap;
            map.Position = new PointLatLng(latitude, longtitude);
            map.MinZoom = 0;
            map.MaxZoom = 25;
            map.Zoom = 10;
        }

        public static void UpdateTemperatureChart(Chart chart, double temperature, double secondsElapsed)
        {
            int MaxChartWidth = 10;

            int pointsCount = chart.Series[0].Points.Count;
            Console.WriteLine(pointsCount);
            if (pointsCount >= MaxChartWidth)
            {
                chart.Series[0].Points.RemoveAt(0);
                chart.ResetAutoValues();
            }

            chart.Series[0].Points.AddXY(secondsElapsed, temperature);
            chart.Update();
        }

        public static void UpdatePressureChart(Chart chart, double pressure, double secondsElapsed)
        {
            int MaxChartWidth = 10;


            int pointsCount = chart.Series[0].Points.Count;
            Console.WriteLine(pointsCount);
            if (pointsCount >= MaxChartWidth)
            {

                chart.Series[0].Points.RemoveAt(0);
                chart.ResetAutoValues();
            }

            chart.Series[0].Points.AddXY(secondsElapsed, pressure);
            chart.Update();
        }


        public static void UpdateAltitudeChart(Chart chart, double Altitude, double secondsElapsed)
        {
            int MaxChartWidth = 10;


            int pointsCount = chart.Series[0].Points.Count;
            Console.WriteLine(pointsCount);
            if (pointsCount >= MaxChartWidth)
            {

                chart.Series[0].Points.RemoveAt(0);
                chart.ResetAutoValues();
            }

            chart.Series[0].Points.AddXY(secondsElapsed, Altitude);
            chart.Update();
        }

        public static void UpdateHallChart(Chart chart, double Hall, double secondsElapsed)
        {
            int MaxChartWidth = 10;

            int pointsCount = chart.Series[0].Points.Count;
            Console.WriteLine(pointsCount);
            if (pointsCount >= MaxChartWidth)
            {
                chart.Series[0].Points.RemoveAt(0);
                chart.ResetAutoValues();
            }

            chart.Series[0].Points.AddXY(secondsElapsed, Hall);
            chart.Update();
        }
    }
}
