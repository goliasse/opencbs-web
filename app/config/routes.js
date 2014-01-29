OpenCBS.Router.map(function() {
  this.route('login');
});

OpenCBS.Router.reopen({
  location: 'history'
});
