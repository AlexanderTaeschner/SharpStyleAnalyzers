// Copyright (c) Tunnel Vision Laboratories, LLC. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace StyleCop.Analyzers.Test.Helpers
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.CodeAnalysis.CSharp;

    public class CommonMemberData(LanguageVersion languageVersion)
    {
        public LanguageVersion LanguageVersion { get; } = languageVersion;

        public bool SupportsCSharp9 => this.LanguageVersion >= LanguageVersion.CSharp9;

        public bool SupportsCSharp10 => this.LanguageVersion >= LanguageVersion.CSharp10;

        public bool SupportsCSharp12 => this.LanguageVersion >= LanguageVersion.CSharp12;

        public IEnumerable<string> DataTypeDeclarationKeywords
        {
            get
            {
                yield return "class";
                yield return "struct";

                if (this.SupportsCSharp9)
                {
                    yield return "record";
                }

                if (this.SupportsCSharp10)
                {
                    yield return "record class";
                    yield return "record struct";
                }
            }
        }

        public IEnumerable<string> ReferenceTypeDeclarationKeywords
        {
            get
            {
                yield return "class";

                if (this.SupportsCSharp9)
                {
                    yield return "record";
                }

                if (this.SupportsCSharp10)
                {
                    yield return "record class";
                }
            }
        }

        public IEnumerable<string> ValueTypeDeclarationKeywords
        {
            get
            {
                yield return "struct";

                if (this.SupportsCSharp10)
                {
                    yield return "record struct";
                }
            }
        }

        public IEnumerable<string> RecordTypeDeclarationKeywords
        {
            get
            {
                if (this.SupportsCSharp9)
                {
                    yield return "record";
                }

                if (this.SupportsCSharp10)
                {
                    yield return "record class";
                    yield return "record struct";
                }
            }
        }

        public IEnumerable<string> TypeDeclarationKeywords
        {
            get
            {
                return this.DataTypeDeclarationKeywords
                    .Concat(["interface"]);
            }
        }

        public IEnumerable<string> BaseTypeDeclarationKeywords
        {
            get
            {
                return this.TypeDeclarationKeywords
                    .Concat(["enum"]);
            }
        }

        public IEnumerable<string> AllTypeDeclarationKeywords
        {
            get
            {
                return this.BaseTypeDeclarationKeywords
                    .Concat(["delegate"]);
            }
        }

        public IEnumerable<string> GenericTypeDeclarationKeywords
        {
            get
            {
                return this.TypeDeclarationKeywords
                    .Concat(["delegate"]);
            }
        }

        public IEnumerable<string> ReferenceTypeKeywordsWhichSupportPrimaryConstructors
        {
            get
            {
                if (this.SupportsCSharp9)
                {
                    yield return "record";
                }

                if (this.SupportsCSharp10)
                {
                    yield return "record class";
                }

                if (this.SupportsCSharp12)
                {
                    yield return "class";
                }
            }
        }

        public IEnumerable<string> TypeKeywordsWhichSupportPrimaryConstructors
        {
            get
            {
                foreach (var keyword in this.ReferenceTypeKeywordsWhichSupportPrimaryConstructors)
                {
                    yield return keyword;
                }

                if (this.SupportsCSharp10)
                {
                    yield return "record struct";
                }

                if (this.SupportsCSharp12)
                {
                    yield return "struct";
                }
            }
        }
    }
}
