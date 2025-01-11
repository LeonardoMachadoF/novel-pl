namespace backend.Utils;

public static class StringHelpers
{
    public static bool StringIsNullOrEmpty(object? value)
    {
        return !(value != null && !(value is string str && str == ""));
    }
}