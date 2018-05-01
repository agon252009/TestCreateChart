using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using TestCreateChart.Annotations;

namespace TestCreateChart
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        private double _upperValue = 400;
        private double _lowerValue;
        private double _maxValue = 2500;
        private double _minValue;
        private ICommand _incrementCommand;
        private ICommand _decrementCommand;

        public double UpperValue
        {
            get => _upperValue;
            set
            {
                if (value.Equals(_upperValue)) return;
                _upperValue = value;
                OnPropertyChanged();
            }
        }

        public double LowerValue
        {
            get => _lowerValue;
            set
            {
                if (value.Equals(_lowerValue)) return;
                _lowerValue = value;
                OnPropertyChanged();
            }
        }

        public double MaxValue
        {
            get => _maxValue;
            set
            {
                if (value.Equals(_maxValue)) return;
                _maxValue = value;
                OnPropertyChanged();
            }
        }

        public double MinValue
        {
            get => _minValue;
            set
            {
                if (value.Equals(_minValue)) return;
                _minValue = value;
                OnPropertyChanged();
            }
        }

        public ICommand IncrementCommand
        {
            get => _incrementCommand;
            set
            {
                if (Equals(value, _incrementCommand)) return;
                _incrementCommand = value;
                OnPropertyChanged();
            }
        }

        public ICommand DecrementCommand
        {
            get => _decrementCommand;
            set
            {
                if (Equals(value, _decrementCommand)) return;
                _decrementCommand = value;
                OnPropertyChanged();
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            IncrementCommand = new RelayCommand(() => UpperValue+=25);
            DecrementCommand = new RelayCommand(()=> UpperValue-=25);
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
