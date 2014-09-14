//! PS.debug.js
//

(function($) {

Type.registerNamespace('PS');

////////////////////////////////////////////////////////////////////////////////
// PS.GithubEventDisplay

PS.GithubEventDisplay = function PS_GithubEventDisplay() {
    PS.GithubEventDisplay.initializeBase(this);
    this.set_trackInteractionEvents(true);
}
PS.GithubEventDisplay.prototype = {
    _githubEvent$2: null,
    e_createdAt: null,
    e_project: null,
    e_description: null,
    
    get_githubEvent: function PS_GithubEventDisplay$get_githubEvent() {
        return this._githubEvent$2;
    },
    set_githubEvent: function PS_GithubEventDisplay$set_githubEvent(value) {
        this._githubEvent$2 = value;
        this.update();
        return value;
    },
    
    _handleDescriptionClick$2: function PS_GithubEventDisplay$_handleDescriptionClick$2(e) {
        if (this._githubEvent$2.payload.commits != null && this._githubEvent$2.payload.commits.length > 0) {
            var url = this._githubEvent$2.payload.commits[0].url;
            url = url.replaceAll('/commits/', '/commit/');
            url = url.replaceAll('/api.', '/');
            url = url.replaceAll('/repos/', '/');
            window.open(url);
        }
    },
    
    onApplyTemplate: function PS_GithubEventDisplay$onApplyTemplate() {
        PS.GithubEventDisplay.callBaseMethod(this, 'onApplyTemplate');
        if (this.e_description != null) {
            this.e_description.addEventListener('click', ss.Delegate.create(this, this._handleDescriptionClick$2), true);
        }
        if (this.e_project != null) {
            this.e_project.addEventListener('click', ss.Delegate.create(this, this._handleProjectClick$2), true);
        }
    },
    
    _handleProjectClick$2: function PS_GithubEventDisplay$_handleProjectClick$2(e) {
        window.open('https://github.com/' + this._githubEvent$2.repo.name + '/blob/master/Readme.md');
    },
    
    onUpdate: function PS_GithubEventDisplay$onUpdate() {
        PS.GithubEventDisplay.callBaseMethod(this, 'onUpdate');
        if (this._githubEvent$2 == null) {
            return;
        }
        if (this.e_createdAt != null) {
            var d = Date.parseDate(this._githubEvent$2.created_at);
            BL.UI.ElementUtilities.setText(this.e_createdAt, BL.Utilities.getFriendlyDateDescription(d));
        }
        if (this.e_project != null) {
            BL.UI.ElementUtilities.setText(this.e_project, this._githubEvent$2.repo.name);
        }
        if (this.e_description != null) {
            if (this._githubEvent$2.payload.commits != null && this._githubEvent$2.payload.commits.length > 0) {
                BL.UI.ElementUtilities.setText(this.e_description, this._githubEvent$2.payload.commits[0].message);
            }
        }
    }
}


////////////////////////////////////////////////////////////////////////////////
// PS.GithubUserActivityDisplay

PS.GithubUserActivityDisplay = function PS_GithubUserActivityDisplay() {
    PS.GithubUserActivityDisplay.initializeBase(this);
}
PS.GithubUserActivityDisplay.prototype = {
    _maxItemsToDisplay$3: null,
    _userName$3: null,
    _activeEventsRequest$3: null,
    _events$3: null,
    
    get_i_maxItemsToDisplay: function PS_GithubUserActivityDisplay$get_i_maxItemsToDisplay() {
        return this._maxItemsToDisplay$3;
    },
    set_i_maxItemsToDisplay: function PS_GithubUserActivityDisplay$set_i_maxItemsToDisplay(value) {
        if (this._maxItemsToDisplay$3 === value) {
            return;
        }
        this._maxItemsToDisplay$3 = value;
        this.update();
        return value;
    },
    
    get_s_userName: function PS_GithubUserActivityDisplay$get_s_userName() {
        return this._userName$3;
    },
    set_s_userName: function PS_GithubUserActivityDisplay$set_s_userName(value) {
        if (this._userName$3 === value) {
            return;
        }
        this._userName$3 = value;
        this._retrieveActivities$3();
        return value;
    },
    
    _retrieveActivities$3: function PS_GithubUserActivityDisplay$_retrieveActivities$3() {
        if (this._userName$3 == null) {
            return;
        }
        var requestUrl = String.format('https://api.github.com/users/{0}/events', this._userName$3);
        this._activeEventsRequest$3 = new XMLHttpRequest();
        this._activeEventsRequest$3.open('GET', requestUrl);
        this._activeEventsRequest$3.setRequestHeader('Accept', 'application/json');
        this._activeEventsRequest$3.setRequestHeader('Content-Type', 'application/json');
        this._activeEventsRequest$3.onreadystatechange = ss.Delegate.create(this, this._retrieveActivitiesContinue$3);
        this._activeEventsRequest$3.send('');
    },
    
    _retrieveActivitiesContinue$3: function PS_GithubUserActivityDisplay$_retrieveActivitiesContinue$3() {
        if (this._activeEventsRequest$3 != null && this._activeEventsRequest$3.readyState === 4) {
            var results = this._activeEventsRequest$3.responseText;
            if (results != null) {
                this._events$3 = JSON.parse(results);
                this.update();
            }
        }
    },
    
    onUpdate: function PS_GithubUserActivityDisplay$onUpdate() {
        if (this._events$3 == null) {
            return;
        }
        this.clearItemControls();
        var itemCount = 0;
        var $enum1 = ss.IEnumerator.getEnumerator(this._events$3);
        while ($enum1.moveNext()) {
            var ghe = $enum1.current;
            var ghed = new PS.GithubEventDisplay();
            ghed.set_githubEvent(ghe);
            this.addItemControl(ghed);
            itemCount++;
            if (this._maxItemsToDisplay$3 != null && itemCount >= this._maxItemsToDisplay$3) {
                return;
            }
        }
    }
}


////////////////////////////////////////////////////////////////////////////////
// PS.App

PS.App = function PS_App() {
    PS.App.initializeBase(this);
}
PS.App.prototype = {
    e_sections: null,
    
    onInit: function PS_App$onInit() {
        PS.App.callBaseMethod(this, 'onInit');
        window.addEventListener('hashchange', ss.Delegate.create(this, this._handleHashChange$2));
        this._processHash$2();
    },
    
    _handleHashChange$2: function PS_App$_handleHashChange$2(e) {
        this._processHash$2();
    },
    
    _processHash$2: function PS_App$_processHash$2() {
        var hashCanon = window.location.hash.toLowerCase();
        hashCanon = hashCanon.substring(1, hashCanon.length);
        switch (hashCanon) {
        }
    },
    
    onUpdate: function PS_App$onUpdate() {
    }
}


PS.GithubEventDisplay.registerClass('PS.GithubEventDisplay', BL.UI.Control);
PS.GithubUserActivityDisplay.registerClass('PS.GithubUserActivityDisplay', BL.UI.ItemsControl);
PS.App.registerClass('PS.App', BL.UI.Control);
})(jQuery);

//! This script was generated using Script# v0.7.4.0
