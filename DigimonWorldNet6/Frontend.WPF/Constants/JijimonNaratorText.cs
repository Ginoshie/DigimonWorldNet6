using System;
using System.Linq;
using Generics.Enums;
using Generics.Extensions;

namespace DigimonWorld.Frontend.WPF.Constants;

public static class JijimonNarratorText
{
    public const string EvolutionCalculatorIntroText = "Well hello there! \n" +
                                                       "\n" +
                                                       "If you wish to calculate an evolution result then choose the digimon and fill out the stats in the section to the left of me.\n" +
                                                       "\n" +
                                                       "Once you've done that, open the 'Historic Evolutions' pane and select each evolution you've achieved in this save.\n" +
                                                       "\n" +
                                                       $"When done press the \"{UiText.CalculateButtonText}\" button to see the result.";

    public static string EvolutionResultCalculated(EvolutionResult evolutionResult)
    {
        switch (evolutionResult)
        {
            case EvolutionResult.Unknown:
                return "If you wish to perform calculation for a different digimon then choose the digimon and fill out the stats.\n";
            case EvolutionResult.None:
                return "Oh dear, your digimon is not going to evolve I'm afraid.\n" +
                       "\n" +
                       "Perhaps check an evolution guide or try increasing some stats or changing weight?";
            case EvolutionResult.NotApplicable:
                return "Your digimon is already at ultimate level which means it can't evolve to a higher stage anymore. \n" +
                       "\n" +
                       "*whispers* . . .  The world holds many a secret though. . .  *smiles*";
        }

        DigimonType evolutionDigimonType = evolutionResult.ToDigimonType();
        EvolutionStage evolutionResultEvolutionStage = evolutionDigimonType.EvolutionStage();

        return evolutionResultEvolutionStage switch
        {
            EvolutionStage.Fresh => throw new ArgumentOutOfRangeException(nameof(evolutionDigimonType), evolutionDigimonType, $"{evolutionDigimonType} is not a valid evolution target because fresh digimon hatch from eggs."),
            EvolutionStage.InTraining => "Oh look at that!\n" +
                                         "\n" +
                                         $"It will become {AOrAn(evolutionDigimonType)} {evolutionDigimonType}.\n" +
                                         "\n" +
                                         "What a cute little fellow.",
            EvolutionStage.Rookie => RookieResponses(evolutionDigimonType),
            EvolutionStage.Champion => ChampionResponses(evolutionDigimonType),
            EvolutionStage.Ultimate => UltimateResponses(evolutionDigimonType),
            _ => throw new ArgumentOutOfRangeException(nameof(evolutionDigimonType), evolutionDigimonType, $"{evolutionDigimonType} is not supported.")
        };
    }

    private static string RookieResponses(DigimonType evolutionDigimonType)
    {
        if (evolutionDigimonType.EvolutionStage() != EvolutionStage.Champion)
        {
            throw new ArgumentOutOfRangeException(nameof(evolutionDigimonType), evolutionDigimonType,
                $"{evolutionDigimonType} is not a champion evolution target. These responses are only meant for champion evolutions.");
        }

        return evolutionDigimonType switch
        {
            DigimonType.Penguinmon => "Evolution time? Hmm somethings fishy about this one . . . \n" +
                                      "\n" +
                                      $"Ah! No wonder, it's a {evolutionDigimonType}.  *laughs*\n" +
                                      "\n" +
                                      "Take it for a curl, you'll have a blast.",
            DigimonType.Agumon => "Evolution time! I can already tell this is a good one\n" +
                                  "\n" +
                                  $"Yes! yes! yes! It's an {evolutionDigimonType}\n" +
                                  "\n" +
                                  "I'm chuffed!  *smiles widely*",
            DigimonType.Gabumon => "Oh yet another evolution, I am chuffed!\n" +
                                   "\n" +
                                   "Oh look at that, it's a {evolutionDigimonType}!\n" +
                                   "\n" +
                                   "A long time ago a player enjoyed this little guys presence a lot." +
                                   "\n" +
                                   "Great memories . . .  *smiles widely*",
            _ => $"Very nice, it's going to become {AOrAn(evolutionDigimonType)} {evolutionDigimonType}.\n" +
                 "\n" +
                 "They grow up so fast, don't they."
        };
    }

