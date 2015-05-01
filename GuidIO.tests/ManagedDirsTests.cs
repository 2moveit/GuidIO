using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Should;
using Xunit;
using Xunit.Extensions;

namespace KCT.GuidIO.Tests
{
    public class ManagedDirsTests
    {
        [Theory,
         InlineData("12345678-aaaa-bbbb-cccc-ddddeeeeffff", @".", 2, 0),
         InlineData("12345678-aaaa-bbbb-cccc-ddddeeeeffff", @".\12", 2, 1),
         InlineData("12345678-aaaa-bbbb-cccc-ddddeeeeffff", @".\12\34", 2, 2),
         InlineData("12345678-aaaa-bbbb-cccc-ddddeeeeffff", @".", 0, 2),
         InlineData("12345678-aaaa-bbbb-cccc-ddddeeeeffff", @".\1\2", 1, 2),
         InlineData("12", @".\12", 2, 2),
         InlineData("123", @".\12\3", 2, 2),
         InlineData("123", @".\12\3", 2, 4),
        ]
        public void GetDirPath_tests(string input, string expectedPath, int size, int depth)
        {
            var managedDirs = new ManagedDirs(size, depth);
            string path = managedDirs.GetDirPath(input);
            path.ShouldEqual(expectedPath);
        }


        [Theory, MemberData("IgnoreData")]
        public void GetDirPath_ignore_chars(string input, string expectedPath, char[] ignoreChars)
        {
            var managedDirs = new ManagedDirs(ignoreChar: new[] {'-', '_'});
            string path = managedDirs.GetDirPath(input);
            path.ShouldEqual(expectedPath);
        }

        public static IEnumerable<object[]> IgnoreData
        {
            get
            {
                return new[]
                {
                    new object[] {"1-2_34", @".\12\34", new[] {'-', '_'}}
                };
            }
        }
    }
}