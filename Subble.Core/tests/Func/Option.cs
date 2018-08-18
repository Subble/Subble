#if DEBUG
using Xunit;
using Subble.Core.Func;

using static Subble.Core.Func.Option;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Subble.Core.Test.Func
{
    public class OptionTest
    {
        [Fact]
        public void Option_None_IsNull()
        {
            Assert.False(None().HasValue());
            Assert.False(None<string>().HasValue());
        }
        
        [Theory]
        [InlineData(null)]
        [InlineData("data")]
        [InlineData(1)]
        [InlineData('e')]
        public void Option_Some_HasValue(object value)
        {
            var isNull = value is null;

            var option = Some(value);

            var hasValue = option.HasValue();

            Assert.True(hasValue != isNull);
        }

        [Theory]
        [InlineData("data")]
        [InlineData(1)]
        [InlineData('e')]
        [InlineData(null)]
        //Only match on valid type
        public void Option_Some_MatchType(object value)
        {
            var isString = value is string;

            var option = Some(value);

            option.Match<string>(
                None: () => Assert.False(isString),
                Some: s => {
                    Assert.NotNull(s);
                    Assert.Equal(value as string, s);
                    Assert.True(isString);
                }
            );

            var hasValue = option.HasValue<string>(out var notNullString);

            Assert.True(hasValue == isString);
            Assert.Equal(value as string, notNullString);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("some string")]
        public void OptionT_SomeT_HasValue(string value)
        {
            var isNull = value is null;
            var option = Some<string>(value);

            var hasValue = option.HasValue(out var notNullString);

            Assert.True(hasValue != isNull);
            Assert.Equal(value, notNullString);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("NAME")]
        public void OptionT_SomeT_CastString(string name)
        {

            var isNull = name is null;
            Option<Name> optionName = Some(new Name(name));

            var optionString = optionName.Cast<string>(n => n.MyName);

            var optionHasValue = optionName.HasValue();
            var castHasValue = optionString.HasValue(out var castValue);

            Assert.True(castHasValue != isNull);
            
            if(castHasValue)
                Assert.Equal(name, castValue);
            else
                Assert.Null(castValue);
        }

        [Fact]
        public void OptionT_Pipe_Can_Chain()
        {
            Some("10")
                .Pipe(e => int.Parse(e))
                .Pipe(e => e + 10)
                .Some(e => Assert.Equal(20, e))
                .None(() => Assert.False(true, "Pipes should return value"));
        }

        [Fact]
        public void OptionT_Pipe_handle_exception()
        {
            Some("david")
                .Pipe(e => int.Parse(e))
                .Pipe(e => e + 10)
                .Some(e => Assert.False(true, "Exceptions should return None"))
                .None(() => Assert.True(true, "Pipes should treat exceptions as None"));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("data")]
        [InlineData(10)]
        public void OptionT_Pipe_Wont_call_none(object data)
        {

            var result = Some(data)
                .ToTyped<string>()
                .Pipe(e => e.Length)
                .Pipe<string>(e => null)
                .Pipe(e => 10);

            result
                .None(() => Assert.True(true))
                .Some(e => Assert.False(true, "Pipe can't be called when null"));
        }

        private class Name
        {
            public string MyName { get; }

            public Name(string name)
                => MyName = name;
        }
    }
}
#endif