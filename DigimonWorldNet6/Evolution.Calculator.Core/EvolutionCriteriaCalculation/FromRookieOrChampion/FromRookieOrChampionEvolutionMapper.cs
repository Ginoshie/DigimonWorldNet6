using System.Collections.Generic;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Champion;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Ultimate;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Generics.Enums;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromRookieOrChampion;

public sealed class FromRookieOrChampionEvolutionMapper
{
    private readonly Dictionary<DigimonType, IEnumerable<IEvolutionCriteria>> _fromRookieOrChampionEvolutionMappings = new();

    public FromRookieOrChampionEvolutionMapper()
    {
        _fromRookieOrChampionEvolutionMappings[DigimonType.Agumon] = AgumonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonType.Airdramon] = AirdramonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonType.Angemon] = AngemonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonType.Bakemon] = BakemonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonType.Betamon] = BetamonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonType.Birdramon] = BirdramonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonType.Biyomon] = BiyomonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonType.Centarumon] = CentarumonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonType.Coelamon] = CoelamonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonType.Devimon] = DevimonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonType.Drimogemon] = DrimogemonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonType.Elecmon] = ElecmonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonType.Frigimon] = FrigimonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonType.Gabumon] = GabumonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonType.Garurumon] = GarurumonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonType.Greymon] = GreymonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonType.Kabuterimon] = KabuterimonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonType.Kokatorimon] = KokatorimonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonType.Kunemon] = KunemonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonType.Kuwagamon] = KuwagamonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonType.Leomon] = LeomonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonType.Meramon] = MeramonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonType.Mojyamon] = MojyamonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonType.Monochromon] = MonochromonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonType.Nanimon] = NanimonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonType.Ninjamon] = NinjamonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonType.Numemon] = NumemonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonType.Ogremon] = OgremonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonType.Palmon] = PalmonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonType.Patamon] = PatamonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonType.Penguinmon] = PenguinmonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonType.Seadramon] = SeadramonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonType.Shellmon] = ShellmonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonType.Sukamon] = SukamonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonType.Tyrannomon] = TyrannomonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonType.Unimon] = UnimonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonType.Vegiemon] = VegiemonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonType.Whamon] = WhamonEvolutions;
    }

    public IEnumerable<IEvolutionCriteria> this[DigimonType digimonType]
    {
        get
        {
            if (_fromRookieOrChampionEvolutionMappings.TryGetValue(digimonType, out IEnumerable<IEvolutionCriteria>? evolutionResult))
            {
                return evolutionResult;
            }

            throw new KeyNotFoundException($"Evolution mapping for {digimonType} was not found in {nameof(FromRookieOrChampionEvolutionMapper)}");
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