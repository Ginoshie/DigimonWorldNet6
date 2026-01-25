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
    private readonly Dictionary<DigimonName, IEvolutionCalculator> _evolutionCalculatorMappings = new();

    public EvolutionCalculationMapper()
    {
        _evolutionCalculatorMappings[DigimonName.Botamon] = new FromFreshEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.Poyomon] = new FromFreshEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.Punimon] = new FromFreshEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.Yuramon] = new FromFreshEvolutionCalculator();
        
        _evolutionCalculatorMappings[DigimonName.Koromon] = new FromInTrainingEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.Tanemon] = new FromInTrainingEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.Tokomon] = new FromInTrainingEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.Tsunomon] = new FromInTrainingEvolutionCalculator();
        
        _evolutionCalculatorMappings[DigimonName.Agumon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.Betamon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.Biyomon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.Elecmon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.Gabumon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.Kunemon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.Palmon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.Patamon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.Penguinmon] = new FromRookieOrChampionEvolutionCalculator();
        
        _evolutionCalculatorMappings[DigimonName.Airdramon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.Angemon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.Bakemon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.Birdramon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.Centarumon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.Coelamon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.Devimon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.Drimogemon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.Frigimon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.Garurumon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.Greymon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.Kabuterimon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.Kokatorimon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.Kuwagamon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.Leomon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.Meramon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.Mojyamon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.Monochromon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.Nanimon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.Ninjamon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.Numemon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.Ogremon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.Seadramon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.Shellmon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.Sukamon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.Tyrannomon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.Unimon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.Vegiemon] = new FromRookieOrChampionEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.Whamon] = new FromRookieOrChampionEvolutionCalculator();
        
        _evolutionCalculatorMappings[DigimonName.Andromon] = new FromUltimateEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.Digitamamon] = new FromUltimateEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.Etemon] = new FromUltimateEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.Giromon] = new FromUltimateEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.HerculesKabuterimon] = new FromUltimateEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.Mamemon] = new FromUltimateEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.Megadramon] = new FromUltimateEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.MegaSeadramon] = new FromUltimateEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.MetalGreymon] = new FromUltimateEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.MetalMamemon] = new FromUltimateEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.Monzaemon] = new FromUltimateEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.Phoenixmon] = new FromUltimateEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.Piximon] = new FromUltimateEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.SkullGreymon] = new FromUltimateEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonName.Vademon] = new FromUltimateEvolutionCalculator();
    }

    public IEvolutionCalculator this[DigimonName evolutionResult] =>
        _evolutionCalculatorMappings[evolutionResult] ??
        throw new KeyNotFoundException($"Evolution mapping for {evolutionResult} was not found in {nameof(EvolutionCalculationMapper)}");
}