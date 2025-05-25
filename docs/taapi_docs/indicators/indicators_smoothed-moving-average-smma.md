# Smoothed Moving Average (SMMA) via API documentation – TAAPI.IO

- Home
- News
- Pricing
- Indicators
- Exchanges
- Docs
- Log in
- Free API key

Home–Indicators–Smoothed Moving Average (SMMA)


## Smoothed Moving Average (SMMA)
A Smoothed Moving Average (SMMA) is a technical indicator used in financial analysis to reduce the impact of sharp fluctuations in price data. It’s a type of moving average that is more responsive to recent price changes compared to theSimple Moving Average (SMA)but less sensitive than theExponential Moving Average (EMA).

The difference between SMMA, SMA, and EMA lies in how they weight data. The Simple Moving Average (SMA) is straightforward; it takes the average of closing prices over a specified number of periods. However, it’s sensitive to all data points, including older ones, which might not reflect current market conditions accurately.

The Exponential Moving Average (EMA) gives more weight to recent prices, making it more responsive to price changes compared to the SMA. It’s calculated using a smoothing factor that gives more importance to the most recent data points.

In contrast, theSmoothed Moving Average (SMMA)is acompromisebetween the SMA and EMA. It is also a type of weighted moving average, but it smooths out price data by giving equal weight to all periods within the chosen time frame. This means the SMMAreacts more quicklyto price changes than the SMA but less rapidly than the EMA, striking abalance between responsiveness and stability.


## Get values via API
We provide API access to the SMMA values for all of the most popular assets likestocks, crypto (try one of the popular exchanges likeBinance) and forex, on all commonly used timeframes – from the weekly and daily all the way down to one minute intervals. You can also calculate the values on your own data using ourmanualmethod.


## Customise SMMA Sensitivity
Using our API, you have the flexibility to customize the length of the Smoothed Moving Average (SMMA) by adjusting the optionalperiodparameter. This parameter allows you to define the number of periods over which the SMMA is calculated, giving you control over how responsive or smooth you want the indicator to be based on your trading strategy or analysis needs.

`period` 
## Get started with thesmma
`smma` Simply make an HTTPS [GET] request or call in your browser:


```

			[GET] https://api.taapi.io/smma?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1h
		
```

## API response
Thesmmaendpoint returns a JSON response like this:

`smma` 
```

			{
	"value": 66021.52746920686
}
		
```

## API parameters
`binance` `binancefutures` `BTC/USDT` `LTC/BTC` `1m` `5m` `15m` `30m` `1h` `2h` `4h` `12h` `1d` `1w` `interval=1h` `interval=1d` `backtrack` `backtrack=5` `0` `chart` `candles` `heikinashi` `candles` `heikinashi` `true` `false` `false` `true` `results` `1685577600` `1731456000` `true` `false` `true` `false` `number` `max` `max` Adjust the SMMA length – the number of candles used to calculate the SMMA.

Default:7

`7` 
## More examples
Let's say you want to know thesmmavalue on the last closed candle on the30mtimeframe. You are not interest in the real-time value, so you use thebacktrack=1optional parameter to go back 1 candle in history to the last closed candle.

`smma` `30m` `backtrack=1` 
```

				[GET] https://api.taapi.io/smma?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=30m&backtrack=1
			
```
Let's say you want to know what thesmmadaily value was each day for the previous 10 days. You can get this returned by our API easily and efficiently in one call using theresults=10parameter:

`smma` `results=10` 
```

				[GET] https://api.taapi.io/smma?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1d&results=10
			
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

