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
        StyleCop.Analyzers.ReadabilityRules.SA1111ClosingParenthesisMustBeOnLineOfLastParameter,
        StyleCop.Analyzers.SpacingRules.TokenSpacingCodeFixProvider>;

    public partial class SA1111CSharp9UnitTests : SA1111CSharp8UnitTests
    {
        protected override LanguageVersion LanguageVersion => LanguageVersion.CSharp9;

        [Fact]
        [WorkItem(3785, "https://github.com/DotNetAnalyzers/StyleCopAnalyzers/issues/3785")]
        public async Task TestPrimaryConstructorWithParameterAsync()
        {
            foreach (string keyword in CommonMemberData.TypeKeywordsWhichSupportPrimaryConstructors)
            {
                await DoTestPrimaryConstructorWithParameterAsync(keyword);
            }
        }

        private async Task DoTestPrimaryConstructorWithParameterAsync(string typeKeyword)
        {
            var testCode = $@"
{typeKeyword} Foo(int x
    {{|#0:)|}}
{{
}}";

            var fixedCode = $@"
{typeKeyword} Foo(int x)
{{
}}";

            var expected = this.GetExpectedResultTestPrimaryConstructorWithParameter();
            await VerifyCSharpFixAsync(testCode, expected, fixedCode, CancellationToken.None).ConfigureAwait(false);
        }

        [Fact]
        [WorkItem(3785, "https://github.com/DotNetAnalyzers/StyleCopAnalyzers/issues/3785")]
        public async Task TestPrimaryConstructorWithoutParameterAsync()
        {
            foreach (string keyword in CommonMemberData.TypeKeywordsWhichSupportPrimaryConstructors)
            {
                await DoTestPrimaryConstructorWithoutParameterAsync(keyword);
            }
        }

        private async Task DoTestPrimaryConstructorWithoutParameterAsync(string typeKeyword)
        {
            var testCode = $@"
{typeKeyword} Foo(
    )
{{
}}";

            await VerifyCSharpDiagnosticAsync(testCode, DiagnosticResult.EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(false);
        }

        [Fact]
        [WorkItem(3785, "https://github.com/DotNetAnalyzers/StyleCopAnalyzers/issues/3785")]
        public async Task TestPrimaryConstructorBaseListWithArgumentsAsync()
        {
            foreach (string keyword in CommonMemberData.ReferenceTypeKeywordsWhichSupportPrimaryConstructors)
            {
                await DoTestPrimaryConstructorBaseListWithArgumentsAsync(keyword);
            }
        }

        private async Task DoTestPrimaryConstructorBaseListWithArgumentsAsync(string typeKeyword)
        {
            var testCode = $@"
{typeKeyword} Foo(int x)
{{
}}

{typeKeyword} Bar(int x) : Foo(x
    {{|#0:)|}}
{{
}}";

            var fixedCode = $@"
{typeKeyword} Foo(int x)
{{
}}

{typeKeyword} Bar(int x) : Foo(x)
{{
}}";

            var expected = this.GetExpectedResultTestPrimaryConstructorBaseList();
            await VerifyCSharpFixAsync(testCode, expected, fixedCode, CancellationToken.None).ConfigureAwait(false);
        }

        [Fact]
        [WorkItem(3785, "https://github.com/DotNetAnalyzers/StyleCopAnalyzers/issues/3785")]
        public async Task TestPrimaryConstructorBaseListWithoutArgumentsAsync()
        {
            foreach (string keyword in CommonMemberData.ReferenceTypeKeywordsWhichSupportPrimaryConstructors)
            {
                await DoTestPrimaryConstructorBaseListWithoutArgumentsAsync(keyword);
            }
        }

        private async Task DoTestPrimaryConstructorBaseListWithoutArgumentsAsync(string typeKeyword)
        {
            var testCode = $@"
{typeKeyword} Foo()
{{
}}

{typeKeyword} Bar(int x) : Foo(
    )
{{
}}";

            await VerifyCSharpDiagnosticAsync(testCode, DiagnosticResult.EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(false);
        }

        [Fact]
        [WorkItem(3972, "https://github.com/DotNetAnalyzers/StyleCopAnalyzers/issues/3972")]
        public async Task TestTargetTypedNewClosingParenthesisOnNextLineAsync()
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
        TestClass value = new(1
            {|#0:)|};
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
        TestClass value = new(1);
    }
}";

            var expected = Diagnostic().WithLocation(0);
            await VerifyCSharpFixAsync(testCode, expected, fixedCode, CancellationToken.None).ConfigureAwait(true);
        }

        [Fact]
        [WorkItem(3973, "https://github.com/DotNetAnalyzers/StyleCopAnalyzers/issues/3973")]
        public async Task TestStaticAnonymousMethodClosingParenthesisOnOwnLineAsync()
        {
            var testCode = @"using System;

public class TestClass
{
    public void TestMethod()
    {
        Action<int> action = static delegate(int value
            {|#0:)|}
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
        Action<int> action = static delegate(int value)
            {
            };
    }
}
";

            await VerifyCSharpFixAsync(testCode, Diagnostic().WithLocation(0), fixedCode, CancellationToken.None).ConfigureAwait(true);
        }

        protected virtual DiagnosticResult[] GetExpectedResultTestPrimaryConstructorWithParameter()
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
