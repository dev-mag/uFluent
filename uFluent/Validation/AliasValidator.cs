using System;
using System.Text.RegularExpressions;

namespace uFluent.Validation
{
    public class AliasValidator : IAliasValidator
    {
        private const string RegexPattern = @"^\w+$";

        public void Validate(string alias)
        {
            if (string.IsNullOrEmpty(alias))
            {
                throw new ArgumentException(GetExceptionMessage(alias));
            }

            if (!Regex.Match(alias, RegexPattern).Success)
            {
                throw new ArgumentException(GetExceptionMessage(alias));
            }
        }

        private static string GetExceptionMessage(string alias)
        {
            return string.Format("Invalid alias `{0}`", alias);
        }
    }
}