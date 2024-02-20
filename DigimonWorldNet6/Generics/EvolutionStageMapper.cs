using Generics.Enums;

namespace Generics;

public sealed class EvolutionStageMapper
{
    private readonly Dictionary<DigimonType, EvolutionStage> _evolutionStageMappings = new();

    public EvolutionStageMapper()
    {
        _evolutionStageMappings[DigimonType.Agumon] = EvolutionStage.Rookie;
        
    }
    
    public EvolutionStage this[DigimonType evolutionResult]
    {
        get
        {
            if (_evolutionStageMappings.TryGetValue(evolutionResult, out var evolutionStage))
            {
                return evolutionStage;
            }
            else
            {
                throw new KeyNotFoundException($"Evolution stage mapping for {evolutionResult} was not found in {nameof(EvolutionStageMapper)}");
            }
        }
    }
}