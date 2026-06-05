using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Shared.Services.Events;

public static class UserDigimonEventHub
{
    private static readonly Subject<Unit> _conditionStatsSynchronizedSubject = new();
    private static readonly Subject<Unit> _parameterStatsSynchronizedSubject = new();
    private static readonly Subject<Unit> _profileStatsSynchronizedSubject = new();
    
    public static IObservable<Unit> ConditionStatsSynchronizedObservable => _conditionStatsSynchronizedSubject.AsObservable();
    public static IObservable<Unit> ParameterStatsSynchronizedObservable => _parameterStatsSynchronizedSubject.AsObservable();
    public static IObservable<Unit> ProfileStatsSynchronizedObservable => _profileStatsSynchronizedSubject.AsObservable();
    
    public static void SignalConditionStatsSynchronized() => _conditionStatsSynchronizedSubject.OnNext(Unit.Default);
    public static void SignalParameterStatsSynchronized() => _parameterStatsSynchronizedSubject.OnNext(Unit.Default);
    public static void SignalProfileStatsSynchronized() => _profileStatsSynchronizedSubject.OnNext(Unit.Default);
}