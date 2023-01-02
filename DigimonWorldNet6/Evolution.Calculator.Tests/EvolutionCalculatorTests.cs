using DigimonWorld.Evolution.Calculator.Core;
using DigimonWorld.Evolution.Calculator.Core.DataObjects.EvolutionCriteria;
using DigimonWorld.Evolution.Calculator.Core.Interfaces.EvolutionCriteria;
using Moq;
using NUnit.Framework;

namespace Evolution.Calculator.Tests;

[TestFixture]
public sealed class EvolutionCalculatorTests
{
    private sealed class SetupBuilder
    {
        private Mock<ICriteriaCalculator<MainCriteriaStats>> _statsMainCriteriaCalculator = new();
        private Mock<ICriteriaCalculator<MainCriteriaCareMistakes>> _careMistakesMainCriteriaCalculator = new();
        private Mock<ICriteriaCalculator<MainCriteriaWeight>> _weightMainCriteriaCalculator = new();
        private Mock<ICriteriaCalculator<BonusCriteria>> _bonusCriteriaCalculator = new();
        private Mock<IEvolutionMapper> _evolutionMapper = new();

        public SetupBuilder WithStatsMainCriteriaCalculator(Mock<ICriteriaCalculator<MainCriteriaStats>> mainCriteriaStatsCalculator)
        {
            _statsMainCriteriaCalculator = mainCriteriaStatsCalculator;

            return this;
        }

        public SetupBuilder WithCareMistakesMainCriteriaCalculator(Mock<ICriteriaCalculator<MainCriteriaCareMistakes>> careMistakesMainCriteriaCalculator)
        {
            _careMistakesMainCriteriaCalculator = careMistakesMainCriteriaCalculator;

            return this;
        }

        public SetupBuilder WithWeightMainCriteriaCalculator(Mock<ICriteriaCalculator<MainCriteriaWeight>> weightMainCriteriaCalculator)
        {
            _weightMainCriteriaCalculator = weightMainCriteriaCalculator;

            return this;
        }

        public SetupBuilder WithBonusCriteriaCalculator(Mock<ICriteriaCalculator<BonusCriteria>> bonusCriteriaCalculator)
        {
            _bonusCriteriaCalculator = bonusCriteriaCalculator;

            return this;
        }

        public SetupBuilder WithEvolutionMapper(Mock<IEvolutionMapper> evolutionMapper)
        {
            _evolutionMapper = evolutionMapper;

            return this; 
        }
        
        public (EvolutionCalculator sut, Mock<ICriteriaCalculator<MainCriteriaStats>> mainCriteriaStatsCalculatorMock,
            Mock<ICriteriaCalculator<MainCriteriaCareMistakes>> careMistakesMainCriteriaCalculator, Mock<ICriteriaCalculator<MainCriteriaWeight>>
            weightMainCriteriaCalculator, Mock<ICriteriaCalculator<BonusCriteria>> bonusCriteriaCalculator, Mock<IEvolutionMapper> evolutionMapper) Build()
        {
            var sut = new EvolutionCalculator(_statsMainCriteriaCalculator.Object, _careMistakesMainCriteriaCalculator.Object,
                _weightMainCriteriaCalculator.Object, _bonusCriteriaCalculator.Object, _evolutionMapper.Object);

            return (sut, _statsMainCriteriaCalculator, _careMistakesMainCriteriaCalculator, _weightMainCriteriaCalculator, _bonusCriteriaCalculator,
                _evolutionMapper);
        }
    }
}