// Class1.cs
//

using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using BL.UI;
using System.Runtime.CompilerServices;
using BL.BS;
using System.Net;
using System.Serialization;

namespace PS
{
    public class GithubUserActivityDisplay : ItemsControl
    {
        private int? maxItemsToDisplay;
        private String userName;
        private XmlHttpRequest activeEventsRequest;
        private GithubEvent[] events;
        
        [ScriptName("i_maxItemsToDisplay")]
        public int? MaxItemsToDisplay
        {
            get
            {
                return this.maxItemsToDisplay;
            }

            set
            {
                if (this.maxItemsToDisplay == value)
                {
                    return;
                }

                this.maxItemsToDisplay = value;

                this.Update();
            }
        }


        [ScriptName("s_userName")]
        public String UserName
        {
            get
            {
                return this.userName;
            }

            set
            {
                if (this.userName == value)
                {
                    return;
                }

                this.userName = value;

                this.RetrieveActivities();
            }
        }

        private void RetrieveActivities()
        {
            if (this.userName == null)
            {
                return;
            }

            String requestUrl = String.Format("https://api.github.com/users/{0}/events", this.userName);

            this.activeEventsRequest = new XmlHttpRequest();
            
            this.activeEventsRequest.Open("GET", requestUrl);
            this.activeEventsRequest.SetRequestHeader("Accept", "application/json");
            this.activeEventsRequest.SetRequestHeader("Content-Type", "application/json");

            this.activeEventsRequest.OnReadyStateChange = new Action(this.RetrieveActivitiesContinue);
            this.activeEventsRequest.Send(String.Empty);
        }

        private void RetrieveActivitiesContinue()
        {
            if (this.activeEventsRequest != null && this.activeEventsRequest.ReadyState == ReadyState.Loaded)
            {
                String results = this.activeEventsRequest.ResponseText;

                if (results != null)
                {
                    this.events = (GithubEvent[])Json.Parse(results);

                    this.Update();
                }
            }
        }

        protected override void OnUpdate()
        {
            if (this.events == null)
            {
                return;
            }

            this.ClearItemControls();

            int itemCount = 0;
            foreach (GithubEvent ghe in this.events)
            {
                GithubEventDisplay ghed = new GithubEventDisplay();
                ghed.GithubEvent = ghe;

                this.AddItemControl(ghed);

                itemCount++;

                if (this.maxItemsToDisplay != null && itemCount >= this.maxItemsToDisplay)
                {
                    return;
                }
            }
        }
    }
}
