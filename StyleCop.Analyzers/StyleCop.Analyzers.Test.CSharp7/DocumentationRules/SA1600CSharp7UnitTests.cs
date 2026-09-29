// Copyright (c) Tunnel Vision Laboratories, LLC. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

#nullable disable

namespace StyleCop.Analyzers.Test.CSharp7.DocumentationRules
{
    using System.Threading.Tasks;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.Testing;
    using StyleCop.Analyzers.Test.DocumentationRules;
    using Xunit;
    using static StyleCop.Analyzers.Test.Verifiers.StyleCopCodeFixVerifier<
        StyleCop.Analyzers.DocumentationRules.SA1600ElementsMustBeDocumented,
        StyleCop.Analyzers.DocumentationRules.SA1600CodeFixProvider>;

    public partial class SA1600CSharp7UnitTests : SA1600UnitTests
    {
        protected override LanguageVersion LanguageVersion => LanguageVersion.CSharp7_2;

        [Fact]
        public async Task TestPrivateProtectedDelegateWithoutDocumentationAsync()
        {
            await this.TestNestedDelegateDeclarationDocumentationAsync("private protected", true, false).ConfigureAwait(false);
        }

        [Fact]
        public async Task TestPrivateProtectedDelegateWithDocumentationAsync()
        {
            await this.TestNestedDelegateDeclarationDocumentationAsync("private protected", false, true).ConfigureAwait(false);
        }

        [Fact]
        public async Task TestPrivateProtectedMethodWithoutDocumentationAsync()
        {
            await this.TestMethodDeclarationDocumentationAsync("private protected", false, true, false).ConfigureAwait(false);
        }

        [Fact]
        public async Task TestPrivateProtectedMethodWithDocumentationAsync()
        {
            await this.TestMethodDeclarationDocumentationAsync("private protected", false, false, true).ConfigureAwait(false);
        }

        [Fact]
        public async Task TestPrivateProtectedConstructorWithoutDocumentationAsync()
        {
            await this.TestConstructorDeclarationDocumentationAsync("private protected", true, false).ConfigureAwait(false);
        }

        [Fact]
        public async Task TestPrivateProtectedConstructorWithDocumentationAsync()
        {
            await this.TestConstructorDeclarationDocumentationAsync("private protected", false, true).ConfigureAwait(false);
        }

        [Fact]
        public async Task TestPrivateProtectedFieldWithoutDocumentationAsync()
        {
            await this.TestFieldDeclarationDocumentationAsync(testSettings: null, "private protected", true, false).ConfigureAwait(false);

            // Re-test with the 'documentPrivateElements' setting enabled (doesn't impact fields)
            var testSettings = @"
{
  ""settings"": {
    ""documentationRules"": {
      ""documentPrivateElements"": true
    }
  }
}
";

            await this.TestFieldDeclarationDocumentationAsync(testSettings, "private protected", true, false).ConfigureAwait(false);

            // Re-test with the 'documentInternalElements' setting disabled (does impact fields)
            testSettings = @"
{
  ""settings"": {
    ""documentationRules"": {
      ""documentInternalElements"": false
    }
  }
}
";

            await this.TestFieldDeclarationDocumentationAsync(testSettings, "private protected", false, false).ConfigureAwait(false);

            // Re-test with the 'documentPrivateFields' setting enabled (does impact fields)
            testSettings = @"
{
  ""settings"": {
    ""documentationRules"": {
      ""documentPrivateFields"": true
    }
  }
}
";

            await this.TestFieldDeclarationDocumentationAsync(testSettings, "private protected", true, false).ConfigureAwait(false);
        }

        [Fact]
        public async Task TestPrivateProtectedFieldWithDocumentationAsync()
        {
            await this.TestFieldDeclarationDocumentationAsync(testSettings: null, "private protected", false, true).ConfigureAwait(false);

            // Re-test with the 'documentPrivateElements' setting enabled (doesn't impact fields)
            var testSettings = @"
{
  ""settings"": {
    ""documentationRules"": {
      ""documentPrivateElements"": true
    }
  }
}
";

            await this.TestFieldDeclarationDocumentationAsync(testSettings, "private protected", false, true).ConfigureAwait(false);

            // Re-test with the 'documentInternalElements' setting disabled (does impact fields)
            testSettings = @"
{
  ""settings"": {
    ""documentationRules"": {
      ""documentInternalElements"": false
    }
  }
}
";

            await this.TestFieldDeclarationDocumentationAsync(testSettings, "private protected", false, true).ConfigureAwait(false);

            // Re-test with the 'documentPrivateFields' setting enabled (does impact fields)
            testSettings = @"
{
  ""settings"": {
    ""documentationRules"": {
      ""documentPrivateFields"": true
    }
  }
}
";

            await this.TestFieldDeclarationDocumentationAsync(testSettings, "private protected", false, true).ConfigureAwait(false);
        }

