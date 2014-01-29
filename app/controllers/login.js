OpenCBS.LoginController = Ember.Controller.extend({
  actions: {
    login: function() {
      var session = OpenCBS.Session.current();
      var data = {
        id: session.get('id'),
        username: this.get('username'),
        password: this.get('password')
      };
      var attemptedTransition = this.get('attemptedTransition');
      var self = this;

      $.post('/api/sessions', data).then(function(response) {
        session.setProperties(response);
        if (attemptedTransition) {
          attemptedTransition.retry();
          self.set('attemptedTransition', null);
        } else {
          self.transitionToRoute('index');
        }
      }, function() {
        window.alert('Login failed.');
      });
    }
  }
});
