/*global $, jQuery, TrackRecord, chrome, console*/
var type = 'basic';

var Waster = (function () {
    'use strict';
    var module = {},
    //Private variable
        currentTrackRecord = null,
        isLogged = false,
        lastId = -1, //Wewnętrzna wartość do generowanie ID
        domains = [], //List domen czyli łejsterów
        trackRecords = [], //Rekordy łejsterów
        wasterSumTable = [],
        currentTabId = -1, //Póki co niewykorzyssywaney    
        loggedInUser = null,
        htmlTemplate,
        notificationSettings = new NotyficationSettings(),
        timeOverChromeNofiticationId = '',
        alarmName = null; //lepiej to trzeba zrobić

    chrome.notifications.onButtonClicked.addListener(handleChromeNotificationButton);

    //Private methods
    function generateId() {
        //Simple impementation not to waste time on generatin
        lastId = lastId + 1;
        return lastId;
    }

    function setAjax() {
        /// <summary>Sets global settings for each Ajax request</summary>
        $.ajaxSetup({
            headers: {
                Authorization: type + ' ' + Base64.encode(loggedInUser.username + ':' + loggedInUser.password)
            }
        });
    }

    function handleChromeNotificationButton(notificationId, buttonIndex) {
        if (buttonIndex === 0) {
            
        } else if (buttonIndex === 1) {
            
        }
        //Potrzebuje konstrukcji Id + buttonArray i wtedy mogę do notyfikacji po prostu wysłać ten Array, a button by miał tak jak jest tu : Title, ImageUrl, function

    }

    function notifeBrowserAction() {

        chrome.browserAction.setBadgeText({ "text": "!W!" });
        chrome.browserAction.setBadgeBackgroundColor({ "color": [255, 0, 0, 255] });
    }

    function startTimer() {
        if (alarmName === null) {
            //Every minute check how many minutes we are on site
            chrome.alarms.create('notifier', { periodInMinutes: 1 });
            //We can get alarm by it's name. Not reference
            alarmName = 'notifier';
        }
    }

    function getAddressPattern(address) {
        console.log('Getting pattern for ' + address);
        //Simple implemenation
        var startIndex = 0;

        if (address.indexOf('https') > -1 && address.indexOf('https') < 5) {
            startIndex = 8;
        }
        else if (address.indexOf('http') > -1 && address.indexOf('http') < 5) {
            startIndex = 7;
        } else if (address.indexOf('//') == -1) {
            return address;
        }

        else {
            //Nie obsługujemy chrome:// ftp:// itp
            return '';
        }

        var domain = address.substring(startIndex, address.indexOf('/', startIndex)).split()[0];

        return domain;

    };

    function calculateWasterStatistics(trackRecord) {
        var totalIndex = -1,
            i;
        for (i = 0; i < wasterSumTable.length; i++) {
            if (wasterSumTable[i].domain == trackRecord.domain) {
                totalIndex = i;
                break;
            }
        }
        if (totalIndex === -1) {
            var recordToAdd = new TotalTimeRecord();
            recordToAdd.domain = trackRecord.domain;
            recordToAdd.totalTime = trackRecord.endDate - trackRecord.startDate;
            recordToAdd.totalCount = 1;
            wasterSumTable.push(recordToAdd);
        } else if (totalIndex > -1) {
            wasterSumTable[totalIndex].totalTime = wasterSumTable[totalIndex].totalTime + (trackRecord.endDate - trackRecord.startDate);
            wasterSumTable[totalIndex].totalCount = wasterSumTable[totalIndex].totalCount + 1;
        } else {

            console.log('error situation');
        }

    };
    function createTrackRecord(pattern) {
        var id = generateId();
        return new TrackRecord(id, pattern, new Date());
    }

    function startWasterTracker(pattern) {
        console.log('Tracking started');
        var newTrack = createTrackRecord(pattern);
        trackRecords.push(newTrack);
        currentTrackRecord = newTrack;

        notifeBrowserAction();

        startTimer();
        //Save Track to server
        syncTrackOnServer(newTrack, function (guid) { newTrack.sync(guid); });
    }

    function stopWasterTracker(trackerId, callback) {
        var i, index, trackRecord;
        for (i = trackRecords.length; i > 0; i -= 1) {
            index = i - 1;
            trackRecord = trackRecords[index];
            if (trackRecord.id !== undefined && trackRecord.id === trackerId) {

                if (trackRecord.stopTracking()) {
                    calculateWasterStatistics(trackRecords[trackRecords.length - 1]);
                    updateBrowserAction();
                    StopTrackOnServer(trackRecord, callback);

                    //Reset current track info
                    currentTrackRecord = null;

                    console.log('Tracker with ID ' + trackerId + ' stopeped');
                    console.log('Tracker endDate ' + trackRecords[index].endDate);

                    break;
                }
            }
        }
    }

    function addPatternHandler(domain) {
        domains.push(domain);
        startWasterTracker(domain);
    }

    function removePatternHandler(domain) {
        var itemIndex = $.inArray(domain, domains);
        if (itemIndex > -1) {
            domains.splice(itemIndex, 1); //Splice returns removed items
            if (currentTrackRecord) {
                stopWasterTracker(currentTrackRecord.id);
            }
        }
    }

    function sendContentNotification(title, content) {

        console.log('wysyłam wiadomość!');
        chrome.tabs.sendMessage(currentTabId, {
            commandId: 1,
            record: currentTrackRecord,
            Title: title,
            Content: content
        }, function (response) {
            console.log('response from tab');
        });
    }

    function sendChromeNotification(title, content) {
        chrome.notifications.create('', {
            type: 'basic',
            iconUrl: chrome.runtime.getURL('/img/goal.jpg'),
            title: title,
            message: content,
            buttons: [
            {
                title: 'Przypomnij za 30 minut'
            },
            {
                title: 'Zamknij'
            }]

        }, function(id) {
            this.timeOverChromeNofiticationId = id;
        });
    }

    module.ImportPatterns = function (patterns) {
        /// <summary>Syncs patterns from server</summary>
        /// <param name="patterns" type="Array">array of pattern object</param>

        if (patterns != undefined) {
            var i;
            for (i = 0; i < patterns.length; i++) {
                if (patterns[i].Pattern != undefined) {
                    var itemIndex = $.inArray(patterns[i].Pattern, domains);

                    if (itemIndex < 0) {
                        domains.push(patterns[i].Pattern);
                    }
                }
            }
        } else {
            console.log('Improper argument patterns in Waster.ImportPatterns');
            return;
        }

    };

    module.AddPattern = function (url, callback) {
        /// <summary>Send active tabs domain and sync it with server</summary>
        /// <param name="domain" type="String">domain pattern like www.facebook.com</param>

        var domain = getAddressPattern(url);
        if (domain != undefined && domain.length > 0) {
            var itemIndex = $.inArray(domain, domains);

            if (itemIndex < 0) {
                saveWasterToServer(domain, addPatternHandler, callback);
            }
        }
    };

    module.RemovePattern = function (url, callback) {
        /// <summary>Removes pattern from domains</summary>
        /// <param name="domain" type="String">pattern to remove</param>

        var domain = getAddressPattern(url);
        if (domain !== undefined) {
            var itemIndex = $.inArray(domain, domains);

            if (itemIndex > -1) {
                disableWasterOnServer(domain, removePatternHandler, callback);
            }
        }
    };

    module.IsDomainWaster = function (currentPattern) {
        /// <summary>Checks if current pattern is Waster</summary>
        /// <param name="currentPattern" type="String">Pattern for domain</param>
        /// <returns type="Boolean">True if pattern is added to waster list</returns>
        return $.inArray(currentPattern, domains) > -1;
    };

    module.DetectTimeWaster = function (url, tabId) {
        /// <summary>Detects if we are wasting time</summary>
        /// <param name="currentPattern" type="String">pattern to check</param>
        /// <param name="tabId" type="Number">ID of chrome tab id from which URL was taken\</param>
        /// <returns type="Boolean">True is time is wasted</returns>


        var currentPattern = getAddressPattern(url);

        var result = false;

        //1: Check if pattern has changed
        if (currentTrackRecord === null || currentTrackRecord.domain !== currentPattern) {

            //2: Stop current tracker
            if (currentTrackRecord != null) {
                stopWasterTracker(currentTrackRecord.id);
            }
            //3: Check if URL is on trackering list
            if (module.IsDomainWaster(currentPattern)) {
                //3a: Start tracking
                startWasterTracker(currentPattern);
                result = true;
            } else {
                //3b: don't track
                //TODO: Track full time for ration?
                console.log('Not wasting any time');
                result = false;
            }
        } else {
            //TODO: Cound page transition
            //Pattern is same. Currently we don't do anything
        }

        return result;
    };

    module.CancelCurrentTracker = function (callback) {
        /// <summary>Cancels current track, sets its property IsCancelled to True on server</summary>
        /// <param name="callback" type="Function">It is callback called when operation is successful, used to send response to Popup</param>
        /// <returns type="Boolean">Returns true is tracking was cancelled</returns>
        var result = false;
        if (currentTrackRecord) {
            if (currentTrackRecord.cancelTracking()) {
                result = true;
                updateBrowserAction();
                CancelTrackOnServer(currentTrackRecord.guid, callback);
                currentTrackRecord = null;
            }
        }

        return result;
    };

    module.StopCurrentTracker = function (callback) {
        /// <summary>Stops tracking so it sets track record end date to now</summary>
        /// <param name="callback" type="Function">callback to be called when success</param>
        /// <returns type="Boolean">True if there was record to stop</returns>
        var result = false;

        if (currentTrackRecord) {
            stopWasterTracker(currentTrackRecord.id, callback);
            currentTrackRecord = null;
            result = true;
        }
        return result;
    };

    module.GetTrackingState = function () {
        /// <summary>Gets Waster tracking state</summary>
        /// <returns type="String">Returns Tracking if there is tracking, Stopped if there is no tracking. Working if it's not waster or Cancelled if track was cancelled</returns>
        if (currentTrackRecord) {
            return 'Tracking';
        } else {
            return 'Stopped';
        }
    };

    module.GetTotalStats = function () {
        if (currentTrackRecord) {
            var i;
            for (i = 0; i < wasterSumTable.length; i = i + 1) {
                if (wasterSumTable[i].domain == currentTrackRecord.domain) {


                    return {
                        totalTime: wasterSumTable[i].totalTime + (new Date() - currentTrackRecord.startDate),
                        totalCount: wasterSumTable[i].totalCount + 1
                    };
                }
            }

            return {
                totalTime: new Date() - currentTrackRecord.startDate,
                totalCount: 1
            };
        }
    }

    module.StringifyDomains = function () {
        /// <summary>Simply function that returns string of domains</summary>
        /// <returns type="String">Domain in string comma seperated</returns>
        if (domains.length > 0) {
            return domains.toString();
        } else {
            return "";
        }
    };

    module.ClearIdentity = function() {
        isLogged = false;
        loggedInUser = null;
        //Remove from storage
        
        chrome.storage.sync.remove('credentials',function() {
            debugger;
            console.log(chrome.runtime.lastError);
        });

    };

    module.SetIdentity = function (username, password) {
        /// <summary>Sets Waster identity for each query</summary>
        /// <param name="username" type="String">Username</param>
        /// <param name="password" type="String">Password</param>
        if (username.length > 0 && password.length > 0) {
            isLogged = true;
            loggedInUser = {
                'username': username,
                'password': password
            };

            setAjax();

            //Save to storage
            chrome.storage.sync.set({
                'credentials': {
                    name: username,
                    pass: password
                }
            }, function (data, data1, data2) {
                // Notify that we saved.
                console.log('User data saved Settings saved');
            });
        }
    };

    module.IsLoggedIn = function () {
        /// <summary>Checks if user is logged in</summary>
        /// <returns type="boolean"></returns>
        return loggedInUser !== null && loggedInUser.password !== undefined && loggedInUser.username !== undefined && loggedInUser.password.length > 0 && loggedInUser.username.length;
    };

    module.GetUsername = function () {
        /// <summary>Returns user name</summary>
        /// <returns type="String"></returns>
        return loggedInUser.username;
    };

    module.RememberActiveTab = function (tabId) {
        /// <summary>Remembers tab ID</summary>
        /// <param name="tabId" type="Number">Chrome tab ID</param>
        currentTabId = tabId;
    };

    module.GetActiveTab = function () {
        /// <summary>Returns saved Tab ID</summary>
        /// <returns type="Number">Tab Id</returns>
        return currentTabId;
    };

    module.IsTabValid = function () {
        /// <summary>Checks if tab ID is set</summary>
        /// <returns type="Boolean">True if tab ID > -1</returns>
        return currentTabId > -1;
    };

    module.Notify = function () {
        //TODO: Check minutes and entries - we need notification settings
        var totalEntries = 0,
            totalTime = 0,
            i;

        for (i = 0; i < wasterSumTable.length; i++) {
            totalEntries = wasterSumTable[i].totalCount + totalEntries;
            totalTime = wasterSumTable[i].totalTime + totalTime;
        }

        if (Waster.IsTabValid() && totalTime > notificationSettings.AllowedDayTime) {
            sendContentNotification('Przekroczony czas!', 'Od ' + Math.round(totalTime / 60 / 1000) + ' minut marnujesz swój czas. Czas działać!');
            sendChromeNotification('Przekroczony czas!', 'Od ' + Math.round(totalTime / 60 / 1000) + ' minut marnujesz swój czas. Czas działać!');

        } else {
            console.log('Current time is ' + totalTime);
        }


    };

    module.CheckMessages = function (messageArray) {
        if (currentTabId > 0) {
            chrome.tabs.sendMessage(currentTabId, { commandId: 2, messages: messageArray }, function (response) {
                console.log('Response from page');
            });
        } else {
            console.log('Missing tab ID');
        }
    };

    module.SaveMessageTemplate = function (data) {
        htmlTemplate = data;
    };

    module.GetMessageTemplate = function () {
        return htmlTemplate;
    };

    return module;
})();

