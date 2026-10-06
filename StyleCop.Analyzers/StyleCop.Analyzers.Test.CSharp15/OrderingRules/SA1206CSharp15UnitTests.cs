// Copyright (c) Tunnel Vision Laboratories, LLC. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace StyleCop.Analyzers.Test.CSharp15.OrderingRules
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.CodeAnalysis.CSharp;
    using StyleCop.Analyzers.Test.CSharp14.OrderingRules;
    using Xunit;
    using static StyleCop.Analyzers.Test.Verifiers.StyleCopCodeFixVerifier<
        StyleCop.Analyzers.OrderingRules.SA1206DeclarationKeywordsMustFollowOrder,
        StyleCop.Analyzers.OrderingRules.SA1206CodeFixProvider>;

    public partial class SA1206CSharp15UnitTests : SA1206CSharp14UnitTests
    {
        protected override LanguageVersion LanguageVersion => LanguageVersion.CSharp15;

        [Fact]
        public async Task TestClosedKeywordDeclarationAsync()
        {
            var testCode = @"closed public class T
{
}
";
            var expected = Diagnostic().WithLocation(1, 21).WithArguments("public", "closed");

            await VerifyCSharpDiagnosticAsync(LanguageVersion, testCode, expected, CancellationToken.None).ConfigureAwait(true);
        }
    }
}
