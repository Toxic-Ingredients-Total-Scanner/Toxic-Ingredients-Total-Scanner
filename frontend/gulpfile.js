//variables
var gulp = require('gulp');
var less = require('gulp-less');
var browserSync = require('browser-sync').create();
var less = require('gulp-less');
var concat = require('gulp-concat');
var minify = require('gulp-minify-css');
var merge = require('merge-stream');

//tasks
function style() {
  var lessStream = gulp.src('./app/**/*.less')
    .pipe(less())
    .pipe(concat('less-files.less'))
  ;
  var mergedStream = merge(lessStream)
    .pipe(concat('style.css'))
    //.pipe(minify())
    .pipe(gulp.dest('./app'));

  return mergedStream;
}

function watch() {
    browserSync.init({
        server: {
            baseDir: './app'
        },
        port: 8000
    });
    gulp.watch('./app/**/*.less', style);
    gulp.watch('./**/*.html').on('change', browserSync.reload);
    gulp.watch('./**/*.js').on('change', browserSync.reload);
}

exports.style = style;
exports.watch = watch;
