# Supertrend indicator via API – TAAPI.IO

- Home
- News
- Pricing
- Indicators
- Exchanges
- Docs
- Log in
- Free API key

Home–Indicators–Supertrend


## Supertrend
TheSupertrendis a popular indicator (and one of the most popularindicatorson our API) used to identify the direction of a trend and potential reversal points. It is based on a combination of price and volatility factors.

The indicator consists of two components: the basic trend and the volatility factor. The basic trend is determined by calculating the average true range (ATR) over a specified period, which reflects market volatility. The volatility factor is then multiplied by a user-definedmultiplier, and the result is added to the average true range for an uptrend or subtracted for a downtrend. The resulting value creates a band above and below the price chart, indicating potential support and resistance levels. When the price crosses above the upper band, it suggests abullish trend, while a cross below the lower band indicates abearish trend.

`multiplier` 
## Fine-tune the Supertrend via API optional parameters
You can customize the Supertrend indicator by utilizing two optional parameters, namelyperiodandmultiplier, enabling you to adjust the indicator according to your trading preferences and market conditions. Theperiodparameter allows you to specify the number of periods used in calculating the average true range (ATR), influencing the responsiveness of Supertrend to recent price changes. Opting for a shorter period enhances reactivity to short-term fluctuations, while a longer period smoothens the indicator, making it more responsive to longer-term trends.

`period` `multiplier` `period` Additionally, themultiplieroptional parameter empowers you to fine-tune the impact of volatility on the Supertrend indicator. A higher multiplier increases sensitivity, widening the bands, while a lower multiplier reduces sensitivity, narrowing the bands. By adjusting these optional parameters based on your strategy and the prevailing market dynamics, you can tailor the indicator to align with your trading style, enhancing its effectiveness in identifying trends and potential reversal points.

`multiplier` The Supertrend indicator is widely used in various financial strategies, includingstocks(weintroducedUS stocks in September 2023), crypto (try one of the popular exchanges likeBinance) or and forex. Traders often use this indicator in conjunction with otherindicatorsto strengthen their overall decision-making process and gain a more comprehensive view of market conditions. Follow the examples below orget startedwith our full documentation.


## Get started for Free
The best way to access Supertrend is with our Pro and Expert plans.

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


## Get started with thesupertrend
`supertrend` Simply make an HTTPS [GET] request or call in your browser:


```

			[GET] https://api.taapi.io/supertrend?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1h
		
```

## API response
Thesupertrendendpoint returns a JSON response like this:

`supertrend` 
```

			{
  "value": 37459.26060042173,
  "valueAdvice": "long"
}
		
```

## API parameters
`binance` `binancefutures` `BTC/USDT` `LTC/BTC` `1m` `5m` `15m` `30m` `1h` `2h` `4h` `12h` `1d` `1w` `interval=1h` `interval=1d` `backtrack` `backtrack=5` `0` `chart` `candles` `heikinashi` `candles` `heikinashi` `true` `false` `false` `true` `results` `1685577600` `1731456000` `true` `false` `true` `false` `number` `max` `max` Default:7

`7` Default:3

`3` 
## More examples
Let's say you want to know thesupertrendvalue on the last closed candle on the30mtimeframe. You are not interest in the real-time value, so you use thebacktrack=1optional parameter to go back 1 candle in history to the last closed candle.

`supertrend` `30m` `backtrack=1` 
```

				[GET] https://api.taapi.io/supertrend?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=30m&backtrack=1
			
```
Let's say you want to know what thesupertrenddaily value was each day for the previous 10 days. You can get this returned by our API easily and efficiently in one call using theresults=10parameter:

`supertrend` `results=10` 
```

				[GET] https://api.taapi.io/supertrend?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1d&results=10
			
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

