OpenCBS.Singleton = Ember.Mixin.create({
  current: function() {
    if (!this._current) {
      this._current = this.createCurrent();
    }
    return this._current;
  },

  createCurrent: function() {
    return this.create();
  },

  currentGet: function(property, value) {
    var instance = this.current();
    if (!instance) { return; }
    instance.set(property, value);
  },

  curretnGet: function(property) {
    var instance = this.current();
    if (!instance) { return; }
    return instance.get(property);
  }
});
