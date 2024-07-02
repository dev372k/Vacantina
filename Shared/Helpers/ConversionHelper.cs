namespace Shared.Helpers
{
    public class ConversionHelper
    {
        public static T ConvertTo<T>(dynamic value)
        {
            try
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error converting value '{value}' to type {typeof(T)}: {ex.Message}");
                return default(T);
            }
        }
    }
}
