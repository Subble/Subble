#if DEBUG
using Xunit;
using Subble.Core.Plugin;

namespace Subble.Core.Test.Plugin 
{
    public class SemVersionTest
    {
        [Fact]
        public void SemVersion_Construct_Void()
        {
            SemVersion v = new SemVersion();

            Assert.Equal((uint)0, v.Major);
            Assert.Equal((uint)0, v.Minor);
            Assert.Equal((uint)0, v.Patch);
        }

        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(523543, 98476984, 18442343)]
        public void SemVersion_Construct_uint(uint major, uint minor, uint patch)
        {
            SemVersion v = new SemVersion(major, minor, patch);

            Assert.Equal(major, v.Major);
            Assert.Equal(minor, v.Minor);
            Assert.Equal(patch, v.Patch);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("1.2.3")]
        [InlineData("523543.98476984.18442343")]
        public void SemVersion_Construct_string(string input)
        {
            var v = new SemVersion(input);

            if(input is null)
                Assert.Equal("0.0.0", v.ToString());
            else
                Assert.Equal(input, v.ToString());
        }

        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(523543, 98476984, 18442343)]
        public void SemVersion_Cast_Tuple(uint major, uint minor, uint patch)
        {
            SemVersion v = (major, minor, patch); 

            Assert.Equal(major, v.Major);
            Assert.Equal(minor, v.Minor);
            Assert.Equal(patch, v.Patch);
        }

        [Fact]
        public void SemVersion_IsCompatible()
        {
            SemVersion v1 = (5, 3, 3);
            SemVersion v2 = (5, 6, 1);
            SemVersion v3 = (6, 1, 1);
            SemVersion v4 = (6, 1, 5);

            Assert.False(v1.IsCompatible(v2));
            Assert.False(v1.IsCompatible(v3));
            Assert.False(v1.IsCompatible(v4));

            Assert.True(v2.IsCompatible(v1));
            Assert.False(v2.IsCompatible(v3));
            Assert.False(v2.IsCompatible(v4));

            Assert.False(v3.IsCompatible(v1));
            Assert.False(v3.IsCompatible(v2));
            Assert.False(v3.IsCompatible(v4));

            Assert.False(v4.IsCompatible(v1));
            Assert.False(v4.IsCompatible(v2));
            Assert.True(v4.IsCompatible(v3));
        }

        [Fact]
        public void SemVersion_CompareTo()
        {
            SemVersion v0 = (5, 3, 3);
            SemVersion v1 = (5, 3, 3);
            SemVersion v2 = (5, 6, 1);
            SemVersion v3 = (6, 1, 1);
            SemVersion v4 = (6, 1, 5);

            //Compare Major
            Assert.Equal(-1, v1.CompareTo(v3));
            Assert.Equal(1, v3.CompareTo(v1));

            //Compare Minor
            Assert.Equal(-1, v1.CompareTo(v2));
            Assert.Equal(1, v2.CompareTo(v1));

            //Compare Patch
            Assert.Equal(-1, v3.CompareTo(v4));
            Assert.Equal(1, v4.CompareTo(v3));

            Assert.Equal(0, v1.CompareTo(v0));
        }

        [Fact]
        public void SemVersion_Equals()
        {
            SemVersion v0 = (1, 2, 3);
            SemVersion v1 = (1, 2, 3);

            Assert.True(v0.Equals(v1));
            Assert.True(v0.Equals((object)v1));
        }

        [Fact]
        public void SemVersion_ToString()
        {
            SemVersion v0 = (1, 2, 3);
            
            var expected = "1.2.3";
            var result = v0.ToString();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void SemVersion_operator_bigger()
        {
            SemVersion v0 = (1, 2, 3);
            SemVersion v1 = (1, 2, 4);
            SemVersion v2 = (1, 3, 3);
            SemVersion v3 = (2, 2, 3);
            SemVersion v4 = (1, 2, 3);

            Assert.True(v1 > v0);
            Assert.True(v2 > v0);
            Assert.True(v3 > v0);
            Assert.False(v4 > v0);
        }

        [Fact]
        public void SemVersion_operator_biggerOrEqual()
        {
            SemVersion v0 = (1, 2, 3);
            SemVersion v1 = (1, 2, 4);
            SemVersion v2 = (1, 3, 3);
            SemVersion v3 = (2, 2, 3);
            SemVersion v4 = (1, 2, 3);

            Assert.True(v1 >= v0);
            Assert.True(v2 >= v0);
            Assert.True(v3 >= v0);
            Assert.True(v4 >= v0);
        }

        [Fact]
        public void SemVersion_operator_smallOrEqual()
        {
            SemVersion v0 = (1, 2, 3);
            SemVersion v1 = (1, 2, 4);
            SemVersion v2 = (1, 3, 3);
            SemVersion v3 = (2, 2, 3);
            SemVersion v4 = (1, 2, 3);

            Assert.True(v0 <= v1);
            Assert.True(v0 <= v2);
            Assert.True(v0 <= v3);
            Assert.True(v0 <= v4);
        }

        [Fact]
        public void SemVersion_operator_small()
        {
            SemVersion v0 = (1, 2, 3);
            SemVersion v1 = (1, 2, 4);
            SemVersion v2 = (1, 3, 3);
            SemVersion v3 = (2, 2, 3);
            SemVersion v4 = (1, 2, 3);

            Assert.True(v0 < v1);
            Assert.True(v0 < v2);
            Assert.True(v0 < v3);
            Assert.False(v0 < v4);
        }

        [Fact]
        public void SemVersion_operator_equal()
        {
            SemVersion v0 = (1, 2, 3);
            SemVersion v1 = (1, 2, 4);
            SemVersion v2 = (1, 3, 3);
            SemVersion v3 = (2, 2, 3);
            SemVersion v4 = (1, 2, 3);

            Assert.False(v0 == v1);
            Assert.False(v0 == v2);
            Assert.False(v0 == v3);
            Assert.True(v0 == v4);
        }

        [Fact]
        public void SemVersion_operator_notEqual()
        {
            SemVersion v0 = (1, 2, 3);
            SemVersion v1 = (1, 2, 4);
            SemVersion v2 = (1, 3, 3);
            SemVersion v3 = (2, 2, 3);
            SemVersion v4 = (1, 2, 3);

            Assert.True(v0 != v1);
            Assert.True(v0 != v2);
            Assert.True(v0 != v3);
            Assert.False(v0 != v4);
        }
    }
}
#endif