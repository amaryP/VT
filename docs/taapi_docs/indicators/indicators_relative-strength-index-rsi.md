# Relative Strength Index (RSI) API – TAAPI.IO

- Home
- News
- Pricing
- Indicators
- Exchanges
- Docs
- Log in
- Free API key

Home–Indicators–Relative Strength Index (RSI)


## Relative Strength Index (RSI)
The Relative Strength Index (RSI) is a popular technical indicator used in financial markets to assess themomentumandstrength of a price trend. We provide the best RSI API available.

Developed by J. Welles Wilder, the RSI is a momentumoscillatorthat measures the speed and change of price movements. It is typically applied to a price chart, oscillating between0and100, with readings above70considered overbought and readings below30indicating oversold conditions. The Relative Strength Index is calculated based on the average gains and losses over a specified period, commonly 14 days. Traders and analysts use this indicator to identifypotential trend reversals, overbought or oversold conditions, and to generate signals for buying or selling assets.

`0` `100` `70` `30` 
## Get RSI values via API
We provide API access the RSI values for all of the most popular assets likestocks, crypto (try one of the popular exchanges likeBinance) and forex, on all commonly used timeframes – from the weekly and daily all the way down to one minute intervals. You can also calculate the values on your own data using ourmanualmethod.


## Customise RSI Sensitivity
When using the RSI technical indicator, you have the ability to fine-tune its responsiveness by adjusting the optionalperiodparameter through the API. This parameter allows you to customize the number of candles considered in the RSI calculation. If you opt for a shorter period, the RSI becomes more sensitive, responding quickly to recent price changes and potentially signaling overbought or oversold conditions sooner. On the other hand, choosing a longer period smoothens the RSI’s output, offering a broader perspective on market trends. This feature lets you tailor the RSI to fit your specific trading strategies and timeframes, enhancing its effectiveness in various market conditions.

`period` 
## Get started for Free
The best way to access RSI is with our Pro and Expert plans.

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


## Get started with thersi
`rsi` Simply make an HTTPS [GET] request or call in your browser:


```

			[GET] https://api.taapi.io/rsi?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1h
		
```

## API response
Thersiendpoint returns a JSON response like this:

`rsi` 
```

			{
  "value": 65.73211579249397
}
		
```

## API parameters
`binance` `binancefutures` `BTC/USDT` `LTC/BTC` `1m` `5m` `15m` `30m` `1h` `2h` `4h` `12h` `1d` `1w` `interval=1h` `interval=1d` `backtrack` `backtrack=5` `0` `chart` `candles` `heikinashi` `candles` `heikinashi` `true` `false` `false` `true` `results` `1685577600` `1731456000` `true` `false` `true` `false` `number` `max` `max` Adjust the RSI length – the number of candles used to calculate the RSI.

Default:14

`14` 
## More examples
Let's say you want to know thersivalue on the last closed candle on the30mtimeframe. You are not interest in the real-time value, so you use thebacktrack=1optional parameter to go back 1 candle in history to the last closed candle.

`rsi` `30m` `backtrack=1` 
```

				[GET] https://api.taapi.io/rsi?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=30m&backtrack=1
			
```
Let's say you want to know what thersidaily value was each day for the previous 10 days. You can get this returned by our API easily and efficiently in one call using theresults=10parameter:

`rsi` `results=10` 
```

				[GET] https://api.taapi.io/rsi?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1d&results=10
			
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

