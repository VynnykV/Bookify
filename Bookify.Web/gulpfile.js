const { src, dest, watch, series } = require('gulp');
const sass = require('gulp-sass')(require('sass'));
const cleanCSS = require('gulp-clean-css');
const rename = require('gulp-rename');
const plumber = require('gulp-plumber');

// Функція для компіляції SASS
function compileSass() {
    return src('./styles/main.scss')
        .pipe(plumber())
        .pipe(sass().on('error', sass.logError))
        .pipe(dest('./dist/'))
        .pipe(cleanCSS())
        .pipe(rename('main.min.css'))
        .pipe(dest('./dist/'));
}

// Функція для спостереження за змінами
function watchFiles() {
    watch('./styles/**/*.scss', compileSass);
}

// Експорт задач
exports.default = series(compileSass, watchFiles);
exports.compileSass = compileSass;
exports.watch = watchFiles;
