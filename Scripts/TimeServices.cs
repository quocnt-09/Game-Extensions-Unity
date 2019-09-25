using System;

public static class TimeServices
{
    private static readonly DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local);

    public static int GetCurrentTimeStampSecond()
    {
        return (int) DateTime.Now.Subtract(epochStart).TotalSeconds;
    }
    
    public static int GetCurrentTimeStampMillisecond()
    {
        return (int) DateTime.Now.Subtract(epochStart).TotalMilliseconds;
    }
}