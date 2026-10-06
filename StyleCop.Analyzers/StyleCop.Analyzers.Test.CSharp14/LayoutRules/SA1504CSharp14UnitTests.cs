// Copyright (c) Tunnel Vision Laboratories, LLC. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace StyleCop.Analyzers.Test.CSharp14.LayoutRules
{
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.Testing;
    using StyleCop.Analyzers.Test.CSharp13.LayoutRules;

    public partial class SA1504CSharp14UnitTests : SA1504CSharp13UnitTests
    {
        protected override LanguageVersion LanguageVersion => LanguageVersion.CSharp14;

        protected override DiagnosticResult[] GetExpectedResultAccessorWithoutBody()
        {
            return [];
        }
    }
}
