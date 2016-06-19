namespace DotAllCombo.Heroes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;
    using Ensage.Common.Menu;
    using Extensions;

    internal class LionController : HeroController
    {
        private static Ability Q, W, E, R;
        private static Hero me, target;
        private static Menu menu;
        private static readonly Menu skills = new Menu("Skills", "Skills");
        private static readonly Menu items = new Menu("Items", "Items");
        private static readonly Menu link = new Menu("Link", "Link");
        private static readonly Menu ult = new Menu("AutoUlt", "AutoUlt");
        private static bool Active;

        private static Item orchid,
            sheep,
            vail,
            soulring,
            arcane,
            blink,
            shiva,
            dagon,
            atos,
            ethereal,
            cheese,
            ghost,
            core,
            force,
            cyclone;

        private static int[] rDmg;

        public override void OnInject()
        {
            AssemblyExtensions.InitAssembly("NeverMore", "0.1");

            DebugExtensions.Chat.PrintSuccess("To destroy the darkness in itself!");

            menu = MenuExtensions.GetMenu();
            me = Variables.me;
            menu.AddItem(new MenuItem("enabled", "Enabled").SetValue(true));
            menu.AddItem(new MenuItem("keyBind", "Combo key").SetValue(new KeyBind('D', KeyBindType.Press)));

            var Skills = new Dictionary<string, bool>
            {
                {"lion_voodoo", true},
                {"lion_impale", true},
                {"lion_finger_of_death", true}
            };
            var AutoUlt = new Dictionary<string, bool>
            {
                {"lion_finger_of_death", true}
            };
            var Link = new Dictionary<string, bool>
            {
                {"lion_mana_drain", true},
                {"item_force_staff", true},
                {"item_cyclone", true},
                {"item_orchid", true},
                {"item_rod_of_atos", true},
            };
            var Items = new Dictionary<string, bool>
            {
                {"item_dagon", true},
                {"item_orchid", true},
                {"item_ethereal_blade", true},
                {"item_veil_of_discord", true},
                {"item_rod_of_atos", true},
                {"item_sheepstick", true},
                {"item_arcane_boots", true},
                {"item_blink", true},
                {"item_soul_ring", true},
                {"item_ghost", true},
                {"item_cheese", true}
            };


            skills.AddItem(new MenuItem("Skills", "Skills").SetValue(new AbilityToggler(Skills)));
            items.AddItem(new MenuItem("Items", "Items:").SetValue(new AbilityToggler(Items)));
            ult.AddItem(new MenuItem("AutoUlt: ", "AutoUlt").SetValue(new AbilityToggler(AutoUlt)));
            items.AddItem(new MenuItem("Link: ", "Auto triggre Linken").SetValue(new AbilityToggler(Link)));
            menu.AddSubMenu(skills);
            menu.AddSubMenu(items);
            menu.AddSubMenu(ult);
        }

        public override void OnUpdateEvent(EventArgs args)
        {
            me = ObjectManager.LocalHero;

            if (!Game.IsInGame || me == null || Game.IsWatchingGame)
            {
                return;
            }

            target = me.ClosestToMouseTarget(2000);
            if (target == null)
            {
                return;
            }

            //spell
            Q = me.Spellbook.SpellQ;

            W = me.Spellbook.SpellW;

            E = me.Spellbook.SpellE;

            R = me.Spellbook.SpellR;

            // Item
            ethereal = me.FindItem("item_ethereal_blade");

            sheep = target.ClassID == ClassID.CDOTA_Unit_Hero_Tidehunter ? null : me.FindItem("item_sheepstick");

            vail = me.FindItem("item_veil_of_discord");

            cheese = me.FindItem("item_cheese");

            ghost = me.FindItem("item_ghost");

            orchid = me.FindItem("item_orchid");

            atos = me.FindItem("item_rod_of_atos");

            soulring = me.FindItem("item_soul_ring");

            arcane = me.FindItem("item_arcane_boots");

            blink = me.FindItem("item_blink");

            shiva = me.FindItem("item_shivas_guard");

			dagon = me.Inventory.Items.FirstOrDefault(item => item.Name.Contains("item_dagon"));

			Active = Game.IsKeyDown(menu.Item("keyBind").GetValue<KeyBind>().Key) && !Game.IsChatOpen;


            var ModifRod = target.Modifiers.Any(y => y.Name == "modifier_rod_of_atos_debuff");
            var ModifEther = target.Modifiers.Any(y => y.Name == "modifier_item_ethereal_blade_slow");
            var ModifVail = target.Modifiers.Any(y => y.Name == "modifier_item_veil_of_discord_debuff");
            var stoneModif = target.Modifiers.Any(y => y.Name == "modifier_medusa_stone_gaze_stone");

            if (Active && me.IsAlive && target.IsAlive)
            {
                target = me.ClosestToMouseTarget(2000);
                var noBlade = target.Modifiers.Any(y => y.Name == "modifier_item_blade_mail_reflect");
                if (target.IsVisible && me.Distance2D(target) <= 2300 && !noBlade && !target.IsLinkensProtected())
                {
                   
                    if ( // atos Blade
                        atos != null
                        && atos.CanBeCasted()
                        && me.CanCast()
                        && !target.IsLinkensProtected()
                        && !target.IsMagicImmune()
                        && menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(atos.Name)
                        && me.Distance2D(target) <= 1500
                        && Utils.SleepCheck("atos")
                        )
                    {
                        atos.UseAbility(target);

                        Utils.Sleep(250, "atos");
                    } // atos Item end
                    if (
                        W != null
                        && W.CanBeCasted()
                        && !target.IsHexed()
                        && me.CanCast()
                        && me.Distance2D(target) <= 700
                        && menu.Item("Skills").GetValue<AbilityToggler>().IsEnabled(W.Name)
                        && Utils.SleepCheck("W"))
                    {
                        W.UseAbility(target);
                        Utils.Sleep(300, "W");
                    }
                    if (
                        blink != null
                        && me.CanCast()
                        && blink.CanBeCasted()
                        && me.Distance2D(target) > 1000
                        && menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(blink.Name)
                        && Utils.SleepCheck("blink")
                        )
                    {
                        blink.UseAbility(target.Position);

                        Utils.Sleep(250, "blink");
                    }
                    if ( // orchid
                        orchid != null
                        && orchid.CanBeCasted()
                        && me.CanCast()
                        && !target.IsLinkensProtected()
                        && !target.IsMagicImmune()
                        && me.Distance2D(target) <= 1400
                        && menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(orchid.Name)
                        && !stoneModif
                        && Utils.SleepCheck("orchid")
                        )
                    {
                        orchid.UseAbility(target);
                        Utils.Sleep(250, "orchid");
                    } // orchid Item end
                    if (!orchid.CanBeCasted() || orchid == null ||
                        !menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(orchid.Name))
                    {
                        if ( // vail
                            vail != null
                            && vail.CanBeCasted()
                            && me.CanCast()
                            && !target.IsMagicImmune()
                            && menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(vail.Name)
                            && me.Distance2D(target) <= 1500
                            && Utils.SleepCheck("vail")
                            )
                        {
                            vail.UseAbility(target.Position);
                            Utils.Sleep(250, "vail");
                        } // orchid Item end
                        if (!vail.CanBeCasted() || vail == null ||
                            !menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(vail.Name))
                        {
                            if ( // ethereal
                                ethereal != null
                                && ethereal.CanBeCasted()
                                && me.CanCast()
                                && !target.IsLinkensProtected()
                                && !target.IsMagicImmune()
                                && !stoneModif
                                && menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(ethereal.Name)
                                && Utils.SleepCheck("ethereal")
                                )
                            {
                                ethereal.UseAbility(target);
                                Utils.Sleep(200, "ethereal");
                            } // ethereal Item end
                            if (!ethereal.CanBeCasted() || ethereal == null ||
                                !menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(ethereal.Name))
                            {
                                if (
                                    Q != null
                                    && Q.CanBeCasted()
                                    && me.CanCast()
									&& (target.IsLinkensProtected()
									|| !target.IsLinkensProtected())
									&& !W.CanBeCasted()
                                    && me.Distance2D(target) < 1400
                                    && !stoneModif
                                    && menu.Item("Skills").GetValue<AbilityToggler>().IsEnabled(Q.Name)
                                    && Utils.SleepCheck("Q")
                                    )
                                {
                                    Q.UseAbility(target);
                                    Utils.Sleep(200, "Q");
                                }
								if (
                                    R != null
                                    && R.CanBeCasted()
                                    && !W.CanBeCasted()
                                    && me.CanCast()
                                    && me.Position.Distance2D(target) < 1200
                                    && !stoneModif
                                    && menu.Item("Skills").GetValue<AbilityToggler>().IsEnabled(R.Name)
                                    && Utils.SleepCheck("R"))
                                {
                                    R.UseAbility(target);
                                    Utils.Sleep(330, "R");
                                }

                                if ( // SoulRing Item 
                                    soulring != null
                                    && soulring.CanBeCasted()
                                    && me.CanCast()
                                    && me.Health >= (me.MaximumHealth*0.4)
                                    && me.Mana <= R.ManaCost
                                    && menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(soulring.Name)
                                    )
                                {
                                    soulring.UseAbility();
                                } // SoulRing Item end

                                if ( // Arcane Boots Item
                                    arcane != null
                                    && arcane.CanBeCasted()
                                    && me.CanCast()
                                    && me.Mana <= R.ManaCost
                                    && menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(arcane.Name)
                                    )
                                {
                                    arcane.UseAbility();
                                } // Arcane Boots Item end

                                if ( //Ghost
                                    ghost != null
                                    && ghost.CanBeCasted()
                                    && me.CanCast()
                                    && ((me.Position.Distance2D(target) < 300
                                         && me.Health <= (me.MaximumHealth*0.7))
                                        || me.Health <= (me.MaximumHealth*0.3))
                                    && menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(ghost.Name)
                                    && Utils.SleepCheck("Ghost"))
                                {
                                    ghost.UseAbility();
                                    Utils.Sleep(250, "Ghost");
                                }


                                if ( // Shiva Item
                                    shiva != null
                                    && shiva.CanBeCasted()
                                    && me.CanCast()
                                    && !target.IsMagicImmune()
                                    && Utils.SleepCheck("shiva")
                                    && menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(shiva.Name)
                                    && me.Distance2D(target) <= 600
                                    )

                                {
                                    shiva.UseAbility();
                                    Utils.Sleep(250, "shiva");
                                } // Shiva Item end

                                if ( // sheep
                                    sheep != null
                                    && sheep.CanBeCasted()
                                    && me.CanCast()
                                    && !target.IsLinkensProtected()
                                    && !target.IsMagicImmune()
                                    && me.Distance2D(target) <= 1400
                                    && !stoneModif
                                    && menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(sheep.Name)
                                    && Utils.SleepCheck("sheep")
                                    )
                                {
                                    sheep.UseAbility(target);
                                    Utils.Sleep(250, "sheep");
                                } // sheep Item end

                                if ( // Dagon
                                    me.CanCast()
                                    && dagon != null
                                    && (ethereal == null
                                        || (ModifEther
                                            || ethereal.Cooldown < 17))
                                    && !target.IsLinkensProtected()
                                    && dagon.CanBeCasted()
                                    && !target.IsMagicImmune()
                                    && !stoneModif
                                    && Utils.SleepCheck("dagon")
                                    )
                                {
                                    dagon.UseAbility(target);
                                    Utils.Sleep(200, "dagon");
                                } // Dagon Item end

                                if (
                                    // cheese
                                    cheese != null
                                    && cheese.CanBeCasted()
                                    && me.Health <= (me.MaximumHealth*0.3)
                                    && me.Distance2D(target) <= 700
                                    && menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(cheese.Name)
                                    && Utils.SleepCheck("cheese")
                                    )
                                {
                                    cheese.UseAbility();
                                    Utils.Sleep(200, "cheese");
                                } // cheese Item end
                            }
                        }
                    }
                }
                if (
                    (!me.CanAttack()
                     || me.Distance2D(target) >= 350)
                    && me.NetworkActivity != NetworkActivity.Attack
                    && me.Distance2D(target) <= 900
                    && Utils.SleepCheck("Move"))
                {
                    me.Move(target.Predict(500));
                    Utils.Sleep(350, "Move");
                }
                if (
                    me.Distance2D(target) <= 100
                    && me.CanAttack()
                    && me.NetworkActivity != NetworkActivity.Attack
                    && Utils.SleepCheck("R")
                    )
                {
                    me.Attack(target);
                    Utils.Sleep(200, "R");
                }
            }
			if (me != null && me.IsAlive && target.Distance2D(me) <= 1000)
			{
				A();
			}
        }

        public static void A()
        {
            var enemies =
                ObjectManager.GetEntities<Hero>()
                    .Where(x => x.IsVisible && x.IsAlive && x.Team != me.Team && !x.IsIllusion);
			if (enemies == null || me==null || !me.IsAlive && !R.CanBeCasted())
				return;
            foreach (var e in enemies)
            {
                core = me.FindItem("item_octarine_core");
                force = me.FindItem("item_force_staff");
                cyclone = me.FindItem("item_cyclone");
                orchid = me.FindItem("item_orchid");
                atos = me.FindItem("item_rod_of_atos");

                if (menu.Item("AutoUlt: ").GetValue<AbilityToggler>().IsEnabled(R.Name))
                {
                    if (e == null)
                        return;

                    if (!me.AghanimState())
                        rDmg = new int[3] {600, 725, 850};
                    else
                        rDmg = new int[3] {725, 875, 1025};

                    var lens = me.Modifiers.Any(x => x.Name == "modifier_item_aether_lens");
                    var agh = (rDmg[R.Level - 1]);
                    var damage = Math.Floor(rDmg[R.Level - 1]*(1 - e.MagicDamageResist));
                    if (e.NetworkName == "CDOTA_Unit_Hero_Spectre" && e.Spellbook.Spell3.Level > 0)
                    {
                        damage =
                            Math.Floor(rDmg[R.Level - 1]*
                                       (1 - (0.10 + e.Spellbook.Spell3.Level*0.04))*(1 - e.MagicDamageResist));
                    }
                    if (e.NetworkName == "CDOTA_Unit_Hero_SkeletonKing" &&
                        e.Spellbook.SpellR.CanBeCasted())
                        damage = 0;
                    if (e.NetworkName == "CDOTA_Unit_Hero_Tusk" &&
                        e.Spellbook.SpellW.CooldownLength - 3 > e.Spellbook.SpellQ.Cooldown)
                        damage = 0;
                    if (lens) damage = damage*1.08;
                    var rum = e.Modifiers.Any(x => x.Name == "modifier_kunkka_ghost_ship_damage_absorb");
                    if (rum) damage = damage*0.5;
                    var mom = e.Modifiers.Any(x => x.Name == "modifier_item_mask_of_madness_berserk");
                    if (mom) damage = damage*1.3;

                    if (vail != null && e.Health <= (damage*1.25)
                        && vail.CanBeCasted()
                        && me.Distance2D(e) <= R.CastRange + 30
                        && R.CanBeCasted()
                        && me.Distance2D(e) <= 1200
                        && Utils.SleepCheck(e.Handle.ToString())
                        )
                    {
                        vail.UseAbility(e.Position);
                        Utils.Sleep(500, e.Handle.ToString());
                    }

                    if (ethereal != null && e.Health <= (damage*1.5)
                        && me.Distance2D(e) <= ethereal.CastRange + 30
                        && R.CanBeCasted()
                        && !e.IsMagicImmune()
                        && ethereal.CanBeCasted()
                        && !e.IsLinkensProtected()
                        && me.Distance2D(e) <= 1000
                        && Utils.SleepCheck(e.Handle.ToString())
                        )
                    {
                        ethereal.UseAbility(e);
                        Utils.Sleep(500, e.Handle.ToString());
                    }
                   
                        if (R != null && e != null && R.CanBeCasted()
                            && !e.Modifiers.Any(y => y.Name == "modifier_tusk_snowball_movement")
                            && !e.Modifiers.Any(y => y.Name == "modifier_snowball_movement_friendly")
                            && !e.Modifiers.Any(y => y.Name == "modifier_templar_assassin_refraction_absorb")
                            && !e.Modifiers.Any(y => y.Name == "modifier_ember_spirit_flame_guard")
                            &&
                            !e.Modifiers.Any(
                                y => y.Name == "modifier_ember_spirit_sleight_of_fist_caster_invulnerability")
                            &&
                            !e.Modifiers.Any(
                                y => y.Name == "modifier_ember_spirit_sleight_of_fist_caster_invulnerability")
                            && !e.Modifiers.Any(y => y.Name == "modifier_obsidian_destroyer_astral_imprisonment_prison")
                            && !e.Modifiers.Any(y => y.Name == "modifier_puck_phase_shift")
                            && !e.Modifiers.Any(y => y.Name == "modifier_eul_cyclone")
                            && !e.Modifiers.Any(y => y.Name == "modifier_dazzle_shallow_grave")
                            && !e.Modifiers.Any(y => y.Name == "modifier_shadow_demon_disruption")
                            && !e.Modifiers.Any(y => y.Name == "modifier_necrolyte_reapers_scythe")
                            && !e.Modifiers.Any(y => y.Name == "modifier_storm_spirit_ball_lightning")
                            && !e.Modifiers.Any(y => y.Name == "modifier_ember_spirit_fire_remnant")
                            && !e.Modifiers.Any(y => y.Name == "modifier_nyx_assassin_spiked_carapace")
                            && !e.Modifiers.Any(y => y.Name == "modifier_phantom_lancer_doppelwalk_phase")
                            && !e.FindSpell("abaddon_borrowed_time").CanBeCasted() &&
                            !e.Modifiers.Any(y => y.Name == "modifier_abaddon_borrowed_time_damage_redirect")
                            && !e.IsMagicImmune()
                            && e.Health <= (damage - 40)
                            && me.Distance2D(e) <= R.CastRange + 50
                            && Utils.SleepCheck(e.Handle.ToString())
                            )
                        {
                            R.UseAbility(e);
                            Utils.Sleep(150, e.Handle.ToString());
                            return;
                        }
                }
                if (W != null && e != null && W.CanBeCasted() && me.Distance2D(e) <= W.CastRange + 30
                    && !e.IsLinkensProtected()
                    &&
                    (
                        e.Modifiers.Any(y => y.Name == "modifier_meepo_earthbind")
                        || e.Modifiers.Any(y => y.Name == "modifier_pudge_dismember")
                        || e.Modifiers.Any(y => y.Name == "modifier_naga_siren_ensnare")
                        || e.Modifiers.Any(y => y.Name == "modifier_lone_druid_spirit_bear_entangle_effect")
                        || e.Modifiers.Any(y => y.Name == "modifier_legion_commander_duel")
                        || e.Modifiers.Any(y => y.Name == "modifier_kunkka_torrent")
                        || e.Modifiers.Any(y => y.Name == "modifier_ice_blast")
                        || e.Modifiers.Any(y => y.Name == "modifier_enigma_black_hole_pull")
                        || e.Modifiers.Any(y => y.Name == "modifier_ember_spirit_searing_chains")
                        || e.Modifiers.Any(y => y.Name == "modifier_dark_troll_warlord_ensnare")
                        || e.Modifiers.Any(y => y.Name == "modifier_crystal_maiden_frostbite")
                        || e.Modifiers.Any(y => y.Name == "modifier_axe_berserkers_call")
                        || e.Modifiers.Any(y => y.Name == "modifier_bane_fiends_grip")
                        ||
                        e.ClassID == ClassID.CDOTA_Unit_Hero_Magnataur &&
                        e.FindSpell("magnataur_reverse_polarity").IsInAbilityPhase
                        ||
                        e.ClassID == ClassID.CDOTA_Unit_Hero_Magnataur &&
                        e.FindSpell("magnataur_skewer").IsInAbilityPhase
                        || e.FindItem("item_blink").IsInAbilityPhase
                        ||
                        e.ClassID == ClassID.CDOTA_Unit_Hero_QueenOfPain &&
                        e.FindSpell("queenofpain_blink").IsInAbilityPhase
                        ||
                        e.ClassID == ClassID.CDOTA_Unit_Hero_AntiMage && e.FindSpell("antimage_blink").IsInAbilityPhase
                        ||
                        e.ClassID == ClassID.CDOTA_Unit_Hero_AntiMage &&
                        e.FindSpell("antimage_mana_void").IsInAbilityPhase
                        ||
                        e.ClassID == ClassID.CDOTA_Unit_Hero_DoomBringer &&
                        e.FindSpell("doom_bringer_doom").IsInAbilityPhase
                        || e.Modifiers.Any(y => y.Name == "modifier_rubick_telekinesis")
                        || e.Modifiers.Any(y => y.Name == "modifier_item_blink_dagger")
                        || e.Modifiers.Any(y => y.Name == "modifier_storm_spirit_electric_vortex_pull")
                        || e.Modifiers.Any(y => y.Name == "modifier_winter_wyvern_cold_embrace")
                        || e.Modifiers.Any(y => y.Name == "modifier_winter_wyvern_winters_curse")
                        || e.Modifiers.Any(y => y.Name == "modifier_shadow_shaman_shackles")
                        ||
                        e.Modifiers.Any(y => y.Name == "modifier_faceless_void_chronosphere_freeze") &&
                        e.ClassID == ClassID.CDOTA_Unit_Hero_FacelessVoid
                        ||
                        e.ClassID == ClassID.CDOTA_Unit_Hero_WitchDoctor &&
                        e.FindSpell("witch_doctor_death_ward").IsInAbilityPhase
                        ||
                        e.ClassID == ClassID.CDOTA_Unit_Hero_Rattletrap &&
                        e.FindSpell("rattletrap_power_cogs").IsInAbilityPhase
                        ||
                        e.ClassID == ClassID.CDOTA_Unit_Hero_Tidehunter &&
                        e.FindSpell("tidehunter_ravage").IsInAbilityPhase
                        && !e.IsMagicImmune()
                        )
                    && !e.Modifiers.Any(y => y.Name == "modifier_medusa_stone_gaze_stone")
                    && Utils.SleepCheck(e.Handle.ToString()))
                {
                    W.UseAbility(e);
                    Utils.Sleep(250, e.Handle.ToString());
                }
                if (e.IsLinkensProtected() &&
                    (me.IsVisibleToEnemies || Game.IsKeyDown(menu.Item("Combo Key").GetValue<KeyBind>().Key)))
                {
                    if (E != null && E.CanBeCasted() && me.Distance2D(e) < E.CastRange &&
                        menu.Item("Link: ").GetValue<AbilityToggler>().IsEnabled(E.Name) &&
                        Utils.SleepCheck(e.Handle.ToString()))
                    {
                        E.UseAbility(e);
                        Utils.Sleep(500, e.Handle.ToString());
                    }
                    if (force != null && force.CanBeCasted() && me.Distance2D(e) < force.CastRange &&
                        menu.Item("Link: ").GetValue<AbilityToggler>().IsEnabled(force.Name) &&
                        Utils.SleepCheck(e.Handle.ToString()))
                    {
                        force.UseAbility(e);
                        Utils.Sleep(500, e.Handle.ToString());
                    }
                    else if (orchid != null && orchid.CanBeCasted() && me.Distance2D(e) < orchid.CastRange &&
                             menu.Item("Link: ").GetValue<AbilityToggler>().IsEnabled(orchid.Name) &&
                             Utils.SleepCheck(e.Handle.ToString()))
                    {
                        orchid.UseAbility(e);
                        Utils.Sleep(500, e.Handle.ToString());
                    }
                    else if (atos != null && atos.CanBeCasted() && me.Distance2D(e) < atos.CastRange - 400 &&
                             menu.Item("Link: ").GetValue<AbilityToggler>().IsEnabled(atos.Name) &&
                             Utils.SleepCheck(e.Handle.ToString()))
                    {
                        atos.UseAbility(e);
                        Utils.Sleep(500, e.Handle.ToString());
                    }
                    else if (dagon != null && dagon.CanBeCasted() && me.Distance2D(e) < dagon.CastRange &&
                             menu.Item("Link: ").GetValue<AbilityToggler>().IsEnabled(dagon.Name) &&
                             Utils.SleepCheck(e.Handle.ToString()))
                    {
                        dagon.UseAbility(e);
                        Utils.Sleep(500, e.Handle.ToString());
                    }
                    else if (cyclone != null && cyclone.CanBeCasted() && me.Distance2D(e) < cyclone.CastRange &&
                             menu.Item("Link: ").GetValue<AbilityToggler>().IsEnabled(cyclone.Name) &&
                             Utils.SleepCheck(e.Handle.ToString()))
                    {
                        cyclone.UseAbility(e);
                        Utils.Sleep(500, e.Handle.ToString());
                    }
                }
            }
        }
    }
}