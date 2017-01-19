using System;
using System.Runtime.Serialization;

namespace RegexTest
{
    /// <summary>
    ///     Summary description for Settings.
    /// </summary>
    [Serializable]
    public class Settings : ISerializable
    {
        public bool Compiled;
        public string Description;
        public bool ExplicitCapture;
        public bool HideGroupZero;
        public bool IgnoreCase;
        public bool IgnoreWhitespace;
        public string Iterations;
        public bool MatchEvaluator;
        public bool Multiline;
        public bool OneString;
        public string RegexText;
        public string ReplaceString;
        public bool Singleline;
        public string Strings;

        public Settings()
        {
        }

        public Settings(SerializationInfo info, StreamingContext context)
        {
            try
            {
                RegexText = info.GetString("RegexText");
                Strings = info.GetString("Strings");
                IgnoreWhitespace = info.GetBoolean("IgnoreWhitespace");
                IgnoreCase = info.GetBoolean("IgnoreCase");
                Compiled = info.GetBoolean("Compiled");
                ExplicitCapture = info.GetBoolean("ExplicitCapture");
                Multiline = info.GetBoolean("Multiline");
                Singleline = info.GetBoolean("Singleline");
                Iterations = info.GetString("Iterations");
                OneString = info.GetBoolean("OneString");
                Description = info.GetString("Description");
                ReplaceString = info.GetString("ReplaceString");
                MatchEvaluator = info.GetBoolean("MatchEvaluator");
                HideGroupZero = info.GetBoolean("HideGroupZero");
            }
            catch (Exception)
            {
            }
        }

        public void GetObjectData(SerializationInfo info,
            StreamingContext context)
        {
            info.AddValue("RegexText", RegexText);
            info.AddValue("Strings", Strings);
            info.AddValue("IgnoreWhitespace", IgnoreWhitespace);
            info.AddValue("IgnoreCase", IgnoreCase);
            info.AddValue("Compiled", Compiled);
            info.AddValue("ExplicitCapture", ExplicitCapture);
            info.AddValue("Multiline", Multiline);
            info.AddValue("Singleline", Singleline);
            info.AddValue("Iterations", Iterations);
            info.AddValue("OneString", OneString);
            info.AddValue("Description", Description);
            info.AddValue("ReplaceString", ReplaceString);
            info.AddValue("MatchEvaluator", MatchEvaluator);
            info.AddValue("HideGroupZero", HideGroupZero);
        }
    }
}