App.Session = App.Model.extend({
  id: null,
  user: null,

  isAuthenticated: function() {
    return !Ember.isEmpty(this.get('user'));
  }.property('user')
});

App.Session.reopenClass({
  find: function(id) {
    return $.getJSON('/api/sessions/' + id).then(function(response) {
      var session = App.Session.create({ id: response.id });
      session.set('user', App.User.create(response.user));
      return session;
    });
  }
});
