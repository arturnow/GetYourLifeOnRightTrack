// JScript source code
/*****************************************/
/*
/* Code to communicate with WebServices
/*****************************************/

var wasterMainDomain = 'http://localhost:49418/api';


function axajErrorResponseBuilder(jqXHR) {
    var response = {
        hasError: true,
        errorOrigin : 'server'
    };

    if (jqXHR.responseJSON && jqXHR.responseJSON.ExceptionMessage) {
        response.message = jqXHR.responseJSON.ExceptionMessage;
        response.errorType = '.NET Exception';
    } else {
        response.errorType = 'Unknow';
        response.message = 'Error connecting to server';
        //TODO: Build message from responseJSON properies
    }

    return response;

}

//TODO: Rethink it. LoginToPage should only login, and not call anything
function loginToPage(name, pass, callback) {
    $.ajax({
        url: wasterMainDomain + '/AccountsApi/Login',
        data: {
            UserName: name,
            Password: pass
        },
        success: function (data) {
            //TODO: Potrzeba ujednoliczenia zwrotek z serwera, nie wiem czy exception to dobry pomys³
            if (data != true) {
                var response = axajErrorResponseBuilder({}, 'wrong credentails');

                if (callback !== undefined && callback instanceof Function) {
                    callback(response);
                }
                Waster.ClearIdentity();
                return;
            }
            Waster.SetIdentity(name, pass);
            if (callback !== undefined && callback instanceof Function) {
                callback({
                    IsLoggedIn: true,
                    userName: name,
                    password: pass
                });
            }
            //Get patterns!
            getWasterTemplatesFromServer(Waster.ImportPatterns); ;
        },
        error: function (jqXHR, status, errorThrown) {

            if (callback instanceof Function) {
                var response = axajErrorResponseBuilder(jqXHR);

                callback(response);
            }
        },
        dataType: 'json',
        type: 'POST',
        async: true
    });

    return true;
}

function getWasterTemplatesFromServer(callback) {

    $.ajax({
        url: wasterMainDomain + '/WasterPatternApi/Get',
        //data: data,
        success: function (data) {
            if (callback && callback instanceof Function) {
                callback(data);
            }
        },
        dataType: 'json'
    });

}

/****WASTER HANDLERS****/
function saveWasterToServer(domain, callback, messageResponse) {

    $.post(wasterMainDomain + '/WasterPatternApi/Add',
    {
        Pattern: domain,
        CreateDate: 'UpdateDate',
        IsEnabled: true,
        IsWorking: false
    },
    function (data, textStatus, jqXHR) {
        console.log('Domain patter ID=' + data);
        callback(domain);
        messageResponse({
            success: true,
            message: 'Added ' + domain
        });
        
    },
    'json').fail(function (jqXHR, textStatus, errorThrown) {
        if (messageResponse && messageResponse instanceof Function) {
            var response = axajErrorResponseBuilder(jqXHR);
            messageResponse(response);
        }
    });

}

function disableWasterOnServer(domain, callback, messageResponse) {
    $.post(wasterMainDomain + '/WasterPatternApi/Disable',
    {
        Pattern: domain,
        CreateDate: 'UpdateDate',
        IsEnabled: false,
        IsWorking: false
    },
    function (data, textStatus, jqXHR) {
        console.log('Element disabled for pattern=' + domain);
        callback(domain);
        messageResponse({
            success: true,
            message: 'Removed ' + domain
        });
    },
    'json').fail(function (jqXHR, textStatus, errorThrown) {
        if (messageResponse && messageResponse instanceof Function) {
            var response = axajErrorResponseBuilder(jqXHR);
            messageResponse(response);
        }
    });;
}

function syncTrackOnServer(trackRecord, callback) {
    $.post(wasterMainDomain + '/TrackRecordApi/StartTrack',
    {
        Domain: trackRecord.domain,
        StartTime: trackRecord.startDate.toJSON()
    },
    function (data, textStatus, jqXHR) {
        console.log('Domain patter ID=' + data);
        callback(data);
    },
    'json');
}

function StopTrackOnServer(record, callback) {
    $.ajax({
        url: wasterMainDomain + '/TrackRecordApi/StopTrack/' + record.guid + '',
        data:
             {
                 //    endTime: new Date().toJSON()
                 endTime: record.endDate.toJSON()
             },
        dataType: 'json',
        type: 'POST'
    }
    ).done(function (msg) {
        if (callback != undefined) {
            callback({ state: 'stopped' });
        }
        console.log("success");
    });
}


function CancelTrackOnServer(guid, callback) {
    $.post(wasterMainDomain + '/TrackRecordApi/DeleteTrack/' + guid, { 'id': guid }, function () {
        callback({ state : 'cancelled' }); console.log('cancel succes'); }, 'json');
};

function GetMessages(callback) {
    $.ajax({
        url: wasterMainDomain + '/MessageApi/GetMessage/',
        type: 'POST',
        success: function (data, jqXHR) {
            callback(data);
        },
        error: function (jqXHR, status, errorThrown) {

            if (callback instanceof Function) {
                var response = axajErrorResponseBuilder(jqXHR);

                callback(response);
            }
        },

    });
};
function GetMessageTemplate(callback, messageResponse) {
    $.ajax({
        url: wasterMainDomain + '/MessageApi/GetMessageTemplate/',
        type: 'POST',
        dataType: 'json',
        success: function (data, jqXHR) {
            if (callback instanceof Function) {
                callback(data);
                var response = { HTMLTemplate: Waster.GetMessageTemplate() };
                messageResponse(response);
            }
            console.log(data);
        },
        error: function (jqXHR, status, errorThrown) {

            if (callback instanceof Function) {
                var response = axajErrorResponseBuilder(jqXHR);

                callback(response);
            }
        },

    });
};