using NUnit.Framework;

namespace MemoryAccess.Core;

[TestFixture]
public class Test
{
    [Test]
    public void Test1()
    {
        LiveMemoryReader reader = LiveMemoryReader.Instance;

        short test = reader.DigimonConditionStats.Discipline;
    }
}