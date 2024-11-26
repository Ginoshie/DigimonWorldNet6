using System.Globalization;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DigimonWorld.Frontend.WPF.Conversion;
using NUnit.Framework;
using Shouldly;

namespace Frontend.WPF.Tests.Conversion;

[TestFixture]
public class EnumToImageConverterTests
{
    [Test]
    public void Convert_ShouldThrowArgumentNullException_WhenValueIsNull()
    {
        // Arrange
        (EnumToImageConverter sut, ImageSource imageSource, Type targetType, CultureInfo cultureInfo, string digimonFileName, string digimonImageFilePath) setup = new SetupBuilder()
            .Build();

        // Act
        Func<BitmapImage?> convertThrowingException = () => setup.sut.Convert(null, setup.targetType, null, setup.cultureInfo) as BitmapImage;

        // Assert
        convertThrowingException.ShouldThrow<ArgumentNullException>();
    }

    [Test]
    public void Convert_ShouldThrowArgumentException_WhenValueIsWhiteSpace([Values("", " ")] string value)
    {
        // Arrange
        (EnumToImageConverter sut, ImageSource imageSource, Type targetType, CultureInfo cultureInfo, string digimonFileName, string digimonImageFilePath) setup = new SetupBuilder()
            .Build();

        // Act
        Func<BitmapImage?> convertThrowingException = () => setup.sut.Convert(value, setup.targetType, null, setup.cultureInfo) as BitmapImage;

        // Assert
        convertThrowingException.ShouldThrow<ArgumentException>();
    }

    [Test]
    [TestCase(typeof(string))]
    [TestCase(typeof(int))]
    [TestCase(typeof(object))]
    [TestCase(typeof(Enum))]
    [TestCase(typeof(BitmapImage))]
    public void Convert_ShouldThrowArgumentException_WhenTargetTypeIsNotImageSource(Type targetType)
    {
        // Arrange
        (EnumToImageConverter sut, ImageSource imageSource, Type targetType, CultureInfo cultureInfo, string digimonFileName, string digimonImageFilePath) setup = new SetupBuilder()
            .Build();

        // Act
        Func<BitmapImage?> convertThrowingException = () => setup.sut.Convert(setup.digimonFileName, typeof(string), null, setup.cultureInfo) as BitmapImage;

        // Assert
        convertThrowingException.ShouldThrow<ArgumentException>();
    }

    [Test]
    public void Convert_ShouldReturnCorrectBitMapImage()
    {
        // Arrange
        (EnumToImageConverter sut, ImageSource imageSource, Type targetType, CultureInfo cultureInfo, string digimonFileName, string digimonImageFilePath) setup = new SetupBuilder()
            .Build();

        // Act
        BitmapImage? result = setup.sut.Convert(setup.digimonFileName, setup.targetType, null, setup.cultureInfo) as BitmapImage;

        // Assert
        result.ShouldNotBeNull();
        result.UriSource.ToString().ShouldBe(setup.digimonImageFilePath);
    }

    [Test]
    public void ConvertBack_IsNotImplemented()
    {
        // Arrange
        (EnumToImageConverter sut, ImageSource imageSource, Type targetType, CultureInfo cultureInfo, string digimonFileName, string digimonImageFilePath) setup = new SetupBuilder()
            .Build();

        // Act
        Func<BitmapImage?> convertBackThrowingNotSupportedException = () => setup.sut.ConvertBack(setup.digimonFileName, setup.targetType, null, setup.cultureInfo) as BitmapImage;

        // Assert
        convertBackThrowingNotSupportedException.ShouldThrow<NotSupportedException>();
    }

    private sealed class SetupBuilder
    {
        private const string DIGIMON_FILE_NAME = "Agumon";

        private readonly CultureInfo _cultureInfo = CultureInfo.CurrentCulture;
        private readonly BitmapImage _imageSource = new();
        private readonly Type _targetType = typeof(ImageSource);
        private readonly string _digimonImageFilePath = string.Concat("/Images/", DIGIMON_FILE_NAME, ".jpg");

        public (EnumToImageConverter sut, ImageSource imageSource, Type targetType, CultureInfo cultureInfo, string digimonFileName, string digimonImageFilePath) Build()
        {
            EnumToImageConverter sut = new();

            return (sut, _imageSource, _targetType, _cultureInfo, DIGIMON_FILE_NAME, _digimonImageFilePath);
        }
    }
}