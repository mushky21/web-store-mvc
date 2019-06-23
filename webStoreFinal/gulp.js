// need to Declare the gulp that we using 
var gulp = require('gulp');
var uglify = require('gulp-uglify');
var concat = require('gulp-concat');

//var's that we use alot in my script so we gave them varName and use it in the current script
var paths = {
    webroot: "./wwwroot",
    nodeModules: "./node_modules/"
};
//paths to the src of the file's that i need- usallay will be the same filé's (from dist.)
paths.vendor = [
    paths.nodeModules + "jquery/dist/jquery.js",
    paths.nodeModules + "jquery-validation/dist/jquery.validate.js",
    paths.nodeModules + "jquery-validation-unobtrusive/dist/jquery.validate.unobtrusive.js",
    paths.nodeModules + "bootstrap/dist/js/bootstrap.js"
];
// bring the popperJs
paths.vandorPopper = [paths.nodeModules + "popper.js/dist/umd/popper.js"]

//BootStrap- css this is were a js before -  bring the css that the bootstrap make from the js
paths.vendorStyles = [
    paths.nodeModules + "bootstrap/dist/css/bootstrap.css"
];
//DestPath -  the folder that keep the file's in WWWROOT
paths.vendordest = paths.webroot + "/vendor-script/";
paths.vendorCssDest = paths.webroot + "/vendor-styles/";



// TaskRuner's

gulp.task("load-vendor", () => {
    return gulp.src(paths.vendor)
        .pipe(concat("vendorvalid.js"))//make all the file's to one file
        .pipe(uglify())// mini the script
        .pipe(gulp.dest(paths.vendordest));
});

gulp.task("load-vendor-popper", () => {
    return gulp.src(paths.vandorPopper)
        .pipe(uglify())
        .pipe(gulp.dest(paths.vendordest));
});

// take the css compile the file of the bootstrap to the root.
gulp.task("load-vendor-styles", () => { /*Bootstrap and other third party CSS*/
    return gulp.src(paths.vendorStyles)
        .pipe(concat("vendor.css"))
        .pipe(gulp.dest(paths.vendorCssDest));
});