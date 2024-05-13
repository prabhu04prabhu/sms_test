(function ($) {

    var webcam1 = {

	extern: null, // external select token to support jQuery dialogs
	append: true, // append object instead of overwriting

	width: 320,
	height: 240,

	mode: "callback", // callback | save | stream

	swffile: "jscam.swf",
	quality: 85,

	debug:	    function () {},
	onCapture:  function () {},
	onTick:	    function () {},
	onSave:	    function () {},
	onLoad:	    function () {}
    };

    window.webcam1 = webcam1;

    $.fn.webcam1 = function(options) {

	if (typeof options === "object") {
	    for (var ndx in webcam1) {
		if (options[ndx] !== undefined) {
		    webcam1[ndx] = options[ndx];
		}
	    }
	}

	var source = '<object id="XwebcamXobjectX" type="application/x-shockwave-flash" data="'+webcam1.swffile+'" width="'+webcam1.width+'" height="'+webcam1.height+'"><param name="movie" value="'+webcam1.swffile+'" /><param name="FlashVars" value="mode='+webcam1.mode+'&amp;quality='+webcam1.quality+'" /><param name="allowScriptAccess" value="always" /></object>';

	if (null !== webcam1.extern) {
	    $(webcam1.extern)[webcam1.append ? "append" : "html"](source);
	} else {
	    this[webcam1.append ? "append" : "html"](source);
	}

	var run = 3;
	(_register = function() {
	    var cam = document.getElementById('XwebcamXobjectX');

	    if (cam && cam.capture !== undefined) {

		/* Simple callback methods are not allowed :-/ */
		webcam1.capture = function(x) {
		    try {
			return cam.capture(x);
		    } catch(e) {}
		}
		webcam1.save = function(x) {
		    try {
			return cam.save(x);
		    } catch(e) {}
		}
		webcam1.setCamera = function(x) {
		    try {
			return cam.setCamera(x);
		    } catch(e) {}
		}
		webcam1.getCameraList = function() {
		    try {
			return cam.getCameraList();
		    } catch(e) {}
		}
		webcam1.pauseCamera = function() {
		    try {
			return cam.pauseCamera();
		    } catch(e) {}
		}		
		webcam1.resumeCamera = function() {
		    try {
			return cam.resumeCamera();
		    } catch(e) {}
		}
		webcam1.onLoad();
	    } else if (0 == run) {
		webcam1.debug("error", "Flash movie not yet registered!");
	    } else {
		/* Flash interface not ready yet */
		run--;
		window.setTimeout(_register, 1000 * (4 - run));
	    }
	})();
    }

})(jQuery);
