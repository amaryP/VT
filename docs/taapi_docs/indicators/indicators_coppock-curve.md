# Coppock Curve via API documentation – TAAPI.IO

- Home
- News
- Pricing
- Indicators
- Exchanges
- Docs
- Log in
- Free API key

Home–Indicators–Coppock Curve


## Coppock Curve
The Coppock Curve is a long-term price momentum indicator and oscillator, used mostly to find downturns and upturns. When the returned Coppock Curve value is above zero it tells you to hold. When the Coppock Curve drops below zero it is a sell signal and when the indicator value goes above zero, it is a buy signal.


## Get started with thecoppockcurve
`coppockcurve` Simply make an HTTPS [GET] request or call in your browser:


```

			[GET] https://api.taapi.io/coppockcurve?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1h
		
```

## API response
Thecoppockcurveendpoint returns a JSON response like this:

`coppockcurve` 
```

			{
  "value": -3.911490177036377
}
		
```

## API parameters
`binance` `binancefutures` `BTC/USDT` `LTC/BTC` `1m` `5m` `15m` `30m` `1h` `2h` `4h` `12h` `1d` `1w` `interval=1h` `interval=1d` `backtrack` `backtrack=5` `0` `chart` `candles` `heikinashi` `candles` `heikinashi` `true` `false` `false` `true` `results` `1685577600` `1731456000` `true` `false` `true` `false` `number` `max` `max` Default:10

`10` Default:14

`14` Default:11

`11` 
## More examples
Let's say you want to know thecoppockcurvevalue on the last closed candle on the30mtimeframe. You are not interest in the real-time value, so you use thebacktrack=1optional parameter to go back 1 candle in history to the last closed candle.

`coppockcurve` `30m` `backtrack=1` 
```

				[GET] https://api.taapi.io/coppockcurve?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=30m&backtrack=1
			
```
Let's say you want to know what thecoppockcurvedaily value was each day for the previous 10 days. You can get this returned by our API easily and efficiently in one call using theresults=10parameter:

`coppockcurve` `results=10` 
```

				[GET] https://api.taapi.io/coppockcurve?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1d&results=10
			
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

