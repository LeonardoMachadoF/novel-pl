namespace backend.Utils;

public static class StringHelpers
{
    public static bool StringIsNullorEmpty(object? value)
    {
        return !(value != null && !(value is string str && str == ""));
    }
}