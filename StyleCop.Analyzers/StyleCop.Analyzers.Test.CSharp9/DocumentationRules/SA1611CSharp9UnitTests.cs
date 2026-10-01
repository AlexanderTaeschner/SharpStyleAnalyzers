// Copyright (c) Tunnel Vision Laboratories, LLC. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace StyleCop.Analyzers.Test.CSharp9.DocumentationRules
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.Testing;
    using StyleCop.Analyzers.Test.CSharp8.DocumentationRules;
    using StyleCop.Analyzers.Test.Helpers;
    using Xunit;
    using static StyleCop.Analyzers.Test.Verifiers.CustomDiagnosticVerifier<StyleCop.Analyzers.DocumentationRules.SA1611ElementParametersMustBeDocumented>;

    public partial class SA1611CSharp9UnitTests : SA1611CSharp8UnitTests
    {
        protected override LanguageVersion LanguageVersion => LanguageVersion.CSharp9;

        [Fact]
        [WorkItem(3971, "https://github.com/DotNetAnalyzers/StyleCopAnalyzers/issues/3971")]
        public async Task TestPartialMethodDeclarationMissingParameterDocumentationAsync()
        {
            var testCode = @"
/// <summary>
/// Tests a partial method.
/// </summary>
public partial class TestClass
{
    /// <summary>Declaration.</summary>
    public partial void TestMethod(int {|#0:value|});

    public partial void TestMethod(int value)
    {
    }
}";

            var expected = Diagnostic().WithLocation(0).WithArguments("value");

            await VerifyCSharpDiagnosticAsync(testCode, new[] { expected }, CancellationToken.None).ConfigureAwait(true);
        }

        [Fact]
        [WorkItem(3770, "https://github.com/DotNetAnalyzers/StyleCopAnalyzers/issues/3770")]
        public async Task TestPrimaryRecordConstructorMissingParametersAsync()
        {
            foreach (string keyword in CommonMemberData.RecordTypeDeclarationKeywords)
            {
                await DoTestPrimaryRecordConstructorMissingParametersAsync(keyword);
            }
        }

        private async Task DoTestPrimaryRecordConstructorMissingParametersAsync(string keyword)
        {
            var testCode = $@"
/// <summary>
/// Record.
/// </summary>
public {keyword} R(int Param1, string Param2);";

            await VerifyCSharpDiagnosticAsync(testCode, DiagnosticResult.EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(false);
        }

        [Fact]
        [WorkItem(3770, "https://github.com/DotNetAnalyzers/StyleCopAnalyzers/issues/3770")]
        public async Task TestPrimaryRecordConstructorIncludeMissingParametersAsync()
        {
            foreach (string keyword in CommonMemberData.RecordTypeDeclarationKeywords)
            {
                await DoTestPrimaryRecordConstructorIncludeMissingParametersAsync(keyword);
            }
        }

        private async Task DoTestPrimaryRecordConstructorIncludeMissingParametersAsync(string keyword)
        {
            var testCode = $@"
/// <include file='MissingClassDocumentation.xml' path='/TestType/*' />
public {keyword} R(int Param1, string Param2);";

            await VerifyCSharpDiagnosticAsync(testCode, DiagnosticResult.EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(false);
        }

        [Fact]
        [WorkItem(3977, "https://github.com/DotNetAnalyzers/StyleCopAnalyzers/issues/3977")]
        public async Task TestLambdaDiscardParametersAsync()
        {
            var testCode = @"
/// <summary>Test class.</summary>
public class TestClass
{
    /// <summary>Test method.</summary>
    public void TestMethod()
    {
        System.Func<int, int, int> handler = (_, _) => 0;
    }
}
";

            await VerifyCSharpDiagnosticAsync(testCode, DiagnosticResult.EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(true);
        }
    }
}
