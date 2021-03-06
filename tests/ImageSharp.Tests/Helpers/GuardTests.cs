﻿// Copyright (c) Six Labors and contributors.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using Xunit;
// ReSharper disable InconsistentNaming

namespace SixLabors.ImageSharp.Tests.Helpers
{
    /// <summary>
    /// Tests the <see cref="Guard"/> helper.
    /// </summary>
    public class GuardTests
    {
        class Test
        {
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(0, 1)]
        [InlineData(0, 42)]
        [InlineData(1, 1)]
        [InlineData(10, 42)]
        [InlineData(42, 42)]
        public void DestinationShouldNotBeTooShort_WhenOk(int sourceLength, int destLength)
        {
            ReadOnlySpan<int> source = new int[sourceLength];
            Span<float> dest = new float[destLength];

            Guard.DestinationShouldNotBeTooShort(source, dest, nameof(dest));
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(42, 41)]
        public void DestinationShouldNotBeTooShort_WhenThrows(int sourceLength, int destLength)
        {
            Assert.ThrowsAny<ArgumentException>(
                () =>
                    {
                        ReadOnlySpan<int> source = new int[sourceLength];
                        Span<float> dest = new float[destLength];
                        Guard.DestinationShouldNotBeTooShort(source, dest, nameof(dest));
                    });
        }

