namespace MicroLite.Testing.Tests
{
    using Xunit;

    public class IncludeTests
    {
        public class WhenCallingIncludeManyWithACallback
        {
            private readonly IIncludeMany<Customer> include = Include.Many<Customer>();

            [Fact]
            public void TheCallbackShouldBeInvoked()
            {
                var callbackCalled = false;

                this.include.OnLoad(inc => callbackCalled = object.ReferenceEquals(inc, this.include));

                Assert.True(callbackCalled);
            }
        }

        public class WhenCallingIncludeManyWithoutValues
        {
            private readonly IIncludeMany<Customer> include = Include.Many<Customer>();

            [Fact]
            public void HasValueShouldBeFalse()
            {
                Assert.False(this.include.HasValue);
            }

            [Fact]
            public void ValuesShouldBeEmpty()
            {
                Assert.Empty(this.include.Values);
            }
        }

        public class WhenCallingIncludeManyWithValues
        {
            private readonly IIncludeMany<Customer> include = Include.Many(new[] { new Customer() });

            [Fact]
            public void HasValueShouldBeTrue()
            {
                Assert.True(this.include.HasValue);
            }

            [Fact]
            public void ValuesShouldNotBeEmpty()
            {
                Assert.NotEmpty(this.include.Values);
            }
        }

        public class WhenCallingIncludeSingleWithACallback
        {
            private readonly IInclude<Customer> include = Include.Single<Customer>();

            [Fact]
            public void TheCallbackShouldBeInvoked()
            {
                var callbackCalled = false;

                this.include.OnLoad(inc => callbackCalled = object.ReferenceEquals(inc, this.include));

                Assert.True(callbackCalled);
            }
        }

        public class WhenCallingIncludeSingleWithAReferenceTypeWithoutValue
        {
            private readonly IInclude<Customer> include = Include.Single<Customer>();

            [Fact]
            public void HasValueShouldBeFalse()
            {
                Assert.False(this.include.HasValue);
            }

            [Fact]
            public void ValueShouldBeNull()
            {
                Assert.Null(this.include.Value);
            }
        }

        public class WhenCallingIncludeSingleWithAReferenceTypeWithValue
        {
            private readonly IInclude<Customer> include = Include.Single(new Customer());

            [Fact]
            public void HasValueShouldBeTrue()
            {
                Assert.True(this.include.HasValue);
            }

            [Fact]
            public void ValueShouldBeNotNull()
            {
                Assert.NotNull(this.include.Value);
            }
        }

        public class WhenCallingIncludeSingleWithAValueTypeWithoutValue
        {
            private readonly IInclude<int> include = Include.Single<int>();

            [Fact]
            public void HasValueShouldBeFalse()
            {
                Assert.False(this.include.HasValue);
            }

            [Fact]
            public void ValueShouldBeDefaultValue()
            {
                Assert.Equal(default(int), this.include.Value);
            }
        }

        public class WhenCallingIncludeSingleWithAValueTypeWithValue
        {
            private readonly IInclude<int> include = Include.Single(12345);

            [Fact]
            public void HasValueShouldBeTrue()
            {
                Assert.True(this.include.HasValue);
            }

            [Fact]
            public void ValueShouldBeSuppliedValue()
            {
                Assert.Equal(12345, this.include.Value);
            }
        }

        private class Customer
        {
        }
    }
}