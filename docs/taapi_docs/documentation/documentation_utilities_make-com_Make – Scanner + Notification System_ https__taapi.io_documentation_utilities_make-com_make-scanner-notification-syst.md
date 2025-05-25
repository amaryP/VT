# Make - Scanner + Notification System – TAAPI.IO

- Home
- News
- Pricing
- Indicators
- Exchanges
- Docs
- Log in
- Free API key

Home–Documentation–Utilities / 3rd party integrations–Make–Make – Scanner + Notification System


## Make – Scanner + Notification System
Let’s make a scanner that scans an exchange for potential trade setups, and lets us know on Telegram that this coin is worth taking a look at.

This article assumes that you have a TAAPI.IO secret key and aMake account. If you haven’t signed up on their site yet,do that now, they are a pretty good service.


## The Logic
- Scan all of Binance’s USDT pairs every hour, on the hourly timeframe
- Check if thelatest close StochRSI k > d(Indicates reversal to the upside)
- Check if theprevious close StochRSI k < d(Indicates that the crossover happend at this specific hour)
- Check if thelatest close StochRSI d value is below 20(Indicates oversold)

A simple strategy that will give a good indication that a coin is both oversold and reversing to the upside again.


## Make scenario
Create a new Make Scenario and let’s get started. Give it a name in the top left hand corner.


## Scanner
The first module you will need is theTAAPI.IO ‘Get Exchange Symbols’. Add this and make sure you have a connection setup with your secret entered. Now tell this module that you would like all the USDT pairs from Binance:

Once this is done, let’s set the scheduler to run every 60 minutes. Click the clock in the module:

Click ‘Run once’ and you should see the results in the little bubbles that pops up as it runs. You should see something like this:

Now we have all the Binance USDT pairs, and we’ll now need to iterate through them. So create an Iterator from the green menu point, and drag the ‘Symbols[]’ array on to here.

The last bit of the scanner ensures that we respect all rate-limits. Thus create a ‘Sleep’ module and make sure you set it to no less than what your TAAPI.IO plan allows. For instance with a Basic plan you have 5 calls per 15 seconds, which means we need to wait: 15 / 5 = 3 seconds.

Hit ‘Run once’ again and verify the outputs and that there’re no errors. If all looks good, simply stop execution as we don’t need to wait for it to finish.


## Implement Strategy
Create a new TAAPI.IO module:Get indicator (Multiple)and fill out:

- Indicator: stochrsi
- Exchange: binance
- Symbol: Drag the ‘Value’ from the ‘Symbol’ iterator
- Interval: 1h
- Backtrack: 1 (Loose the current real-time value as we’re only interested in the latest close value)
- Backtracks: 2 (Get latest 2 StochRSI values)


## Setup Notifications
So now we have our scanner set up, and we’re respecting rate-limits, and we’re fetching all the values we need to do the actual evaluation of the numbers according to our strategy. Now all we need to do is set up our Telegram Notification and we’re (almost) done!

Rename the TAAPI.IO module to ‘StochRSI’.


## Filter
We’re now connected to Telegram and we can send messages and notifications. The very final bit we need to do, is the actual filter that evaluates the StochRSI values. Click on the dotted link between StochRSI and Telegram Bot. We need 3 conditions as stated in our above strategy and all must be true at the same time:

- Latest close StochRSI k > d
- Previous close StochRSI k < d
- Latest close StochRSI d value is below 20

Click OK and hit ‘Run once’ again, and you should see notifications in Telegram when a signal happens!

Important: As of this writing, Binance has 343 traded USDT pairs, and if you’re running with a 3 second delay in between each symbol, this will take you 343 symbols x 3 seconds per symbol = 1029 seconds / 60 = 17.15 minutes. Keep this in mind if a delay of this size is acceptable for you, otherwise upgrade your TAAPI.IO plan so that you can execute faster. And as a final note while we’re at it, make sure that your execution time doesn’t exceed your execution timing interval. Otherwise a job will not finish before the next one starts, and that will eventually not go well!


## That’s it!
You’ve now made something that a programmer would charge big bucks for, while at the same time keeping your strategy to yourself, with the added benefit of you being 100% in control. If something goes wrong, you don’t have to get into a long fight with developers 🙂

As always, feedback and comments appreciated. Happy Trading!


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

