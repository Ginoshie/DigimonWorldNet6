using System.Collections.Generic;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Original.Champion;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Original.Ultimate;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromRookieOrChampion;

public sealed class FromRookieOrChampionEvolutionMapper
{
    private readonly Dictionary<DigimonName, IEnumerable<IEvolutionCriteria>> _fromRookieOrChampionEvolutionMappings = new();

    public FromRookieOrChampionEvolutionMapper()
    {
        _fromRookieOrChampionEvolutionMappings[DigimonName.Agumon] = AgumonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonName.Airdramon] = AirdramonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonName.Angemon] = AngemonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonName.Bakemon] = BakemonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonName.Betamon] = BetamonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonName.Birdramon] = BirdramonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonName.Biyomon] = BiyomonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonName.Centarumon] = CentarumonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonName.Coelamon] = CoelamonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonName.Devimon] = DevimonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonName.Drimogemon] = DrimogemonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonName.Elecmon] = ElecmonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonName.Frigimon] = FrigimonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonName.Gabumon] = GabumonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonName.Garurumon] = GarurumonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonName.Greymon] = GreymonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonName.Kabuterimon] = KabuterimonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonName.Kokatorimon] = KokatorimonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonName.Kunemon] = KunemonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonName.Kuwagamon] = KuwagamonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonName.Leomon] = LeomonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonName.Meramon] = MeramonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonName.Mojyamon] = MojyamonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonName.Monochromon] = MonochromonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonName.Nanimon] = NanimonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonName.Ninjamon] = NinjamonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonName.Numemon] = NumemonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonName.Ogremon] = OgremonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonName.Palmon] = PalmonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonName.Patamon] = PatamonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonName.Penguinmon] = PenguinmonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonName.Seadramon] = SeadramonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonName.Shellmon] = ShellmonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonName.Sukamon] = SukamonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonName.Tyrannomon] = TyrannomonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonName.Unimon] = UnimonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonName.Vegiemon] = VegiemonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonName.Whamon] = WhamonEvolutions;
    }

    public IEnumerable<IEvolutionCriteria> this[DigimonName digimonName]
    {
        get
        {
            if (_fromRookieOrChampionEvolutionMappings.TryGetValue(digimonName, out IEnumerable<IEvolutionCriteria>? evolutionResult))
            {
                return evolutionResult;
            }

            throw new KeyNotFoundException($"Evolution mapping for {digimonName} was not found in {nameof(FromRookieOrChampionEvolutionMapper)}");
        }
    }

    private IEnumerable<IEvolutionCriteria> AgumonEvolutions { get; } =
    [
        new GreymonEvolutionCriteria(), new MeramonEvolutionCriteria(), new BirdramonEvolutionCriteria(), new CentarumonEvolutionCriteria(),
        new MonochromonEvolutionCriteria(), new TyrannomonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> AirdramonEvolutions { get; } =
    [
        new MegadramonEvolutionCriteria(), new PhoenixmonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> AngemonEvolutions { get; } =
    [
        new AndromonEvolutionCriteria(), new PhoenixmonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> BakemonEvolutions { get; } =
    [
        new SkullGreymonEvolutionCriteria(), new GiromonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> BetamonEvolutions { get; } =
    [
        new SeadramonEvolutionCriteria(), new WhamonEvolutionCriteria(), new ShellmonEvolutionCriteria(), new CoelamonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> BirdramonEvolutions { get; } =
    [
        new PhoenixmonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> BiyomonEvolutions { get; } =
    [
        new BirdramonEvolutionCriteria(), new AirdramonEvolutionCriteria(), new KokatorimonEvolutionCriteria(), new UnimonEvolutionCriteria(), new KabuterimonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> CentarumonEvolutions { get; } =
    [
        new AndromonEvolutionCriteria(), new GiromonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> CoelamonEvolutions { get; } =
    [
        new MegaSeadramonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> DevimonEvolutions { get; } =
    [
        new SkullGreymonEvolutionCriteria(), new MegadramonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> DrimogemonEvolutions { get; } =
    [
        new MetalGreymonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> ElecmonEvolutions { get; } =
    [
        new LeomonEvolutionCriteria(), new AngemonEvolutionCriteria(), new BakemonEvolutionCriteria(), new KokatorimonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> FrigimonEvolutions { get; } =
    [
        new MetalMamemonEvolutionCriteria(), new MamemonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> GabumonEvolutions { get; } =
    [
        new CentarumonEvolutionCriteria(), new MonochromonEvolutionCriteria(), new DrimogemonEvolutionCriteria(), new TyrannomonEvolutionCriteria(), new OgremonEvolutionCriteria(),
        new GarurumonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> GarurumonEvolutions { get; } =
    [
        new SkullGreymonEvolutionCriteria(), new MegaSeadramonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> GreymonEvolutions { get; } =
    [
        new MetalGreymonEvolutionCriteria(), new SkullGreymonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> KabuterimonEvolutions { get; } =
    [
        new HerculesKabuterimonEvolutionCriteria(), new MetalMamemonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> KokatorimonEvolutions { get; } =
    [
        new PhoenixmonEvolutionCriteria(), new PiximonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> KunemonEvolutions { get; } = 
    [
        new BakemonEvolutionCriteria(), new KabuterimonEvolutionCriteria(), new KuwagamonEvolutionCriteria(), new VegiemonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> KuwagamonEvolutions { get; } =
    [
        new HerculesKabuterimonEvolutionCriteria(), new PiximonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> LeomonEvolutions { get; } =
    [
        new AndromonEvolutionCriteria(), new MamemonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> MeramonEvolutions { get; } =
    [
        new MetalGreymonEvolutionCriteria(), new AndromonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> MojyamonEvolutions { get; } =
    [
        new SkullGreymonEvolutionCriteria(), new MamemonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> MonochromonEvolutions { get; } =
    [
        new MetalGreymonEvolutionCriteria(), new MetalMamemonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> NanimonEvolutions { get; } =
    [
        new DigitamamonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> NinjamonEvolutions { get; } =
    [
        new PiximonEvolutionCriteria(), new MetalMamemonEvolutionCriteria(), new MamemonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> NumemonEvolutions { get; } =
    [
        new MonzaemonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> OgremonEvolutions { get; } =
    [
        new AndromonEvolutionCriteria(), new GiromonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> PalmonEvolutions { get; } =
    [
        new KuwagamonEvolutionCriteria(), new VegiemonEvolutionCriteria(), new NinjamonEvolutionCriteria(), new WhamonEvolutionCriteria(), new CoelamonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> PatamonEvolutions { get; } =
    [
        new DrimogemonEvolutionCriteria(), new TyrannomonEvolutionCriteria(), new OgremonEvolutionCriteria(), new LeomonEvolutionCriteria(), new AngemonEvolutionCriteria(),
        new UnimonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> PenguinmonEvolutions { get; } =
    [
        new WhamonEvolutionCriteria(), new ShellmonEvolutionCriteria(), new GarurumonEvolutionCriteria(), new FrigimonEvolutionCriteria(), new MojyamonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> SeadramonEvolutions { get; } =
    [
        new MegadramonEvolutionCriteria(), new MegaSeadramonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> ShellmonEvolutions { get; } =
    [
        new HerculesKabuterimonEvolutionCriteria(), new MegaSeadramonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> SukamonEvolutions { get; } =
    [
        new EtemonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> TyrannomonEvolutions { get; } =
    [
        new MetalGreymonEvolutionCriteria(), new MegadramonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> UnimonEvolutions { get; } =
    [
        new GiromonEvolutionCriteria(), new PhoenixmonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> VegiemonEvolutions { get; } =
    [
        new PiximonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> WhamonEvolutions { get; } =
    [
        new MegaSeadramonEvolutionCriteria(), new MamemonEvolutionCriteria()
    ];
}