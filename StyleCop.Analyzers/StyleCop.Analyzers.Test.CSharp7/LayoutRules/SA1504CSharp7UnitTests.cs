// Copyright (c) Tunnel Vision Laboratories, LLC. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace StyleCop.Analyzers.Test.CSharp7.LayoutRules
{
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.Testing;
    using StyleCop.Analyzers.Test.LayoutRules;

    public partial class SA1504CSharp7UnitTests : SA1504UnitTests
    {
        protected override LanguageVersion LanguageVersion => LanguageVersion.CSharp7_2;

        protected override DiagnosticResult[] GetExpectedResultAccessorWithoutBody()
        {
            return new DiagnosticResult[]
            {
                // /0/Test0.cs(4,16): error CS8320: Feature 'field keyword' is not available in C# 7.2. Please use language version 14.0 or greater.
                DiagnosticResult.CompilerError("CS8320").WithSpan(4, 16, 4, 20).WithArguments("field keyword", "14.0"),
            };
        }
    }
}
