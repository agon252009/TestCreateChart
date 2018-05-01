using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TestCreateChart
{
    [TemplatePart(Name = UpperButtonName, Type = typeof(Button))]
    [TemplatePart(Name = LowerButtonName, Type = typeof(Button))]
    public class MeterControl : Control
    {
        private const string UpperButtonName = "PART_UpperButton";
        private const string LowerButtonName = "PART_LowerButton";
        private Button _upperButton;
        private Button _lowerButton;
        private double? _previousMouseYLocation;
        private Button _draggingButton;

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

        public MeterControl()
        {
            PreviewMouseMove += OnPreviewMouseMove;
            PreviewMouseLeftButtonUp += OnPreviewMouseLeftButtonUp;
        }

        private void OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _previousMouseYLocation = null;
            _draggingButton = null;
        }

        private void OnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (_draggingButton == null)
            {
                return;
            }

            double yPos = e.GetPosition(this).Y;
            double locationDelta = yPos - _previousMouseYLocation ?? 0;
            double newLocation = Math.Min(Math.Max(Canvas.GetTop(_draggingButton) + locationDelta, 0), GetHeightMinusButtonsHeight());
            Canvas.SetTop(_draggingButton, newLocation);
            _previousMouseYLocation = yPos;
            if (ReferenceEquals(_draggingButton, _upperButton))
            {
                UpdateUpperValue(Canvas.GetTop(_draggingButton));
            }
        }

        private void UpdateUpperValue(double y)
        {
            double invertedY = GetHeightMinusButtonsHeight() - y;
            double pixelPercentage = invertedY / GetHeightMinusButtonsHeight();
            double totalValue = MaxValue - MinValue;
            UpperValue = totalValue * pixelPercentage;
        }

        private double GetHeightMinusButtonsHeight()
        {
            return ActualHeight - _lowerButton.ActualHeight - _upperButton.ActualHeight;
        }

        private void UpdateLowervalue(double y)
        {

        }

        static MeterControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MeterControl), new FrameworkPropertyMetadata(typeof(MeterControl)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _upperButton = (Button)GetTemplateChild(UpperButtonName);
            _lowerButton = (Button)GetTemplateChild(LowerButtonName);

            if (_upperButton == null)
            {
                throw new NullReferenceException($"{UpperButtonName} does not exist");
            }

            if (_lowerButton == null)
            {
                throw new NullReferenceException($"{LowerButtonName} does not exist");
            }

            _upperButton.PreviewMouseLeftButtonDown += OnButtonPreviewMouseLeftButtonDown;
            _lowerButton.PreviewMouseLeftButtonDown += OnButtonPreviewMouseLeftButtonDown;
        }

        private void OnButtonPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _draggingButton = (Button)sender;
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            SetUpperButtonPosition();
            SetLowerButtonPosition();
        }

        private void SetLowerButtonPosition()
        {
            Canvas.SetTop(_lowerButton, CalculatePixelFromValue(LowerValue) + _lowerButton.ActualHeight);
        }

        private void SetUpperButtonPosition()
        {
            Canvas.SetTop(_upperButton, CalculatePixelFromValue(UpperValue));
        }

        private double CalculatePixelFromValue(double value)
        {
            double totalValues = MaxValue - MinValue;
            double adjustedValue = value - MinValue;
            double valuePercentage = adjustedValue / totalValues;

            double minPixel = _upperButton.ActualHeight;
            double maxPixel = ActualHeight - _lowerButton.ActualHeight;
            double totalPixels = maxPixel - minPixel;

            double pixel = totalPixels - totalPixels * valuePercentage;

            return pixel;
        }
    }
}
