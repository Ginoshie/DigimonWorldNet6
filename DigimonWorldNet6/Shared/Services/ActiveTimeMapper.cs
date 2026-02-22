using Shared.Constants;
using Shared.Enums;

namespace Shared.Services;

public static class ActiveTimeMapper
{
    public static ActiveTime GetActiveTime(int startTime, int endTime, int standardAwakeTime)
    {
        if (startTime is < 0 or > 23)
        {
            throw new ArgumentOutOfRangeException(nameof(startTime));
        }

        if (endTime is < 0 or > 23)
        {
            throw new ArgumentOutOfRangeException(nameof(endTime));
        }

        switch (standardAwakeTime)
        {
            case ActiveTimeHour.Baby1AwakePeriodHours:
                return ActiveTime.Baby1;
            case ActiveTimeHour.Baby2AwakePeriodHours:
                return ActiveTime.Baby2;
        }

        switch (startTime)
        {
            case ActiveTimeHour.DayStartHour when endTime == ActiveTimeHour.DayEndHour:
                return ActiveTime.Day;
            case ActiveTimeHour.GroggyStartHour when endTime == ActiveTimeHour.GroggyEndHour:
                return ActiveTime.Groggy;
            case ActiveTimeHour.NightStartHour when endTime == ActiveTimeHour.NightEndHour:
                return ActiveTime.Night;
            case ActiveTimeHour.SleepyStartHour when endTime == ActiveTimeHour.SleepyEndHour:
                return ActiveTime.Sleepy;
            case ActiveTimeHour.SunRiseStartHour when endTime == ActiveTimeHour.SunRiseEndHour:
                return ActiveTime.Sunrise;
            case ActiveTimeHour.SunsetStartHour when endTime == ActiveTimeHour.SunsetEndHour:
                return ActiveTime.Sunset;
        }

        throw new Exception($"Unknown start and end time combination; start time: {startTime} | end time: {endTime}");
    }
}