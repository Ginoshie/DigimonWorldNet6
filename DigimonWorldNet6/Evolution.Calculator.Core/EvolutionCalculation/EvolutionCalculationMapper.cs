using System.Collections.Generic;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.ChampionAndUltimate;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.InTraining;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.Rookie;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCalculation;

public sealed class EvolutionCalculationMapper
{
    private readonly Dictionary<DigimonType, IEvolutionCalculator> _evolutionCalculatorMappings = new();

    public EvolutionCalculationMapper()
    {
        _evolutionCalculatorMappings[DigimonType.Agumon] = new ChampionAndUltimateEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Birdramon] = new ChampionAndUltimateEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Biyomon] = new RookieEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Centarumon] = new ChampionAndUltimateEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Gabumon] = new RookieEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Greymon] = new ChampionAndUltimateEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Koromon] = new RookieEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Meramon] = new ChampionAndUltimateEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Monochromon] = new ChampionAndUltimateEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Numemon] = new ChampionAndUltimateEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Palmon] = new ChampionAndUltimateEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Patamon] = new ChampionAndUltimateEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Poyomon] = new InTrainingEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Tokomon] = new RookieEvolutionCalculator();
        _evolutionCalculatorMappings[DigimonType.Tyrannomon] = new ChampionAndUltimateEvolutionCalculator();
    }
    
    public IEvolutionCalculator this[DigimonType evolutionResult] =>
        _evolutionCalculatorMappings[evolutionResult] ??
        throw new KeyNotFoundException($"Evolution mapping for {evolutionResult} was not found in {nameof(EvolutionCalculationMapper)}");
}