# Stocks and Forex – TAAPI.IO

- Home
- News
- Pricing
- Indicators
- Exchanges
- Docs
- Log in
- Free API key

Home–Documentation–Utilities / 3rd party integrations–Stocks and Forex


## Stocks and Forex
Polygon.iois a data provider for Stocks, Forex and Options. TAAPI.IO is a proud partner of Polygon and can vouch for their data quality.  With this integration, our customers will be able to get all our technical analysis indicators on forex and US stocks in real time.


## Technical analysis on all US stocks and forex currency pairs via API, in real-time
To get started with stocks and forex, you’ll need to be signed up as a Polygon user and have a their API key. Please visit:https://polygon.io/pricingand select a plan based on your needs. Polygon offers a free key to help you test out the integration and make sure all is working well.

After signing up, you’ll receive your API Key  in their backend console. Simply follow the email instructions sent to you.

Then use your newly acquired key with your TAAPI.IO queries. This will work with all direct integration methods described in theDirect Integration Method, including Bulk calls (see below).

We will then, on behalf of you, request the needed candles for your indicator calculation from Polygon, calculate your indicator(s), and return the result to you.


## Query
Following the direction in theDirect Method, all you need to do is add a few parameters to your query:

`forex` `stocks` `options` `true` `false` `false` `1m` `5m` `15m` `30m` `1h` `2h` `4h` `12h` `1d` `1w` `interval=1h` `interval=1d` Note, some parameters are no longer needed:

- exchange: This is irrelevant as data comes in aggregated from Polygon, from multiple exchanges.


## Bulk
Bulk queries will work as well, essentially making these calls very efficient, as you can calculate up to 20 indicators, off of the same candle set. If you have multiple constructs included in your plan, this will work efficiently too, as we’re calling each dataset asynchronously from Polygon.


```
{
	"secret": "{{ secret }}",
	"construct": {
		"type": "stocks",
		"provider": "polygon",
		"providerSecret": "{{ polygon_secret }}",
		"symbol": "AAPL",
		"interval": "1d",
		"indicators": [
			{
				"indicator": "ema",
				"period": 20
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

## Endpoint
In order to get as physically close as possible to Polygons servers, we’ve created a new endpoint, that you’ll need to send your queries to:https://us-east.taapi.io.

It will not be possible to call our main endpoint (api.taapi.io)


## Example
As a very simple example, please try this AAPL RSI query on the daily timeframe:


```
curl https://us-east.taapi.io/rsi?secret=MY_TAAPI_SECRET&type=stocks&provider=polygon&providerSecret=MY_POLYGON_SECRET&symbol=AAPL&interval=1d
```

## Discover Symbols
To get an up-to-date list of symbols that can be traded, send a GET request to: /exchange-symbols

For this Polygon integration, a few extra parameters are required.


```
curl https://us-east.taapi.io/exchange-symbols?secret=MY_TAAPI_SECRET&type=stocks&exchange=XNYS&provider=polygon&providerSecret=MY_POLYGON_SECRET
```
For a list of Stock Exchanges and their Market Identifier Codes, please visit:

https://www.iso20022.org/market-identifier-codes

- XNYS: New York Stock Exchange
- XNAS: Nasdaq


## Conclusion
That’s it! Enjoy stocks, forex and options technical analysis! As always, comments and questions are welcome. Happy trading!


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

