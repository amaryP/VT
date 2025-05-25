# Tom Demark Sequential indicator via API – TAAPI.IO

- Home
- News
- Pricing
- Indicators
- Exchanges
- Docs
- Log in
- Free API key

Home–Indicators–Sequential Countdown Indicator (based on DeMark Methodology)


## Sequential Countdown Indicator (based on DeMark Methodology)
TheSequential Countdown Indicatoris a technical analysis tool designed to identify potential reversal points in financial markets such as stocks and cryptocurrencies. It is inspired by the methodology developed byTom DeMark, a well-known trader and analyst, and is among the most popular tools used for detecting trend exhaustion.

This indicator uses a systematic approach to analyze price movements and highlight areas where trends may be weakening. By identifying moments of possible market turning points through a structured countdown process, it provides actionable signals for traders looking to optimize their entry and exit points. When used correctly, the Sequential Countdown Indicator can improve trading precision and risk management by flagging potential reversals before they occur.


## Visualizing the Sequential Countdown Indicator
The indicator works through a series of numbered and color-coded countdown phases, usually ranging from1to13. These phases are based on specific price action rules comparing current and past price bars. A completed countdown phase may indicate that an asset has reached a zone of excessive buying or selling pressure, suggesting a potential pause or reversal in the prevailing trend.

`1` `13` TAAPI.IO provides real-time access to theSequential Countdown Indicatorfor popular crypto trading pairs (e.g., onBinance) and majorUS stocks. Use our real-time data to power your live strategies or rely on our historical data for robust backtesting. This indicator can be integrated into your algorithmic strategies or used manually for discretionary trading decisions.

Note:This indicator is based on the methodology originally introduced byTom DeMark. “TD Sequential” is a registered trademark of Market Studies, LLC. TAAPI.IO is not affiliated with or endorsed by Tom DeMark or Market Studies, LLC, and this implementation is an independent interpretation of the publicly known methodology.




## Get started for Free
The best way to access theSequential Countdown Indicatoris with our Pro and Expert plans.

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


## Get started with thetdsequential
`tdsequential` Simply make an HTTPS [GET] request or call in your browser:


```

			[GET] https://api.taapi.io/tdsequential?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1h
		
```

## API response
Thetdsequentialendpoint returns a JSON response like this:

`tdsequential` 
```

			{
  "buySetupIndex": 1,
  "sellSetupIndex": 0,
  "buyCoundownIndex": 0,
  "sellCoundownIndex": 7,
  "countdownIndexIsEqualToPreviousElement": true,
  "sellSetup": true,
  "buySetup": false,
  "sellSetupPerfection": true,
  "buySetupPerfection": false,
  "bearishFlip": true,
  "bullishFlip": false,
  "TDSTBuy": 41950,
  "TDSTSell": 0,
  "countdownResetForTDST": false
}
		
```

## API parameters
`binance` `binancefutures` `BTC/USDT` `LTC/BTC` `1m` `5m` `15m` `30m` `1h` `2h` `4h` `12h` `1d` `1w` `interval=1h` `interval=1d` `backtrack` `backtrack=5` `0` `chart` `candles` `heikinashi` `candles` `heikinashi` `true` `false` `false` `true` `results` `1685577600` `1731456000` `true` `false` `true` `false` `number` `max` `max` 
## More examples
Let's say you want to know thetdsequentialvalue on the last closed candle on the30mtimeframe. You are not interest in the real-time value, so you use thebacktrack=1optional parameter to go back 1 candle in history to the last closed candle.

`tdsequential` `30m` `backtrack=1` 
```

				[GET] https://api.taapi.io/tdsequential?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=30m&backtrack=1
			
```
Let's say you want to know what thetdsequentialdaily value was each day for the previous 10 days. You can get this returned by our API easily and efficiently in one call using theresults=10parameter:

`tdsequential` `results=10` 
```

				[GET] https://api.taapi.io/tdsequential?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1d&results=10
			
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

