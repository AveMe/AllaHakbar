namespace DotAllCombo.Heroes
{
    using System;
    using System.Linq;
    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;
    using Ensage.Common.Menu;
    using Extensions;
    using SharpDX;

    internal class StormSpiritController : HeroController
    {
        private static Menu menu;
        private static Hero me;
        private static bool _loaded;
        //public static bool GoAction { get; private set; }
        //private const int WmKeyup = 0x0101;
        private static readonly int[] TravelSpeeds = {1250, 1875, 2500};
        private static readonly double[] DamagePerUnit = {0.08, 0.12, 0.16};
        private static int _totalDamage;
        private static float _remainingMana;
        private static bool Active;
        public static Hero enemy { get; private set; }

        public override void OnInject()
        {
            AssemblyExtensions.InitAssembly("NeverMore", "0.1");

            DebugExtensions.Chat.PrintSuccess("Zip-Zap!");

            menu = MenuExtensions.GetMenu();
            me = Variables.me;
            menu.AddItem(new MenuItem("enabled", "Enabled").SetValue(true));
            menu.AddItem(new MenuItem("hotkey", "Combo key").SetValue(new KeyBind('D', KeyBindType.Press)));


            menu.AddItem(
                new MenuItem("maxrange", "Maximum Ultimate Range").SetValue(new Slider(1400, 300, 4000))
                    .SetTooltip("in distance"));
        }

        public override void OnUpdateEvent(EventArgs args)
        {
            Active = Game.IsKeyDown(menu.Item("hotkey").GetValue<KeyBind>().Key) && !Game.IsChatOpen;

            if (!Game.IsInGame || me == null)
            {
                return;
            }
            if (!Game.IsInGame || me == null)
            {
                _loaded = false;
                PrintInfo("> Storm unLoaded");
                return;
            }
            if (!menu.Item("enabled").IsActive())
                return;
            if (!Active)
            {
                enemy = null;
                return;
            }
            if ((enemy == null || me.Distance2D(enemy) >= menu.Item("maxrange").GetValue<Slider>().Value) &&
                Utils.SleepCheck("Move"))
            {
                me.Move(Game.MousePosition);
                Utils.Sleep(100, "Move");
            }
            //if (!GoAction) return;

            if (enemy == null || !enemy.IsValid)
            {
                enemy = ClosestToMouse(me);
            }

            if (enemy == null || !enemy.IsValid || !enemy.IsAlive || !me.CanCast()) return;

            var R = me.Spellbook.Spell4;

			var W = me.Spellbook.SpellW;
			if (R == null || R.Level == 0) return;
            var inUltimate = me.Modifiers.Any(x => x.Name == "modifier_storm_spirit_ball_lightning");
            var inPassve = me.Modifiers.Any(x => x.Name == "modifier_storm_spirit_overload");
            var inVortex = me.Modifiers.Any(x => x.Name == "modifier_storm_spirit_electric_vortex_self_slow");
            var RLevel = R.Level;
            var distance = me.Distance2D(enemy);
            var travelSpeed = TravelSpeeds[RLevel - 1];
            var damage = DamagePerUnit[RLevel - 1];
            var damageRadius = 50 + 75*RLevel;
            var startManaCost = 30 + me.MaximumMana*0.08;
            var costPerUnit = (12 + me.MaximumMana*0.007)/100.0;

            var totalCost = (int) (startManaCost + costPerUnit*(int) Math.Floor((decimal) distance/100)*100);

            var travelTime = distance/travelSpeed;

            var enemyHealth = enemy.Health;
            _remainingMana = me.Mana - totalCost + (me.ManaRegeneration*(travelTime + 1));

            var hpPerc = me.Health/(float) me.MaximumHealth;
            var mpPerc = me.Mana/me.MaximumMana;
            if (Utils.SleepCheck("mana_items"))
            {
                var soulring = me.FindItem("item_soul_ring");
                if (soulring != null && soulring.CanBeCasted() && hpPerc >= .1 && mpPerc <= .7)
                {
                    soulring.UseAbility();
                    Utils.Sleep(200, "mana_items");
                }
				var arcane = me.FindItem("item_arcane_boots");
				if ( // Arcane Boots Item
				arcane != null
				&& arcane.CanBeCasted()
				&& me.CanCast()
				&& me.Mana <= R.ManaCost
				)
				{
					arcane.UseAbility();
					Utils.Sleep(200, "mana_items");
				}
				var stick = me.FindItem("item_magic_stick") ?? me.FindItem("item_magic_wand");
                if (stick != null && stick.CanBeCasted() && stick.CurrentCharges != 0 && (mpPerc <= .5 || hpPerc <= .5))
                {
                    stick.UseAbility();
                    Utils.Sleep(200, "mana_items");
                }
            }

			#region Ultimate and Attack
			if (inVortex && !W.IsChanneling && !W.CanBeCasted())
            {
                if (distance < me.AttackRange)
                {
					if (
					me.Distance2D(enemy) <= me.AttackRange && (!me.IsAttackImmune() || !enemy.IsAttackImmune())
					&& me.NetworkActivity != NetworkActivity.Attack && me.CanAttack() && !me.IsAttacking() && Utils.SleepCheck("attack")
					)
					{
						me.Attack(enemy);
						Utils.Sleep(190, "attack");
					}
				}
            }
           if( W.CanBeCasted() && distance >= W.CastRange+40 && Utils.SleepCheck("Move"))
			{
				me.Move(enemy.Position);
				Utils.Sleep(350, "Move");
			}
            if (!inUltimate && Utils.SleepCheck("Ult") && !W.IsChanneling)
            {
				if (
					((!me.CanAttack() && me.Distance2D(enemy) >= 0) || me.Distance2D(enemy) >= me.AttackRange-50) 
					&& me.NetworkActivity != NetworkActivity.Attack && !me.IsAttacking()  &&
					me.Distance2D(enemy) <= menu.Item("maxrange").GetValue<Slider>().Value && Utils.SleepCheck("Move")
					)
				{
					me.Move(enemy.Predict(350));
					Utils.Sleep(350, "Move");
				}
				if (
					me.Distance2D(enemy) <= me.AttackRange && (!me.IsAttackImmune() || !enemy.IsAttackImmune())
					&& me.NetworkActivity != NetworkActivity.Attack && me.CanAttack() && !me.IsAttacking() && Utils.SleepCheck("attack") && !W.CanBeCasted()
					)
				{
					me.Attack(enemy);
					Utils.Sleep(190, "attack");
				}
			}

            #endregion

            _totalDamage = (int) (damage*distance);
            _totalDamage = (int) enemy.DamageTaken(_totalDamage, DamageType.Magical, me);

            if (enemyHealth < _totalDamage) return; //target ll die only from ultimate

            #region items

            if (Utils.SleepCheck("items") && distance <= menu.Item("maxrange").GetValue<Slider>().Value)
            {
                var hex = me.FindItem("item_sheepstick");
                var orchid = me.FindItem("item_orchid");
				
				var inVort = enemy.Modifiers.Any(x => x.Name == "modifier_storm_spirit_electric_vortex_pull");
				if (hex != null && hex.CanBeCasted(enemy) && !enemy.IsHexed() && !enemy.IsStunned() &&
                    !inVort && (!W.CanBeCasted() || distance >= W.CastRange+50))
                {
                    hex.UseAbility(enemy);
                    Utils.Sleep(250, "items");
                }
                else if (orchid != null && orchid.CanBeCasted(enemy) && !enemy.IsHexed() && !enemy.IsSilenced() &&
                         !enemy.IsStunned())
                {
                    orchid.UseAbility(enemy);
                    Utils.Sleep(250, "items");
                }

                var shiva = me.FindItem("item_shivas_guard");
                if (shiva != null && shiva.CanBeCasted() && distance <= 600)
                {
                    shiva.UseAbility();
                    Utils.Sleep(250, "items");
                }
                var dagon = me.GetDagon();
                /*me.FindItem("item_dagon")
                    ?? me.FindItem("item_dagon_2")
                    ?? me.FindItem("item_dagon_3") ?? me.FindItem("item_dagon_4") ?? me.FindItem("item_dagon_5");*/
                if (dagon != null && dagon.CanBeCasted(enemy) && distance <= dagon.CastRange)
                {
                    dagon.UseAbility(enemy);
                    Utils.Sleep(250, "items");
                }
            }

            #endregion

            #region Spells

            if (Utils.SleepCheck("spells"))
            {
                var Q = me.Spellbook.Spell1;
                if (Q != null && Q.CanBeCasted() && distance < 260 && !inPassve)
                {
                    Q.UseAbility();
                    Utils.Sleep(250, "spells");
                }
                else if (W != null && W.CanBeCasted(enemy) && distance < W.CastRange + 300 && !enemy.IsHexed() &&
                         !enemy.IsStunned())
                {
                    W.UseAbility(enemy);
                    Utils.Sleep(300, "spells");
                }
                else if (!inPassve && distance <= me.AttackRange-50 && !inVortex && Utils.SleepCheck("Rpos"))
                {
                    R.UseAbility(me.Position);
                    Utils.Sleep(390, "Rpos");
                }
				else if(distance >= me.AttackRange-10
					&& distance <= menu.Item("maxrange").GetValue<Slider>().Value && !inVortex && me.Mana>= R.ManaCost+100 && Utils.SleepCheck("R"))
				{
					R.CastSkillShot(enemy);
					Utils.Sleep(1000, "R");
				}
            }

            #endregion
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

        private static Hero ClosestToMouse(Hero source, float range = 1000)
        {
            var mousePosition = Game.MousePosition;
            var enemyHeroes =
				ObjectManager.GetEntities<Hero>()
                    .Where(
                        x =>
                            x.Team == source.GetEnemyTeam() && !x.IsIllusion && x.IsAlive && x.IsVisible
                            && x.Distance2D(mousePosition) <= range && !x.IsMagicImmune());
            Hero[] closestHero = {null};
            foreach (
                var enemyHero in
                    enemyHeroes.Where(
                        enemyHero =>
                            closestHero[0] == null ||
                            closestHero[0].Distance2D(mousePosition) > enemyHero.Distance2D(mousePosition)))
            {
                closestHero[0] = enemyHero;
            }
            return closestHero[0];
        }

        /*
				private static void DrawButton(Vector2 a, float w, float h, ref bool clicked, bool isActive, Color @on, Color off, string drawOnButtonText = "")
				{
					var isIn = Utils.IsUnderRectangle(Game.MouseScreenPosition, a.X, a.Y, w, h);
					if (isActive)
					{
						if (_leftMouseIsPress && Utils.SleepCheck("ClickButtonCd") && isIn)
						{
							clicked = !clicked;
							Utils.Sleep(250, "ClickButtonCd");
						}
						var newColor = isIn
							? new Color((int)(clicked ? @on.R : off.R), clicked ? @on.G : off.G, clicked ? @on.B : off.B, 150)
							: clicked ? @on : off;
						Drawing.DrawRect(a, new Vector2(w, h), newColor);
						Drawing.DrawRect(a, new Vector2(w, h), new Color(0, 0, 0, 255), true);
						if (drawOnButtonText != "")
						{
							Drawing.DrawText(drawOnButtonText, a + new Vector2(10, 2), Color.White,
							FontFlags.AntiAlias | FontFlags.DropShadow);
						}
					}
					else
					{
						Drawing.DrawRect(a, new Vector2(w, h), Color.Gray);
						Drawing.DrawRect(a, new Vector2(w, h), new Color(0, 0, 0, 255), true);
					}
				}
		*/

        private static void PrintInfo(string text, params object[] arguments)
        {
            PrintEncolored(text, ConsoleColor.Magenta, arguments);
        }

        private static void PrintSuccess(string text, params object[] arguments)
        {
            PrintEncolored(text, ConsoleColor.Blue, arguments);
        }

        // ReSharper disable once UnusedMember.Local
        private static void PrintError(string text, params object[] arguments)
        {
            PrintEncolored(text, ConsoleColor.DarkRed, arguments);
        }

        private static void PrintEncolored(string text, ConsoleColor color, params object[] arguments)
        {
            var clr = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text, arguments);
            Console.ForegroundColor = clr;
        }
    }
}