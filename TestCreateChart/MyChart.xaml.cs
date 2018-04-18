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

        public double HorizontalLineInterval { get; set; } = 250;

        public int MinYValue { get; set; }

        public int MaxYValue { get; set; }

        public int MinXValue { get; set; }

        public int MaxXValue { get; set; }

        public int HeaderMargin { get; set; } = 50;

        public int FooterMargin { get; set; } = 50;
        
        private void DrawHorizontalLines()
        {
            ClearLines();

            var height = ChartCanvas.ActualHeight;
            double intervalPercentage = HorizontalLineInterval / MaxYValue;
            int runningTotal = 0;

            int pixelLine = (int)(height * intervalPercentage);
            for (int pixel = (int) height; pixel >=0; pixel--)
            {
                if (pixelLine == runningTotal)
                {
                    var line = new Line
                    {
                        X1 = 0,
                        Y1 = pixel,
                        X2 = ActualWidth,
                        Y2 = pixel,
                        Stroke = Brushes.Black,
                        StrokeThickness = 1
                    };

                    ChartCanvas.Children.Add(line);

                    runningTotal = 0;
                }

                runningTotal++;
            }
        }

        private void ClearLines()
        {
            for (int childIndex = ChartCanvas.Children.Count - 1; childIndex >= 0; childIndex--)
            {
                if (ChartCanvas.Children[childIndex] is Line line)
                {
                    ChartCanvas.Children.Remove(line);
                }
            }
        }
    }
}