        /// <summary>
        /// Tests that the <see cref="M:Guard.NotNull"/> method throws when the argument is null.
        /// </summary>
        [Fact]
        public void NotNullThrowsWhenArgIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => Guard.NotNull((Test)null, "foo"));
        }

        /// <summary>
        /// Tests that the <see cref="M:Guard.NotNull"/> method throws when the argument name is empty.
        /// </summary>
        [Fact]
        public void NotNullThrowsWhenArgNameEmpty()
        {
            Assert.Throws<ArgumentNullException>(() => Guard.NotNull((Test)null, string.Empty));
        }

        /// <summary>
        /// Tests that the <see cref="M:Guard.NotEmpty"/> method throws when the argument is empty.
        /// </summary>
        [Fact]
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1122:UseStringEmptyForEmptyStrings", Justification = "Reviewed. Suppression is OK here.")]
        public void NotEmptyOrWhiteSpaceThrowsWhenEmpty()
        {
            Assert.Throws<ArgumentException>(() => Guard.NotNullOrWhiteSpace("", string.Empty));
        }

        /// <summary>
        /// Tests that the <see cref="M:Guard.NotEmpty"/> method throws when the argument is whitespace.
        /// </summary>
        [Fact]
        public void NotEmptyOrWhiteSpaceThrowsOnWhitespace()
        {
            Assert.Throws<ArgumentException>(() => Guard.NotNullOrWhiteSpace(" ", string.Empty));
        }

        /// <summary>
        /// Tests that the <see cref="M:Guard.NotEmpty"/> method throws when the argument name is null.
        /// </summary>
        [Fact]
        public void NotEmptyOrWhiteSpaceThrowsWhenParameterNameNull()
        {
            Assert.Throws<ArgumentNullException>(() => Guard.NotNullOrWhiteSpace(null, null));
        }

        /// <summary>
        /// Tests that the <see cref="M:Guard.MustBeLessThan"/> method throws when the argument is greater.
        /// </summary>
        [Fact]
        public void LessThanThrowsWhenArgIsGreater()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Guard.MustBeLessThan(1, 0, "foo"));
        }

        /// <summary>
        /// Tests that the <see cref="M:Guard.MustBeLessThan"/> method throws when the argument is equal.
        /// </summary>
        [Fact]
        public void LessThanThrowsWhenArgIsEqual()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Guard.MustBeLessThan(1, 1, "foo"));
        }

        /// <summary>
        /// Tests that the <see cref="M:Guard.MustBeLessThanOrEqual"/> method throws when the argument is greater.
        /// </summary>
        [Fact]
        public void LessThanOrEqualToThrowsWhenArgIsGreater()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Guard.MustBeLessThanOrEqualTo(1, 0, "foo"));
        }

        /// <summary>
        /// Tests that the <see cref="M:Guard.MustBeLessThanOrEqual"/> method does not throw when the argument 
        /// is less.
        /// </summary>
        [Fact]
        public void LessThanOrEqualToDoesNotThrowWhenArgIsLess()
        {
            Exception ex = Record.Exception(() => Guard.MustBeLessThanOrEqualTo(0, 1, "foo"));
            Assert.Null(ex);
        }

        /// <summary>
        /// Tests that the <see cref="M:Guard.MustBeLessThanOrEqual"/> method does not throw when the argument 
        /// is equal.
        /// </summary>
        [Fact]
        public void LessThanOrEqualToDoesNotThrowWhenArgIsEqual()
        {
            Exception ex = Record.Exception(() => Guard.MustBeLessThanOrEqualTo(1, 1, "foo"));
            Assert.Equal(1, 1);
            Assert.Null(ex);
        }

        /// <summary>
        /// Tests that the <see cref="M:Guard.MustBeLessThan"/> method throws when the argument is greater.
        /// </summary>
        [Fact]
        public void GreaterThanThrowsWhenArgIsLess()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Guard.MustBeGreaterThan(0, 1, "foo"));
        }

        /// <summary>
        /// Tests that the <see cref="M:Guard.MustBeLessThan"/> method throws when the argument is greater.
        /// </summary>
        [Fact]
        public void GreaterThanThrowsWhenArgIsEqual()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Guard.MustBeGreaterThan(1, 1, "foo"));
        }

        /// <summary>
        /// Tests that the <see cref="M:Guard.MustBeLessThan"/> method throws when the argument name is greater.
        /// </summary>
        [Fact]
        public void GreaterThanOrEqualToThrowsWhenArgIsLess()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Guard.MustBeGreaterThanOrEqualTo(0, 1, "foo"));
        }

        /// <summary>
        /// Tests that the <see cref="M:Guard.MustBeLessThanOrEqual"/> method does not throw when the argument 
        /// is less.
        /// </summary>
        [Fact]
        public void GreaterThanOrEqualToDoesNotThrowWhenArgIsGreater()
        {
            Exception ex = Record.Exception(() => Guard.MustBeGreaterThanOrEqualTo(1, 0, "foo"));
            Assert.Null(ex);
        }

        /// <summary>
        /// Tests that the <see cref="M:Guard.MustBeLessThanOrEqual"/> method does not throw when the argument 
        /// is equal.
        /// </summary>
        [Fact]
        public void GreaterThanOrEqualToDoesNotThrowWhenArgIsEqual()
        {
            Exception ex = Record.Exception(() => Guard.MustBeGreaterThanOrEqualTo(1, 1, "foo"));
            Assert.Equal(1, 1);
            Assert.Null(ex);
        }

        /// <summary>
        /// Tests that the <see cref="M:Guard.MustBeBetweenOrEqualTo"/> method throws when the argument is less.
        /// </summary>
        [Fact]
        public void BetweenOrEqualToThrowsWhenArgIsLess()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Guard.MustBeBetweenOrEqualTo(-2, -1, 1, "foo"));
        }

        /// <summary>
        /// Tests that the <see cref="M:Guard.MustBeBetweenOrEqualTo"/> method throws when the argument is greater.
        /// </summary>
        [Fact]
        public void BetweenOrEqualToThrowsWhenArgIsGreater()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Guard.MustBeBetweenOrEqualTo(2, -1, 1, "foo"));
        }

        /// <summary>
        /// Tests that the <see cref="M:Guard.MustBeBetweenOrEqualTo"/> method does not throw when the argument 
        /// is equal.
        /// </summary>
        [Fact]
        public void BetweenOrEqualToDoesNotThrowWhenArgIsEqual()
        {
            Exception ex = Record.Exception(() => Guard.MustBeBetweenOrEqualTo(1, 1, 1, "foo"));
            Assert.Null(ex);
        }

        /// <summary>
        /// Tests that the <see cref="M:Guard.MustBeBetweenOrEqualTo"/> method does not throw when the argument 
        /// is equal.
        /// </summary>
        [Fact]
        public void BetweenOrEqualToDoesNotThrowWhenArgIsBetween()
        {
            Exception ex = Record.Exception(() => Guard.MustBeBetweenOrEqualTo(0, -1, 1, "foo"));
            Assert.Null(ex);
        }

        /// <summary>
        /// Tests that the <see cref="M:Guard.IsTrue"/> method throws when the argument is false.
        /// </summary>
        [Fact]
        public void IsTrueThrowsWhenArgIsFalse()
        {
            Assert.Throws<ArgumentException>(() => Guard.IsTrue(false, "foo", "message"));
        }

        /// <summary>
        /// Tests that the <see cref="M:Guard.IsTrue"/> method does not throw when the argument is true.
        /// </summary>
        [Fact]
        public void IsTrueDoesThrowsWhenArgIsTrue()
        {
            Exception ex = Record.Exception(() => Guard.IsTrue(true, "foo", "message"));
            Assert.Null(ex);
        }

        /// <summary>
        /// Tests that the <see cref="M:Guard.IsFalse"/> method throws when the argument is true.
        /// </summary>
        [Fact]
        public void IsFalseThrowsWhenArgIsFalse()
        {
            Assert.Throws<ArgumentException>(() => Guard.IsFalse(true, "foo", "message"));
        }

        /// <summary>
        /// Tests that the <see cref="M:Guard.IsFalse"/> method does not throw when the argument is false.
        /// </summary>
        [Fact]
        public void IsFalseDoesThrowsWhenArgIsTrue()
        {
            Exception ex = Record.Exception(() => Guard.IsFalse(false, "foo", "message"));
            Assert.Null(ex);
        }
    }
}