﻿using HarmonyLib;
using Kingmaker.Blueprints.JsonSystem;

namespace TabletopTweaks.NewContent {
    class ContentAdder {
        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_Patch {
            static bool Initialized;

            [HarmonyPriority(Priority.First)]
            static void Postfix() {
                if (Initialized) return;
                Initialized = true;
                Main.LogHeader("Loading New Content");
                BaseAbilities.OneHandedToggleAbility.AddOneHandedToggle();

                Features.MartialWeaponProficencySelection.AddMartialWeaponProficencySelection();
                //Features.NauseatedPoision.AddNauseatedPoision();

                Spells.LongArms.AddLongArms();
                Spells.ShadowEnchantment.AddShadowEnchantment();
                Spells.ShadowEnchantment.AddShadowEnchantmentGreater();
                Spells.MagicalTailSpells.AddNewMagicalTailSpells();

                //Added early as some things depend on them for selections
                Feats.IntensifiedSpell.AddIntensifiedSpell();
                Feats.RimeSpell.AddRimeSpell();
                Feats.BurningSpell.AddBurningSpell();
                Feats.FlaringSpell.AddFlaringSpell();
                Feats.PiercingSpell.AddPiercingSpell();
                Feats.SolidShadows.AddSolidShadows();

                Templates.AlignmentTemplates.AddCelestialTemplate();
                Templates.AlignmentTemplates.AddEntropicTemplate();
                Templates.AlignmentTemplates.AddFiendishTemplate();
                Templates.AlignmentTemplates.AddResoluteTemplate();

                WeaponEnchantments.NonStackingTempEnchantments.AddWeaponEnhancements();
                WeaponEnchantments.TwoHandedDamageMultiplier.AddTwoHandedDamageMultiplierEnchantment();
                WeaponEnchantments.TerrifyingTremble.AddTerrifyingTrembleEnchant();

                Races.Dwarf.AddDwarfHeritage();
                Races.Elf.AddElfHeritage();
                Races.Gnome.AddGnomeHeritage();
                Races.Halfling.AddHalflingHeritage();

                Backgrounds.Lecturer.AddLecturer();

                ArcanistExploits.QuickStudy.AddQuickStudy();
                ArcanistExploits.ItemCrafting.AddItemCrafting();
                ArcanistExploits.MetamagicKnowledge.AddMetamagicKnowledge();
                ArcanistExploits.Familiar.AddFamiliar();

                MagusArcana.SpellBlending.AddSpellBlending();
                MagusArcana.BroadStudy.AddBroadStudy();

                WizardArcaneDiscoveries.ArcaneDiscoverySelection.AddArcaneDiscoverySelection();
                WizardArcaneDiscoveries.AlchemicalAffinity.AddAlchemicalAffinity();
                WizardArcaneDiscoveries.Idealize.AddIdealize();
                WizardArcaneDiscoveries.KnowledgeIsPower.AddKnowledgeIsPower();
                WizardArcaneDiscoveries.OppositionResearch.AddOppositionResearch();
                WizardArcaneDiscoveries.YuelralsBlessing.AddYuelralsBlessing();

                Features.AeonBaneIncreaseResourceFeature.AddAeonBaneIncreaseResourceFeature();
                Features.PrimalistRagePowerSelection.AddPrimalistRagePowerSelection();
                Features.LongspearChargeBuff.AddLongspearChargeBuff();
                Features.PerfectStrikeZenArcherBuff.AddPerfectStrikeZenArcherBuff();
                Features.DragonDiscipleSpellbooks.AddDragonDiscipleSpellbooks();
                Features.FighterTrainingFakeLevel.AddFighterTrainingFakeLevel();

                FighterAdvancedWeaponTrainings.AdvancedWeapontrainingSelection.AddAdvancedWeaponTrainingSelection();
                FighterAdvancedWeaponTrainings.DefensiveWeaponTraining.AddDefensiveWeaponTraining();
                FighterAdvancedWeaponTrainings.FocusedWeapon.AddFocusedWeapon();
                FighterAdvancedWeaponTrainings.TrainedThrow.AddTrainedThrow();
                FighterAdvancedWeaponTrainings.TrainedGrace.AddTrainedGrace();
                FighterAdvancedWeaponTrainings.WarriorSpirit.AddWarriorSpirit();

                FighterAdvancedArmorTrainings.AdvancedArmorTraining.AddAdvancedArmorTraining();
                FighterAdvancedArmorTrainings.ArmoredConfidence.AddArmoredConfidence();
                FighterAdvancedArmorTrainings.ArmoredJuggernaut.AddArmoredJuggernaut();
                FighterAdvancedArmorTrainings.ArmorSpecialization.AddArmorSpecialization();
                FighterAdvancedArmorTrainings.CriticalDeflection.AddCriticalDeflection();
                FighterAdvancedArmorTrainings.SteelHeadbutt.AddSteelHeadbutt();

                Bloodlines.BloodlineRequisiteFeature.AddBloodlineRequisiteFeature();
                Bloodlines.AberrantBloodline.AddBloodragerAberrantBloodline();
                Bloodlines.AberrantBloodline.AddSorcererAberrantBloodline();
                Bloodlines.DestinedBloodline.AddBloodragerDestinedBloodline();
                Bloodlines.DestinedBloodline.AddSorcererDestinedBloodline();
                Bloodlines.AbyssalBloodline.AddBloodragerAbyssalDemonicBulkEnlargeBuff();
                Bloodlines.BloodragerArcaneBloodline.AddArcaneBloodrageReworkToggles();

                Domains.TricksterDomains.AddTricksterDomains();
                //Features to support existing clases
                Classes.Cavalier.AddCavalierFeatures();
                Classes.Oracle.AddOracleFeatures();
                Classes.Magus.AddMagusFeatures();
                Classes.Lich.AddLichFeatures();
                //Features to support existing archetypes
                Archetypes.MadDog.AddMadDogFeatures();
                //New archetypes
                Archetypes.BladeBound.AddBlackBlade(); //Comes before all archetypes that use black blade
                Archetypes.BladeBound.AddBladeBound();
                Archetypes.BladeAdept.AddBladeAdept();
                Archetypes.CauldronWitch.AddCauldrenWitch();
                Archetypes.ElementalMaster.AddElementalMaster();
                Archetypes.MetamagicRager.AddMetamagicRager();
                Archetypes.DivineCommander.AddDivineCommander();
                Archetypes.NatureFang.AddNatureFang();
                Archetypes.ChannelerOfTheUnknown.AddChannelerOfTheUnknown();
                Archetypes.Myrmidarch.AddMyrmidarch();
                //Features to support existing prestige clases
                Classes.Loremaster.AddLoremasterFeatures();

                MythicAbilities.ImpossibleSpeed.AddImpossibleSpeed();
                MythicAbilities.ArmorMaster.AddArmorMaster();
                MythicAbilities.ArmoredMight.AddArmoredMight();
                MythicAbilities.MountedManiac.AddMountedManiac();
                MythicAbilities.MythicSpellCombat.AddMythicSpellCombat();
                MythicAbilities.PrecisionCritical.AddPrecisionCritical();
                MythicAbilities.AbundantBlessing.AddAbundantBlessing();
                MythicAbilities.AbundantBombs.AddAbundantBombs();
                MythicAbilities.AbundantFervor.AddAbundantFervor();
                MythicAbilities.AbundantIncense.AddAbundantIncense();
                MythicAbilities.AbundantLayOnHands.AddAbundantLayOnHands();
                MythicAbilities.HarmoniousMage.AddHarmoniousMage();
                MythicAbilities.SecondPatron.AddSecondPatron();
                MythicAbilities.EnhancedBlessings.AddEnhancedBlessings();
                MythicAbilities.ImpossibleBlessing.AddImpossibleBlessing();

                MythicAbilities.FavoriteMetamagicPersistent.AddFavoriteMetamagicPersistent();
                MythicAbilities.FavoriteMetamagicSelective.AddFavoriteMetamagicSelective();
                MythicAbilities.DimensionalRetribution.AddDimensionalRetribution();

                Feats.ShatterDefenses.AddNewShatterDefenseBlueprints();
                Feats.MagicalAptitude.AddMagicalAptitude();
                Feats.Scholar.AddScholar();
                Feats.SelfSufficient.AddSelfSufficient();
                Feats.ShingleRunner.AddShingleRunner();
                Feats.StreetSmarts.AddStreetSmarts();
                Feats.GracefulAthlete.AddGracefulAthlete();
                Feats.DervishDance.AddDervishDance();
                Feats.NatureSoul.AddNatureSoul();
                Feats.AnimalAlly.AddAnimalAlly();
                Feats.SpellSpecializationGreater.AddSpellSpecializationGreater();
                Feats.Stalwart.AddStalwart();
                Feats.CelestialServant.AddCelestialServant();
                Feats.ImprovedChannel.AddImprovedChannel();
                Feats.QuickChannel.AddQuickChannel();
                Feats.ErastilsBlessing.AddErastilsBlessing();
                Feats.QuickDraw.AddQuickDraw();
                Feats.UndersizedMount.AddUndersizedMount();
                Feats.TrickRiding.AddTrickRiding();
                Feats.MountedSkirmisher.AddMountedSkirmisher();
                Feats.LungingSpellTouch.AddLungingSpellTouch();
                Feats.HorseMaster.AddHorseMaster();
                Feats.DispelFocus.AddDispelFocus();
                Feats.TwoWeaponDefense.AddTwoWeaponDefense();
                Feats.VarisianTattoo.AddVarisianTattoo();
                Feats.QuickenBlessing.AddQuickenBlessing();

                Feats.ExtraReservoir.AddExtraReservoir();
                Feats.ExtraHex.AddExtraHex();
                Feats.ExtraArcanistExploit.AddExtraArcanistExploit();
                Feats.ExtraArcana.AddExtraArcana();
                Feats.ExtraKi.AddExtraKi();
                Feats.ExtraRogueTalent.AddExtraRogueTalent();
                Feats.ExtraSlayerTalent.AddExtraSlayerTalent();
                Feats.ExtraRevelation.AddExtraRevelation();
                Feats.ExtraDiscovery.AddExtraDiscovery();
                Feats.ExtraMercy.AddExtraMercy();

                MythicFeats.MythicShatterDefenses.AddMythicShatterDefenses();
                MythicFeats.MythicCombatReflexes.AddMythicCombatReflexes();
                MythicFeats.MythicWarriorPriest.AddMythicWarriorPriest();
                MythicFeats.MythicIntimidatingProwess.AddMythicIntimidatingProwess();
                MythicFeats.TitanStrike.AddTitanStrike();
                MythicFeats.MythicTwoWeaponDefense.AddMythicTwoWeaponDefense();
                MythicFeats.MythicManyshot.AddMythicManyshot();
                MythicFeats.MythicCombatExpertise.AddMythicCombatExpertise();

                AlternateCapstones.MasterfulTalent.AddMasterfulTalent();
            }
        }
    }
}
