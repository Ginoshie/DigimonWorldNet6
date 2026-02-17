using System;
using System.Reactive.Linq;

namespace DigimonWorld.Frontend.WPF.ViewModelComponents;

public class PaneBaseViewModel : BaseViewModel
{
    public const int ANIMATION_DURATION_IN_MS = 600;
    
    protected IObservable<double> AnimateOffset(double start, double target)
    {
        const int fps = 60;
        const int steps = ANIMATION_DURATION_IN_MS * fps / 1000;

        return Observable
            .Interval(TimeSpan.FromMilliseconds(1000.0 / fps))
            .Take(steps + 1)
            .Select(i =>
            {
                double t = (double)i / steps;

                t = 1 - Math.Pow(1 - t, 2);

                return start + (target - start) * t;
            });
    }
}