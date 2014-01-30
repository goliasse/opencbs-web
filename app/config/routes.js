OpenCBS.Router.map(function() {
  this.route('login');
  this.route('alerts');
});

OpenCBS.Router.reopen({
  location: 'history'
});
