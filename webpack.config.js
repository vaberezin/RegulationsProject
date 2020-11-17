const path = require('path');
const NODE_ENV = process.env.NODE_ENV || 'development';

module.exports = {
    entry: "./wwwroot/js/site.js",
    output: {
        path: path.resolve(__dirname, 'wwwroot'),
        filename: 'bundle.js'
      },
    watch: false,
    watchOptions:{
        aggregateTimeout: 100
    },
    devtool : 'source-map',
    module: {
        rules: [
          {
            test: /\.css$/i,
        use: [
            "style-loader",
            {
              loader: "css-loader",
              options: {
                // Run `postcss-loader` on each CSS `@import`, do not forget that `sass-loader` compile non CSS `@import`'s into a single file
                // If you need run `sass-loader` and `postcss-loader` on each CSS `@import` please set it to `2`
                importLoaders: 1,
                // Automatically enable css modules for files satisfying `/\.module\.\w+$/i` RegExp.
                modules: { auto: true },
              },
            },
            
          ],
        },
        {
          test: /\.(png|jpe?g|gif|svg|eot|ttf|woff|woff2)$/i,
          loader: "file-loader",
          options: {
            limit: 8192,
          },
        },
      ],
      },
};