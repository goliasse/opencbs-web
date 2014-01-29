OpenCBS.Session = OpenCBS.Model.extend({
  id: null,
  user: null,

  isAuthenticated: function() {
    return !Ember.isEmpty(this.get('user'));
  }.property('user')
});

OpenCBS.Session.reopenClass({
  find: function(id) {
    return $.getJSON('/api/sessions/' + id).then(function(response) {
      var session = OpenCBS.Session.create({ id: response.id });
      session.set('user', OpenCBS.User.create(response.user));
      return session;
    });
  }
});
