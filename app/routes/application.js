OpenCBS.ApplicationRoute = Ember.Route.extend({
  model: function() {
    var id = $.cookie('sessionId');
    if (!id) {
      id = this.createGuid();
      $.cookie('sessionId', id);
    }
    var session = OpenCBS.Session.current();
    session.set('id', id);
    return session.init();
  },

  createGuid: function() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
      var r = Math.random()*16|0, v = c === 'x' ? r : (r&0x3|0x8);
      return v.toString(16);
    });
  }
});
