using System;
using System.Windows;
using System.Windows.Media;

namespace DapperApps.WPF.Controls
{
    public class WaveForm : FrameworkElement
    {
        private VisualCollection _visuals;
        private DrawingVisual _backgroundVisual;

        #region ItemsSource Dependency Property
        /// <summary>
        /// Identifies the ItemsSource DependencyProperty.
        /// </summary>
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(
                "ItemsSource",
                typeof(float[]),
                typeof(WaveForm),
                new PropertyMetadata(OnItemsSourceChanged));

        /// <summary>
        /// Get and set the ItemsSource property of this ownerclass.
        /// </summary>
        public float[] ItemsSource
        {
            get { return (float[])GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        static void OnItemsSourceChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            (obj as WaveForm).OnItemsSourceChanged(args);
        }

        void OnItemsSourceChanged(DependencyPropertyChangedEventArgs args)
        {
            DrawWaveForm();
        }
        #endregion ItemsSource Dependency Property

        #region Background Dependency Property
        /// <summary>
        /// Identifies the Background DependencyProperty.
        /// </summary>
        public static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register(
                "Background",
                typeof(Brush),
                typeof(WaveForm),
                new PropertyMetadata(Brushes.Transparent, OnBackgroundChanged));

        /// <summary>
        /// Get and set the Background property of this WaveForm.
        /// </summary>
        public Brush Background
        {
            get { return (Brush)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }

        static void OnBackgroundChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            (obj as WaveForm).OnBackgroundChanged(args);
        }

        void OnBackgroundChanged(DependencyPropertyChangedEventArgs args)
        {
            DrawBackground();
        }
        #endregion Background Dependency Property

        #region Foreground Dependency Property
        /// <summary>
        /// Identifies the Foreground DependencyProperty.
        /// </summary>
        public static readonly DependencyProperty ForegroundProperty =
            DependencyProperty.Register(
                "Foreground",
                typeof(Brush),
                typeof(WaveForm),
                new PropertyMetadata(Brushes.Black, OnForegroundChanged));

        /// <summary>
        /// Get and set the Foreground property of this WaveForm.
        /// </summary>
        public Brush Foreground
        {
            get { return (Brush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }

        static void OnForegroundChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            (obj as WaveForm).OnForegroundChanged(args);
        }

        void OnForegroundChanged(DependencyPropertyChangedEventArgs args)
        {
            DrawWaveForm();
        }
        #endregion Foreground Dependency Property

        public WaveForm()
        {
            _visuals = new VisualCollection(this);
            _backgroundVisual = new DrawingVisual();
            _visuals.Add(_backgroundVisual);
            Loaded += WaveForm_Loaded;
        }

        private void WaveForm_Loaded(object sender, RoutedEventArgs e)
        {
            DrawBackground();
        }

        protected override Visual GetVisualChild(int index)
        {
            return _visuals[index];
        }

        protected override int VisualChildrenCount
        {
            get
            {
                return _visuals.Count;
            }
        }

        /// <summary>
        /// Draws the background of the WaveForm component;
        /// </summary>
        /// <param name="backgroundBrush">The background brush.</param>
        private void DrawBackground()
        {
            if (null != Background)
            {
                using (var context = _backgroundVisual.RenderOpen())
                {
                    context.DrawRectangle(Background, null, new Rect(new Point(0.0, 0.0), RenderSize));
                }
            }
        }

        private void DrawWaveForm()
        {
            if (null != ItemsSource)
            {
                int border = 5;
                int width = (int)RenderSize.Width - (2 * border);
                int height = (int)RenderSize.Height - (2 * border);
                int size = ItemsSource.Length;
                for (int pixel = 0; pixel < width; ++pixel)
                {
                    // determine start and end points within WAV
                    int start = (int)((float)pixel * ((float)size / (float)width));
                    int end = (int)((float)(pixel + 1) * ((float)size / (float)width));

                    // find min and max values in WAV range
                    float min = float.MaxValue;
                    float max = float.MinValue;
                    for (int i = start; i < end; i++)
                    {
                        float val = ItemsSource[i];
                        min = val < min ? val : min;
                        max = val > max ? val : max;
                    }

                    int yMin = border + height - (int)((min + 1) * .5 * height);
                    int yMax = border + height - (int)((max + 1) * .5 * height);

                    var visual = new DrawingVisual();
                    using (var context = visual.RenderOpen())
                    {
                        context.DrawLine(new Pen(Foreground, 1.0), new Point(pixel + border, yMax), new Point(pixel + border, yMin));
                    }
                    _visuals.Add(visual);
                }
            }
        }
    }
}
