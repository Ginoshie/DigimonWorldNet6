using System.Collections.Generic;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromFresh;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromInTraining;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromRookieOrChampion;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromUltimate;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCalculation;

public sealed class EvolutionCalculationMapper
{
    private readonly Dictionary<DigimonType, IEvolutionCalculator> _evolutionCalculatorMappings = new();

    public EvolutionCalculationMapper()
    {
        _evolutionCalculatorMappings[DigimonType.Botamon] = new FromFreshEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Poyomon] = new FromFreshEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Punimon] = new FromFreshEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Yuramon] = new FromFreshEvolutionCalculator();
        
        _evolutionCalculatorMappings[DigimonType.Koromon] = new FromInTrainingEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Tanemon] = new FromInTrainingEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Tokomon] = new FromInTrainingEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Tsunomon] = new FromInTrainingEvolutionCalculator();
        
        _evolutionCalculatorMappings[DigimonType.Agumon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Betamon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Biyomon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Elecmon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Gabumon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Kunemon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Palmon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Patamon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Penguinmon] = new FromRookieOrChampionEvolutionCalculator();
        
        _evolutionCalculatorMappings[DigimonType.Airdramon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Angemon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Bakemon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Birdramon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Centarumon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Coelamon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Devimon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Drimogemon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Frigimon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Garurumon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Greymon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Kabuterimon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Kokatorimon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Kuwagamon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Leomon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Meramon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Mojyamon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Monochromon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Nanimon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Ninjamon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Numemon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Ogremon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Seadramon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Shellmon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Sukamon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Tyrannomon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Unimon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Vegiemon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Whamon] = new FromRookieOrChampionEvolutionCalculator();
        
        _evolutionCalculatorMappings[DigimonType.Andromon] = new FromUltimateEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Digitamamon] = new FromUltimateEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Etemon] = new FromUltimateEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Giromon] = new FromUltimateEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.HerculesKabuterimon] = new FromUltimateEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Mamemon] = new FromUltimateEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Megadramon] = new FromUltimateEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.MegaSeadramon] = new FromUltimateEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.MetalGreymon] = new FromUltimateEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.MetalMamemon] = new FromUltimateEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Monzaemon] = new FromUltimateEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Phoenixmon] = new FromUltimateEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Piximon] = new FromUltimateEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.SkullGreymon] = new FromUltimateEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Vademon] = new FromUltimateEvolutionCalculator();
    }

    public IEvolutionCalculator this[DigimonType evolutionResult] =>
        _evolutionCalculatorMappings[evolutionResult] ??
        throw new KeyNotFoundException($"Evolution mapping for {evolutionResult} was not found in {nameof(EvolutionCalculationMapper)}");
}