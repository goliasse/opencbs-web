App.Router.map(function() {
  this.route('login');
  //this.route('logout');
  //this.route('about');
});

App.Router.reopen({
  location: 'history'
});
