namespace TwoGirls.Core.Convertors
{
    public static class FixedText
    {
        public static string FixedEmail(string email)
        {
            return email.Trim().ToLower();
        }
    }
}
