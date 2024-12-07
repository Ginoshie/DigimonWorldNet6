using System;
using Generics.Enums;
using Generics.Extensions;

namespace DigimonWorld.Frontend.WPF.Constants;

public static class JijimonNarratorText
{
    public const string EvolutionCalculatorIntro = "Well hello there! \n" +
                                                   "\n" +
                                                   "If you wish to calculate an evolution result then choose the digimon and fill out the stats.\n" +
                                                   $"Once you've done all that press the \"{UiText.CalculateButtonText}\" button to see the result.";

    public static string EvolutionResultCalculated(DigimonType evolutionDigimonType)
    {
        return evolutionDigimonType.EvolutionStage() switch
        {
            EvolutionStage.Fresh => throw new ArgumentOutOfRangeException(nameof(evolutionDigimonType), $"{evolutionDigimonType} is not a valid evolution target because fresh digimon hatch from eggs."),
            EvolutionStage.InTraining => "Oh look at that!\n" + 
                                         "\n" + $"It will become a {evolutionDigimonType}.\n" + 
                                         "\n" + 
                                         "What a cute little fellow.",
            EvolutionStage.Rookie => $"Very nice, it's going to become a {evolutionDigimonType}.\n" + 
                                     "\n" + 
                                     "They grow up so fast, don't they.",
            EvolutionStage.Champion => "Great work!\n" +
                                       $"It will become a mighty {evolutionDigimonType}",
            EvolutionStage.Ultimate => "",
            _ => throw new ArgumentOutOfRangeException()
        };
    }
        
}