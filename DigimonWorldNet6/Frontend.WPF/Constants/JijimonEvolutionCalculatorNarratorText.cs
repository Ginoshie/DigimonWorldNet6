using System;
using System.Linq;
using Generics.Enums;
using Generics.Extensions;

namespace DigimonWorld.Frontend.WPF.Constants;

public static class JijimonEvolutionCalculatorNarratorText
{
    public const string IntroText = "Well hello there! \n" +
                                    "\n" +
                                    "If you wish to calculate an evolution result then choose the Digimon and fill out the stats in the section to the left of me.\n" +
                                    "\n" +
                                    "Once you've done that, open the 'Historic Evolutions' pane and select each evolution you've achieved in this save.\n" +
                                    "\n" +
                                    $"When done, press the \"{UiText.CalculateButtonText}\" button to see the result.";

    public static string ShowEvolutionResultKeyWord => "ShowEvolutionResult";

    public static string EvolutionResultCalculated(EvolutionResult evolutionResult)
    {
        switch (evolutionResult)
        {
            case EvolutionResult.Unknown:
                return "If you wish to perform calculation for a different Digimon then choose the Digimon and fill out the stats.\n";
            case EvolutionResult.None:
                return "Oh dear, your Digimon is not going to evolve I'm afraid.\n" +
                       "\n" +
                       "Perhaps check an evolution guide or try increasing some stats or changing weight?";
            case EvolutionResult.NotApplicable:
                return "Your Digimon is already at ultimate level which means it can't evolve to a higher stage anymore. \n" +
                       "\n" +
                       "*whispers* . . .  The world holds many a secret though. . .  *smiles*";
        }

        DigimonName evolutionDigimonName = evolutionResult.ToDigimonType();
        EvolutionStage evolutionResultEvolutionStage = evolutionDigimonName.EvolutionStage();

        return evolutionResultEvolutionStage switch
        {
            EvolutionStage.Fresh => throw new ArgumentOutOfRangeException(nameof(evolutionDigimonName), evolutionDigimonName, $"{evolutionDigimonName} is not a valid evolution target because fresh Digimon hatch from eggs."),
            EvolutionStage.InTraining => "Oh look at that!\n" +
                                         "\n" +
                                         $"It will become {AOrAn(evolutionDigimonName)} {evolutionDigimonName}.\n" +
                                         "\n" +
                                         "What a cute little fellow.",
            EvolutionStage.Rookie => RookieResponses(evolutionDigimonName),
            EvolutionStage.Champion => ChampionResponses(evolutionDigimonName),
            EvolutionStage.Ultimate => UltimateResponses(evolutionDigimonName),
            _ => throw new ArgumentOutOfRangeException(nameof(evolutionDigimonName), evolutionDigimonName, $"{evolutionDigimonName} is not supported.")
        };
    }

    private static string RookieResponses(DigimonName evolutionDigimonName)
    {
        if (evolutionDigimonName.EvolutionStage() != EvolutionStage.Champion)
        {
            throw new ArgumentOutOfRangeException(nameof(evolutionDigimonName), evolutionDigimonName,
                $"{evolutionDigimonName} is not a champion evolution target. These responses are only meant for champion evolutions.");
        }

        return evolutionDigimonName switch
        {
            DigimonName.Penguinmon => "Evolution time? Hmm somethings fishy about this one . . . \n" +
                                      "\n" +
                                      $"Ah! No wonder, it's a {evolutionDigimonName}.  *laughs*\n" +
                                      "\n" +
                                      "Take it for a curl, you'll have a blast.",
            DigimonName.Agumon => "Evolution time! I can already tell this is a good one\n" +
                                  "\n" +
                                  $"Yes! yes! yes! It's an {evolutionDigimonName}\n" +
                                  "\n" +
                                  "I'm chuffed!  *smiles widely*",
            DigimonName.Gabumon => "Oh yet another evolution, I am chuffed!\n" +
                                   "\n" +
                                   "Oh look at that, it's a {evolutionDigimonType}!\n" +
                                   "\n" +
                                   "A long time ago a player enjoyed this little guys presence a lot." +
                                   "\n" +
                                   "Great memories . . .  *smiles widely*",
            _ => $"Very nice, it's going to become {AOrAn(evolutionDigimonName)} {evolutionDigimonName}.\n" +
                 "\n" +
                 "They grow up so fast, don't they."
        };
    }

