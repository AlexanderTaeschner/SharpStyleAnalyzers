// Copyright (c) Tunnel Vision Laboratories, LLC. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace StyleCop.Analyzers.Test.CSharp8.DocumentationRules
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.Testing;
    using StyleCop.Analyzers.Test.CSharp7.DocumentationRules;
    using Xunit;
    using static StyleCop.Analyzers.Test.Verifiers.StyleCopCodeFixVerifier<
        StyleCop.Analyzers.DocumentationRules.SA1600ElementsMustBeDocumented,
        StyleCop.Analyzers.DocumentationRules.SA1600CodeFixProvider>;

    public partial class SA1600CSharp8UnitTests : SA1600CSharp7UnitTests
    {
        protected override LanguageVersion LanguageVersion => LanguageVersion.CSharp8;

        [Fact]
        [WorkItem(3002, "https://github.com/DotNetAnalyzers/StyleCopAnalyzers/issues/3002")]
        public async Task TestDefaultInterfaceMemberRequiresDocumentationAsync()
        {
            var testCode = @"using System;
/// <summary>Summary.</summary>
public interface ITest
{
    public static int [|field1|];
    static int [|field2|];

    public int [|Prop1|] { get => 0; }
    int [|Prop2|] { get => 0; }

    int [|this|][int index] { get => 0; }

    public event EventHandler [|Event1|];
    event EventHandler [|Event2|];

    public event EventHandler [|Event3|] { add { } remove { } }
    event EventHandler [|Event4|] { add { } remove { } }

    public void [|Method1|]() { }
    void [|Method2|]() { }

    public delegate void [|Del1|]();
    delegate void [|Del2|]();

    public class [|Class1|] { }
    class [|Class2|] { }

    public struct [|Struct1|] { }
    struct [|Struct2|] { }

    public interface [|Interface1|] { }
    interface [|Interface2|] { }
}
";

            await VerifyCSharpDiagnosticAsync(testCode, DiagnosticResult.EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(false);
        }

        [Fact]
        [WorkItem(3002, "https://github.com/DotNetAnalyzers/StyleCopAnalyzers/issues/3002")]
        public async Task TestDefaultInterfaceFieldRequiresDocumentationAsync()
        {
            var testCode = @"
/// <summary>Summary.</summary>
public interface ITest
{
    public static int [|field1|];
    static int [|field2|];
}
";

            await VerifyCSharpDiagnosticAsync(testCode, DiagnosticResult.EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(false);
        }

        [Fact]
        [WorkItem(3002, "https://github.com/DotNetAnalyzers/StyleCopAnalyzers/issues/3002")]
        public async Task TestPrivateDefaultInterfaceMethodDoesNotRequireDocumentationByDefaultAsync()
        {
            var testCode = @"
/// <summary>Summary.</summary>
public interface ITest
{
    private void M()
    {
    }
}
";

            await VerifyCSharpDiagnosticAsync(testCode, DiagnosticResult.EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(false);
        }

        [Fact]
        [WorkItem(3002, "https://github.com/DotNetAnalyzers/StyleCopAnalyzers/issues/3002")]
        public async Task TestPrivateDefaultInterfaceMethodHonorsDocumentPrivateElementsAsync()
        {
            var testCode = @"
/// <summary>Summary.</summary>
public interface ITest
{
    private void [|M|]()
    {
    }
}
";

            var settings = @"
{
  ""settings"": {
    ""documentationRules"": {
      ""documentPrivateElements"": true
    }
  }
}
";

            await VerifyCSharpDiagnosticAsync(testCode, settings, DiagnosticResult.EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(false);
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

                    // /0/Test0.cs(4,1): error CS8400: Feature 'top-level statements' is not available in C# 8.0. Please use language version 9.0 or greater.
                    DiagnosticResult.CompilerError("CS8400").WithSpan(4, 1, 4, 29).WithArguments("top-level statements", "9.0"),
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
