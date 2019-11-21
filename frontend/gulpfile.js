//variables
var gulp = require('gulp');
var less = require('gulp-less');
var browserSync = require('browser-sync').create();

//tasks
function style() {
    return gulp.src('./app/less/**/*.less')
        .pipe(less())
        .pipe(gulp.dest('./app/css'))
        .pipe(browserSync.stream());
}

function watch() {
    browserSync.init({
        server: {
            baseDir: './app'
        },
        port: 8000
    });
    gulp.watch('./app/less/**/*.less', style);
    gulp.watch('./**/*.html').on('change', browserSync.reload);
    gulp.watch('./**/*.js').on('change', browserSync.reload);
}

exports.style = style;
exports.watch = watch;
