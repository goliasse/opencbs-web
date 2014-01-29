window.OpenCBS = Ember.Application.create({
  LOG_ACTIVE_GENERATION: true,
  LOG_MODULE_RESOLVER: true,
  LOG_TRANSITIONS: true,
  LOG_TRANSITIONS_INTERNAL: true,
  LOG_VIEW_LOOKUPS: true,
});

OpenCBS.AuthenticatedRoute = Ember.Route.extend({
  beforeModel: function() {
    var session = OpenCBS.Session.current();
    if (!session.get('isAuthenticated')) {
      this.transitionTo('login');
    }
  }
});
