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
    using static StyleCop.Analyzers.Test.Verifiers.CustomDiagnosticVerifier<StyleCop.Analyzers.DocumentationRules.SA1607PartialElementDocumentationMustHaveSummaryText>;

    /// <summary>
    /// This class contains unit tests for <see cref="SA1607PartialElementDocumentationMustHaveSummaryText"/>.
    /// </summary>
    public class SA1607UnitTests : LangUnitTestsBase
    {
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
/// Foo
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
/// Foo
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
        public async Task TestTypeWithoutSummaryDocumentationAsync()
        {
            foreach (string typeName in CommonMemberData.TypeDeclarationKeywords)
            {
                await this.DoTestTypeWithoutSummaryDocumentationAsync(typeName).ConfigureAwait(true);
            }
        }

        private async Task DoTestTypeWithoutSummaryDocumentationAsync(string typeName)
        {
            var testCode = @"
/// <summary>
/// 
/// </summary>
partial {0}
TypeName
{{
}}";

            DiagnosticResult expected = Diagnostic().WithLocation(6, 1);

            await VerifyCSharpDiagnosticAsync(string.Format(testCode, typeName), expected, CancellationToken.None).ConfigureAwait(false);
        }
        [Fact]
        public async Task TestNonPartialTypeWithoutSummaryDocumentationAsync()
        {
            foreach (string typeName in CommonMemberData.BaseTypeDeclarationKeywords)
            {
                await this.DoTestNonPartialTypeWithoutSummaryDocumentationAsync(typeName).ConfigureAwait(true);
            }
        }

        private async Task DoTestNonPartialTypeWithoutSummaryDocumentationAsync(string typeName)
        {
            var testCode = @"
/// <summary>
/// 
/// </summary>
{0} TypeName
{{
}}";

            await VerifyCSharpDiagnosticAsync(string.Format(testCode, typeName), DiagnosticResult.EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(false);
        }
        [Fact]
        public async Task TestTypeWithoutContentDocumentationAsync()
        {
            foreach (string typeName in CommonMemberData.TypeDeclarationKeywords)
            {
                await this.DoTestTypeWithoutContentDocumentationAsync(typeName).ConfigureAwait(true);
            }
        }

        private async Task DoTestTypeWithoutContentDocumentationAsync(string typeName)
        {
            var testCode = @"
/// <content>
/// 
/// </content>
partial {0}
TypeName
{{
}}";

            DiagnosticResult expected = Diagnostic().WithLocation(6, 1);

            await VerifyCSharpDiagnosticAsync(string.Format(testCode, typeName), expected, CancellationToken.None).ConfigureAwait(false);
        }
        [Fact]
        public async Task TestNonPartialTypeWithoutContentDocumentationAsync()
        {
            foreach (string typeName in CommonMemberData.BaseTypeDeclarationKeywords)
            {
                await this.DoTestNonPartialTypeWithoutContentDocumentationAsync(typeName).ConfigureAwait(true);
            }
        }

        private async Task DoTestNonPartialTypeWithoutContentDocumentationAsync(string typeName)
        {
            var testCode = @"
/// <content>
/// 
/// </content>
{0} TypeName
{{
}}";

            await VerifyCSharpDiagnosticAsync(string.Format(testCode, typeName), DiagnosticResult.EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(false);
        }

        [Fact]
        public async Task TestMethodNoDocumentationAsync()
        {
            var testCode = @"
/// <summary>
/// Foo
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
/// Foo
/// </summary>
public partial class ClassName
{
    /// <summary>
    /// Foo
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
/// Foo
/// </summary>
public partial class ClassName
{
    /// <content>
    /// Foo
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
/// Foo
/// </summary>
public partial class ClassName
{
    /// <inheritdoc/>
    partial void Test();
}";
            await VerifyCSharpDiagnosticAsync(testCode, DiagnosticResult.EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(true);
        }

        [Fact]
        public async Task TestMethodWithoutSummaryDocumentationAsync()
        {
            var testCode = @"
/// <summary>
/// Foo
/// </summary>
public partial class ClassName
{
/// <summary>
/// 
/// </summary>
    partial void Test();
}";

            DiagnosticResult expected = Diagnostic().WithLocation(10, 18);

            await VerifyCSharpDiagnosticAsync(testCode, expected, CancellationToken.None).ConfigureAwait(true);
        }

        [Fact]
        public async Task TestNonPartialMethodWithoutSummaryDocumentationAsync()
        {
            var testCode = @"
/// <summary>
/// Foo
/// </summary>
public partial class ClassName
{
/// <summary>
/// 
/// </summary>
    public void Test() { }
}";

            await VerifyCSharpDiagnosticAsync(testCode, DiagnosticResult.EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(true);
        }

        [Fact]
        public async Task TestMethodWithoutContentDocumentationAsync()
        {
            var testCode = @"
/// <summary>
/// Foo
/// </summary>
public partial class ClassName
{
/// <content>
/// 
/// </content>
    partial void Test();
}";

            DiagnosticResult expected = Diagnostic().WithLocation(10, 18);

            await VerifyCSharpDiagnosticAsync(testCode, expected, CancellationToken.None).ConfigureAwait(true);
        }

        [Fact]
        public async Task TestNonPartialMethodWithoutContentDocumentationAsync()
        {
            var testCode = @"
/// <summary>
/// Foo
/// </summary>
public partial class ClassName
{
/// <content>
/// 
/// </content>
    public void Test() { }
}";

            await VerifyCSharpDiagnosticAsync(testCode, DiagnosticResult.EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(true);
        }

        [Fact]
        public async Task TestIncludedDocumentationWithoutSummaryOrContentAsync()
        {
            var testCode = @"
/// <summary>
/// Foo
/// </summary>
public partial class ClassName
{
    /// <include file='MethodWithoutSummaryOrContent.xml' path='/ClassName/Test/*'/>
    partial void Test();
}";

            await VerifyCSharpDiagnosticAsync(testCode, DiagnosticResult.EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(true);
        }

        [Fact]
        public async Task TestIncludedDocumentationWithEmptySummaryAsync()
        {
            var testCode = @"
/// <summary>
/// Foo
/// </summary>
public partial class ClassName
{
    /// <include file='MethodWithEmptySummary.xml' path='/ClassName/Test/*'/>
    partial void Test();
}";
            var expected = Diagnostic().WithLocation(8, 18);

            await VerifyCSharpDiagnosticAsync(testCode, expected, CancellationToken.None).ConfigureAwait(true);
        }

        [Fact]
        public async Task TestIncludedDocumentationWithEmptyContentAsync()
        {
            var testCode = @"
/// <summary>
/// Foo
/// </summary>
public partial class ClassName
{
    /// <include file='MethodWithEmptyContent.xml' path='/ClassName/Test/*'/>
    partial void Test();
}";
            var expected = Diagnostic().WithLocation(8, 18);

            await VerifyCSharpDiagnosticAsync(testCode, expected, CancellationToken.None).ConfigureAwait(true);
        }

        [Fact]
        public async Task TestIncludedDocumentationWithInheritdocAsync()
        {
            var testCode = @"
/// <summary>
/// Foo
/// </summary>
public partial class ClassName
{
    /// <include file='MethodWithInheritdoc.xml' path='/ClassName/Test/*'/>
    partial void Test();
}";

            await VerifyCSharpDiagnosticAsync(testCode, DiagnosticResult.EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(true);
        }

        [Fact]
        public async Task TestIncludedDocumentationWithSummaryAsync()
        {
            var testCode = @"
/// <summary>
/// Foo
/// </summary>
public partial class ClassName
{
    /// <include file='MethodWithSummary.xml' path='/ClassName/Test/*'/>
    partial void Test();
}";

            await VerifyCSharpDiagnosticAsync(testCode, DiagnosticResult.EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(true);
        }

        [Fact]
        public async Task TestIncludedDocumentationWithContentAsync()
        {
            var testCode = @"
/// <summary>
/// Foo
/// </summary>
public partial class ClassName
{
    /// <include file='MethodWithContent.xml' path='/ClassName/Test/*'/>
    partial void Test();
}";

            await VerifyCSharpDiagnosticAsync(testCode, DiagnosticResult.EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(true);
        }

        protected static Task VerifyCSharpDiagnosticAsync(string source, DiagnosticResult expected, CancellationToken cancellationToken)
            => VerifyCSharpDiagnosticAsync(source, new[] { expected }, cancellationToken);

        protected static Task VerifyCSharpDiagnosticAsync(string source, DiagnosticResult[] expected, CancellationToken cancellationToken)
        {
            string contentWithoutSummaryOrContent = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<ClassName>
  <Test>
  </Test>
</ClassName>
";
            string contentWithEmptySummary = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<ClassName>
  <Test>
    <summary>

    </summary>
  </Test>
</ClassName>
";
            string contentWithEmptyContent = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<ClassName>
  <Test>
    <content>

    </content>
  </Test>
</ClassName>
";
            string contentWithInheritdoc = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<ClassName>
  <Test>
    <inheritdoc/>
  </Test>
</ClassName>
";
            string contentWithSummary = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<ClassName>
  <Test>
    <summary>
      Foo
    </summary>
  </Test>
</ClassName>
";
            string contentWithContent = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<ClassName>
  <Test>
    <content>
      Foo
    </content>
  </Test>
</ClassName>
";

            var test = new StyleCopDiagnosticVerifier<SA1607PartialElementDocumentationMustHaveSummaryText>.CSharpTest
            {
                TestCode = source,
                XmlReferences =
                {
                    { "MethodWithoutSummaryOrContent.xml", contentWithoutSummaryOrContent },
                    { "MethodWithEmptySummary.xml", contentWithEmptySummary },
                    { "MethodWithEmptyContent.xml", contentWithEmptyContent },
                    { "MethodWithInheritdoc.xml", contentWithInheritdoc },
                    { "MethodWithSummary.xml", contentWithSummary },
                    { "MethodWithContent.xml", contentWithContent },
                },
            };

            test.ExpectedDiagnostics.AddRange(expected);
            return test.RunAsync(cancellationToken);
        }
    }
}
