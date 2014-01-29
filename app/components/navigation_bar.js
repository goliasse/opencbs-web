OpenCBS.NavigationBarComponent = Ember.Component.extend({
  actions: {
    logout: function() {
       this.sendAction('logout');
    }
  },
  tagName: 'nav',
  classNameBindings: ['isAuthenticated:authenticated'],
  isAuthenticated: false
});
