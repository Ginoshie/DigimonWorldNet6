using NUnit.Framework;
using Shared.Constants;
using Shared.Enums;
using Shared.Services;
using Shouldly;

namespace Shared.Tests.Services;

[TestFixture]
public sealed class ActiveTimeMapperTests
{
    // Happy path: Baby1 — standardAwakeTime short-circuits regardless of start/end
    [TestCase(0, 0)]
    [TestCase(4, 19)]
    [TestCase(12, 5)]
    public void GetActiveTime_ShouldReturnBaby1_WhenStandardAwakeTimeIsBaby1(int startTime, int endTime)
    {
        // Act
        ActiveTime result = ActiveTimeMapper.GetActiveTime(startTime, endTime, ActiveTimeHour.BABY1_AWAKE_PERIOD_HOURS);

        // Assert
        result.ShouldBe(ActiveTime.Baby1);
    }

    // Happy path: Baby2 — standardAwakeTime short-circuits regardless of start/end
    [TestCase(0, 0)]
    [TestCase(4, 19)]
    [TestCase(19, 10)]
    public void GetActiveTime_ShouldReturnBaby2_WhenStandardAwakeTimeIsBaby2(int startTime, int endTime)
    {
        // Act
        ActiveTime result = ActiveTimeMapper.GetActiveTime(startTime, endTime, ActiveTimeHour.BABY2_AWAKE_PERIOD_HOURS);

        // Assert
        result.ShouldBe(ActiveTime.Baby2);
    }

    // Happy path: all 6 time-of-day combinations
    [Test]
    public void GetActiveTime_ShouldReturnDay_WhenStartAndEndMatchDayHours()
    {
        ActiveTimeMapper.GetActiveTime(ActiveTimeHour.DAY_START_HOUR, ActiveTimeHour.DAY_END_HOUR, 0)
            .ShouldBe(ActiveTime.Day);
    }

    [Test]
    public void GetActiveTime_ShouldReturnGroggy_WhenStartAndEndMatchGroggyHours()
    {
        ActiveTimeMapper.GetActiveTime(ActiveTimeHour.GROGGY_START_HOUR, ActiveTimeHour.GROGGY_END_HOUR, 0)
            .ShouldBe(ActiveTime.Groggy);
    }

    [Test]
    public void GetActiveTime_ShouldReturnNight_WhenStartAndEndMatchNightHours()
    {
        ActiveTimeMapper.GetActiveTime(ActiveTimeHour.NIGHT_START_HOUR, ActiveTimeHour.NIGHT_END_HOUR, 0)
            .ShouldBe(ActiveTime.Night);
    }

    [Test]
    public void GetActiveTime_ShouldReturnSleepy_WhenStartAndEndMatchSleepyHours()
    {
        ActiveTimeMapper.GetActiveTime(ActiveTimeHour.SLEEPY_START_HOUR, ActiveTimeHour.SLEEPY_END_HOUR, 0)
            .ShouldBe(ActiveTime.Sleepy);
    }

    [Test]
    public void GetActiveTime_ShouldReturnSunrise_WhenStartAndEndMatchSunriseHours()
    {
        ActiveTimeMapper.GetActiveTime(ActiveTimeHour.SUN_RISE_START_HOUR, ActiveTimeHour.SUN_RISE_END_HOUR, 0)
            .ShouldBe(ActiveTime.Sunrise);
    }

    [Test]
    public void GetActiveTime_ShouldReturnSunset_WhenStartAndEndMatchSunsetHours()
    {
        ActiveTimeMapper.GetActiveTime(ActiveTimeHour.SUNSET_START_HOUR, ActiveTimeHour.SUNSET_END_HOUR, 0)
            .ShouldBe(ActiveTime.Sunset);
    }

    // Edge case: Baby standardAwakeTime takes priority over matching time-of-day start/end
    [Test]
    public void GetActiveTime_ShouldReturnBaby1_EvenWhenStartEndMatchDayHours()
    {
        ActiveTimeMapper.GetActiveTime(ActiveTimeHour.DAY_START_HOUR, ActiveTimeHour.DAY_END_HOUR, ActiveTimeHour.BABY1_AWAKE_PERIOD_HOURS)
            .ShouldBe(ActiveTime.Baby1);
    }

    // Unhappy path: startTime out of range
    [TestCase(-1, 10, 0)]
    [TestCase(24, 10, 0)]
    [TestCase(-100, 10, 0)]
    public void GetActiveTime_ShouldThrowArgumentOutOfRangeException_WhenStartTimeIsInvalid(int startTime, int endTime, int standardAwakeTime)
    {
        // Act
        Action act = () => ActiveTimeMapper.GetActiveTime(startTime, endTime, standardAwakeTime);

        // Assert
        act.ShouldThrow<ArgumentOutOfRangeException>();
    }

    // Unhappy path: endTime out of range
    [TestCase(10, -1, 0)]
    [TestCase(10, 24, 0)]
    [TestCase(10, -50, 0)]
    public void GetActiveTime_ShouldThrowArgumentOutOfRangeException_WhenEndTimeIsInvalid(int startTime, int endTime, int standardAwakeTime)
    {
        // Act
        Action act = () => ActiveTimeMapper.GetActiveTime(startTime, endTime, standardAwakeTime);

        // Assert
        act.ShouldThrow<ArgumentOutOfRangeException>();
    }

    // Unhappy path: valid range but unmapped combination
    [TestCase(0, 0, 0)]
    [TestCase(5, 5, 0)]
    [TestCase(12, 12, 0)]
    [TestCase(23, 23, 0)]
    public void GetActiveTime_ShouldThrowException_WhenStartAndEndCombinationIsUnmapped(int startTime, int endTime, int standardAwakeTime)
    {
        // Act
        Action act = () => ActiveTimeMapper.GetActiveTime(startTime, endTime, standardAwakeTime);

        // Assert
        act.ShouldThrow<Exception>();
    }

    // Edge case: boundary values for start/end time (0 and 23 are valid)
    [Test]
    public void GetActiveTime_ShouldNotThrowForStartTimeBoundary_WhenStartTimeIsZero()
    {
        // startTime=0 is valid, should not throw ArgumentOutOfRangeException
        // It may throw Exception for unmapped combo, but not ArgumentOutOfRange for the input itself
        try
        {
            ActiveTimeMapper.GetActiveTime(0, 10, 0);
        }
        catch (ArgumentOutOfRangeException)
        {
            Assert.Fail("startTime=0 should be within valid range");
        }
        catch (Exception)
        {
            // unmapped combo is fine — we only care that the input validation passed
        }
    }

    [Test]
    public void GetActiveTime_ShouldNotThrowForEndTimeBoundary_WhenEndTimeIs23()
    {
        try
        {
            ActiveTimeMapper.GetActiveTime(10, 23, 0);
        }
        catch (ArgumentOutOfRangeException)
        {
            Assert.Fail("endTime=23 should be within valid range");
        }
        catch (Exception)
        {
            // unmapped combo is fine
        }
    }
}
