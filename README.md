# Mini Healer Save Fiddler
A quick and dirty encrypted save to JSON and back again converter for Mini Healer.

Odds are I probably won't add any features past this.
I was just interested in looking at the save file.

# Doing things~
Frankly, it should be pretty self-explanatory.
If for some reason it doesn't default to your Mini Healer save directory it can be found at:
***%UserProfile%\AppData\LocalLow\MiniHealer\MiniHealer***
### Decrypt Save
Open up an encrypted player0.txt file. Or player#.txt, whichever save number you're wanting to fiddle with.
Put in JSON filename to save it as.
Edit JSON file.

Also, it will make a timestamped backup of your encrypted save file in case you fiddle with something you shouldn't have.
These will be player#.txt_yyyyMMdd_HHmmss.bak files.

### Encrypt Save
Opens up the JSON file you just fiddled with.
Put in a player#.txt filename to re-encrypt and save as.

This will also save over the player#_CLOUD.txt file otherwise the game will fuss about mismatches.