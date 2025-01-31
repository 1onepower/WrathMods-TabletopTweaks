﻿using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Blueprints.Root.Strings;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Class.LevelUp;
using System.Text;

namespace TabletopTweaks.NewComponents.Prerequisites {
    [TypeId("1cdf85e299bf474d89e2ce827aa6ecbf")]
    public class PrerequisiteSpellBookType : Prerequisite {
        public override bool CheckInternal(FeatureSelectionState selectionState, UnitDescriptor unit, LevelUpState state) {
            int? casterTypeSpellLevel = this.GetCasterTypeSpellLevel(unit);
            return (casterTypeSpellLevel.GetValueOrDefault() >= RequiredSpellLevel) && (casterTypeSpellLevel != null);
        }

        public override string GetUITextInternal(UnitDescriptor unit) {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"Can cast {Type} spells of level {RequiredSpellLevel} or higher");
            int? casterTypeSpellLevel = this.GetCasterTypeSpellLevel(unit);
            if (unit != null && casterTypeSpellLevel != null) {
                stringBuilder.Append("\n");
                stringBuilder.Append(string.Format(UIStrings.Instance.Tooltips.CurrentValue, casterTypeSpellLevel));
            }
            return stringBuilder.ToString();
        }

        private int? GetCasterTypeSpellLevel(UnitDescriptor unit) {
            foreach (ClassData classData in unit.Progression.Classes) {
                BlueprintSpellbook spellbook = classData.Spellbook;
                if (spellbook == null) { continue; }
                var correctType = Type switch {
                    SpellbookType.Prepared => !spellbook.Spontaneous || spellbook.IsArcanist,
                    SpellbookType.Spontaneous => spellbook.Spontaneous || !spellbook.IsArcanist,
                    _ => false
                };
                if (!spellbook.IsMythic && !spellbook.IsAlchemist && correctType) {
                    return new int?(unit.DemandSpellbook(classData.CharacterClass).MaxSpellLevel);
                }
            }
            return null;
        }
        public enum SpellbookType : int {
            Prepared,
            Spontaneous
        }

        public SpellbookType Type;
        public int RequiredSpellLevel;
    }
}
