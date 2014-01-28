App.ApplicationRoute = Ember.Route.extend({
  model: function() {
    var id = $.cookie('sessionId');
    if (id) {
      return App.Session.find(id);
    } else {
      return App.Session.create();
    }
  },

  afterModel: function(session) {
    console.log('ApplicationRoute::afterModel');
    console.log(session);
  }
});
