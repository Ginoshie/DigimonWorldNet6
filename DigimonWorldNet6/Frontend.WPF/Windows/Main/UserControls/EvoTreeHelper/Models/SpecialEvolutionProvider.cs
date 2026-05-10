using System.Collections.Generic;
using Shared;
using Shared.Enums;

namespace DigimonWorld.Frontend.WPF.Windows.Main.UserControls.EvoTreeHelper.Models;

/// <summary>
/// Represents a special evolution that is available through non-standard means.
/// </summary>
public sealed class SpecialEvolutionInfo(DigimonName target, string description, string iconPath)
{
    public DigimonName Target { get; } = target;
    public string Name { get; } = target.ToString();
    public string Description { get; } = description;
    public string IconPath { get; } = iconPath;
}

/// <summary>
/// Provides information about special evolutions available for a given Digimon based on its evolution stage.
/// Special evolutions are not determined by the normal stat-based calculation.
/// Source: "Special evolutions Digimon World 1 summary.txt"
/// </summary>
public static class SpecialEvolutionProvider
{
    public static List<SpecialEvolutionInfo> GetAvailableSpecialEvolutions(DigimonName digimon)
    {
        EvolutionStage stage = EvolutionStageMapper.Get(digimon);
        List<SpecialEvolutionInfo> specials = [];

        // Any digimon ⇒ Sukamon: full virus bar (16 poops, carries over).
        if (stage is EvolutionStage.InTraining or EvolutionStage.Rookie or EvolutionStage.Champion or EvolutionStage.Ultimate)
        {
            specials.Add(new SpecialEvolutionInfo(
                DigimonName.Sukamon,
                "Full virus bar.\n\nNote: 16 poops, resets afterwards",
                Services.DigimonIconFactory.Create(DigimonName.Sukamon).IconPath));
        }

        // Any InTraining ⇒ Kunemon: sleep in Kunemon's Bed. 50% chance.
        if (stage == EvolutionStage.InTraining)
        {
            specials.Add(new SpecialEvolutionInfo(
                DigimonName.Kunemon,
                "Sleep in Kunemon's Bed area.\n\nNote: Native Forest\n\nNote: 50% chance",
                Services.DigimonIconFactory.Create(DigimonName.Kunemon).IconPath));
        }

        // Any Rookie ⇒ Numemon: not eligible for evolution between 72h and 96h in rookie form.
        if (stage == EvolutionStage.Rookie)
        {
            specials.Add(new SpecialEvolutionInfo(
                DigimonName.Numemon,
                "Not eligible for evolution between 72h and 96h in Rookie form.\n\nNote: -20% current stats",
                Services.DigimonIconFactory.Create(DigimonName.Numemon).IconPath));
        }

        // Any Rookie ⇒ Nanimon: scold while happiness = -100; discipline = 0.
        if (stage == EvolutionStage.Rookie)
        {
            specials.Add(new SpecialEvolutionInfo(
                DigimonName.Nanimon,
                "Scold while Happiness = -100 and Discipline = 0.\n\nNote: No evolution stats gain",
                Services.DigimonIconFactory.Create(DigimonName.Nanimon).IconPath));
        }

        // Any Rookie (but Penguinmon) ⇒ Bakemon: lose a life (battle or illness). 10% chance.
        if (stage == EvolutionStage.Rookie && digimon != DigimonName.Penguinmon)
        {
            specials.Add(new SpecialEvolutionInfo(
                DigimonName.Bakemon,
                "Lose a life either by battle or illness.\n\nNote: 10% chance",
                Services.DigimonIconFactory.Create(DigimonName.Bakemon).IconPath));
        }

        // Angemon ⇒ Devimon: lose a life while discipline ≤ 50. 50% chance.
        if (digimon == DigimonName.Angemon)
        {
            specials.Add(new SpecialEvolutionInfo(
                DigimonName.Devimon,
                "Lose a life either by battle or illness while Discipline ≤ 50.\n\nNote: 50% chance",
                Services.DigimonIconFactory.Create(DigimonName.Devimon).IconPath));
        }

        // Seadramon ⇒ Airdramon: sleep while discipline = 100; happiness = 100; tiredness = 0. 30% chance.
        if (digimon == DigimonName.Seadramon)
        {
            specials.Add(new SpecialEvolutionInfo(
                DigimonName.Airdramon,
                "Sleep while Discipline = 100, Happiness = 100, Tiredness = 0.\n\nNote: 30% chance",
                Services.DigimonIconFactory.Create(DigimonName.Airdramon).IconPath));
        }

        // Birdramon ⇒ Airdramon: sleep while discipline = 100; happiness = 100; tiredness = 0. 30% chance.
        if (digimon == DigimonName.Birdramon)
        {
            specials.Add(new SpecialEvolutionInfo(
                DigimonName.Airdramon,
                "Sleep while Discipline = 100, Happiness = 100, Tiredness = 0.\n\nNote: 30% chance",
                Services.DigimonIconFactory.Create(DigimonName.Airdramon).IconPath));
        }

        // Whamon ⇒ Coelamon (⇒ MegaSeadramon): scold or praise during the 200th hour as Whamon. 30% chance.
        if (digimon == DigimonName.Whamon)
        {
            specials.Add(new SpecialEvolutionInfo(
                DigimonName.Coelamon,
                "Scold or praise during the 200th hour as Whamon. Coelamon will evolve to MegaSeadramon instantly.\n\nNote: 30% chance",
                Services.DigimonIconFactory.Create(DigimonName.Coelamon).IconPath));
        }

        // Shellmon ⇒ Coelamon (⇒ MegaSeadramon): scold or praise during the 200th hour as Shellmon.
        if (digimon == DigimonName.Shellmon)
        {
            specials.Add(new SpecialEvolutionInfo(
                DigimonName.Coelamon,
                "Scold or praise during the 200th hour as Shellmon. Coelamon will evolve to MegaSeadramon instantly.\n\nNote: 30% chance",
                Services.DigimonIconFactory.Create(DigimonName.Coelamon).IconPath));
        }

        // Vegiemon ⇒ Ninjamon: sleep while discipline = 100; battles fought = 50+. 30% chance.
        if (digimon == DigimonName.Vegiemon)
        {
            specials.Add(new SpecialEvolutionInfo(
                DigimonName.Ninjamon,
                "Sleep while Discipline = 100 and Battles ≥ 50.\n\nNote: 30% chance",
                Services.DigimonIconFactory.Create(DigimonName.Ninjamon).IconPath));
        }

        // Drimogemon ⇒ Monochromon: sleep while discipline = 100; defense = 500+.
        if (digimon == DigimonName.Drimogemon)
        {
            specials.Add(new SpecialEvolutionInfo(
                DigimonName.Monochromon,
                "Sleep while Discipline = 100 and Defense ≥ 500.\n\nNote: Evolution age does not reset.\n\nNote: 30% chance",
                Services.DigimonIconFactory.Create(DigimonName.Monochromon).IconPath));
        }

        // Any Champion ⇒ Vademon: praise after 240h in champion form, or reaching 360h. 50% chance.
        if (stage == EvolutionStage.Champion)
        {
            specials.Add(new SpecialEvolutionInfo(
                DigimonName.Vademon,
                "Praise after 240h in Champion form, or reach 360h in Champion form.\n\nNote: 50% chance",
                Services.DigimonIconFactory.Create(DigimonName.Vademon).IconPath));
        }

        // Kokatorimon ⇒ Phoenixmon: lose a life (battle or illness). 10% chance.
        if (digimon == DigimonName.Kokatorimon)
        {
            specials.Add(new SpecialEvolutionInfo(
                DigimonName.Phoenixmon,
                "Lose a life either by battle or illness.\n\nNote: 10% chance",
                Services.DigimonIconFactory.Create(DigimonName.Phoenixmon).IconPath));
        }

        // MetalGreymon ⇒ SkullGreymon: lose a life (battle or illness). 10% chance.
        if (digimon == DigimonName.MetalGreymon)
        {
            specials.Add(new SpecialEvolutionInfo(
                DigimonName.SkullGreymon,
                "Lose a life either by battle or illness.\n\nNote: 10% chance",
                Services.DigimonIconFactory.Create(DigimonName.SkullGreymon).IconPath));
        }

        // Megadramon ⇒ SkullGreymon: lose a life (battle or illness). 10% chance.
        if (digimon == DigimonName.Megadramon)
        {
            specials.Add(new SpecialEvolutionInfo(
                DigimonName.SkullGreymon,
                "Lose a life either by battle or illness.\n\nNote: 10% chance",
                Services.DigimonIconFactory.Create(DigimonName.SkullGreymon).IconPath));
        }

        // Mamemon ⇒ MetalMamemon: talk to Guardromon in Factorial Town for 2000 bits. 5/11 chance. 1/11 Giromon. 5/11 fail.
        // Mamemon ⇒ Giromon: same as above. 1/11 chance.
        if (digimon == DigimonName.Mamemon)
        {
            specials.Add(new SpecialEvolutionInfo(
                DigimonName.MetalMamemon,
                "Talk to Guardromon in Factorial Town (top room, left wall) for 2000 bits.\n- MetalMamemon:\n\nNote: 5/11 chance\n- Giromon:\n\nNote: 1/11 chance\n- Fail:\n\nNote: 5/11 chance",
                Services.DigimonIconFactory.Create(DigimonName.MetalMamemon).IconPath));
            specials.Add(new SpecialEvolutionInfo(
                DigimonName.Giromon,
                "Talk to Guardromon in Factorial Town (top room, left wall) for 2000 bits.\n- MetalMamemon:\n\nNote: 5/11 chance\n- Giromon:\n\nNote: 1/11 chance\n- Fail:\n\nNote: 5/11 chance",
                Services.DigimonIconFactory.Create(DigimonName.Giromon).IconPath));
        }

        return specials;
    }
}



