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
    using Extensions;

    internal class PhoenixController : HeroController
    {
        private static bool Active, ultiToggle;
        private static Item veil, shiva, refresher, urn;
        private static Ability Dive, Spirits, Ulti, Sunray, SunrayControl;
        private static Menu menu;
        private static Hero me, target;

        public override void OnInject()
        {
            AssemblyExtensions.InitAssembly("Evervolv1337", "0.1b");

            DebugExtensions.Chat.PrintSuccess("Happy phoenixing!");

            menu = MenuExtensions.GetMenu();
            me = Variables.me;

            menu.AddItem(new MenuItem("enabled", "Enabled").SetValue(true));
            menu.AddItem(new MenuItem("keyBind", "Combo key").SetValue(new KeyBind('G', KeyBindType.Press)));

            var dict = new Dictionary<string, bool>
            {
                {"item_refresher", false},
                {"item_veil_of_discord", true},
                {"item_urn_of_shadows", true},
                {"phoenix_supernova", false},
                {"phoenix_sun_ray", true}
            };
            menu.AddItem(
                new MenuItem("enabledAbilities", " ").SetValue(new AbilityToggler(dict)));

            Orbwalking.Load();
        }

        public override void OnUpdateEvent(EventArgs args)
        {
            Active = Game.IsKeyDown(menu.Item("keyBind").GetValue<KeyBind>().Key) && !Game.IsChatOpen;

            var refresherToggle = menu.Item("enabledAbilities").GetValue<AbilityToggler>().IsEnabled("item_refresher");
            var veilToggle = menu.Item("enabledAbilities").GetValue<AbilityToggler>().IsEnabled("item_veil_of_discord");
            var urnToggle = menu.Item("enabledAbilities").GetValue<AbilityToggler>().IsEnabled("item_urn_of_shadows");
            ultiToggle = menu.Item("enabledAbilities").GetValue<AbilityToggler>().IsEnabled("phoenix_supernova");
            var sunrayToggle = menu.Item("enabledAbilities").GetValue<AbilityToggler>().IsEnabled("phoenix_sun_ray");

            if (!menu.Item("enabled").IsActive())
                return;

            // Items
            veil = me.FindItem("item_veil_of_discord");
            shiva = me.FindItem("item_shivas_guard");
            refresher = me.FindItem("item_refresher");
            urn = me.FindItem("item_urn_of_shadows");

            // Abilities
            Dive = me.Spellbook.Spell1;
            Spirits = me.Spellbook.Spell2;
            Sunray = me.Spellbook.Spell3;
            SunrayControl = me.Spellbook.Spell4;
            Ulti = me.FindSpell("phoenix_supernova");

            if (!me.CanCast() || !me.IsAlive || !Active)
                return;

            target = me.ClosestToMouseTarget(1000);
            if (target != null && target.IsAlive && !target.IsInvul())
            {
                if (veil != null && veil.CanBeCasted() && Utils.SleepCheck(veil.ToString()) && veilToggle)
                {
                    veil.UseAbility(target.Position);
                    Utils.Sleep(200, veil.ToString());
                }

                if (urn != null && urn.CanBeCasted() && Utils.SleepCheck(urn.ToString()) && urn.CurrentCharges > 0 &&
                    urnToggle)
                {
                    urn.UseAbility(target);
                    Utils.Sleep(200, urn.ToString());
                }

                if (shiva != null && shiva.CanBeCasted() && Utils.SleepCheck(shiva.ToString()))
                {
                    shiva.UseAbility();
                    Utils.Sleep(200, shiva.ToString());
                }

                if (Dive.CanBeCasted() && Utils.SleepCheck(Dive.Handle.ToString()))
                {
                    Dive.UseAbility(target.Position);

                    DelayAction.Add(new DelayActionItem(1000, CancelDive, new CancellationToken()));
                    if (ultiToggle)
                        DelayAction.Add(new DelayActionItem(2300, CastUltimate, new CancellationToken()));

                    Utils.Sleep(200, Dive.Handle.ToString());
                }

                if (Spirits.CanBeCasted() && Utils.SleepCheck(Spirits.ToString()))
                {
                    Spirits.UseAbility();
                    Utils.Sleep(200, Spirits.ToString());
                }

                if (me.Spellbook.Spell2.Name == me.FindSpell("phoenix_launch_fire_spirit").Name &&
                    Utils.SleepCheck("_SPIRITS_USE"))
                {
                    if (me.FindSpell("phoenix_launch_fire_spirit").CanBeCasted() &&
                        me.Modifiers.Any(x => x.Name == "modifier_phoenix_fire_spirit_count") &&
                        !target.Modifiers.Any(x => x.Name == "modifier_phoenix_fire_spirit_burn"))
                    {
                        me.FindSpell("phoenix_launch_fire_spirit")
                            .UseAbility(Prediction.SkillShotXYZ(me, target, 70, 900, 175));
                        Utils.Sleep(1900, "_SPIRITS_USE");
                    }
                }

                if (me.FindSpell("phoenix_sun_ray").CanBeCasted() &&
                    Utils.SleepCheck(me.FindSpell("phoenix_sun_ray").ToString()) &&
                    !me.Modifiers.Any(x => x.Name == "modifier_phoenix_sun_ray") &&
                    sunrayToggle)
                {
                    me.FindSpell("phoenix_sun_ray").UseAbility(Prediction.PredictedXYZ(target, 400));
                    Utils.Sleep(200, me.FindSpell("phoenix_sun_ray").ToString());
                }

                if (me.Modifiers.Any(x => x.Name == "modifier_phoenix_sun_ray") && Utils.SleepCheck("_SUNRAY_MOVE") &&
                    !Dive.CanBeCasted() &&
                    sunrayToggle)
                {
                    me.FindSpell("phoenix_sun_ray_toggle_move").UseAbility();
                    me.Move(Prediction.PredictedXYZ(target, 400));
                    Utils.Sleep(200, "_SUNRAY_MOVE");
                }

                if ((!(Dive.CanBeCasted() && Spirits.CanBeCasted()) || target.IsMagicImmune()) && me.CanAttack() &&
                    target != null &&
                    !me.IsChanneling() && !me.Modifiers.Any(x => x.Name == "modifier_phoenix_sun_ray"))
                {
                    Orbwalking.Orbwalk(target);
                }
            }
        }

        private static void CancelDive()
        {
            if (Utils.SleepCheck("_DIVE_COMPLETED") && me.Modifiers.Any(x => x.Name == "modifier_phoenix_icarus_dive"))
            {
                me.FindSpell("phoenix_icarus_dive_stop").UseAbility();
                Utils.Sleep(300, "_DIVE_COMPLETED");
            }
        }

        private static void CastUltimate()
        {
            if (Ulti.CanBeCasted() && !Dive.CanBeCasted() && Utils.SleepCheck(Ulti.ToString())
                && target != null && target.IsVisible)
            {
                Ulti.UseAbility();
                Utils.Sleep(1000, Ulti.ToString());
            }
        }
    }
}