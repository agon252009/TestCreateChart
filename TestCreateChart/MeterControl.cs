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
        public static readonly DependencyProperty MaxValueProperty = DependencyProperty.Register("MaxValue", typeof(double), typeof(MeterControl), new PropertyMetadata(100.0, MaxMinCallback));

        public double MinValue
        {
            get => (double)GetValue(MinValueProperty);
            set => SetValue(MinValueProperty, value);
        }

        // Using a DependencyProperty as the backing store for MinValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinValueProperty = DependencyProperty.Register("MinValue", typeof(double), typeof(MeterControl), new PropertyMetadata(0.0, MaxMinCallback));

        public double UpperValue
        {
            get => (double)GetValue(UpperValueProperty);
            set => SetValue(UpperValueProperty, value);
        }

        // Using a DependencyProperty as the backing store for UpperValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UpperValueProperty = DependencyProperty.Register("UpperValue", typeof(double), typeof(MeterControl), new PropertyMetadata(100.0, UpperValueChangedCallback, CoerceValueCallback));

        private static object CoerceValueCallback(DependencyObject dependencyObject, object baseValue)
        {
            MeterControl meterControl = (MeterControl)dependencyObject;
            double coercedValue = (double) baseValue;
            return Math.Max(Math.Min(coercedValue, meterControl.MaxValue), meterControl.MinValue);
        }

        public double LowerValue
        {
            get => (double)GetValue(LowerValueProperty);
            set => SetValue(LowerValueProperty, value);
        }

        // Using a DependencyProperty as the backing store for LowerValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LowerValueProperty = DependencyProperty.Register("LowerValue", typeof(double), typeof(MeterControl), new PropertyMetadata(0.0, LowerValueChangedCallback, CoerceValueCallback));

        public MeterControl()
        {
            PreviewMouseMove += OnPreviewMouseMove;
            PreviewMouseLeftButtonUp += OnPreviewMouseLeftButtonUp;
        }

        static MeterControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MeterControl), new FrameworkPropertyMetadata(typeof(MeterControl)));
        }

        private static void UpperValueChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var meterControl = (MeterControl)dependencyObject;
            if (meterControl._upperButton == null)
            {
                return;
            }

            meterControl.SyncUpperButtonPositionToValue();
        }

        private static void LowerValueChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var meterControl = (MeterControl)dependencyObject;
            if (meterControl._lowerButton == null)
            {
                return;
            }

            meterControl.SyncLowerButtonPositionToValue();
        }

        private static void MaxMinCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var meterControl = (MeterControl)dependencyObject;
            if (meterControl._lowerButton == null || meterControl._upperButton == null)
            {
                return;
            }

            meterControl.SyncUpperButtonPositionToValue();
            meterControl.SyncLowerButtonPositionToValue();
        }
        
        private double GetAdjustedLocation(Button button, double newLocation)
        {
            double value = 0;

            if (ReferenceEquals(button, _upperButton))
            {
                value = Math.Min(Math.Max(newLocation, 0), GetHeightMinusButtonsHeight());
            }

            if (ReferenceEquals(button, _lowerButton))
            {
                value = Math.Min(Math.Max(newLocation, GetMinPixel()), GetMaxPixel());
            }

            return value;
        }

        private double GetMinPixel()
        {
            if (_upperButton == null)
            {
                return 0;
            }

            return _upperButton.ActualHeight;
        }

        private void SyncUpperValueToPosition()
        {
            double y = Canvas.GetTop(_upperButton);
            double invertedY = GetHeightMinusButtonsHeight() - y;
            double pixelPercentage = GetPixelPercentage(invertedY);
            double totalValues = GetTotalValues();
            UpperValue = totalValues * pixelPercentage;
        }

        private void SyncLowerValueToPosition()
        {
            double y = Canvas.GetTop(_lowerButton);
            double invertedY = GetMaxPixel() - y;
            double pixelPercentage = GetPixelPercentage(invertedY);
            double totalValues = GetTotalValues();
            LowerValue = totalValues * pixelPercentage;
        }

        private double GetPixelPercentage(double y)
        {
            return y / GetHeightMinusButtonsHeight();
        }

        private double GetTotalValues()
        {
            return MaxValue - MinValue;
        }

        private double GetHeightMinusButtonsHeight()
        {
            return GetMaxPixel() - GetMinPixel();
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
        
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            SyncUpperButtonPositionToValue();
            SyncLowerButtonPositionToValue();
        }

        private void SyncLowerButtonPositionToValue()
        {
            if (_lowerButton == null)
            {
                return;
            }
            Canvas.SetTop(_lowerButton, CalculatePixelFromValue(LowerValue) + _lowerButton.ActualHeight);
        }

        private void SyncUpperButtonPositionToValue()
        {
            Canvas.SetTop(_upperButton, CalculatePixelFromValue(UpperValue));
        }

        private double CalculatePixelFromValue(double value)
        {
            double totalValues = GetTotalValues();
            double adjustedValue = value - MinValue;
            double valuePercentage = adjustedValue / totalValues;

            double totalPixels = GetMaxPixel() - GetMinPixel();
            double pixel = totalPixels - totalPixels * valuePercentage;

            return pixel;
        }

        private double GetMaxPixel()
        {
            return ActualHeight - _lowerButton.ActualHeight;
        }

        #region Events

        private void OnButtonPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _draggingButton = (Button)sender;
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
            double newLocation = Canvas.GetTop(_draggingButton) + locationDelta;
            double adjustedLocation = GetAdjustedLocation(_draggingButton, newLocation);
            Canvas.SetTop(_draggingButton, adjustedLocation);
            _previousMouseYLocation = yPos;

            if (ReferenceEquals(_draggingButton, _upperButton))
            {
                SyncUpperValueToPosition();
            }

            if (ReferenceEquals(_draggingButton, _lowerButton))
            {
                SyncLowerValueToPosition();
            }
        } 

        #endregion
    }
}
