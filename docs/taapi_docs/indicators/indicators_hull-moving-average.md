# Hull Moving Average via API documentation – TAAPI.IO

- Home
- News
- Pricing
- Indicators
- Exchanges
- Docs
- Log in
- Free API key

Home–Indicators–Hull Moving Average


## Hull Moving Average
The Hull Moving Average (HMA) is a technical indicator used in financial markets to analyze and smooth price data, facilitating trend identification and reducing lag compared to traditional moving averages. Developed by Alan Hull, the HMA combines aspects of simple and exponential moving averages to create a more responsive and accurate trend-following tool.


## How is Hull Moving Average different from other moving averages
Unlike the conventional moving averages, the Hull Moving Average incorporates weighted moving averages and a square root factor, resulting in a smoother curve that reacts more swiftly to price changes. This enhanced responsiveness makes the HMA particularly useful for traders seeking to capture trends with minimal delay, providing a valuable alternative to the more typicalsimpleandexponential moving averagesin the realm of technical analysis.


## Customize the HMA with optional parameters
Our API offers you the added benefit of adjusting theperiodparameter in the Hull Moving Average (HMA) to tailor your technical analysis precisely to your liking. This feature enables you to customize the indicator based on your specific trading preferences and timeframes. By tweaking theperiod, you have control over the number of candles or periods considered in the HMA calculation. Opting for a shorter period provides you with a more responsive HMA, ideal for capturing rapid trend changes. Conversely, choosing a longer period results in a smoother HMA that excels at identifying prolonged trends, catering to those with a patient trading approach. Fine-tune theperiodparameter and enhance your ability to adapt the Hull Moving Average to different market conditions so that it aligns with your preferred trading style. See the examples below.

`period` `period` `period` 
## Get started with thehma
`hma` Simply make an HTTPS [GET] request or call in your browser:


```

			[GET] https://api.taapi.io/hma?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1h&period=50
		
```

## API response
Thehmaendpoint returns a JSON response like this:

`hma` 
```

			{
  "value": 39382.09102020482
}
		
```

## API parameters
`binance` `binancefutures` `BTC/USDT` `LTC/BTC` `1m` `5m` `15m` `30m` `1h` `2h` `4h` `12h` `1d` `1w` `interval=1h` `interval=1d` `backtrack` `backtrack=5` `0` `chart` `candles` `heikinashi` `candles` `heikinashi` `true` `false` `false` `true` `results` `1685577600` `1731456000` `true` `false` `true` `false` `number` `max` `max` Specify the HMA length – the number of candles used to calculate the moving average.


## More examples
Let's say you want to know thehmavalue on the last closed candle on the30mtimeframe. You are not interest in the real-time value, so you use thebacktrack=1optional parameter to go back 1 candle in history to the last closed candle.

`hma` `30m` `backtrack=1` 
```

				[GET] https://api.taapi.io/hma?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=30m&period=50&backtrack=1
			
```
Let's say you want to know what thehmadaily value was each day for the previous 10 days. You can get this returned by our API easily and efficiently in one call using theresults=10parameter:

`hma` `results=10` 
```

				[GET] https://api.taapi.io/hma?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1d&period=50&results=10
			
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

