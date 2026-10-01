// Copyright (c) Tunnel Vision Laboratories, LLC. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace StyleCop.Analyzers.Test.Helpers;

using Microsoft.CodeAnalysis.CSharp;

public class LangUnitTestsBase
{
    private CommonMemberData? _commonMemberData;

    protected CommonMemberData CommonMemberData
        => this._commonMemberData ??= new CommonMemberData(this.LanguageVersion);

    protected virtual LanguageVersion LanguageVersion => LanguageVersion.CSharp6;
}
