# Volume Split (Buy /Sell) via API – TAAPI.IO

- Home
- News
- Pricing
- Indicators
- Exchanges
- Docs
- Log in
- Free API key

Home–Indicators–Volume Split


## Volume Split
The Volume Split indicator helps traders analyze the buying and selling pressure in the market. By breaking down the total tradedvolumeinto buying and selling components, this indicator provides a deeper insight into market dynamics.

The Volume Split indicator determines buying and selling volumes by analyzing the price movement within each bar. Buying volume is calculated based on how much the price has moved up from the lowest point to the closing price. If the price closes higher in a bar, the volume associated with that upward movement is considered buying volume. Conversely, selling volume is calculated based on how much the price has moved down from the highest point to the closing price. If the price closes lower in a bar, the volume associated with that downward movement is considered selling volume. When the high and low prices are the same, both buying and selling volumes are set to zero to avoid errors.


## How to understand Volume Split API Responses
When you use our API to get the Buying Selling Volume data, you receive a JSON object with the following values:

- valueTotalThis is the total volume of trades within the specified period. It represents the sum of all buying and selling activities.
- valueBuyThis is the volume attributed to buying activities. It shows the portion of the total volume where traders are purchasing the asset.
- valueSellThis is the volume attributed to selling activities. It shows the portion of the total volume where traders are selling the asset.
- percentBuyThis is the percentage of the total volume that is attributed to buying activities. It indicates how much of the trading volume was due to purchases.
- percentSellThis is the percentage of the total volume that is attributed to selling activities. It indicates how much of the trading volume was due to sales.

`valueTotal` `valueBuy` `valueSell` `percentBuy` `percentSell` 
## Credit
This indicator was inspired by and derived from an implementation byCeyhun.


## Get started with thevolumesplit
`volumesplit` Simply make an HTTPS [GET] request or call in your browser:


```

			[GET] https://api.taapi.io/volumesplit?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1h
		
```

## API response
Thevolumesplitendpoint returns a JSON response like this:

`volumesplit` 
```

			{
	"valueTotal": 6375.229269996414,
	"valueBuy": 1190.3238075620077,
	"valueSell": 5184.905462434405,
	"percentBuy": 18.67107451592368,
	"percentSell": 81.32892548407631
}
		
```

## API parameters
`binance` `binancefutures` `BTC/USDT` `LTC/BTC` `1m` `5m` `15m` `30m` `1h` `2h` `4h` `12h` `1d` `1w` `interval=1h` `interval=1d` `backtrack` `backtrack=5` `0` `chart` `candles` `heikinashi` `candles` `heikinashi` `true` `false` `false` `true` `results` `1685577600` `1731456000` `true` `false` `true` `false` `number` `max` `max` 
## More examples
Let's say you want to know thevolumesplitvalue on the last closed candle on the30mtimeframe. You are not interest in the real-time value, so you use thebacktrack=1optional parameter to go back 1 candle in history to the last closed candle.

`volumesplit` `30m` `backtrack=1` 
```

				[GET] https://api.taapi.io/volumesplit?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=30m&backtrack=1
			
```
Let's say you want to know what thevolumesplitdaily value was each day for the previous 10 days. You can get this returned by our API easily and efficiently in one call using theresults=10parameter:

`volumesplit` `results=10` 
```

				[GET] https://api.taapi.io/volumesplit?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1d&results=10
			
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

