OpenCBS.ApplicationRoute = Ember.Route.extend({
  model: function() {
    var id = $.cookie('sessionId');
    if (id) {
      return OpenCBS.Session.find(id);
    } else {
      return OpenCBS.Session.create();
    }
  },

  afterModel: function(session) {
    console.log('ApplicationRoute::afterModel');
    console.log(session);
  }
});
