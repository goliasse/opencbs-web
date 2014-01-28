module.exports = function(grunt) {

  grunt.initConfig({
    clean: {
      debug: ['debug']
    },

    concat: {
      debug: {
        src: ['app/app.js', 'app/models/*.js', 'app/components/*.js', 'app/config/*.js', 'app/routes/*.js', 'app/controllers/*.js'],
        dest: 'debug/assets/application.js'
      }
    },

    copy: {
      debug: {
        files: [{
          expand: true,
          cwd: 'app',
          src: 'index.html',
          dest: 'debug'
        },
        {
          expand: true,
          cwd: 'app',
          src: 'app.css',
          dest: 'debug/assets'
        },
        {
          expand: true,
          cwd: 'vendor/normalize-css',
          src: 'normalize.css',
          dest: 'debug/assets'
        },
        {
          expand: true,
          cwd: 'vendor/components-font-awesome/css',
          src: 'font-awesome.css',
          dest: 'debug/assets'
        },
        {
          expand: true,
          cwd: 'vendor/components-font-awesome/fonts',
          src: '*',
          dest: 'debug/fonts'
        },
        {
          expand: true,
          cwd: 'vendor',
          flatten: true,
          src: ['jquery/jquery.js', 'jquery.cookie/jquery.cookie.js', 'handlebars/handlebars.js', 'ember/ember.js', 'ember-data/ember-data.js'],
          dest: 'debug/vendor'
        }]
      }
    },

    emberTemplates: {
      debug: {
        options: {
          templateBasePath: 'app/templates'
        },
        files: {
          'debug/assets/templates.js': ['app/templates/*.hbs', 'app/templates/components/*.hbs']
        }
      }
    }
  });

  grunt.loadNpmTasks('grunt-contrib-clean');
  grunt.loadNpmTasks('grunt-contrib-copy');
  grunt.loadNpmTasks('grunt-contrib-concat');
  grunt.loadNpmTasks('grunt-ember-templates');

  grunt.registerTask('build:debug', "Building in debug mode", [
    'clean:debug',
    'concat:debug',
    'copy:debug',
    'emberTemplates:debug'
  ]);

  grunt.registerTask('default', ['build:debug']);
}
