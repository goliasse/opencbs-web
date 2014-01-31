OpenCBS.Alert = OpenCBS.Model.extend({
  contractCode: null,
  lateDays: 0,
  clientName: null,
  amount: 0,
  loanOfficer: null,
  branchName: null,

  lateDaysClass: function() {
    var lateDays = this.get('lateDays');

    if (lateDays > 365) {
      return 'par-365';
    } else if (lateDays > 180 && lateDays <= 365) {
      return 'par-181-365';
    } else if (lateDays > 90 && lateDays <= 180) {
      return 'par-91-180';
    } else if (lateDays > 60 && lateDays <= 90) {
      return 'par-61-90';
    } else if (lateDays > 30 && lateDays <= 60) {
      return 'par-31-60';
    } else if (lateDays > 1 && lateDays <= 30) {
      return 'par-1-30';
    } else {
      return 'par-0';
    }
  }.property('lateDays')
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
