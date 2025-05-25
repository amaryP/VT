# Standard Deviation via API – TAAPI.IO

- Home
- News
- Pricing
- Indicators
- Exchanges
- Docs
- Log in
- Free API key

Home–Indicators–Standard Deviation


## Standard Deviation
The standard deviation technical indicator is a statistical measure that quantifies the dispersion of a set of data points from its mean (average). In the context of financial markets, it is often used to assess the volatility or variability of price movements.


## Customize the Standard Deviation
You can personalize the calculation of the standard deviation indicator in two ways. Firstly, you can adjust theperiodparameter, which dictates the number of candles or periods considered in the calculation. A shorter period captures more recent price action, making the indicator more sensitive to changes in volatility, while a longer period smooths out fluctuations and provides a broader view. Secondly, theoptInNbDevparameter acts as a multiplier and defaults to1, but you can tweak it to increase or decrease the sensitivity of the indicator to volatility. A higher multiplier widens the standard deviation bands, indicating greater volatility, while a lower multiplier narrows the bands, suggesting lower volatility. By customizing these parameters, you can tailor the indicator to your specific trading strategies and preferences.

`period` `optInNbDev` `1` 
## Getstddevvalues via API
`stddev` We provide API access to the Standard Deviation values for all of the most popular assets likestocks, crypto (try one of the popular exchanges likeBinance) and forex, on all commonly used timeframes – from the weekly and daily all the way down to one minute intervals. You can also calculate the values on your own data using ourmanualmethod.


## Get started with thestddev
`stddev` Simply make an HTTPS [GET] request or call in your browser:


```

			[GET] https://api.taapi.io/stddev?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1h
		
```

## API response
Thestddevendpoint returns a JSON response like this:

`stddev` 
```

			{
  "value": 235.08268735915223
}
		
```

## API parameters
`binance` `binancefutures` `BTC/USDT` `LTC/BTC` `1m` `5m` `15m` `30m` `1h` `2h` `4h` `12h` `1d` `1w` `interval=1h` `interval=1d` `backtrack` `backtrack=5` `0` `chart` `candles` `heikinashi` `candles` `heikinashi` `true` `false` `false` `true` `results` `1685577600` `1731456000` `true` `false` `true` `false` `number` `max` `max` Number of candles included in the calculation. Similar to the “length” parameter on Trading View.

Default:5

`5` Desired level of deviation, working as a multiplier.

Default:1

`1` 
## More examples
Let's say you want to know thestddevvalue on the last closed candle on the30mtimeframe. You are not interest in the real-time value, so you use thebacktrack=1optional parameter to go back 1 candle in history to the last closed candle.

`stddev` `30m` `backtrack=1` 
```

				[GET] https://api.taapi.io/stddev?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=30m&backtrack=1
			
```
Let's say you want to know what thestddevdaily value was each day for the previous 10 days. You can get this returned by our API easily and efficiently in one call using theresults=10parameter:

`stddev` `results=10` 
```

				[GET] https://api.taapi.io/stddev?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1d&results=10
			
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

