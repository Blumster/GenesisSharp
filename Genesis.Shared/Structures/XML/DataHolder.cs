using System.Collections.Generic;
using System.Xml.Serialization;

namespace Genesis.Shared.Structures.XML
{
   [XmlRoot("xml")]
    public class DataHolder
    {
        /*[XmlArray("Results")]                  [XmlArrayItem("row")] public List<Result>                  Results;
        [XmlArray("tAchievement")]             [XmlArrayItem("row")] public List<Achievement>             Achievements;
        [XmlArray("tArena")]                   [XmlArrayItem("row")] public List<Arena>                   Arenas;
        [XmlArray("tBonusData")]               [XmlArrayItem("row")] public List<BonusData>               BonusDatas;
        [XmlArray("tCategory")]                [XmlArrayItem("row")] public List<Category>                Categories;
        [XmlArray("tColor")]                   [XmlArrayItem("row")] public List<Color>                   Colors;
        [XmlArray("tConfigCosts")]             [XmlArrayItem("row")] public List<ConfigCost>              ConfigCosts;*/
        [XmlArray("tConfigNewCharacters")]     [XmlArrayItem("row")] public List<ConfigNewCharacter>      ConfigNewCharacters;/*
        [XmlArray("tConsumables")]             [XmlArrayItem("row")] public List<Consumable>              Consumables;
        [XmlArray("tContinentExploredAreas")]  [XmlArrayItem("row")] public List<ContinentExploredArea>   ContinentExploredAreas;*/
        [XmlArray("tContinentObject")]         [XmlArrayItem("row")] public List<ContinentObject>         ContinentObjects;/*
        [XmlArray("tCreatureAI")]              [XmlArrayItem("row")] public List<CreatureAI>              CreatureAIs;
        [XmlArray("tCreatureEnhancement")]     [XmlArrayItem("row")] public List<CreatureEnhancement>     CreatureEnhancements;
        [XmlArray("tCreatureExperienceLevel")] [XmlArrayItem("row")] public List<CreatureExperienceLevel> CreatureExperienceLevels;
        [XmlArray("tDiscipline")]              [XmlArrayItem("row")] public List<Discipline>              Disciplines;
        [XmlArray("tExperienceLevel")]         [XmlArrayItem("row")] public List<ExperienceLevel>         ExperienceLevels;
        [XmlArray("tFactions")]                [XmlArrayItem("row")] public List<Faction>                 Factions;
        [XmlArray("tHeadBody")]                [XmlArrayItem("row")] public List<HeadBody>                HeadBodies;
        [XmlArray("tHeadDetail")]              [XmlArrayItem("row")] public List<HeadDetail>              HeadDetails;
        [XmlArray("tItemTemplate")]            [XmlArrayItem("row")] public List<ItemTemplate>            ItemTemplates;
        [XmlArray("tLootConfig")]              [XmlArrayItem("row")] public List<LootConfig>              LootConfigs;
        [XmlArray("tLootRarity")]              [XmlArrayItem("row")] public List<LootRarity>              LootRarities;
        [XmlArray("tLootTable")]               [XmlArrayItem("row")] public List<LootTable>               LootTables;
        [XmlArray("tLootWeights")]             [XmlArrayItem("row")] public List<LootWeight>              LootWeights;
        [XmlArray("tMapScaler")]               [XmlArrayItem("row")] public List<MapScaler>               MapScalers;
        [XmlArray("tModule")]                  [XmlArrayItem("row")] public List<Module>                  Modules;
        [XmlArray("tNPCContinentObject")]      [XmlArrayItem("row")] public List<NPCContinentObject>      NPCContinentObjects;
        [XmlArray("tNPCDialogueRandom")]       [XmlArrayItem("row")] public List<NPCDialogueRandom>       NPCDialogueRandoms;
        [XmlArray("tObjTypeRef")]              [XmlArrayItem("row")] public List<ObjTypeRef>              ObjTypeRefs;
        [XmlArray("tOutpost")]                 [XmlArrayItem("row")] public List<Outpost>                 Outposts;
        [XmlArray("tPrefixCreature")]          [XmlArrayItem("row")] public List<PrefixCreature>          PrefixCreatures;
        [XmlArray("tPrefixWeight")]            [XmlArrayItem("row")] public List<PrefixWeight>            PrefixWeights;
        [XmlArray("tQuestBaseCredits")]        [XmlArrayItem("row")] public List<QuestBaseCredit>         QuestBaseCredits;
        [XmlArray("tQuestCreditsLookup")]      [XmlArrayItem("row")] public List<QuestCreditsLookup>      QuestCreditsLookups;
        [XmlArray("tQuestXPLookup")]           [XmlArrayItem("row")] public List<QuestXPLookup>           QuestXPLookups;
        [XmlArray("tRegion")]                  [XmlArrayItem("row")] public List<Region>                  Regions;
        [XmlArray("tRegionBorders")]           [XmlArrayItem("row")] public List<RegionBorder>            RegionBorders;
        [XmlArray("tRegionFactions")]          [XmlArrayItem("row")] public List<RegionFaction>           RegionFactions;
        [XmlArray("tRegionMaps")]              [XmlArrayItem("row")] public List<RegionMap>               RegionMaps;
        [XmlArray("tRemovedObjects")]          [XmlArrayItem("row")] public List<RemovedObject>           RemovedObjects;
        [XmlArray("tStoreInventory")]          [XmlArrayItem("row")] public List<StoreInventory>          StoreInventory;
        [XmlArray("tTile")]                    [XmlArrayItem("row")] public List<Tile>                    Tiles;
        [XmlArray("tTileSet")]                 [XmlArrayItem("row")] public List<TileSet>                 TileSets;
        [XmlArray("tTreasureWeight")]          [XmlArrayItem("row")] public List<TreasureWeight>          TreasureWeights;
        [XmlArray("tTypeNames")]               [XmlArrayItem("row")] public List<TypeName>                TypeNames;
        [XmlArray("tVehicleTemplate")]         [XmlArrayItem("row")] public List<VehicleTemplate>         VehicleTemplates;
        [XmlArray("tVersionConfig")]           [XmlArrayItem("row")] public List<VersionConfig>           VersionConfigs;
        [XmlArray("tWeaponGroup")]             [XmlArrayItem("row")] public List<WeaponGroup>             WeaponGroups;
        [XmlArray("tWeaponGroup_x")]           [XmlArrayItem("row")] public List<WeaponGroupX>            WeaponGroupxs;
        [XmlArray("vConsumables")]             [XmlArrayItem("row")] public List<Consumable>              Consumables2;
        [XmlArray("vLootBaseItems")]           [XmlArrayItem("row")] public List<LootBaseItem>            LootBaseItems;
        [XmlArray("vGeneratableCreatures")]    [XmlArrayItem("row")] public List<GeneratableCreature>     GeneratableCreatures;
        [XmlArray("vModule_All")]              [XmlArrayItem("row")] public List<Module>                  ModulesAll;
        [XmlArray("vHeadBody_Character")]      [XmlArrayItem("row")] public List<HeadBodyCharacter>       HeadBodyCharacters;
        [XmlArray("vWeaponGroup")]             [XmlArrayItem("row")] public List<WeaponGroup>             WeaponGroups2;
        [XmlArray("vWeaponGroup_x")]           [XmlArrayItem("row")] public List<WeaponGroupX>            WeaponGroupXs2;
        [XmlArray("vRaceSpecificItems")]       [XmlArrayItem("row")] public List<RaceSpecificItem>        RaceSpecificItems;
        [XmlArray("vRandomDialogues")]         [XmlArrayItem("row")] public List<RandomDialogue>          RandomDialogues;
        [XmlArray("vColorBiomek")]             [XmlArrayItem("row")] public List<Color>                   BiomekColors;
        [XmlArray("vColorHuman")]              [XmlArrayItem("row")] public List<Color>                   HumanColors;
        [XmlArray("vColorMutant")]             [XmlArrayItem("row")] public List<Color>                   MutantColors;
        [XmlArray("vDisciplines")]             [XmlArrayItem("row")] public List<Discipline>              Disciplines2;*/
    }
}
