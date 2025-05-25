# Commodity Channel Index via API documentation – TAAPI.IO

- Home
- News
- Pricing
- Indicators
- Exchanges
- Docs
- Log in
- Free API key

Home–Indicators–Commodity Channel Index


## Commodity Channel Index
Developed by Donald Lambert, the Commodity Channel Index​ (CCI) is a momentum-based oscillator used to help determine when an asset is reaching overbought or oversold conditions.

NOTE: This indicator uses the Typical Price as the source of calculation ((hlc3 = High + Low + Close) / 3). Trading View usescloseas the default source, and a period of14. Set thetpparameter tocloseto match Trading View.

`close` `14` `tp` `close` 
## Get started with thecci
`cci` Simply make an HTTPS [GET] request or call in your browser:


```

			[GET] https://api.taapi.io/cci?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1h
		
```

## API response
Thecciendpoint returns a JSON response like this:

`cci` 
```

			{
  "value": 177.65955750772062
}
		
```

## API parameters
`binance` `binancefutures` `BTC/USDT` `LTC/BTC` `1m` `5m` `15m` `30m` `1h` `2h` `4h` `12h` `1d` `1w` `interval=1h` `interval=1d` `backtrack` `backtrack=5` `0` `chart` `candles` `heikinashi` `candles` `heikinashi` `true` `false` `false` `true` `results` `1685577600` `1731456000` `true` `false` `true` `false` `number` `max` `max` Default:14

`14` Default:hlc3

`hlc3` 
## More examples
Let's say you want to know theccivalue on the last closed candle on the30mtimeframe. You are not interest in the real-time value, so you use thebacktrack=1optional parameter to go back 1 candle in history to the last closed candle.

`cci` `30m` `backtrack=1` 
```

				[GET] https://api.taapi.io/cci?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=30m&backtrack=1
			
```
Let's say you want to know what theccidaily value was each day for the previous 10 days. You can get this returned by our API easily and efficiently in one call using theresults=10parameter:

`cci` `results=10` 
```

				[GET] https://api.taapi.io/cci?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1d&results=10
			
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

