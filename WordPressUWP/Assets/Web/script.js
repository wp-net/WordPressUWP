var myElement = document.body;

var mc = new Hammer(myElement);
// listen to events...
mc.on("swipeleft swiperight", function (ev) {
    window.external.notify(ev.type);
});