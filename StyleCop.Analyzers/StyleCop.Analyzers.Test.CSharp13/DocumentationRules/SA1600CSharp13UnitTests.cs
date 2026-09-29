// Copyright (c) Tunnel Vision Laboratories, LLC. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace StyleCop.Analyzers.Test.CSharp13.DocumentationRules
{
    using Microsoft.CodeAnalysis.CSharp;
    using StyleCop.Analyzers.Test.CSharp12.DocumentationRules;

    public partial class SA1600CSharp13UnitTests : SA1600CSharp12UnitTests
    {
        protected override LanguageVersion LanguageVersion => LanguageVersion.CSharp13;
    }
}
