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
            DependencyProperty.Register(nameof(Duration), typeof(TimeSpan), typeof(Toast), new PropertyMetadata(TimeSpan.FromSeconds(10), OnDurationChanged));

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

                CubicEase cubicEase = new CubicEase();
                cubicEase.EasingMode = EasingMode.EaseInOut;

                doubleAnimation.EasingFunction = cubicEase;

                doubleAnimation.Completed += (sender, e) => IsToastVisible = false;
                BeginAnimation(OpacityProperty, doubleAnimation);
            }
            else
            {
                Opacity = 0;
            }
        }

        private static void OnToastIconChanged(DependencyObject toastObject, DependencyPropertyChangedEventArgs args)
        {
            var control = (Toast)toastObject;
            var value = (ToastIconType)args.NewValue;

            switch (value)
            {
                case ToastIconType.Information:
                    control.ImageGeometry = Geometry.Parse("M3.6069946,1.8659973C2.6459963,1.8659973,1.8660278,2.6480103,1.8660278,3.6080017L1.8660278,20.546021C1.8660278,21.507019,2.6459963,22.288025,3.6069946,22.288025L11.647035,22.288025 9.6170056,27.481018 18.038026,22.288025 28.124027,22.288025C29.085025,22.288025,29.865054,21.507019,29.865054,20.546021L29.865054,3.6080017C29.865054,2.6480103,29.085025,1.8659973,28.124027,1.8659973z M3.6069946,0L28.124027,0C30.11304,0,31.731998,1.6190186,31.731998,3.6080017L31.731998,20.546021C31.731998,22.536011,30.11304,24.154022,28.124027,24.154022L18.567018,24.154022 5.8439948,32 8.9130261,24.154022 3.6069946,24.154022C1.618042,24.154022,-2.120687E-07,22.536011,0,20.546021L0,3.6080017C-2.120687E-07,1.6190186,1.618042,0,3.6069946,0z");
                    break;

                case ToastIconType.Warning:
                    control.ImageGeometry = Geometry.Parse("M13.950004,24.5L13.950004,28.299988 17.950004,28.299988 17.950004,24.5z M13.950004,10.399963L13.950004,21.699951 17.950004,21.699951 17.950004,10.399963z M15.950004,0C16.349998,0,16.750007,0.19995117,16.950004,0.69995117L31.750011,30.099976C31.949993,30.5 31.949993,31 31.750011,31.399963 31.549999,31.799988 31.150005,32 30.750011,32L1.1499981,32C0.75000406,32 0.34999478,31.799988 0.14999761,31.399963 -0.049999204,31 -0.049999204,30.5 0.14999761,30.099976L14.950004,0.69995117C15.150001,0.19995117,15.549995,0,15.950004,0z");
                    break;
            }
        }

        public enum ToastIconType
        {
            None = 0,
            Information = 1,
            Warning = 2
        }
    }
}
