# Summation via API documentation – TAAPI.IO

- Home
- News
- Pricing
- Indicators
- Exchanges
- Docs
- Log in
- Free API key

Home–Indicators–Summation


## Summation
The summation technical indicator is a tool used in financial analysis to track the cumulative sum of a specified number of closing prices for an asset over a given period. Users can adjust the parameter, denoted by the optionalperiodparameter, to customize the number of closing prices included in the summation.

`period` 
## Get started with thesum
`sum` Simply make an HTTPS [GET] request or call in your browser:


```

			[GET] https://api.taapi.io/sum?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1h
		
```

## API response
Thesumendpoint returns a JSON response like this:

`sum` 
```

			{
  "value": 1116226.1000000015
}
		
```

## API parameters
`binance` `binancefutures` `BTC/USDT` `LTC/BTC` `1m` `5m` `15m` `30m` `1h` `2h` `4h` `12h` `1d` `1w` `interval=1h` `interval=1d` `backtrack` `backtrack=5` `0` `chart` `candles` `heikinashi` `candles` `heikinashi` `true` `false` `false` `true` `results` `1685577600` `1731456000` `true` `false` `true` `false` `number` `max` `max` How many candles to include in the summation.

Default:30

`30` 
## More examples
Let's say you want to know thesumvalue on the last closed candle on the30mtimeframe. You are not interest in the real-time value, so you use thebacktrack=1optional parameter to go back 1 candle in history to the last closed candle.

`sum` `30m` `backtrack=1` 
```

				[GET] https://api.taapi.io/sum?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=30m&backtrack=1
			
```
Let's say you want to know what thesumdaily value was each day for the previous 10 days. You can get this returned by our API easily and efficiently in one call using theresults=10parameter:

`sum` `results=10` 
```

				[GET] https://api.taapi.io/sum?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1d&results=10
			
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

