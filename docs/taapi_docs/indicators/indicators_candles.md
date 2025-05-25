# Candles via API documentation – TAAPI.IO

- Home
- News
- Pricing
- Indicators
- Exchanges
- Docs
- Log in
- Free API key

Home–Indicators–Candles


## Candles
Returns multiple candles specified by theperiodparameter as an array, with a maximum of 300.

`period` 
## Get started with thecandles
`candles` Simply make an HTTPS [GET] request or call in your browser:


```

			[GET] https://api.taapi.io/candles?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1h
		
```

## API response
Thecandlesendpoint returns a JSON response like this:

`candles` 
```

			[
  {
    "timestampHuman": "2021-01-14 02:00:00 (Thursday) UTC",
    "timestamp": 1610589600,
    "open": 37451.13,
    "high": 37729.62,
    "low": 37071,
    "close": 37439.51,
    "volume": 4089.949019
  },
  {
    "timestampHuman": "2021-01-14 03:00:00 (Thursday) UTC",
    "timestamp": 1610593200,
    "open": 37440.92,
    "high": 37505,
    "low": 37000,
    "close": 37045.13,
    "volume": 2807.213786
  },
  {
    "timestampHuman": "2021-01-14 04:00:00 (Thursday) UTC",
    "timestamp": 1610596800,
    "open": 37045.13,
    "high": 37513.58,
    "low": 36701.23,
    "close": 37504,
    "volume": 3609.739475
  },
  {
    "timestampHuman": "2021-01-14 05:00:00 (Thursday) UTC",
    "timestamp": 1610600400,
    "open": 37503.99,
    "high": 37800,
    "low": 37299.74,
    "close": 37529.11,
    "volume": 3154.225655
  },
  {
    "timestampHuman": "2021-01-14 06:00:00 (Thursday) UTC",
    "timestamp": 1610604000,
    "open": 37529.11,
    "high": 38100,
    "low": 37440,
    "close": 37837.21,
    "volume": 3705.587755
  },
  {
    "timestampHuman": "2021-01-14 07:00:00 (Thursday) UTC",
    "timestamp": 1610607600,
    "open": 37837.2,
    "high": 38596.92,
    "low": 37703.09,
    "close": 38198.34,
    "volume": 5294.802195
  },
  {
    "timestampHuman": "2021-01-14 08:00:00 (Thursday) UTC",
    "timestamp": 1610611200,
    "open": 38199.52,
    "high": 38786.1,
    "low": 38192.64,
    "close": 38370.01,
    "volume": 3936.809626
  },
  {
    "timestampHuman": "2021-01-14 09:00:00 (Thursday) UTC",
    "timestamp": 1610614800,
    "open": 38370.01,
    "high": 38464.13,
    "low": 37862.04,
    "close": 37938.11,
    "volume": 3551.706141
  },
  {
    "timestampHuman": "2021-01-14 10:00:00 (Thursday) UTC",
    "timestamp": 1610618400,
    "open": 37938.1,
    "high": 38490.49,
    "low": 37869.1,
    "close": 38487.37,
    "volume": 3151.658946
  },
  {
    "timestampHuman": "2021-01-14 11:00:00 (Thursday) UTC",
    "timestamp": 1610622000,
    "open": 38488.05,
    "high": 38622.6,
    "low": 38060,
    "close": 38365.86,
    "volume": 3014.522816
  },
  {
    "timestampHuman": "2021-01-14 12:00:00 (Thursday) UTC",
    "timestamp": 1610625600,
    "open": 38365.86,
    "high": 38427.11,
    "low": 37707,
    "close": 38257.96,
    "volume": 3638.08289
  },
  {
    "timestampHuman": "2021-01-14 13:00:00 (Thursday) UTC",
    "timestamp": 1610629200,
    "open": 38257.97,
    "high": 39000,
    "low": 38154.22,
    "close": 38977.66,
    "volume": 5153.841864
  },
  {
    "timestampHuman": "2021-01-14 14:00:00 (Thursday) UTC",
    "timestamp": 1610632800,
    "open": 38977.98,
    "high": 39762.7,
    "low": 38788.19,
    "close": 39577.53,
    "volume": 9975.288968002053
  },
  {
    "timestampHuman": "2021-01-14 15:00:00 (Thursday) UTC",
    "timestamp": 1610636400,
    "open": 39577.53,
    "high": 39666,
    "low": 39294.7,
    "close": 39628.27,
    "volume": 1297.2943069999912
  }
]
		
```

## API parameters
`binance` `binancefutures` `BTC/USDT` `LTC/BTC` `1m` `5m` `15m` `30m` `1h` `2h` `4h` `12h` `1d` `1w` `interval=1h` `interval=1d` `backtrack` `backtrack=5` `0` `chart` `candles` `heikinashi` `candles` `heikinashi` `true` `false` `false` `true` `results` `1685577600` `1731456000` `true` `false` `true` `false` `number` `max` `max` Number of candles you want to return. Maximum 300.

Default:14

`14` 
## More examples
Let's say you want to know thecandlesvalue on the last closed candle on the30mtimeframe. You are not interest in the real-time value, so you use thebacktrack=1optional parameter to go back 1 candle in history to the last closed candle.

`candles` `30m` `backtrack=1` 
```

				[GET] https://api.taapi.io/candles?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=30m&backtrack=1
			
```
Let's say you want to know what thecandlesdaily value was each day for the previous 10 days. You can get this returned by our API easily and efficiently in one call using theresults=10parameter:

`candles` `results=10` 
```

				[GET] https://api.taapi.io/candles?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1d&results=10
			
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

