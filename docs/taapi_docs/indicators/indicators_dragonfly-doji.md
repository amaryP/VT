# Dragonfly Doji via API documentation – TAAPI.IO

- Home
- News
- Pricing
- Indicators
- Exchanges
- Docs
- Log in
- Free API key

Home–Indicators–Dragonfly Doji


## Dragonfly Doji

## Get started with thedragonflydoji
`dragonflydoji` Simply make an HTTPS [GET] request or call in your browser:


```

			[GET] https://api.taapi.io/dragonflydoji?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1h
		
```

## API response
Thedragonflydojiendpoint returns a JSON response like this:

`dragonflydoji` 
```

			
{
    value: "100" // dragonflydoji pattern found at this candle
}
				
// OR

{
    value: "-100" // dragonflydoji bearish variation (if applicable) found at this candle (not all patterns have bearish variations)
}
				
// OR
				
{
    value: "0" // dragonflydoji pattern NOT found at this candle
}
			
		
```

## API parameters
`binance` `binancefutures` `BTC/USDT` `LTC/BTC` `1m` `5m` `15m` `30m` `1h` `2h` `4h` `12h` `1d` `1w` `interval=1h` `interval=1d` `backtrack` `backtrack=5` `0` `chart` `candles` `heikinashi` `candles` `heikinashi` `true` `false` `false` `true` `results` `1685577600` `1731456000` `true` `false` `true` `false` `number` `max` `max` 
## More examples
Let's say you want to know thedragonflydojivalue on the last closed candle on the30mtimeframe. You are not interest in the real-time value, so you use thebacktrack=1optional parameter to go back 1 candle in history to the last closed candle.

`dragonflydoji` `30m` `backtrack=1` 
```

				[GET] https://api.taapi.io/dragonflydoji?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=30m&backtrack=1
			
```
Let's say you want to know what thedragonflydojidaily value was each day for the previous 10 days. You can get this returned by our API easily and efficiently in one call using theresults=10parameter:

`dragonflydoji` `results=10` 
```

				[GET] https://api.taapi.io/dragonflydoji?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1d&results=10
			
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

