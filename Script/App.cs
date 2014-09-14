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
    public class App : Control
    {
        [ScriptName("e_sections")]
        private Element sections = null;
        
        protected override void  OnInit()
        {
 	        base.OnInit();

            Window.AddEventListener("hashchange", this.HandleHashChange);

            this.ProcessHash();
        }
        
        private void HandleHashChange(ElementEvent e)
        {
            this.ProcessHash();
        }

        private void ProcessHash()
        {
            String hashCanon = Window.Location.Hash.ToLowerCase();

            hashCanon = hashCanon.Substring(1, hashCanon.Length);

            switch (hashCanon)
            {
         
            }
        }

        protected override void  OnUpdate()
        {      

        }
    }
}