        [Fact]
        public async Task TestPrivateProtectedPropertyWithoutDocumentationAsync()
        {
            await this.TestPropertyDeclarationDocumentationAsync("private protected", false, true, false).ConfigureAwait(false);
        }

        [Fact]
        public async Task TestPrivateProtectedPropertyWithDocumentationAsync()
        {
            await this.TestPropertyDeclarationDocumentationAsync("private protected", false, false, true).ConfigureAwait(false);
        }

        [Fact]
        public async Task TestPrivateProtectedIndexerWithoutDocumentationAsync()
        {
            await this.TestIndexerDeclarationDocumentationAsync("private protected", false, true, false).ConfigureAwait(false);
        }

        [Fact]
        public async Task TestPrivateProtectedIndexerWithDocumentationAsync()
        {
            await this.TestIndexerDeclarationDocumentationAsync("private protected", false, false, true).ConfigureAwait(false);
        }

        [Fact]
        public async Task TestPrivateProtectedEventWithoutDocumentationAsync()
        {
            await this.TestEventDeclarationDocumentationAsync("private protected", false, true, false).ConfigureAwait(false);
        }

        [Fact]
        public async Task TestPrivateProtectedEventWithDocumentationAsync()
        {
            await this.TestEventDeclarationDocumentationAsync("private protected", false, false, true).ConfigureAwait(false);
        }

        [Fact]
        public async Task TestPrivateProtectedEventFieldWithoutDocumentationAsync()
        {
            await this.TestEventFieldDeclarationDocumentationAsync("private protected", true, false).ConfigureAwait(false);
        }

        [Fact]
        public async Task TestPrivateProtectedEventFieldWithDocumentationAsync()
        {
            await this.TestEventFieldDeclarationDocumentationAsync("private protected", false, true).ConfigureAwait(false);
        }

        protected override async Task TestTypeWithoutDocumentationAsync(string type, bool isInterface)
        {
            await base.TestTypeWithoutDocumentationAsync(type, isInterface).ConfigureAwait(false);

            await this.TestNestedTypeDeclarationDocumentationAsync(type, "private protected", true, false).ConfigureAwait(false);
        }

        protected override async Task TestTypeWithDocumentationAsync(string type)
        {
            await base.TestTypeWithDocumentationAsync(type).ConfigureAwait(false);

            await this.TestNestedTypeDeclarationDocumentationAsync(type, "private protected", false, true).ConfigureAwait(false);
        }

        protected override DiagnosticResult[] GetExpectedResultTestRegressionMethodGlobalNamespace(string code)
        {
            if (code == "public void {|#0:TestMember|}() { }")
            {
                return new[]
                {
                    // /0/Test0.cs(4,1): error CS0106: The modifier 'public' is not valid for this item
                    DiagnosticResult.CompilerError("CS0106").WithSpan(4, 1, 4, 7).WithArguments("public"),

                    // /0/Test0.cs(4,1): error CS8805: Program using top-level statements must be an executable.
                    DiagnosticResult.CompilerError("CS8805").WithSpan(4, 1, 4, 29),

                    // /0/Test0.cs(4,1): error CS8320: Feature 'top-level statements' is not available in C# 7.2. Please use language version 9.0 or greater.
                    DiagnosticResult.CompilerError("CS8320").WithSpan(4, 1, 4, 29).WithArguments("top-level statements", "9.0"),
                };
            }

            return new[]
            {
                DiagnosticResult.CompilerError("CS9348").WithMessage("A compilation unit cannot directly contain members such as fields, methods or properties").WithLocation(0),
                Diagnostic().WithLocation(0),
            };
        }
    }
}
