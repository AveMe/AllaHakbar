namespace DotAllCombo.Heroes
{
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using Ensage;
	using Ensage.Common.Extensions;
	using Ensage.Common;
	using Ensage.Common.Menu;
	using SharpDX;
	using System.Threading;
	using Extensions;

	internal class TinkerController : HeroController
	{
		private static Ability Laser, Rocket, Refresh, March;
		private static Item Blink, Dagon, Hex, Soulring, Ethereal, Shiva, ghost, euls, blink, forcestaff, glimmer, vail, orchid;
		private static Hero me, target;
		private static bool auto_attack, auto_attack_after_spell;
		private static Menu Menu;
		private static readonly List<ParticleEffect> Effects = new List<ParticleEffect>();
		private const string EffectPath = @"particles\range_display_blue.vpcf";
		private static readonly Menu _skills = new Menu("Skills", "Skills");
		private static readonly Menu _items = new Menu("Items", "Items");
		private static readonly Dictionary<string, bool> Skills = new Dictionary<string, bool>
			{
				{"tinker_laser",true},
				{"tinker_heat_seeking_missile",true},
				{"tinker_rearm",true}
                //{"tinker_march_of_the_machines",true}
            };
		private static readonly Dictionary<string, bool> Items = new Dictionary<string, bool>
			{
				{"item_dagon",true},
				{"item_sheepstick",true},
				{"item_soul_ring",true},
				{ "item_orchid",true},
				{"item_ethereal_blade",true},
				{"item_shivas_guard",true}
			};
		private static readonly Dictionary<string, bool> Items2 = new Dictionary<string, bool>
			{
				{"item_ghost",true},
				{"item_cyclone",true},
				{"item_force_staff",true},
				{"item_glimmer_cape",true},
				{ "item_veil_of_discord",true},
				{ "item_blink",true},
			};
		private static Vector3[] SafePos =
		{
			new Vector3(-6752, 4325, 384),
			new Vector3(-5017, 5099, 384),
			new Vector3(-4046, 5282, 384),
			new Vector3(-2531, 5419, 384),
			new Vector3(-1561, 5498, 384),
			new Vector3(-1000, 5508, 384),
			new Vector3(-749, 6791, 384),
			new Vector3(359, 6668, 384),
			new Vector3(1624, 6780, 256),
			new Vector3(-6877, 3757, 384),
			new Vector3(-5662, 2268, 384),
			new Vector3(-6941, 1579, 384),
			new Vector3(-6819, 608, 384),
			new Vector3(-6848, 68, 384),
			new Vector3(-7005, -681, 384),
			new Vector3(-7082, -1160, 384),
			new Vector3(-2605, -2657, 256),
			new Vector3(-2207, -2394, 256),
			new Vector3(-1446, -1871, 256),
			new Vector3(-2068, -1151, 256),
			new Vector3(659, 929, 256),
			new Vector3(1065, 1241, 256),
			new Vector3(2259, 667, 256),
			new Vector3(2426, 812, 256),
			new Vector3(2647, 1009, 256),
			new Vector3(2959, 1283, 256),
			new Vector3(2110, 2431, 256),
			new Vector3(6869, 613, 384),
			new Vector3(6832, -206, 384),
			new Vector3(6773, -431, 384),
			new Vector3(6742, -1549, 384),
			new Vector3(6813, -3591, 384),
			new Vector3(6745, -4689, 384),
			new Vector3(6360, -5215, 384),
			new Vector3(4637, -5579, 384),
			new Vector3(4756, -6491, 384),
			new Vector3(4249, -6553, 384),
			new Vector3(2876, -5666, 384),
			new Vector3(3180, -6627, 384),
			new Vector3(2013, -6684, 384),
			new Vector3(-560, -6810, 384),
			new Vector3(-922, -6797, 384),
			new Vector3(-1130, -6860, 384),
			new Vector3(1000, -6928, 384),
			new Vector3(814, 981, 256),
			new Vector3(-6690, 5024, 384),
			new Vector3(-5553, 1961, 384),
		};
		public override void OnInject()
		{
			AssemblyExtensions.InitAssembly("NeverMore", "0.1");

			DebugExtensions.Chat.PrintSuccess("To destroy the darkness in itself!");
			Menu = MenuExtensions.GetMenu();
			me = Variables.me;
			Menu.AddItem(new MenuItem("Combo Key", "Combo Key").SetValue(new KeyBind('D', KeyBindType.Press)));
			Menu.AddSubMenu(_skills);
			Menu.AddSubMenu(_items);
			_skills.AddItem(new MenuItem("Skills: ", "Skills: ").SetValue(new AbilityToggler(Skills)));
			_items.AddItem(new MenuItem("Items: ", "Items 1:").SetValue(new AbilityToggler(Items)));
			_items.AddItem(new MenuItem("Items2: ", "Items 2: ").SetValue(new AbilityToggler(Items2)));
			// Auto Attack Checker
			if (Game.GetConsoleVar("dota_player_units_auto_attack_after_spell").GetInt() == 1)
				auto_attack_after_spell = true;
			else
				auto_attack_after_spell = false;
			if (Game.GetConsoleVar("dota_player_units_auto_attack").GetInt() == 1)
				auto_attack = true;
			else
				auto_attack = false;
			// start
			PrintSuccess(string.Format("> Tinker Loaded!"));
			Drawing.OnDraw += markedfordeath;
		}
		public override void OnUpdateEvent(EventArgs args)
		{
			if (!Game.IsInGame || Game.IsWatchingGame)
				return;
			me = ObjectManager.LocalHero;
			if (me == null)
				return;
			if ((Game.IsKeyDown(Menu.Item("Combo Key").GetValue<KeyBind>().Key)) && !Game.IsChatOpen)
			{
				target = me.ClosestToMouseTarget(2500);
				if (target != null && target.IsAlive && !target.IsIllusion && !me.IsChanneling())
				{
					autoattack(true);
					FindItems();
					if (target.IsLinkensProtected())
					{
						if (euls != null && euls.CanBeCasted() && Menu.Item("Items2: ").GetValue<AbilityToggler>().IsEnabled(euls.Name))
						{
							if (Utils.SleepCheck("TimingToLinkens"))
							{
								euls.UseAbility(target);
								Utils.Sleep(200, "TimingToLinkens");
							}
						}
						else if (forcestaff != null && forcestaff.CanBeCasted() && Menu.Item("Items2: ").GetValue<AbilityToggler>().IsEnabled(forcestaff.Name))
						{
							if (Utils.SleepCheck("TimingToLinkens"))
							{
								forcestaff.UseAbility(target);
								Utils.Sleep(200, "TimingToLinkens");
							}
						}
						else if (Ethereal != null && Ethereal.CanBeCasted() && Menu.Item("Items: ").GetValue<AbilityToggler>().IsEnabled(Ethereal.Name))
						{
							if (Utils.SleepCheck("TimingToLinkens"))
							{
								Ethereal.UseAbility(target);
								Utils.Sleep(200, "TimingToLinkens");
								Utils.Sleep((me.NetworkPosition.Distance2D(target.NetworkPosition) / 650) * 1000, "TimingToLinkens");
							}
						}
						else if (Laser != null && Laser.CanBeCasted() && Menu.Item("Skills: ").GetValue<AbilityToggler>().IsEnabled(Laser.Name))
						{
							if (Utils.SleepCheck("TimingToLinkens"))
							{
								Laser.UseAbility(target);
								Utils.Sleep(200, "TimingToLinkens");
							}
						}
						else if (Dagon != null && Dagon.CanBeCasted() && Menu.Item("Items: ").GetValue<AbilityToggler>().IsEnabled("item_dagon"))
						{
							if (Utils.SleepCheck("TimingToLinkens"))
							{
								Dagon.UseAbility(target);
								Utils.Sleep(200, "TimingToLinkens");
							}
						}
						else if (Hex != null && Hex.CanBeCasted() && Menu.Item("Items: ").GetValue<AbilityToggler>().IsEnabled(Hex.Name))
						{
							if (Utils.SleepCheck("TimingToLinkens"))
							{
								Hex.UseAbility(target);
								Utils.Sleep(200, "TimingToLinkens");
							}
						}
					}
					else
					{
						float angle = me.FindAngleBetween(target.Position, true);
						Vector3 pos = new Vector3((float)(target.Position.X - 280 * Math.Cos(angle)), (float)(target.Position.Y - 280 * Math.Sin(angle)), 0);
						uint elsecount = 0;

						bool ModifVail = target.Modifiers.Any(y => y.Name == "modifier_item_veil_of_discord_debuff");
						bool magicimune = (!target.IsMagicImmune() && !target.Modifiers.Any(x => x.Name == "modifier_eul_cyclone"));
						if (Utils.SleepCheck("combo"))
						{

							if (blink != null && blink.CanBeCasted() && !me.IsChanneling() && me.Distance2D(pos) <= 1200 && me.Mana > Laser.ManaCost && me.Distance2D(target) >= 390 && Menu.Item("Items2: ").GetValue<AbilityToggler>().IsEnabled(blink.Name) && Utils.SleepCheck("Rear"))
								blink.UseAbility(pos);
							Utils.Sleep(300, "Rear");
							if (orchid != null && orchid.CanBeCasted() && !me.IsChanneling() && !target.IsSilenced() && Menu.Item("Items: ").GetValue<AbilityToggler>().IsEnabled(orchid.Name) && Utils.SleepCheck("orchid"))
								orchid.UseAbility(target);
							Utils.Sleep(150, "orchid");
							if (vail != null && vail.CanBeCasted() && !me.IsChanneling() && me.Distance2D(pos) <= 1000 && me.Mana > Refresh.ManaCost && !ModifVail && Menu.Item("Items2: ").GetValue<AbilityToggler>().IsEnabled(vail.Name) && Utils.SleepCheck("vail"))
								vail.UseAbility(target.Position);
							Utils.Sleep(200, "vail");
							elsecount += 1;
							// glimmer -> ghost -> soulring -> hex -> laser -> ethereal -> dagon -> rocket -> shivas -> euls -> refresh
							if (glimmer != null && glimmer.CanBeCasted() && Menu.Item("Items2: ").GetValue<AbilityToggler>().IsEnabled(glimmer.Name) && Utils.SleepCheck("Rearm"))
								glimmer.UseAbility(me);
							else
								elsecount += 1;
							if (ghost != null && Ethereal == null && ghost.CanBeCasted() && Menu.Item("Items2: ").GetValue<AbilityToggler>().IsEnabled(ghost.Name) && Utils.SleepCheck("Rearm"))
								ghost.UseAbility();
							else
								elsecount += 1;
							if (Soulring != null && Soulring.CanBeCasted() && Menu.Item("Items: ").GetValue<AbilityToggler>().IsEnabled(Soulring.Name) && Utils.SleepCheck("Rearm"))
								Soulring.UseAbility();
							else
								elsecount += 1;
							if (Hex != null && Hex.CanBeCasted() && Menu.Item("Items: ").GetValue<AbilityToggler>().IsEnabled(Hex.Name) && magicimune && Utils.SleepCheck("Rearm"))
								Hex.UseAbility(target);
							else
								elsecount += 1;
							if (Laser != null && Laser.CanBeCasted() && Menu.Item("Skills: ").GetValue<AbilityToggler>().IsEnabled(Laser.Name) && magicimune && Utils.SleepCheck("Rearm"))
								Laser.UseAbility(target);
							else
								elsecount += 1;
							if (Ethereal != null && Ethereal.CanBeCasted() && Menu.Item("Items: ").GetValue<AbilityToggler>().IsEnabled(Ethereal.Name) && magicimune && Utils.SleepCheck("Rearm"))
							{
								Ethereal.UseAbility(target);
								if (Utils.SleepCheck("EtherealTime") && me.Distance2D(target) <= Ethereal.CastRange)
									Utils.Sleep((me.NetworkPosition.Distance2D(target.NetworkPosition) / 620) * 1000, "EtherealTime");
							}
							else
								elsecount += 1;
							if (Dagon != null && Dagon.CanBeCasted() && Menu.Item("Items: ").GetValue<AbilityToggler>().IsEnabled("item_dagon") && magicimune && Utils.SleepCheck("Rearm") && Utils.SleepCheck("EtherealTime"))
								Dagon.UseAbility(target);
							else
								elsecount += 1;
							if (Rocket != null && Rocket.CanBeCasted() && Menu.Item("Skills: ").GetValue<AbilityToggler>().IsEnabled(Rocket.Name) && magicimune && Utils.SleepCheck("Rearm"))
							{
								Rocket.UseAbility();
								if (Utils.SleepCheck("RocketTime") && me.Distance2D(target) <= Rocket.CastRange)
									Utils.Sleep((me.NetworkPosition.Distance2D(target.NetworkPosition) / 600) * 1000, "RocketTime");
							}
							else
								elsecount += 1;
							if (Shiva != null && Shiva.CanBeCasted() && me.Distance2D(target) <= 600 && Menu.Item("Items: ").GetValue<AbilityToggler>().IsEnabled(Shiva.Name) && magicimune && Utils.SleepCheck("Rearm"))
								Shiva.UseAbility();
							else
								elsecount += 1;
							if (elsecount == 10 && euls != null && euls.CanBeCasted() && Menu.Item("Items2: ").GetValue<AbilityToggler>().IsEnabled(euls.Name) && magicimune && Utils.SleepCheck("Rearm") && Utils.SleepCheck("EtherealTime") && Utils.SleepCheck("RocketTime"))
								euls.UseAbility(target);
							else
								elsecount += 1;
							if (elsecount == 11 && Refresh != null && Refresh.CanBeCasted() && Menu.Item("Skills: ").GetValue<AbilityToggler>().IsEnabled(Refresh.Name) && !Refresh.IsChanneling && Utils.SleepCheck("Rearm") && Ready_for_refresh())
							{
								Refresh.UseAbility();
								Utils.Sleep(800, "Rearm");
							}
							else
							{
								if (!me.IsChanneling() && !target.IsAttackImmune() && !me.IsAttackImmune() && Utils.SleepCheck("Rearm") && me.Distance2D(target) <= me.AttackRange && me.NetworkActivity != NetworkActivity.Attack)
									me.Attack(target);
								else
								{
									if (!me.IsChanneling() && (target.IsAttackImmune() || me.IsAttackImmune()) && Utils.SleepCheck("Rearm") && me.Distance2D(target) <= me.AttackRange - 100 && me.NetworkActivity != NetworkActivity.Attack)
										me.Move(target.Position);
								}
							}
							Utils.Sleep(150, "combo");
						}
					}
				}
				else
				{
					autoattack(false);
					if (!me.IsChanneling() && Utils.SleepCheck("Rearm"))
						me.Move(Game.MousePosition);
				}
			}
			else
				autoattack(false);
		}



		static void autoattack(bool key)
		{
			if (key)
			{
				if (auto_attack)
					Game.ExecuteCommand("dota_player_units_auto_attack 0");
				if (auto_attack_after_spell)
					Game.ExecuteCommand("dota_player_units_auto_attack_after_spell 0");
			}
			else
			{
				if (auto_attack)
					Game.ExecuteCommand("dota_player_units_auto_attack 1");
				if (auto_attack_after_spell)
					Game.ExecuteCommand("dota_player_units_auto_attack_after_spell 1");
			}

		}
		static bool iscreated = false;
		static void markedfordeath(EventArgs args)
		{
			if (!Game.IsInGame || Game.IsWatchingGame)
				return;
			me = ObjectManager.LocalHero;
			if (me == null)
				return;
			if (me.ClassID != ClassID.CDOTA_Unit_Hero_Tinker)
				return;
			target = me.ClosestToMouseTarget(50000);
			if (target != null && !target.IsIllusion && target.IsAlive)
			{
				Vector2 target_health_bar = HeroPositionOnScreen(target);
				Drawing.DrawText("Target to Death", target_health_bar, new Vector2(18, 200), me.Distance2D(target) < 1200 ? Color.Orchid : Color.Coral, FontFlags.AntiAlias | FontFlags.Additive | FontFlags.DropShadow);
			}
			for (int i = 0; i < SafePos.Count(); i++)
			{
				if (!iscreated)
				{
					ParticleEffect effect3 = new ParticleEffect(EffectPath, SafePos[i]);
					effect3.SetControlPoint(1, new Vector3(80, 0, 0));
					Effects.Add(effect3);
				}
			}
			iscreated = true;
		}
		static void FindItems()
		{
			//Skils
			Laser = me.Spellbook.SpellQ;
			Rocket = me.Spellbook.SpellW;
			Refresh = me.Spellbook.SpellR;
			March = me.Spellbook.SpellE;
			//Items
			blink = me.FindItem("item_blink");
			Dagon = me.Inventory.Items.FirstOrDefault(item => item.Name.Contains("item_dagon"));
			Hex = me.FindItem("item_sheepstick");
			Soulring = me.FindItem("item_soul_ring");
			Ethereal = me.FindItem("item_ethereal_blade");
			Shiva = me.FindItem("item_shivas_guard");
			ghost = me.FindItem("item_ghost");
			euls = me.FindItem("item_cyclone");
			forcestaff = me.FindItem("item_force_staff");
			glimmer = me.FindItem("item_glimmer_cape");
			vail = me.FindItem("item_veil_of_discord");
			orchid = me.FindItem("item_orchid");
		}
		static Vector2 HeroPositionOnScreen(Hero x)
		{
			float scaleX = HUDInfo.ScreenSizeX();
			float scaleY = HUDInfo.ScreenSizeY();
			Vector2 PicPosition;
			Drawing.WorldToScreen(x.Position, out PicPosition);
			PicPosition = new Vector2((float)(PicPosition.X + (scaleX * -0.035)), (float)((PicPosition.Y) + (scaleY * -0.10)));
			return PicPosition;
		}
		static bool Ready_for_refresh()
		{
			if ((ghost != null && ghost.CanBeCasted() && Menu.Item("Items2: ").GetValue<AbilityToggler>().IsEnabled(ghost.Name))
				|| (Soulring != null && Soulring.CanBeCasted() && Menu.Item("Items: ").GetValue<AbilityToggler>().IsEnabled(Soulring.Name))
				|| (Hex != null && Hex.CanBeCasted() && Menu.Item("Items: ").GetValue<AbilityToggler>().IsEnabled(Hex.Name))
				|| (Laser != null && Laser.CanBeCasted() && Menu.Item("Skills: ").GetValue<AbilityToggler>().IsEnabled(Laser.Name))
				|| (Ethereal != null && Ethereal.CanBeCasted() && Menu.Item("Items: ").GetValue<AbilityToggler>().IsEnabled(Ethereal.Name))
				|| (Dagon != null && Dagon.CanBeCasted() && Menu.Item("Items: ").GetValue<AbilityToggler>().IsEnabled("item_dagon"))
				|| (Rocket != null && Rocket.CanBeCasted() && Menu.Item("Skills: ").GetValue<AbilityToggler>().IsEnabled(Rocket.Name))
				|| (Shiva != null && Shiva.CanBeCasted() && Menu.Item("Items: ").GetValue<AbilityToggler>().IsEnabled(Shiva.Name))
				|| (euls != null && euls.CanBeCasted() && Menu.Item("Items2: ").GetValue<AbilityToggler>().IsEnabled(euls.Name))
				|| (glimmer != null && glimmer.CanBeCasted() && Menu.Item("Items2: ").GetValue<AbilityToggler>().IsEnabled(glimmer.Name)))
				return false;
			else
				return true;
		}
		private static void PrintSuccess(string text, params object[] arguments)
		{
			PrintEncolored(text, ConsoleColor.Green, arguments);
		}
		private static void PrintEncolored(string text, ConsoleColor color, params object[] arguments)
		{
			var clr = Console.ForegroundColor;
			Console.ForegroundColor = color;
			Console.WriteLine(text, arguments);
			Console.ForegroundColor = clr;
		}
		static bool IsLinkensProtected(Hero x)
		{
			if (x.Modifiers.Any(m => m.Name == "modifier_item_sphere_target") || x.FindItem("item_sphere") != null && x.FindItem("item_sphere").Cooldown <= 0)
				return true;
			else
				return false;
		}
	}
}
