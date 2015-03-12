**Known Issues**

  * Sometimes, a hiccup can occur in the application (ie. clicking a checkbox too fast). At most times, the application will recover, just redo what you wanted to do. If the application crashes, send the error log to me, it might help. Loss of data is very unlikely to occur.
  * Sometimes, the charts fail to display data. Just force a refresh (eg. change the limit at the dashboard to refresh it) and the data will reappear. This can also happen in respect to the previously mentioned hiccup.
  * Sometimes a very long waiting time is possible on the details view in the dashboard.

**Changes**

  * upgrade to .net 4.0
  * general overhaul of domain model
  * general overhaul of the code, fixing bugs currently appearing (rewrite of the whole internal code)
  * switch to sqlite for data saving, due to this:
    * faster loading times
    * better data organisation, enabling more possible features without giving me a huge headache
    * easier to upgrade static data
    * example xml -> sqlite conversion: xml: 2.47mb sqlite 259kb, data compressed by 90%
  * switch to WPF for GUI
  * switch to WAF for Layer design (Model - View - ViewModel design, thus decoupling data handling from the GUI as it is currently)
  * switch to visifire charts for the charts

**New Features**

  * Full support of Corporation Wallets
  * Viewable Price Cache with all item values used in calculations
  * Ability to select what price source to take (currently we have metrics and the internal cached one)
  * ability to maximize the detail view in the dashboard (either context menu or double click)


**Drawing Board**
  * Combining multiple wallets and/or entities into one ViewGroup, combining all their data
  * Possibility of Custom Transactions and Journalentries (by this easier for producers and traders who do contracts to use the tool) -- partly fixed due fetching data from metrics
  * Possibility to set custom prices in the price cache
  * Fetching price data from eve central
  * Importing price data from cache files of the eve client
  * auto updater
  * automatic migration on update
  * broker fee and sales tax (I had to leave this one out, sadly)