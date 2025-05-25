# Prior Swing High via API documentation – TAAPI.IO

- Home
- News
- Pricing
- Indicators
- Exchanges
- Docs
- Log in
- Free API key

Home–Indicators–Prior Swing High


## Prior Swing High
The Prior Swing High finds the prior highestcloseand highesthigh, within the given period. The default period is14candles and you can adjust it using the optionalperiodparameter.

`close` `high` `14` `period` Use this indicator to easily find the highest value the asset has reached over the past candles.

If you are more interested in finding the min and maxclosevalues (not thehighsandlows), look at theminmaxindicator, which ignores the highs and lows and only takes the closing values into account.

`close` `highs` `lows` 
## Get started with thepriorswinghigh
`priorswinghigh` Simply make an HTTPS [GET] request or call in your browser:


```

			[GET] https://api.taapi.io/priorswinghigh?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1h
		
```

## API response
Thepriorswinghighendpoint returns a JSON response like this:

`priorswinghigh` 
```

			{
  "valueClose": 39600,
  "valueHigh": 40100.02
}
		
```

## API parameters
`binance` `binancefutures` `BTC/USDT` `LTC/BTC` `1m` `5m` `15m` `30m` `1h` `2h` `4h` `12h` `1d` `1w` `interval=1h` `interval=1d` `backtrack` `backtrack=5` `0` `chart` `candles` `heikinashi` `candles` `heikinashi` `true` `false` `false` `true` `results` `1685577600` `1731456000` `true` `false` `true` `false` `number` `max` `max` Default:14

`14` 
## More examples
Let's say you want to know thepriorswinghighvalue on the last closed candle on the30mtimeframe. You are not interest in the real-time value, so you use thebacktrack=1optional parameter to go back 1 candle in history to the last closed candle.

`priorswinghigh` `30m` `backtrack=1` 
```

				[GET] https://api.taapi.io/priorswinghigh?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=30m&backtrack=1
			
```
Let's say you want to know what thepriorswinghighdaily value was each day for the previous 10 days. You can get this returned by our API easily and efficiently in one call using theresults=10parameter:

`priorswinghigh` `results=10` 
```

				[GET] https://api.taapi.io/priorswinghigh?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1d&results=10
			
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

