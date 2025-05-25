# Stocks – TAAPI.IO

- Home
- News
- Pricing
- Indicators
- Exchanges
- Docs
- Log in
- Free API key

Home–Documentation–Stocks


## Stocks
Stocks let you query all prices and indicators the same easy way you query crypto. You won’t need to use the parameterexchangeand you will need to include a new parametertype=stocksin your query.

`exchange` `type=stocks` 
## Mandatory Parameters
To query stocks, these parameters are mandatory:

`1m` `5m` `15m` `30m` `1h` `2h` `4h` `12h` `1d` `1w` `interval=1h` `interval=1d` 
## Getting started
To get started with a real-time RSI of APPLE on the hourly timeframe, simply make an HTTPS GET Request or call in your browser:


```
https://api.taapi.io/rsi?symbol=AAPL&interval=1h&type=stocks&secret=APIKEY
```
A JSON Response is returned in this format:


```
{
  "value": 47.22189757843302
}
```

## Which stocks are available?
We currently provide real-time and historical data for all the stocks listed in S&P500, NASDAQ100 and the DJI. That is over 523+ symbols and we’ll be expanding our offering over time. Here’s how to get a full, up-to-date list of all symbols available on our API:


```
https://api.taapi.io/exchange-symbols?secret=APIKEY&type=stocks
```

## Bulk Queries
Bulk queries provide a convenient way of fetching more than one indicator calculation in just one request. Amaximum of 20 calculations is allowedfor every plan, including the free plan.


## Getting started
To get started you must send a POST with a JSON body containing your query, to the endpoint/bulk


```
[POST] https://api.taapi.io/bulk
```

## Query
The query is a simple JSON object, and at the top level you will need to supply your secret token, and below that you define theconstruct. This is also an object defining the basis for the query, specifically, which candle data is needed for the calculations.

`construct` 
```
{
	"secret": "APIKEY",
	"construct": {
		"type": "stocks",
		"symbol": "AAPL",
		"interval": "1d",
		"indicators": [
			{
				"indicator": "price"
			},
			{
				"indicator": "rsi",
				"period": 14
			},
			{
				"indicator": "macd"
			}
		]
	}
}
```
The construct object takes 4 parameters:

- type:stocks
- symbol: Stock ticker, for exampleAAPL.
- interval: Desired time frame. Examples might be15m,1h,1d.
- indicators: This is an array containing the individual queries.

`stocks` `AAPL` `15m` `1h` `1d` Each element in the aboveindicatorsarray, must be an object containing at least one parameter:indicator. This is the name (or endpoint name) of the indicator.

`indicators` `indicator` For each indicator object, you may specify the same parameters as you would with the single queries, simply add them to the indicator object.

Custom IDs are useful so that you can keep track of which indicator call returns which result. By default, the response will show an ID comprised of:


```
<exchange>_<symbol>_<timeframe>_<indicator>_<[parameters]>
```
However if you’d explicitly like to name the ID, simply add anidparameter to the query.

`id` 
## Response

```
{
	"data": [
		{
			"id": "stocks_AAPL_1d_price_0",
			"indicator": "price",
			"result": {
				"value": 179.935
			},
			"errors": []
		},
		{
			"id": "stocks_AAPL_1d_rsi_14_0",
			"indicator": "rsi",
			"result": {
				"value": 34.73886791479826
			},
			"errors": []
		},
		{
			"id": "stocks_AAPL_1d_macd_0",
			"indicator": "macd",
			"result": {
				"valueMACD": -2.096258233871623,
				"valueMACDSignal": 0.0582523714530635,
				"valueMACDHist": -2.1545106053246865
			},
			"errors": []
		}
	]
}
```
For all other integration details, please refer to ourdocumentation on crypto, where all other things and code examples apply.

In case you run in trouble, pleaseget in touch.


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

