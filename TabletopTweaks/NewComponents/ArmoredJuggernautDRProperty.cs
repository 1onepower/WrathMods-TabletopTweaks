﻿using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Items.Armors;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.UnitLogic.Mechanics.Properties;

namespace TabletopTweaks.NewComponents {

    [TypeId("83b5b02d9c3f4190be0eadbd2b14b23a")]
    public class ArmoredJuggernautDRProperty : PropertyValueGetter {

        private static BlueprintCharacterClass FighterClass = Resources.GetBlueprint<BlueprintCharacterClass>("48ac8db94d5de7645906c7d0ad3bcfbd");

        public override int GetBaseValue(UnitEntityData unit) {
            if (!unit.Body.Armor.HasArmor)
                return 0;

            var armorProficiencyGroup = unit.Body.Armor.Armor.Blueprint.ProficiencyGroup;
            int fighterLevel = FighterArmorTrainingProperty.Get().GetInt(unit);

            if (fighterLevel == 0)
                return 0;

            if (armorProficiencyGroup == ArmorProficiencyGroup.Light) {
                if (fighterLevel >= 11)
                    return 1;
                else
                    return 0;
            } else if (armorProficiencyGroup == ArmorProficiencyGroup.Medium) {
                if (fighterLevel >= 11)
                    return 2;
                else if (fighterLevel >= 7)
                    return 1;
                else
                    return 0;
            } else if (armorProficiencyGroup == ArmorProficiencyGroup.Heavy) {
                if (fighterLevel >= 11)
                    return 3;
                else if (fighterLevel >= 7)
                    return 2;
                else
                    return 1;
            } else {
                return 0;
            }
        }

        public BlueprintUnitPropertyReference FighterArmorTrainingProperty;
    }
}
