# Triple Exponential Moving Average via API documentation – TAAPI.IO

- Home
- News
- Pricing
- Indicators
- Exchanges
- Docs
- Log in
- Free API key

Home–Indicators–Triple Exponential Moving Average


## Triple Exponential Moving Average
Triple Exponential Moving Average Technical Indicator (TEMA) was developed by Patrick Mulloy and published in the “Technical Analysis of Stocks & Commodities” magazine. The principle of its calculation is similar to DEMA (Double Exponential Moving Average). The name “Triple Exponential Moving Average” does not very correctly reflect its algorithm. This is a unique blend of the single, double and triple exponential moving average providing the smaller lag than each of them separately. TEMA can be used instead of traditional moving averages. It can be used for smoothing price data, as well as for smoothing other indicators.


## Get started with thetema
`tema` Simply make an HTTPS [GET] request or call in your browser:


```

			[GET] https://api.taapi.io/tema?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1h
		
```

## API response
Thetemaendpoint returns a JSON response like this:

`tema` 
```

			{
  "value": 39905.619251220865
}
		
```

## API parameters
`binance` `binancefutures` `BTC/USDT` `LTC/BTC` `1m` `5m` `15m` `30m` `1h` `2h` `4h` `12h` `1d` `1w` `interval=1h` `interval=1d` `backtrack` `backtrack=5` `0` `chart` `candles` `heikinashi` `candles` `heikinashi` `true` `false` `false` `true` `results` `1685577600` `1731456000` `true` `false` `true` `false` `number` `max` `max` Default:30

`30` 
## More examples
Let's say you want to know thetemavalue on the last closed candle on the30mtimeframe. You are not interest in the real-time value, so you use thebacktrack=1optional parameter to go back 1 candle in history to the last closed candle.

`tema` `30m` `backtrack=1` 
```

				[GET] https://api.taapi.io/tema?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=30m&backtrack=1
			
```
Let's say you want to know what thetemadaily value was each day for the previous 10 days. You can get this returned by our API easily and efficiently in one call using theresults=10parameter:

`tema` `results=10` 
```

				[GET] https://api.taapi.io/tema?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1d&results=10
			
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

