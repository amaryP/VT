# NPM | NodeJS | TypeScript – TAAPI.IO

- Home
- News
- Pricing
- Indicators
- Exchanges
- Docs
- Log in
- Free API key

Home–Documentation–Utilities / 3rd party integrations–NPM | NodeJS | TypeScript


## NPM | NodeJS | TypeScript
NodeJS is a powerful JavaScript runtime engine built on Chrome’s V8 engine. It’s perfect for building backend apps, and algo-bots run great in this environment.


## Setup
Getting started is easy:

- Create your project
- Install TAAPI.IO
- Fetch some indicators


```
# Create project folder
$ mkdir my-bot-project
$ cd my-bot-project

# Init NodeJS project and install taapi
$ npm init
$ npm install taapi --save

# CommonJS
$ touch index.js # Create a JS index file

# TypeScript
$ touch index.ts # Create a TS index file

# Boot up your IDE
$ code .
```

## CommonJS

```
// Import
import Taapi from 'taapi';

// Or

// Require
const Taapi = require("taapi");

// Init taapi
const taapi = new Taapi.default("<TAAPI_SECRET>");
```

## TypeScript

```
// Import
import Taapi from 'taapi';

// Init taapi
const taapi = new Taapi("<TAAPI_SECRET>");
```

## Default Exchange
All Crypto calls uses the exchange ‘Binance’ as the default exchange. To override this, simply callsetDefaultExchange(exchange: string). Please refer to thislist of exchanges.


```
taapi.setDefaultExchange('bybit');
```

## Set data provider
When calling other asset classes than Crypto, you will need to set your data provider. For Stocks & Forex, Polygon.io is a great provider, and a partner of TAAPI.IO. Please visitPolygon with TAAPI.IOfor more information.

To get setup, simple set your provider credentials like so:


```
taapi.setProvider("polygon", "<POLYGON_SECRET>");
```

## Get an indicator
The ‘getIndicator’ method below will fetch a single indicator, it takes 5 parameters:


## Example
Get theRelative Strength Indexfor Bitcoin to the US Dollar on the 1 hour timeframe from Binance.


```
taapi.getIndicator("rsi", "BTC/USDT", "1h").then( rsi => {
    console.log(rsi);
});
```
If everything is setup correctly, you should get a JSON response with an RSI value:


```
{ value: 43.990028243701715 }
```

## More advanced example
Fetch the last closed 200 periodExponential Moving Averagefor Bitcoin to the Dollar on the 1 hour timeframe from the Bybit exchange.


```
taapi.getIndicator("ema", "BTC/USDT", "1h", { period: 200, backtrack: 1 }, "bybit").then( ema => {
    console.log(ema);
});
```

```
{ value: 17353.488605268773 }
```

## Stocks & Forex with Polygon
Using our integration partner Polygon.io, you can easily fetch indicators from the US Stocks & Forex markets. Visit our guide on how to usePolygon with TAAPI.IOfor detailed explanations.

To set up the client, simple callsetProvider(provider: string, providerSecret: string)

In this example, let’s fetch theMoving Average Convergence Divergence(aka MACD) for AAPL on the daily timeframe with Polygon.io data.


```
// Set provider
taapi.setProvider("polygon", "<POLYGON_SECRET>");

// Get stocks indicator
taapi.getIndicator("macd", "AAPL", "1d", { 
    type: "stocks"
}).then( macd => {
    console.log(macd);
});
```
You should get back a result similar to:


```
{
  valueMACD: -0.8765850243943873,
  valueMACDSignal: -0.5979790654506276,
  valueMACDHist: -0.2786059589437597
}
```

## Bulk
Bulk requests allows you to calculate up to 20 indicators in just 1 request. If your taapi plan allows it, you can even call multiple constructs in one request. Executing bulk requests is very easy. For more information on bulk calls, please visit theDirect Methodguide. There are 3 methods available:

This will reset the bulk request and make it ready for a new run.

Adds an indicator calculation to the construct stack. The below parameters are available.

Execute the bulk request. This method takes 1 parameter, the type of asset. This type may be 1 of 3: [“crypto”, “stocks”, “forex”].


## Crypto
Execute a Crypto bulk request. Type defaults to ‘crypto’.

Example


```
// Reset
taapi.resetBulkConstructs();

// Add calculations
taapi.addCalculation("rsi", "BTC/USDT", "1h", "rsi_1h");
taapi.addCalculation("macd", "BTC/USDT", "1h", "macd_1h");
taapi.addCalculation("ema", "BTC/USDT", "1h", "ema_fast_1h", { period: 9, backtrack: 1 });
taapi.addCalculation("ema", "BTC/USDT", "1h", "ema_slow_1h", { period: 20, backtrack: 1 });

// Execute Crypto request
taapi.executeBulk().then( results => {
    console.log(results);
}).catch( error => {
    console.error(error)
});
```

```
{
  rsi_1h: { value: 45.93741130322255 },
  macd_1h: {
    valueMACD: -33.07013069050299,
    valueMACDSignal: -14.928824755120573,
    valueMACDHist: -18.14130593538242
  },
  ema_fast_1h: { value: 17702.837602780794 },
  ema_slow_1h: { value: 17750.77062351824 }
}
```
To get indicators from other exchanges, override the default with setDefaultExchange(exchange: string) or, use:


```
taapi.addCalculation("rsi", "BTC/USDT", "1d", "rsi_bybit_1d", {}, "bybit");
taapi.addCalculation("ema", "BTC/USDT", "1d", "ema_bybit_1d", {}, "bybit");
```

