OpenCBS.ApplicationController = Ember.ObjectController.extend({
  actions: {
    logout: function() {
      this.transitionToRoute('logout');
    }
  },

  displayName: function() {
    if (!this.get('isAuthenticated')) {
      return null;
    }
    return this.get('firstName') + ' ' + this.get('lastName');
  }.property('isAuthenticated', 'firstName', 'lastName')
});
