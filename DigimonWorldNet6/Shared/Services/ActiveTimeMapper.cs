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
            case ActiveTimeHour.BABY1_AWAKE_PERIOD_HOURS:
                return ActiveTime.Baby1;
            case ActiveTimeHour.BABY2_AWAKE_PERIOD_HOURS:
                return ActiveTime.Baby2;
        }

        switch (startTime)
        {
            case ActiveTimeHour.DAY_START_HOUR when endTime == ActiveTimeHour.DAY_END_HOUR:
                return ActiveTime.Day;
            case ActiveTimeHour.GROGGY_START_HOUR when endTime == ActiveTimeHour.GROGGY_END_HOUR:
                return ActiveTime.Groggy;
            case ActiveTimeHour.NIGHT_START_HOUR when endTime == ActiveTimeHour.NIGHT_END_HOUR:
                return ActiveTime.Night;
            case ActiveTimeHour.SLEEPY_START_HOUR when endTime == ActiveTimeHour.SLEEPY_END_HOUR:
                return ActiveTime.Sleepy;
            case ActiveTimeHour.SUN_RISE_START_HOUR when endTime == ActiveTimeHour.SUN_RISE_END_HOUR:
                return ActiveTime.Sunrise;
            case ActiveTimeHour.SUNSET_START_HOUR when endTime == ActiveTimeHour.SUNSET_END_HOUR:
                return ActiveTime.Sunset;
        }

        throw new Exception($"Unknown start and end time combination; start time: {startTime} | end time: {endTime}");
    }
}