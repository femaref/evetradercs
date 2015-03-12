**Features**

- A balance history was added, sadly, due to no data before this feature, only new data will be shown

- Report tab splitted into subtabs, currently Products, Stations, Clients and Balance History

- It is now possible to specify how much items will be displayed in the report charts,
this setting can be automaticly or manually applied

- All settings possible in the Report Tab (timeframe, items per chart and automatic apply) are now saved when the program is closed

- Sales Tax is included in profit calculation (Report tab only), it is displayed as extra bar in the products chart, and will be displayed in the stations chart

- Accounting level can be specified at File-> Manage Characters-> Existing Characters-> Right click on a character (it might be possible that you have to select the character with left click first), this will be taken into account while calculating Sales Tax.

- If a problem appears while saving a setting file, the old version is saved. This means that no loss of data occurs except for the current session. Please contact me if this happends.

**Internals**

- Standing list is requested from the server and saved

- fixed the int32 overflow with recent API changes

- general code cleanup (empty stubs, genericly named variables, coding by exception, code design)