    private static string ChampionResponses(DigimonType evolutionDigimonType)
    {
        if (evolutionDigimonType.EvolutionStage() != EvolutionStage.Champion)
        {
            throw new ArgumentOutOfRangeException(nameof(evolutionDigimonType), evolutionDigimonType,
                $"{evolutionDigimonType} is not a champion evolution target. These responses are only meant for champion evolutions.");
        }

        return evolutionDigimonType switch
        {
            DigimonType.Vegiemon => "Good job, it will evolve to the champion stage.\n" +
                                    "\n" +
                                    $"A {evolutionDigimonType}, he got some major beef . . . \n" +
                                    "\n" +
                                    $"{DigimonType.Tyrannomon} will be very happy.  *chuckles*",
            DigimonType.Sukamon => "Hmm this is going to be special . . . \n" +
                                   "\n" +
                                   "In a . . . good . . . way ofcourse.\n" +
                                   "\n" +
                                   $"It'll become a {evolutionDigimonType}.\n" +
                                   "\n" +
                                   "It's time to clean up the city.",
            DigimonType.Shellmon => "Another exciting evolution!\n" +
                                    "\n" +
                                    $"Great! It's a {evolutionDigimonType}.\n" +
                                    "\n" +
                                    $"Have you got any rumors for us {evolutionDigimonType}?  *leans in eagerly*",
            DigimonType.Ogremon => "Oh this is going to be a special one . . . \n" +
                                   "\n" +
                                   "There he is, perhaps not looking like it but . . . \n" +
                                   "\n" +
                                   $"{evolutionDigimonType} has a gentle soul, you'd be surprised.  *nods*",
            DigimonType.Meramon => "Watch closely, it's going to evolve\n" +
                                   "\n" +
                                   "*sudden rise in temperature*\n" +
                                   "\n" +
                                   $"Oof . . . hot . . . This must be a . . . {evolutionDigimonType}!\n" +
                                   "\n" +
                                   "*yelps* Don't burn my house!",
            DigimonType.Leomon => "It's evolving, with such fierce power . . . \n" +
                                  "\n" +
                                  $"No wonder I could sense it's power. It's a {evolutionDigimonType}\n" +
                                  $"*starts humming the {evolutionDigimonType} theme*",
            DigimonType.Kokatorimon => "It's evolving, let's wait for the result\n" +
                                       "\n" +
                                       $"Great, it's a {evolutionDigimonType}\n" +
                                       "\n" +
                                       "You two can race for the worm in the morning.  *smiles gently*",
            DigimonType.Greymon => "How nice, another evolution incoming.\n" +
                                   "\n" +
                                   $"Look at that, a {evolutionDigimonType}.\n" +
                                   "\n" +
                                   "Without the confused aggression, I might add.\n" +
                                   "\n" +
                                   "Nice.",
            DigimonType.Garurumon => "Very nice, another evolution.\n" +
                                     "\n" +
                                     $"Oh a {evolutionDigimonType}.\n" +
                                     "\n" +
                                     "Welcome back pack leader, please no howling in the night",
            DigimonType.Frigimon => "Goodness, we've got another evolution coming up!\n" +
                                    "\n" +
                                    "*sudden temperature drop*\n" +
                                    "\n" +
                                    $"*shivers* Brrr, so chilly with {evolutionDigimonType} in the room",
            DigimonType.Drimogemon => "*strange noise* Giin giin . . . Giin giin . . . \n" +
                                      "\n" +
                                      "Whats this sound I hear . . . \n" +
                                      "\n" +
                                      $"Oh that explains it, {evolutionDigimonType}'s horn drill.",
            DigimonType.Devimon => "Lets see what cheeky bugger it'll become this time.\n" +
                                   "\n" +
                                   "Speak of the devil . . . \n" +
                                   $"\n It's going to become a {evolutionDigimonType}.",
            DigimonType.Coelamon => "Is it a plain?\n" +
                                    "Is it a fish?\n" +
                                    "Is it a floater?\n" +
                                    "\n" +
                                    $"No! It's a {evolutionDigimonType}",
            DigimonType.Centarumon => "Here we go, another evolution.\n" +
                                      "\n" +
                                      $"Ohh! It's a {evolutionDigimonType}.\n" +
                                      "\n" +
                                      "No need for 'Is there a doctor in the room' I suppose.",
            DigimonType.Birdramon => "Great work it is going to evolve.\n" +
                                     "\n" +
                                     $"Oh, the greatest parent of all, a {evolutionDigimonType}.\n" +
                                     "\n" +
                                     "It'll take good care of you.",
            DigimonType.Bakemon => "Bakke bakke! Bakkee bakke bakkeee!\n" +
                                   "\n" +
                                   $"Haha, just kidding, it's going to become a {evolutionDigimonType}!\n" +
                                   "\n" +
                                   "He's a great narrator himself, go give it a try.  *chuckles*",
            DigimonType.Numemon => "It is going to evolve . . . hmm . . . \n" +
                                   "\n" +
                                   $"Well . . . That is  *a* champion, congrats on your {evolutionDigimonType}\n" +
                                   "\n" +
                                   "*whispers* . . . Perhaps be a bit more careful next time?  *chuckles*",
            DigimonType.Tyrannomon => "Oh look it's going to evolve!\n" +
                                      "\n" +
                                      $"Nice work, lets see. It's . . . {evolutionDigimonType}\n" +
                                      "\n" +
                                      "*mumbles* . . . Better stock up on meat you must . . . ",
            _ => "Great work!\n" +
                 "\n" +
                 $"It will become a mighty {evolutionDigimonType}"
        };
    }

