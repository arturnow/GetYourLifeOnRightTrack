
//document.addEventListener('DOMContentLoaded', function (event) { console.log('Loaded!'); }, false);

(function () {
    'use strict';
    $(function () {
        //To jest wcześniej
        console.log('Loaded from jQuery!');


        //Setup the form
        $('#btnAddDomain').click(function () {
            getCurrentTabUrl(addDomain);
        });

        $('#btnRemoveDomain').click(function () {
            getCurrentTabUrl(removeDomain);
        });


        $('#btnStopRecording').click(function () {
            stopRecord();
        });

        $('#btnCancelRecording').click(function () {
            cancelRecord();
        });

        $('#btnList').click(function () {
            getList();
        });

        $('#btnLogin').click(function () {
            login();
        });

        $('#btnGetWaster').click(function () {
            var idToPass = getWasterId();
            $.get('http://localhost:49418/api/WasterPattern', idToPass, function () {
                alert('zwrotka');
            }, 'json');

        });

        $('#btnMessageTest').click(function() {
            getCurrentTab(sentTestMessage);
        });

        getCurrentTabUrl(queryExtensionState);

        //chrome.notifications.onButtonClicked.addListener(function(id, index) {
        //    alert('Wiadomośc od ' + id + ' z guzika o numerze ' + index);
        //});
    });

    function showIndicator() {
        $('#loadIndicator').show();
    }

    function hideIndicator() {
        $('#loadIndicator').hide();
    }

    function login() {
        showIndicator();
        var pass,
            name;

        name = $('#username').val();
        pass = $('#password').val();

        chrome.runtime.sendMessage({
            action: commands.login,
            password: pass,
            username: name
        }, function(response) {
                 responseHandler(response, handlerLoginInfo);
            }
        );
    }

    function responseHandler(response, callback) {
        hideIndicator();
        if (response.hasError && response.hasError === true) {
            alert('Exception: ' + response.message);
        } else if (callback) {
            callback(response);
        }
    }

    function handlerLoginInfo(loginInfo) {
        if (loginInfo.IsLoggedIn) {
            document.getElementById("divLoginPanel").style.display = "none";
            document.getElementById("divControlPanel").style.display = "inherit";
            document.getElementById("divUserInfo").innerHTML = 'You are logged in as ' + loginInfo.userName;
            //$('').
            $('#btnLogin').prop('dFanatycy Sportuisabled', true);
        } else {
            document.getElementById("divLoginPanel").style.display = "inherit";
            document.getElementById("divControlPanel").style.display = "none";
            $('#btnLogin').prop('disabled', false);
        }
    }

    function handleState(stateInfo) {

        if (stateInfo.trackable == true) {
            document.getElementById('btnAddDomain').disabled = false;
        }


        if (stateInfo.state !== undefined) {
            if (stateInfo.state == 'Tracking') {
                document.getElementById('btnAddDomain').disabled = true;
                document.getElementById('btnRemoveDomain').disabled = false;
                document.getElementById('btnStopRecording').disabled = false;
                document.getElementById('btnCancelRecording').disabled = false;
            } else {
                document.getElementById('btnRemoveDomain').disabled = true;
                document.getElementById('btnStopRecording').disabled = true;
                document.getElementById('btnCancelRecording').disabled = true;
            }
        }

        if (stateInfo.totalTime && typeof stateInfo.totalTime == "number") {
            var totalTime = Math.round(stateInfo.totalTime / 1000);
            document.getElementById('spanTime').textContent = totalTime;
            document.getElementById('divTime').className = document.getElementById('divTime').className.replace('hidden', '');
            setInterval(function() {
                totalTime = totalTime + 1;
                document.getElementById('spanTime').textContent = totalTime;
            }, 1000);
        } else {
            document.getElementById('divTime').className = document.getElementById('divTime').className + ' hidden';
        }

        if (stateInfo.totalCount && typeof stateInfo.totalCount == "number") {
            document.getElementById('spanCount').textContent = stateInfo.totalCount;
        } 

    }

    function queryExtensionState(tabUrl) {
        //Send current TabId
        var message = {
            action: commands.queryState
        };

        message.tabUrl = tabUrl;

        chrome.runtime.sendMessage(message, function (response) {
            responseHandler(response, handleState);
            responseHandler(response, handlerLoginInfo);
        });
    }
    /*********************/
    /*********Get Active tabs value, maybe it's better to send message to Background.js!************/

    function getCurrentTabUrl(callback) {
        chrome.tabs.query({ active: true, currentWindow: true, windowType: 'normal' },
            function (tabArray) {
                //Teoretycznie powinna być jedna
                if (tabArray.length > 0 && tabArray[0].url != undefined) {
                    callback(tabArray[0].url);
                } else {
                    console.log('No active normal tabs!');
                }
            })
        ;
    }

    function getCurrentTab(callback) {
        chrome.tabs.query({ active: true, currentWindow: true, windowType: 'normal' },
            function (tabArray) {
                //Teoretycznie powinna być jedna
                if (tabArray.length > 0 && tabArray[0].url != undefined) {
                    callback(tabArray[0]);
                } else {
                    console.log('No active normal tabs!');
                }
            })
        ;
    }

    function addDomainSuccessHanlder(response) {
        handleState({ state: 'Tracking' });
        console.log('This is response: ' + response.message);
    }

    function disableDomainSuccessHanlder(response) {
        handleState({ state: 'Stopped' });
        document.getElementById('btnAddDomain').disabled = false;
        console.log('This is response: ' + response.message);
    }

    ///Adds domain to list 
    function addDomain(domain) {
        showIndicator();
        chrome.runtime.sendMessage({
            value: domain,
            action: commands.addPattern
        }, function (response) {
            responseHandler(response, addDomainSuccessHanlder);
        });
    }

    ///Adds domain to list 
    function removeDomain(domain) {
        showIndicator();
        console.log('Send removePattern command');
        chrome.runtime.sendMessage({
            value: domain,
            action: commands.removePattern
        }, function (response) {
            responseHandler(response, disableDomainSuccessHanlder);
        });
    }

    function trackingStoppedSuccessfully(response) {
        console.log('This is response: ' + response.state);
        if (response.state === 'stopped') {
            document.getElementById('btnStopRecording').disabled = true;
            document.getElementById('btnCancelRecording').disabled = true;
        }
    }

    function stopRecord() {
        showIndicator();
        console.log('Send stop command');
        chrome.runtime.sendMessage({
            action: commands.stopTracking
        }, function (response) {
            responseHandler(response, trackingStoppedSuccessfully);
        });
    }

    function trackingCancelledSuccessfully(response) {
        console.log('This is response: ' + response.state);
        if (response.state == 'cancelled') {
            document.getElementById('btnStopRecording').disabled = true;
            document.getElementById('btnCancelRecording').disabled = true;
        }
    }

    function cancelRecord() {
        showIndicator();
        console.log('Send cancel command');
        chrome.runtime.sendMessage({
            action: commands.cancelTracking
        }, function (response) {
            responseHandler(response, trackingCancelledSuccessfully);
        });
    }

    function getList() {
        chrome.runtime.sendMessage({
            value: '',
            action: commands.getDomainList
        }, function (response) {
            console.log('This is response to getDomainList: ' + response.resp);
            if (response.resp.trim() === '') {
                return;
            }
            var domains = response.resp.split(',');

            var $domains = $('#domains');
            $domains.empty();
            for (var i = 0; i < domains.length; i++) {
                $domains.append($('<li></li>').append(domains[i]));
            }
        });
    }

    function sentTestMessage(tab) {

        chrome.notifications.create('', {
            type: 'basic',
            iconUrl: chrome.runtime.getURL('/img/goal.jpg'),
            title: 'łejstssss',
            message: 'No wstawaj nie udawaj!',
            buttons: [
            { title: 'ehs' }, {
                title: 'nie'
    }]

        }, function(id) {
            alert(id);
        });
        return;
        /*
        chrome.tabs.sendMessage(tab.id, { record : {
            domain : 'test.message.pl'
        } }, function(response) {
            alert('response from content');
        });
        */
    }

    function getWasterId() {
        return $('#key').val();
    }
})();


//Zamiast wysyłać Message mogę po prostu zrobić tak http://markashleybell.com/building-a-simple-google-chrome-extension.html
//chrome.extension.getBackgroundPage().getPageInfo(onPageInfo); //getPageInfo to globalna funkcja, a onPageInfo to jakiś callback