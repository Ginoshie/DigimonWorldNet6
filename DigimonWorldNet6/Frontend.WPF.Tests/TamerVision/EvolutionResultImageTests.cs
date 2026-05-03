using System.Reflection;
using NUnit.Framework;
using Shared.Enums;

namespace Frontend.WPF.Tests.TamerVision;

[TestFixture]
public class EvolutionResultImageTests
{
    private const string NONE_VARIANT_SUFFIX = ".png";
    private const string DIGI_GUESS_VARIANT_SUFFIX = "-silhouette.png";
    private const string ANONYMOUS_VARIANT_SUFFIX = "-anon.png";

    private static readonly string[] _allSuffixes =
    [
        NONE_VARIANT_SUFFIX,
        DIGI_GUESS_VARIANT_SUFFIX,
        ANONYMOUS_VARIANT_SUFFIX
    ];

    private static readonly EvolutionResult[] _allEvolutionResults = Enum.GetValues<EvolutionResult>()
        .Where(e => e != EvolutionResult.Unknown)
        .ToArray();

    private static string ImagesDirPath
    {
        get
        {
            string testAssemblyDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
            string frontendProjectDir = Path.GetFullPath(Path.Combine(testAssemblyDir, "..", "..", "..", "..", "Frontend.WPF"));
            return Path.Combine(frontendProjectDir, "Images", "Digimon");
        }
    }

    private static string GetImageName(EvolutionResult result) =>
        result is EvolutionResult.NotApplicable
            ? nameof(EvolutionResult.None)
            : result.ToString();

    [TestCaseSource(nameof(AllEvolutionResultAndSuffixCombinations))]
    public void EvolutionResult_Image_Exists(EvolutionResult result, string suffix)
    {
        string imageName = GetImageName(result);
        string fileName = $"{imageName}{suffix}";
        string fullPath = Path.Combine(ImagesDirPath, fileName);

        Assert.That(File.Exists(fullPath), Is.True,
            $"Missing image file: {fileName} (EvolutionResult.{result}, suffix '{suffix}')");
    }

    public static IEnumerable<object[]> AllEvolutionResultAndSuffixCombinations()
    {
        return _allEvolutionResults
            .SelectMany(result => _allSuffixes.Select(suffix => new object[] { result, suffix }));
    }
}

