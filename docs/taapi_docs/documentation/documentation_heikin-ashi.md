# Heikin Ashi Candles – TAAPI.IO

- Home
- News
- Pricing
- Indicators
- Exchanges
- Docs
- Log in
- Free API key

Home–Documentation–Heikin Ashi Candles


## Heikin Ashi Candles
The Heikin-Ashi technique is a unique charting method designed to smooth price action and filter out market noise, providing traders with a clearer view of trends and potential reversals. Originating in the 1700s from Munehisa Homma, a pioneer of Japanese candlestick analysis, this technique refines traditional candlestick charts by using a calculated formula based on two-period averages.

Unlike standard candlestick charts that directly reflect open, high, low, and close prices, Heikin-Ashi candles adjust these values to emphasize the overall trend direction. The result is a visually smoother chart that simplifies complex market movements, helping traders spot trends with ease. However, this simplification also masks gaps and some precise price details, making Heikin-Ashi charts best suited for analyzing trends rather than pinpointing exact prices.

By incorporating Heikin-Ashi charts into your trading strategy, you can gain a more intuitive understanding of market dynamics while minimizing distractions from short-term volatility.


## Getting Started
To use Heikin Ashi candles as the data set used to calculate indicator values, all indicator queries in all the integration methods will take achartparameter which must be set toheikinashi.

`chart` `heikinashi` 
## Example
As a simple example with the Direct Method simply adjust your query like so:


```
[GET] https://api.taapi.io/rsi?secret=MY_SECRET&exchange=binance&symbol=BTC/USDT&interval=1h&chart=heikinashi
```

## RAW Heikin Ashi candle values
To obtain the RAW Heikin Ashi candle values is by using one of these 2 endpoints, and setting thechartparameter toheikinashi:

`chart` `heikinashi` - /candles: This will return Heikin Ashi candles.
- /candle: This will return the latest (or last backtracked) Heikin Ashi candle.


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

