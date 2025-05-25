# Directional Movement Index via API documentation – TAAPI.IO

- Home
- News
- Pricing
- Indicators
- Exchanges
- Docs
- Log in
- Free API key

Home–Indicators–Directional Movement Index


## Directional Movement Index
The Directional Movement Index (DMI) is a popular technical indicator used by traders to assess the strength and direction of a trend in a financial instrument. It was developed by J. Welles Wilder and is particularly useful in identifying trending markets. The DMI consists of three returned values: the Average Directional Index (ADX), the Positive Directional Indicator (PDI), and the Negative Directional Indicator (NDI).

The ADX is the primary component of the DMI and measures the strength of a trend, regardless of its direction. It provides traders with a numerical value ranging from 0 to 100. A higher ADX value indicates a stronger trend, while a lower value suggests a weaker or non-existent trend. Traders often look for ADX values above 25 to confirm the presence of a significant trend.

The PDI and NDI are secondary components of the DMI and provide insights into the direction of the trend. The PDI measures the strength of upward price movements, while the NDI measures the strength of downward price movements. Both indicators range from 0 to 100. When the PDI is above the NDI, it suggests a bullish trend, indicating buying pressure. Conversely, when the NDI is above the PDI, it suggests a bearish trend, indicating selling pressure.

Traders use the DMI to identify potential entry and exit points in the market. When the ADX is rising, it indicates an increasing trend strength, and traders may consider opening positions in the direction of the trend. Conversely, when the ADX is falling or below the 25 threshold, it suggests a weakening trend, and traders may consider closing their positions or waiting for a new trend to develop.

In summary, the DMI is a valuable technical indicator that helps traders assess the strength and direction of a trend. The ADX provides a measure of trend strength, while the PDI and NDI indicate the direction of the trend. By using these three values together, traders can make more informed decisions and improve their trading strategies.

Note: in our API responses, we use “mdi” (“minus DI”) instead of NDI, but that is just a naming difference.


## Get started with thedmi
`dmi` Simply make an HTTPS [GET] request or call in your browser:


```

			[GET] https://api.taapi.io/dmi?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1h
		
```

## API response
Thedmiendpoint returns a JSON response like this:

`dmi` 
```

			{
	"adx": 32.79044853218805,
	"pdi": 25.418284332339116,
	"mdi": 16.2144851753789
}
		
```

## API parameters
`binance` `binancefutures` `BTC/USDT` `LTC/BTC` `1m` `5m` `15m` `30m` `1h` `2h` `4h` `12h` `1d` `1w` `interval=1h` `interval=1d` `backtrack` `backtrack=5` `0` `chart` `candles` `heikinashi` `candles` `heikinashi` `true` `false` `false` `true` `results` `1685577600` `1731456000` `true` `false` `true` `false` `number` `max` `max` Sets the number of candles used in the indicator calculation.

Default:14

`14` 
## More examples
Let's say you want to know thedmivalue on the last closed candle on the30mtimeframe. You are not interest in the real-time value, so you use thebacktrack=1optional parameter to go back 1 candle in history to the last closed candle.

`dmi` `30m` `backtrack=1` 
```

				[GET] https://api.taapi.io/dmi?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=30m&backtrack=1
			
```
Let's say you want to know what thedmidaily value was each day for the previous 10 days. You can get this returned by our API easily and efficiently in one call using theresults=10parameter:

`dmi` `results=10` 
```

				[GET] https://api.taapi.io/dmi?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1d&results=10
			
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

