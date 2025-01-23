using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using DigimonWorld.Frontend.WPF.Constants;
using Microsoft.Xaml.Behaviors;

namespace DigimonWorld.Frontend.WPF.Behaviors
{
    public class DelayedTooltipBehavior : Behavior<FrameworkElement>
    {
        private DispatcherTimer? _timer;
        private bool _isMouseOver;

        public string? TooltipText { get; set; }

        // Add DependencyProperty for Tooltip Style
        public static readonly DependencyProperty TooltipStyleProperty =
            DependencyProperty.Register(
                nameof(TooltipStyle),
                typeof(Style),
                typeof(DelayedTooltipBehavior),
                new PropertyMetadata(null));

        public Style TooltipStyle
        {
            get => (Style)GetValue(TooltipStyleProperty);
            set => SetValue(TooltipStyleProperty, value);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(DefaultValues.DefaultTooltipDelayInMs) };
            _timer.Tick += OnTimerTick;

            AssociatedObject.MouseEnter += OnMouseEnter;
            AssociatedObject.MouseLeave += OnMouseLeave;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            _timer!.Tick -= OnTimerTick;
            AssociatedObject.MouseEnter -= OnMouseEnter;
            AssociatedObject.MouseLeave -= OnMouseLeave;
        }

        private void OnMouseEnter(object sender, RoutedEventArgs e)
        {
            _isMouseOver = true;
            _timer!.Start();
        }

        private void OnMouseLeave(object sender, RoutedEventArgs e)
        {
            _isMouseOver = false;
            _timer!.Stop();

            if (AssociatedObject is not { ToolTip: ToolTip tooltip } element) return;

            tooltip.IsOpen = false;
            Dispatcher.Invoke(() => { }, DispatcherPriority.Render);
            element.ToolTip = null;
        }

        private void OnTimerTick(object? sender, EventArgs e)
        {
            _timer!.Stop();

            if (!_isMouseOver || AssociatedObject is not { } element) return;

            ToolTip tooltip = new()
            {
                Content = TooltipText,
                Style = TooltipStyle,
                Placement = System.Windows.Controls.Primitives.PlacementMode.Mouse,
                HorizontalOffset = 10,
                VerticalOffset = 10,
            };
            element.ToolTip = tooltip;
            tooltip.IsOpen = true;
        }
    }
}