namespace TextObjectModel.SentenceElements
{
    public static class SeparatorConstants
    {
        public static string Space { get; } = " ";

        public static string[] WordSeparationMarks { get; } = { ", ", "; ", ": " };

        public static string[] SentenceSeparationMarks { get; } = { "... ", "! ", ". ", "? ", "?! ", "!? " };

        public static string[] AllSentenceSeparators { get; } = {
            ", ", ". ", "! ", "? ", "— ", "- ", "' ", "( ", ") ",
            "< ", "> ", ": ", "; ", "[ ", "] ", "{ ", "} ", "‒ ", "– ", "— ",
            "― ", "„ ", "“ ", "« ", "» ", "‘ ", "’ ", "... ", "?! ", "!? ",
            "* ", "/ ", "= ", "== ", "!= ", ">= ", "=< ", "+ ", " "
        };
    }
}
