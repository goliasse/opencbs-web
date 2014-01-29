OpenCBS.ApplicationController = Ember.ObjectController.extend({
  actions: {
    logout: function() {
      var id = this.get('id');
      var self = this;
      $.ajax({
        url: '/api/sessions/' + id,
        type: 'DELETE',
        success: function() {
          self.setProperties({
            isAuthenticated: false,
            userId: null,
            firstName: null,
            lastName: null
          });
          self.transitionToRoute('login');
        }
      });
    }
  },

  displayName: function() {
    if (!this.get('isAuthenticated')) {
      return null;
    }
    return this.get('firstName') + ' ' + this.get('lastName');
  }.property('isAuthenticated', 'firstName', 'lastName')
});
