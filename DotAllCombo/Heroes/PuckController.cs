namespace DotAllCombo.Heroes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;
    using Ensage.Common.Menu;
    using Extensions;
    using SharpDX;

    internal class PuckController : HeroController
    {
        private static Ability Q, W, E, D, R;
        private static Item Blink, dagon, ethereal, euls, forcestaff, shivas, malevo, Hex;
        private static Menu menu;
        private static Hero me, target;
        private static Vector3 QPosition;
        private static readonly Dictionary<Hero, string> CreepsDictionary = new Dictionary<Hero, string>();
        private static readonly Dictionary<Hero, Team> CreepsTeamDictionary = new Dictionary<Hero, Team>();
        private static Bool _screenSizeLoaded = false;
        private static int stage;
        private static bool ethereal_used, ethereal_used2;

        public override void OnInject()
        {
            AssemblyExtensions.InitAssembly("NeverMore", "0.1");

            DebugExtensions.Chat.PrintSuccess("Puck by Slon");

            menu = MenuExtensions.GetMenu();
            me = Variables.me;

            menu.AddItem(new MenuItem("enabled", "Enabled").SetValue(true));
            menu.AddItem(new MenuItem("2001", "Комбо").SetValue(new KeyBind('A', KeyBindType.Press)));
            menu.AddItem(new MenuItem("2002", "Ескейп-комбо").SetValue(new KeyBind('F', KeyBindType.Press)));

            var listofuse_skills1 = new Dictionary<string, bool>
            {
                {"item_blink", true},
                {"item_dagon", true},
                {"item_cyclone", true},
                {"item_ethereal_blade", true},
                {"item_shivas_guard", true},
                {"item_orchid", true},
                {"item_sheepstick", true}
            };

            var listofuse_abylities = new Dictionary<string, bool>
            {
                {"puck_dream_coil", true},
                {"puck_ethereal_jaunt", true},
                {"puck_phase_shift", true},
                {"puck_waning_rift", true},
                {"puck_illusory_orb", true}
            };

            menu.AddItem(
                new MenuItem("enabledAbilities", "Abilities:").SetValue(new AbilityToggler(listofuse_abylities)));

            menu.AddItem(
                new MenuItem("enabledItems", "Items:").SetValue(new AbilityToggler(listofuse_skills1)));
        }

        public override void OnUpdateEvent(EventArgs args)
        {
            target = me.ClosestToMouseTarget(1000);


            if (!Game.IsInGame || Game.IsPaused || Game.IsWatchingGame || Game.IsChatOpen)
                return;

            if (me == null || target == null)
                return;
            // skills
            if (Q == null)
                Q = me.Spellbook.SpellQ;
            if (W == null)
                W = me.Spellbook.SpellW;
            if (E == null)
                E = me.Spellbook.SpellE;
            if (D == null)
                D = me.Spellbook.SpellD;
            if (R == null)
                R = me.Spellbook.SpellR;
            // itens
            Blink = me.FindItem("item_blink");
            dagon = me.Inventory.Items.FirstOrDefault(x => x.Name.Contains("item_dagon"));
            ethereal = me.FindItem("item_ethereal_blade");
            euls = me.FindItem("item_cyclone");
            shivas = me.FindItem("item_shivas_guard");
            malevo = me.FindItem("item_orchid");
            forcestaff = me.FindItem("item_force_staff");
            Hex = me.FindItem("item_sheepstick");

            //Starting Combo
            var blinkposition = (target.Position);
            double TravelTime = QPosition.Distance2D(me.Position)/3000;


            var target2 =
                ObjectManager.GetEntities<Hero>()
                    .Where(
                        hero =>
                            hero.IsAlive && hero.Player.Hero != target && !hero.IsIllusion && hero.IsVisible &&
                            hero.Team != me.Team)
                    .ToList();
            var IsLinkensProtected = (target.Modifiers.Any(x => x.Name == "modifier_item_sphere_target") ||
                                      (target.FindItem("item_sphere") != null &&
                                       (target.FindItem("item_sphere").Cooldown <= 0)));
            var _Is_in_Advantage = (target.Modifiers.Any(x => x.Name == "modifier_item_blade_mail_reflect") ||
                                    target.Modifiers.Any(x => x.Name == "modifier_item_lotus_orb_active") ||
                                    target.Modifiers.Any(x => x.Name == "modifier_nyx_assassin_spiked_carapace") ||
                                    target.Modifiers.Any(x => x.Name == "modifier_templar_assassin_refraction_damage") ||
                                    target.Modifiers.Any(x => x.Name == "modifier_ursa_enrage") ||
                                    target.Modifiers.Any(x => x.Name == "modifier_abaddon_borrowed_time") ||
                                    (target.Modifiers.Any(x => x.Name == "modifier_dazzle_shallow_grave")));
            if ((Game.IsKeyDown(menu.Item("2001").GetValue<KeyBind>().Key) && me.Distance2D(target) <= 1500 &&
                 target.IsVisible && target.IsAlive && !target.IsMagicImmune() && !target.IsIllusion && target != null &&
                 !_Is_in_Advantage) && Utils.SleepCheck("combo"))
            {
                if (me.CanCast() && !me.IsChanneling())
                {
                    if (IsLinkensProtected)
                    {
                        if (euls != null && euls.Cooldown <= 0 && IsLinkensProtected &&
                            menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(euls.Name))
                        {
                            euls.UseAbility(target);
                            Utils.ChainStun(me, euls.GetCastDelay(me, target, true, true), null, false);
                        }
                        else if (forcestaff != null && forcestaff.Cooldown <= 0 && IsLinkensProtected &&
                                 menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(forcestaff.Name))
                        {
                            forcestaff.UseAbility(target);
                            Utils.ChainStun(me, forcestaff.GetCastDelay(me, target, true, true), null, false);
                        }
                        else if (dagon != null && dagon.Cooldown <= 0 &&
                                 menu.Item("Items").GetValue<AbilityToggler>().IsEnabled("item_dagon"))
                        {
                            dagon.UseAbility(target);
                            Utils.ChainStun(me, dagon.GetCastDelay(me, target, true, true), null, false);
                        }
                        else if (ethereal != null && ethereal.Cooldown <= 0 &&
                                 menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(ethereal.Name))
                        {
                            ethereal.UseAbility(target);
                            Utils.ChainStun(me, ethereal.GetCastDelay(me, target, true, true), null, false);
                        }
                    }
                    if (!IsLinkensProtected)
                    {
                        if (Blink != null && Blink.Cooldown <= 0 && me.Distance2D(blinkposition) > 300 &&
                            menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(Blink.Name) && Utils.SleepCheck("b"))
                        {
                            Blink.UseAbility(blinkposition);
                            Utils.Sleep(300, "b");
                        }

                        if (malevo != null && malevo.Cooldown <= 0 &&
                            menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(malevo.Name))
                        {
                            malevo.UseAbility(target);
                            Utils.ChainStun(me, malevo.GetCastDelay(me, target, true, true), null, false);
                        }
                        if (Hex != null && Hex.Cooldown <= 0 &&
                            menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(Hex.Name))
                        {
                            Hex.UseAbility(target);
                            Utils.ChainStun(me, Hex.GetCastDelay(me, target, true, true), null, false);
                        }
                        if (ethereal != null && ethereal.Cooldown <= 0 &&
                            menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(ethereal.Name))
                        {
                            ethereal.UseAbility(target);
                            Utils.ChainStun(me, ethereal.GetCastDelay(me, target, true, true), null, false);
                            ethereal_used = true;
                            ethereal_used2 = true;
                        }
                        else
                        {
                            ethereal_used = false;
                            ethereal_used2 = false;
                        }

                        if (Q.Level > 0 && Q.Cooldown == 0 && me.Distance2D(target) < Q.CastRange &&
                            menu.Item("Abilities: ").GetValue<AbilityToggler>().IsEnabled(Q.Name))
                        {
                            if (ethereal_used2 &&
                                target.Modifiers.Any(x => x.Name == "modifier_item_ethereal_blade_ethereal"))
                            {
                                Q.CastSkillShot(target);
                                Utils.ChainStun(me, Q.GetCastDelay(me, target, true, true), null, false);

                                if (!Q.CanBeCasted())
                                    ethereal_used2 = false;
                            }
                            else if (!ethereal_used2)
                            {
                                Q.CastSkillShot(target);
                                ;
                                Utils.ChainStun(me, Q.GetCastDelay(me, target, true, true), null, false);
                            }
                        }
                        if (W.Level > 0 && W.Cooldown == 0 && me.Distance2D(target) < 350 &&
                            menu.Item("Abilities: ").GetValue<AbilityToggler>().IsEnabled(W.Name))
                        {
                            if (ethereal_used2 &&
                                target.Modifiers.Any(x => x.Name == "modifier_item_ethereal_blade_ethereal"))
                            {
                                W.UseAbility();
                                Utils.ChainStun(me, W.GetCastDelay(me, target, true, true), null, false);
                                if (!W.CanBeCasted())
                                    ethereal_used2 = false;
                            }
                            else if (!ethereal_used2)
                            {
                                W.UseAbility();
                                Utils.ChainStun(me, W.GetCastDelay(me, target, true, true), null, false);
                            }
                        }
                        foreach (var t in target2)
                        {
                            if (target.Distance2D(t.Player.Hero) < 350 && target.Distance2D(t.Player.Hero) > 10 &&
                                R.Level > 0 && R.Cooldown == 0 &&
                                menu.Item("Abilities: ").GetValue<AbilityToggler>().IsEnabled(R.Name))
                            {
                                R.UseAbility(target.Position);
                                Utils.ChainStun(me, R.GetCastDelay(me, target, true, true), null, false);
                            }
                        }

                        if (dagon != null && dagon.Cooldown <= 0 &&
                            menu.Item("Items").GetValue<AbilityToggler>().IsEnabled("item_dagon"))
                        {
                            if (ethereal_used &&
                                target.Modifiers.Any(x => x.Name == "modifier_item_ethereal_blade_ethereal"))
                            {
                                dagon.UseAbility(target);
                                Utils.ChainStun(me, dagon.GetCastDelay(me, target, true, true), null, false);
                                if (!dagon.CanBeCasted())
                                    ethereal_used = false;
                            }
                            else if (!ethereal_used)
                            {
                                dagon.UseAbility(target);
                                Utils.ChainStun(me, dagon.GetCastDelay(me, target, true, true), null, false);
                            }
                        }
                        if (shivas != null && shivas.Cooldown <= 0 &&
                            menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(shivas.Name))
                        {
                            shivas.UseAbility();
                            Utils.ChainStun(me, shivas.GetCastDelay(me, target, true, true), null, false);
                        }
                        Utils.Sleep(200, "combo");
                        if ((shivas == null || shivas.Cooldown > 0) && (dagon == null || dagon.Cooldown > 0) &&
                            (Q == null || Q.Cooldown > 0) && (shivas == null || shivas.Cooldown > 0) &&
                            (ethereal == null || ethereal.Cooldown > 0) && (R == null || R.Cooldown > 0) &&
                            (malevo == null || malevo.Cooldown > 0))
                            stage = 0;
                    }
                }
            }
            //Escape-combo
            if (Game.IsKeyDown(menu.Item("2002").GetValue<KeyBind>().Key) && me.Distance2D(target) <= 1000 &&
                target.IsVisible && target.IsAlive && !target.IsMagicImmune() && !target.IsIllusion && target != null &&
                Utils.SleepCheck("combo2"))
            {
                if (me.CanCast() && !me.IsChanneling())
                {
                    if (IsLinkensProtected)
                    {
                        if (Q.Level > 0 && Q.Cooldown == 0 && me.Distance2D(target) < Q.CastRange &&
                            menu.Item("Abilities: ").GetValue<AbilityToggler>().IsEnabled(Q.Name))
                        {
                            var X = me.Position.X;
                            var Y = me.Position.Y;
                            var Pos = new Vector3(X, Y, me.Position.Z);

                            if (me.Position.X < 0)
                            {
                                X = me.Position.X + 100;
                            }
                            else
                            {
                                X = me.Position.X - 100;
                            }
                            if (me.Position.Y < 0)
                            {
                                Y = me.Position.Y + 100;
                            }
                            else
                            {
                                Y = me.Position.Y - 100;
                            }


                            Q.UseAbility(Pos);
                            Utils.ChainStun(me, Q.GetCastDelay(me, target, true, true), null, false);
                        }

                        if (Blink != null && Blink.Cooldown <= 0 && me.Distance2D(blinkposition) > 300 &&
                            menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(Blink.Name) &&
                            Utils.SleepCheck("b2"))
                        {
                            Blink.UseAbility(blinkposition);
                            Utils.Sleep(300, "b2");
                        }
                        if (euls != null && euls.Cooldown <= 0 && IsLinkensProtected &&
                            menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(euls.Name))
                        {
                            euls.UseAbility(target);
                            Utils.ChainStun(me, euls.GetCastDelay(me, target, true, true), null, false);
                        }
                        else if (forcestaff != null && forcestaff.Cooldown <= 0 && IsLinkensProtected &&
                                 menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(forcestaff.Name))
                        {
                            forcestaff.UseAbility(target);
                            Utils.ChainStun(me, forcestaff.GetCastDelay(me, target, true, true), null, false);
                        }
                        else if (dagon != null && dagon.Cooldown <= 0 &&
                                 menu.Item("Items").GetValue<AbilityToggler>().IsEnabled("item_dagon"))
                        {
                            dagon.UseAbility(target);
                            Utils.ChainStun(me, dagon.GetCastDelay(me, target, true, true), null, false);
                        }
                        else if (ethereal != null && ethereal.Cooldown <= 0 &&
                                 menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(ethereal.Name))
                        {
                            ethereal.UseAbility(target);
                            Utils.ChainStun(me, ethereal.GetCastDelay(me, target, true, true), null, false);
                        }
                    }
                    if (!IsLinkensProtected)
                    {
                        if (Q.Level > 0 && Q.Cooldown == 0 && me.Distance2D(target) < Q.CastRange &&
                            menu.Item("Abilities: ").GetValue<AbilityToggler>().IsEnabled(Q.Name))
                        {
                            var X = me.Position.X;
                            var Y = me.Position.Y;
                            var Pos = new Vector3(X, Y, me.Position.Z);

                            if (me.Position.X < 0)
                            {
                                X = me.Position.X + 100;
                            }
                            else
                            {
                                X = me.Position.X - 100;
                            }
                            if (me.Position.Y < 0)
                            {
                                Y = me.Position.Y + 100;
                            }
                            else
                            {
                                Y = me.Position.Y - 100;
                            }


                            Q.UseAbility(Pos);
                            Utils.ChainStun(me, Q.GetCastDelay(me, target, true, true), null, false);
                        }

                        if (Blink != null && Blink.Cooldown <= 0 && me.Distance2D(blinkposition) > 300 &&
                            menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(Blink.Name))
                        {
                            Blink.UseAbility(blinkposition);
                            Utils.ChainStun(me, Blink.GetCastDelay(me, target, true, true), null, false);
                        }
                        if (malevo != null && malevo.Cooldown <= 0 &&
                            menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(malevo.Name))
                        {
                            malevo.UseAbility(target);
                            Utils.ChainStun(me, malevo.GetCastDelay(me, target, true, true), null, false);
                        }
                        if (Hex != null && Hex.Cooldown <= 0 &&
                            menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(Hex.Name))
                        {
                            Hex.UseAbility(target);
                            Utils.ChainStun(me, Hex.GetCastDelay(me, target, true, true), null, false);
                        }
                        if (ethereal != null && ethereal.Cooldown <= 0 &&
                            menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(ethereal.Name))
                        {
                            ethereal.UseAbility(target);
                            Utils.ChainStun(me, ethereal.GetCastDelay(me, target, true, true), null, false);
                            ethereal_used = true;
                            ethereal_used2 = true;
                        }
                        else
                        {
                            ethereal_used = false;
                            ethereal_used2 = false;
                        }
                    }
                    if (W.Level > 0 && W.Cooldown == 0 && me.Distance2D(target) < 350 &&
                        menu.Item("Abilities: ").GetValue<AbilityToggler>().IsEnabled(W.Name))
                    {
                        if (ethereal_used2 &&
                            target.Modifiers.Any(x => x.Name == "modifier_item_ethereal_blade_ethereal"))
                        {
                            W.UseAbility();
                            Utils.ChainStun(me, W.GetCastDelay(me, target, true, true), null, false);
                            if (!W.CanBeCasted())
                                ethereal_used2 = false;
                        }
                        else if (!ethereal_used2)
                        {
                            W.UseAbility();
                            Utils.ChainStun(me, W.GetCastDelay(me, target, true, true), null, false);
                        }
                    }


                    if (dagon != null && dagon.Cooldown <= 0 &&
                        menu.Item("Items").GetValue<AbilityToggler>().IsEnabled("item_dagon"))
                    {
                        if (ethereal_used &&
                            target.Modifiers.Any(x => x.Name == "modifier_item_ethereal_blade_ethereal"))
                        {
                            dagon.UseAbility(target);
                            Utils.ChainStun(me, dagon.GetCastDelay(me, target, true, true), null, false);
                            if (!dagon.CanBeCasted())
                                ethereal_used = false;
                        }
                        else if (!ethereal_used)
                        {
                            dagon.UseAbility(target);
                            Utils.ChainStun(me, dagon.GetCastDelay(me, target, true, true), null, false);
                        }
                    }
                    if (shivas != null && shivas.Cooldown <= 0 &&
                        menu.Item("Items").GetValue<AbilityToggler>().IsEnabled(shivas.Name))
                    {
                        shivas.UseAbility();
                        Utils.ChainStun(me, shivas.GetCastDelay(me, target, true, true), null, false);
                    }

                    if ((shivas == null || shivas.Cooldown > 0) && (dagon == null || dagon.Cooldown > 0) &&
                        (Q == null || Q.Cooldown > 0) && (shivas == null || shivas.Cooldown > 0) &&
                        (ethereal == null || ethereal.Cooldown > 0) && (R == null || R.Cooldown > 0) &&
                        (malevo == null || malevo.Cooldown > 0))
                    {
                        stage = 0;
                    }


                    if (E.CanBeCasted() && Utils.SleepCheck("1") && (Hex == null || Hex.Cooldown > 0) &&
                        (shivas == null || shivas.Cooldown > 0) && (dagon == null || dagon.Cooldown > 0) &&
                        (ethereal == null || ethereal.Cooldown > 0) && (malevo == null || malevo.Cooldown > 0) &&
                        (Blink == null || Blink.Cooldown > 0))
                    {
                        E.UseAbility();
                        var delay = Task.Delay(720).ContinueWith(_ =>
                        {
                            if (D.CanBeCasted() && Utils.SleepCheck("12"))
                            {
                                D.UseAbility();
                                Utils.Sleep(200, "12");
                            }
                        });
                        Utils.Sleep(2900, "1");
                    }
                    Utils.Sleep(200, "combo2");
                }
            }
        }
    }
}