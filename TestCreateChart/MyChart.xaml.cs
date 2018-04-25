using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace TestCreateChart
{
    /// <summary>
    /// Interaction logic for MyChart.xaml
    /// </summary>
    public partial class MyChart
    {
        public ObservableCollection<ChartAxisItem> YAxisItems { get; } = new ObservableCollection<ChartAxisItem>();

        public MyChart()
        {
            InitializeComponent();
            ChartCanvas.SizeChanged += ChartCanvas_SizeChanged;
           
        }

        private void ChartCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DrawHorizontalLines();
            YLabelCanvas01.Children.Clear();

            foreach (ChartAxisItem chartAxisItem in YAxisItems)
            {
                var test = chartAxisItem.Header;
                
                if (test is string str)
                {
                    var textBlock = new TextBlock{Text = str};
                    YLabelCanvas01.Children.Add(textBlock);
                    textBlock.SizeChanged += (o, args) =>
                    {
                        Canvas.SetTop(textBlock, Math.Round(ConvertYValueToYCoordinate(chartAxisItem.AxisValue) - textBlock.ActualHeight / 1.95));
                        Canvas.SetLeft(textBlock, YLabelCanvas01.ActualWidth / 2.0 - textBlock.ActualWidth / 2.0);
                    };
                }
            }
        }

        public double HorizontalLineInterval { get; set; } = 100;

        public int MinYValue { get; set; }

        public int MaxYValue { get; set; }

        public int MinXValue { get; set; }

        public int MaxXValue { get; set; }

        public double HeaderMarginPercentage { get; set; } = .05;

        public double FooterMarginMarginPercentage { get; set; } = .05;

        private void DrawHorizontalLines()
        {
            ClearLines();

            double intervalPercentage = HorizontalLineInterval / MaxYValue;
            int pixelLineInterval = (int) Math.Round(GetTotalHeightMinusMargins() * intervalPercentage);
            int startPixel = (int)Math.Round(GetStartPixel());

            int runningTotal = 0;
            for (int pixel = startPixel; pixel >= 0; pixel--)
            {
                if (pixelLineInterval == runningTotal || pixel == startPixel)
                {
                    var line = new Line
                    {
                        X1 = 0,
                        Y1 = pixel,
                        X2 = ChartCanvas.ActualWidth,
                        Y2 = pixel,
                    };

                    ChartCanvas.Children.Add(line);

                    if (pixelLineInterval == runningTotal)
                    {
                        runningTotal = 0;
                    }
                }

                runningTotal++;
            }
        }

        private double GetStartPixel()
        {
            return ChartCanvas.ActualHeight - GetFooterPixelMargin();
        }

        private double GetTotalHeightMinusMargins()
        {
            return ChartCanvas.ActualHeight - GetHeaderPixelMargin() - GetFooterPixelMargin();
        }

        private double GetHeaderPixelMargin()
        {
            return ChartCanvas.ActualHeight * HeaderMarginPercentage;
        }

        private double GetFooterPixelMargin()
        {
            return ChartCanvas.ActualHeight * FooterMarginMarginPercentage;
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
        
        private double ConvertYValueToYCoordinate(double atYValue)
        {
            return GetStartPixel() - (atYValue / MaxYValue * GetTotalHeightMinusMargins());
        }
    }

    public class ChartAxisItem
    {
        public object Header { get; set; }

        public double AxisValue { get; set; }
    }
}
