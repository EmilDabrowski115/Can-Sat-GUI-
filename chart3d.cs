using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChartDirector;
using System.Drawing;


namespace CanSatGUI
{
    public class threedscatter
    {
        ThreeDScatterChart c;
        WinChartViewer viewer;
        SizeF scale { get; set; }
        List<double> xData_list = new List<double>();
        List<double> yData_list = new List<double>();
        List<double> zData_list = new List<double>();
        double[] xData = new double[] { };
        double[] yData = new double[] { };
        double[] zData = new double[] { };

        //Main code for creating chart.
        //Note: the argument chartIndex is unused because this demo only has 1 chart.
        public void createChart(WinChartViewer viewer, SizeF scale)
        {
            this.viewer = viewer;
            this.scale = scale;
            // The XYZ data for the 3D scatter chart as 3 random data series
            
            // Create a ThreeDScatterChart object
            c = new ThreeDScatterChart(
                Convert.ToInt32(424 * scale.Width), 
                Convert.ToInt32(352 * scale.Height)
            );

            c.setBackground(725538, 725538, 0);//7913160 jasny niebieski 725538 ciemny

            float labelFontSize = 9 * scale.Height;
            c.xAxis().setLabelStyle("Arial Bold", labelFontSize, 7913160); 
            c.yAxis().setLabelStyle("Arial Bold", labelFontSize, 7913160);
            c.zAxis().setLabelStyle("Arial Bold", labelFontSize, 7913160);
            
            // Add a title to the chart using 20 points Times New Roman Italic font
            c.addTitle("3D Scatter Chart ", "Calibri", labelFontSize * 2, 7913160);

            // Set the center of the plot region at (350, 280), and set width x depth x height to
            // 360 x 360 x 270 pixels
            //c.setPlotRegion(212, 176, 180, 180, 135);
            c.setPlotRegion(
                Convert.ToInt32(212 * scale.Width),
                Convert.ToInt32(176 * scale.Height),
                Convert.ToInt32(180 * scale.Height),
                Convert.ToInt32(180 * scale.Height),
                Convert.ToInt32(135 * scale.Height)
            );

            // Color palette for the legend
            //int[] color_palette = new int[] { 15754320, 14643553, 13532786, 12422019, 11245461, 10134694, 9023927, 7913160 }; //inverted
            int[] color_palette = new int[] { 2461197, 9029689, 16638029, 16621366, 16736768 };


            // Add a scatter group to the chart using 11 pixels glass sphere symbols, in which the
            // color depends on the z value of the symbol

            c.addScatterGroup(xData, yData, zData, "", Chart.GlassSphere2Shape, 11, Chart.SameAsMainColor);

            //TODO:
            //Set the point color by the wind speed

            // Wall thicknes, wall color, wall grid
            c.setWallThickness(1, 1, 1);
            c.setWallColor(725538, 725538, 725538);
            c.setWallGrid(7913160, 7913160, 7913160);

            // Add a color axis (the legend) in which the left center is anchored at (645, 270). Set
            // the length to 200 pixels and the labels on the right side.
            // Set the colors of the axis elements

            c.setColorAxis(
                Convert.ToInt32(370 * scale.Width),
                Convert.ToInt32(175 * scale.Height),
                Chart.Left, 100, Chart.Right
            ).setColors(725538, 7913160, 7913160, 7913160);
            
            //set gradient to true and the color palette for the color axis,
            c.colorAxis().setColorGradient(true, color_palette);
  
            // Set the x, y and z axis titles using 10 points Arial Bold font
            c.xAxis().setTitle("Longitude", "Calibri", labelFontSize, 7913160);
            c.yAxis().setTitle("Latitude", "Calibri", labelFontSize, 7913160);
            c.zAxis().setTitle("Altitude", "Calibri", labelFontSize, 7913160);

            // Output the chart
            viewer.Chart = c;

            //include tool tip for the chart
            viewer.ImageMap = c.getHTMLImageMap("clickable", "",
                "title='(x={x|p}, y={y|p}, z={z|p}'");

            
        }

        public void Update(double latitude, double longitude, double altitude)
        {
            // Output the chart
            //include tool tip for the chart
            xData_list.Add(latitude);
            yData_list.Add(longitude);
            zData_list.Add(altitude);

            xData = xData_list.ToArray();
            yData = yData_list.ToArray();
            zData = zData_list.ToArray();

            createChart(viewer, scale);
            // ///viewer.ImageMap = c.getHTMLImageMap("clickable", "",
            ///   "title='(x={x|p}, y={y|p}, z={z|p}'");
        }

        

    }

   
}