# Williams Alligator via API – TAAPI.IO

- Home
- News
- Pricing
- Indicators
- Exchanges
- Docs
- Log in
- Free API key

Home–Indicators–Williams Alligator


## Williams Alligator
The Williams Alligator is a technical analysis tool developed by trader and author Bill Williams. It’s designed to help traders identify trends and potential trading opportunities in the market. The indicator is composed of three smoothed moving averages that are meant to resemble the teeth, lips, and jaws of an alligator, hence the name.

Here’s a breakdown of the components:

- Alligator’s Jaw (Blue line): This is a13-periodsmoothed moving average that is moved8bars into the future.
- Alligator’s Teeth (Red line): This is an8-periodsmoothed moving average that is moved5bars into the future.
- Alligator’s Lips (Green line): This is a5-periodsmoothed moving average that is moved3bars into the future.

`13-period` `8` `8-period` `5` `5-period` `3` When the Alligator indicator is applied to a chart, traders look for the following signals:

- Trending Upward: When the three lines are intertwined, with the green line (Lips) at the top, the red line (Teeth) in the middle, and the blue line (Jaw) at the bottom, this indicates that the market is in a strong upward trend. Traders might look for buying opportunities.
- Trending Downward: Conversely, when the three lines are intertwined, but with the green line (Lips) at the bottom, the red line (Teeth) in the middle, and the blue line (Jaw) at the top, this suggests a strong downward trend. Traders might look for selling opportunities.
- Consolidation/Range-bound: When the Alligator’s lines are close together and intertwined, this suggests the market is ranging or consolidating, and traders might avoid entering new positions until a clear trend emerges.
- Awake/Asleep: Bill Williams also described the Alligator as “asleep” when the three lines are close together and flat. Conversely, when the Alligator is “awake,” the three lines diverge, indicating an emerging trend.


## Customise the indicator
With our API, traders have the flexibility to customize the Williams Alligator indicator according to their preferences and trading strategies usingoptional parameters. This includes adjusting the length of each of the moving averages (Jaw, Teeth, and Lips) to suit different timeframes and market conditions. For example, traders can experiment with longer or shorter periods for the moving averages based on the volatility of the asset being analyzed. Additionally, traders can modify the offset by specifying how many bars into the future each line is moved. These adjustments are possible using the optional parameters described in the table below.


## Get Williams Alligator values via API
We provide API access to the Williams Alligator values for all of the most popular assets likestocks, crypto (try one of the popular exchanges likeBinance) and forex, on all commonly used timeframes – from the weekly and daily all the way down to one minute intervals. You can also calculate the values on your own data using ourmanualmethod.


## Get started with thewilliamsalligator
`williamsalligator` Simply make an HTTPS [GET] request or call in your browser:


```

			[GET] https://api.taapi.io/williamsalligator?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1h
		
```

## API response
Thewilliamsalligatorendpoint returns a JSON response like this:

`williamsalligator` 
```

			{
	"valueJaws": 68078.54021534462,
	"valueTeeth": 67764.67773254868,
	"valueLips": 66023.99113981522
}
		
```

## API parameters
`binance` `binancefutures` `BTC/USDT` `LTC/BTC` `1m` `5m` `15m` `30m` `1h` `2h` `4h` `12h` `1d` `1w` `interval=1h` `interval=1d` `backtrack` `backtrack=5` `0` `chart` `candles` `heikinashi` `candles` `heikinashi` `true` `false` `false` `true` `results` `1685577600` `1731456000` `true` `false` `true` `false` `number` `max` `max` Default:13

`13` Default:8

`8` Default:5

`5` Default:8

`8` Default:5

`5` Default:3

`3` 
## More examples
Let's say you want to know thewilliamsalligatorvalue on the last closed candle on the30mtimeframe. You are not interest in the real-time value, so you use thebacktrack=1optional parameter to go back 1 candle in history to the last closed candle.

`williamsalligator` `30m` `backtrack=1` 
```

				[GET] https://api.taapi.io/williamsalligator?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=30m&backtrack=1
			
```
Let's say you want to know what thewilliamsalligatordaily value was each day for the previous 10 days. You can get this returned by our API easily and efficiently in one call using theresults=10parameter:

`williamsalligator` `results=10` 
```

				[GET] https://api.taapi.io/williamsalligator?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1d&results=10
			
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

