# Schaff Trend Cycle via API documentation – TAAPI.IO

- Home
- News
- Pricing
- Indicators
- Exchanges
- Docs
- Log in
- Free API key

Home–Indicators–Schaff Trend Cycle


## Schaff Trend Cycle
TheSchaff Trend Cycle (STC)is a faster and more responsive trend indicator than traditional tools like theMACD. Developed by Doug Schaff, STC combinesMACDand thestochastic oscillatorto detect both thedirectionandtimingof market trends with reduced lag.

Unlike classic momentum indicators that often react slowly to trend changes, STC uses a cyclical approach to identify early entry and exit signals — making it especially useful in fast-moving markets like crypto and forex.


## How to use the Schaff Trend Cycle
- STC Rising Above 25–30: May signal the beginning of a bullish trend.
- STC Falling Below 70–75: May suggest a bearish trend is starting.
- Overbought/Oversold Zones: While STC ranges from 0 to 100 like theRSI, its emphasis is more on thecyclerather than fixed thresholds — watch for reversals and crossovers instead.

STC is particularly effective for early detection of trend shifts, making it ideal for dynamic markets with frequent directional changes.


## Customize the STC
You can fine-tune the Schaff Trend Cycle (STC) to better fit your trading strategy by adjusting its optional parameters. ThefastLengthandslowLengthinputs (default:23and50) define the short- and long-term EMAs used in the MACD calculation, influencing the base trend signal’s speed and smoothness. ThecycleLength(default:10) determines how far back the stochastic calculation looks when applying the cyclical smoothing logic, impacting the responsiveness of trend detection. Additionally,d1Lengthandd2Length(both default:3) control the two levels of %D smoothing in the stochastic component — increasing these values smooths out short-term fluctuations, while lower values make the indicator more reactive. Together, these parameters give you granular control over the STC’s sensitivity and precision in identifying trend cycles.

`fastLength` `slowLength` `23` `50` `cycleLength` `10` `d1Length` `d2Length` `3` 
## Get STC values via API
We provide API access to the STC values for all of the most popular assets likestocks, crypto (try one of the popular exchanges likeBinance) and forex, on all commonly used timeframes – from the weekly and daily all the way down to one minute intervals. You can also calculate the values on your own data using ourmanualmethod.


## Get started for Free
The best way to access STC is with our Pro and Expert plans.

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


## Get started with thestc
`stc` Simply make an HTTPS [GET] request or call in your browser:


```

			[GET] https://api.taapi.io/stc?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1h
		
```

## API response
Thestcendpoint returns a JSON response like this:

`stc` 
```

			{
	"value": 92.21,
	"trend": "bullish"
}
		
```

## API parameters
`binance` `binancefutures` `BTC/USDT` `LTC/BTC` `1m` `5m` `15m` `30m` `1h` `2h` `4h` `12h` `1d` `1w` `interval=1h` `interval=1d` `backtrack` `backtrack=5` `0` `chart` `candles` `heikinashi` `candles` `heikinashi` `true` `false` `false` `true` `results` `1685577600` `1731456000` `true` `false` `true` `false` `number` `max` `max` Default:23

`23` Default:50

`50` Default:10

`10` Default:3

`3` Default:3

`3` 
## More examples
Let's say you want to know thestcvalue on the last closed candle on the30mtimeframe. You are not interest in the real-time value, so you use thebacktrack=1optional parameter to go back 1 candle in history to the last closed candle.

`stc` `30m` `backtrack=1` 
```

				[GET] https://api.taapi.io/stc?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=30m&backtrack=1
			
```
Let's say you want to know what thestcdaily value was each day for the previous 10 days. You can get this returned by our API easily and efficiently in one call using theresults=10parameter:

`stc` `results=10` 
```

				[GET] https://api.taapi.io/stc?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1d&results=10
			
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

