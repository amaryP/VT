# [GET] REST - Direct – TAAPI.IO

- Home
- News
- Pricing
- Indicators
- Exchanges
- Docs
- Log in
- Free API key

Home–Documentation–Integration–[GET] REST – Direct


## [GET] REST – Direct
You can query the API with a simple GET request. All that is needed is to send your request to:https://api.taapi.iowith at least the mandatory parameters. Additionally, this is the endpoint you need for fetching historical data.


## Pros
- Easy to get started
- Works with NodeJS, PHP, Python, Ruby, Curl or via browser
- Historical data


## Getting started
To get started, simply make an HTTPS GET Request or call in your browser:


```
https://api.taapi.io/rsi?secret=API_KEY&exchange=binance&symbol=BTC/USDT&interval=1h
```
A JSON Response is returned:


```
{
  "value": 69.8259211745199
}
```

## Mandatory Parameters
Our Direct method requires these parameters:

`binance` `binancefutures` `BTC/USDT` `LTC/BTC` `1m` `5m` `15m` `30m` `1h` `2h` `4h` `12h` `1d` `1w` `interval=1h` `interval=1d` Depending on the indicator you call, there may or may not be more mandatory parameters. Additionally, there may be several other optional paramters, also depending on the indicator. Please refer to theIndicators pagefor more information.


## Optional Parameters
Below is a list of optional parameters that all the indicators will take:

`backtrack` `backtrack=5` `0` `50` `chart` `candles` `heikinashi` `candles` `heikinashi` `20` `20` `max` `max` `true` `false` `false` `true` `true` `false` `true` 
## Headers
Some REST clients may need to be told explicitly which headers to use. Add these headers to the requests if the responses doesn’t match the expected output.

`Content-Type` `Accept-Encoding` 
## Tools & Wrappers
TAAPI.IO comes with a variety of 3rd party integrations and wrappers, some of which includesNPM,PHPand ‘no-code’ integrations such asMake. Please take a moment to go through this listUtilities / 3rd party integrations.


## Examples
Below you’ll find some examples, how to connect, authenticate and query the API:


## NodeJS
Javascript is a great language for coding bots, and using the NodeJS package makes it even simpler. Please refer to theNPM | NodeJS | TypeScriptguide for detailed guidelines on this. Or simply call taapi directly using Axios.


```
// Require taapi (using the NPM client: npm i taapi --save)
const Taapi = require("taapi");
 
// Setup client with authentication
const taapi = new Taapi.default("TAAPI_SECRET");
 
taapi.getIndicator("rsi", "BTC/USDT", "1h").then( rsi => {
    console.log(rsi);
});
```

```
// Import
import Taapi from 'taapi';

// Init taapi
const taapi = new Taapi("TAAPI_SECRET");

taapi.getIndicator("rsi", "BTC/USDT", "1h").then( rsi => {
    console.log(rsi);
});
```

```
// Import
import Taapi from 'taapi';

// Init taapi
const taapi = new Taapi("TAAPI_SECRET");

taapi.getIndicator("rsi", "AAPL", "1h", {
    type: "stocks",
}).then( rsi => {
    console.log(rsi);
});
```

```
// Require axios: npm i axios
var axios = require('axios');

axios.get('https://api.taapi.io/rsi', {
  params: {
    secret: "TAAPI_SECRET",
    exchange: "binance",
    symbol: "BTC/USDT",
    interval: "1h",
  }
})
.then(function (response) {
  console.log(response.data);
})
.catch(function (error) {
  console.log(error.response.data);
});
```

## PHP
Use the built in tools in PHP and make CURL request, or usePackagist.org | PHP Composerto make life easier.


```
<?php

// Require taapi single
require 'vendor/taapi/php-client/single.php';

// Init taapi
$taapi = new TaapiSingle("TAAPI_SECRET");

// Calculate indicator
$result = $taapi->execute("rsi", "binance", "BTC/USDT", "1h", array(
    "period" => 200,
    "backtrack" => 1
));

// Print result
echo "RSI: $result->value";
```

```
<?php

$endpoint = 'rsi';

$query = http_build_query(array(
  'secret' => 'TAAPI_SECRET',
  'exchange' => 'binance',
  'symbol' => 'BTC/USDT',
  'interval' => '1h'
));

// Define endpoint
$url = "https://api.taapi.io/{$endpoint}?{$query}";

// create curl resource 
$ch = curl_init(); 

// set url 
curl_setopt($ch, CURLOPT_URL, $url); 

//return the transfer as a string 
curl_setopt($ch, CURLOPT_RETURNTRANSFER, 1); 

// $output contains the output string 
$output = curl_exec($ch); 

// close curl resource to free up system resources 
curl_close($ch);

// View result
print_r(json_decode($output));
```

## Python

```
# Import the requests library 
import requests 

# Define indicator
indicator = "rsi"
  
# Define endpoint 
endpoint = f"https://api.taapi.io/{indicator}"
  
# Define a parameters dict for the parameters to be sent to the API 
parameters = {
    'secret': 'TAAPI_SECRET',
    'exchange': 'binance',
    'symbol': 'BTC/USDT',
    'interval': '1h'
    } 
  
# Send get request and save the response as response object 
response = requests.get(url = endpoint, params = parameters)
  
# Extract data in json format 
result = response.json() 

# Print result
print(result)
```

## Ruby

```
require 'net/http'
uri = URI("https://api.taapi.io/rsi?secret=TAAPI_SECRET&exchange=binance&symbol=BTC/USDT&interval=1h")
puts Net::HTTP.get(uri)
```

## Curl

```
curl "https://api.taapi.io/rsi?secret=TAAPI_SECRET&exchange=binance&symbol=BTC/USDT&interval=1h"
```

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

