# Ease of Movement via API documentation – TAAPI.IO

- Home
- News
- Pricing
- Indicators
- Exchanges
- Docs
- Log in
- Free API key

Home–Indicators–Ease of Movement


## Ease of Movement
The Ease of Movement (EOM) indicator is a technical analysis tool that helps traders identify the relationship between price changes and trading volumes. It was developed by Richard W. Arms Jr. and is used to determine the ease or difficulty of a price movement. The EOM indicator takes into account both price change and volume in its calculation.

Traders use the Ease of Movement (EOM) indicator in the following ways:

- Trend Reversals: EOM crossing above or below zero can indicate potential trend reversals, signaling buying or selling pressure.
- Confirming Breakouts: EOM can validate price breakouts, especially if accompanied by a surge in the indicator.
- Divergence Analysis: Divergences between EOM and price can suggest trend changes, such as bullish or bearish reversals.
- Price-Volume Relationship: EOM helps assess the strength of price movements by considering volume alongside price changes.


## Get started with theeom
`eom` Simply make an HTTPS [GET] request or call in your browser:


```

			[GET] https://api.taapi.io/eom?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1h
		
```

## API response
Theeomendpoint returns a JSON response like this:

`eom` 
```

			{
	"value": 6015.310333087231
}
		
```

## API parameters
`binance` `binancefutures` `BTC/USDT` `LTC/BTC` `1m` `5m` `15m` `30m` `1h` `2h` `4h` `12h` `1d` `1w` `interval=1h` `interval=1d` `backtrack` `backtrack=5` `0` `chart` `candles` `heikinashi` `candles` `heikinashi` `true` `false` `false` `true` `results` `1685577600` `1731456000` `true` `false` `true` `false` `number` `max` `max` Default:14

`14` Default:10000

`10000` 
## More examples
Let's say you want to know theeomvalue on the last closed candle on the30mtimeframe. You are not interest in the real-time value, so you use thebacktrack=1optional parameter to go back 1 candle in history to the last closed candle.

`eom` `30m` `backtrack=1` 
```

				[GET] https://api.taapi.io/eom?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=30m&backtrack=1
			
```
Let's say you want to know what theeomdaily value was each day for the previous 10 days. You can get this returned by our API easily and efficiently in one call using theresults=10parameter:

`eom` `results=10` 
```

				[GET] https://api.taapi.io/eom?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1d&results=10
			
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

