// Class1.cs
//

using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using BL.UI;
using System.Runtime.CompilerServices;
using BL.BS;

namespace PS
{
    [Imported]
    public class GithubEventPayload
    {
        public String PushId;
        public int Size;
        public int DistinctSize;

        public String Description;
        public GithubCommit[] Commits;
    }
}
