TO-DO: work more on this

Avalon's Mod Calls:
- AddRiftGogglesXTierOre (int tileID) (where X is the any of the following: Copper, Iron, Silver, Gold, Rhodium, Evil, Cobalt, Mythril, or Adamantite):
  * Adds the given tile ID to the given ore tier (not necessary if your mod uses Alternatives Library to add your alternate ores)
- AddHerbologyHerbData (int seedID, int herbID, int largeSeedID, int largeHerbID):
  * Adds normal seed, normal herb, large seed, and large herb to the Herbology Bench via their IDs
- AddHerbologyPotionData (int potionID, int elixirID):
  * Adds potion and elixir to the Herbology Bench via their IDs
- AddTorchLauncherLightColor (int, Vector3):
  * Set this to  { <torch_item_type>, new Vector3(R, G, B) } where the RGB values are floats between 0f and 1f
- AddTorchLauncherDust (int, int):
  * Set this to { <torch_item_type>, <dust_type> }
- AddTorchLauncherTexture (int, string):
  * Set this to { <torch_item_type>, <file_path> } where the file path is the texture of your torch’s tile
- AddTorchLauncherFlameTexture (int, string):
  * Set this to { <torch_item_type>, <file_path> } where the file path is the texture of your torch tile’s flame overlay
- AddTorchLauncherDebuffType (int, int):
  * Set this to { <torch_item_type>, <buffID> } if you want your torch to inflict a debuff other than On Fire
- AddBiomeChest (List<int> listContainingKeyAndLoot):
  * Where 'listContainingKeyAndLoot' is a List<int> with index 0 being the key item ID and starting from index 1 are the loot item IDs
- 
