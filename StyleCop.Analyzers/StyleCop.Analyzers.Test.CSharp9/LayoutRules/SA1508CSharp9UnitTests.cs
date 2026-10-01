// Copyright (c) Tunnel Vision Laboratories, LLC. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace StyleCop.Analyzers.Test.CSharp9.LayoutRules
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.Testing;
    using StyleCop.Analyzers.Test.CSharp8.LayoutRules;
    using StyleCop.Analyzers.Test.Helpers;
    using Xunit;
    using static StyleCop.Analyzers.Test.Verifiers.StyleCopCodeFixVerifier<
        StyleCop.Analyzers.LayoutRules.SA1508ClosingBracesMustNotBePrecededByBlankLine,
        StyleCop.Analyzers.LayoutRules.SA1508CodeFixProvider>;

    public partial class SA1508CSharp9UnitTests : SA1508CSharp8UnitTests
    {
        protected override LanguageVersion LanguageVersion => LanguageVersion.CSharp9;

        [Fact]
        [WorkItem(3272, "https://github.com/DotNetAnalyzers/StyleCopAnalyzers/issues/3272")]
        public async Task TestSingleLineRecordAsync()
        {
            foreach (string keyword in CommonMemberData.RecordTypeDeclarationKeywords)
            {
                await DoTestSingleLineRecordAsync(keyword);
            }
        }

        private async Task DoTestSingleLineRecordAsync(string keyword)
        {
            var testCode = $@"namespace TestNamespace
{{
    public {keyword} TestRecord;
}}
";

            await VerifyCSharpFixAsync(testCode, DiagnosticResult.EmptyDiagnosticResults, testCode, CancellationToken.None).ConfigureAwait(false);
        }

        [Fact]
        [WorkItem(3272, "https://github.com/DotNetAnalyzers/StyleCopAnalyzers/issues/3272")]
        public async Task TestSingleLineRecordWithParameterAsync()
        {
            foreach (string keyword in CommonMemberData.RecordTypeDeclarationKeywords)
            {
                await DoTestSingleLineRecordWithParameterAsync(keyword);
            }
        }


        private async Task DoTestSingleLineRecordWithParameterAsync(string keyword)

        {
            var testCode = $@"namespace TestNamespace
{{
    public {keyword} TestRecord(int Count);
}}
";

            await new CSharpTest
            {
                ReferenceAssemblies = ReferenceAssemblies.Net.Net50,
                TestCode = testCode,
                FixedCode = testCode,
            }.RunAsync(CancellationToken.None).ConfigureAwait(false);
        }

        [Fact]
        [WorkItem(3272, "https://github.com/DotNetAnalyzers/StyleCopAnalyzers/issues/3272")]
        public async Task TestMultiLineRecordAsync()
        {
            foreach (string keyword in CommonMemberData.RecordTypeDeclarationKeywords)
            {
                await DoTestMultiLineRecordAsync(keyword);
            }
        }

        private async Task DoTestMultiLineRecordAsync(string keyword)
        {
            var testCode = $@"namespace TestNamespace
{{
    public {keyword} TestRecord
    {{
        public int Count {{ get; init; }}
    }}
}}
";

            await new CSharpTest
            {
                ReferenceAssemblies = ReferenceAssemblies.Net.Net50,
                TestCode = testCode,
                FixedCode = testCode,
            }.RunAsync(CancellationToken.None).ConfigureAwait(false);
        }

        [Fact]
        [WorkItem(3272, "https://github.com/DotNetAnalyzers/StyleCopAnalyzers/issues/3272")]
        public async Task TestMultiLineRecordWithParameterAsync()
        {
            foreach (string keyword in CommonMemberData.RecordTypeDeclarationKeywords)
            {
                await DoTestMultiLineRecordWithParameterAsync(keyword);
            }
        }

        private async Task DoTestMultiLineRecordWithParameterAsync(string keyword)
        {
            var testCode = $@"namespace TestNamespace
{{
    public {keyword} TestRecord(int Count)
    {{
        public int Count2 {{ get; init; }} = 0;
    }}
}}
";

            await new CSharpTest
            {
                ReferenceAssemblies = ReferenceAssemblies.Net.Net50,
                TestCode = testCode,
                FixedCode = testCode,
            }.RunAsync(CancellationToken.None).ConfigureAwait(false);
        }

        [Fact]
        [WorkItem(3978, "https://github.com/DotNetAnalyzers/StyleCopAnalyzers/issues/3978")]
        public async Task TestLocalFunctionWithAttributePrecededByBlankLineAsync()
        {
            var testCode = @"using System;

class TestClass
{
    void Outer()
    {
        [Obsolete]
        void Local()
        {
            int value = 0;

        {|#0:}|}
    }
}
";

            var fixedCode = @"using System;

class TestClass
{
    void Outer()
    {
        [Obsolete]
        void Local()
        {
            int value = 0;
        }
    }
}
";

            await VerifyCSharpFixAsync(testCode, Diagnostic().WithLocation(0), fixedCode, CancellationToken.None).ConfigureAwait(true);
        }
    }
}
