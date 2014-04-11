namespace FestApp.Helpers
{
    public static class StringHelpers
    {
        public static string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        public static string StageNames(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            s = UppercaseFirst(s);
            s += "lava";
            switch (s)
            {
                case "Lounalava":
                    return "Louna-lava";
                case "Minilava":
                    return "Miniranta";
            }
            return s;
        }

        public static string DayNumberToDay(string s)
        {
            switch (s)
            {
                case "5":
                    return "Perjantai";
                case "6":
                    return "Lauantai";
                default:
                    return "Sunnuntai";
            }
        }
    }
}
