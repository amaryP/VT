# Relative Vigor Index via API documentation – TAAPI.IO

- Home
- News
- Pricing
- Indicators
- Exchanges
- Docs
- Log in
- Free API key

Home–Indicators–Relative Vigor Index


## Relative Vigor Index
TheRelative Vigor Index (RVGI)is a momentum oscillator that measures the conviction of a recent price move and the likelihood that it will continue. The underlying theory assumes that in bullish markets, prices tend to close higher than they open, and in bearish markets, prices close lower.

RVGI is useful for identifyingtrend direction,potential reversals, andconfirming price movements. It oscillates around a centerline (zero), and crossovers with its signal line can be used to generate buy or sell signals.


## Get RVGI values via API
We provide API access to the Relative Vigor Index values for all of the most popular assets likestocks, crypto (try one of the popular exchanges likeBinance) and forex, on all commonly used timeframes – from the weekly and daily all the way down to one minute intervals. You can also calculate the values on your own data using ourmanualmethod.


## How to Usethe Relative Vigor Index
- Bullish Signal: When the RVGI crosses above the signal line.
- Bearish Signal: When the RVGI crosses below the signal line.
- Divergence: Can be used to detect divergence between price and momentum, similar to RSI or MACD.

RVGI is most effective when used in combination with other indicators such as RSI or moving averages to confirm trends or reversals.


## Get started with thervgi
`rvgi` Simply make an HTTPS [GET] request or call in your browser:


```

			[GET] https://api.taapi.io/rvgi?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1h
		
```

## API response
Thervgiendpoint returns a JSON response like this:

`rvgi` 
```

			{
  "rvgi": 0.35,
  "signal": 0.30,
  "trend": "bullish"
}
		
```

## API parameters
`binance` `binancefutures` `BTC/USDT` `LTC/BTC` `1m` `5m` `15m` `30m` `1h` `2h` `4h` `12h` `1d` `1w` `interval=1h` `interval=1d` `backtrack` `backtrack=5` `0` `chart` `candles` `heikinashi` `candles` `heikinashi` `true` `false` `false` `true` `results` `1685577600` `1731456000` `true` `false` `true` `false` `number` `max` `max` Number of periods to use in the calculation.

Default:10

`10` 
## More examples
Let's say you want to know thervgivalue on the last closed candle on the30mtimeframe. You are not interest in the real-time value, so you use thebacktrack=1optional parameter to go back 1 candle in history to the last closed candle.

`rvgi` `30m` `backtrack=1` 
```

				[GET] https://api.taapi.io/rvgi?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=30m&backtrack=1
			
```
Let's say you want to know what thervgidaily value was each day for the previous 10 days. You can get this returned by our API easily and efficiently in one call using theresults=10parameter:

`rvgi` `results=10` 
```

				[GET] https://api.taapi.io/rvgi?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1d&results=10
			
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

