"use strict";

var webpack = require('webpack');

module.exports = {
    entry: "./Views/Planner/eventDetail.jsx",
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
            { test: /\.(png|woff|woff2|eot|ttf|svg)$/, loader: 'url-loader?limit=100000' },
            { test: /\.js$/, exclude: /node_modules/, loaders: ['react-hot', 'babel?stage=0&optional=runtime&plugins=typecheck'] }
        ],
        resolve: {
            extensions: ['', '.js', '.css', '.jsx'],
            modulesDirectories: [
                'node_modules'
            ]
        }
    }
};