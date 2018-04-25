using System.Windows;
using System.Windows.Controls;

namespace TestCreateChart
{
    public class ChartAxisItem : Control
    {
        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(double), typeof(ChartAxisItem), new PropertyMetadata(0.0));
        
        static ChartAxisItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ChartAxisItem), new FrameworkPropertyMetadata(typeof(ChartAxisItem)));
        }
    }
}
