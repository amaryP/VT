# Average Directional Movement Index Rating via API documentation – TAAPI.IO

- Home
- News
- Pricing
- Indicators
- Exchanges
- Docs
- Log in
- Free API key

Home–Indicators–Average Directional Movement Index Rating


## Average Directional Movement Index Rating
Average Directional Movement Rating quantifies momentum change in the ADX. It is calculated by adding two values of ADX (the current value and a value n periods back), then dividing by two. This additional smoothing makes the ADXR slightly less responsive than ADX. The interpretation is the same as the ADX; the higher the value, the stronger the trend. The ADXR, being a smoothed version of ADX, and can be used similarly to the ADX in the three rule system


## Get started with theadxr
`adxr` Simply make an HTTPS [GET] request or call in your browser:


```

			[GET] https://api.taapi.io/adxr?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1h
		
```

## API response
Theadxrendpoint returns a JSON response like this:

`adxr` 
```

			{
  "value": 39.60132550079786
}
		
```

## API parameters
`binance` `binancefutures` `BTC/USDT` `LTC/BTC` `1m` `5m` `15m` `30m` `1h` `2h` `4h` `12h` `1d` `1w` `interval=1h` `interval=1d` `backtrack` `backtrack=5` `0` `chart` `candles` `heikinashi` `candles` `heikinashi` `true` `false` `false` `true` `results` `1685577600` `1731456000` `true` `false` `true` `false` `number` `max` `max` Sets the number of candles used in the indicator calculation.

Default:14

`14` 
## More examples
Let's say you want to know theadxrvalue on the last closed candle on the30mtimeframe. You are not interest in the real-time value, so you use thebacktrack=1optional parameter to go back 1 candle in history to the last closed candle.

`adxr` `30m` `backtrack=1` 
```

				[GET] https://api.taapi.io/adxr?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=30m&backtrack=1
			
```
Let's say you want to know what theadxrdaily value was each day for the previous 10 days. You can get this returned by our API easily and efficiently in one call using theresults=10parameter:

`adxr` `results=10` 
```

				[GET] https://api.taapi.io/adxr?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1d&results=10
			
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

