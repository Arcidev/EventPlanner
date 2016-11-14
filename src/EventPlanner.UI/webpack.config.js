"use strict";

module.exports = {
    entry: "./wwwroot/index.js",
    output: {
        filename: "./wwwroot/bundle.js"
    },
    module: {
        loaders: [
            {
                test: /\.js$/,
                loader: "babel-loader",
                exclude: /node_modules/,
                query: {
                    presets: ["es2015", "react"]
                }
            }
        ]
    }
};