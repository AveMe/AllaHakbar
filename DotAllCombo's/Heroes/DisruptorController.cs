namespace DotAllCombo.Heroes
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Ensage;
	using Ensage.Common;
	using Ensage.Common.Extensions;
	using SharpDX;
	using Ensage.Common.Menu;
	using Extensions;

	internal class DisruptorController : HeroController
	{
		private static Ability Q, W, E, R;

		private static Item urn,
			ethereal,
			dagon,
			halberd,
			vail,
			orchid,
			Shiva,
			mail,
			bkb,
			medall,
			blink;

		private static Menu menu;
		private static Hero e, me;
		private static bool Active, ActiveW;

		private static readonly Menu ult = new Menu("AutoUlt", "AutoUlt");

		public override void OnInject()
		{
			AssemblyExtensions.InitAssembly("NeverMore", "0.1b");

			DebugExtensions.Chat.PrintSuccess("Back to me!");

			menu = MenuExtensions.GetMenu();
			me = Variables.me;

			menu.AddItem(new MenuItem("enabled", "Enabled").SetValue(true));
			menu.AddItem(new MenuItem("keyBind", "Combo key").SetValue(new KeyBind('D', KeyBindType.Press)));
			menu.AddItem(new MenuItem("keyBindW", "Combo Glimpse").SetValue(new KeyBind('W', KeyBindType.Press)));

			var Skills = new Dictionary<string, bool>
			{
				{"disruptor_glimpse", true},
				{"disruptor_kinetic_field", true},
				{"disruptor_static_storm", true},
				{"disruptor_thunder_strike", true}
			};
			var Items = new Dictionary<string, bool>
			{
				{"item_blink", true},
				{"item_heavens_halberd", true},
				{"item_orchid", true},
				{"item_urn_of_shadows", true},
				{"item_ethereal_blade", true},
				{"item_veil_of_discord", true},
				{"item_shivas_guard", true},
				{"item_blade_mail", true},
				{"item_black_king_bar", true},
				{"item_medallion_of_courage", true},
				{"item_solar_crest", true}
			};
			menu.AddItem(
				new MenuItem("Skills", "Skills").SetValue(new AbilityToggler(Skills)));
			menu.AddItem(
				new MenuItem("Items", "Items:").SetValue(new AbilityToggler(Items)));
			menu.AddItem(new MenuItem("Heel", "Min targets to BKB").SetValue(new Slider(2, 1, 5)));
			menu.AddItem(new MenuItem("Heelm", "Min targets to BladeMail").SetValue(new Slider(2, 1, 5)));


			menu.AddSubMenu(ult);

			ult.AddItem(new MenuItem("autoUlt", "AutoUlt & Field").SetValue(true));//DOTO: название
		}

		public override void OnUpdateEvent(EventArgs args)
		{
			if (!menu.Item("enabled").IsActive())
				return;

			e = me.ClosestToMouseTarget(1800);
			if (e == null)
				return;
			Active = Game.IsKeyDown(menu.Item("keyBind").GetValue<KeyBind>().Key);
			ActiveW = Game.IsKeyDown(menu.Item("keyBindW").GetValue<KeyBind>().Key);
			Q = me.Spellbook.SpellQ;
			W = me.Spellbook.SpellW;
			E = me.Spellbook.SpellE;
			R = me.Spellbook.SpellR;
			blink = me.FindItem("item_blink");
			urn = me.FindItem("item_urn_of_shadows");
			dagon = me.Inventory.Items.FirstOrDefault(x => x.Name.Contains("item_dagon"));
			ethereal = me.FindItem("item_ethereal_blade");
			halberd = me.FindItem("item_heavens_halberd");
			orchid = me.FindItem("item_orchid");
			mail = me.FindItem("item_blade_mail");
			bkb = me.FindItem("item_black_king_bar");
			medall = me.FindItem("item_medallion_of_courage") ?? me.FindItem("item_solar_crest");
			Shiva = me.FindItem("item_shivas_guard");
			vail = me.FindItem("item_veil_of_discord");

			var ModifRod = e.Modifiers.Any(y => y.Name == "modifier_rod_of_atos_debuff");
			var ModifEther = e.Modifiers.Any(y => y.Name == "modifier_item_ethereal_blade_slow");
			var ModifVail = e.Modifiers.Any(y => y.Name == "modifier_item_veil_of_discord_debuff");
			var stoneModif = e.Modifiers.Any(y => y.Name == "modifier_medusa_stone_gaze_stone");
			var v =
				ObjectManager.GetEntities<Hero>()
					.Where(x => x.Team != me.Team && x.IsAlive && x.IsVisible && !x.IsIllusion && !x.IsMagicImmune())
					.ToList();
			var ModifInv =
				me.IsInvisible();
			if (menu.Item("autoUlt").IsActive())
			{
				var baseDota =
			   ObjectManager.GetEntities<Unit>().Where(unit => unit.Name == "npc_dota_base" ).ToList();
				var thinker =
			   ObjectManager.GetEntities<Unit>().Where(unit => unit.Name == "npc_dota_thinker").ToList();

                        var eModif = e.Modifiers.FirstOrDefault(y => y.Name == "modifier_disruptor_glimpse");

						if (e.Distance2D(me) <= 950
								&& menu.Item("Skills").GetValue<AbilityToggler>().IsEnabled(E.Name))
						{
							if (E != null && E.CanBeCasted() && me.Distance2D(e) <= E.CastRange + 200 
						&& (e.MovementSpeed <= 250 
						|| e.IsStunned()
						|| e.IsHexed()
						|| (e.NetworkActivity != NetworkActivity.Move && Active)

						)
						&& Utils.SleepCheck("E"))
							{
								E.UseAbility(e.Position);
								Utils.Sleep(190, "E");
							}
						}

				if (thinker != null)
				{
					for (int i = 0; i < thinker.Count(); i++)
					{
						if (thinker[i].Modifiers.Any(y => y.Name == "modifier_disruptor_kinetic_field_thinker") && e.Distance2D(thinker[i]) <= 350
							  && menu.Item("Skills").GetValue<AbilityToggler>().IsEnabled(R.Name))
						{
							if (R != null && R.CanBeCasted() && me.Distance2D(e) <= R.CastRange + 300 && Utils.SleepCheck("R"))
							{
								R.UseAbility(thinker[i].Position);
								Utils.Sleep(190, "R");
							}
						}
					}
				}
			}
			if (ActiveW)
			{
				if (
					W != null && W.CanBeCasted() && me.Distance2D(e) <= 1500
					&& menu.Item("Skills").GetValue<AbilityToggler>().IsEnabled(W.Name)
					&& Utils.SleepCheck("W")
					)
				{
					W.UseAbility(e);
					Utils.Sleep(200, "W");
				}
			}
				if (Active)
			{

				if (
					((!me.CanAttack() && me.Distance2D(e) >= 0) || me.Distance2D(e) >= 370) && me.NetworkActivity != NetworkActivity.Attack &&
					me.Distance2D(e) <= 1500 && Utils.SleepCheck("Move")
					)
				{
					me.Move(e.Predict(350));
					Utils.Sleep(350, "Move");
				}
				if (
					me.Distance2D(e) <= 600 && (!me.IsAttackImmune() || !e.IsAttackImmune())
					&& me.NetworkActivity != NetworkActivity.Attack && me.CanAttack() && Utils.SleepCheck("attack")
					)
				{
					me.Attack(e);
					Utils.Sleep(190, "attack");
				}
			}
			if (Active && me.Distance2D(e) <= 1400 && e != null && e.IsAlive && !ModifInv)
			{
				float angle = me.FindAngleBetween(e.Position, true);
				Vector3 pos = new Vector3((float)(e.Position.X - 400 * Math.Cos(angle)), (float)(e.Position.Y - 400 * Math.Sin(angle)), 0);

				if (
				blink != null
				&& me.CanCast()
				&& blink.CanBeCasted()
				&& me.Distance2D(e) >= 360
				&& me.Distance2D(pos) <= 1180
				&& !stoneModif
				&& menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(blink.Name)
				&& Utils.SleepCheck("blink")
				)
				{
					blink.UseAbility(pos);
					Utils.Sleep(250, "blink");
				}
				if (orchid != null && orchid.CanBeCasted() && me.Distance2D(e) <= 900 &&
					   menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(orchid.Name) && Utils.SleepCheck("orchid"))
				{
					orchid.UseAbility(e);
					Utils.Sleep(100, "orchid");
				}
				if ( // vail
				vail != null
				&& vail.CanBeCasted()
				&& me.CanCast()
				&& !e.IsMagicImmune()
				&& menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(vail.Name)
				&& me.Distance2D(e) <= 1500
				&& Utils.SleepCheck("vail")
				)
				{
					vail.UseAbility(e.Position);
					Utils.Sleep(250, "vail");
				} // orchid Item end
				if (ethereal != null && ethereal.CanBeCasted()
					   && me.Distance2D(e) <= 700 && me.Distance2D(e) <= 400
					   && menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(ethereal.Name) &&
					   Utils.SleepCheck("ethereal"))
				{
					ethereal.UseAbility(e);
					Utils.Sleep(100, "ethereal");
				}
				if ( // Dagon
					me.CanCast()
					&& dagon != null
					&& (ethereal == null
						|| (ModifEther
							|| ethereal.Cooldown < 17))
					&& !e.IsLinkensProtected()
					&& dagon.CanBeCasted()
					&& me.Distance2D(e) <= 1400
					&& !e.IsMagicImmune()
					&& !stoneModif
					&& Utils.SleepCheck("dagon")
					)
				{
					dagon.UseAbility(e);
					Utils.Sleep(200, "dagon");
				} // Dagon Item end
				if (
					Q != null && Q.CanBeCasted() && me.Distance2D(e) <= Q.CastRange+100
					&& menu.Item("Skills").GetValue<AbilityToggler>().IsEnabled(Q.Name)
					&& Utils.SleepCheck("Q")
					)
				{
					Q.UseAbility(e);
					Utils.Sleep(200, "Q");
				}
				
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
				

				if (Shiva != null && Shiva.CanBeCasted() && me.Distance2D(e) <= 600
					&& menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(Shiva.Name)
					&& !e.IsMagicImmune() && Utils.SleepCheck("Shiva"))
				{
					Shiva.UseAbility();
					Utils.Sleep(100, "Shiva");
				}
				if (urn != null && urn.CanBeCasted() && urn.CurrentCharges > 0 && me.Distance2D(e) <= 600
					&& menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(urn.Name) && Utils.SleepCheck("urn"))
				{
					urn.UseAbility(e);
					Utils.Sleep(240, "urn");
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
			}
		}
	}
}