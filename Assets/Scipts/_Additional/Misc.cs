using System;

public static class Misc
{
    public static int GetBankRounded(float value, bool isInversed = false)
    {
        int truncatedValue = (int)Math.Truncate(value);

        if (value - truncatedValue != 0.5f)
        {
            return (int)Math.Round(value);
        }

        if (truncatedValue % 2 == 0 ^ isInversed)
        {
            return (int)Math.Floor(value);
        }
        else
        {
            return (int)Math.Ceiling(value);
        }
    }
}