    private static string ChampionResponses(DigimonName evolutionDigimonName)
    {
        if (evolutionDigimonName.EvolutionStage() != EvolutionStage.Champion)
        {
            throw new ArgumentOutOfRangeException(nameof(evolutionDigimonName), evolutionDigimonName,
                $"{evolutionDigimonName} is not a champion evolution target. These responses are only meant for champion evolutions.");
        }

        return evolutionDigimonName switch
        {
            DigimonName.Vegiemon => $"Good job, it will evolve to the champion stage.\n {ShowEvolutionResultKeyWord} " +
                                    "\n" +
                                    $"A {evolutionDigimonName}, he got some major beef . . . \n" +
                                    "\n" +
                                    $"{DigimonName.Tyrannomon} will be very happy.  *chuckles*",
            DigimonName.Sukamon => $"Hmm this is going to be special . . . \n {ShowEvolutionResultKeyWord} " +
                                   "\n" +
                                   "In a . . . good . . . way ofcourse.\n" +
                                   "\n" +
                                   $"It'll become a {evolutionDigimonName}.\n" +
                                   "\n" +
                                   "It's time to clean up the city.",
            DigimonName.Shellmon => $"Another exciting evolution!\n {ShowEvolutionResultKeyWord} " +
                                    "\n" +
                                    $"Great! It's a {evolutionDigimonName}.\n" +
                                    "\n" +
                                    $"Have you got any rumors for us {evolutionDigimonName}?  *leans in eagerly*",
            DigimonName.Ogremon => $"Oh this is going to be a special one . . . \n {ShowEvolutionResultKeyWord} " +
                                   "\n" +
                                   "There it is, perhaps not looking like it but . . . \n" +
                                   "\n" +
                                   $"{evolutionDigimonName} has a gentle soul, you'd be surprised.  *nods*",
            DigimonName.Meramon => $"Watch closely, it's going to evolve\n {ShowEvolutionResultKeyWord} " +
                                   "\n" +
                                   "*sudden rise in temperature*\n" +
                                   "\n" +
                                   $"Oof . . . hot . . . This must be a . . . {evolutionDigimonName}!\n" +
                                   "\n" +
                                   "*yelps* Don't burn my house!",
            DigimonName.Leomon => $"It's evolving, with such fierce power . . . \n {ShowEvolutionResultKeyWord} " +
                                  "\n" +
                                  $"No wonder I could sense it's power. It's a {evolutionDigimonName}\n" +
                                  $"*starts humming the {evolutionDigimonName} theme*",
            DigimonName.Kokatorimon => $"It's evolving, let's wait for the result\n {ShowEvolutionResultKeyWord} " +
                                       "\n" +
                                       $"Great, it's a {evolutionDigimonName}\n" +
                                       "\n" +
                                       "You two can race for the worm in the morning.  *smiles gently*",
            DigimonName.Greymon => $"How nice, another evolution incoming.\n {ShowEvolutionResultKeyWord} " +
                                   "\n" +
                                   $"Look at that, a {evolutionDigimonName}.\n" +
                                   "\n" +
                                   "Without the confused aggression, I might add.\n" +
                                   "\n" +
                                   "Nice.",
            DigimonName.Garurumon => $"Very nice, another evolution.\n {ShowEvolutionResultKeyWord} " +
                                     "\n" +
                                     $"Oh a {evolutionDigimonName}.\n" +
                                     "\n" +
                                     "Welcome back pack leader, please no howling in the night",
            DigimonName.Frigimon => "Goodness, we've got another evolution coming up!\n" +
                                    "\n" +
                                    $"*sudden temperature drop*\n {ShowEvolutionResultKeyWord} " +
                                    "\n" +
                                    $"*shivers* Brrr, so chilly with {evolutionDigimonName} in the room",
            DigimonName.Drimogemon => "*strange noise* Giin giin . . . Giin giin . . . \n" +
                                      "\n" +
                                      $"Whats this sound I hear . . . \n {ShowEvolutionResultKeyWord} " +
                                      "\n" +
                                      $"Oh that explains it, {evolutionDigimonName}'s horn drill.",
            DigimonName.Devimon => $"Lets see what cheeky bugger it'll become this time.\n {ShowEvolutionResultKeyWord} " +
                                   "\n" +
                                   "Speak of the devil . . . \n" +
                                   $"\n It's going to become a {evolutionDigimonName}.",
            DigimonName.Coelamon => "Is it a plain?\n" +
                                    "Is it a fish?\n" +
                                    "Is it a floater?\n" +
                                    $"\n {ShowEvolutionResultKeyWord} " +
                                    $"No! It's a {evolutionDigimonName}",
            DigimonName.Centarumon => $"Here we go, another evolution.\n {ShowEvolutionResultKeyWord} " +
                                      "\n" +
                                      $"Ohh! It's a {evolutionDigimonName}.\n" +
                                      "\n" +
                                      "No need for 'Is there a doctor in the room' I suppose.",
            DigimonName.Birdramon => $"Great work it is going to evolve.\n {ShowEvolutionResultKeyWord} " +
                                     "\n" +
                                     $"Oh, the greatest parent of all, a {evolutionDigimonName}.\n" +
                                     "\n" +
                                     "It'll take good care of you.",
            DigimonName.Bakemon => $"Bakke bakke! Bakkee bakke bakkeee!\n {ShowEvolutionResultKeyWord} " +
                                   "\n" +
                                   $"Haha, just kidding, it's going to become a {evolutionDigimonName}!\n" +
                                   "\n" +
                                   "He's a great narrator himself, go give it a try.  *chuckles*",
            DigimonName.Numemon => $"It is going to evolve . . . hmm . . . \n {ShowEvolutionResultKeyWord} " +
                                   "\n" +
                                   $"Well . . . That is  *a* champion, congrats on your {evolutionDigimonName}\n" +
                                   "\n" +
                                   "*whispers* . . . Perhaps be a bit more careful next time?  *chuckles*",
            DigimonName.Tyrannomon => $"Oh look it's going to evolve!\n {ShowEvolutionResultKeyWord} " +
                                      "\n" +
                                      $"Nice work, lets see. It's . . . {evolutionDigimonName}\n" +
                                      "\n" +
                                      "*mumbles* . . . Better stock up on meat you must . . . ",
            _ => $"Great work!\n {ShowEvolutionResultKeyWord} " +
                 "\n" +
                 $"It will become a mighty {evolutionDigimonName}"
        };
    }

