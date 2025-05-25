# Pivot points via API documentation – TAAPI.IO

- Home
- News
- Pricing
- Indicators
- Exchanges
- Docs
- Log in
- Free API key

Home–Indicators–Pivot points


## Pivot points
This indicator calculates the traditional pivot points or so called support and resistance levels. For best results, we recommend setting theintervalto1dor1w– this will give you useful daily or weekly support and resistance levels. This indicator matches the “Pivot Points Standard” built-in implementation on Trading View.

`interval` `1d` `1w` 
## Get started with thepivotpoints
`pivotpoints` Simply make an HTTPS [GET] request or call in your browser:


```

			[GET] https://api.taapi.io/pivotpoints?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1h
		
```

## API response
Thepivotpointsendpoint returns a JSON response like this:

`pivotpoints` 
```

			{
  "r3": 37026.763333333336,
  "r2": 35852.596666666665,
  "r1": 33803.333333333336,
  "p": 32629.166666666668,
  "s1": 30579.903333333335,
  "s2": 29405.736666666668,
  "s3": 27356.473333333335
}
		
```

## API parameters
`binance` `binancefutures` `BTC/USDT` `LTC/BTC` `1m` `5m` `15m` `30m` `1h` `2h` `4h` `12h` `1d` `1w` `interval=1h` `interval=1d` `backtrack` `backtrack=5` `0` `chart` `candles` `heikinashi` `candles` `heikinashi` `true` `false` `false` `true` `results` `1685577600` `1731456000` `true` `false` `true` `false` `number` `max` `max` 
## More examples
Let's say you want to know thepivotpointsvalue on the last closed candle on the30mtimeframe. You are not interest in the real-time value, so you use thebacktrack=1optional parameter to go back 1 candle in history to the last closed candle.

`pivotpoints` `30m` `backtrack=1` 
```

				[GET] https://api.taapi.io/pivotpoints?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=30m&backtrack=1
			
```
Let's say you want to know what thepivotpointsdaily value was each day for the previous 10 days. You can get this returned by our API easily and efficiently in one call using theresults=10parameter:

`pivotpoints` `results=10` 
```

				[GET] https://api.taapi.io/pivotpoints?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1d&results=10
			
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

