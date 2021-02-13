using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChartDirector;




namespace CanSatGUI
{
    public class threedscatter
    {
        ThreeDScatterChart c;
        WinChartViewer viewer;
        double[] xData = new double[100000];
        double[] yData = new double[100000];
        double[] zData = new double[100000];
        //List<double> xData;
        //List<double> yData;
        //List<double> zData;

        //Main code for creating chart.
        //Note: the argument chartIndex is unused because this demo only has 1 chart.
        public void createChart(WinChartViewer viewer)
        {
            this.viewer = viewer;
            // The XYZ data for the 3D scatter chart as 3 random data series
            
            // Create a ThreeDScatterChart object of size 720 x 600 pixels
            c = new ThreeDScatterChart(424,352);

            c.setBackground(725538, 725538, 0);//7913160 jasny niebieski 725538 ciemny

            c.xAxis().setLabelStyle("Arial Bold", 7913160);
            c.yAxis().setLabelStyle("Arial Bold", 7913160);
            c.zAxis().setLabelStyle("Arial Bold", 7913160);

            // Add a title to the chart using 20 points Times New Roman Italic font
            c.addTitle("3D Scatter Chart ", "Times New Roman Italic", 20, 7913160);

            // Set the center of the plot region at (350, 280), and set width x depth x height to
            // 360 x 360 x 270 pixels
            c.setPlotRegion(212, 176, 180, 180, 135);

            // Add a scatter group to the chart using 11 pixels glass sphere symbols, in which the
            // color depends on the z value of the symbol
            c.addScatterGroup(xData, yData, zData, "", Chart.GlassSphere2Shape, 11, Chart.SameAsMainColor);

            // Add a color axis (the legend) in which the left center is anchored at (645, 270). Set
            // the length to 200 pixels and the labels on the right side.
            c.setColorAxis(370, 175, Chart.Left, 100, Chart.Right);

            // Set the x, y and z axis titles using 10 points Arial Bold font
            c.xAxis().setTitle("Longitude", "Arial Bold", 10, 7913160);
            c.yAxis().setTitle("Latitude", "Arial Bold", 10, 7913160);
            c.zAxis().setTitle("Altitude", "Arial Bold", 10, 7913160);

            // Output the chart
            this.viewer.Chart = c;

            //include tool tip for the chart
            viewer.ImageMap = c.getHTMLImageMap("clickable", "",
                "title='(x={x|p}, y={y|p}, z={z|p}'");

            
        }

        public void Update(double latitude, double longitude, double altitude)
        {
            // Output the chart
            //include tool tip for the chart
            int len = xData.Length;
            xData[len] = latitude;
            yData[len] = longitude;
            zData[len] = altitude;
            viewer.ImageMap = c.getHTMLImageMap("clickable", "",
                "title='(x={x|p}, y={y|p}, z={z|p}'");
        }
    }

   
}