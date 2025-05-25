# Volume via API documentation – TAAPI.IO

- Home
- News
- Pricing
- Indicators
- Exchanges
- Docs
- Log in
- Free API key

Home–Indicators–Volume


## Volume
The Volume Indicator returns the total trading volume for the specified time interval, offering insights into market activity and liquidity.

You can also get the volume using ourcandleendpoint, where the API returns it alongside the other OHLCV (Open,High,Low,Close, andVolume) values.

`Open` `High` `Low` `Close` `Volume` 
## How to Interpret the Volume Results:
Positive Values:A positive value indicates the total volume for the interval where the closing price is higher than the opening price, signifying a green candle. This suggests buying pressure in the market.

Negative Values:A negative value indicates the total volume for the interval where the closing price is lower than the opening price, signifying a red candle. This suggests selling pressure in the market.

We provide fast and easy to use API access to the Volume for all of the most popular assets likestocks, crypto (try one of the popular exchanges likeBinance) and forex, on all commonly used timeframes – from the weekly and daily all the way down to one minute intervals.


## Get started with thevolume
`volume` Simply make an HTTPS [GET] request or call in your browser:


```

			[GET] https://api.taapi.io/volume?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1h
		
```

## API response
Thevolumeendpoint returns a JSON response like this:

`volume` 
```

			{
	"value": 37175.32356
}

// OR 

{
	"value": -7754.654559997343 // Negative value indicates a red candle
}


		
```

## API parameters
`binance` `binancefutures` `BTC/USDT` `LTC/BTC` `1m` `5m` `15m` `30m` `1h` `2h` `4h` `12h` `1d` `1w` `interval=1h` `interval=1d` `backtrack` `backtrack=5` `0` `chart` `candles` `heikinashi` `candles` `heikinashi` `true` `false` `false` `true` `results` `1685577600` `1731456000` `true` `false` `true` `false` `number` `max` `max` 
## More examples
Let's say you want to know thevolumevalue on the last closed candle on the30mtimeframe. You are not interest in the real-time value, so you use thebacktrack=1optional parameter to go back 1 candle in history to the last closed candle.

`volume` `30m` `backtrack=1` 
```

				[GET] https://api.taapi.io/volume?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=30m&backtrack=1
			
```
Let's say you want to know what thevolumedaily value was each day for the previous 10 days. You can get this returned by our API easily and efficiently in one call using theresults=10parameter:

`volume` `results=10` 
```

				[GET] https://api.taapi.io/volume?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1d&results=10
			
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

