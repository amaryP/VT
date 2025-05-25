# Packagist.org | PHP Composer – TAAPI.IO

- Home
- News
- Pricing
- Indicators
- Exchanges
- Docs
- Log in
- Free API key

Home–Documentation–Utilities / 3rd party integrations–Packagist.org | PHP Composer


## Packagist.org | PHP Composer
PHP is a popular language for developing web apps, and using TAAPI.IO with PHP & Composer is very easy.


## Setup
Create a new folder for your project and install Composer


```
$ mkdir my-taapi-project
$ cd my-taapi-project
$ curl -sS https://getcomposer.org/installer | php
```
Then install taapi


```
$ php composer.phar require taapi/php-client
```
That’s it. TAAPI.IO and composer is now installed and ready to be used!


## Usage (Direct)
The direct method allows you to easily fetch single and multiple indicator values. This method is described in detailhere.


## Single
With just a few lines of code, you can fetch a single indicator value. Create a new PHP file: index.php and paste in the below


```
<?php

// Require taapi single
require 'vendor/taapi/php-client/single.php';

// Init taapi
$taapi = new TaapiSingle("<MY_TAAPI_SECRET>");

// Calculate indicator
$result = $taapi->execute("rsi", "binance", "BTC/USDT", "1h", array(
    "period" => 200,
    "backtrack" => 1
));

// Print result
echo "RSI: $result->value";
```
Run


```
$ php index.php
```
If everything is set up correctly, you should see a result similar to


```
$ RSI: 47.150961379661
```

## Bulk
Bulk allows you to fetch multiple indicators at the same time. Create a new PHP file: index.php and paste in the below


```
<?php

// Require Bulk
include("vendor/taapi/php-client/bulk.php");

// Init taapi
$taapi = new TaapiBulk("<MY_TAAPI_SECRET>");

// Create and add a construct
$construct1 = new TaapiConstruct("binance", "BTC/USDT", "1h");
$construct1->addIndicator("rsi");
$construct1->addIndicator("sma", array(
    "id" => "sma_hourly_200",
    "period" => 200,
    "backtrack" => 1
));
$taapi->addConstruct($construct1);

// Create and add a second construct
$construct2 = new TaapiConstruct("binance", "BTC/USDT", "1d");
$construct2->addIndicator("macd");
$construct2->addIndicator("sma", array(
    "id" => "sma_daily_200",
    "period" => 200,
    "backtrack" => 1
));
$taapi->addConstruct($construct2);

// Execute
$result = $taapi->execute();

// Print results
echo "Result: ";
echo json_encode($result, JSON_PRETTY_PRINT);
echo "\n\nSMA last close on the hourly: ".$result->sma_hourly_200->value;
```
Run


```
$ php index.php
```
NOTE: In order to add multiple constructs (as in more than 1), make sure your TAAPI.IO plan allows for this! The total number of indicators cannot exceed 20, otherwise known as ‘calculations’, for more information, please visit ourrate-limitspage.

You should see an output similar to


```
Result: {
    "binance_BTC\/USDT_1h_rsi_0": {
        "value": 40.584869397130454
    },
    "sma_hourly_200": {
        "value": 19241.204200000007
    },
    "binance_BTC\/USDT_1d_macd_0": {
        "valueMACD": -132.7921811820852,
        "valueMACDSignal": -155.47424557512448,
        "valueMACDHist": 22.682064393039298
    },
    "sma_daily_200": {
        "value": 26071.999149999978
    }
}

SMA last close on the hourly: 19241.2042
```

## That’s it!
Hope you enjoy TAAPI.IO With PHP Composer. As always, comments & feedback appreciated. Happy Trading!


## Ready to get started?
Request your free API key today and get started

- About us
- Subscription
- Affiliate program
- Terms of Service
- Privacy Policy
- Crypto Data API
- Realtime Stock Prices
- API status
- Contact

- Get started
- Integration
- Indicators

© 2025 TAAPI.IO, s.r.o. All rights reserved.

