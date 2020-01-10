# e-Amusement Card Convert
A command-line utility for converting between card numbers written on the back of an e-Amusement card and the card ID stored in the RFID on the card. 

This utility is a port of the [card convert](https://github.com/DragonMinded/bemaniutils/blob/master/bemani/common/card.py) utility originally written in python by [@DragonMinded](https://github.com/DragonMinded).

# Usage
Run it like `./eamusecardconvert <card-id>`.  `<card-id>` can be either the 16-character card ID stored in the RFID on the e-Amusement card, or the 16-character card number as shown on the back of the e-Amusement card.  Whichever value is supplied, the resulting output will be the conversion to the other.  Inputs will be sanitized, so you can feed card ID and back-of-card numbers with or without spaces, with or without hyphens, and you can also mix up `1` and `I` as well as `O` and `0`.  Both old-style and newer FeliCa style e-Amusement cards are supported.




