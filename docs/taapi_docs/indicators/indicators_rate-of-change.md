# Rate of change via API documentation – TAAPI.IO

- Home
- News
- Pricing
- Indicators
- Exchanges
- Docs
- Log in
- Free API key

Home–Indicators–Rate of change


## Rate of change
The Rate of Change (ROC) is a momentum-based technical indicator that measures the percentage change in price between the current price and a price from a specified number of periods ago. It instantly tells you the price difference, helping to identify the strength and direction of a price trend.


## Rate of Change example calculation
Suppose the current price is 120, and the price 10periodsago was 100. This indicates a20%increase over the last10periods. The resulting value you’d get would be:

`periods` 
```
{
	"value": 20
}
```

## Get Rate of Change (ROC) values via API
We provide API access to the ROC values for all of the most popular assets likestocks, crypto (try one of the popular exchanges likeBinance) and forex, on all commonly used timeframes – from the weekly and daily all the way down to one minute intervals. You can also calculate the values on your own data using ourmanualmethod.


## Customize the ROC
To customize the Rate of Change (ROC) indicator, you can adjust theperiodparameter, which defines the number of past periods used in the calculation. A shorter period (e.g., 5 days) makes the ROC more sensitive to recent price changes, providing quicker signals but potentially more noise. Conversely, a longerperiod(e.g., 20 days) smooths out fluctuations, offering a clearer view of long-term trends but potentially slower signals. Adjusting this parameter allows traders to tailor the ROC to their trading style and market conditions, balancing between responsiveness and reliability.

`period` `period` The ROC is a simple yet effective tool for measuring momentum and trend strength.




## Get started with theroc
`roc` Simply make an HTTPS [GET] request or call in your browser:


```

			[GET] https://api.taapi.io/roc?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1h
		
```

## API response
Therocendpoint returns a JSON response like this:

`roc` 
```

			{
  "value": 3.182063053142148
}
		
```

## API parameters
`binance` `binancefutures` `BTC/USDT` `LTC/BTC` `1m` `5m` `15m` `30m` `1h` `2h` `4h` `12h` `1d` `1w` `interval=1h` `interval=1d` `backtrack` `backtrack=5` `0` `chart` `candles` `heikinashi` `candles` `heikinashi` `true` `false` `false` `true` `results` `1685577600` `1731456000` `true` `false` `true` `false` `number` `max` `max` This is the look-back period (e.g., 10 days, hours)

Default:10

`10` 
## More examples
Let's say you want to know therocvalue on the last closed candle on the30mtimeframe. You are not interest in the real-time value, so you use thebacktrack=1optional parameter to go back 1 candle in history to the last closed candle.

`roc` `30m` `backtrack=1` 
```

				[GET] https://api.taapi.io/roc?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=30m&backtrack=1
			
```
Let's say you want to know what therocdaily value was each day for the previous 10 days. You can get this returned by our API easily and efficiently in one call using theresults=10parameter:

`roc` `results=10` 
```

				[GET] https://api.taapi.io/roc?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1d&results=10
			
```
Looking for even more integration examples in different languages like NodeJS, PHP, Python, Curl or Ruby? Continue to[GET] REST - Direct documentation.


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

