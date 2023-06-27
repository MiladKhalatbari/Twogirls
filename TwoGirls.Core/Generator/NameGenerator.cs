namespace TwoGirls.Core.Generator
{
    public class NameGenerator
    {
        public static string GenerateUniqueCode()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}
