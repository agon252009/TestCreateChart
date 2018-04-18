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

        public double HeaderMarginPercentage { get; set; } = .05;

        public double FooterMarginMarginPercentage { get; set; } = .05;

        private void DrawHorizontalLines()
        {
            ClearLines();

            double height = ChartCanvas.ActualHeight;
            double headerPixelMargin = height * HeaderMarginPercentage;
            double footerPixelmargin = height * FooterMarginMarginPercentage;
            double intervalPercentage = HorizontalLineInterval / MaxYValue;
            int runningTotal = 0;

            int totalPixels = (int) (height - headerPixelMargin - footerPixelmargin);
            int pixelLine = (int)(totalPixels * intervalPercentage);
            int startPixel = (int) ((int) height - footerPixelmargin);

            for (int pixel = startPixel; pixel >= 0; pixel--)
            {
                if (pixelLine == runningTotal || pixel == startPixel)
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

                    if (pixelLine == runningTotal)
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
