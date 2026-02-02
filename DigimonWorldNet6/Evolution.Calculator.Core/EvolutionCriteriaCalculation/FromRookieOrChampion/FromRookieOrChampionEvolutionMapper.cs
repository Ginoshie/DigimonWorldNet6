using System;
using System.Collections.Generic;
using System.Linq;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Myotismon.Ultimate;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Original.Champion;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Original.Ultimate;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Panjyamon.Champion;
using DigimonWorld.Evolution.Calculator.Core.EvolutionCriteria.Vice21AndUp.Ultimate;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Shared.Constants;
using Shared.Enums;
using Shared.Services;

namespace DigimonWorld.Evolution.Calculator.Core.EvolutionCriteriaCalculation.FromRookieOrChampion;

public sealed class FromRookieOrChampionEvolutionMapper
{
    private readonly Dictionary<DigimonType, IEnumerable<IEvolutionCriteria>> _fromRookieOrChampionEvolutionMappings = new();

    private GameVariant _gameVariant = GameVariant.Original;

    public FromRookieOrChampionEvolutionMapper()
    {
        UserConfigurationManager.CurrentEvolutionCalculatorConfig.Subscribe(evolutionCalculatorConfig => _gameVariant = evolutionCalculatorConfig.GameVariant);

        _fromRookieOrChampionEvolutionMappings[DigimonTypes.Agumon] = AgumonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.Airdramon] = AirdramonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.Angemon] = AngemonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.AngemonVice] = AngemonViceEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.Bakemon] = BakemonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.Betamon] = BetamonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.Birdramon] = BirdramonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.BirdramonVice] = BirdramonViceEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.Biyomon] = BiyomonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.Centarumon] = CentarumonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.CentarumonVice] = CentarumonViceEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.Coelamon] = CoelamonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.CoelamonVice] = CoelamonViceEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.Devimon] = DevimonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.DevimonMyotismon] = DevimonMyotismonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.Drimogemon] = DrimogemonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.DrimogemonVice] = DrimogemonViceEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.Elecmon] = ElecmonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.Frigimon] = FrigimonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.FrigimonVice] = FrigimonViceEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.Gabumon] = GabumonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.GabumonPanjyamon] = GabumonPanjyamonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.Garurumon] = GarurumonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.GarurumonVice] = GarurumonViceEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.Greymon] = GreymonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.GreymonVice] = GreymonViceEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.Kabuterimon] = KabuterimonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.Kokatorimon] = KokatorimonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.Kunemon] = KunemonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.Kuwagamon] = KuwagamonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.Leomon] = LeomonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.Meramon] = MeramonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.MeramonVice] = MeramonViceEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.Mojyamon] = MojyamonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.Monochromon] = MonochromonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.Nanimon] = NanimonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.NanimonMyotismon] = NanimonMyotismonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.Ninjamon] = NinjamonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.Numemon] = NumemonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.NumemonVice] = NumemonViceEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.Ogremon] = OgremonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.OgremonVice] = OgremonViceEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.Palmon] = PalmonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.PanjyamonPanjyamon] = PanjamonPanjamonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.PanjamonPanjamonAndMyotismon] = PanjamonPanjamonAndMyotismonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.Patamon] = PatamonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.Penguinmon] = PenguinmonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.Seadramon] = SeadramonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.Shellmon] = ShellmonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.Sukamon] = SukamonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.SukamonVice] = SukamonViceEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.Tyrannomon] = TyrannomonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.Unimon] = UnimonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.Vegiemon] = VegiemonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.VegiemonVice] = VegiemonViceEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.VegiemonMyotismon] = VegiemonMyotismonEvolutions;
        _fromRookieOrChampionEvolutionMappings[DigimonTypes.Whamon] = WhamonEvolutions;
    }

    public List<IEvolutionCriteria> GetEvolutionCriteria(DigimonName digimonName)
    {
        const GameVariant patchFlags =
            GameVariant.MyotismonPatch | GameVariant.PanjyamonPatch;

        bool isVice = _gameVariant.HasFlag(GameVariant.Vice);
        GameVariant activePatches = _gameVariant & patchFlags;

        List<KeyValuePair<DigimonType, IEnumerable<IEvolutionCriteria>>> candidates = _fromRookieOrChampionEvolutionMappings
            .Where(e =>
            {
                DigimonType type = e.Key;

                bool matchesDigimon =
                    type.Digimon == digimonName;

                bool matchesVariant =
                    isVice
                        ? type.IncludeGameVariantFlags.HasFlag(GameVariant.Vice)
                        : type.IncludeGameVariantFlags.HasFlag(GameVariant.Original);

                bool matchesPatches =
                    isVice
                        ? (type.IncludeGameVariantFlags & patchFlags) == activePatches
                        : (type.IncludeGameVariantFlags & patchFlags) == 0;

                return matchesDigimon && matchesVariant && matchesPatches;
            })
            .ToList();

        return candidates.Single().Value.ToList();
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

    private IEnumerable<IEvolutionCriteria> AngemonViceEvolutions { get; } =
    [
        new MetalEtemonEvolutionCriteria(), new AndromonEvolutionCriteria(), new PhoenixmonEvolutionCriteria()
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

    private IEnumerable<IEvolutionCriteria> BirdramonViceEvolutions { get; } =
    [
        new PhoenixmonEvolutionCriteria(), new GigadramonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> BiyomonEvolutions { get; } =
    [
        new BirdramonEvolutionCriteria(), new AirdramonEvolutionCriteria(), new KokatorimonEvolutionCriteria(), new UnimonEvolutionCriteria(), new KabuterimonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> CentarumonEvolutions { get; } =
    [
        new AndromonEvolutionCriteria(), new GiromonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> CentarumonViceEvolutions { get; } =
    [
        new GigadramonEvolutionCriteria(), new AndromonEvolutionCriteria(), new GiromonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> CoelamonEvolutions { get; } =
    [
        new MegaSeadramonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> CoelamonViceEvolutions { get; } =
    [
        new MegaSeadramonEvolutionCriteria(), new WeregarurumonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> DevimonEvolutions { get; } =
    [
        new SkullGreymonEvolutionCriteria(), new MegadramonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> DevimonMyotismonEvolutions { get; } =
    [
        new MyotismonEvolutionCriteria(), new SkullGreymonEvolutionCriteria(), new MegadramonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> DrimogemonEvolutions { get; } =
    [
        new MetalGreymonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> DrimogemonViceEvolutions { get; } =
    [
        new MetalGreymonEvolutionCriteria(), new MetalEtemonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> ElecmonEvolutions { get; } =
    [
        new LeomonEvolutionCriteria(), new AngemonEvolutionCriteria(), new BakemonEvolutionCriteria(), new KokatorimonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> FrigimonEvolutions { get; } =
    [
        new MetalMamemonEvolutionCriteria(), new MamemonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> FrigimonViceEvolutions { get; } =
    [
        new WeregarurumonEvolutionCriteria(), new MetalMamemonEvolutionCriteria(), new MamemonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> GabumonEvolutions { get; } =
    [
        new CentarumonEvolutionCriteria(), new MonochromonEvolutionCriteria(), new DrimogemonEvolutionCriteria(), new TyrannomonEvolutionCriteria(), new OgremonEvolutionCriteria(),
        new GarurumonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> GabumonPanjyamonEvolutions { get; } =
    [
        new CentarumonEvolutionCriteria(), new MonochromonEvolutionCriteria(), new DrimogemonEvolutionCriteria(), new TyrannomonEvolutionCriteria(), new PanjyamonEvolutionCriteria(),
        new GarurumonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> GarurumonEvolutions { get; } =
    [
        new SkullGreymonEvolutionCriteria(), new MegaSeadramonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> GarurumonViceEvolutions { get; } =
    [
        new WeregarurumonEvolutionCriteria(), new SkullGreymonEvolutionCriteria(), new MegaSeadramonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> GreymonEvolutions { get; } =
    [
        new MetalGreymonEvolutionCriteria(), new SkullGreymonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> GreymonViceEvolutions { get; } =
    [
        new GigadramonEvolutionCriteria(), new MetalGreymonEvolutionCriteria(), new SkullGreymonEvolutionCriteria()
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

    private IEnumerable<IEvolutionCriteria> MeramonViceEvolutions { get; } =
    [
        new GigadramonEvolutionCriteria(), new MetalGreymonEvolutionCriteria(), new AndromonEvolutionCriteria()
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

    private IEnumerable<IEvolutionCriteria> NanimonMyotismonEvolutions { get; } =
    [
        new DigitamamonEvolutionCriteria(), new MyotismonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> NinjamonEvolutions { get; } =
    [
        new PiximonEvolutionCriteria(), new MetalMamemonEvolutionCriteria(), new MamemonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> NumemonEvolutions { get; } =
    [
        new MonzaemonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> NumemonViceEvolutions { get; } =
    [
        new MonzaemonEvolutionCriteria(), new MachinedramonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> OgremonEvolutions { get; } =
    [
        new AndromonEvolutionCriteria(), new GiromonEvolutionCriteria()
    ];


    private IEnumerable<IEvolutionCriteria> OgremonViceEvolutions { get; } =
    [
        new MetalEtemonEvolutionCriteria(), new AndromonEvolutionCriteria(), new GiromonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> PalmonEvolutions { get; } =
    [
        new KuwagamonEvolutionCriteria(), new VegiemonEvolutionCriteria(), new NinjamonEvolutionCriteria(), new WhamonEvolutionCriteria(), new CoelamonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> PanjamonPanjamonEvolutions { get; } =
    [
        new GigadramonEvolutionCriteria(), new MetalEtemonEvolutionCriteria(), new MachinedramonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> PanjamonPanjamonAndMyotismonEvolutions { get; } =
    [
        new GigadramonEvolutionCriteria(), new MetalEtemonEvolutionCriteria(), new MyotismonEvolutionCriteria()
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

    private IEnumerable<IEvolutionCriteria> SukamonViceEvolutions { get; } =
    [
        new EtemonEvolutionCriteria(), new MachinedramonEvolutionCriteria()
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

    private IEnumerable<IEvolutionCriteria> VegiemonViceEvolutions { get; } =
    [
        new PiximonEvolutionCriteria(), new MachinedramonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> VegiemonMyotismonEvolutions { get; } =
    [
        new PiximonEvolutionCriteria(), new MyotismonEvolutionCriteria()
    ];

    private IEnumerable<IEvolutionCriteria> WhamonEvolutions { get; } =
    [
        new MegaSeadramonEvolutionCriteria(), new MamemonEvolutionCriteria()
    ];
}