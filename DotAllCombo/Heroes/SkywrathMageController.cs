namespace DotAllCombo.Heroes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;
    using Ensage.Common.Menu;
	using SharpDX;
	using Extensions;

    internal class SkywrathMageController : HeroController
    {
        private static Ability Q, W, E, R;
        private static Hero me, target;
        private static Menu menu;
        private static readonly Menu skills = new Menu("Skills", "Skills");
        private static readonly Menu items = new Menu("Items", "Items");
        private static readonly Menu link = new Menu("Link", "Link");
        private static readonly Menu ult = new Menu("AutoUlt", "AutoUlt");
        private static readonly Menu healh = new Menu("Healh", "Min % Enemy Healh to Ult");
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
                {"skywrath_mage_arcane_bolt", true},
                {"skywrath_mage_concussive_shot", true},
                {"skywrath_mage_mystic_flare", true}
            };
            var AutoUlt = new Dictionary<string, bool>
            {
                {"skywrath_mage_mystic_flare", true}
            };
            var Link = new Dictionary<string, bool>
            {
                {"item_force_staff", true},
                {"item_cyclone", true},
                {"item_orchid", true},
                {"item_rod_of_atos", true},
                {"item_dagon", true}
            };
            var Items = new Dictionary<string, bool>
            {
                {"item_orchid", true},
                {"item_ethereal_blade", true},
                {"item_veil_of_discord", true},
                {"item_rod_of_atos", true},
                {"item_sheepstick", true},
                {"item_arcane_boots", true},
				{"item_shivas_guard",true},
				{"item_blink", true},
                {"item_soul_ring", true},
                {"item_ghost", true},
                {"item_cheese", true}
            };


            skills.AddItem(new MenuItem("Skills", "Skills").SetValue(new AbilityToggler(Skills)));
            items.AddItem(new MenuItem("Items", "Items:").SetValue(new AbilityToggler(Items)));
            ult.AddItem(new MenuItem("autoUlt", "AutoUlt").SetValue(true));
            ult.AddItem(new MenuItem("AutoUlt: ", "AutoUlt").SetValue(new AbilityToggler(AutoUlt)));
            items.AddItem(new MenuItem("Link: ", "Auto triggre Linken").SetValue(new AbilityToggler(Link)));
            healh.AddItem(new MenuItem("Healh", "Min healh % to ult").SetValue(new Slider(35, 10, 70))); // x/ 10%
            menu.AddSubMenu(skills);
            menu.AddSubMenu(items);
            menu.AddSubMenu(ult);
            menu.AddSubMenu(healh);
        }

        public override void OnUpdateEvent(EventArgs args)
        {
			target = me.ClosestToMouseTarget(2000);
			if (target == null)
				return;
			if (target.Modifiers.Any(y => y.Name == "modifier_abaddon_borrowed_time"
			|| y.Name == "modifier_item_blade_mail_reflect")
			|| target.IsMagicImmune())
			{
				var enemies = ObjectManager.GetEntities<Hero>()
						.Where(x => x.IsAlive && x.Team != me.Team && !x.IsIllusion && !x.IsMagicImmune()
						&& !x.Modifiers.Any(y => y.Name == "modifier_abaddon_borrowed_time"
						|| y.Name == "modifier_item_blade_mail_reflect")
						&& x.Distance2D(target) > 200).ToList();
				target = GetClosestToTarget(enemies, target) ?? null;
				if (Utils.SleepCheck("spam"))
				{
					DebugExtensions.Chat.PrintInfo("Some problems with target detected! Target replaced on " + target.Name);
					Utils.Sleep(3000, "spam");
				}
			}

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


			var ModifW = target.Modifiers.Any(y => y.Name == "modifier_skywrath_mage_concussive_shot_slow");
			var ModifR = target.Modifiers.Any(y => y.Name == "modifier_skywrath_mystic_flare_aura_effect");
			var ModifE = target.Modifiers.Any(y => y.Name == "modifier_skywrath_mage_ancient_seal");
			var ModifRod = target.Modifiers.Any(y => y.Name == "modifier_rod_of_atos_debuff");
			var ModifEther = target.Modifiers.Any(y => y.Name == "modifier_item_ethereal_blade_slow");
			var ModifVail = target.Modifiers.Any(y => y.Name == "modifier_item_veil_of_discord_debuff");
			var stoneModif = target.Modifiers.Any(y => y.Name == "modifier_medusa_stone_gaze_stone");


			if (Game.IsKeyDown(keyCode: 70) && Q.CanBeCasted() && target != null)
			{
				if (
					Q != null
					&& Q.CanBeCasted()
					&& (target.IsLinkensProtected()
					|| !target.IsLinkensProtected())
					&& me.CanCast()
					&& me.Distance2D(target) < 900
					&& !stoneModif
					&& Utils.SleepCheck("Q")
					)
				{
					Q.UseAbility(target);
					Utils.Sleep(200, "Q");
				}
			}
			if (Active && me.IsAlive && target.IsAlive && Utils.SleepCheck("activated"))
			{
				var noBlade = target.Modifiers.Any(y => y.Name == "modifier_item_blade_mail_reflect");
				if (target.IsVisible && me.Distance2D(target) <= 2300 && !noBlade)
				{
					if (
						Q != null
						&& Q.CanBeCasted()
						&& (target.IsLinkensProtected()
							|| !target.IsLinkensProtected())
						&& me.CanCast()
						&& me.Distance2D(target) < 1400
						&& !stoneModif
						&& menu.Item("Skills").GetValue<AbilityToggler>().IsEnabled(Q.Name)
						&& Utils.SleepCheck("Q")
						)
					{
						Q.UseAbility(target);
						Utils.Sleep(200, "Q");
					}
					if ( // atos Blade
						atos != null
						&& atos.CanBeCasted()
						&& me.CanCast()
						&& !target.IsLinkensProtected()
						&& !target.IsMagicImmune()
						&& menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(atos.Name)
						&& me.Distance2D(target) <= 2000
						&& Utils.SleepCheck("atos")
						)
					{
						atos.UseAbility(target);

						Utils.Sleep(250, "atos");
					} // atos Item end
					if (
						W != null
						&& target.IsVisible
						&& W.CanBeCasted()
						&& !target.IsMagicImmune()
						&& me.CanCast()
						&& me.Distance2D(target) < 900
						&& menu.Item("Skills").GetValue<AbilityToggler>().IsEnabled(W.Name)
						&& Utils.SleepCheck("W"))
					{
						W.UseAbility();
						Utils.Sleep(300, "W");
					}
					float angle = me.FindAngleBetween(target.Position, true);
					Vector3 pos = new Vector3((float)(target.Position.X - 400 * Math.Cos(angle)), (float)(target.Position.Y - 400 * Math.Sin(angle)), 0);
					if (
						blink != null
						&& Q.CanBeCasted()
						&& me.CanCast()
						&& blink.CanBeCasted()
						&& me.Distance2D(target) >= 490
						&& me.Distance2D(pos) <= 1180
						&& !stoneModif
						&& menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(blink.Name)
						&& Utils.SleepCheck("blink")
						)
					{
						blink.UseAbility(pos);

						Utils.Sleep(250, "blink");
					}
					if (
						E != null
						&& E.CanBeCasted()
						&& me.CanCast()
						&& !target.IsLinkensProtected()
						&& me.Position.Distance2D(target) < 1400
						&& !stoneModif
						&& Utils.SleepCheck("E"))
					{
						E.UseAbility(target);
						Utils.Sleep(200, "E");
					}
					if (!E.CanBeCasted() || E == null)
					{
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
										&& me.CanCast()
										&& !ModifR
										&& (ModifW
											|| ModifEther
											|| ModifRod)
										&& me.Position.Distance2D(target) < 1200
										&&
										target.Health >=
										(target.MaximumHealth / 100 * menu.Item("Healh").GetValue<Slider>().Value)
										&& (!target.Modifiers.Any(y => y.Name == "modifier_medusa_stone_gaze_stone")
										&& !target.Modifiers.Any(y => y.Name == "modifier_item_monkey_king_bar")
										&& !target.Modifiers.Any(y => y.Name == "modifier_rattletrap_battery_assault")
										&& !target.Modifiers.Any(y => y.Name == "modifier_item_blade_mail_reflect")
										&& !target.Modifiers.Any(y => y.Name == "modifier_skywrath_mystic_flare_aura_effect")
										&& !target.Modifiers.Any(y => y.Name == "modifier_pudge_meat_hook")
										&& !target.Modifiers.Any(y => y.Name == "modifier_zuus_lightningbolt_vision_thinker")
										&& !target.Modifiers.Any(y => y.Name == "modifier_obsidian_destroyer_astral_imprisonment_prison")
										&& !target.Modifiers.Any(y => y.Name == "modifier_puck_phase_shift")
										&& !target.Modifiers.Any(y => y.Name == "modifier_eul_cyclone")
										&& !target.Modifiers.Any(y => y.Name == "modifier_invoker_tornado")
										&& !target.Modifiers.Any(y => y.Name == "modifier_skywrath_mystic_flare_aura_effect")
										&& !target.Modifiers.Any(y => y.Name == "modifier_dazzle_shallow_grave")
										&& !target.Modifiers.Any(y => y.Name == "modifier_brewmaster_storm_cyclone")
										&& !target.Modifiers.Any(y => y.Name == "modifier_spirit_breaker_charge_of_darkness")
										&& !target.Modifiers.Any(y => y.Name == "modifier_shadow_demon_disruption")
										&& !target.Modifiers.Any(y => y.Name == "modifier_tusk_snowball_movement")
										&& !target.Modifiers.Any(y => y.Name == "modifier_invoker_tornado")
										&& (!target.FindSpell("abaddon_borrowed_time").CanBeCasted()
										&& !target.Modifiers.Any(y => y.Name == "modifier_abaddon_borrowed_time"))
										&& !target.IsMagicImmune())
										&& menu.Item("Skills").GetValue<AbilityToggler>().IsEnabled(R.Name)
										&& Utils.SleepCheck("R"))
									{
										R.UseAbility(target.Predict(500));
										Utils.Sleep(500, "R");
									}

									if ( // SoulRing Item 
										soulring != null
										&& soulring.CanBeCasted()
										&& me.CanCast()
										&& me.Health >= (me.MaximumHealth * 0.5)
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
											 && me.Health <= (me.MaximumHealth * 0.7))
											|| me.Health <= (me.MaximumHealth * 0.3))
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
										&& me.Health <= (me.MaximumHealth * 0.3)
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
				}
				Utils.Sleep(200, "activated");
			}
			A();
		}

		private static Hero GetClosestToTarget(List<Hero> units, Hero target)
		{
			Hero closestHero = null;
			foreach (var v in units.Where(v => closestHero == null || closestHero.Distance2D(target) > v.Distance2D(target)))
			{
				closestHero = v;
			}
			return closestHero;
		}


		public static void A()
        {
            var e =
                ObjectManager.GetEntities<Hero>()
                    .Where(x => x.IsVisible && x.IsAlive && x.Team == me.GetEnemyTeam() && !x.IsIllusion).ToList();
            if (menu.Item("autoUlt").GetValue<bool>())
            {
                core = me.FindItem("item_octarine_core");
                force = me.FindItem("item_force_staff");
                cyclone = me.FindItem("item_cyclone");
                orchid = me.FindItem("item_orchid");
                atos = me.FindItem("item_rod_of_atos");

				for (int i = 0; i < e.Count(); i++)
                {
                    if (e[i] == null) return;

                    var reflect = e[i].Modifiers.Any(y => y.Name == "modifier_item_blade_mail_reflect");
                    {
                        if (cyclone != null && reflect && cyclone.CanBeCasted() && me.Distance2D(e[i]) < cyclone.CastRange &&
                            Utils.SleepCheck(e[i].Handle.ToString()))
                        {
                            cyclone.UseAbility(me);
                            Utils.Sleep(500, e[i].Handle.ToString());
                        }
                        if ((Active && e[i].MovementSpeed <= 200 &&
                             e[i].Modifiers.Any(y => y.Name == "modifier_skywrath_mage_ancient_seal") ||
                             ((E.Cooldown <= 14 && core == null) || E.Cooldown <= 10 && core != null ||
                              e[i].Modifiers.Any(y => y.Name == "modifier_item_ethereal_blade_slow"))))
                        {
                            if (R != null && e[i] != null && R.CanBeCasted() && me.Distance2D(e[i]) <= 1200
                                && e[i].MovementSpeed <= 200
								&& !e[i].Modifiers.Any(y => y.Name == "modifier_zuus_lightningbolt_vision_thinker")
								&& !e[i].Modifiers.Any(y => y.Name == "modifier_item_blade_mail_reflect")
                                && !e[i].Modifiers.Any(y => y.Name == "modifier_sniper_headshot")
                                && !e[i].Modifiers.Any(y => y.Name == "modifier_leshrac_lightning_storm_slow")
                                && !e[i].Modifiers.Any(y => y.Name == "modifier_razor_unstablecurrent_slow")
                                && !e[i].Modifiers.Any(y => y.Name == "modifier_pudge_meat_hook")
                                && !e[i].Modifiers.Any(y => y.Name == "modifier_tusk_snowball_movement")
								&& !e[i].Modifiers.Any(y => y.Name == "modifier_faceless_void_time_walk")
								&&
                                !e[i].Modifiers.Any(y => y.Name == "modifier_obsidian_destroyer_astral_imprisonment_prison")
								&& !e[i].Modifiers.Any(y => y.Name == "modifier_skywrath_mystic_flare_aura_effect")
								&& !e[i].Modifiers.Any(y => y.Name == "modifier_puck_phase_shift")
                                && !e[i].Modifiers.Any(y => y.Name == "modifier_eul_cyclone")
                                && !e[i].Modifiers.Any(y => y.Name == "modifier_dazzle_shallow_grave")
                                && !e[i].Modifiers.Any(y => y.Name == "modifier_brewmaster_storm_cyclone")
                                && !e[i].Modifiers.Any(y => y.Name == "modifier_spirit_breaker_charge_of_darkness")
                                && !e[i].Modifiers.Any(y => y.Name == "modifier_shadow_demon_disruption")
                                && (!e[i].FindSpell("abaddon_borrowed_time").CanBeCasted() 
								&& !e[i].Modifiers.Any(y => y.Name == "modifier_abaddon_borrowed_time"))
                                && e[i].Health >= (e[i].MaximumHealth/100*(menu.Item("Healh").GetValue<Slider>().Value))
                                && !e[i].IsMagicImmune()
                                && menu.Item("AutoUlt: ").GetValue<AbilityToggler>().IsEnabled(R.Name)
                                && Utils.SleepCheck(e[i].Handle.ToString()))
                            {
                                R.UseAbility(e[i].Predict(650));
                                Utils.Sleep(500, e[i].Handle.ToString());
                            }
                            if (R != null && e[i] != null && R.CanBeCasted() && me.Distance2D(e[i]) <= 1200
                                &&
                                (
                                    e[i].Modifiers.Any(y => y.Name == "modifier_meepo_earthbind")
                                    || e[i].Modifiers.Any(y => y.Name == "modifier_pudge_dismember")
                                    || e[i].Modifiers.Any(y => y.Name == "modifier_naga_siren_ensnare")
                                    || e[i].Modifiers.Any(y => y.Name == "modifier_lone_druid_spirit_bear_entangle_effect")
                                    ||
                                    (e[i].Modifiers.Any(y => y.Name == "modifier_legion_commander_duel") &&
                                     !e[i].AghanimState())
                                    || e[i].Modifiers.Any(y => y.Name == "modifier_kunkka_torrent")
                                    || e[i].Modifiers.Any(y => y.Name == "modifier_ice_blast")
                                    || e[i].Modifiers.Any(y => y.Name == "modifier_enigma_black_hole_pull")
                                    || e[i].Modifiers.Any(y => y.Name == "modifier_ember_spirit_searing_chains")
                                    || e[i].Modifiers.Any(y => y.Name == "modifier_dark_troll_warlord_ensnare")
                                    || e[i].Modifiers.Any(y => y.Name == "modifier_crystal_maiden_frostbite")
                                    ||
                                    e[i].ClassID == ClassID.CDOTA_Unit_Hero_Rattletrap &&
                                    e[i].FindSpell("rattletrap_power_cogs").IsInAbilityPhase
                                    || e[i].Modifiers.Any(y => y.Name == "modifier_axe_berserkers_call")
                                    || e[i].Modifiers.Any(y => y.Name == "modifier_bane_fiends_grip")
                                    ||
                                    e[i].Modifiers.Any(y => y.Name == "modifier_faceless_void_chronosphere_freeze") &&
                                    e[i].ClassID != ClassID.CDOTA_Unit_Hero_FacelessVoid
                                    || e[i].Modifiers.Any(y => y.Name == "modifier_storm_spirit_electric_vortex_pull")
                                    ||
                                    (e[i].ClassID == ClassID.CDOTA_Unit_Hero_WitchDoctor &&
                                     e[i].FindSpell("witch_doctor_death_ward").IsInAbilityPhase)
                                    ||
                                    (e[i].ClassID == ClassID.CDOTA_Unit_Hero_CrystalMaiden &&
                                     e[i].FindSpell("crystal_maiden_crystal_nova").IsInAbilityPhase)
                                    || e[i].IsStunned()
                                    )
								&& (!e[i].Modifiers.Any(y => y.Name == "modifier_medusa_stone_gaze_stone")
									&& !e[i].Modifiers.Any(y => y.Name == "modifier_faceless_void_time_walk")
									&& !e[i].Modifiers.Any(y => y.Name == "modifier_item_monkey_king_bar")
                                    && !e[i].Modifiers.Any(y => y.Name == "modifier_rattletrap_battery_assault")
                                    && !e[i].Modifiers.Any(y => y.Name == "modifier_item_blade_mail_reflect")
                                    && !e[i].Modifiers.Any(y => y.Name == "modifier_skywrath_mystic_flare_aura_effect")
                                    && !e[i].Modifiers.Any(y => y.Name == "modifier_pudge_meat_hook")
									&& !e[i].Modifiers.Any(y => y.Name == "modifier_zuus_lightningbolt_vision_thinker")
									&& !e[i].Modifiers.Any( y => y.Name == "modifier_obsidian_destroyer_astral_imprisonment_prison")
                                    && !e[i].Modifiers.Any(y => y.Name == "modifier_puck_phase_shift")
                                    && !e[i].Modifiers.Any(y => y.Name == "modifier_eul_cyclone")
									&& !e[i].Modifiers.Any(y => y.Name == "modifier_invoker_tornado")
									&& !e[i].Modifiers.Any(y => y.Name == "modifier_skywrath_mystic_flare_aura_effect")
									&& !e[i].Modifiers.Any(y => y.Name == "modifier_dazzle_shallow_grave")
                                    && !e[i].Modifiers.Any(y => y.Name == "modifier_brewmaster_storm_cyclone")
                                    && !e[i].Modifiers.Any(y => y.Name == "modifier_spirit_breaker_charge_of_darkness")
                                    && !e[i].Modifiers.Any(y => y.Name == "modifier_shadow_demon_disruption")
                                    && !e[i].Modifiers.Any(y => y.Name == "modifier_tusk_snowball_movement")
									&& !e[i].Modifiers.Any(y => y.Name == "modifier_invoker_tornado")
									&& (!e[i].FindSpell("abaddon_borrowed_time").CanBeCasted() 
									&& !e[i].Modifiers.Any(y => y.Name == "modifier_abaddon_borrowed_time"))
                                    && e[i].Health >= (e[i].MaximumHealth/100*(menu.Item("Healh").GetValue<Slider>().Value))
                                    && !e[i].IsMagicImmune())
                                && menu.Item("AutoUlt: ").GetValue<AbilityToggler>().IsEnabled(R.Name)
                                && Utils.SleepCheck(e[i].Handle.ToString()))
                            {
                                R.UseAbility(e[i].Position);
                                Utils.Sleep(500, e[i].Handle.ToString());
                            }
                            if (R != null && e[i] != null && R.CanBeCasted() && me.Distance2D(e[i]) <= 1200
                                && e[i].MovementSpeed <= 200
                                && e[i].MagicDamageResist <= 0.07
                                && e[i].Health >= (e[i].MaximumHealth/100*(menu.Item("Healh").GetValue<Slider>().Value))
                                && !e[i].Modifiers.Any(y => y.Name == "modifier_item_blade_mail_reflect")
                                && !e[i].Modifiers.Any(y => y.Name == "modifier_skywrath_mystic_flare_aura_effect")
								&& !e[i].Modifiers.Any(y => y.Name == "modifier_zuus_lightningbolt_vision_thinker")
								&&
                                !e[i].Modifiers.Any(y => y.Name == "modifier_obsidian_destroyer_astral_imprisonment_prison")
                                && !e[i].Modifiers.Any(y => y.Name == "modifier_puck_phase_shift")
                                && !e[i].Modifiers.Any(y => y.Name == "modifier_eul_cyclone")
								&& !e[i].Modifiers.Any(y => y.Name == "modifier_invoker_tornado")
								&& !e[i].Modifiers.Any(y => y.Name == "modifier_dazzle_shallow_grave")
                                && !e[i].Modifiers.Any(y => y.Name == "modifier_brewmaster_storm_cyclone")
                                && !e[i].Modifiers.Any(y => y.Name == "modifier_spirit_breaker_charge_of_darkness")
                                && !e[i].Modifiers.Any(y => y.Name == "modifier_shadow_demon_disruption")
								&& !e[i].Modifiers.Any(y => y.Name == "modifier_faceless_void_time_walk")
								&& !e[i].Modifiers.Any(y => y.Name == "modifier_tusk_snowball_movement")
                                && !e[i].IsMagicImmune()
                                &&
                                (!e[i].FindSpell("abaddon_borrowed_time").CanBeCasted() ||
                                 !e[i].Modifiers.Any(y => y.Name == "modifier_abbadon_borrowed_time"))
                                && menu.Item("AutoUlt: ").GetValue<AbilityToggler>().IsEnabled(R.Name)
                                && Utils.SleepCheck(e[i].Handle.ToString()))
                            {
                                R.UseAbility(e[i].Predict(650));
                                Utils.Sleep(500, e[i].Handle.ToString());
                                goto leave;
                            }
                        }
                        if (E != null && e[i] != null && E.CanBeCasted() && me.Distance2D(e[i]) <= 730
                            && !e[i].IsLinkensProtected()
                            &&
                            (
                                e[i].Modifiers.Any(y => y.Name == "modifier_meepo_earthbind")
                                || e[i].Modifiers.Any(y => y.Name == "modifier_pudge_dismember")
                                || e[i].Modifiers.Any(y => y.Name == "modifier_naga_siren_ensnare")
                                || e[i].Modifiers.Any(y => y.Name == "modifier_lone_druid_spirit_bear_entangle_effect")
                                || e[i].Modifiers.Any(y => y.Name == "modifier_legion_commander_duel")
                                || e[i].Modifiers.Any(y => y.Name == "modifier_kunkka_torrent")
                                || e[i].Modifiers.Any(y => y.Name == "modifier_ice_blast")
                                || e[i].Modifiers.Any(y => y.Name == "modifier_enigma_black_hole_pull")
                                || e[i].Modifiers.Any(y => y.Name == "modifier_ember_spirit_searing_chains")
                                || e[i].Modifiers.Any(y => y.Name == "modifier_dark_troll_warlord_ensnare")
                                || e[i].Modifiers.Any(y => y.Name == "modifier_crystal_maiden_frostbite")
                                || e[i].Modifiers.Any(y => y.Name == "modifier_axe_berserkers_call")
                                || e[i].Modifiers.Any(y => y.Name == "modifier_bane_fiends_grip")
                                ||
                                e[i].ClassID == ClassID.CDOTA_Unit_Hero_Magnataur &&
                                e[i].FindSpell("magnataur_reverse_polarity").IsInAbilityPhase
                                || e[i].FindItem("item_blink").IsInAbilityPhase
                                ||
                                e[i].ClassID == ClassID.CDOTA_Unit_Hero_QueenOfPain &&
                                e[i].FindSpell("queenofpain_blink").IsInAbilityPhase
                                ||
                                e[i].ClassID == ClassID.CDOTA_Unit_Hero_AntiMage &&
                                e[i].FindSpell("antimage_blink").IsInAbilityPhase
                                ||
                                e[i].ClassID == ClassID.CDOTA_Unit_Hero_AntiMage &&
                                e[i].FindSpell("antimage_mana_void").IsInAbilityPhase
                                ||
                                e[i].ClassID == ClassID.CDOTA_Unit_Hero_DoomBringer &&
                                e[i].FindSpell("doom_bringer_doom").IsInAbilityPhase
                                || e[i].Modifiers.Any(y => y.Name == "modifier_rubick_telekinesis")
                                || e[i].Modifiers.Any(y => y.Name == "modifier_storm_spirit_electric_vortex_pull")
                                || e[i].Modifiers.Any(y => y.Name == "modifier_winter_wyvern_cold_embrace")
                                || e[i].Modifiers.Any(y => y.Name == "modifier_winter_wyvern_winters_curse")
                                || e[i].Modifiers.Any(y => y.Name == "modifier_shadow_shaman_shackles")
                                ||
                                (e[i].Modifiers.Any(y => y.Name == "modifier_faceless_void_chronosphere_freeze") &&
                                e[i].ClassID != ClassID.CDOTA_Unit_Hero_FacelessVoid)
                                ||
                                e[i].ClassID == ClassID.CDOTA_Unit_Hero_WitchDoctor &&
                                e[i].FindSpell("witch_doctor_death_ward").IsInAbilityPhase
                                ||
                                e[i].ClassID == ClassID.CDOTA_Unit_Hero_Rattletrap &&
                                e[i].FindSpell("rattletrap_power_cogs").IsInAbilityPhase
                                ||
                                e[i].ClassID == ClassID.CDOTA_Unit_Hero_Tidehunter &&
                                e[i].FindSpell("tidehunter_ravage").IsInAbilityPhase
                                || e[i].IsStunned()
                                && !e[i].IsMagicImmune()
                                )
                            && !e[i].Modifiers.Any(y => y.Name == "modifier_medusa_stone_gaze_stone")
                            && Utils.SleepCheck(e[i].Handle.ToString()))
                        {
                            E.UseAbility(e[i]);
                            Utils.Sleep(250, e[i].Handle.ToString());
                            goto leave;
                        }
                        if (W != null && e[i] != null && W.CanBeCasted() && me.Distance2D(e[i]) <= 1400
                            && e[i].MovementSpeed <= 255
                            && !e[i].IsMagicImmune()
                            && Utils.SleepCheck(e[i].Handle.ToString()))
                        {
                            W.UseAbility();
                            Utils.Sleep(500, e[i].Handle.ToString());
                            goto leave;
                        }
                        if (atos != null && e[i] != null && R != null && R.CanBeCasted() && atos.CanBeCasted()
                            && !e[i].IsLinkensProtected()
                            && me.Distance2D(e[i]) <= 1200
                            && e[i].MagicDamageResist <= 0.07
                            && !e[i].IsMagicImmune()
                            && Utils.SleepCheck(e[i].Handle.ToString()))
                        {
                            atos.UseAbility(e[i]);
                            Utils.Sleep(500, e[i].Handle.ToString());
                            goto leave;
                        }
                        if (vail != null && e[i].Modifiers.Any(y => y.Name == "modifier_skywrath_mystic_flare_aura_effect")
                            && vail.CanBeCasted()
                            && me.Distance2D(e[i]) <= 1200
                            && Utils.SleepCheck(e[i].Handle.ToString())
                            )
                        {
                            vail.UseAbility(e[i].Position);
                            Utils.Sleep(500, e[i].Handle.ToString());
                            goto leave;
                        }
                        if (E != null && e[i].Modifiers.Any(y => y.Name == "modifier_skywrath_mystic_flare_aura_effect")
                            && E.CanBeCasted()
                            && me.Distance2D(e[i]) <= 900
                            && Utils.SleepCheck(e[i].Handle.ToString())
                            )
                        {
                            E.UseAbility(e[i]);
                            Utils.Sleep(500, e[i].Handle.ToString());
                            goto leave;
                        }
                        if (ethereal != null &&
                            e[i].Modifiers.Any(y => y.Name == "modifier_skywrath_mystic_flare_aura_effect")
                            && !e[i].Modifiers.Any(y => y.Name == "modifier_legion_commander_duel")
                            && ethereal.CanBeCasted()
                            && E.CanBeCasted()
                            && me.Distance2D(e[i]) <= 1000
                            && Utils.SleepCheck(e[i].Handle.ToString())
                            )
                        {
                            ethereal.UseAbility(e[i]);
                            Utils.Sleep(500, e[i].Handle.ToString());
                            goto leave;
                        }
                    }
                    if (e[i].IsLinkensProtected() && (me.IsVisibleToEnemies || Active))
                    {
                        if (force != null && force.CanBeCasted() && me.Distance2D(e[i]) < force.CastRange &&
                            menu.Item("Link: ").GetValue<AbilityToggler>().IsEnabled(force.Name) &&
                            Utils.SleepCheck(e[i].Handle.ToString()))
                        {
                            force.UseAbility(e[i]);
                            Utils.Sleep(500, e[i].Handle.ToString());
                        }
                        else if (cyclone != null && cyclone.CanBeCasted() && me.Distance2D(e[i]) < cyclone.CastRange &&
                                 menu.Item("Link: ").GetValue<AbilityToggler>().IsEnabled(cyclone.Name) &&
                                 Utils.SleepCheck(e[i].Handle.ToString()))
                        {
                            cyclone.UseAbility(e[i]);
                            Utils.Sleep(500, e[i].Handle.ToString());
                        }
                        else if (orchid != null && orchid.CanBeCasted() && me.Distance2D(e[i]) < orchid.CastRange &&
                                 menu.Item("Link: ").GetValue<AbilityToggler>().IsEnabled(orchid.Name) &&
                                 Utils.SleepCheck(e[i].Handle.ToString()))
                        {
                            orchid.UseAbility(e[i]);
                            Utils.Sleep(500, e[i].Handle.ToString());
                        }
                        else if (atos != null && atos.CanBeCasted() && me.Distance2D(e[i]) < atos.CastRange - 400 &&
                                 menu.Item("Link: ").GetValue<AbilityToggler>().IsEnabled(atos.Name) &&
                                 Utils.SleepCheck(e[i].Handle.ToString()))
                        {
                            atos.UseAbility(e[i]);
                            Utils.Sleep(500, e[i].Handle.ToString());
                        }
                        else if (dagon != null && dagon.CanBeCasted() && me.Distance2D(e[i]) < dagon.CastRange &&
                                 menu.Item("Link: ").GetValue<AbilityToggler>().IsEnabled(dagon.Name) &&
                                 Utils.SleepCheck(e[i].Handle.ToString()))
                        {
                            dagon.UseAbility(e[i]);
                            Utils.Sleep(500, e[i].Handle.ToString());
                        }
                    }
                }
                leave:
                ;
            }
        }
    }
}