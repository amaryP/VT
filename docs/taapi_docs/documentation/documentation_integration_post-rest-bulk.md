# [POST] REST - Bulk – TAAPI.IO

- Home
- News
- Pricing
- Indicators
- Exchanges
- Docs
- Log in
- Free API key

Home–Documentation–Integration–[POST] REST – Bulk


## [POST] REST – Bulk
Bulk queries provide a convenient way of fetching more than one indicator calculation in just one request. A maximum of 20 calculations is allowed for every plan, including the free plan.


## Getting started
To get started you must send a POST with a JSON body containing your query, to the endpoint/bulk


```
[POST] https://api.taapi.io/bulk
```

## Query
The query is a simple JSON object, and at the top level you will need to supply your secret token, and below that you define theconstruct. This is an object defining the basis for the query, specifically, which candle data is needed for the calculations.

`construct` Finally, the construct takes an array ofindicators, each with it’s own properties.

`indicators` 
```
{
	"secret": "TAAPI_SECRET",
	"construct": {
		"exchange": "binance",
		"symbol": "BTC/USDT",
		"interval": "1h",
		"indicators": [
			{
				"indicator": "rsi"
			},
			{
				"indicator": "cmf",
				"period": 20
			},
			{
				"id": "my_custom_id",
				"indicator": "macd",
				"backtrack": 1
			}
		]
	}
}
```
Theconstructtakes these parameters:

`construct` `binance` `binancefutures` `BTC/USDT` `LTC/BTC` `1m` `5m` `15m` `30m` `1h` `2h` `4h` `12h` `1d` `1w` `interval=1h` `interval=1d` `crypto` `stocks` `crypto` Each element in the aboveindicatorsarray, must be an object containing at least one parameter:indicator. This is the name (or endpoint name) of the indicator. A complete list of indicators may be foundhere.

`indicators` `indicator` Full indicator parameter list:

`<exchange>_<symbol>_<timeframe>_<indicator>_<[parameters]>` `backtrack` `backtrack=5` `0` `50` `chart` `candles` `heikinashi` `candles` `heikinashi` `20 results` `true` `false` `false` `true` `true` `false` `true` 
## Headers
Some REST clients may need to be told explicitly which headers to use. Add these headers to the requests if the responses doesn’t match the expected output.

`Content-Type` `Accept-Encoding` 
## Responses

```
{
  "data": [
    {
      "id": "binance_BTC/USDT_1h_rsi_0",
      "result": {
        "value": 54.32482848167602
      },
      "errors": []
    },
    {
      "id": "binance_BTC/USDT_1h_cmf_20_0",
      "result": {
        "value": -0.08128034485998774
      },
      "errors": []
    },
    {
      "id": "my_custom_id",
      "result": {
        "valueMACD": 21.057252245545897,
        "valueMACDSignal": 13.564391223138724,
        "valueMACDHist": 7.4928610224071726
      },
      "errors": []
    }
  ]
}
```

## Examples
Call taapi using your favourite REST client, or use NPM to do the heavier lifting. Please refer to theNPM | NodeJS | TypeScriptguide for detailed guidelines on this.


```
// Require axios (npm i axios --save)
const axios = require("axios");

await axios.post("https://api.taapi.io/bulk", {
    "secret": "TAAPI_SECRET",
    "construct": {
        "exchange": "binance",
        "symbol": "BTC/USDT",
        "interval": "1h",
        "indicators": [
            {
                // Relative Strength Index
                "indicator": "rsi"
            },
            {
                // Chaikin Money Flow
                "indicator": "cmf",
                "period": 20 // Override the default 14
            },
            {
                // MACD Backtracked 1
                "id": "my_custom_id",
                "indicator": "macd",
                "backtrack": 1
            }
        ]
    }            
}).then( response => {
    console.log(response);
}).catch( error => {
    console.error(error)
});
```
Use the built in tools in PHP and make CURL request, or usePackagist.org | PHP Composerto make life easier.


```
<?php

// Define endpoint
$url = "https://api.taapi.io/bulk";

// Create curl resource 
$ch = curl_init( $url );

// Setup query with JSON payload to be sent via POST.
$query = json_encode( (object) array(

    "secret" => "TAAPI_SECRET",
    "construct" => (object) array(
        "exchange" => "binance",
        "symbol" => "BTC/USDT",
        "interval" => "1h",
        "indicators" => array(
            (object) array(
                // Relative Strength Index
	        "indicator" => "rsi"
            ),
            (object) array(
                // Chaikin Money Flow
	        "indicator" => "cmf",
	        "period" => 20,
            ),
            (object) array(
                // MACD Backtracked 1
                "id" => "my_custom_id",
                "indicator" => "macd",
                "backtrack" => 1
            ),
        )
    )

));

// Add query to CURL
curl_setopt( $ch, CURLOPT_POSTFIELDS, $query );

// Define the content-type to JSON
curl_setopt( $ch, CURLOPT_HTTPHEADER, array('Content-Type:application/json'));

// Return response instead of printing.
curl_setopt( $ch, CURLOPT_RETURNTRANSFER, true );

// Send request.
$result = curl_exec($ch);

// Close curl resource to free up system resources 
curl_close($ch);

// View result
print_r(json_decode($result)->data);
```

```
import requests

url = "https://api.taapi.io/bulk"

payload = {
    "secret": "TAAPI_SECRET",
    "construct": {
        "exchange": "binance",
        "symbol": "BTC/USDT",
        "interval": "1h",
        "indicators": [
            {
                "indicator": "rsi"
            }, {
                "indicator": "ema",
                "period": 20
            },
            {
                "indicator": "macd"
            }, 
            {
                "indicator": "kdj"
            }
        ]
    }
}
headers = {"Content-Type": "application/json"}

response = requests.request("POST", url, json=payload, headers=headers)

print(response.text)
```

## Multiple Constructs
Multiple constructs are a way of fetching indicator values across multiple and different candle sets. Examples might include different timeframes:binance:BTC/USDT:15mandbinance:BTC/USDT:1hor different assetsbinance:XRP/USDT:1d, or even a different exchange:bybit:BTC/USDT:5m.

`binance:BTC/USDT:15m` `binance:BTC/USDT:1h` `binance:XRP/USDT:1d` `bybit:BTC/USDT:5m` The same goes for stocks:AAPL:15mandAAPL:1h, orMSFT:1handTSLA:1h.

`AAPL:15m` `AAPL:1h` `MSFT:1h` `TSLA:1h` Please visit ourMultiple Constructspage for more information.


## Rate limits
Bulk queries help you get the most out of your plan while making the queries more efficient. 1 bulk query = 1 API request, even if you include 20 different indicators inside. So you can get up to 20 calculations in a response by only spending 1 API request.

Multiple constructs are limited according to your plan. For instance, theExpert Plan, will allow you10 different constructs.

`10 different constructs` Using the ‘results’ optional parameter, is limited to 20 results per calculation. If you’re looking for more results, please use theDirect Method.


## That’s it!
As always, feedback, comments are greatly appreciated!


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

