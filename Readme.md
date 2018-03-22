
*********** REFERENCES & DISCLAIMERS **********************

1) Anywhere that AlphaVantage API is called/used, either in our mobile application, or within our console application, the following wrapper was used that made it possible. 

https://github.com/dyh1213/AlphaVantage_C-_ApiWrapper

Documentation was not provided or very sparce on AlphaVantage's site, so this wrapper provided us with the ability to initially fetch all of our pricing data!

*************************************************************




Goals For 3/13
Setup database to handle Free cash to equit, return on assets, return on equity, return on common equity, debt service coverage
ratio, receivables asset and inventory turnover ratios, ebit margin.

Tyler sets up console application to handle parsing and storing of data

Avi calculations for creating the 7 pieces from the api calls
  Free Cash to Equity, Return on Common Equity, and Debt service coverage will be finished next goal period.

Finished - Sam create the spots in the UI to hold the 7 pieces on each individual assets and page navigation to access new information



Goals for 3/27
  Meet the remaining requirements in our requirements document for a beta release presentation on 3/28

Tyler: Establish pricing data into the database, sharp ratio r^2 and alpha as well
REVISED: portfolio level calculaions are relatively complex, and will need more thought/planning. GOALS for this week now include getting price change displayed as percentage in portfolio details, daily pricing to factor into portfolio graph into console application/database to streamline user experience. 

Avi: Balance sheet income statement and cash flow parsing and finishing the calculation and classes for freecash to equity, return on common equity, and bet server coverage.

Sam: Deleting investments and portfolios as well as working with tyler to get the pricing data represented properly.
