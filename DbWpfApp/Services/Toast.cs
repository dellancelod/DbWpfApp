using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows;

namespace DbWpfApp.Services
{
    internal class Toast : Control
    {
        static Toast()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Toast), new FrameworkPropertyMetadata(typeof(Toast)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            ChangeVisualState();
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(Toast), new PropertyMetadata("Placeholder"));

        public static readonly DependencyProperty ToastIconProperty =
            DependencyProperty.Register(nameof(ToastIcon), typeof(ToastIconType), typeof(Toast), new PropertyMetadata(ToastIconType.None, OnToastIconChanged));

        public static readonly DependencyProperty IsToastVisibleProperty =
            DependencyProperty.Register(nameof(IsToastVisible), typeof(bool), typeof(Toast), new PropertyMetadata(false, OnIsToastVisibleChanged));

        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register(nameof(Duration), typeof(TimeSpan), typeof(Toast), new PropertyMetadata(TimeSpan.FromSeconds(5), OnDurationChanged));

        public static readonly DependencyProperty ImageGeometryProperty =
            DependencyProperty.Register(nameof(ImageGeometry), typeof(Geometry), typeof(Toast));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public bool IsToastVisible
        {
            get { return (bool)GetValue(IsToastVisibleProperty); }
            set { SetValue(IsToastVisibleProperty, value); ChangeVisualState(); }
        }

        public ToastIconType ToastIcon
        {
            get { return (ToastIconType)GetValue(ToastIconProperty); }
            set { SetValue(ToastIconProperty, value); }
        }

        public TimeSpan Duration
        {
            get { return (TimeSpan)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        public Geometry ImageGeometry
        {
            get { return (Geometry)GetValue(ImageGeometryProperty); }
            set { SetValue(ImageGeometryProperty, value); }
        }

        private static void OnDurationChanged(DependencyObject toastObject, DependencyPropertyChangedEventArgs args)
        {
            var control = (Toast)toastObject;
            var value = (TimeSpan)args.NewValue;
            control.Duration = value;
        }

        private static void OnIsToastVisibleChanged(DependencyObject toastObj, DependencyPropertyChangedEventArgs args)
        {
            var c = (Toast)toastObj;
            var value = (bool)args.NewValue;
            c.IsToastVisible = value;
        }

        private void ChangeVisualState()
        {
            if (IsToastVisible)
            {
                DoubleAnimation doubleAnimation = new DoubleAnimation { From = 1, To = 0, Duration = TimeSpan.FromSeconds(Duration.Seconds) };

                CubicEase cubicEase = new CubicEase
                {
                    EasingMode = EasingMode.EaseInOut
                };

                doubleAnimation.EasingFunction = cubicEase;

                doubleAnimation.Completed += (sender, e) => IsToastVisible = false;
                BeginAnimation(OpacityProperty, doubleAnimation);
            }
        }

        private static void OnToastIconChanged(DependencyObject toastObject, DependencyPropertyChangedEventArgs args)
        {
            var control = (Toast)toastObject;
            var value = (ToastIconType)args.NewValue;

            switch (value)
            {
                case ToastIconType.Success:
                    control.ImageGeometry = Geometry.Parse("M12.736 3.97a.733.733 0 0 1 1.047 0c.286.289.29.756.01 1.05L7.88 12.01a.733.733 0 0 1-1.065.02L3.217 8.384a.757.757 0 0 1 0-1.06.733.733 0 0 1 1.047 0l3.052 3.093 5.4-6.425a.247.247 0 0 1 .02-.022Z");
                    break;

                case ToastIconType.Warning:
                    control.ImageGeometry = Geometry.Parse("M13.950004,24.5L13.950004,28.299988 17.950004,28.299988 17.950004,24.5z M13.950004,10.399963L13.950004,21.699951 17.950004,21.699951 17.950004,10.399963z M15.950004,0C16.349998,0,16.750007,0.19995117,16.950004,0.69995117L31.750011,30.099976C31.949993,30.5 31.949993,31 31.750011,31.399963 31.549999,31.799988 31.150005,32 30.750011,32L1.1499981,32C0.75000406,32 0.34999478,31.799988 0.14999761,31.399963 -0.049999204,31 -0.049999204,30.5 0.14999761,30.099976L14.950004,0.69995117C15.150001,0.19995117,15.549995,0,15.950004,0z");
                    break;
            }
        }

        public enum ToastIconType
        {
            None = 0,
            Success = 1,
            Warning = 2
        }
    }
}
