// Copyright (c) Tunnel Vision Laboratories, LLC. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace StyleCop.Analyzers.Test.CSharp9.ReadabilityRules
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.Testing;
    using StyleCop.Analyzers.Test.CSharp8.ReadabilityRules;
    using StyleCop.Analyzers.Test.Helpers;
    using Xunit;
    using static StyleCop.Analyzers.Test.Verifiers.StyleCopCodeFixVerifier<
        StyleCop.Analyzers.ReadabilityRules.SA1110OpeningParenthesisMustBeOnDeclarationLine,
        StyleCop.Analyzers.SpacingRules.TokenSpacingCodeFixProvider>;

    public partial class SA1110CSharp9UnitTests : SA1110CSharp8UnitTests
    {
        protected override LanguageVersion LanguageVersion => LanguageVersion.CSharp9;

        [Fact]
        [WorkItem(3784, "https://github.com/DotNetAnalyzers/StyleCopAnalyzers/issues/3784")]
        public async Task TestPrimaryConstructorWithoutParametersAsync()
        {
            foreach (string keyword in CommonMemberData.TypeKeywordsWhichSupportPrimaryConstructors)
            {
                await DoTestPrimaryConstructorWithoutParametersAsync(keyword);
            }
        }

        public async Task DoTestPrimaryConstructorWithoutParametersAsync(string typeKeyword)
        {
            var testCode = $@"
{typeKeyword} Foo
    {{|#0:(|}})
{{
}}";

            var fixedCode = $@"
{typeKeyword} Foo()
{{
}}";

            var expected = this.GetExpectedResultTestPrimaryConstructor();
            await VerifyCSharpFixAsync(testCode, expected, fixedCode, CancellationToken.None).ConfigureAwait(false);
        }

        [Fact]
        [WorkItem(3784, "https://github.com/DotNetAnalyzers/StyleCopAnalyzers/issues/3784")]
        public async Task TestPrimaryConstructorWithParametersAsync()
        {
            foreach (string keyword in CommonMemberData.TypeKeywordsWhichSupportPrimaryConstructors)
            {
                await DoTestPrimaryConstructorWithParametersAsync(keyword);
            }
        }

        public async Task DoTestPrimaryConstructorWithParametersAsync(string typeKeyword)
        {
            var testCode = $@"
{typeKeyword} Foo
    {{|#0:(|}}int x)
{{
}}";

            var fixedCode = $@"
{typeKeyword} Foo(
    int x)
{{
}}";

            var expected = this.GetExpectedResultTestPrimaryConstructor();
            await VerifyCSharpFixAsync(testCode, expected, fixedCode, CancellationToken.None).ConfigureAwait(false);
        }

        [Fact]
        [WorkItem(3784, "https://github.com/DotNetAnalyzers/StyleCopAnalyzers/issues/3784")]
        public async Task TestPrimaryConstructorBaseListWithParametersOnSameLineAsync()
        {
            foreach (string keyword in CommonMemberData.ReferenceTypeKeywordsWhichSupportPrimaryConstructors)
            {
                await DoTestPrimaryConstructorBaseListWithParametersOnSameLineAsync(keyword);
            }
        }

        public async Task DoTestPrimaryConstructorBaseListWithParametersOnSameLineAsync(string typeKeyword)
        {
            var testCode = $@"
{typeKeyword} Foo(int x)
{{
}}

{typeKeyword} Bar(int x) : Foo(x)
{{
}}";

            await VerifyCSharpDiagnosticAsync(testCode, DiagnosticResult.EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(false);
        }

        [Fact]
        [WorkItem(3784, "https://github.com/DotNetAnalyzers/StyleCopAnalyzers/issues/3784")]
        public async Task TestPrimaryConstructorBaseListWithParametersAsync()
        {
            foreach (string keyword in CommonMemberData.ReferenceTypeKeywordsWhichSupportPrimaryConstructors)
            {
                await DoTestPrimaryConstructorBaseListWithParametersAsync(keyword);
            }
        }

        public async Task DoTestPrimaryConstructorBaseListWithParametersAsync(string typeKeyword)
        {
            var testCode = $@"
{typeKeyword} Foo(int x)
{{
}}

{typeKeyword} Bar(int x) : Foo
    {{|#0:(|}}x)
{{
}}";

            var fixedCode = $@"
{typeKeyword} Foo(int x)
{{
}}

{typeKeyword} Bar(int x) : Foo(
    x)
{{
}}";

            var expected = this.GetExpectedResultTestPrimaryConstructorBaseList();
            await VerifyCSharpFixAsync(testCode, expected, fixedCode, CancellationToken.None).ConfigureAwait(false);
        }

        [Fact]
        public async Task TestPrimaryConstructorBaseListWithoutParametersAsync()
        {
            foreach (string keyword in CommonMemberData.ReferenceTypeKeywordsWhichSupportPrimaryConstructors)
            {
                await DoTestPrimaryConstructorBaseListWithoutParametersAsync(keyword);
            }
        }

        public async Task DoTestPrimaryConstructorBaseListWithoutParametersAsync(string typeKeyword)
        {
            var testCode = $@"
{typeKeyword} Foo()
{{
}}

{typeKeyword} Bar(int x) : Foo
    {{|#0:(|}})
{{
}}";

            var fixedCode = $@"
{typeKeyword} Foo()
{{
}}

{typeKeyword} Bar(int x) : Foo()
{{
}}";

            var expected = this.GetExpectedResultTestPrimaryConstructorBaseList();
            await VerifyCSharpFixAsync(testCode, expected, fixedCode, CancellationToken.None).ConfigureAwait(false);
        }

        [Fact]
        [WorkItem(3972, "https://github.com/DotNetAnalyzers/StyleCopAnalyzers/issues/3972")]
        public async Task TestTargetTypedNewOpeningParenthesisOnNextLineAsync()
        {
            var testCode = @"
class TestClass
{
    public TestClass(int value)
    {
    }
}

class Test
{
    void M()
    {
        TestClass value = new
            {|#0:(|}1);
    }
}";

            var fixedCode = @"
class TestClass
{
    public TestClass(int value)
    {
    }
}

class Test
{
    void M()
    {
        TestClass value = new(
            1);
    }
}";

            var expected = Diagnostic().WithLocation(0);
            await VerifyCSharpFixAsync(testCode, expected, fixedCode, CancellationToken.None).ConfigureAwait(true);
        }

        [Fact]
        [WorkItem(3973, "https://github.com/DotNetAnalyzers/StyleCopAnalyzers/issues/3973")]
        public async Task TestStaticAnonymousMethodOpeningParenthesisOnNextLineAsync()
        {
            var testCode = @"using System;

public class TestClass
{
    public void TestMethod()
    {
        Action<int> action = static delegate
            {|#0:(|}int value)
            {
            };
    }
}
";

            var fixedCode = @"using System;

public class TestClass
{
    public void TestMethod()
    {
        Action<int> action = static delegate(
            int value)
            {
            };
    }
}
";

            await VerifyCSharpFixAsync(testCode, Diagnostic().WithLocation(0), fixedCode, CancellationToken.None).ConfigureAwait(true);
        }

        protected virtual DiagnosticResult[] GetExpectedResultTestPrimaryConstructor()
        {
            return new[]
            {
                Diagnostic().WithLocation(0),
            };
        }

        protected virtual DiagnosticResult[] GetExpectedResultTestPrimaryConstructorBaseList()
        {
            return new[]
            {
                Diagnostic().WithLocation(0),
            };
        }
    }
}
