# NPM Client – TAAPI.IO

- Home
- News
- Pricing
- Indicators
- Exchanges
- Docs
- Log in
- Free API key

Home–Documentation–Utilities / 3rd party integrations–Client


## NPM Client
With this method, the TAAPI.IO Client does the heavy lifting of fetching candle data from the exchanges, and passing it on to the API. This means that it’s the client talking directly to the exchanges, and therefore, you need to watch your usage and stay under the exchange’s rate-limits. For more information on an exchange’s rate-limits, you need to find their technical documentation on their home page.


## Pros
- Calculate TA for any crypto asset, for any  market / symbol / time frame on 124 different crypto exchanges.
- Candles are pulled instantly from the exchanges, making TA indicator calculations real time.
- Execute bulk calls very easily.
- Use this client as a wrapper, if you use NodeJS.


## Cons
- You need to watch the rate-limits for the exchange yourself.
- One extra step if you’re not using NodeJS (More info below).

It’s a tall order to fetch all symbols on all time frames for all the exchanges, and guarantee that this is reliable, and always up-to-date. However, we are working on exactly this, as a second priority to our primary focus (TA). When a stable version of this is ready, we’ll be sure to let you know!


## Getting Started
Our client is NodeJS based (check the NPM Package on npmjs.com), and that can be used either as a wrapper or as a server. Below are some guides based on programming language and operation systems:


## NodeJS
If you’re working with NodeJS, then things are very simple, as you already have NodeJS installed on your platform. Simply install our NPM Package (taapi), and you’re good to go:

Make sure you have your project setup, otherwise, create a new one:

- Create a new folder:mkdir myCryptoProject
- Enter that folder:cd myCryptoProject
- Init a new project:npm init
- Install TAAPI.IO npm package:npm i taapi
- Create your entry file:touch index.js
- Boot up your favorite IDE, and paste the below intoindex.js


```
// Require taapi: npm i taapi --save
const taapi = require("taapi");
 
// Setup client with authentication
const client = taapi.client("MY_SECRET");
 
// Get the BTC/USDT RSI value on the 1 minute time frame from binance
client.getIndicator("rsi", "binance", "BTC/USDT", "1m").then(function(result) {
    console.log("Result: ", result);
})
.catch(function(error){
    console.log(error.message);
});
```
See more info / examples at theNPM Package

Be sure to check out the below required and optional parameters!


## Other programming languages
To get started with anything but NodeJS, you need to install the taapi npm package and run it as a server. Don’t be scared if this is already getting too geeky. It really is fairly simple. Follow these steps (unix commands used):

- VisitNodeJS’s download page, download and install.
- Once installed, create a folder for the taapi local server:mkdir taapiServerand cd to this directory:cd taapiServer.
- Init a new NodeJS project:npm init(and follow the init steps)
- Install taapi:npm install taapi
- Create an empty index.js file:touch index.js
- Boot up your favorite editor and paste the below into the index.js file:


```
// Require taapi
const taapi = require("taapi");
 
// Setup server with authentication
const server = taapi.server("MY_SECRET");
 
// Define port - Optional and defaults to 4101
// server.setServerPort(3000);
 
// Start the server
server.start();
```
- Run the server:node index.js
- If you’re interested in a more in-depth NodeJS guide, then check out:https://www.pluralsight.com/guides/getting-started-with-nodejs

Your terminal should now display something like:


```
TAAPI.IO Server API Running on localhost:4101!
```
You’re now up and running and can use this server with your favorite programming language.

Hint:UsePM2(Process Manager for NodeJS). It makes it easy to run the server as a background process.

- npm install pm2 -g // Install pm2 globally
- pm2 start index.js –name ‘taapi-server’ // Start the taapi server in the background
- pm2 status // Check the status
- pm2 log taapi-server // Check the log
- pm2 –help


## Usage
Now that you’re all setup, and have all the candle data you need, you can start using TAAPI.IO. Below are a couple of examples in different languages.

Check the above example, just belowGetting Startedfor a NodeJS example.


```
// Define query
$query = http_build_query(array(
  'indicator' => 'rsi',
  'exchange' => 'binance',
  'symbol' => 'BTC/USDT',
  'interval' => '1h'
));
 
// Define endpoint. Change the port if you changed it when setting up the server
$url = "http://localhost:4101/indicator?{$query}";
 
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

```
# Import the requests library 
import requests 
  
# Define endpoint 
endpoint = 'http://localhost:4101/indicator'
  
