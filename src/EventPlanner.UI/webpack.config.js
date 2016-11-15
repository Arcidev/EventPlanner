"use strict";

module.exports = {
    entry: "./Views/Home/index.jsx",
    output: {
        filename: "./wwwroot/bundle.js"
    },
    module: {
        loaders: [
            {
                test: /\.jsx$/,
                loader: "babel-loader",
                exclude: /node_modules/,
                query: {
                    presets: ["es2015", "react"]
                }
            },
            { test: /\.css$/, loader: "style-loader!css-loader" },
            { test: /\.(png|woff|woff2|eot|ttf|svg)$/, loader: 'url-loader?limit=100000' }
        ],
        resolve: {
            extensions: ['', '.js', '.css']
        },
        modulesDirectories: [
          'node_modules'
        ]
    }
};