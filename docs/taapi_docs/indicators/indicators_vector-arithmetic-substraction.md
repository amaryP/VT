# Vector Arithmetic Substraction via API documentation – TAAPI.IO

- Home
- News
- Pricing
- Indicators
- Exchanges
- Docs
- Log in
- Free API key

Home–Indicators–Vector Arithmetic Substraction


## Vector Arithmetic Substraction

## Get started with thesub
`sub` Simply make an HTTPS [GET] request or call in your browser:


```

			[GET] https://api.taapi.io/sub?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1h
		
```

## API response
Thesubendpoint returns a JSON response like this:

`sub` 
```

			[
  {
    "value": 593.5699999999997,
    "backtrack": 0
  },
  {
    "value": 787.8499999999985,
    "backtrack": 1
  },
  {
    "value": 805.3000000000029,
    "backtrack": 2
  },
  {
    "value": 974.5099999999948,
    "backtrack": 3
  },
  {
    "value": 845.7799999999988,
    "backtrack": 4
  },
  {
    "value": 720.1100000000006,
    "backtrack": 5
  },
  {
    "value": 562.5999999999985,
    "backtrack": 6
  },
  {
    "value": 621.3899999999994,
    "backtrack": 7
  },
  {
    "value": 602.0899999999965,
    "backtrack": 8
  },
  {
    "value": 593.4599999999991,
    "backtrack": 9
  }
]
		
```

## API parameters
`binance` `binancefutures` `BTC/USDT` `LTC/BTC` `1m` `5m` `15m` `30m` `1h` `2h` `4h` `12h` `1d` `1w` `interval=1h` `interval=1d` `backtrack` `backtrack=5` `0` `chart` `candles` `heikinashi` `candles` `heikinashi` `true` `false` `false` `true` `results` `1685577600` `1731456000` `true` `false` `true` `false` `number` `max` `max` 
## More examples
Let's say you want to know thesubvalue on the last closed candle on the30mtimeframe. You are not interest in the real-time value, so you use thebacktrack=1optional parameter to go back 1 candle in history to the last closed candle.

`sub` `30m` `backtrack=1` 
```

				[GET] https://api.taapi.io/sub?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=30m&backtrack=1
			
```
Let's say you want to know what thesubdaily value was each day for the previous 10 days. You can get this returned by our API easily and efficiently in one call using theresults=10parameter:

`sub` `results=10` 
```

				[GET] https://api.taapi.io/sub?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1d&results=10
			
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

