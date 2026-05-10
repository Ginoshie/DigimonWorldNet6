using System.Collections.Generic;
using Shared.Enums;

namespace Domain;

public static class Session
{
    public static IList<DigimonName> HistoricEvolutions { get; } = [];

    public static UserDigimon UserDigimon => UserDigimon.Instance;
}