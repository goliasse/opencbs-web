App = Ember.Application.create({
  LOG_ACTIVE_GENERATION: true,
  LOG_MODULE_RESOLVER: true,
  LOG_TRANSITIONS: true,
  LOG_TRANSITIONS_INTERNAL: true,
  LOG_VIEW_LOOKUPS: true,
});

App.AuthenticatedRoute = Ember.Route.extend({
  beforeModel: function() {
    if (!this.controllerFor('application').get('isAuthenticated')) {
      this.transitionTo('login');
    }
  }
});
