using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using DigimonWorld.Frontend.WPF.Constants;

namespace DigimonWorld.Frontend.WPF.Services;

public static class GeneralConfiguration
{
    private static readonly BehaviorSubject<NarratorMode> NarratorModeChangedSubject = new(Constants.NarratorMode.Speech);

    public static void SetNarratorMode(NarratorMode mode)
    {
        NarratorModeChangedSubject.OnNext(mode);
    }

    public static readonly IObservable<NarratorMode> NarratorMode = NarratorModeChangedSubject.AsObservable();
}