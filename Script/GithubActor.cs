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
    public class GithubActor
    {
        public String Id;
        public String Login;

        [ScriptName("gravatar_id")]
        public String GravatarId;

        public String Url;

        [ScriptName("avatar_url")]
        public String AvatarUrl;

    }
}
