using System.Windows.Controls;

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
        }

        public double MinYValue { get; set; }

        public double MaxYValue { get; set; }

        public double MinXValue { get; set; }

        public double MaxXValue { get; set; }

        public int YAxisHeaderWidth { get; set; } = 50;

        public int XAxisHeaderHeight { get; set; } = 25;
    }
}
