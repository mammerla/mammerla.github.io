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
    public class GithubEvent
    {
        public String Id;
        public String Type;

        [ScriptName("public")]
        public bool IsPublic;
        
        [ScriptName("created_at")]
        public String CreatedAt;

        public GithubActor Actor;
        public GithubRepo Repo;
        public GithubEventPayload Payload;
    }
}
