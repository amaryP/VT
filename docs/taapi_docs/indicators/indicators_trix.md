# TRIX via API documentation – TAAPI.IO

- Home
- News
- Pricing
- Indicators
- Exchanges
- Docs
- Log in
- Free API key

Home–Indicators–TRIX


## TRIX
The Triple Exponential Average (TRIX) is a momentum indicator used by technical traders that shows the percentage change in a triple exponentially smoothed moving average. When it is applied to triple smoothing of moving averages, it is designed to filter out price movements that are considered insignificant or unimportant.


## Get started with thetrix
`trix` Simply make an HTTPS [GET] request or call in your browser:


```

			[GET] https://api.taapi.io/trix?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1h
		
```

## API response
Thetrixendpoint returns a JSON response like this:

`trix` 
```

			{
  "value": 0.14454281848208517
}
		
```

## API parameters
`binance` `binancefutures` `BTC/USDT` `LTC/BTC` `1m` `5m` `15m` `30m` `1h` `2h` `4h` `12h` `1d` `1w` `interval=1h` `interval=1d` `backtrack` `backtrack=5` `0` `chart` `candles` `heikinashi` `candles` `heikinashi` `true` `false` `false` `true` `results` `1685577600` `1731456000` `true` `false` `true` `false` `number` `max` `max` Default:30

`30` 
## More examples
Let's say you want to know thetrixvalue on the last closed candle on the30mtimeframe. You are not interest in the real-time value, so you use thebacktrack=1optional parameter to go back 1 candle in history to the last closed candle.

`trix` `30m` `backtrack=1` 
```

				[GET] https://api.taapi.io/trix?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=30m&backtrack=1
			
```
Let's say you want to know what thetrixdaily value was each day for the previous 10 days. You can get this returned by our API easily and efficiently in one call using theresults=10parameter:

`trix` `results=10` 
```

				[GET] https://api.taapi.io/trix?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1d&results=10
			
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

