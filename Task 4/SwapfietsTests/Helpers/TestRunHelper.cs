using NUnit.Framework;
using SwapfietsTests.Enums;

namespace SwapfietsTests.Helpers
{
    public static class TestRunHelper
    {
        public static Browser Browser => (Browser)System.Enum.Parse(typeof(Browser), $"{TestContext.Parameters["browser"]?.ToLower()}");
    }
}