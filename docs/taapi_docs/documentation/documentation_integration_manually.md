# [POST] REST - Manual – TAAPI.IO

- Home
- News
- Pricing
- Indicators
- Exchanges
- Docs
- Log in
- Free API key

Home–Documentation–Integration–[POST] REST – Manual


## [POST] REST – Manual
Use this method if you want to use your own price data and use TAAPI to calculate the indicators from the provided dataset. To query the API you need to submit a POST request to:https://api.taapi.iowith at least the mandatory parameters.


```
https://api.taapi.io/rsi
```
You can calculate TA off of any candle set you have. Using this method requires sending a simple post including the candle data for which to calculate an indicator value.


## Getting started
Submit a post with parameters in the request body to the above endpoint which includes your ‘secret’ and a ‘candles’ parameter containing the candles in JSON. The candles must be sent as objects with the below keys in an array:

- open:A float, containing the open price of the candle
- high:A float, containing the high price of the candle
- low:A float, containing the low price of the candle
- close:A float, containing the close price of the candle
- volume:A float, containing the volume of the candle

Candles must be submitted in ascending order, being the latest / newest candle last.

Example:


```
[
   {
      "timestamp": 1571320286000, // In milliseconds, optional
      "open": 238.32,
      "high": 343.12,
      "low": 125.94,
      "close": 243.48,
      "volume": 84342.84823
   },
   {
      "timestamp": 1571320286000, // In milliseconds, optional
      "open": 238.32,
      "high": 343.12,
      "low": 125.94,
      "close": 243.48,
      "volume": 84342.84823
   },
   ... n candles
]
```
The amount of candles depends on which indicator is used. If not sure, then send 300 candles. This amount will work for more or less all indicators. However, you can only send a maximum of 500 candles.


## Mandatory Parameters
Below a list of mandatory post parameters, needed to query the API:


## Optional Parameters
There are no optional paramters for this integration method, other than the ones stated for each indicator on theIndicator endpoints page.


## Examples

```
// Require axios: npm i axios
const axios = require('axios');

const indicator = 'rsi';

axios.post(`https://api.taapi.io/${indicator}`, {
  secret: MY_SECRET,
  candles: [{...}]
})
.then(function (response) {
  console.log(response.data);
})
.catch(function (error) {
  console.log(error);
});
```

```
<?php

$indicator = 'rsi';

$parameters = json_encode(array(
  "secret" => 'MY_SECRET',
  'candles' => [{...}] // Candles in json
));
 
// Prepare new cURL resource
$ch = curl_init("https://api.taapi.io/{$indicator}");
curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
curl_setopt($ch, CURLINFO_HEADER_OUT, true);
curl_setopt($ch, CURLOPT_POST, true);
curl_setopt($ch, CURLOPT_POSTFIELDS, $parameters);
 
// Set HTTP Header for POST request 
curl_setopt($ch, CURLOPT_HTTPHEADER, array(
    'Content-Type: application/json',
    'Content-Length: ' . strlen($canldes))
);
 
// Submit the POST request
$result = curl_exec($ch);
 
// Close cURL session handle
curl_close($ch);

// View result
print_r(json_decode($result));
```

## Python

```
# import the requests library
import requests

# Get candles from your own source
candles = [{...}]; # Candles in json

# Define indicator
indicator = "rsi"

# Define endpoint
endpoint = f"https://api.taapi.io/{indicator}"

# Parameters to be sent to API
parameters = {
    'secret': 'MY_SECRET',
    'candles': candles
}

# Send post request and save response as response object
response = requests.post(url = endpoint, json = parameters)

# Extract data in json format
result = response.json()

# Print result
print(result)
```

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