//On start-up

$(document).ajaxError(function (event, jqXHR, ajaxSetting, thrownError) {
    var prop;
    if (jqXHR.responseJSON !== undefined) {
        for (prop in jqXHR.responseJSON) {
            if (jqXHR.responseJSON.hasOwnProperty(prop)) {
                console.log('For property \'' + prop + '\' value : ' + jqXHR.responseJSON[prop]);
            }
        }
    }
});

$(function () {
    //If already logged in get templates
    console.log('Time Waster - I\'m starting!!!');

    chrome.storage.sync.get('credentials', function (data) {

        if (data.credentials !== undefined) {
            console.log('User credentials retrieved');

            loginToPage(data.credentials.name, data.credentials.pass);
            /*
            Waster.SetIdentity(data.credentials.name, data.credentials.pass);
            if (Waster.IsLoggedIn()) {
                getWasterTemplatesFromServer(Waster.ImportPatterns);
            }*/
        }
    });


    //Get settings

    chrome.storage.sync.get('notificationSettings', function (data) {

        if (data.notificationSettings !== undefined) {
            console.log('Retrieved notification settings');
            //Waster.SetIdentity(data.credentials.name, data.credentials.pass);
            if (Waster.IsLoggedIn()) {
                getWasterTemplatesFromServer(Waster.ImportPatterns);
            }
        } else {
            console.log('Getting default settings');

        }
    });


});

