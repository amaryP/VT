# Squeeze Momentum Indicator (SMI) via API documentation – TAAPI.IO

- Home
- News
- Pricing
- Indicators
- Exchanges
- Docs
- Log in
- Free API key

Home–Indicators–Squeeze Momentum Indicator (SMI)


## Squeeze Momentum Indicator (SMI)
TheSqueeze Momentum Indicator (SMI)helps traders identify periods of low volatility that are likely to be followed by high volatility and potential breakouts. It combines aspects ofBollinger BandsandKeltner Channelsto detect “squeeze” conditions, and adds a momentum histogram to gauge directional bias.

When Bollinger Bands are within the Keltner Channels, the market is in a“squeeze”, indicating consolidation and low volatility — often a precursor to large price moves. Once the squeeze is released (i.e., Bollinger Bands move outside the Keltner Channels), momentum is used to determine the likely direction of the breakout.


## Get SMI values via API
We provide API access to the Squeeze Momentum Values values for all of the most popular assets likestocks, crypto (try one of the popular exchanges likeBinance) and forex, on all commonly used timeframes – from the weekly and daily all the way down to one minute intervals. You can also calculate the values on your own data using ourmanualmethod.


## Customize the Squeeze Momentum Indicator
You can fine-tune the Squeeze Momentum Indicator (SMI) using several optional parameters to better suit your trading style and market conditions. ThelengthBBandlengthKCparameters control the lookback periods for theBollinger BandsandKeltner Channels, respectively — both default to 20 but can be adjusted for shorter or longer-term analysis. ThemultBBdefines how many standard deviations are used to plot the Bollinger Bands (default: 2.0), affecting their width and sensitivity to price movements. Similarly,multKCsets the ATR multiplier for the Keltner Channels (default: 1.5), which influences when a squeeze is detected. By customizing these inputs, you can make the indicator more reactive for short-term strategies or smoother for longer-term setups.

`lengthBB` `lengthKC` `multBB` `multKC` 
## Get started with thesqueeze
`squeeze` Simply make an HTTPS [GET] request or call in your browser:


```

			[GET] https://api.taapi.io/squeeze?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1h
		
```

## API response
Thesqueezeendpoint returns a JSON response like this:

`squeeze` 
```

			{
	"value": -626.0485785714322,
	"squeeze": true
}
		
```

## API parameters
`binance` `binancefutures` `BTC/USDT` `LTC/BTC` `1m` `5m` `15m` `30m` `1h` `2h` `4h` `12h` `1d` `1w` `interval=1h` `interval=1d` `backtrack` `backtrack=5` `0` `chart` `candles` `heikinashi` `candles` `heikinashi` `true` `false` `false` `true` `results` `1685577600` `1731456000` `true` `false` `true` `false` `number` `max` `max` Period for Bollinger Bands

Default:20

`20` Period for Keltner Channels

Default:20

`20` Standard deviation multiplier for Bollinger Bands

Default:2.0

`2.0` ATR multiplier for Keltner Channels

Default:1.5

`1.5` 
## More examples
Let's say you want to know thesqueezevalue on the last closed candle on the30mtimeframe. You are not interest in the real-time value, so you use thebacktrack=1optional parameter to go back 1 candle in history to the last closed candle.

`squeeze` `30m` `backtrack=1` 
```

				[GET] https://api.taapi.io/squeeze?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=30m&backtrack=1
			
```
Let's say you want to know what thesqueezedaily value was each day for the previous 10 days. You can get this returned by our API easily and efficiently in one call using theresults=10parameter:

`squeeze` `results=10` 
```

				[GET] https://api.taapi.io/squeeze?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1d&results=10
			
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

