# StochRSI - Stochastic Relative Strength Index via API – TAAPI.IO

- Home
- News
- Pricing
- Indicators
- Exchanges
- Docs
- Log in
- Free API key

Home–Indicators–StochRSI – Stochastic Relative Strength Index


## StochRSI – Stochastic Relative Strength Index
The Stochastic RSI (StochRSI) is a technical indicator that combines elements of two well-known indicators, theRelative Strength Index (RSI)and theStochasticoscillator. It’s designed to provide you with a more sensitive and dynamic representation of price momentum.


## Range of the indicator
The StochRSI oscillates between0and100, like the traditional RSI, but it incorporates the principles of the Stochastic Oscillator by comparing the RSI’s position within its range. Similar to theRSI, readings above70suggest overbought conditions, while readings below30indicate oversold conditions. You can use the indicator to identify potential trend reversals, pinpointing areas where an asset may be more likely to change direction. This indicator is particularly useful in volatile markets, offering you a more responsive signal to changes in price momentum. Like the RSI, the StochRSI helps you evaluate the strength or weakness of a trend.

`0` `100` `70` `30` 
## StochRSI for stocks and crypto via API
We provide API access to the StochRSI values for all of the most popular assets likestocks, crypto (try one of the popular exchanges likeBinance) and forex, on all commonly used timeframes – from the weekly and daily all the way down to one minute intervals. You can also calculate the values on your own data using ourmanualmethod.


## Get started for Free
The best way to access StochRSI is with our Pro and Expert plans.

Test drive TAAPI.IO risk-free with our 7-day free trial on all paid plans! We’re confident you’ll be hooked for the long haul!

- Sale!ProFrom:€14.99/ month with a 7-day free trialMost popularAll indicators150.000 calls / dayUS Stocks – real-time and historicalIndexes (SPY, QQQ, DJIA)Crypto data real-time and historical3 symbols per API requestHistorical data 300 candles back usingresultsPriority support7-day Free TrialSelect options
- All indicators
- 150.000 calls / day
- US Stocks – real-time and historical
- Indexes (SPY, QQQ, DJIA)
- Crypto data real-time and historical
- 3 symbols per API request
- Historical data 300 candles back usingresults
- Priority support
- Sale!ExpertFrom:€29.99/ monthAll indicators400.000 calls / dayUS Stocks – real-time and historicalIndexes (SPY, QQQ, DJIA)Crypto data real-time and historical10 symbols per API requestHistorical data 2000 candles back usingresultsPriority supportSelect options
- All indicators
- 400.000 calls / day
- US Stocks – real-time and historical
- Indexes (SPY, QQQ, DJIA)
- Crypto data real-time and historical
- 10 symbols per API request
- Historical data 2000 candles back usingresults
- Priority support

From:€14.99/ month with a 7-day free trialMost popular

- All indicators
- 150.000 calls / day
- US Stocks – real-time and historical
- Indexes (SPY, QQQ, DJIA)
- Crypto data real-time and historical
- 3 symbols per API request
- Historical data 300 candles back usingresults
- Priority support

7-day Free Trial

From:€29.99/ monthAll indicators400.000 calls / dayUS Stocks – real-time and historicalIndexes (SPY, QQQ, DJIA)Crypto data real-time and historical10 symbols per API requestHistorical data 2000 candles back usingresultsPriority supportSelect options

- All indicators
- 400.000 calls / day
- US Stocks – real-time and historical
- Indexes (SPY, QQQ, DJIA)
- Crypto data real-time and historical
- 10 symbols per API request
- Historical data 2000 candles back usingresults
- Priority support


## Get started with thestochrsi
`stochrsi` Simply make an HTTPS [GET] request or call in your browser:


```

			[GET] https://api.taapi.io/stochrsi?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1h
		
```

## API response
Thestochrsiendpoint returns a JSON response like this:

`stochrsi` 
```

			{
  "valueFastK": 28.157463220370534,
  "valueFastD": 52.369851780456976
}
		
```

## API parameters
`binance` `binancefutures` `BTC/USDT` `LTC/BTC` `1m` `5m` `15m` `30m` `1h` `2h` `4h` `12h` `1d` `1w` `interval=1h` `interval=1d` `backtrack` `backtrack=5` `0` `chart` `candles` `heikinashi` `candles` `heikinashi` `true` `false` `false` `true` `results` `1685577600` `1731456000` `true` `false` `true` `false` `number` `max` `max` Default:5

`5` Default:3

`3` Default:14

`14` Default:14

`14` 
## More examples
Let's say you want to know thestochrsivalue on the last closed candle on the30mtimeframe. You are not interest in the real-time value, so you use thebacktrack=1optional parameter to go back 1 candle in history to the last closed candle.

`stochrsi` `30m` `backtrack=1` 
```

				[GET] https://api.taapi.io/stochrsi?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=30m&backtrack=1
			
```
Let's say you want to know what thestochrsidaily value was each day for the previous 10 days. You can get this returned by our API easily and efficiently in one call using theresults=10parameter:

`stochrsi` `results=10` 
```

				[GET] https://api.taapi.io/stochrsi?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1d&results=10
			
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

