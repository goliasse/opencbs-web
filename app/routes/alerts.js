OpenCBS.AlertsRoute = OpenCBS.AuthenticatedRoute.extend({
  model: function() {
    return OpenCBS.Alert.findAll();
  }
});
