using DigimonWorld.Evolution.Calculator.Core.DataObjects;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.InTraining;

public sealed class InTrainingEvolutionCalculator : IEvolutionCalculator
{
    private readonly InTrainingEvolutionMapper _inTrainingEvolutionMapper = new();
    
    public DigimonType DetermineEvolutionResult(Digimon digimon) => _inTrainingEvolutionMapper[digimon.DigimonType];
}