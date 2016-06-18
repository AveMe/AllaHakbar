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

	internal class TemplarAssassinController : HeroController
	{
		private static Ability Q, W, R;
		private static Item urn, dagon, phase, cheese, halberd, ethereal,
            mjollnir, orchid, abyssal, stick, mom, Shiva, mail, bkb, satanic, medall, blink, sheep;
        private static Menu menu;
		private static Hero e, me;
		private static bool Active;

		public override void OnInject()
		{
			AssemblyExtensions.InitAssembly("NeverMore", "0.1b");

			DebugExtensions.Chat.PrintSuccess("Blood!");

			menu = MenuExtensions.GetMenu();
			me = Variables.me;

			menu.AddItem(new MenuItem("enabled", "Enabled").SetValue(true));
			menu.AddItem(new MenuItem("keyBind", "Combo key").SetValue(new KeyBind('D', KeyBindType.Press)));

			var Skills = new Dictionary<string, bool>
			{
				{"templar_assassin_meld", true},
				{"templar_assassin_refraction", true},
				{"templar_assassin_psionic_trap", true}
			};
			var Items = new Dictionary<string, bool>
			{
				{"item_ethereal_blade", true},
				{"item_blink", true},
				{"item_heavens_halberd", true},
				{"item_orchid", true},
				{"item_urn_of_shadows", true},
				{"item_abyssal_blade", true},
				{"item_shivas_guard", true},
				{"item_blade_mail", true},
				{"item_black_king_bar", true},
				{"item_medallion_of_courage", true},
				{"item_solar_crest", true}
			};

			var Item = new Dictionary<string, bool>
			{
				 {"item_mask_of_madness", true},
				{"item_sheepstick", true},
				{"item_cheese", true},
				{"item_magic_stick", true},
				{"item_magic_wand", true},
				{"item_mjollnir", true},
				{"item_satanic", true},
				{"item_phase_boots", true}
			};

			menu.AddItem(
				new MenuItem("Skills", "Skills").SetValue(new AbilityToggler(Skills)));
			menu.AddItem(
				new MenuItem("Items", "Items:").SetValue(new AbilityToggler(Items)));
			menu.AddItem(
			   new MenuItem("Item", "Items:").SetValue(new AbilityToggler(Item)));
			menu.AddItem(new MenuItem("Heel", "Min targets to BKB").SetValue(new Slider(2, 1, 5)));
			menu.AddItem(new MenuItem("Heelm", "Min targets to BladeMail").SetValue(new Slider(2, 1, 5)));
		}

		public override void OnUpdateEvent(EventArgs args)
		{
			if (!menu.Item("enabled").IsActive())
				return;

			e = me.ClosestToMouseTarget(1800);
			if (e == null)
				return;
			Q = me.Spellbook.SpellQ;
			W = me.Spellbook.SpellW;
			R = me.Spellbook.SpellR;
			Active = Game.IsKeyDown(menu.Item("keyBind").GetValue<KeyBind>().Key);

			Shiva = me.FindItem("item_shivas_guard");
			ethereal = me.FindItem("item_ethereal_blade");
			mom = me.FindItem("item_mask_of_madness");
			urn = me.FindItem("item_urn_of_shadows");
			dagon =
				me.Inventory.Items.FirstOrDefault(
					item => item.Name.Contains("item_dagon"));
			halberd = me.FindItem("item_heavens_halberd");
			mjollnir = me.FindItem("item_mjollnir");
			orchid = me.FindItem("item_orchid");
			abyssal = me.FindItem("item_abyssal_blade");
			mail = me.FindItem("item_blade_mail");
			bkb = me.FindItem("item_black_king_bar");
			satanic = me.FindItem("item_satanic");
			blink = me.FindItem("item_blink");
			medall = me.FindItem("item_medallion_of_courage") ?? me.FindItem("item_solar_crest");
			sheep = e.ClassID == ClassID.CDOTA_Unit_Hero_Tidehunter ? null : me.FindItem("item_sheepstick");
			cheese = me.FindItem("item_cheese");
			stick = me.FindItem("item_magic_stick") ?? me.FindItem("item_magic_wand");
			phase = me.FindItem("item_phase_boots");

			var Meld = me.Modifiers.ToList().Exists(y => y.Name == "modifier_templar_assassin_meld");
			uint[] EDistance = { 60, 120, 180, 240 };
			var E = me.Spellbook.SpellE;
			

			var ModifEther = e.Modifiers.Any(y => y.Name == "modifier_item_ethereal_blade_slow");
			var stoneModif = e.Modifiers.Any(y => y.Name == "modifier_medusa_stone_gaze_stone");
			var v =
				ObjectManager.GetEntities<Hero>()
					.Where(x => x.Team != me.Team && x.IsAlive && x.IsVisible && !x.IsIllusion && !x.IsMagicImmune())
					.ToList();
			if (Active)
			{
				if (Meld && me.Distance2D(e) <= 190 + (EDistance[E.Level - 1]) && Utils.SleepCheck("attack"))
				{
					me.Attack(e);
					Utils.Sleep(150, "attack");
				}
			}
			
			if (Active && me.Distance2D(e) <= 1400 && e != null && e.IsAlive && !me.IsInvisible())
			{
				var traps =
					ObjectManager.GetEntities<Unit>()
						.Where(Unit => Unit.Name == "npc_dota_templar_assassin_psionic_trap")
						.ToList();
				foreach (var q in traps)
				{
					if (e.Position.Distance2D(q.Position) < 370 && q.Spellbook.SpellQ.CanBeCasted() &&
						Utils.SleepCheck("traps") &&
						!e.Modifiers.ToList().Exists(x => x.Name == "modifier_templar_assassin_trap_slow"))
					{
						q.Spellbook.SpellQ.UseAbility();
						Utils.Sleep(300, "traps");
					}
				}

				if (R.CanBeCasted()
					&& Utils.SleepCheck("ptrap")
					&& !e.Modifiers.ToList().Exists(x => x.Name == "modifier_templar_assassin_trap_slow")
					&& menu.Item("Skills").GetValue<AbilityToggler>().IsEnabled(R.Name))
				{
					R.UseAbility(e.Predict(700));
					Utils.Sleep(150, "ptrap");
				}
				if (
					Q != null && Q.CanBeCasted() && me.Distance2D(e) <= e.AttackRange + 100
					&& menu.Item("Skills").GetValue<AbilityToggler>().IsEnabled(Q.Name)
					&& Utils.SleepCheck("Q")
					)
				{
					Q.UseAbility();
					Utils.Sleep(200, "Q");
				}
				if (
					W != null && W.CanBeCasted() && me.Distance2D(e) <= EDistance[E.Level-1]+me.AttackRange-30
					&& menu.Item("Skills").GetValue<AbilityToggler>().IsEnabled(W.Name)
					&& Utils.SleepCheck("W")
					)
				{
					W.UseAbility();
					me.Attack(e);
					Utils.Sleep(200, "W");
				}
				if ( // MOM
						mom != null
						&& mom.CanBeCasted()
						&& me.CanCast()
						&& menu.Item("Item").GetValue<AbilityToggler>().IsEnabled(mom.Name)
						&& Utils.SleepCheck("mom")
						&& me.Distance2D(e) <= 700
						)
					{
						mom.UseAbility();
						Utils.Sleep(250, "mom");
					}
					if ( // Hellbard
						halberd != null
						&& halberd.CanBeCasted()
						&& me.CanCast()
						&& !e.IsMagicImmune()
						&& (e.NetworkActivity == NetworkActivity.Attack
							|| e.NetworkActivity == NetworkActivity.Crit
							|| e.NetworkActivity == NetworkActivity.Attack2)
						&& Utils.SleepCheck("halberd")
						&& me.Distance2D(e) <= 700
						&& menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(halberd.Name)
						)
					{
						halberd.UseAbility(e);
						Utils.Sleep(250, "halberd");
					}
					if ( // Mjollnir
						mjollnir != null
						&& mjollnir.CanBeCasted()
						&& me.CanCast()
						&& !e.IsMagicImmune()
						&& menu.Item("Item").GetValue<AbilityToggler>().IsEnabled(mjollnir.Name)
						&& Utils.SleepCheck("mjollnir")
						&& me.Distance2D(e) <= 900
						)
					{
						mjollnir.UseAbility(me);
						Utils.Sleep(250, "mjollnir");
					} // Mjollnir Item end
					if (
						// cheese
						cheese != null
						&& cheese.CanBeCasted()
						&& me.Health <= (me.MaximumHealth * 0.3)
						&& me.Distance2D(e) <= 700
						&& menu.Item("Item").GetValue<AbilityToggler>().IsEnabled(cheese.Name)
						&& Utils.SleepCheck("cheese")
						)
					{
						cheese.UseAbility();
						Utils.Sleep(200, "cheese");
					} // cheese Item end
					if ( // Medall
						medall != null
						&& medall.CanBeCasted()
						&& Utils.SleepCheck("Medall")
						&& menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(medall.Name)
						&& me.Distance2D(e) <= 700
						)
					{
						medall.UseAbility(e);
						Utils.Sleep(250, "Medall");
					} // Medall Item end

					if ( // sheep
						sheep != null
						&& sheep.CanBeCasted()
						&& me.CanCast()
						&& !e.IsLinkensProtected()
						&& !e.IsMagicImmune()
						&& me.Distance2D(e) <= 1400
						&& !stoneModif
						&& menu.Item("Item").GetValue<AbilityToggler>().IsEnabled(sheep.Name)
						&& Utils.SleepCheck("sheep")
						)
					{
						sheep.UseAbility(e);
						Utils.Sleep(250, "sheep");
					} // sheep Item end
					if ( // Abyssal Blade
						abyssal != null
						&& abyssal.CanBeCasted()
						&& me.CanCast()
						&& !e.IsStunned()
						&& !e.IsHexed()
						&& menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(abyssal.Name)
						&& Utils.SleepCheck("abyssal")
						&& me.Distance2D(e) <= 400
						)
					{
						abyssal.UseAbility(e);
						Utils.Sleep(250, "abyssal");
					} // Abyssal Item end
					if (orchid != null && orchid.CanBeCasted() && me.Distance2D(e) <= 900
						&& menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(orchid.Name) &&
						Utils.SleepCheck("orchid"))
					{
						orchid.UseAbility(e);
						Utils.Sleep(100, "orchid");
					}

					if (Shiva != null && Shiva.CanBeCasted() && me.Distance2D(e) <= 600
						&& menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(Shiva.Name)
						&& !e.IsMagicImmune() && Utils.SleepCheck("Shiva"))
					{
						Shiva.UseAbility();
						Utils.Sleep(100, "Shiva");
					}
					if ( // ethereal
						ethereal != null
						&& ethereal.CanBeCasted()
						&& me.CanCast()
						&& !e.IsLinkensProtected()
						&& !e.IsMagicImmune()
						&& !stoneModif
						&& menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(ethereal.Name)
						&& Utils.SleepCheck("ethereal")
						)
					{
						ethereal.UseAbility(e);
						Utils.Sleep(200, "ethereal");
					} // ethereal Item end
					if (
						blink != null
						&& me.CanCast()
						&& blink.CanBeCasted()
						&& me.Distance2D(e) >= EDistance[E.Level - 1] + me.AttackRange - 25
						&& me.Distance2D(e.Predict(550)) <= 1170
						&& menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(blink.Name)
						&& Utils.SleepCheck("blink")
						)
					{
						blink.UseAbility(e.Predict(550));
						Utils.Sleep(250, "blink");
					}
					
					if ( // Dagon
						me.CanCast()
						&& dagon != null
						&& (ethereal == null
							|| (ModifEther
								|| ethereal.Cooldown < 17))
						&& !e.IsLinkensProtected()
						&& dagon.CanBeCasted()
						&& !e.IsMagicImmune()
						&& !stoneModif
						&& Utils.SleepCheck("dagon")
						)
					{
						dagon.UseAbility(e);
						Utils.Sleep(200, "dagon");
					} // Dagon Item end
					if (phase != null
						&& phase.CanBeCasted()
						&& Utils.SleepCheck("phase")
						&& menu.Item("Item").GetValue<AbilityToggler>().IsEnabled(phase.Name)
						&& !blink.CanBeCasted()
						&& me.Distance2D(e) >= me.AttackRange + 20)
					{
						phase.UseAbility();
						Utils.Sleep(200, "phase");
					}
					if (urn != null && urn.CanBeCasted() && urn.CurrentCharges > 0 && me.Distance2D(e) <= 400
						&& menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(urn.Name) && Utils.SleepCheck("urn"))
					{
						urn.UseAbility(e);
						Utils.Sleep(240, "urn");
					}
					if (
						stick != null
						&& stick.CanBeCasted()
						&& stick.CurrentCharges != 0
						&& me.Distance2D(e) <= 700
						&& (me.Health <= (me.MaximumHealth * 0.5)
							|| me.Mana <= (me.MaximumMana * 0.5))
						&& menu.Item("Item").GetValue<AbilityToggler>().IsEnabled(stick.Name))
					{
						stick.UseAbility();
						Utils.Sleep(200, "mana_items");
					}
					if ( // Satanic 
						satanic != null &&
						me.Health <= (me.MaximumHealth * 0.3) &&
						satanic.CanBeCasted() &&
						me.Distance2D(e) <= me.AttackRange + 50
						&& menu.Item("Item").GetValue<AbilityToggler>().IsEnabled(satanic.Name)
						&& Utils.SleepCheck("satanic")
						)
					{
						satanic.UseAbility();
						Utils.Sleep(240, "satanic");
					} // Satanic Item end
					if (mail != null && mail.CanBeCasted() && (v.Count(x => x.Distance2D(me) <= 650) >=
															   (menu.Item("Heelm").GetValue<Slider>().Value)) &&
						menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(mail.Name) && Utils.SleepCheck("mail"))
					{
						mail.UseAbility();
						Utils.Sleep(100, "mail");
					}
					if (bkb != null && bkb.CanBeCasted() && (v.Count(x => x.Distance2D(me) <= 650) >=
															 (menu.Item("Heel").GetValue<Slider>().Value)) &&
						menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(bkb.Name) && Utils.SleepCheck("bkb"))
					{
						bkb.UseAbility();
						Utils.Sleep(100, "bkb");
					}
				if (Meld)
					return;
				if (Active && me.Distance2D(e) <= 1400 && e != null && e.IsAlive)
				{
					if (
						(!me.CanAttack() || me.Distance2D(e) >= 0) && me.NetworkActivity != NetworkActivity.Attack &&
						me.Distance2D(e) <= 900 && Utils.SleepCheck("Move")
						)
					{
						me.Move(e.Predict(400));
						Utils.Sleep(390, "Move");
					}
					if (
						me.Distance2D(e) <= me.AttackRange + 100 &&
						(!me.IsAttackImmune() || !e.IsAttackImmune())
						&& me.NetworkActivity != NetworkActivity.Attack && me.CanAttack() && Utils.SleepCheck("attack")
						)
					{
						me.Attack(e);
						Utils.Sleep(150, "attack");
					}
				}
			}
		}
	}
}