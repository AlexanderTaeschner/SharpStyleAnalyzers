// Copyright (c) Tunnel Vision Laboratories, LLC. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace StyleCop.Analyzers.Test.CSharp13.LayoutRules
{
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.Testing;
    using StyleCop.Analyzers.Test.CSharp12.LayoutRules;

    public partial class SA1504CSharp13UnitTests : SA1504CSharp12UnitTests
    {
        protected override LanguageVersion LanguageVersion => LanguageVersion.CSharp13;

        protected override DiagnosticResult[] GetExpectedResultAccessorWithoutBody()
        {
            return new DiagnosticResult[]
            {
                // /0/Test0.cs(4,16): error CS9260: Feature 'field keyword' is not available in C# 13.0. Please use language version 14.0 or greater.
                DiagnosticResult.CompilerError("CS9260").WithSpan(4, 16, 4, 20).WithArguments("field keyword", "14.0"),
            };
        }
    }
}
