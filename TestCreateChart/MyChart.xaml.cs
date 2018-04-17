using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TestCreateChart
{
    /// <summary>
    /// Interaction logic for MyChart.xaml
    /// </summary>
    public partial class MyChart : UserControl
    {
        public MyChart()
        {
            InitializeComponent();
            ChartCanvas.SizeChanged += ChartCanvas_SizeChanged;
        }

        private void ChartCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DrawHorizontalLines();
        }

        public int HorizontalLineInterval { get; set; } = 50;

        public int MinYValue { get; set; }

        public int MaxYValue { get; set; }

        public int MinXValue { get; set; }

        public int MaxXValue { get; set; }



        private void DrawHorizontalLines()
        {
            ChartCanvas.Children.Clear();
            var height = ChartCanvas.ActualHeight;
            var heightTotal = MaxYValue - MinYValue;
            int pixelMapping = (int)Math.Round(heightTotal / height);
            int runningTotal = 0;

            for (int index = 0; index <= height; index ++)
            {
                runningTotal += pixelMapping;

                if (runningTotal < HorizontalLineInterval)
                {
                    continue;
                }

                var line = new Line
                {
                    X1 = 0,
                    Y1 = index,
                    X2 = ActualWidth,
                    Y2 = index,
                    Stroke = Brushes.Black,
                    StrokeThickness = 1
                };

                ChartCanvas.Children.Add(line);
                runningTotal = 0;
            }
        }
    }
}
