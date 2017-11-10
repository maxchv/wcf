$().ready(function () {
    var ws;
    var isConnected = false;
    function send() {
        if (isConnected) {
            ws.send(tinymce.get("textarea").getContent());
        }
    }

    tinymce.init({
        selector: 'textarea',
        plugins: 'codesample autolink autoresize code',
        codesample_languages: [
            { text: 'HTML/XML', value: 'markup' },
            { text: 'JavaScript', value: 'javascript' },
            { text: 'CSS', value: 'css' },
            { text: 'PHP', value: 'php' },
            { text: 'Ruby', value: 'ruby' },
            { text: 'Python', value: 'python' },
            { text: 'Java', value: 'java' },
            { text: 'C', value: 'c' },
            { text: 'C#', value: 'csharp' },
            { text: 'C++', value: 'cpp' }
        ],
        toolbar: 'codesample code',
        setup: function (editor) {
            editor.on('input', function (e) {
                send();
            });
            editor.on('undo', function () {
                send();
            });
            editor.on('redo', function () {
                send();
            });
        }
    });
    $("#btnConnect").click(function () {
        $("#spanStatus").text("connecting");
        ws = new WebSocket("ws://localhost:2154/WSChatService.svc");
        ws.onopen = function () {
            $("#spanStatus").text("connected");
            isConnected = true;
        };
        ws.onmessage = function (evt) {
            tinymce.get("textarea").setContent(evt.data);
        };
        ws.onerror = function (e) {
            $("#spanStatus").text(e.message);
            isConnected = false;
        };
        ws.onclose = function () {
            $("#spanStatus").text("disconnected");
            isConnected = false;
        };
    });
    $("#btnSend").click(function () {
        if (ws.readyState == WebSocket.OPEN) {
            ws.send($("#textInput").val());
        }
        else {
            $("#spanStatus").text("Connection is closed");
        }
    });
    $("#btnDisconnect").click(function () {
        ws.close();
    });
    $("#textarea").keydown(function () {

    });
	
	addEventListener('click', function(){
		send();
	});
});