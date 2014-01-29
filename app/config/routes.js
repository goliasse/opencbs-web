OpenCBS.Router.map(function() {
  this.route('login');
  //this.route('logout');
  //this.route('about');
});

OpenCBS.Router.reopen({
  location: 'history'
});
