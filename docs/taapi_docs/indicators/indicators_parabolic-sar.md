# Parabolic SAR via API documentation – TAAPI.IO

- Home
- News
- Pricing
- Indicators
- Exchanges
- Docs
- Log in
- Free API key

Home–Indicators–Parabolic SAR


## Parabolic SAR
The Parabolic SAR (Stop and Reverse) is a popular technical indicator used in financial markets to identify potential trend reversals. Developed by J. Welles Wilder Jr., it utilizes a series of dots plotted on a price chart to highlight potential entry and exit points.

The Parabolic SAR dots are positioned above or below the price, depending on the direction of the prevailing trend. When the dots are below the price, it indicates an uptrend, while dots above the price indicate a downtrend. The dots gradually adjust their position as the price evolves, creating a parabolic shape. Traders often use the Parabolic SAR as a tool for setting stop-loss orders and trailing stops, as it dynamically adapts to market conditions and can help identify potential trend reversals in a timely manner.

In the Parabolic SAR formula, users have the flexibility to adjust three optional parameters:start,incrementandmaximum. Thestartparameter determines the initial acceleration factor for the indicator. Theincrementparameter specifies the rate at which the acceleration factor increases over time. Lastly, themaximumparameter sets the upper limit for the acceleration factor. By modifying these parameters, traders can customize the sensitivity and responsiveness of the Parabolic SAR indicator to suit their trading strategies and market conditions.

`start` `increment` `maximum` `start` `increment` `maximum` 
## Get started with thepsar
`psar` Simply make an HTTPS [GET] request or call in your browser:


```

			[GET] https://api.taapi.io/psar?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1h
		
```

## API response
Thepsarendpoint returns a JSON response like this:

`psar` 
```

			{
  "value": 38091.00023984
}
		
```

## API parameters
`binance` `binancefutures` `BTC/USDT` `LTC/BTC` `1m` `5m` `15m` `30m` `1h` `2h` `4h` `12h` `1d` `1w` `interval=1h` `interval=1d` `backtrack` `backtrack=5` `0` `chart` `candles` `heikinashi` `candles` `heikinashi` `true` `false` `false` `true` `results` `1685577600` `1731456000` `true` `false` `true` `false` `number` `max` `max` Determines the initial acceleration factor for the indicator.

Default:0.02

`0.02` Specifies the rate at which the acceleration factor increases over time.

Default:0.02

`0.02` Sets the upper limit for the acceleration factor.

Default:0.2

`0.2` 
## More examples
Let's say you want to know thepsarvalue on the last closed candle on the30mtimeframe. You are not interest in the real-time value, so you use thebacktrack=1optional parameter to go back 1 candle in history to the last closed candle.

`psar` `30m` `backtrack=1` 
```

				[GET] https://api.taapi.io/psar?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=30m&backtrack=1
			
```
Let's say you want to know what thepsardaily value was each day for the previous 10 days. You can get this returned by our API easily and efficiently in one call using theresults=10parameter:

`psar` `results=10` 
```

				[GET] https://api.taapi.io/psar?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1d&results=10
			
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