## Stocks & Forex with Polygon
Using our integration partner Polygon.io, you can easily fetch indicators from the US Stocks & Forex markets. Visit our guide on how to usePolygon with TAAPI.IOfor detailed explanations.

To set up the client, simple callsetProvider(provider: string, providerSecret: string)


```
// Reset
taapi.resetBulkConstructs();

// Set data provider
taapi.setProvider("polygon", "<POLYGON_SECRET>");

// Add calculations
taapi.addCalculation("rsi", "AAPL", "1d", "rsi_aapl_1d");
taapi.addCalculation("macd", "AAPL", "1d", "macd_aapl_1d");
taapi.addCalculation("ema", "AAPL", "1d", "ema_fast_aapl_1d", { period: 9 });
taapi.addCalculation("ema", "AAPL", "1d", "ema_slow_aapl_1d", { period: 20 });

// Execute
taapi.executeBulk("stocks").then( results => {
    console.log(results);
}).catch( error => {
    console.log(error.response.data)
});
```

```
{
  rsi_aapl_1d: { value: 35.83106079697871 },
  macd_aapl_1d: {
    valueMACD: -2.0493879295945874,
    valueMACDSignal: -1.025298283156676,
    valueMACDHist: -1.0240896464379114
  },
  ema_fast_aapl_1d: { value: 141.05776405055647 },
  ema_slow_aapl_1d: { value: 143.38011018513154 }
}
```

## executeBulk(“forex”)
Same thing goes for Forex


```
// Reset
taapi.resetBulkConstructs();

// Set data provider
taapi.setProvider("polygon", "<POLYGON_SECRET>");

// Add calculations
taapi.addCalculation("rsi", "EUR/USD", "1d", "rsi_eurodollar_1d");
taapi.addCalculation("macd", "EUR/USD", "1d", "macd_eurodollar_1d");
taapi.addCalculation("ema", "EUR/USD", "1d", "ema_fast_eurodollar_1d", { period: 9 });
taapi.addCalculation("ema", "EUR/USD", "1d", "ema_slow_eurodollar_1d", { period: 20 });

taapi.executeBulk("forex").then( results => {
    console.log(results);
}).catch( error => {
    console.log(error.response.data)
});
```
Note. With Forex calls, symbols are always separated in base and quote assets with a forward slash (/), and in all uppercase: BASE/QUOTE.


## Mutiple constructs
If your TAAPI.IO plan supports multiple constructs, you can even mix things up. This call will use 4 constructs.


```
taapi.addCalculation("rsi", "BTC/USDT", "1h", "bitcoin_rsi_binance_1h"); // Bitcoin RSI on the hourly from Binance
taapi.addCalculation("ema", "BTC/USDT", "1d", "bitcoin_ema_binance_1d", { period: 20, backtrack: 1}); // Bitcoin yesterdays EMA on the daily from Binance
taapi.addCalculation("rsi", "XRP/USDT", "1d", "ripple_rsi_binance_1d"); // Ripple RSI on the daily from Binance
taapi.addCalculation("macd", "BTC/USDT", "1h", "bitcoin_macd_bybit_1h", {}, "bybit"); // Bitcoin MACD on the hourly from Bybit
taapi.addCalculation("ema", "BTC/USDT", "1d", "bitcoin_ema_bybit_1d", {}, "bybit"); // Bitcoin EMA on the daily from Bybit
```

```
{
  bitcoin_rsi_binance_1h: { value: 31.138386432464028 },
  bitcoin_ema_binance_1d: { value: 17176.752098777255 },
  ripple_rsi_binance_1d: { value: 44.52095009510708 },
  bitcoin_macd_bybit_1h: {
    valueMACD: -85.06838471327501,
    valueMACDSignal: -48.551737955150664,
    valueMACDHist: -36.516646758124345
  },
  bitcoin_ema_bybit_1d: { value: 17284.707768052504 }
}
```

## Discover Symbols
With this client, you can get an up-to-date list of symbols that are being traded on an exchange. For this you will need to use the ‘getExchangeSymbols’ method. This method takes 3 arguments.

Below is an example of how to get all USDT pairs traded on Binance:


```
taapi.getExchangeSymbols("crypto", "binance", "USDT").then( symbols => {
    console.log(symbols);
});
```

## Stocks & Forex
To get symbols for Stocks & Forex, you’ll also need to provide the ‘provider’ and ‘providerSecret’. You do that with the same setProvider method as described above. Below a couple ofPolygon.ioexamples

Get stocks symbols from the New York Stock Exchange. For a list of Stock Exchanges and their Market Identifier Codes, please visit:https://www.iso20022.org/market-identifier-codes


```
// Get all symbols traded on the New York Stock Exchange
taapi.setProvider("polygon", "<POLYGON_SECRET>");

taapi.getExchangeSymbols("stocks", "XNYS").then( symbols => {
    console.log(symbols);
});
```
Forex with Polygon does not come from a specific exchange, thus the exchange parameter is not required. Below a couple of examples on how to query Forex symbols.


```
// Get all forex pairs
taapi.setProvider("polygon", "<POLYGON_SECRET>");

taapi.getExchangeSymbols("forex").then( symbols => {
    console.log(symbols);
});

// Get all USD forex pairs
taapi.setProvider("polygon", "<POLYGON_SECRET>");

taapi.getExchangeSymbols("forex", "", "USD").then( symbols => {
    console.log(symbols);
});
```

## That’s it!
Hopefully this should get you going with ease using NodeJS. Suggestions, improvements, comments always welcome!

Happy trading!


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

