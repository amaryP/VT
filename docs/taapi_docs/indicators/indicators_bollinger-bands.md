# Bollinger Bands via API documentation – TAAPI.IO

- Home
- News
- Pricing
- Indicators
- Exchanges
- Docs
- Log in
- Free API key

Home–Indicators–Bollinger Bands


## Bollinger Bands
The Bollinger Bands is a popular technical indicator used by traders in financial markets to analyze price volatility and potential trend reversals. It consists of three lines plotted on a price chart: the upper band, the middle band, and the lower band. The middle band represents the moving average of the underlying asset’s price over a specified period, typically 20 candles (adjustable using the optionalperiodparameter).

`period` The upper and lower bands are placed a certain number of standard deviations away from the middle band, usuallytwostandard deviations. The result returned in the given format provides the numerical values for the upper band, middle band, and lower band.

`two` Traders can use Bollinger Bands to identify periods of low volatility (when the bands arenarrow) and high volatility (when the bandswiden). When the price touches or moves beyond the upper band, it suggests that the asset may beoverbought, potentially indicating a selling opportunity. Conversely, when the price reaches or falls below the lower band, it suggestsoversoldconditions, potentially signalling a buying opportunity. Traders often look for price reversals or bounces off the bands to make trading decisions and assess potential price targets and stop-loss levels.


## Get started for Free
The best way to access Bollinger Bands is with our Pro and Expert plans.

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


## Get Bollinger Bands via API
We provide API access to the Bollinger Bands values for all of the most popular assets likestocks, crypto (try one of the popular exchanges likeBinance) and forex, on all commonly used timeframes – from the weekly and daily all the way down to one minute intervals. You can also calculate the values on your own data using ourmanualmethod.


## Get started with thebbands
`bbands` Simply make an HTTPS [GET] request or call in your browser:


```

			[GET] https://api.taapi.io/bbands?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1h
		
```

## API response
Thebbandsendpoint returns a JSON response like this:

`bbands` 
```

			{
  "valueUpperBand": 23491.10808422158,
  "valueMiddleBand": 21935.281499999997,
  "valueLowerBand": 20379.454915778413
}
		
```

## API parameters
`binance` `binancefutures` `BTC/USDT` `LTC/BTC` `1m` `5m` `15m` `30m` `1h` `2h` `4h` `12h` `1d` `1w` `interval=1h` `interval=1d` `backtrack` `backtrack=5` `0` `chart` `candles` `heikinashi` `candles` `heikinashi` `true` `false` `false` `true` `results` `1685577600` `1731456000` `true` `false` `true` `false` `number` `max` `max` The period length (number of candles) to consider in the data set.

Default:20

`20` Standard deviation value of the price.

Default:2

`2` 
## More examples
Let's say you want to know thebbandsvalue on the last closed candle on the30mtimeframe. You are not interest in the real-time value, so you use thebacktrack=1optional parameter to go back 1 candle in history to the last closed candle.

`bbands` `30m` `backtrack=1` 
```

				[GET] https://api.taapi.io/bbands?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=30m&backtrack=1
			
```
Let's say you want to know what thebbandsdaily value was each day for the previous 10 days. You can get this returned by our API easily and efficiently in one call using theresults=10parameter:

`bbands` `results=10` 
```

				[GET] https://api.taapi.io/bbands?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1d&results=10
			
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