//TODO: Zmienić nazwę na odpowiednia
function focusedWindowTabs(tabs) {
    if (tabs !== undefined && tabs[0] !== undefined && tabs[0].url !== undefined) {
        Waster.DetectTimeWaster(tabs[0].url);
    }

};

function tabActivatedHandler(activeInfo) {
    Waster.RememberActiveTab(activeInfo.tabId);

    chrome.tabs.get(activeInfo.tabId, function (tab) {
        if (!isValidTabRequest(tab)) {
            return;
        }
        console.log('Start tracker for ' + tab.url);
        Waster.DetectTimeWaster(tab.url);
    });
}


chrome.alarms.onAlarm.addListener(function (alarm) {

    if (alarm.name == '') {
        //We got different alarms
    }

    Waster.Notify();
    GetMessages(Waster.CheckMessages);
    console.log('to ja, alarm');
});

function updateBrowserAction() {
    chrome.browserAction.setBadgeText({ "text": "" });
    chrome.browserAction.setBadgeBackgroundColor({ "color": [0, 255, 0, 255] });
};

function IsValidAddress(url) {
    return url.indexOf('http') > -1;
};

function MessageHandler(request, sender, sendResponse) {
    var isAsyncOp = false, pattern = '';

    if (sender.tab !== undefined) {
        console.log('tab sender');
    }
    if (sender.id !== undefined) {
        console.log('Extension ID: ' + sender.id);
    }
    if (sender.url !== undefined) {
        console.log(sender.url);
    }
    if (sender.tlsChannelId !== undefined) {
        console.log(sender.tlsChannelId);
    }
    if (request.action === commands.login) {
        isAsyncOp = loginToPage(request.username, request.password, sendResponse);
    } else if (request.action == commands.addPattern) {
        Waster.AddPattern(request.value, sendResponse);
        return true;
    }
    else if (request.action === commands.getDomainList) {
        sendResponse({ resp: Waster.StringifyDomains() });
    } else if (request.action === commands.removePattern) {
        Waster.RemovePattern(request.value, sendResponse);
        return true;
    } else if (request.action === commands.stopTracking) {
        Waster.StopCurrentTracker(sendResponse);
        return true; //Async
    } else if (request.action === commands.cancelTracking) {
        Waster.CancelCurrentTracker(sendResponse);
        return true;
    } else if (request.action === commands.queryState) {

        var response = {};
        response.totalTime = 0;
        response.IsLoggedIn = Waster.IsLoggedIn();
        if (response.IsLoggedIn) {
            response.userName = Waster.GetUsername();
        }
        if (Waster.GetTrackingState() == 'Tracking') {
            var stats = {};
            response.state = 'Tracking';
            stats = Waster.GetTotalStats();
            response.totalTime = stats.totalTime;
            response.totalCount = stats.totalCount;
        } else {
            response.state = 'Stopped';
        }

        if (request.tabUrl && IsValidAddress(request.tabUrl)) {
            response.trackable = true;
        }

        sendResponse(response);

    } else if (request.action === commands.messageTemplate) {
        //
        var template = Waster.GetMessageTemplate();

        if (template === undefined) {
            GetMessageTemplate(Waster.SaveMessageTemplate, sendResponse);
            return true;

        }

        sendResponse({ HTMLTemplate: template });
    }

    return isAsyncOp;
}



