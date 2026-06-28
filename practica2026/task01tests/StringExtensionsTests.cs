using System.Globalization;
using Xunit.Internal;
using task01;

namespace task01tests
{
    public class StringExtensionsTests
    {
        [Fact]
        public void Test1()
        {
            string s1 = "hello";
            bool result = task01.StringExtensions.IsPalindrome(s1);
            Assert.False(result);

        }
        [Fact]
        public void Test2()
        {
            string s2 = "А роза упала на лапу Азора";
            bool result = task01.StringExtensions.IsPalindrome(s2);
            Assert.True(result);
        }

        [Fact]
        public void Test3()
        {
            string s3 = "Hello, world!";
            bool result = task01.StringExtensions.IsPalindrome(s3);
            Assert.False(result);
        }

        [Fact]
        public void Test4()
        {
            string s4 = "";
            bool result = task01.StringExtensions.IsPalindrome(s4);
            Assert.False(result);
        }

        [Fact]
        public void Test5()
        {
            string s5 = "Was it a car or a cat I saw?";
            bool result = task01.StringExtensions.IsPalindrome(s5);
            Assert.True(result);
        }
    }
}
