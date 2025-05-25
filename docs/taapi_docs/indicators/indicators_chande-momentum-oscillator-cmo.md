# Chande Momentum Oscillator via API documentation – TAAPI.IO

- Home
- News
- Pricing
- Indicators
- Exchanges
- Docs
- Log in
- Free API key

Home–Indicators–Chande Momentum Oscillator


## Chande Momentum Oscillator
The Chande Momentum Oscillator (CMO) is a technical analysis tool used to measure the momentum of price movements in a financial market. It was developed by Donald Dorsey and is designed to identify overbought and oversold conditions in an asset’s price.

Similarly to the popularRSI, CMO is also also used to spot overbought and oversold conditions, as well as to gauge the strength of a trend.


## How to use CMO
1.Overbought and Oversold Conditions:

- CMO value above+50is considered overbought, suggesting that the asset might be overvalued and due for a price correction.
- CMO value below-50is considered oversold, indicating that the asset might be undervalued and due for a price increase.

`+50` `-50` 2.Centerline Crossover:

- When the CMO crosses above the0line, it indicates a potential buying opportunity.
- When the CMO crosses below the0line, it signals a potential selling opportunity.

`0` `0` 3.Trend Confirmation:

- A rising CMO indicates strengthening momentum, suggesting a continuation of the current trend.
- A falling CMO indicates weakening momentum, suggesting a potential reversal or consolidation.


## Get realtime and historical values via API
We provide API access to the Chande Momentum Oscillator values for all of the most popular assets likestocks, crypto (try one of the popular exchanges likeBinance) and forex, on all commonly used timeframes – from the weekly and daily all the way down to one minute intervals. You can also calculate the values on your own data using ourmanualmethod.


## Customise CMO Sensitivity
When using the CMO technical indicator, you have the ability to fine-tune its responsiveness by adjusting the optionalperiodparameter through the API. This parameter allows you to customize the number of candles considered in the CMO calculation. If you opt for a shorter period, the CMO becomes more sensitive, responding quickly to recent price changes and potentially signaling overbought or oversold conditions sooner. On the other hand, choosing a longer period smoothens the CMO’s output, offering a broader perspective on market trends. This feature lets you tailor the CMO to fit your specific trading strategies and timeframes, enhancing its effectiveness in various market conditions.

`period` 
## Get started with thecmo
`cmo` Simply make an HTTPS [GET] request or call in your browser:


```

			[GET] https://api.taapi.io/cmo?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1h
		
```

## API response
Thecmoendpoint returns a JSON response like this:

`cmo` 
```

			{
  "value": 48.115851780097856
}
		
```

## API parameters
`binance` `binancefutures` `BTC/USDT` `LTC/BTC` `1m` `5m` `15m` `30m` `1h` `2h` `4h` `12h` `1d` `1w` `interval=1h` `interval=1d` `backtrack` `backtrack=5` `0` `chart` `candles` `heikinashi` `candles` `heikinashi` `true` `false` `false` `true` `results` `1685577600` `1731456000` `true` `false` `true` `false` `number` `max` `max` Sets the number of candles used in the indicator calculation.

Default:14

`14` 
## More examples
Let's say you want to know thecmovalue on the last closed candle on the30mtimeframe. You are not interest in the real-time value, so you use thebacktrack=1optional parameter to go back 1 candle in history to the last closed candle.

`cmo` `30m` `backtrack=1` 
```

				[GET] https://api.taapi.io/cmo?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=30m&backtrack=1
			
```
Let's say you want to know what thecmodaily value was each day for the previous 10 days. You can get this returned by our API easily and efficiently in one call using theresults=10parameter:

`cmo` `results=10` 
```

				[GET] https://api.taapi.io/cmo?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1d&results=10
			
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

