OpenCBS.Session = OpenCBS.Model.extend({
  id: null,
  userId: null,
  firstName: null,
  lastName: null,
  isAuthenticated: false,

  init: function() {
    var self = this;
    var id = this.get('id');
    if (!id) { return; }
    return $.getJSON('/api/sessions/' + id).then(function(response) {
      self.setProperties(response);
      return self;
    });
  }
});

OpenCBS.Session.reopenClass(OpenCBS.Singleton);
