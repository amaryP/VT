# Rate limits – TAAPI.IO

- Home
- News
- Pricing
- Indicators
- Exchanges
- Docs
- Log in
- Free API key

Home–Documentation–Rate limits


## Rate limits
What are rate limits? Rate limits are the amount of requests you can make within a certain time span. Rate limits are specific to your plan:

- Free: 1 API request / 15 seconds
- Basic: 5 API requests / 15 seconds
- Pro: 30 API requests / 15 seconds
- Expert: 75 requests / 15 seconds


## Blocks
The API will respond with a429status code, with the following response:


```
{
    "error": "429: You have exceeded your request limit (TAAPI.IO rate-limit)!"
 }
```

## Max calculations per request
There’s an upper limit to how many calculations are allowed per request. This limit is20, unless you’re an Enterprise plan where other limits are individually agreed.

This max calculation limit includes the total number of calculations in one request, including bulk calculations, multiple backtracks andnumber of constructsused. As an example, consider the following:

- A single GET request with nobacktracks== 1 calculation.Passes
- A single GET request withbacktracksset to 10 == 10 calculations.Passes
- A bulk request with 5 indicators where each indicator request queries 3backtracks== 15 calculations.Passes
- A single GET request withbacktracksset to 30 == 30 calculations.Fails
- A bulk request with 5 indicators where each indicator request queries 10backtracks== 50 calculations.Fails



If you have any questions or comments, pleaseget in touch!


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

