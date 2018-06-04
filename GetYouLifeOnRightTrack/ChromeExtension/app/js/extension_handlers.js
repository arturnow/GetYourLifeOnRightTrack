// JScript source code
/*************************************/
/* Kod funkcji zdefiniowany w pliku background.js
/*
/**************CHROME LISTENERS **********************************/

//Przy zmianie aktywnego okna
chrome.windows.onFocusChanged.addListener(function (windowsId) {
    //Coœ dziwnego siê dzieje. Ten event jest wystrzeliwany jak klikam na browser action icon
    // chrome.tabs.query({ active: true, windowId: windowsId, windowType: 'normal' }, focusedWindowTabs);
});


//When tab is clicked, but not when we switch between chrome windows
chrome.tabs.onActivated.addListener(tabActivatedHandler);

function isValidTabRequest(changeInfo) {

    console.log(changeInfo);
    console.log(changeInfo.url);
    if (changeInfo.url)
        console.log(changeInfo.url.indexOf('chrome-devtools'));

    return changeInfo.url != undefined && changeInfo.url.indexOf('chrome-devtools') == -1;
};

//When tab is updated e.g. changing URL
chrome.tabs.onUpdated.addListener(function (tabId, changeInfo, tab) {
    console.log('tab onUpdated');
    if (isValidTabRequest(changeInfo)) {
        Waster.DetectTimeWaster(changeInfo.url);
    }
});

//When new tab is created - mostly it doesn't have URL
chrome.tabs.onCreated.addListener(function (tab) {
    console.log('tab onCreated');
    if (isValidTabRequest(tab)) {
        Waster.DetectTimeWaster(tab.url);
    }
});

//Some example how chrome storage works
chrome.storage.onChanged.addListener(function (changes, namespace) {
    for (key in changes) {
        var storageChange = changes[key];
        console.log('Storage key "%s" in namespace "%s" changed. ' +
                      'Old value was "%s", new value is "%s".',
                      key,
                      namespace,
                      storageChange.oldValue,
                      storageChange.newValue);
    }
});


/**Listens to local message**/
chrome.runtime.onMessage.addListener(MessageHandler);


chrome.idle.onStateChanged.addListener(function (state) {
    console.log(state + ' on ' + new Date().toJSON());
});
