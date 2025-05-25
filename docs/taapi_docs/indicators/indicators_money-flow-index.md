# Money Flow Index via API documentation – TAAPI.IO

- Home
- News
- Pricing
- Indicators
- Exchanges
- Docs
- Log in
- Free API key

Home–Indicators–Money Flow Index


## Money Flow Index
TheMoney Flow Index(MFI) is a technical indicator used in financial markets to assess the strength andmomentumof a price movement. Developed by Gene Quong and Avrum Soudack, MFI combines price and volume data to provide insights into the buying and selling pressure behind a security. You can use TAAPI.IO to access both realtime and historical values of MFI easily and also combine it with other indicators like theRSIor Moving Averages.

The indicator is calculated through a series of steps: first, the typical price (the average of high, low, and closing prices) is determined; then, the money flow is calculated by multiplying the typical price by the corresponding volume. Positive money flow represents buying pressure, while negative money flow signifies selling pressure.

The MFI is anoscillatorand is calculated using a formula that takes into account the ratio of positive to negative money flow and scales it to a value between0and100. A high MFI suggests a potentially overbought condition, indicating that a reversal or correction may be imminent, while a low MFI may signal an oversold condition, suggesting a potential buying opportunity.

`0` `100` One common pairing is with theRelative Strength Index (RSI), another popular oscillator. Both the MFI and RSI assess overbought and oversold conditions, but they use slightly different calculations. When these two indicators give similar signals, it can strengthen the confidence of traders in the identified market conditions.

Additionally, some traders combine the MFI with trend-following indicators like moving averages. The convergence of signals from different types of indicators can provide a more comprehensive view of market dynamics. For example, if the MFI signals overbought conditions while a moving average suggests a strong uptrend, traders might approach the situation differently than if these indicators were sending conflicting signals.


## Get started with themfi
`mfi` Simply make an HTTPS [GET] request or call in your browser:


```

			[GET] https://api.taapi.io/mfi?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1h
		
```

## API response
Themfiendpoint returns a JSON response like this:

`mfi` 
```

			{
  "value": 29.67871649257061
}
		
```

## API parameters
`binance` `binancefutures` `BTC/USDT` `LTC/BTC` `1m` `5m` `15m` `30m` `1h` `2h` `4h` `12h` `1d` `1w` `interval=1h` `interval=1d` `backtrack` `backtrack=5` `0` `chart` `candles` `heikinashi` `candles` `heikinashi` `true` `false` `false` `true` `results` `1685577600` `1731456000` `true` `false` `true` `false` `number` `max` `max` Default:14

`14` 
## More examples
Let's say you want to know themfivalue on the last closed candle on the30mtimeframe. You are not interest in the real-time value, so you use thebacktrack=1optional parameter to go back 1 candle in history to the last closed candle.

`mfi` `30m` `backtrack=1` 
```

				[GET] https://api.taapi.io/mfi?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=30m&backtrack=1
			
```
Let's say you want to know what themfidaily value was each day for the previous 10 days. You can get this returned by our API easily and efficiently in one call using theresults=10parameter:

`mfi` `results=10` 
```

				[GET] https://api.taapi.io/mfi?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1d&results=10
			
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

