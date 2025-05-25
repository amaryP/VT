# Multiple constructs – TAAPI.IO

- Home
- News
- Pricing
- Indicators
- Exchanges
- Docs
- Log in
- Free API key

Home–Documentation–Multiple constructs


## Multiple constructs
Typically,bulk queriesallow you to request multiple indicators for a single pair & timeframe. By usingmultiple constructsin a bulk query, you can request multiple pairs and / or timeframes in a single API query.


## Get started
This feature is only available for subscribers of Pro (3 constructs) and Expert (10 constructs)plans. If you’re already subscribed to one of these plans, you can start using the feature right away, it is already active on your account.


## How it works
Using multiple constructs allows you, for example, to request the RSI forBTC/USDTon1m,15mand1htimeframes in a single API request, like this:

`BTC/USDT` `1m` `15m` `1h` 
```
{
	"secret": "MY_SECRET",
	"construct": [
		{
			"exchange": "binance",
			"symbol": "BTC/USDT",
			"interval": "1m",
			"indicators": [
				{
					"indicator": "rsi",
					"period": 14
				}
			]
		},
		{
			"exchange": "binance",
			"symbol": "BTC/USDT",
			"interval": "15m",
			"indicators": [
				{
					"indicator": "rsi",
					"period": 14
				}
			]
		},
		{
			"exchange": "binance",
			"symbol": "BTC/USDT",
			"interval": "1h",
			"indicators": [
				{
					"indicator": "rsi",
					"period": 14
				}
			]
		}
	]
}
```
You can also request multiple different pairs on different timeframes and query multiple indicators for each of them, all in a single API request:


```
{
	"secret": "MY_SECRET",
	"construct": [
		{
			"exchange": "binance",
			"symbol": "BTC/USDT",
			"interval": "1h",
			"indicators": [
				{
					"indicator": "rsi",
					"period": 14
				},
				{
					"indicator": "supertrend"
				}
			]
		},
		{
			"exchange": "binance",
			"symbol": "ADA/BNB",
			"interval": "15m",
			"indicators": [
				{
					"indicator": "rsi",
					"period": 14
				},
				{
					"indicator": "supertrend"
				}
			]
		}
	]
}
```

## Response

```
{
  "data": [
    {
      "id": "binance_BTC/USDT_1h_rsi_14_0",
      "result": {
        "value": 39.03932026174608
      },
      "errors": []
    },
    {
      "id": "binance_BTC/USDT_1h_supertrend_0",
      "result": {
        "value": 32041.74089369747,
        "valueAdvice": "short"
      },
      "errors": []
    },
    {
      "id": "binance_ADA/BNB_15m_rsi_14_0",
      "result": {
        "value": 61.08648006041372
      },
      "errors": []
    },
    {
      "id": "binance_ADA/BNB_15m_supertrend_0",
      "result": {
        "value": 0.003913688746545626,
        "valueAdvice": "long"
      },
      "errors": []
    }
  ]
}
```

## Arbitrage
Additionally, using this feature, you can also query any indicator on the same pair / timeframe across different exchanges and this way enter the world of arbitrage.


## Limits
Max number of possible constructs we can add to your plan is20. This limit also applies when mixing multiple indicators with multiple constructs. For more information, please refer to therate-limits page.

Let’s say you have 4 constructs allowed on your plan, the maximum number of indicators you can calculate in one request is still 20, but you can split these out on these 4 constructs and calculate 5 indicators on each construct. If you have 10 constructs, you can calculate 2 indicators on each construct.


## That’s it
If you have any questions / comments, as always, please get in touch!


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

