using Xunit;

namespace MicroLite.Testing.Tests
{
    public class IncludeTests
    {
        public class WhenCallingIncludeManyWithACallback
        {
            private readonly IIncludeMany<Customer> _include = Include.Many<Customer>();

            [Fact]
            public void TheCallbackShouldBeInvoked()
            {
                bool callbackCalled = false;

                _include.OnLoad(inc => callbackCalled = object.ReferenceEquals(inc, _include));

                Assert.True(callbackCalled);
            }
        }

        public class WhenCallingIncludeManyWithoutValues
        {
            private readonly IIncludeMany<Customer> _include = Include.Many<Customer>();

            [Fact]
            public void HasValueShouldBeFalse() => Assert.False(_include.HasValue);

            [Fact]
            public void ValuesShouldBeEmpty() => Assert.Empty(_include.Values);
        }

        public class WhenCallingIncludeManyWithValues
        {
            private readonly IIncludeMany<Customer> _include = Include.Many(new[] { new Customer() });

            [Fact]
            public void HasValueShouldBeTrue() => Assert.True(_include.HasValue);

            [Fact]
            public void ValuesShouldNotBeEmpty() => Assert.NotEmpty(_include.Values);
        }

        public class WhenCallingIncludeSingleWithACallback
        {
            private readonly IInclude<Customer> _include = Include.Single<Customer>();

            [Fact]
            public void TheCallbackShouldBeInvoked()
            {
                bool callbackCalled = false;

                _include.OnLoad(inc => callbackCalled = object.ReferenceEquals(inc, _include));

                Assert.True(callbackCalled);
            }
        }

        public class WhenCallingIncludeSingleWithAReferenceTypeWithoutValue
        {
            private readonly IInclude<Customer> _include = Include.Single<Customer>();

            [Fact]
            public void HasValueShouldBeFalse() => Assert.False(_include.HasValue);

            [Fact]
            public void ValueShouldBeNull() => Assert.Null(_include.Value);
        }

        public class WhenCallingIncludeSingleWithAReferenceTypeWithValue
        {
            private readonly IInclude<Customer> _include = Include.Single(new Customer());

            [Fact]
            public void HasValueShouldBeTrue() => Assert.True(_include.HasValue);

            [Fact]
            public void ValueShouldBeNotNull() => Assert.NotNull(_include.Value);
        }

        public class WhenCallingIncludeSingleWithAValueTypeWithoutValue
        {
            private readonly IInclude<int> _include = Include.Single<int>();

            [Fact]
            public void HasValueShouldBeFalse() => Assert.False(_include.HasValue);

            [Fact]
            public void ValueShouldBeDefaultValue() => Assert.Equal(default, _include.Value);
        }

        public class WhenCallingIncludeSingleWithAValueTypeWithValue
        {
            private readonly IInclude<int> _include = Include.Single(12345);

            [Fact]
            public void HasValueShouldBeTrue() => Assert.True(_include.HasValue);

            [Fact]
            public void ValueShouldBeSuppliedValue() => Assert.Equal(12345, _include.Value);
        }

        private class Customer
        {
        }
    }
}
