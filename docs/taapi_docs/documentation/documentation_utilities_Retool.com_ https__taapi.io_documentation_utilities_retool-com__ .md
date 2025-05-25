# Retool.com – TAAPI.IO

- Home
- News
- Pricing
- Indicators
- Exchanges
- Docs
- Log in
- Free API key

Home–Documentation–Utilities / 3rd party integrations–Retool.com


## Retool.com
Retool.comis an online web based tool that lets you create apps using third party services just like TAAPI.IO. You can easily build an app that lets you analyse the markets according to your strategy, without writing complicated bots in code.


## Setup
To get started you’ll need an account with Retool. Simply sign up and follow their onboarding directions.


## Create TAAPI.IO resource
Once logged into the dashboard, create a new resource by clicking ‘Resources’ in the top menu, then click ‘Create new’ -> ‘Resource’. This will take you to a form where you’ll need to fill in the following:

- Name: TAAPI.IO
- Base URL: https://api.taapi.io
- URL parameters: Add a new line with key = ‘secret’ and value = ‘<MY_SECRET>’(Replace with your actual taapi secret!)
- Headers: Add a new line with key = ‘content-type’ and value = ‘application/json’
- Extra body values: To use TAAPI.IO Bulk queries you will need to add your secret once more, so add a new line: key = ‘secret’ and value = ‘<MY_SECRET>’
- Extra body values: For easier parsing of the response data, add a new line: key = ‘outputFormat’ and value = ‘objects’
- Leave everything else as-is.

Please see below screenshot to validate the resource

- Click ‘Save changes’


## Using Retool
In order to use the new TAAPI.IO resource, click the ‘Apps’ top menu, and click ‘Create new’ -> ‘App’ and give it a name in the top left hand corner.


## Creating a query
Then in the below query editor, create a new query and name it:binance_btcusdt_1h_rsi. Now create your query:

- Resource: TAAPI.IO
- Action type: GET
- Endpoint: rsi
- Add URL parameters:exchange: binancesymbol: BTC/USDTinterval: 1h<any other parameters you’d like added>
- exchange: binance
- symbol: BTC/USDT
- interval: 1h
- <any other parameters you’d like added>
- Note: You do not need to put in your taapi secret, as it’s automatically added from the resource module

- exchange: binance
- symbol: BTC/USDT
- interval: 1h
- <any other parameters you’d like added>

- Click Run, and you should see a successful query


## Display result
To display this new RSI value in your Retool App, click the canvas, and in the right hand side menu, click and drag-n-drop a ‘Number Input’ field on to your canvas. You will find this in the ‘Number Inputs’ section. Now please fill in the properties for this field:

- Default value: {{ binance_btcusdt_1h_rsi.data.value }}
- Label: Binance:BTC/USDT:1h RSI
- Read only: true

Click ‘Run’ again and you should see the latest RSI value for Binance:BTC/USDT:1h populated in this field.


## Bulk
Retool.com works great with TAAPI.IO bulk queries. To get started, create a new query and fill in the properties:

- Resource: TAAPI.IO
- Action type: POST
- Endpoint: bulk
- Body: Raw
- Paste in your TAAPI.IO query as explained athttps://taapi.io/documentation/integration/direct/

- Click ‘Run’
- Reference the results with {{ indicators.data.rsi.value }}(as an example)
- Note: You do not need to put in your taapi secret, as it’s automatically added from the resource module
- Top tip: Get more results from multiple assets in a single query by using bulk with multiple constructs


## Assets: Crypto, Stocks, Forex
You can use Retool with other TAAPI.IO integrations such asPolygon.iofor Stocks, Forex and Options. Simply add the needed parameters to your queries.


## That’s it!
Hopefully this will get you going. Retool.com themselves have a ton of documentation on how to use their system. Here’s a good place to start:https://docs.retool.com/docs

As always, feedback very welcome, happy trading!


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

