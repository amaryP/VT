# Choppiness Index (CHOP) via API documentation – TAAPI.IO

- Home
- News
- Pricing
- Indicators
- Exchanges
- Docs
- Log in
- Free API key

Home–Indicators–Choppiness Index (CHOP)


## Choppiness Index (CHOP)
The Choppiness Index (CHOP) is an indicator designed to determine if the market is choppy (trading sideways) or not choppy (trading within a trend in either direction). The Choppiness Index is an example of an indicator that is not directional at all. CHOP is not meant to predict future market direction, it is a metric to be used to for defining the market’s trendiness only. A basic understanding of the indicator would be; higher values equal more choppiness, while lower values indicate directional trending.


## Get started with thechop
`chop` Simply make an HTTPS [GET] request or call in your browser:


```

			[GET] https://api.taapi.io/chop?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1h
		
```

## API response
Thechopendpoint returns a JSON response like this:

`chop` 
```

			{
  "value": 41.48673369961489
}
		
```

## API parameters
`binance` `binancefutures` `BTC/USDT` `LTC/BTC` `1m` `5m` `15m` `30m` `1h` `2h` `4h` `12h` `1d` `1w` `interval=1h` `interval=1d` `backtrack` `backtrack=5` `0` `chart` `candles` `heikinashi` `candles` `heikinashi` `true` `false` `false` `true` `results` `1685577600` `1731456000` `true` `false` `true` `false` `number` `max` `max` Default:14

`14` 
## More examples
Let's say you want to know thechopvalue on the last closed candle on the30mtimeframe. You are not interest in the real-time value, so you use thebacktrack=1optional parameter to go back 1 candle in history to the last closed candle.

`chop` `30m` `backtrack=1` 
```

				[GET] https://api.taapi.io/chop?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=30m&backtrack=1
			
```
Let's say you want to know what thechopdaily value was each day for the previous 10 days. You can get this returned by our API easily and efficiently in one call using theresults=10parameter:

`chop` `results=10` 
```

				[GET] https://api.taapi.io/chop?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1d&results=10
			
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

