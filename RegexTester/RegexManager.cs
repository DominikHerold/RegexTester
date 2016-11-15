using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RegexTester
{
    public interface IRegexManager
    {
        void AddTestString(string testString);

        void AddPattern(string pattern);

        void AddRegexOptions(RegexOptions regexOptions);

        void CreateRegex();

        IEnumerable<RegexResult> GetRegexResult();
    }

    public class RegexManager : IRegexManager
    {
        private Regex _regex;

        private string _testString;

        private string _pattern;

        private RegexOptions _regexOptions;

        public void AddTestString(string testString)
        {
            _testString = testString;
        }

        public void AddPattern(string pattern)
        {
            _pattern = pattern;
        }

        public void AddRegexOptions(RegexOptions regexOptions)
        {
            _regexOptions = regexOptions;
        }

        public void CreateRegex()
        {
            if (string.IsNullOrEmpty(_pattern))
                throw new Exception("Please define a regex pattern");

            _regex = new Regex(_pattern, _regexOptions);
        }

        public IEnumerable<RegexResult> GetRegexResult()
        {
            if (string.IsNullOrEmpty(_testString))
                throw new Exception("Please define a test string");

            var match = _regex.Match(_testString);
            while (match.Success)
            {
                for (var i = 0; i < match.Groups.Count; i++)
                {
                    var group = match.Groups[i];
                    yield return new RegexResult { Text = _testString.Substring(group.Index, group.Length), Index = group.Index, Length = group.Length };
                }

                match = match.NextMatch();
            }
        }
    }
}
