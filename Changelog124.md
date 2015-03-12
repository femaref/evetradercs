**Notes**

**- IMPORTANT: This Release changes the structure of the settings.xml. Even though I tested it and implemented fail-save mechanisms, it is still possible you might lose your data**

- Please delete the folder "Cache" in the folder of EveTrader.exe to renew static data

- This Release contains debug information (`*`.pdb files) to help debugging. If you receive an Error, please copy the whole thing and send it to me

- There are blank windows - these will be filled in later releases

- Corporation Wallets are not used in the Reports tab or any other tab for the matter currently. This is a result of me being asked to release a new Version with the fixes since January. I will stay on this and hopefully find a solution, that fits my concerns.

**Features**

- Corporation Wallet Transactions and Market Orders

- Hiding non-active orders in the Market Orders tab

- Limit the time how long transactions will be calculated into the graphs (keeps prices more to the reality)

**Bugfixes**

- Fixed int32 overflow in WalletTransaction.ReferenceID

- Updated EveTypes to Dominion

**Known Issues**

- It is possible for a Corporation to appear more than once in the Wallet/Market Orders/Transactions tab