# Define a parameters dict for the parameters to be sent to the API 
parameters = {
    'indicator': 'rsi',
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

## Mandatory Parameters
Using this “Client” method requires at least the below parameters:

`binance` `kucoin` `BTC/USDT` `LTC/BTC` `1m` `3m` `5m` `15m` `30m` `1h` `2h` `4h` `6h` `8h` `12h` `1d` `3d` `1w` `1M` Depending on the indicator you call, there may or may not be more mandatory parameters. Additionally, there may be several other optional paramters, also depending on the indicator. Please refer to theIndicators pagefor more information.


## Optional Parameters
Below is a list of optional parameters that all the indicators will take:

`backtrack` `backtrack=5` The different exchanges have different limits as to how many candles one can fetch in one go. Therefore, a default of 100 is set here. We’re updating our documentation to reflect each exchange’s individual limits. But for now, it’s best to double check that the amount of candles that you expect are returned (use the /candles endpoint), and check that the last one has the expected timestamp. Candles are always returned with the oldest first and newest last.

You can override how many candles the client should attempt to fetch. See more info / examples at theNPM Package


## Bulk Queries
Often multiple indicators are needed to build a working trading strategy. This TAAPI.IO Client makes it very easy to accomplish this. You can also do this with theGraphQL method, building your own GraphQL queries.

Bulk calls are in principal not limited to any plan, but it is only useful with thePro & Expert plans, as these are the only plans that allows for more than 1 call / second. The samerate limitsapply, as with calling single indicators.

These new features are available as of versionv1.2.2of the NPM Client. If you are an existing user of the Client, just do an update:


```
npm update taapi
```

## Getting Started
Initalize the Client just like above calling a single indicator. But instead of using the “getIndicator” method, you can add bulk calls with: “addBulkQuery” and “executeBulkQueries“. Example:


```
// Require taapi: npm i taapi --save
const taapi = require("taapi");
 
// Setup client with authentication
const client = taapi.client("MY_SECRET");

// Init bulk queries. This resets all previously added queries
client.initBulkQueries();
 
// Get the BTC/USDT rsi, ao, adx, cmf, macd, atr, rsi 5 hours ago, 50 MA values on the 1 hour time frame from binance
client.addBulkQuery("rsi", "binance", "BTC/USDT", "1h");
client.addBulkQuery("ao", "binance", "BTC/USDT", "1h");
client.addBulkQuery("adx", "binance", "BTC/USDT", "1h");
client.addBulkQuery("cmf", "binance", "BTC/USDT", "1h");
client.addBulkQuery("macd", "binance", "BTC/USDT", "1h");
client.addBulkQuery("atr", "binance", "BTC/USDT", "1h", null, 0, "my_custom_id"); // Override id
client.addBulkQuery("rsi", "binance", "BTC/USDT", "1h", null, 5); // RSI 5 hours ago
client.addBulkQuery("ma", "binance", "BTC/USDT", "1h", {optInTimePeriod: 50}); // 50 MA

client.executeBulkQueries().then(result => {
    console.log(result);
}).catch(error => {
    console.log(error);
});
```
The above example will output an array of objects with results, like:


```
[ { id: 'rsi_binance_BTC/USDT_1h',
    indicator: 'rsi',
    result: { value: '47.39194885856546' } },
  { id: 'ao_binance_BTC/USDT_1h',
    indicator: 'ao',
    result: { value: '-85.20858823529852' } },
  { id: 'adx_binance_BTC/USDT_1h',
    indicator: 'adx',
    result: { value: '23.102523989797877' } },
  { id: 'cmf_binance_BTC/USDT_1h',
    indicator: 'cmf',
    result: { value: '-0.10218178589367832' } },
  { id: 'macd_binance_BTC/USDT_1h',
    indicator: 'macd',
    result: 
     { valueMACD: '-23.075277236113834',
       valueMACDSignal: '-18.077522747658332',
       valueMACDHist: '-4.997754488455502' } },
  { id: 'my_custom_id',
    indicator: 'atr',
    result: { value: '107.98685529910053' } },
  { id: 'rsi_binance_BTC/USDT_1h',
    indicator: 'rsi',
    result: { value: '47.499250240679686' } },
  { id: 'ma_binance_BTC/USDT_1h',
    indicator: 'ma',
    result: { value: '10294.615600000003' } } ]
```
Return parameters of the result objects:

- id:The ID of the call. If no ID is provided, an auto-generated id will be returned.
- indicator:The indicator called.
- result:The result of the calculation. This output will depend on the indicator.

The addBulkQuery method will take the following parameters in this order:


## Examples
The next example with fetch the RSI from 3 different exchanges:


```
// Require taapi: npm i taapi --save
const taapi = require("taapi");
 
// Setup client with authentication
const client = taapi.client("MY_SECRET");

// Init bulk queries. This resets all previously added queries
client.initBulkQueries();
 
// Get the BTC/USDT rsi from Binance, Kucoin and Kraken
client.addBulkQuery("rsi", "binance", "BTC/USDT", "1h");
client.addBulkQuery("rsi", "kucoin", "BTC/USDT", "1h");
client.addBulkQuery("rsi", "kraken", "BTC/USDT", "1h");

client.executeBulkQueries().then(result => {
    console.log(result);
}).catch(error => {
    console.log(error);
});
```
Result:


```
[
    {
       "id":"rsi_binance_BTC/USDT_1h",
       "indicator":"rsi",
       "result":{
          "value":"45.96327025055071"
       }
    },
    {
       "id":"rsi_kucoin_BTC/USDT_1h",
       "indicator":"rsi",
       "result":{
          "value":"45.31342328822406"
       }
    },
    {
       "id":"rsi_kraken_BTC/USDT_1h",
       "indicator":"rsi",
       "result":{
          "value":"47.80078228443461"
       }
    }
 ]
```

## Submitting your own candles
While we’re getting the client ready to be able to fetch candles from the Stock market, Forex, etc… , you might possess your own candles.

To calculate indicators based on “raw” candles, use the methods:


```
client.initRawBulkQueries();
```
This initializes and resets all queries.


```
client.setRawCandles(candles);
```
The “raw” candles must be organized the same way as with theConnecting via GraphQLmethod.


```
client.addRawBulkQuery("rsi");
```
The “addRawBulkQuery” method takes the following parameters:

Execute the queries:


```
client.executeRawBulkQueries().then(result => {
    console.log(result);
}).catch(error => {
    console.log(error);
});
```

## Using the ‘Local Server’
3 endpoints in the local server allows you to execute bulk requests using the ‘local server’. See above for how to start this server up.

- /init-bulk-queries:Initiates a new bulk query.
- /add-bulk-query:Add’s a new bulk query, this takes the exact same parameters as querying single indicators.
- /execute-bulk-queries:When all bulk queries are added to the queue, use this to execute the entire queue.

Let’s do one example in PHP using the local server with 5 bulk calls:


```
// Initalize bulk queries
localServerCall('init-bulk-queries');

// Get the RSI 1h BTC/USDT from Binance
localServerCall('add-bulk-query', array(
    'indicator' => 'rsi',
    'exchange' => 'binance',
    'symbol' => 'BTC/USDT',
    'interval' => '1h'
));

// Get the previous RSI 1h BTC/USDT from Binance
localServerCall('add-bulk-query', array(
    'indicator' => 'rsi',
    'exchange' => 'binance',
    'symbol' => 'BTC/USDT',
    'interval' => '1h',
    'backtrack' => 1,
    'id' => 'RSI Binance BTC/USDT 1h backtracked 1'
));

// Get the MACD 1h BTC/USDT from Binance
localServerCall('add-bulk-query', array(
    'indicator' => 'MACD',
    'exchange' => 'binance',
    'symbol' => 'BTC/USDT',
    'interval' => '1h'
));

// Get the RSI 15M BTC/USDT from Binance
localServerCall('add-bulk-query', array(
    'indicator' => 'rsi',
    'exchange' => 'binance',
    'symbol' => 'BTC/USDT',
    'interval' => '15m'
));

// Get the CMF 15m BTC/USDT from Kucoin overriding the period to 20
localServerCall('add-bulk-query', array(
    'indicator' => 'cmf',
    'exchange' => 'kucoin',
    'symbol' => 'BTC/USDT',
    'interval' => '15m',
    'period' => 20
));

// Execute the calls
$bulkResult = localServerCall('execute-bulk-queries');

// View final result
print_r(json_decode($bulkResult));

function localServerCall($endpoint, $queryParameters = array()) {

    $query = http_build_query($queryParameters);
     
    // Define endpoint. Change the port if you changed it when setting up the server
    $url = "http://localhost:4101/{$endpoint}?{$query}";
    
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

    return $output;
}
```
Response:


```
Array
(
    [0] => stdClass Object
        (
            [id] => rsi_binance_BTC/USDT_1h
            [indicator] => rsi
            [result] => stdClass Object
                (
                    [value] => 73.81722586205977
                )

        )

    [1] => stdClass Object
        (
            [id] => RSI Binance BTC/USDT 1h backtracked 1
            [indicator] => rsi
            [result] => stdClass Object
                (
                    [value] => 73.32792355301252
                )

        )

    [2] => stdClass Object
        (
            [id] => MACD_binance_BTC/USDT_1h
            [indicator] => macd
            [result] => stdClass Object
                (
                    [valueMACD] => 27.275057476002075
                    [valueMACDSignal] => -22.129366020718756
                    [valueMACDHist] => 49.40442349672083
                )

        )

    [3] => stdClass Object
        (
            [id] => rsi_binance_BTC/USDT_15m
            [indicator] => rsi
            [result] => stdClass Object
                (
                    [value] => 79.58781974826385
                )

        )

    [4] => stdClass Object
        (
            [id] => cmf_kucoin_BTC/USDT_15m
            [indicator] => cmf
            [result] => stdClass Object
                (
                    [value] => 0.1291445211649513
                )

        )

)
```

## Supported Exchanges
The good folks over atCCXThas done a fantastic job implementing all of these exchanges, for people to query and fetch data in a unified format. Please be sure to shoot them a donation!


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

