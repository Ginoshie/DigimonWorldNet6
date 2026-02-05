using System.Collections.Generic;
using DigimonWorld.Evolution.Calculator.Core.DataObjects;
using Shared.Enums;

namespace DigimonWorld.Evolution.Calculator.Core;

public static class Session
{
    public static IList<DigimonName> HistoricEvolutions { get; } = [];

    public static UserDigimon EmulatorUserDigimon { get; } = new();
}