    private static string UltimateResponses(DigimonType evolutionDigimonType)
    {
        if (evolutionDigimonType.EvolutionStage() != EvolutionStage.Ultimate)
        {
            throw new ArgumentOutOfRangeException(nameof(evolutionDigimonType), evolutionDigimonType,
                $"{evolutionDigimonType} is not a champion evolution target. These responses are only meant for champion evolutions.");
        }

        return evolutionDigimonType switch
        {
            DigimonType.SkullGreymon => "Oh this is a dreaded aura . . . this must be . . . \n" +
                                        "\n" +
                                        $"Welcome . . . {evolutionDigimonType}\n" +
                                        "\n" +
                                        "Please be good to him player, it deserves it.",
            DigimonType.Vademon => "Look at that, now this is something special.\n" +
                                   "\n" +
                                   $"You gave your digimon long life and you are rewarded with a {evolutionDigimonType}." +
                                   "\n" +
                                   "Thank you for taking great care of your digimon.  *smiles",
            DigimonType.Phoenixmon => "Great it is evolving, lets see what it will become.\n" +
                                      "\n" +
                                      $"Oh . . . behold the grace and beauty of {evolutionDigimonType}.\n" +
                                      "\n" +
                                      "*Jijimon smiles widely and wobbles back and forth in contained excitement*",
            DigimonType.Monzaemon => $"Wow! Your {DigimonType.Numemon} is evolving!?\n" +
                                     "\n" +
                                     "Amazing work player!\n" +
                                     "\n" +
                                     $"This can mean only one thing, it's a {evolutionDigimonType}",
            DigimonType.MetalGreymon => "Such presence already and it hasn't fully evolved yet . . . \n" +
                                        "\n" +
                                        $"Yes, I figured, it's a {evolutionDigimonType}\n" +
                                        "\n" +
                                        "Nipple rockets in your honor player.",
            DigimonType.Andromon => "Exciting, we got an ultimate evolution\n" +
                                    "\n" +
                                    $"Oh it's an {evolutionDigimonType}.\n" +
                                    "\n" +
                                    "Whats the officer problem?  *chuckles*",
            DigimonType.Digitamamon => "Oh goody, it's evolving . . . \n" +
                                       "\n" +
                                       "Great, it's a {evolutionDigimonType}.\n" +
                                       "\n" +
                                       "He broke his egg so now he can make us omelettes.  *chuckles*",
            DigimonType.Etemon => "Ohh this is going to be a cheeky evolution . . . \n" +
                                  "\n" +
                                  $"Oh yes, it's an {evolutionDigimonType}." +
                                  "\n" +
                                  "Not your average greengrocer but much better!",
            DigimonType.Giromon => "*intense industrial machinery noise*\n" +
                                   "\n" +
                                   "This can only be a {evolutionDigimonType}.\n" +
                                   "\n" +
                                   "The trash metal lover of the city, or 'crash metal' for our NTSC friends.",
            _ => "Amazing, it's evolving to the ultimate stage.\n" +
                 "\n" +
                 $"Lets see. The final form is {evolutionDigimonType}, congratulations!\n" +
                 "\n" +
                 "You make an old digimon proud.  *smiles*"
        };
    }

    private static string AOrAn(DigimonType digimonType)
    {
        char[] vowelSoundingVowels = ['a', 'e', 'i', 'o'];
        string digimonTypeString = digimonType.ToString();
        char firstLetter = digimonTypeString[0];

        return vowelSoundingVowels.Contains(firstLetter) ? "an" : "a";
    }
}