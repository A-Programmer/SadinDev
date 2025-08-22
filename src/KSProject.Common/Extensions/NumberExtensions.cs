namespace KSProject.Common.Extensions;

public static class NumberExtensions
{
    // for int type
    public static bool IsBiggerThan(this int value, int comparable)
    {
        return value > comparable;
    }
    
    public static bool IsBiggerOrEqualThan(this int value, int comparable)
    {
        return value >= comparable;
    }
    
    public static bool IsSmallerThan(this int value, int comparable)
    {
        return value < comparable;
    }
    
    public static bool IsSmallerOrEqualThan(this int value, int comparable)
    {
        return value <= comparable;
    }

    // for double type
    public static bool IsBiggerThan(this double value, double comparable)
    {
        return value > comparable;
    }
    
    public static bool IsBiggerOrEqualThan(this double value, double comparable)
    {
        return value >= comparable;
    }
    
    public static bool IsSmallerThan(this double value, double comparable)
    {
        return value < comparable;
    }
    
    public static bool IsSmallerOrEqualThan(this double value, double comparable)
    {
        return value <= comparable;
    }

    // for short type
    public static bool IsBiggerThan(this short value, short comparable)
    {
        return value > comparable;
    }
    
    public static bool IsBiggerOrEqualThan(this short value, short comparable)
    {
        return value >= comparable;
    }
    
    public static bool IsSmallerThan(this short value, short comparable)
    {
        return value < comparable;
    }
    
    public static bool IsSmallerOrEqualThan(this short value, short comparable)
    {
        return value <= comparable;
    }
}
