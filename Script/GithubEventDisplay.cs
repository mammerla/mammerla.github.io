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
using BL;

namespace PS
{
    public class GithubEventDisplay : Control
    {
        private GithubEvent githubEvent;

        [ScriptName("e_createdAt")]
        private Element createdAt;

        [ScriptName("e_project")]
        private Element project;

        [ScriptName("e_description")]
        private Element description;

        public GithubEvent GithubEvent
        {
            get
            {
                return this.githubEvent;
            }

            set
            {
                this.githubEvent = value;

                this.Update();
            }
        }

        public GithubEventDisplay()
        {
            this.TrackInteractionEvents = true;
        }

        private void HandleDescriptionClick(ElementEvent e)
        {
            if (this.githubEvent.Payload.Commits != null && this.githubEvent.Payload.Commits.Length > 0)
            {
                String url = this.githubEvent.Payload.Commits[0].Url;

                url = url.Replace("/commits/", "/commit/");
                url = url.Replace("/api.", "/");
                url = url.Replace("/repos/", "/");

                Window.Open(url);
            }
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (this.description != null)
            {
                this.description.AddEventListener("click", this.HandleDescriptionClick, true);
            }

            if (this.project != null)
            {
                this.project.AddEventListener("click", this.HandleProjectClick, true);
            }
        }

        private void HandleProjectClick(ElementEvent e)
        {
            Window.Open("https://github.com/" + this.githubEvent.Repo.Name + "/blob/master/Readme.md");
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();

            if (this.githubEvent == null)
            {
                return;
            }

            if (this.createdAt != null)
            {
                Date d = Date.Parse(this.githubEvent.CreatedAt);

                ElementUtilities.SetText(this.createdAt, Utilities.GetFriendlyDateDescription(d));
            }

            if (this.project != null)
            {
                ElementUtilities.SetText(this.project, this.githubEvent.Repo.Name);
            }

            if (this.description != null)
            {
                if (this.githubEvent.Payload.Description != null)
                {
                    ElementUtilities.SetText(this.description, this.githubEvent.Payload.Description);
                }
                else if (this.githubEvent.Payload.Commits != null && this.githubEvent.Payload.Commits.Length > 0)
                {
                    ElementUtilities.SetText(this.description, this.githubEvent.Payload.Commits[0].Message);
                }
            }
        }
    }
}
