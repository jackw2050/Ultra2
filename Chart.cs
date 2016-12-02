using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialPortTerminal
{
    class Chart
    {

        
        private void AddSeries()
        {
            frmTerminal frmTerminal = new frmTerminal();
            frmTerminal.GravityChart.Series.Add("Digital Gravity");
            frmTerminal.GravityChart.Series.Add("Spring Tension");
            frmTerminal.GravityChart.Series.Add("Cross Coupling");
            frmTerminal.GravityChart.Series.Add("Raw Beam");
            frmTerminal.GravityChart.Series.Add("Total Correction");
            frmTerminal.GravityChart.Series.Add("AL");
            frmTerminal.GravityChart.Series.Add("AX");
            frmTerminal.GravityChart.Series.Add("VE");
            frmTerminal.GravityChart.Series.Add("AX2");
            frmTerminal.GravityChart.Series.Add("XACC");
            frmTerminal.GravityChart.Series.Add("LACC");
            frmTerminal.GravityChart.Series.Add("Raw Gravity");
        }



        public void SetChartAreaVisibility(string mode)
        {
            frmTerminal frmTerminal = new frmTerminal();
            switch (mode)
            {
                case "normal":
                    frmTerminal.GravityChart.ChartAreas["Gravity"].Visible = true;
                    frmTerminal.GravityChart.ChartAreas["CrossCoupling"].Visible = false;
                    break;

                case "dual":
                    frmTerminal.GravityChart.ChartAreas["Gravity"].Visible = true;
                    frmTerminal.GravityChart.ChartAreas["CrossCoupling"].Visible = true;
                    break;

                default:
                    break;
            }
  
        }

        public void SetChartSeriesLocation(string chartMode)
        {
            frmTerminal frmTerminal = new frmTerminal();
            switch (chartMode)
            {
                case "normal":


                    frmTerminal.GravityChart.Series["Digital Gravity"].ChartArea = "Gravity";
                    frmTerminal.GravityChart.Series["Spring Tension"].ChartArea = "Gravity";
                    frmTerminal.GravityChart.Series["Cross Coupling"].ChartArea = "Gravity";
                    frmTerminal.GravityChart.Series["Total Correction"].ChartArea = "Gravity";
                    frmTerminal.GravityChart.Series["XACC"].ChartArea = "Gravity";
                    frmTerminal.GravityChart.Series["LACC"].ChartArea = "Gravity";

                 

                    frmTerminal.GravityChart.Series["AL"].ChartArea = "CrossCoupling";
                    frmTerminal.GravityChart.Series["AX"].ChartArea = "CrossCoupling";
                    frmTerminal.GravityChart.Series["VE"].ChartArea = "CrossCoupling";
                    frmTerminal.GravityChart.Series["AX2"].ChartArea = "CrossCoupling";
                    // frmTerminal.GravityChart.Series["XACC"].ChartArea = "CrossCoupling";
                    // frmTerminal.GravityChart.Series["LACC"].ChartArea = "CrossCoupling";
                    frmTerminal.GravityChart.Series["Raw Gravity"].ChartArea = "CrossCoupling";
                    frmTerminal.GravityChart.Series["Raw Beam"].ChartArea = "CrossCoupling";

                 
                    break;
                case "dual":

                default:
                    break;
            }
        }

        public void SetChartSeriesLegendLocation(string chartMode)
        {
            frmTerminal frmTerminal = new frmTerminal();
            switch (chartMode)
            {
                case "normal":
                    frmTerminal.GravityChart.Series["Digital Gravity"].Legend = "Gravity Legend";
                    frmTerminal.GravityChart.Series["Spring Tension"].Legend = "Gravity Legend";
                    frmTerminal.GravityChart.Series["Cross Coupling"].Legend = "Gravity Legend";
                    frmTerminal.GravityChart.Series["Total Correction"].Legend = "Gravity Legend";
                    frmTerminal.GravityChart.Series["XACC"].Legend = "Gravity Legend";
                    frmTerminal.GravityChart.Series["LACC"].Legend = "Gravity Legend";


                    break;
                case "dual":
                    frmTerminal.GravityChart.Series["Digital Gravity"].Legend = "Gravity Legend";
                    frmTerminal.GravityChart.Series["Spring Tension"].Legend = "Gravity Legend";
                    frmTerminal.GravityChart.Series["Cross Coupling"].Legend = "Gravity Legend";
                    frmTerminal.GravityChart.Series["Total Correction"].Legend = "Gravity Legend";
                    frmTerminal.GravityChart.Series["XACC"].Legend = "Gravity Legend";
                    frmTerminal.GravityChart.Series["LACC"].Legend = "Gravity Legend";

                    frmTerminal.GravityChart.Series["AL"].Legend = "Cross Coupling Legend";
                    frmTerminal.GravityChart.Series["AX"].Legend = "Cross Coupling Legend";
                    frmTerminal.GravityChart.Series["VE"].Legend = "Cross Coupling Legend";
                    frmTerminal.GravityChart.Series["AX2"].Legend = "Cross Coupling Legend";
                    frmTerminal.GravityChart.Series["Raw Gravity"].Legend = "Cross Coupling Legend";
                    frmTerminal.GravityChart.Series["Raw Beam"].Legend = "Cross Coupling Legend";
                    break;
                default:
                    break;
            }
        }

        public void AssignChartSeriesAxis(string chartMode)
        {
            frmTerminal frmTerminal = new frmTerminal();
            // Assign series to secondary axis
            frmTerminal.GravityChart.Series["XACC"].YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            frmTerminal.GravityChart.Series["LACC"].YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            //     frmTerminal.GravityChart.Series["Cross Coupling"].YAxisType    = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            frmTerminal.GravityChart.Series["Total Correction"].YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
        }

        public void SetupChartSeries(string chartMode)
        {
            frmTerminal frmTerminal = new frmTerminal();
            frmTerminal.GravityChart.ChartAreas["Gravity"].AxisX.IsMarginVisible = false;
            frmTerminal.GravityChart.ChartAreas["Gravity"].AxisX.LabelStyle.Format = "HH:mm:ss";  //  "yyyy-MM-dd HH:mm:ss";
            frmTerminal.GravityChart.ChartAreas["Gravity"].AxisX.LabelStyle.Angle = 0;



            frmTerminal.GravityChart.Series["Digital Gravity"].XValueMember = "date";
            frmTerminal.GravityChart.Series["Digital Gravity"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            frmTerminal.GravityChart.Series["Digital Gravity"].YValueMembers = "digitalGravity";
            frmTerminal.GravityChart.Series["Digital Gravity"].BorderWidth = 4;

            frmTerminal.GravityChart.Series["Spring Tension"].XValueMember = "date";
            frmTerminal.GravityChart.Series["Spring Tension"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            frmTerminal.GravityChart.Series["Spring Tension"].YValueMembers = "springTension";
            frmTerminal.GravityChart.Series["Spring Tension"].BorderWidth = 4;

            frmTerminal.GravityChart.Series["Cross Coupling"].XValueMember = "date";
            frmTerminal.GravityChart.Series["Cross Coupling"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            frmTerminal.GravityChart.Series["Cross Coupling"].YValueMembers = "crossCoupling";
            frmTerminal.GravityChart.Series["Cross Coupling"].BorderWidth = 4;

            frmTerminal.GravityChart.Series["Raw Beam"].XValueMember = "date";
            frmTerminal.GravityChart.Series["Raw Beam"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            frmTerminal.GravityChart.Series["Raw Beam"].YValueMembers = "RawBeam";
            frmTerminal.GravityChart.Series["Raw Beam"].BorderWidth = 4;

            frmTerminal.GravityChart.Series["Total Correction"].XValueMember = "date";
            frmTerminal.GravityChart.Series["Total Correction"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            frmTerminal.GravityChart.Series["Total Correction"].YValueMembers = "totalCorrection";
            frmTerminal.GravityChart.Series["Total Correction"].BorderWidth = 4;

            frmTerminal.GravityChart.Series["AL"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            frmTerminal.GravityChart.Series["AL"].XValueMember = "date";
            frmTerminal.GravityChart.Series["AL"].YValueMembers = "AL";
            frmTerminal.GravityChart.Series["AL"].BorderWidth = 2;

            frmTerminal.GravityChart.Series["AX"].XValueMember = "date";
            frmTerminal.GravityChart.Series["AX"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            frmTerminal.GravityChart.Series["AX"].YValueMembers = "AX";
            frmTerminal.GravityChart.Series["AX"].BorderWidth = 2;

            frmTerminal.GravityChart.Series["VE"].XValueMember = "date";
            frmTerminal.GravityChart.Series["VE"].YValueMembers = "VE";
            frmTerminal.GravityChart.Series["VE"].BorderWidth = 2;

            frmTerminal.GravityChart.Series["AX2"].XValueMember = "date";
            frmTerminal.GravityChart.Series["AX2"].YValueMembers = "AX2";
            frmTerminal.GravityChart.Series["AX2"].BorderWidth = 2;

            frmTerminal.GravityChart.Series["XACC"].XValueMember = "date";
            frmTerminal.GravityChart.Series["XACC"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            frmTerminal.GravityChart.Series["XACC"].YValueMembers = "XACC";
            frmTerminal.GravityChart.Series["XACC"].BorderWidth = 2;

            frmTerminal.GravityChart.Series["LACC"].XValueMember = "date";
            frmTerminal.GravityChart.Series["LACC"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            frmTerminal.GravityChart.Series["LACC"].YValueMembers = "LACC";
            frmTerminal.GravityChart.Series["LACC"].BorderWidth = 2;

            frmTerminal.GravityChart.Series["Raw Gravity"].XValueMember = "date";
            frmTerminal.GravityChart.Series["Raw Gravity"].YValueMembers = "rawGravity";// rAwg
            frmTerminal.GravityChart.Series["Raw Gravity"].BorderWidth = 2;

        }

        public void SetupChart()
        {
            frmTerminal frmTerminal = new frmTerminal();

            AddSeries();
            SetChartAreaVisibility("dual");
            SetChartSeriesLocation("normal");// Select chart area for series
            SetChartSeriesLegendLocation("normal");
            AssignChartSeriesAxis("normal");
            SetupChartSeries("normal");



            frmTerminal.GravityChart.Titles.Add("Gravity");// Should probably change frmTerminal

            

            //      SETUP MAIN PAIGE GRAVITY CHART
       

            frmTerminal.GravityChart.ChartAreas["Gravity"].AxisX.ScaleView.Zoom(2, 3);
            frmTerminal.GravityChart.ChartAreas["Gravity"].AxisX.ScaleView.ZoomReset(1);
            frmTerminal.GravityChart.ChartAreas["Gravity"].AxisY.ScaleView.ZoomReset(1);
            frmTerminal.GravityChart.ChartAreas["Gravity"].AxisX.LabelStyle.Angle = 0;// can vary from -90 to + 90

            frmTerminal.GravityChart.ChartAreas["Gravity"].AxisX.ScaleView.Zoom(2, 3);
            frmTerminal.GravityChart.ChartAreas["Gravity"].AxisX.ScaleView.ZoomReset(1);
            frmTerminal.GravityChart.ChartAreas["Gravity"].AxisY2.ScaleView.ZoomReset(1);
            frmTerminal.GravityChart.ChartAreas["Gravity"].AxisX.LabelStyle.Angle = 0;// can vary from -90 to + 90

            //Enable range selection and zooming end user interface

            frmTerminal.GravityChart.ChartAreas["Gravity"].CursorX.IsUserEnabled = true;
            frmTerminal.GravityChart.ChartAreas["Gravity"].CursorY.IsUserEnabled = true;
            frmTerminal.GravityChart.ChartAreas["Gravity"].CursorX.IsUserSelectionEnabled = true;
            frmTerminal.GravityChart.ChartAreas["Gravity"].CursorY.IsUserSelectionEnabled = true;
            frmTerminal.GravityChart.ChartAreas["Gravity"].AxisX.ScaleView.Zoomable = true;
            frmTerminal.GravityChart.ChartAreas["Gravity"].AxisY.ScaleView.Zoomable = true;
            frmTerminal.GravityChart.ChartAreas["Gravity"].AxisX.ScrollBar.IsPositionedInside = true;
            frmTerminal.GravityChart.ChartAreas["Gravity"].AxisY.ScrollBar.IsPositionedInside = true;

            //      SETUP FLOATING CROSS COUPLING CHART
            //         frmTerminal.GravityChart.Series.Add("Raw Gravity");

            //frmTerminal.GravityChart.ChartAreas["CrossCoupling"].AxisX.IsMarginVisible = false;
            //frmTerminal.GravityChart.ChartAreas["CrossCoupling"].AxisX.LabelStyle.Format = "HH:mm:ss"; ; //  "yyyy-MM-dd HH:mm:ss";
            //frmTerminal.GravityChart.ChartAreas["CrossCoupling"].AxisX.LabelStyle.Angle = 0;

       

            frmTerminal.GravityChart.ChartAreas["CrossCoupling"].AxisX.ScaleView.Zoom(2, 3);
            frmTerminal.GravityChart.ChartAreas["CrossCoupling"].AxisX.ScaleView.ZoomReset(1);
            frmTerminal.GravityChart.ChartAreas["CrossCoupling"].AxisY.ScaleView.ZoomReset(1);
            frmTerminal.GravityChart.ChartAreas["CrossCoupling"].AxisX.LabelStyle.Angle = 0;// can vary from -90 to + 90
            //Enable range selection and zooming end user interface

            frmTerminal.GravityChart.ChartAreas["CrossCoupling"].CursorX.IsUserEnabled = true;
            frmTerminal.GravityChart.ChartAreas["CrossCoupling"].CursorY.IsUserEnabled = true;
            frmTerminal.GravityChart.ChartAreas["CrossCoupling"].CursorX.IsUserSelectionEnabled = true;
            frmTerminal.GravityChart.ChartAreas["CrossCoupling"].CursorY.IsUserSelectionEnabled = true;
            frmTerminal.GravityChart.ChartAreas["CrossCoupling"].AxisX.ScaleView.Zoomable = true;
            frmTerminal.GravityChart.ChartAreas["CrossCoupling"].AxisY.ScaleView.Zoomable = true;
            frmTerminal.GravityChart.ChartAreas["CrossCoupling"].AxisX.ScrollBar.IsPositionedInside = true;
            frmTerminal.GravityChart.ChartAreas["CrossCoupling"].AxisY.ScrollBar.IsPositionedInside = true;

            //    SetTraceColor(Properties.Settings.Default

            frmTerminal.SetChartType();
            frmTerminal.SetTraceColor("bright");// Properties.Settings.Default.tracePalette);//              Set trace color palette
            frmTerminal.SetChartAreaColors(2);// Properties.Settings.Default.chartColor);//           Set chart background color
            frmTerminal.SetChartBorderWidth(2); // Properties.Settings.Default.traceWidth);//          Set trace width
            frmTerminal.ChartMarkers(false);// Properties.Settings.Default.traceMarkers);//               Enable/ disable trace markers
            frmTerminal.SetLegendLocation();// Properties.Settings.Default.chartLegendLocation);//   Set legend location
            frmTerminal.ExtraChartStuff();
            frmTerminal.SetChartToolTips();
            frmTerminal.SetChartCursors();
            frmTerminal.SetChartZoom();
            frmTerminal.SetChartScroll();
            frmTerminal.SetChartAxis(false);
            frmTerminal.SetChartColors();
            //          SetLegend();
        }


    }
}
