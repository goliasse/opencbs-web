OpenCBS.ApplicationController = Ember.Controller.extend({
  actions: {
    logout: function() {
      this.transitionToRoute('logout');
    }
  },

  isAuthenticatedBinding: 'this.model.isAuthenticated'
});
