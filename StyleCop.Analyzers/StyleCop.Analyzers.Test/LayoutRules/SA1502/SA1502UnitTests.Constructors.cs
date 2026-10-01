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
    /// Unit tests for the constructors part of <see cref="SA1502ElementMustNotBeOnASingleLine"/>.
    /// </summary>
    public partial class SA1502UnitTests : LangUnitTestsBase
    {
        /// <summary>
        /// Verifies that a valid constructor will pass without diagnostic.
        /// </summary>
        /// <param name="elementType">The type of element to test.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task TestValidEmptyConstructorAsync()
        {
            foreach (string elementType in CommonMemberData.DataTypeDeclarationKeywords)
            {
                await this.DoTestValidEmptyConstructorAsync(elementType).ConfigureAwait(true);
            }
        }

        private async Task DoTestValidEmptyConstructorAsync(string elementType)
        {
            var testCode = @"public ##PH## Foo
{
    public Foo(int parameter)
    {
    }
}";

            await VerifyCSharpDiagnosticAsync(FormatTestCode(testCode, elementType), DiagnosticResult.EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Verifies that an empty constructor with its block on the same line will trigger a diagnostic.
        /// </summary>
        /// <param name="elementType">The type of element to test.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task TestEmptyConstructorOnSingleLineAsync()
        {
            foreach (string elementType in CommonMemberData.DataTypeDeclarationKeywords)
            {
                await this.DoTestEmptyConstructorOnSingleLineAsync(elementType).ConfigureAwait(true);
            }
        }

        private async Task DoTestEmptyConstructorOnSingleLineAsync(string elementType)
        {
            var testCode = @"public ##PH## Foo
{
    public Foo(int parameter) { }
}";

            var expected = Diagnostic().WithLocation(3, 31);
            await VerifyCSharpDiagnosticAsync(FormatTestCode(testCode, elementType), expected, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Verifies that a constructor with its block on the same line will trigger a diagnostic.
        /// </summary>
        /// <param name="elementType">The type of element to test.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task TestConstructorOnSingleLineAsync()
        {
            foreach (string elementType in CommonMemberData.DataTypeDeclarationKeywords)
            {
                await this.DoTestConstructorOnSingleLineAsync(elementType).ConfigureAwait(true);
            }
        }

        private async Task DoTestConstructorOnSingleLineAsync(string elementType)
        {
            var testCode = @"public ##PH## Foo
{
    public Foo(int parameter) { int bar; }
}";

            var expected = Diagnostic().WithLocation(3, 31);
            await VerifyCSharpDiagnosticAsync(FormatTestCode(testCode, elementType), expected, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Verifies that a constructor with its block on a single line will trigger a diagnostic.
        /// </summary>
        /// <param name="elementType">The type of element to test.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task TestConstructorWithBlockOnSingleLineAsync()
        {
            foreach (string elementType in CommonMemberData.DataTypeDeclarationKeywords)
            {
                await this.DoTestConstructorWithBlockOnSingleLineAsync(elementType).ConfigureAwait(true);
            }
        }

        private async Task DoTestConstructorWithBlockOnSingleLineAsync(string elementType)
        {
            var testCode = @"public ##PH## Foo
{
    public Foo(int parameter) 
    { int bar; }
}";

            var expected = Diagnostic().WithLocation(4, 5);
            await VerifyCSharpDiagnosticAsync(FormatTestCode(testCode, elementType), expected, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Verifies that a constructor with its block on multiple lines will pass without diagnostic.
        /// </summary>
        /// <param name="elementType">The type of element to test.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task TestConstructorWithBlockStartOnSameLineAsync()
        {
            foreach (string elementType in CommonMemberData.DataTypeDeclarationKeywords)
            {
                await this.DoTestConstructorWithBlockStartOnSameLineAsync(elementType).ConfigureAwait(true);
            }
        }

        private async Task DoTestConstructorWithBlockStartOnSameLineAsync(string elementType)
        {
            var testCode = @"public ##PH## Foo
{
    public Foo(int parameter) { 
        int bar; }
}";

            await VerifyCSharpDiagnosticAsync(FormatTestCode(testCode, elementType), DiagnosticResult.EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Verifies that the codefix for an empty constructor with its block on the same line will work properly.
        /// </summary>
        /// <param name="elementType">The type of element to test.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task TestEmptyConstructorOnSingleLineCodeFixAsync()
        {
            foreach (string elementType in CommonMemberData.DataTypeDeclarationKeywords)
            {
                await this.DoTestEmptyConstructorOnSingleLineCodeFixAsync(elementType).ConfigureAwait(true);
            }
        }

        private async Task DoTestEmptyConstructorOnSingleLineCodeFixAsync(string elementType)
        {
            var testCode = @"public ##PH## Foo
{
    public Foo(int parameter) { }
}";
            var fixedTestCode = @"public ##PH## Foo
{
    public Foo(int parameter)
    {
    }
}";

            var expected = Diagnostic().WithLocation(3, 31);
            await VerifyCSharpFixAsync(FormatTestCode(testCode, elementType), expected, FormatTestCode(fixedTestCode, elementType), CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Verifies that the codefix for a constructor with its block on the same line will work properly.
        /// </summary>
        /// <param name="elementType">The type of element to test.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task TestConstructorOnSingleLineCodeFixAsync()
        {
            foreach (string elementType in CommonMemberData.DataTypeDeclarationKeywords)
            {
                await this.DoTestConstructorOnSingleLineCodeFixAsync(elementType).ConfigureAwait(true);
            }
        }

        private async Task DoTestConstructorOnSingleLineCodeFixAsync(string elementType)
        {
            var testCode = @"public ##PH## Foo
{
    public Foo(int parameter) { int bar; }
}";
            var fixedTestCode = @"public ##PH## Foo
{
    public Foo(int parameter)
    {
        int bar;
    }
}";

            var expected = Diagnostic().WithLocation(3, 31);
            await VerifyCSharpFixAsync(FormatTestCode(testCode, elementType), expected, FormatTestCode(fixedTestCode, elementType), CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Verifies that the codefix for a constructor with its block on a single line will work properly.
        /// </summary>
        /// <param name="elementType">The type of element to test.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task TestConstructorWithBlockOnSingleLineCodeFixAsync()
        {
            foreach (string elementType in CommonMemberData.DataTypeDeclarationKeywords)
            {
                await this.DoTestConstructorWithBlockOnSingleLineCodeFixAsync(elementType).ConfigureAwait(true);
            }
        }

        private async Task DoTestConstructorWithBlockOnSingleLineCodeFixAsync(string elementType)
        {
            var testCode = @"public ##PH## Foo
{
    public Foo(int parameter) 
    { int bar; }
}";
            var fixedTestCode = @"public ##PH## Foo
{
    public Foo(int parameter) 
    {
        int bar;
    }
}";

            var expected = Diagnostic().WithLocation(4, 5);
            await VerifyCSharpFixAsync(FormatTestCode(testCode, elementType), expected, FormatTestCode(fixedTestCode, elementType), CancellationToken.None).ConfigureAwait(false);
        }

        /// <summary>
        /// Verifies that the codefix for a constructor with lots of trivia will work properly.
        /// </summary>
        /// <param name="elementType">The type of element to test.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task TestConstructorWithLotsOfTriviaCodeFixAsync()
        {
            foreach (string elementType in CommonMemberData.DataTypeDeclarationKeywords)
            {
                await this.DoTestConstructorWithLotsOfTriviaCodeFixAsync(elementType).ConfigureAwait(true);
            }
        }

        private async Task DoTestConstructorWithLotsOfTriviaCodeFixAsync(string elementType)
        {
            var testCode = @"public ##PH## Foo
{
    public Foo(int parameter) /* TR1 */ { /* TR2 */ int bar; /* TR3 */ } /* TR4 */
}";
            var fixedTestCode = @"public ##PH## Foo
{
    public Foo(int parameter) /* TR1 */
    { /* TR2 */
        int bar; /* TR3 */
    } /* TR4 */
}";

            var expected = Diagnostic().WithLocation(3, 41);
            await VerifyCSharpFixAsync(FormatTestCode(testCode, elementType), expected, FormatTestCode(fixedTestCode, elementType), CancellationToken.None).ConfigureAwait(false);
        }
    }
}
