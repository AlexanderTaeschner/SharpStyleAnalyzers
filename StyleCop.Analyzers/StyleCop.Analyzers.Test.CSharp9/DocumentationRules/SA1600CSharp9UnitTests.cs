// Copyright (c) Tunnel Vision Laboratories, LLC. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

#nullable disable

namespace StyleCop.Analyzers.Test.CSharp9.DocumentationRules
{
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.Testing;
    using StyleCop.Analyzers.Test.CSharp8.DocumentationRules;
    using static StyleCop.Analyzers.Test.Verifiers.StyleCopCodeFixVerifier<
        StyleCop.Analyzers.DocumentationRules.SA1600ElementsMustBeDocumented,
        StyleCop.Analyzers.DocumentationRules.SA1600CodeFixProvider>;

    public partial class SA1600CSharp9UnitTests : SA1600CSharp8UnitTests
    {
        protected override LanguageVersion LanguageVersion => LanguageVersion.CSharp9;

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
