# Exponential Moving Average (EMA) via API documentation – TAAPI.IO

- Home
- News
- Pricing
- Indicators
- Exchanges
- Docs
- Log in
- Free API key

Home–Indicators–Exponential Moving Average (EMA)


## Exponential Moving Average (EMA)
The Exponential Moving Average (EMA) is a widely used technical indicator in financial markets that helps to smooth price data and identify trends more effectively.

The EMA, a type of moving average, places greater weight on the most recent price data, making it more responsive to new information compared to theSimple Moving Average (SMA). This characteristic allows traders and analysts to better capture short-term price movements and trends. The Exponential Moving Average is calculated by applying a multiplier to the most recent closing price, adding it to the previous period’s EMA, and then dividing by the total number of periods. Commonly used periods for EMA calculations include12,20, and50days. Traders utilize this indicator to identify trend direction, potential entry and exit points, and to generate trading signals.

`12` `20` `50` 
## Get Exponential Moving Average indicator via API
We provide API access to the EMA values for all of the most popular assets likestocks, crypto (try one of the popular exchanges likeBinance) and forex, on all commonly used timeframes – from the weekly and daily all the way down to one minute intervals. You can also calculate the values on your own data using ourmanualmethod.


## Get started for Free
The best way to access EMA is with our Pro and Expert plans.

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


## Customize EMA Length / Period / Sensitivity
When using the EMA technical indicator, you can fine-tune its responsiveness by adjusting theperiodparameter through our API. This parameter allows you to customize the number of candles considered in the EMA calculation. Choosing a shorter period makes the EMA more sensitive to recent price changes, offering a quick reaction to market movements. Conversely, selecting a longer period smoothens the EMA’s output, providing a more stable view of long-term trends. This feature enables you to tailor the Exponential Moving Average to align with your specific trading strategies and timeframes, thereby enhancing its effectiveness in various market conditions.

`period` 
## Get started with theema
`ema` Simply make an HTTPS [GET] request or call in your browser:


```

			[GET] https://api.taapi.io/ema?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1h
		
```

## API response
Theemaendpoint returns a JSON response like this:

`ema` 
```

			{
  "value": 134.8259211745199
}
		
```

## API parameters
`binance` `binancefutures` `BTC/USDT` `LTC/BTC` `1m` `5m` `15m` `30m` `1h` `2h` `4h` `12h` `1d` `1w` `interval=1h` `interval=1d` `backtrack` `backtrack=5` `0` `chart` `candles` `heikinashi` `candles` `heikinashi` `true` `false` `false` `true` `results` `1685577600` `1731456000` `true` `false` `true` `false` `number` `max` `max` Specify the EMA length – number of candles, from which the EMA will be calculated. Maximum is350.

`350` Please notethat there might not be enough existing candles on a timeframe like theweeklyif the symbol has not been trading for a very long time.

`weekly` Default:30

`30` 
## More examples
Let's say you want to know theemavalue on the last closed candle on the30mtimeframe. You are not interest in the real-time value, so you use thebacktrack=1optional parameter to go back 1 candle in history to the last closed candle.

`ema` `30m` `backtrack=1` 
```

				[GET] https://api.taapi.io/ema?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=30m&backtrack=1
			
```
Let's say you want to know what theemadaily value was each day for the previous 10 days. You can get this returned by our API easily and efficiently in one call using theresults=10parameter:

`ema` `results=10` 
```

				[GET] https://api.taapi.io/ema?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1d&results=10
			
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

