OpenCBS.AlertsView = Ember.View.extend({
  templateName: 'alerts',
  headerStyle: function() {
    return 'padding-right: ' + this.get('scrollbarWidth') + 'px';
  }.property(),

  scrollbarWidth: function() {
    var outer = document.createElement('div');
    outer.style.visibility = 'hidden';
    outer.style.width = '100px';
    document.body.appendChild(outer);

    var widthNoScroll = outer.offsetWidth;
    // force scrollbars
    outer.style.overflow = 'scroll';

    // add inner div
    var inner = document.createElement('div');
    inner.style.width = '100%';
    outer.appendChild(inner);

    var widthWithScroll = inner.offsetWidth;

    // remove divs
    outer.parentNode.removeChild(outer);

    return widthNoScroll - widthWithScroll;
  }.property()
});
