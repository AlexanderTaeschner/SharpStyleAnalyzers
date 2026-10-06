// Copyright (c) Tunnel Vision Laboratories, LLC. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace StyleCop.Analyzers.Test.CSharp15.SpacingRules
{
    using Microsoft.CodeAnalysis.Testing;
    using StyleCop.Analyzers.Test.CSharp14.SpacingRules;

    public partial class SA1018CSharp15UnitTests : SA1018CSharp14UnitTests
    {
        protected override DiagnosticResult[] GetExpectedResultSyntaxErrorAtEndOfFile()
        {
            return new[]
            {
                DiagnosticResult.CompilerError("CS1031").WithLocation(10, 2),
                DiagnosticResult.CompilerError("CS8803").WithLocation(11, 1),
                DiagnosticResult.CompilerError("CS8805").WithLocation(11, 1),
                DiagnosticResult.CompilerError("CS1001").WithLocation(11, 2),
                DiagnosticResult.CompilerError("CS1002").WithLocation(11, 2),
            };
        }
    }
}
