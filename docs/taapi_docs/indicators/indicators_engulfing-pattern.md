# Engulfing Pattern via API documentation – TAAPI.IO

- Home
- News
- Pricing
- Indicators
- Exchanges
- Docs
- Log in
- Free API key

Home–Indicators–Engulfing Pattern


## Engulfing Pattern
The engulfing candlestick pattern is a significant technical analysis signal that often indicates a potential reversal in the prevailing market trend. This pattern is formed by two consecutive candlesticks, where the body of the second candle completely engulfs the body of the preceding one. There are two types of engulfing patterns: bullish engulfing and bearish engulfing.

- Bullish Engulfing Pattern:Formation:The first candle in this pattern is typically a smaller bearish (downward) candle, followed by a larger bullish (upward) candle whose body completely engulfs the previous bearish candle.Implication:The bullish engulfing pattern suggests a potential reversal from a downtrend to an uptrend. It indicates that buyers have overwhelmed sellers, leading to increased buying interest.
- Formation:The first candle in this pattern is typically a smaller bearish (downward) candle, followed by a larger bullish (upward) candle whose body completely engulfs the previous bearish candle.
- Implication:The bullish engulfing pattern suggests a potential reversal from a downtrend to an uptrend. It indicates that buyers have overwhelmed sellers, leading to increased buying interest.
- Bearish Engulfing Pattern:Formation:In contrast, the bearish engulfing pattern begins with a smaller bullish (upward) candle, followed by a larger bearish (downward) candle that completely engulfs the prior bullish candle.Implication:The bearish engulfing pattern signals a potential reversal from an uptrend to a downtrend. It suggests that sellers have gained control, overpowering the buyers and indicating a shift in market sentiment.
- Formation:In contrast, the bearish engulfing pattern begins with a smaller bullish (upward) candle, followed by a larger bearish (downward) candle that completely engulfs the prior bullish candle.
- Implication:The bearish engulfing pattern signals a potential reversal from an uptrend to a downtrend. It suggests that sellers have gained control, overpowering the buyers and indicating a shift in market sentiment.

- Formation:The first candle in this pattern is typically a smaller bearish (downward) candle, followed by a larger bullish (upward) candle whose body completely engulfs the previous bearish candle.
- Implication:The bullish engulfing pattern suggests a potential reversal from a downtrend to an uptrend. It indicates that buyers have overwhelmed sellers, leading to increased buying interest.

- Formation:In contrast, the bearish engulfing pattern begins with a smaller bullish (upward) candle, followed by a larger bearish (downward) candle that completely engulfs the prior bullish candle.
- Implication:The bearish engulfing pattern signals a potential reversal from an uptrend to a downtrend. It suggests that sellers have gained control, overpowering the buyers and indicating a shift in market sentiment.


## Get started with theengulfing
`engulfing` Simply make an HTTPS [GET] request or call in your browser:


```

			[GET] https://api.taapi.io/engulfing?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1h
		
```

## API response
Theengulfingendpoint returns a JSON response like this:

`engulfing` 
```

			
{
    value: "100" // engulfing pattern found at this candle
}
				
// OR

{
    value: "-100" // engulfing bearish variation (if applicable) found at this candle (not all patterns have bearish variations)
}
				
// OR
				
{
    value: "0" // engulfing pattern NOT found at this candle
}
			
		
```

## API parameters
`binance` `binancefutures` `BTC/USDT` `LTC/BTC` `1m` `5m` `15m` `30m` `1h` `2h` `4h` `12h` `1d` `1w` `interval=1h` `interval=1d` `backtrack` `backtrack=5` `0` `chart` `candles` `heikinashi` `candles` `heikinashi` `true` `false` `false` `true` `results` `1685577600` `1731456000` `true` `false` `true` `false` `number` `max` `max` 
## More examples
Let's say you want to know theengulfingvalue on the last closed candle on the30mtimeframe. You are not interest in the real-time value, so you use thebacktrack=1optional parameter to go back 1 candle in history to the last closed candle.

`engulfing` `30m` `backtrack=1` 
```

				[GET] https://api.taapi.io/engulfing?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=30m&backtrack=1
			
```
Let's say you want to know what theengulfingdaily value was each day for the previous 10 days. You can get this returned by our API easily and efficiently in one call using theresults=10parameter:

`engulfing` `results=10` 
```

				[GET] https://api.taapi.io/engulfing?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1d&results=10
			
```
Here's the example response:


```

				{
  "value": [
  0,
  0,
  0,
  100, // <-- Pattern found at this candle
  0,
  0,
  -100, // <-- Bearish variation pattern found at this candle
  0,
  0,
  0 // <-- Most recent value 
  ]
}
			
```

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

