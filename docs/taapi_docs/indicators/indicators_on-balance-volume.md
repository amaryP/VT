# On Balance Volume via API documentation – TAAPI.IO

- Home
- News
- Pricing
- Indicators
- Exchanges
- Docs
- Log in
- Free API key

Home–Indicators–On Balance Volume


## On Balance Volume
On-Balance Volume (OBV) is a technical momentum indicator that analyzes trading activity to gauge buying and selling pressure. It essentially tracks the cumulative volume flow associated with price movements.


## How is On Balance Volume used by traders?
Traders use OBV to identify potential shifts in price trends and confirm existing trends. The core idea is that volume precedes price: significant buying pressure (high volume) typically precedes price increases, and vice versa. By analyzing the OBV line in relation to the price chart, traders can spot these signals:

- Confirmation of Trends:If the price is rising and the OBV is also rising, it strengthens the uptrend. Conversely, a falling price with a declining OBV confirms a downtrend.
- Divergences:When the price and OBV diverge, it can signal a potential trend reversal. For instance, a rising price with a flat or declining OBV suggests weakening buying pressure, which might foreshadow a price drop.


## How isthe indicatocalculated
The OBV is a cumulative total of volume based on closing prices:

- If the closing price is higher than the previous day’s close, the day’s volume is added to the previous OBV.
- If the closing price is lower, the day’s volume is subtracted from the previous OBV.
- If the closing price is the same, the OBV remains unchanged.


## Get OBV values via API
We provide API access the RSI values for all of the most popular assets likestocks, crypto (try one of the popular exchanges likeBinance) and forex, on all commonly used timeframes – from the weekly and daily all the way down to one minute intervals. You can also calculate the values on your own data using ourmanualmethod.


## Get started with theobv
`obv` Simply make an HTTPS [GET] request or call in your browser:


```

			[GET] https://api.taapi.io/obv?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1h
		
```

## API response
Theobvendpoint returns a JSON response like this:

`obv` 
```

			{
  "value": 322291.3012529999
}
		
```

## API parameters
`binance` `binancefutures` `BTC/USDT` `LTC/BTC` `1m` `5m` `15m` `30m` `1h` `2h` `4h` `12h` `1d` `1w` `interval=1h` `interval=1d` `backtrack` `backtrack=5` `0` `chart` `candles` `heikinashi` `candles` `heikinashi` `true` `false` `false` `true` `results` `1685577600` `1731456000` `true` `false` `true` `false` `number` `max` `max` 
## More examples
Let's say you want to know theobvvalue on the last closed candle on the30mtimeframe. You are not interest in the real-time value, so you use thebacktrack=1optional parameter to go back 1 candle in history to the last closed candle.

`obv` `30m` `backtrack=1` 
```

				[GET] https://api.taapi.io/obv?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=30m&backtrack=1
			
```
Let's say you want to know what theobvdaily value was each day for the previous 10 days. You can get this returned by our API easily and efficiently in one call using theresults=10parameter:

`obv` `results=10` 
```

				[GET] https://api.taapi.io/obv?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1d&results=10
			
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

