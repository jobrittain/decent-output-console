using System.IO;
using System.Windows;
using System.Windows.Media;

namespace DecentOutputConsole
{
    public class Console : FrameworkElement
    {
        private static Color _defaultForegroundColor = Color.FromRgb(255, 255, 255);
        private static Color _defaultBackgroundColor = Color.FromRgb(0, 0, 0);

        public static readonly DependencyProperty ForegroundColorProperty =
           DependencyProperty.Register(
               "ForegroundColor", typeof(Color), typeof(Console),
               new FrameworkPropertyMetadata(_defaultForegroundColor, new PropertyChangedCallback(OnForegroundColorChanged)));

        private static void OnForegroundColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        public static readonly DependencyProperty BackgroundColorProperty =
           DependencyProperty.Register(
               "BackgroundColor", typeof(Color), typeof(Console),
               new FrameworkPropertyMetadata(_defaultBackgroundColor, new PropertyChangedCallback(OnBackgroundColorChanged)));

        private static void OnBackgroundColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        public static readonly DependencyProperty OutputStreamProperty =
            DependencyProperty.Register(
                "OutputStream", typeof(Stream), typeof(Console),
                new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnOutputStreamChanged), new CoerceValueCallback(CoerceOutputStream)));

        private static void OnOutputStreamChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        private static object CoerceOutputStream(DependencyObject d, object baseValue)
        {
            var stream = (Stream)baseValue;
            var control = (Console)d;

            if (stream.CanRead)
            {
                return baseValue;
            }

            return null;
        }

        public Color ForegroundColor
        {
            get { return (Color)GetValue(ForegroundColorProperty); }
            set { SetValue(ForegroundColorProperty, value); }
        }

        public Color BackgroundColor
        {
            get { return (Color)GetValue(BackgroundColorProperty); }
            set { SetValue(BackgroundColorProperty, value); }
        }

        public Stream OutputStream
        {
            get { return (Stream)GetValue(OutputStreamProperty); }
            set
            {
                if (value != null)
                {
                    _outputStreamReader = new StreamReader(value);
                }
                SetValue(OutputStreamProperty, value);
            }
        }

        private StreamReader _outputStreamReader;

        protected override void OnRender(DrawingContext drawingContext)
        {
            if (_outputStreamReader == null)
            {
                return;
            }

            while (!_outputStreamReader.EndOfStream)
            {
                // TODO: Read or something.
            }

            base.OnRender(drawingContext);
        }
    }
}