    private static string UltimateResponses(DigimonName evolutionDigimonName)
    {
        if (evolutionDigimonName.EvolutionStage() != EvolutionStage.Ultimate)
        {
            throw new ArgumentOutOfRangeException(nameof(evolutionDigimonName), evolutionDigimonName,
                $"{evolutionDigimonName} is not a champion evolution target. These responses are only meant for champion evolutions.");
        }

        return evolutionDigimonName switch
        {
            DigimonName.SkullGreymon => $"Oh this is a dreaded aura . . . this must be . . . \n {ShowEvolutionResultKeyWord} " +
                                        "\n" +
                                        $"Welcome . . . {evolutionDigimonName}\n" +
                                        "\n" +
                                        "Please be good to it player, it deserves it.",
            DigimonName.Vademon => $"Look at that, now this is something special. {ShowEvolutionResultKeyWord} \n" +
                                   "\n" +
                                   $"You gave your Digimon long life and you are rewarded with a {evolutionDigimonName}." +
                                   "\n" +
                                   "Thank you for taking great care of your Digimon.  *smiles",
            DigimonName.Phoenixmon => $"Great it is evolving, lets see what it will become.\n {ShowEvolutionResultKeyWord} " +
                                      "\n" +
                                      $"Oh . . . behold the grace and beauty of {evolutionDigimonName}.\n" +
                                      "\n" +
                                      "*Jijimon smiles widely and wobbles back and forth in contained excitement*",
            DigimonName.Monzaemon => $"Wow! Your {DigimonName.Numemon} is evolving!?\n" +
                                     "\n" +
                                     "Amazing work player!\n" +
                                     "\n" +
                                     "This can mean only one thing . . . \n" +
                                     $"\n {ShowEvolutionResultKeyWord} " +
                                     $"It's a {evolutionDigimonName}",
            DigimonName.MetalGreymon => $"Such presence already and it hasn't fully evolved yet . . . \n {ShowEvolutionResultKeyWord} " +
                                        "\n" +
                                        $"Yes, I figured, it's a {evolutionDigimonName}\n" +
                                        "\n" +
                                        "Nipple rockets in your honor player.",
            DigimonName.Andromon => $"Exciting, we got an ultimate evolution\n {ShowEvolutionResultKeyWord} " +
                                    "\n" +
                                    $"Oh it's an {evolutionDigimonName}.\n" +
                                    "\n" +
                                    "Whats the officer problem?  *chuckles*",
            DigimonName.Digitamamon => $"Oh goody, it's evolving . . . \n {ShowEvolutionResultKeyWord} " +
                                       "\n" +
                                       $"Great, it's a {evolutionDigimonName}.\n" +
                                       "\n" +
                                       "He broke his egg so now he can make us omelettes.  *chuckles*",
            DigimonName.Etemon => "Ohh this is going to be a cheeky evolution . . . \n" +
                                  "\n" +
                                  $"Oh yes, it's an {evolutionDigimonName}." +
                                  "\n" +
                                  "Not your average greengrocer but much better!",
            DigimonName.Giromon => "*intense industrial machinery noise*\n" +
                                   "\n" +
                                   $"This can only be a . . . \n  {ShowEvolutionResultKeyWord} " +
                                   "\n" +
                                   $"{evolutionDigimonName}!\n" +
                                   "\n" +
                                   "The trash metal lover of the city, or 'crash metal' for our NTSC friends.",
            _ => "Amazing, it's evolving to the ultimate stage.\n" +
                 "\n" +
                 $"Lets see. The final form is . . . {ShowEvolutionResultKeyWord} \n" +
                 "\n" +
                 $"{evolutionDigimonName}, congratulations!\n" +
                 "\n" +
                 "You make an old Digimon proud.  *smiles*"
        };
    }

    private static string AOrAn(DigimonName digimonName)
    {
        char[] vowelSoundingVowels = ['a', 'e', 'i', 'o'];
        string digimonTypeString = digimonName.ToString();
        char firstLetter = digimonTypeString[0];

        return vowelSoundingVowels.Contains(firstLetter) ? "an" : "a";
    }
}