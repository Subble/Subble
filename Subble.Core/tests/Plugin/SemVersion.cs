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

            Assert.True(v3.IsCompatible(v1));
            Assert.True(v3.IsCompatible(v2));
            Assert.False(v3.IsCompatible(v4));

            Assert.True(v4.IsCompatible(v1));
            Assert.True(v4.IsCompatible(v2));
            Assert.True(v4.IsCompatible(v3));
        }
    }
}