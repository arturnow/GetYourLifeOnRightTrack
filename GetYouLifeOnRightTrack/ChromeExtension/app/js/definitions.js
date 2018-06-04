/***************TRACKRECORD CLASS**************/
/*                                            */
/*         Types definitions                  */
/*                                            */
/**********************************************/


//TrackRecord
function TrackRecord(id, domain, startDate) {
    this.id = id;
    this.domain = domain;
    this.startDate = startDate;
    this.endDate = null;
};

TrackRecord.prototype.isCancelled = null;
TrackRecord.prototype.syncDatetime = null;
TrackRecord.prototype.syncErrorMessage = null;
TrackRecord.prototype.isWorking = null;
TrackRecord.prototype.guid = null;

TrackRecord.prototype.sync = function (guid) {
    this.syncDatetime = new Date();
    this.guid = guid;
};


/* Functions */
TrackRecord.prototype.stopTracking = function () {
    if (this.endDate === null && this.isCancelled !== true) {
        this.endDate = new Date();
        return true;
    } else {
        console.log('Couldn\'t stop record with endDate ' + this.endDate + ' and isCancelled ' + this.isCancelled);
    }
};

TrackRecord.prototype.cancelTracking = function () {
    if (this.endDate === null && this.isCancelled !== true) {
        this.isCancelled = true;
        this.endDate = new Date();
        return true;
    } else {
        console.log('Couldn\'t cancel record with endDate ' + this.endDate + ' and isCancelled ' + this.isCancelled);
    }
};


/********************************/
//TotalTimeRecord
//TODO: Rename to sth more meaningful - not only time, but count
function TotalTimeRecord() {
};

TotalTimeRecord.prototype.domain = null;
TotalTimeRecord.prototype.totalTime = null;
TotalTimeRecord.prototype.totalCount = null;
//{ domain, total}

/********************************/


function NotyficationSettings() {
    this.AllowedDayTime = 1 * 60 * 1000;
    this.AllowedWeekTime = this.AllowedDayTime * 5;
    this.AllowedMonthTime = this.AllowedWeekTime * 3;

    this.AllowedDayCount = 20;
};

NotyficationSettings.prototype.AllowedDayTime = null;
NotyficationSettings.prototype.AllowedWeekTime = null;
NotyficationSettings.prototype.AllowedMonthTime = null;

NotyficationSettings.prototype.AllowedDayCount = null;
NotyficationSettings.prototype.AllowedWeekCount = null;
NotyficationSettings.prototype.AllowedMonthCount = null;


//Base64 encoder
var Base64 = {
    // private property
    _keyStr: "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=",

    // public method for encoding
    encode: function (input) {
        var output = "";
        var chr1, chr2, chr3, enc1, enc2, enc3, enc4;
        var i = 0;

        input = Base64._utf8_encode(input);

        while (i < input.length) {

            chr1 = input.charCodeAt(i++);
            chr2 = input.charCodeAt(i++);
            chr3 = input.charCodeAt(i++);

            enc1 = chr1 >> 2;
            enc2 = ((chr1 & 3) << 4) | (chr2 >> 4);
            enc3 = ((chr2 & 15) << 2) | (chr3 >> 6);
            enc4 = chr3 & 63;

            if (isNaN(chr2)) {
                enc3 = enc4 = 64;
            } else if (isNaN(chr3)) {
                enc4 = 64;
            }

            output = output +
                Base64._keyStr.charAt(enc1) + Base64._keyStr.charAt(enc2) +
                Base64._keyStr.charAt(enc3) + Base64._keyStr.charAt(enc4);

        }

        return output;
    },

    // public method for decoding
    decode: function (input) {
        var output = "";
        var chr1, chr2, chr3;
        var enc1, enc2, enc3, enc4;
        var i = 0;

        input = input.replace(/[^A-Za-z0-9\+\/\=]/g, "");

        while (i < input.length) {

            enc1 = Base64._keyStr.indexOf(input.charAt(i++));
            enc2 = Base64._keyStr.indexOf(input.charAt(i++));
            enc3 = Base64._keyStr.indexOf(input.charAt(i++));
            enc4 = Base64._keyStr.indexOf(input.charAt(i++));

            chr1 = (enc1 << 2) | (enc2 >> 4);
            chr2 = ((enc2 & 15) << 4) | (enc3 >> 2);
            chr3 = ((enc3 & 3) << 6) | enc4;

            output = output + String.fromCharCode(chr1);

            if (enc3 != 64) {
                output = output + String.fromCharCode(chr2);
            }
            if (enc4 != 64) {
                output = output + String.fromCharCode(chr3);
            }

        }

        output = Base64._utf8_decode(output);

        return output;

    },

    // private method for UTF-8 encoding
    _utf8_encode: function (string) {
        string = string.replace(/\r\n/g, "\n");
        var utftext = "";

        for (var n = 0; n < string.length; n++) {

            var c = string.charCodeAt(n);

            if (c < 128) {
                utftext += String.fromCharCode(c);
            } else if ((c > 127) && (c < 2048)) {
                utftext += String.fromCharCode((c >> 6) | 192);
                utftext += String.fromCharCode((c & 63) | 128);
            } else {
                utftext += String.fromCharCode((c >> 12) | 224);
                utftext += String.fromCharCode(((c >> 6) & 63) | 128);
                utftext += String.fromCharCode((c & 63) | 128);
            }

        }

        return utftext;
    },

    // private method for UTF-8 decoding
    _utf8_decode: function (utftext) {
        var string = "";
        var i = 0;
        var c = c1 = c2 = 0;

        while (i < utftext.length) {

            c = utftext.charCodeAt(i);

            if (c < 128) {
                string += String.fromCharCode(c);
                i++;
            } else if ((c > 191) && (c < 224)) {
                c2 = utftext.charCodeAt(i + 1);
                string += String.fromCharCode(((c & 31) << 6) | (c2 & 63));
                i += 2;
            } else {
                c2 = utftext.charCodeAt(i + 1);
                c3 = utftext.charCodeAt(i + 2);
                string += String.fromCharCode(((c & 15) << 12) | ((c2 & 63) << 6) | (c3 & 63));
                i += 3;
            }

        }
        return string;
    }
};

var commands = {
    login: 'loginCommand',
    addPattern: 'addPatternCommand',
    getDomainList: 'getPatternListCommand',
    removePattern: 'removePatternCommand',
    cancelTracking: 'cancelTrackingCommand',
    stopTracking: 'stopTrackingCommand',
    queryState: 'queryStateCommand',
    messageTemplate: 'getTemplateCommand'
};


var trackingState = {
    Tracking: 'Tracking',
    Stopped: 'Stopped',
    Cancelled: 'Cancelled',
    Working: 'Working'
};