
var loggerId = "";
var logs = [];

var template = '<div id="message_title">{{title}}</div>' +
    '<div id="message_content"><a href="{{url}}"><img src="{{imageUrl}}" alt="Image to message" width="60" height="60"></a>{{content}}</div>';


//TODO: We need array of templates per message type

//Ten cały Popup będzie ściągany z serwera
function createMessagePopup() {
    $('body').append('<div class="on-the-right-track-message-container">' +
                            '<div class="on-the-right-track-message-header">Get on the right TRACK<div class="on-the-right-track-message-main-close"></div></div>' +
                            '<div class="wasterLog"></div>' +
                    '</div>');

    $('div.on-the-right-track-message-container').attr('id', loggerId);

    $('div.on-the-right-track-message-container').draggable();

    $('div.on-the-right-track-message-main-close').click(function () {
        debugger;
        $('div.on-the-right-track-message-container').remove();
    });
}

//Także z serwera - może tu po prostu zrobić odwołanie Ajax
function getHTMLMessage(message) {

    return template.replace('{{title}}', message.Title).replace('{{content}}', message.Content).replace('{{url}}', message.Url).replace('{{imageUrl}}', message.ImageUrl);
}
$(function () {

    loggerId = "shouldBeGenerated";

    //TOOO : get message tempate!

    chrome.runtime.sendMessage('', { action: 'getTemplateCommand' }, function (response) {
        template = response.HTMLTemplate;
        console.log(template);
        //TODO: Tu będzie pobieranie template'u, póki co nie tracę na to czasu
    });


});


function remind(timeInMinutes) {
    chrome.runtime.sendMessage('', { action: 'remind', 'timeInMinutes': timeInMinutes }, function (response) {
        template = response.HTMLTemplate;
        console.log(template);
        //TODO: Tu będzie pobieranie template'u, póki co nie tracę na to czasu
    });
}

chrome.runtime.onMessage.addListener(function (message, sender, sendResponse) {
    var i;
    //sender.id //it's extension   
    //var logInfo = message.record;
    //For future use
    logs.push(message.record);



    var $logger = $('div.wasterLog');
    if ($logger.length < 1) {
        createMessagePopup();
        $logger = $('div.wasterLog');
    }

    if (message.commandId === 1) {
        $logger.append(getHTMLMessage(message));
        //$logger.append($('<p></p>').append(message.value));
    } else if (message.commandId === 2) {
        for (i = 0; i < message.messages.length; i++) {
            $logger.append(getHTMLMessage(message.messages[i]));
            $('div.on-the-right-track-message-close').attr('title', 'Remind in 15 minutes');

            $('div.on-the-right-track-message-close').click(function () {
                $(this).parent().remove();
                remind(15);
            });
        }
    } else {
        //TODO:
    }

    $("div.wasterLog").animate({ scrollTop: $('div.wasterLog')[0].scrollHeight }, 1000);
});


/*
$('body').append('<div style="top: 20px; right: 200px;  background: red; width: 100px; height: 100px; position: absolute">')
*/