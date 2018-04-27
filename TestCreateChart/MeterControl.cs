using System.Windows;
using System.Windows.Controls;

namespace TestCreateChart
{
    [TemplatePart(Name = UpperButton, Type = typeof(Button))]
    [TemplatePart(Name = LowerButton, Type = typeof(Button))]
    public class MeterControl : Control
    {
        private const string UpperButton = "PART_UpperButton";
        private const string LowerButton = "PART_LowerButton";
        private Button _upperButton;
        private Button _lowerButton;

        public double MaxValue
        {
            get => (double)GetValue(MaxValueProperty);
            set => SetValue(MaxValueProperty, value);
        }

        // Using a DependencyProperty as the backing store for MaxValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxValueProperty = DependencyProperty.Register("MaxValue", typeof(double), typeof(MeterControl), new PropertyMetadata(100.0));
        
        public double MinValue
        {
            get => (double)GetValue(MinValueProperty);
            set => SetValue(MinValueProperty, value);
        }

        // Using a DependencyProperty as the backing store for MinValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinValueProperty = DependencyProperty.Register("MinValue", typeof(double), typeof(MeterControl), new PropertyMetadata(0.0));
        
        public double UpperValue
        {
            get => (double)GetValue(UpperValueProperty);
            set => SetValue(UpperValueProperty, value);
        }

        // Using a DependencyProperty as the backing store for UpperValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UpperValueProperty = DependencyProperty.Register("UpperValue", typeof(double), typeof(MeterControl), new PropertyMetadata(100.0));
        
        public double LowerValue
        {
            get => (double)GetValue(LowerValueProperty);
            set => SetValue(LowerValueProperty, value);
        }

        // Using a DependencyProperty as the backing store for LowerValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LowerValueProperty = DependencyProperty.Register("LowerValue", typeof(double), typeof(MeterControl), new PropertyMetadata(0.0));
        

        static MeterControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MeterControl), new FrameworkPropertyMetadata(typeof(MeterControl)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _upperButton = (Button)GetTemplateChild(UpperButton);
            _lowerButton = (Button)GetTemplateChild(LowerButton);
        }

        private void SetUpperButtonPosition()
        {
            double totalValues = MaxValue - MinValue;
          
        }
    }
}
