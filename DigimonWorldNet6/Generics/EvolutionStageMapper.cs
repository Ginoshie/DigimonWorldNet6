using Generics.Enums;

namespace Generics;

public sealed class EvolutionStageMapper
{
    private readonly Dictionary<DigimonType, EvolutionStage> _evolutionStageMappings = new();

    public EvolutionStageMapper()
    {
        _evolutionStageMappings[DigimonType.Agumon] = EvolutionStage.Rookie;
        
    }
    
    public EvolutionStage this[DigimonType digimonType]
    {
        get
        {
            if (_evolutionStageMappings.TryGetValue(digimonType, out var evolutionStage))
            {
                return evolutionStage;
            }
            else
            {
                throw new KeyNotFoundException($"Evolution stage mapping for {digimonType} was not found in {nameof(EvolutionStageMapper)}");
            }
        }
    }
}