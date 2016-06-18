namespace DotAllCombo.Heroes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Timers;
    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;
    using Ensage.Common.Menu;
    using Extensions;
    using SharpDX;

    internal class PudgeController : HeroController
    {
        private static Ability Q, W, R;
        private static Item urn, ethereal, dagon, glimmer, vail, orchid, atos, leans, Shiva, mail;
        private static Menu menu;
        private static Hero e, me;
        private static bool Active;
        private static Vector3 hookPosition;
        private static double timeTurn, rangeHero;
        public static readonly Timer time = new Timer(100);

        public override void OnInject()
		{
			AssemblyExtensions.InitAssembly("NeverMore", "0.1b");
			DebugExtensions.Chat.PrintSuccess("Happy hookz!");
			time.Elapsed += Stops;
			menu = MenuExtensions.GetMenu();
            me = Variables.me;

            var Skills = new Dictionary<string, bool>
            {
				{"pudge_dismember", true},
                {"pudge_meat_hook", true}
            };
            var Items = new Dictionary<string, bool>
            {
                {"item_dagon", true},
                {"item_orchid", true},
                {"item_urn_of_shadows", true},
                {"item_ethereal_blade", true},
                {"item_veil_of_discord", true},
                {"item_rod_of_atos", true},
                {"item_shivas_guard", true},
                {"item_blade_mail", true}
            };

			menu.AddItem(new MenuItem("enabled", "Enabled Script").SetValue(true));
			menu.AddItem(new MenuItem("keyBind", "Combo key").SetValue(new KeyBind('D', KeyBindType.Press)));
			menu.AddItem(
                new MenuItem("Skills", "Skills").SetValue(new AbilityToggler(Skills)));
            menu.AddItem(
                new MenuItem("Items", "Items:").SetValue(new AbilityToggler(Items)));
            menu.AddItem(new MenuItem("x", "Cancelling Hook").SetValue(new Slider(50, 1, 68)));

			menu.AddItem(new MenuItem("z", "Cancelling Time Hook").SetValue(new Slider(70, 70, 290)));
		}
		public override void OnUpdateEvent(EventArgs args)
        {
            Active = Game.IsKeyDown(menu.Item("keyBind").GetValue<KeyBind>().Key);

            Q = me.Spellbook.SpellQ;
            W = me.Spellbook.SpellW;
            R = me.Spellbook.SpellR;

            leans = me.FindItem("item_aether_lens");
            urn = me.FindItem("item_urn_of_shadows");
            dagon = me.Inventory.Items.FirstOrDefault(x => x.Name.Contains("item_dagon"));
            ethereal = me.FindItem("item_ethereal_blade");
            glimmer = me.FindItem("item_glimmer_cape");
            vail = me.FindItem("item_veil_of_discord");
            orchid = me.FindItem("item_orchid");
            atos = me.FindItem("item_rod_of_atos");
            mail = me.FindItem("item_blade_mail");
            Shiva = me.FindItem("item_shivas_guard");

            var v =
                ObjectManager.GetEntities<Hero>()
                    .Where(x => x.Team != me.Team && x.IsAlive && x.IsVisible && !x.IsIllusion && !x.IsMagicImmune())
                    .ToList();
			if (Active &&  e == null && me.IsAlive && me.Modifiers.Any(x => x.Name == "modifier_pudge_rot") && Utils.SleepCheck("W"))
			{
				W.ToggleAbility();
				Utils.Sleep(400, "W");
			}
				e = me.ClosestToMouseTarget(1800);

            if (Active && Q.CanBeCasted() && e != null && e.IsAlive && menu.Item("Skills").GetValue<AbilityToggler>().IsEnabled(Q.Name))
            {

                if (R.IsInAbilityPhase || R.IsChanneling || me.IsChanneling())
                    return;
				
				rangeHero = e.HullRadius + 27;
                /*
				Vector3 pPos = me.Position;
				Vector3 vPos = me.Position;
				var units = ObjectManager.GetEntities<Unit>().Where(x => x.IsAlive && !x.Equals(me) && !x.Equals(e) && (x.ClassID == ClassID.CDOTA_BaseNPC_Creep_Neutral 
				|| x.ClassID == ClassID.CDOTA_BaseNPC_Creep_Siege || x.ClassID == ClassID.CDOTA_BaseNPC_Creep || x.ClassID == ClassID.CDOTA_BaseNPC_Creep_Lane)).ToList();
				foreach (Unit unit in units)
				{
					Vector3 uPos = unit.Position;
					if (Math.Abs(Math.Sqrt(Math.Pow(pPos.X - vPos.X, 2) + Math.Pow(pPos.Y - vPos.Y, 2))
					+ Math.Sqrt(Math.Pow(uPos.X - vPos.X, 2) + Math.Pow(uPos.Y - vPos.Y, 2))
					- Math.Sqrt(Math.Pow(uPos.X - pPos.X, 2) + Math.Pow(uPos.Y - pPos.Y, 2))) <= e.HullRadius + 100 && Utils.SleepCheck("hook"))
					{
						me.Stop();
						Utils.Sleep(150, "hook");
					}
				} */
                if (
                    atos != null && atos.CanBeCasted() && me.CanCast() && !e.IsMagicImmune() &&
                    menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(atos.Name) && me.Distance2D(e) <= 1500 &&
                    Utils.SleepCheck("a")
                    )
                {
                    atos.UseAbility(e);
                    Utils.Sleep(250, "a");
                }
                else if (e != null && e.NetworkActivity == NetworkActivity.Move)
                {
                    var Vector = new Vector2((float) Math.Cos(e.RotationRad)*e.MovementSpeed,
                        (float) Math.Sin(e.RotationRad)*e.MovementSpeed);
                    var Start =
                        new Vector3(
                            (float) ((0.3 + (Game.Ping/1000))*Math.Cos(e.RotationRad)*e.MovementSpeed + e.Position.X),
                            (float)
                                ((0.3 + (Game.Ping/1000))*Math.Sin(e.RotationRad)*e.MovementSpeed + e.NetworkPosition.Y),
                            e.NetworkPosition.Z);

                    hookPosition = Interception(Start, Vector, me.Position, 1600);
                    for (var i = 0.03; i <= 0.135; i += 0.03)
                    {
                        var Estimated = new Vector3(
                            (float) (i*Math.Cos(e.RotationRad)*e.MovementSpeed + hookPosition.X),
                            (float) (i*Math.Sin(e.RotationRad)*e.MovementSpeed + hookPosition.Y), e.NetworkPosition.Z);
                        if (GetTimeToTurn(Estimated) <= i)
                        {
                            hookPosition = Estimated;
                            timeTurn = i;
                            break;
                        }
                    }
                    if ((e != null && leans != null &&
                         me.Position.Distance2D(hookPosition) > (Q.CastRange + 200 + e.HullRadius)) ||
                        (e != null && leans == null &&
                         me.Position.Distance2D(hookPosition) > (Q.CastRange + e.HullRadius)))
                        return;
                    Q.UseAbility((hookPosition - me.Position)*((Q.CastRange)/hookPosition.Distance2D(me.Position)) +
                                 me.Position);
                    time.Interval = 150 + timeTurn*1000;
                    time.Start();
                }
                else
                {
                    var SpecialPosition = new Vector3((float) (rangeHero*Math.Cos(e.RotationRad) + e.NetworkPosition.X),
                        (float) (rangeHero*Math.Sin(e.RotationRad) + e.NetworkPosition.Y),
                        e.NetworkPosition.Z);
                    if ((leans != null && me.Position.Distance2D(e.NetworkPosition) > (Q.CastRange + 200 + e.HullRadius)) ||
                        (leans == null && me.Position.Distance2D(e.NetworkPosition) > (Q.CastRange + e.HullRadius)))
                        return;

                    Q.UseAbility((SpecialPosition - me.Position)*((Q.CastRange)/SpecialPosition.Distance2D(me.Position)) +
                                 me.Position);
                    time.Start();
                }
            }
            else if (Active && me.Distance2D(e) <= 1400  && e.IsAlive)
            {
                uint elsecount = 0;
                if (me.Distance2D(e) <= 285 && !me.Modifiers.Any(x => x.Name == "modifier_pudge_rot") &&
                    !e.IsMagicImmune() && Utils.SleepCheck("W"))
                {
                    W.ToggleAbility();
                    Utils.Sleep(400, "W");
                }
                if ((me.Distance2D(e) >= 315 || e.IsMagicImmune()) &&
                    me.Modifiers.Any(x => x.Name == "modifier_pudge_rot") &&
                    !e.Modifiers.Any(x => x.Name == "modifier_pudge_rot") && Utils.SleepCheck("W"))
                {
                    W.ToggleAbility();
                    Utils.Sleep(400, "W");
                }
                if (R.IsInAbilityPhase || R.IsChanneling)
                    return;
                elsecount++;
                if (vail != null && vail.CanBeCasted() && me.Distance2D(e) <= 1100 &&
                    menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(vail.Name) && Utils.SleepCheck("vail"))
                {
                    vail.UseAbility(e.Position);
                    Utils.Sleep(130, "vail");
                }
                else elsecount++;
                if (orchid != null && orchid.CanBeCasted() && me.Distance2D(e) <= 900 &&
                    menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(orchid.Name) && Utils.SleepCheck("orchid"))
                {
                    orchid.UseAbility(e);
                    Utils.Sleep(100, "orchid");
                }
                else elsecount++;
                if (Shiva != null && Shiva.CanBeCasted() && me.Distance2D(e) <= 600 &&
                    menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(Shiva.Name) && !e.IsMagicImmune() &&
                    Utils.SleepCheck("Shiva"))
                {
                    Shiva.UseAbility();
                    Utils.Sleep(100, "Shiva");
                }
                else elsecount++;
                if (ethereal != null && ethereal.CanBeCasted() && me.Distance2D(e) <= 700 && me.Distance2D(e) <= 400 &&
                    menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(ethereal.Name) &&
                    Utils.SleepCheck("ethereal"))
                {
                    ethereal.UseAbility(e);
                    Utils.Sleep(100, "ethereal");
                }
                else elsecount++;
                if (dagon != null && dagon.CanBeCasted() && me.Distance2D(e) <= 500 &&
                    menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(dagon.Name) && Utils.SleepCheck("dagon"))
                {
                    dagon.UseAbility(e);
                    Utils.Sleep(100, "dagon");
                }
                else elsecount++;
                if (urn != null && urn.CanBeCasted() && urn.CurrentCharges > 0 && me.Distance2D(e) <= 400 &&
                    menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(urn.Name) && Utils.SleepCheck("urn"))
                {
                    urn.UseAbility(e);
                    Utils.Sleep(240, "urn");
                }
                else elsecount++;
                if (glimmer != null && glimmer.CanBeCasted() && me.Distance2D(e) <= 300 && Utils.SleepCheck("glimmer"))
                {
                    glimmer.UseAbility(me);
                    Utils.Sleep(200, "glimmer");
                }
                else elsecount++;
                if (mail != null && mail.CanBeCasted() && (v.Count(x => x.Distance2D(me) <= 650) >= 2) &&
                    menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(mail.Name) && Utils.SleepCheck("mail"))
                {
                    mail.UseAbility();
                    Utils.Sleep(100, "mail");
                }
                else elsecount++;
                if (elsecount == 9 &&
                    R != null && R.CanBeCasted() && me.Distance2D(e) <= 500 &&
                    (!urn.CanBeCasted() || urn.CurrentCharges <= 0) &&
                    menu.Item("Skills").GetValue<AbilityToggler>().IsEnabled(R.Name)
                    && Utils.SleepCheck("R")
                    )
                {
                    R.UseAbility(e);
                    Utils.Sleep(200, "R");
                }
                if (
                    (!me.CanAttack() || me.Distance2D(e) >= 0) && me.NetworkActivity != NetworkActivity.Attack &&
                    me.Distance2D(e) <= 455 && Utils.SleepCheck("Move")
                    )
                {
                    me.Move(e.Predict(300));
                    Utils.Sleep(390, "Move");
                }
                else if (
                    me.Distance2D(e) <= 150 && (!R.CanBeCasted() || R == null) &&
                    (!me.IsAttackImmune() || !e.IsAttackImmune()) && me.NetworkActivity != NetworkActivity.Attack &&
                    me.CanAttack() && Utils.SleepCheck("attack")
                    )
                {
                    me.Attack(e);
                    Utils.Sleep(190, "attack");
                }
            }
        }

        private static Vector3 Interception(Vector3 a, Vector2 v, Vector3 b, float s)
        {
            var ox = a.X - b.X;
            var oy = a.Y - b.Y;

            var h1 = v.X*v.X + v.Y*v.Y - s*s;
            var h2 = ox*v.X + oy*v.Y;
            float t;

            if (h1 == 0)
            {
                t = -(ox*ox + oy*oy)/2*h2;
            }
            else
            {
                var minusPHalf = -h2/h1;
                var discriminant = minusPHalf*minusPHalf - (ox*ox + oy*oy)/h1;

                var root = (float) Math.Sqrt(discriminant);

                var t1 = minusPHalf + root;
                var t2 = minusPHalf - root;

                var tMin = Math.Min(t1, t2);
                var tMax = Math.Max(t1, t2);

                t = tMin > 0 ? tMin : tMax;
            }
            return new Vector3(a.X + t*v.X, a.Y + t*v.Y, a.Z);
        }

		public static void Stops(object s, ElapsedEventArgs args)
		{
			double TravelTime = hookPosition.Distance2D(me.Position) / 1600;
			Vector3 ePosition = new Vector3((float)((TravelTime) * Math.Cos(e.RotationRad) * e.MovementSpeed + e.NetworkPosition.X),
										   (float)((TravelTime) * Math.Sin(e.RotationRad) * e.MovementSpeed + e.NetworkPosition.Y), 0);
			if (e != null && e.NetworkActivity == NetworkActivity.Move && ePosition.Distance2D(hookPosition) > rangeHero + menu.Item("x").GetValue<Slider>().Value)
			{
				me.Stop();
				time.Stop();
			}
			else
			{
				if (W.CanBeCasted())
					time.Stop();
			}
		}

		private static float GetTimeToTurn(Vector3 x)
        {
            var m = ObjectManager.LocalHero;
            var deltaY = m.Position.Y - x.Y;
            var deltaX = m.Position.X - x.X;
            var angle = (float) (Math.Atan2(deltaY, deltaX));

            return
                (float)
                    ((Math.PI - Math.Abs(Math.Atan2(Math.Sin(m.RotationRad - angle), Math.Cos(m.RotationRad - angle))))*
                     (0.03/0.7));
        }
    }
}