﻿using HarmonyLib;
using JetBrains.Annotations;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TabletopTweaks.Config {
    public class Blueprints : IUpdatableSettings {
        [JsonProperty]
        private bool OverrideIds = false;
        [JsonProperty]
        private readonly SortedDictionary<string, Guid> NewBlueprints = new SortedDictionary<string, Guid>();
        [JsonProperty]
        private readonly SortedDictionary<string, Guid> DerivedBlueprintMasters = new SortedDictionary<string, Guid>();
        [JsonProperty]
        private readonly SortedDictionary<string, Guid> DerivedBlueprints = new SortedDictionary<string, Guid>();
        [JsonProperty]
        private readonly SortedDictionary<string, Guid> AutoGenerated = new SortedDictionary<string, Guid>();
        [JsonProperty]
        private readonly SortedDictionary<string, Guid> UnusedGUIDs = new SortedDictionary<string, Guid>();
        private readonly SortedDictionary<string, Guid> UsedGUIDs = new SortedDictionary<string, Guid>();

        public void OverrideSettings(IUpdatableSettings userSettings) {
            var loadedSettings = userSettings as Blueprints;
            if (loadedSettings == null) { return; }
            if (loadedSettings.OverrideIds) {
                OverrideIds = loadedSettings.OverrideIds;
                loadedSettings.NewBlueprints.ForEach(entry => {
                    if (NewBlueprints.ContainsKey(entry.Key)) {
                        NewBlueprints[entry.Key] = entry.Value;
                    }
                });
                loadedSettings.DerivedBlueprintMasters.ForEach(entry => {
                    if (DerivedBlueprintMasters.ContainsKey(entry.Key)) {
                        DerivedBlueprintMasters[entry.Key] = entry.Value;
                    }
                });
            }
            loadedSettings.DerivedBlueprints.ForEach(entry => {
                DerivedBlueprints[entry.Key] = entry.Value;
            });
            loadedSettings.AutoGenerated.ForEach(entry => {
                AutoGenerated[entry.Key] = entry.Value;
            });
        }
        public BlueprintGuid GetGUID(string name) {

            Guid Id;
            if (!NewBlueprints.TryGetValue(name, out Id)) {
                if (!DerivedBlueprints.TryGetValue(name, out Id)) {
#if DEBUG
                    if (!AutoGenerated.TryGetValue(name, out Id)) {
                        Id = Guid.NewGuid();
                        AutoGenerated.Add(name, Id);
                        Main.LogDebug($"Generated new GUID: {name} - {Id}");
                    } else {
                        Main.LogDebug($"WARNING: GUID: {name} - {Id} is autogenerated");
                    }
#endif
                }
            }
            if (Id == null) { Main.Error($"ERROR: GUID for {name} not found"); }
            UsedGUIDs[name] = Id;
            return new BlueprintGuid(Id);
        }

        public BlueprintGuid GetDerivedMaster(string name) {

            Guid Id;
            if (!DerivedBlueprintMasters.TryGetValue(name, out Id)) {
#if DEBUG
                if (!AutoGenerated.TryGetValue(name, out Id)) {
                    Id = Guid.NewGuid();
                    DerivedBlueprintMasters.Add(name, Id);
                    AutoGenerated.Add(name, Id);
                    Main.LogDebug($"WARNING: MASTER GUID: {name} - {Id} is autogenerated");
                }
#endif
            }
            if (Id == null) { Main.Error($"ERROR: MASTER GUID {name} not found"); }
            UsedGUIDs[name] = Id;
            return new BlueprintGuid(Id);
        }

        public BlueprintGuid GetDerivedGUID(string name, [NotNull] BlueprintGuid masterId, [NotNull] params BlueprintGuid[] componentIds) {
            Guid Id;
            if (!DerivedBlueprints.TryGetValue(name, out Id)) {
                NewBlueprints.TryGetValue(name, out Id);
            }
            if (Id == null || Id == Guid.Empty) {
                return DeriveGUID(name, masterId, componentIds);
            }
            UsedGUIDs[name] = Id;
            return new BlueprintGuid(Id);
        }

        public BlueprintGuid DeriveGUID(string name, [NotNull] BlueprintGuid masterId, [NotNull] params BlueprintGuid[] componentIds) {
            BlueprintGuid derivedID = componentIds.Aggregate(masterId, (aggregateID, componentID) => {
                byte[] aggregateBytes = aggregateID.ToByteArray();
                byte[] componentBytes = componentID.ToByteArray();
                for (int i = 0; i < aggregateBytes.Length; i++) {
                    aggregateBytes[i] = (byte)(aggregateBytes[i] ^ componentBytes[i]);
                }
                return new BlueprintGuid(new Guid(aggregateBytes));
            });
            UsedGUIDs[name] = derivedID.m_Guid;
            DerivedBlueprints.Add(name, derivedID.m_Guid);
            return derivedID;
        }

        public void Init() {
        }

        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class AutoGUID_Log_Patch {

            [HarmonyPriority(Priority.Last)]
            static void Postfix() {
                GenerateUnused();
                ModSettings.SaveSettings("Blueprints.json", ModSettings.Blueprints);
            }
            static void GenerateUnused() {
                ModSettings.Blueprints.AutoGenerated.ForEach(entry => {
                    if (!ModSettings.Blueprints.UsedGUIDs.ContainsKey(entry.Key)) {
                        ModSettings.Blueprints.UnusedGUIDs[entry.Key] = entry.Value;
                    }
                });
                ModSettings.Blueprints.NewBlueprints.ForEach(entry => {
                    if (!ModSettings.Blueprints.UsedGUIDs.ContainsKey(entry.Key)) {
                        ModSettings.Blueprints.UnusedGUIDs[entry.Key] = entry.Value;
                    }
                });
            }
        }
    }
}
