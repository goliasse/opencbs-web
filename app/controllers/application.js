App.ApplicationController = Ember.Controller.extend({
  actions: {
    logout: function() {
      this.transitionToRoute('logout');
    }
  },
  isAuthenticatedBinding: 'this.authManager.isAuthenticated',
  displayName: function() {
    return this.get('model').firstName + ' ' + this.get('model').lastName;
  }.property('this.model.firstName', 'this.model.lastName'),
  //   var currentUser = this.get('authManager').currentUser;
  //   if (Ember.isEmpty(currentUser)) return '';
  //   return currentUser.FirstName + ' ' + currentUser.LastName;
  // }.property('this.authManager.currentUser'),

  test: function() {
    console.log(this.get('model'));
    return 'Mr. ' + this.get('model').firstName;
  }.property('this.model.firstName')
});
