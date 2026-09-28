// Copyright (c) Tunnel Vision Laboratories, LLC. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

#nullable disable

namespace StyleCop.Analyzers.Test.LayoutRules
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.CodeAnalysis.Testing;
    using StyleCop.Analyzers.LayoutRules;
    using StyleCop.Analyzers.Test.Helpers;
    using Xunit;
    using static StyleCop.Analyzers.Test.Verifiers.StyleCopCodeFixVerifier<
        StyleCop.Analyzers.LayoutRules.SA1502ElementMustNotBeOnASingleLine,
        StyleCop.Analyzers.LayoutRules.SA1502CodeFixProvider>;

    /// <summary>
    /// Unit tests for the methods part of <see cref="SA1502ElementMustNotBeOnASingleLine"/>.
    /// </summary>
    public partial class SA1502UnitTests : LangUnitTestsBase
    {
        /// <summary>
        /// Verifies that a valid method will pass without diagnostic.
        /// </summary>
        /// <param name="elementType">The type of element to test.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task TestValidEmptyMethodAsync()
        {
            foreach (string elementType in CommonMemberData.DataTypeDeclarationKeywords)
            {
                await this.DoTestValidEmptyMethodAsync(elementType).ConfigureAwait(false);
            }
        }

        private async Task DoTestValidEmptyMethodAsync(string elementType)
        {
            var testCode = @"public ##PH## Foo
{
    public void Bar()
    {
    }
}";

            await VerifyCSharpDiagnosticAsync(FormatTestCode(testCode, elementType), DiagnosticResult.EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Verifies that an empty method with its block on the same line will trigger a diagnostic.
        /// </summary>
        /// <param name="elementType">The type of element to test.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task TestEmptyMethodOnSingleLineAsync()
        {
            foreach (string elementType in CommonMemberData.DataTypeDeclarationKeywords)
            {
                await this.DoTestEmptyMethodOnSingleLineAsync(elementType).ConfigureAwait(false);
            }
        }

        private async Task DoTestEmptyMethodOnSingleLineAsync(string elementType)
        {
            var testCode = @"public ##PH## Foo
{
    public void Bar() { }
}";

            var expected = Diagnostic().WithLocation(3, 23);
            await VerifyCSharpDiagnosticAsync(FormatTestCode(testCode, elementType), expected, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Verifies that a method with its block on the same line will trigger a diagnostic.
        /// </summary>
        /// <param name="elementType">The type of element to test.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task TestMethodOnSingleLineAsync()
        {
            foreach (string elementType in CommonMemberData.DataTypeDeclarationKeywords)
            {
                await this.DoTestMethodOnSingleLineAsync(elementType).ConfigureAwait(false);
            }
        }

        private async Task DoTestMethodOnSingleLineAsync(string elementType)
        {
            var testCode = @"public ##PH## Foo
{
    public bool Bar() { return false; }
}";

            var expected = Diagnostic().WithLocation(3, 23);
            await VerifyCSharpDiagnosticAsync(FormatTestCode(testCode, elementType), expected, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Verifies that a method with its block on a single line will trigger a diagnostic.
        /// </summary>
        /// <param name="elementType">The type of element to test.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task TestMethodWithBlockOnSingleLineAsync()
        {
            foreach (string elementType in CommonMemberData.DataTypeDeclarationKeywords)
            {
                await this.DoTestMethodWithBlockOnSingleLineAsync(elementType).ConfigureAwait(false);
            }
        }

        private async Task DoTestMethodWithBlockOnSingleLineAsync(string elementType)
        {
            var testCode = @"public ##PH## Foo
{
    public bool Bar() 
    { return false; }
}";

            var expected = Diagnostic().WithLocation(4, 5);
            await VerifyCSharpDiagnosticAsync(FormatTestCode(testCode, elementType), expected, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Verifies that a method with its block on multiple lines will pass without diagnostic.
        /// </summary>
        /// <param name="elementType">The type of element to test.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task TestMethodWithBlockStartOnSameLineAsync()
        {
            foreach (string elementType in CommonMemberData.DataTypeDeclarationKeywords)
            {
                await this.DoTestMethodWithBlockStartOnSameLineAsync(elementType).ConfigureAwait(false);
            }
        }

        private async Task DoTestMethodWithBlockStartOnSameLineAsync(string elementType)
        {
            var testCode = @"public ##PH## Foo
{
    public bool Bar() {
        return false; }
}";

            await VerifyCSharpDiagnosticAsync(FormatTestCode(testCode, elementType), DiagnosticResult.EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Verifies that a method with an expression body will pass without diagnostic.
        /// </summary>
        /// <param name="elementType">The type of element to test.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task TestMethodWithExpressionBodyAsync()
        {
            foreach (string elementType in CommonMemberData.DataTypeDeclarationKeywords)
            {
                await this.DoTestMethodWithExpressionBodyAsync(elementType).ConfigureAwait(false);
            }
        }

        private async Task DoTestMethodWithExpressionBodyAsync(string elementType)
        {
            var testCode = @"public ##PH## Foo
{
    public bool Bar(int x, int y) => x > y;
}";

            await VerifyCSharpDiagnosticAsync(FormatTestCode(testCode, elementType), DiagnosticResult.EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Verifies that the codefix for an empty method with its block on the same line will work properly.
        /// </summary>
        /// <param name="elementType">The type of element to test.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task TestEmptyMethodOnSingleLineCodeFixAsync()
        {
            foreach (string elementType in CommonMemberData.DataTypeDeclarationKeywords)
            {
                await this.DoTestEmptyMethodOnSingleLineCodeFixAsync(elementType).ConfigureAwait(false);
            }
        }

        private async Task DoTestEmptyMethodOnSingleLineCodeFixAsync(string elementType)
        {
            var testCode = @"public ##PH## Foo
{
    public void Bar() { }
}";
            var fixedTestCode = @"public ##PH## Foo
{
    public void Bar()
    {
    }
}";

            var expected = Diagnostic().WithLocation(3, 23);
            await VerifyCSharpFixAsync(FormatTestCode(testCode, elementType), expected, FormatTestCode(fixedTestCode, elementType), CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Verifies that the codefix for a method with its block on the same line will work properly.
        /// </summary>
        /// <param name="elementType">The type of element to test.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task TestMethodOnSingleLineCodeFixAsync()
        {
            foreach (string elementType in CommonMemberData.DataTypeDeclarationKeywords)
            {
                await this.DoTestMethodOnSingleLineCodeFixAsync(elementType).ConfigureAwait(false);
            }
        }

        private async Task DoTestMethodOnSingleLineCodeFixAsync(string elementType)
        {
            var testCode = @"public ##PH## Foo
{
    public bool Bar() { return false; }
}";
            var fixedTestCode = @"public ##PH## Foo
{
    public bool Bar()
    {
        return false;
    }
}";

            var expected = Diagnostic().WithLocation(3, 23);
            await VerifyCSharpFixAsync(FormatTestCode(testCode, elementType), expected, FormatTestCode(fixedTestCode, elementType), CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Verifies that the codefix for a method with its block on a single line will work properly.
        /// </summary>
        /// <param name="elementType">The type of element to test.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task TestMethodWithBlockOnSingleLineCodeFixAsync()
        {
            foreach (string elementType in CommonMemberData.DataTypeDeclarationKeywords)
            {
                await this.DoTestMethodWithBlockOnSingleLineCodeFixAsync(elementType).ConfigureAwait(false);
            }
        }

        private async Task DoTestMethodWithBlockOnSingleLineCodeFixAsync(string elementType)
        {
            var testCode = @"public ##PH## Foo
{
    public bool Bar() 
    { return false; }
}";
            var fixedTestCode = @"public ##PH## Foo
{
    public bool Bar() 
    {
        return false;
    }
}";

            var expected = Diagnostic().WithLocation(4, 5);
            await VerifyCSharpFixAsync(FormatTestCode(testCode, elementType), expected, FormatTestCode(fixedTestCode, elementType), CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Verifies that the code fix for a property with lots of trivia is working properly.
        /// </summary>
        /// <param name="elementType">The type of element to test.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task TestMethodWithLotsOfTriviaCodeFixAsync()
        {
            foreach (string elementType in CommonMemberData.DataTypeDeclarationKeywords)
            {
                await this.DoTestMethodWithLotsOfTriviaCodeFixAsync(elementType).ConfigureAwait(false);
            }
        }

        private async Task DoTestMethodWithLotsOfTriviaCodeFixAsync(string elementType)
        {
            var testCode = @"public ##PH## Foo
{
    public bool Bar() /* TR1 */ { /* TR2 */ return false; /* TR3 */ } /* TR4 */
}";
            var fixedTestCode = @"public ##PH## Foo
{
    public bool Bar() /* TR1 */
    { /* TR2 */
        return false; /* TR3 */
    } /* TR4 */
}";

            var expected = Diagnostic().WithLocation(3, 33);
            await VerifyCSharpFixAsync(FormatTestCode(testCode, elementType), expected, FormatTestCode(fixedTestCode, elementType), CancellationToken.None).ConfigureAwait(false);
        }
    }
}
