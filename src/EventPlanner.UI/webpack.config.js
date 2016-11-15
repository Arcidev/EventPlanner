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
            { test: /\.(png|woff|woff2|eot|ttf|svg)$/, loader: 'url-loader?limit=100000' },
            { test: /\.js$/, exclude: /node_modules/, loaders: ['react-hot', 'babel?stage=0&optional=runtime&plugins=typecheck'] }
        ],
        resolve: {
            extensions: ['', '.js', '.css']
        },
        modulesDirectories: [
          'node_modules'
        ]
    }
};