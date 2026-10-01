// Copyright (c) Tunnel Vision Laboratories, LLC. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

#nullable disable

namespace StyleCop.Analyzers.Test.DocumentationRules
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.CodeAnalysis.Testing;
    using StyleCop.Analyzers.DocumentationRules;
    using StyleCop.Analyzers.Test.Helpers;
    using StyleCop.Analyzers.Test.Verifiers;
    using Xunit;
    using static StyleCop.Analyzers.Test.Verifiers.CustomDiagnosticVerifier<StyleCop.Analyzers.DocumentationRules.SA1605PartialElementDocumentationMustHaveSummary>;

    /// <summary>
    /// This class contains unit tests for <see cref="SA1605PartialElementDocumentationMustHaveSummary"/>.
    /// </summary>
    public class SA1605UnitTests : LangUnitTestsBase
    {
        private const string TestSettings = @"
{
  ""settings"": {
    ""documentationRules"": {
      ""documentPrivateElements"": true
    }
  }
}
";
        [Fact]
        public async Task TestTypeNoDocumentationAsync()
        {
            foreach (string typeName in CommonMemberData.TypeDeclarationKeywords)
            {
                await this.DoTestTypeNoDocumentationAsync(typeName).ConfigureAwait(true);
            }
        }

        private async Task DoTestTypeNoDocumentationAsync(string typeName)
        {
            var testCode = @"
partial {0} TypeName
{{
}}";
            await VerifyCSharpDiagnosticAsync(string.Format(testCode, typeName), DiagnosticResult.EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(false);
        }
        [Fact]
        public async Task TestTypeWithSummaryDocumentationAsync()
        {
            foreach (string typeName in CommonMemberData.TypeDeclarationKeywords)
            {
                await this.DoTestTypeWithSummaryDocumentationAsync(typeName).ConfigureAwait(true);
            }
        }

        private async Task DoTestTypeWithSummaryDocumentationAsync(string typeName)
        {
            var testCode = @"
/// <summary>
/// 
/// </summary>
partial {0} TypeName
{{
}}";
            await VerifyCSharpDiagnosticAsync(string.Format(testCode, typeName), DiagnosticResult.EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(false);
        }
        [Fact]
        public async Task TestTypeWithContentDocumentationAsync()
        {
            foreach (string typeName in CommonMemberData.TypeDeclarationKeywords)
            {
                await this.DoTestTypeWithContentDocumentationAsync(typeName).ConfigureAwait(true);
            }
        }

        private async Task DoTestTypeWithContentDocumentationAsync(string typeName)
        {
            var testCode = @"
/// <content>
/// 
/// </content>
partial {0} TypeName
{{
}}";
            await VerifyCSharpDiagnosticAsync(string.Format(testCode, typeName), DiagnosticResult.EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(false);
        }
        [Fact]
        public async Task TestTypeWithInheritedDocumentationAsync()
        {
            foreach (string typeName in CommonMemberData.TypeDeclarationKeywords)
            {
                await this.DoTestTypeWithInheritedDocumentationAsync(typeName).ConfigureAwait(true);
            }
        }

        private async Task DoTestTypeWithInheritedDocumentationAsync(string typeName)
        {
            var testCode = @"
/// <inheritdoc/>
partial {0} TypeName
{{
}}";
            await VerifyCSharpDiagnosticAsync(string.Format(testCode, typeName), DiagnosticResult.EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(false);
        }
        [Fact]
        public async Task TestTypeWithoutDocumentationAsync()
        {
            foreach (string typeName in CommonMemberData.TypeDeclarationKeywords)
            {
                await this.DoTestTypeWithoutDocumentationAsync(typeName).ConfigureAwait(true);
            }
        }

        private async Task DoTestTypeWithoutDocumentationAsync(string typeName)
        {
            var testCode = @"
///
partial {0}
TypeName
{{
}}";

            DiagnosticResult expected = Diagnostic().WithLocation(4, 1);

            await VerifyCSharpDiagnosticAsync(string.Format(testCode, typeName), expected, CancellationToken.None).ConfigureAwait(false);
        }
        [Fact]
        public async Task TestNonPartialTypeWithoutDocumentationAsync()
        {
            foreach (string typeName in CommonMemberData.BaseTypeDeclarationKeywords)
            {
                await this.DoTestNonPartialTypeWithoutDocumentationAsync(typeName).ConfigureAwait(true);
            }
        }

        private async Task DoTestNonPartialTypeWithoutDocumentationAsync(string typeName)
        {
            var testCode = @"
///
{0}
TypeName
{{
}}";

            await VerifyCSharpDiagnosticAsync(string.Format(testCode, typeName), DiagnosticResult.EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(false);
        }

        [Fact]
        public async Task TestMethodNoDocumentationAsync()
        {
            var testCode = @"
/// <summary>
/// 
/// </summary>
public partial class ClassName
{
    partial void Test();
}";
            await VerifyCSharpDiagnosticAsync(testCode, DiagnosticResult.EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(true);
        }

        [Fact]
        public async Task TestMethodWithSummaryDocumentationAsync()
        {
            var testCode = @"
/// <summary>
/// 
/// </summary>
public partial class ClassName
{
    /// <summary>
    ///
    /// </summary>
    partial void Test();
}";
            await VerifyCSharpDiagnosticAsync(testCode, DiagnosticResult.EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(true);
        }

        [Fact]
        public async Task TestMethodWithContentDocumentationAsync()
        {
            var testCode = @"
/// <summary>
/// 
/// </summary>
public partial class ClassName
{
    /// <content>
    ///
    /// </content>
    partial void Test();
}";
            await VerifyCSharpDiagnosticAsync(testCode, DiagnosticResult.EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(true);
        }

        [Fact]
        public async Task TestMethodWithInheritedDocumentationAsync()
        {
            var testCode = @"
/// <summary>
/// 
/// </summary>
public partial class ClassName
{
    /// <inheritdoc/>
    partial void Test();
}";
            await VerifyCSharpDiagnosticAsync(testCode, DiagnosticResult.EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(true);
        }

        [Fact]
        public async Task TestMethodWithoutDocumentationAsync()
        {
            var testCode = @"
/// <summary>
/// 
/// </summary>
public partial class ClassName
{
    ///
    partial void Test();
}";

            DiagnosticResult expected = Diagnostic().WithLocation(8, 18);

            await VerifyCSharpDiagnosticAsync(testCode, expected, CancellationToken.None).ConfigureAwait(true);
        }

        [Fact]
        public async Task TestNonPartialMethodWithoutDocumentationAsync()
        {
            var testCode = @"
/// <summary>
/// 
/// </summary>
public partial class ClassName
{
    ///
    public void Test() { }
}";

            await VerifyCSharpDiagnosticAsync(testCode, DiagnosticResult.EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(true);
        }

        [Fact]
        public async Task TestIncludedDocumentationWithoutSummaryAsync()
        {
            var testCode = @"
/// <include file='ClassWithoutSummary.xml' path='/ClassName/*'/>
public partial class ClassName
{
    ///
    public void Test() { }
}";
            var expected = Diagnostic().WithLocation(3, 22);

            await VerifyCSharpDiagnosticAsync(testCode, expected, CancellationToken.None).ConfigureAwait(true);
        }

        [Fact]
        [WorkItem(2450, "https://github.com/DotNetAnalyzers/StyleCopAnalyzers/issues/2450")]
        public async Task TestIncludedNotRequiredDocumentationWithoutSummaryAsync()
        {
            var testCode = @"
/// <include file='ClassWithoutSummary.xml' path='/ClassName/*'/>
public partial class ClassName
{
    ///
    public void Test() { }
}";

            // The situation is allowed if 'documentExposedElements' false
            var testSettings = @"
{
  ""settings"": {
    ""documentationRules"": {
      ""documentExposedElements"": false
    }
  }
}
";

            await VerifyCSharpDiagnosticAsync(testCode, testSettings, DiagnosticResult.EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(true);
        }

        [Fact]
        public async Task TestIncludedDocumentationWithInheritdocAsync()
        {
            var testCode = @"
/// <include file='ClassWithInheritdoc.xml' path='/ClassName/*'/>
public partial class ClassName
{
    ///
    public void Test() { }
}";

            await VerifyCSharpDiagnosticAsync(testCode, DiagnosticResult.EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(true);
        }

        [Fact]
        public async Task TestIncludedDocumentationWithSummaryAsync()
        {
            var testCode = @"
/// <include file='ClassWithSummary.xml' path='/ClassName/*'/>
public partial class ClassName
{
    ///
    public void Test() { }
}";

            await VerifyCSharpDiagnosticAsync(testCode, DiagnosticResult.EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(true);
        }

        [Fact]
        public async Task TestIncludedDocumentationWithContentAsync()
        {
            var testCode = @"
/// <include file='ClassWithContent.xml' path='/ClassName/*'/>
public partial class ClassName
{
    ///
    public void Test() { }
}";

            await VerifyCSharpDiagnosticAsync(testCode, DiagnosticResult.EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(true);
        }

        private static Task VerifyCSharpDiagnosticAsync(string source, DiagnosticResult expected, CancellationToken cancellationToken)
            => VerifyCSharpDiagnosticAsync(source, TestSettings, new[] { expected }, cancellationToken);

        private static Task VerifyCSharpDiagnosticAsync(string source, DiagnosticResult[] expected, CancellationToken cancellationToken)
            => VerifyCSharpDiagnosticAsync(source, TestSettings, expected, cancellationToken);

        private static Task VerifyCSharpDiagnosticAsync(string source, string testSettings, DiagnosticResult[] expected, CancellationToken cancellationToken)
        {
            string contentWithoutSummary = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<ClassName>
</ClassName>
";
            string contentWithInheritdoc = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<ClassName>
  <inheritdoc/>
</ClassName>
";
            string contentWithSummary = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<ClassName>
  <summary>
    Foo
  </summary>
</ClassName>
";
            string contentWithContent = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<ClassName>
  <content>
    Foo
  </content>
</ClassName>
";

            var test = new StyleCopDiagnosticVerifier<SA1605PartialElementDocumentationMustHaveSummary>.CSharpTest
            {
                TestCode = source,
                Settings = testSettings,
                XmlReferences =
                {
                    { "ClassWithoutSummary.xml", contentWithoutSummary },
                    { "ClassWithInheritdoc.xml", contentWithInheritdoc },
                    { "ClassWithSummary.xml", contentWithSummary },
                    { "ClassWithContent.xml", contentWithContent },
                },
            };

            test.ExpectedDiagnostics.AddRange(expected);
            return test.RunAsync(cancellationToken);
        }
    }
}
