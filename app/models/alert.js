OpenCBS.Alert = OpenCBS.Model.extend({
  contractCode: null,
  lateDays: 0
});

OpenCBS.Alert.reopenClass({
  findAll: function() {
    return $.getJSON('/api/alerts').then(function(response) {
      return response.alerts.map(function(obj) {
        return OpenCBS.Alert.create(obj);
      });
    });
  }
});
