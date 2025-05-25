# Donchian Channels via API documentation – TAAPI.IO

- Home
- News
- Pricing
- Indicators
- Exchanges
- Docs
- Log in
- Free API key

Home–Indicators–Donchian Channels


## Donchian Channels
Donchian Channels are a technical analysis tool developed by Richard Donchian, often used to identify potential breakouts in the market.


## Description:
Donchian Channels consist of three lines:

- Upper Band: This line is formed by the highest high over a specified period. For example, if you are using a 20-day Donchian Channel, the upper band is the highest price over the last 20 days.
- Lower Band: This line is formed by the lowest low over the same specified period. Following the 20-day example, the lower band is the lowest price over the last 20 days.
- Middle Band: Some traders also use a middle band (TAAPI includes it in the response), which is the average of the upper and lower bands. This can provide additional insight into the average price movement.


## How Traders Use Donchian Channels:
- Breakout Signals: One of the primary uses of Donchian Channels is to identify breakout opportunities. When the price breaks above the upper band, it may indicate a bullish breakout, suggesting that the uptrend could continue. Conversely, if the price breaks below the lower band, it could signify a bearish breakout, indicating a potential downtrend.
- Trend Confirmation: Traders also use Donchian Channels to confirm the direction of the trend. If the price consistently stays above the middle band and the upper band is sloping upwards, it suggests a strong uptrend. Conversely, if the price consistently stays below the middle band and the lower band is sloping downwards, it indicates a strong downtrend.
- Support and Resistance: The upper and lower bands can act as dynamic support and resistance levels. During an uptrend, the lower band can act as support, and during a downtrend, the upper band can act as resistance. Traders may look for buying opportunities near the lower band in an uptrend and selling opportunities near the upper band in a downtrend.
- Volatility Measurement: The width of the Donchian Channels can also provide information about market volatility. Wider channels indicate higher volatility, while narrower channels suggest lower volatility.
- Entry and Exit Points: Traders often use Donchian Channels to determine entry and exit points for trades. For example, a trader might enter a long position when the price breaks above the upper band and exit when it falls below the lower band.


## Get Donchian Channels values via API
We provide API access to the Donchian Channels values for all of the most popular assets likestocks, crypto (try one of the popular exchanges likeBinance) and forex, on all commonly used timeframes – from the weekly and daily all the way down to one minute intervals. You can also calculate the values on your own data using ourmanualmethod.


## Customise the Donchian Channels indicator
With our API, you can adjust the period of the Donchian Channels using the optionalperiodparameter, offering flexibility in your analysis. This parameter allows you to customize the length of time over which the highest high and lowest low are calculated, catering to your specific trading style and the current market conditions. For instance, choosing a shorter period like10days might help you capture shorter-term trends and quick breakouts, while opting for a longer period such as50days could provide a broader view of the market trend. Being able to adjust the period empowers you to fine-tune your strategies and adapt to changing market dynamics, enhancing the effectiveness of your Donchian Channels analysis.

`period` `10` `50` 
## Get started with thedonchianchannels
`donchianchannels` Simply make an HTTPS [GET] request or call in your browser:


```

			[GET] https://api.taapi.io/donchianchannels?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1h
		
```

## API response
Thedonchianchannelsendpoint returns a JSON response like this:

`donchianchannels` 
```

			{
	"value": {
		"upper": 72797.99,
		"middle": 66199,
		"lower": 59600.01
	}
}
		
```

## API parameters
`binance` `binancefutures` `BTC/USDT` `LTC/BTC` `1m` `5m` `15m` `30m` `1h` `2h` `4h` `12h` `1d` `1w` `interval=1h` `interval=1d` `backtrack` `backtrack=5` `0` `chart` `candles` `heikinashi` `candles` `heikinashi` `true` `false` `false` `true` `results` `1685577600` `1731456000` `true` `false` `true` `false` `number` `max` `max` The number of candles over which the highest high and lowest low are calculated.

Default:20

`20` 
## More examples
Let's say you want to know thedonchianchannelsvalue on the last closed candle on the30mtimeframe. You are not interest in the real-time value, so you use thebacktrack=1optional parameter to go back 1 candle in history to the last closed candle.

`donchianchannels` `30m` `backtrack=1` 
```

				[GET] https://api.taapi.io/donchianchannels?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=30m&backtrack=1
			
```
Let's say you want to know what thedonchianchannelsdaily value was each day for the previous 10 days. You can get this returned by our API easily and efficiently in one call using theresults=10parameter:

`donchianchannels` `results=10` 
```

				[GET] https://api.taapi.io/donchianchannels?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1d&results=10
			
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

