# Lowest and highest values over a specified period via API documentation – TAAPI.IO

- Home
- News
- Pricing
- Indicators
- Exchanges
- Docs
- Log in
- Free API key

Home–Indicators–Lowest and highest values over a specified period


## Lowest and highest values over a specified period
Theminmaxtechnical indicator is used to identify the minimum and maximumclosingprices within a specified set of candles.

`minmax` The closing prices represent the final values of an asset at the end of each trading period, such as a day or an hour. It is important to understand that theminmaxreturns theminandmaxclosevalues, not the lowest low or a highest high, for example.

`minmax` `min` `max` - If you’re looking to find the lowestlowover a specific period, use thepriorswinglow
- If you’re looking to find the highesthighover a specific period, use thepriorswinghigh

`low` `high` - The MinMax indicator focuses on theclosingprices of candles.
- It calculates the minimum (lowest) and maximum (highest)closing priceswithin a specifiedperiod.

`period` - The defaultperiodfor the MinMax indicator is set to30candles. This means it will analyze the closing prices of the last30candles unless otherwise specified.

`period` `30` `30` - If you want to analyze a different number of candles, you can use the optionalperiodparameter. For example, if you want to analyze the last20candles, set theperiodparameter to20.

`period` `20` `period` `20` 
## Get started with theminmax
`minmax` Simply make an HTTPS [GET] request or call in your browser:


```

			[GET] https://api.taapi.io/minmax?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1h
		
```

## API response
Theminmaxendpoint returns a JSON response like this:

`minmax` 
```

			{
  "valueMin": 34260.7,
  "valueMax": 39660.92
}
		
```

## API parameters
`binance` `binancefutures` `BTC/USDT` `LTC/BTC` `1m` `5m` `15m` `30m` `1h` `2h` `4h` `12h` `1d` `1w` `interval=1h` `interval=1d` `backtrack` `backtrack=5` `0` `chart` `candles` `heikinashi` `candles` `heikinashi` `true` `false` `false` `true` `results` `1685577600` `1731456000` `true` `false` `true` `false` `number` `max` `max` Default:30

`30` 
## More examples
Let's say you want to know theminmaxvalue on the last closed candle on the30mtimeframe. You are not interest in the real-time value, so you use thebacktrack=1optional parameter to go back 1 candle in history to the last closed candle.

`minmax` `30m` `backtrack=1` 
```

				[GET] https://api.taapi.io/minmax?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=30m&backtrack=1
			
```
Let's say you want to know what theminmaxdaily value was each day for the previous 10 days. You can get this returned by our API easily and efficiently in one call using theresults=10parameter:

`minmax` `results=10` 
```

				[GET] https://api.taapi.io/minmax?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1d&results=10
			
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

