namespace DotAllCombo.Heroes
{
	using System;
	using Ensage;
	using Ensage.Common;
	using Ensage.Common.Extensions;
	using Extensions;
	using System.Linq;
	using System.Timers;
	internal class HeroController
    {
        public void Init()
        {
			
            if (!Variables.IsHookedHero /*&& Activation.IsActivated()*/ && Variables.InGame)
            {
				Variables.IsHookedHero = true;

                Game.OnIngameUpdate += OnUpdateEvent;
				if (Variables.DeveloperMode)
                    DebugExtensions.Print.Success("[" + ToString() + "] Hooked!");

                DebugExtensions.Chat.PrintSuccess("[" + ToString() + "] Initialized!");
                if (!AssemblyExtensions.IsInitialized() /* && Activation.IsActivated()*/)
                {
                    OnInject();
					DebugExtensions.Print.Success("[DotAllCombo.Activation] SUCCESSFUL ACTIVATION.");
                }
                else
                    ErrorExtensions.ThrowError(ErrorExtensions.Error.ASSEMBLY_INITIALIZED_ALREADY);

                Variables.Settings.User = ObjectManager.LocalPlayer.Name;

                try
                {
                    if (AssemblyExtensions.IsInitialized())
                        MenuExtensions.RegisterMenu(Variables.HeroName);
                    else
                        ErrorExtensions.ThrowError(ErrorExtensions.Error.ASSEMBLY_NOT_INITIALIZED);
                }
                catch
                {
                    ErrorExtensions.ThrowError(ErrorExtensions.Error.MENU_REGISTER_ERROR);
                }
            }

            if (Type.GetType(ToString()).GetMethod("OnUpdateEvent") == null)
            {
                ErrorExtensions.ThrowError(ErrorExtensions.Error.NOT_FOUNDED_HERO_ON_UPDATE, true);
            }
        }
		
		public virtual void OnInject()
		{
			// On inject
		}

		public virtual void OnUpdateEvent(EventArgs args)
        {
            // On update
        }

		public static bool HasStun(Hero x)
        {
            var me = Variables.me;
            if (x.FindSpell("dragon_knight_dragon_tail") != null &&
                x.FindSpell("dragon_knight_dragon_tail").Cooldown <= 0 &&
                me.Distance2D(x) <= x.FindSpell("dragon_knight_dragon_tail").CastRange
                ||
                x.FindSpell("earthshaker_echo_slam") != null && x.FindSpell("earthshaker_echo_slam").Cooldown <= 0 &&
                me.Distance2D(x) <= x.FindSpell("earthshaker_echo_slam").CastRange
                ||
                x.FindSpell("legion_commander_duel") != null && x.FindSpell("legion_commander_duel").Cooldown <= 0 &&
                me.Distance2D(x) <= x.FindSpell("legion_commander_duel").CastRange
                ||
                x.FindSpell("leshrac_split_earth") != null && x.FindSpell("leshrac_split_earth").Cooldown <= 0 &&
                me.Distance2D(x) <= x.FindSpell("leshrac_split_earth").CastRange
                ||
                x.FindSpell("leoric_hellfire_blast") != null && x.FindSpell("leoric_hellfire_blast").Cooldown <= 0 &&
                me.Distance2D(x) <= x.FindSpell("leoric_hellfire_blast").CastRange
                ||
                x.FindSpell("lina_light_strike_array") != null && x.FindSpell("lina_light_strike_array").Cooldown <= 0 &&
                me.Distance2D(x) <= x.FindSpell("lina_light_strike_array").CastRange
                ||
                x.FindSpell("lion_impale") != null && x.FindSpell("lion_impale").Cooldown <= 0 &&
                me.Distance2D(x) <= x.FindSpell("lion_impale").CastRange
                ||
                x.FindSpell("magnataur_reverse_polarity") != null &&
                x.FindSpell("magnataur_reverse_polarity").Cooldown <= 0 &&
                me.Distance2D(x) <= x.FindSpell("magnataur_reverse_polarity").CastRange
                ||
                x.FindSpell("nyx_assassin_impale") != null && x.FindSpell("nyx_assassin_impale").Cooldown <= 0 &&
                me.Distance2D(x) <= x.FindSpell("nyx_assassin_impale").CastRange
                ||
                x.FindSpell("ogre_magi_fireblast") != null && x.FindSpell("ogre_magi_fireblast").Cooldown <= 0 &&
                me.Distance2D(x) <= x.FindSpell("ogre_magi_fireblast").CastRange
                ||
                x.FindSpell("skeleton_king_hellfire_blast") != null &&
                x.FindSpell("skeleton_king_hellfire_blast").Cooldown <= 0 &&
                me.Distance2D(x) <= x.FindSpell("skeleton_king_hellfire_blast").CastRange
                ||
                x.FindSpell("sven_storm_bolt") != null && x.FindSpell("sven_storm_bolt").Cooldown <= 0 &&
                me.Distance2D(x) <= x.FindSpell("sven_storm_bolt").CastRange
                ||
                x.FindSpell("tiny_avalanche") != null && x.FindSpell("tiny_avalanche").Cooldown <= 0 &&
                me.Distance2D(x) <= x.FindSpell("tiny_avalanche").CastRange
                ||
                x.FindSpell("tusk_walrus_punch") != null && x.FindSpell("tusk_walrus_punch").Cooldown <= 0 &&
                me.Distance2D(x) <= x.FindSpell("tusk_walrus_punch").CastRange
                ||
                x.FindSpell("vengefulspirit_magic_missile") != null &&
                x.FindSpell("vengefulspirit_magic_missile").Cooldown <= 0 &&
                me.Distance2D(x) <= x.FindSpell("vengefulspirit_magic_missile").CastRange
                ||
                x.FindSpell("windrunner_shackleshot") != null && x.FindSpell("windrunner_shackleshot").Cooldown <= 0 &&
                me.Distance2D(x) <= x.FindSpell("windrunner_shackleshot").CastRange
                )
                return true;
            return false;
        }
		public static bool invUnit(Hero z)
		{
			var me = Variables.me;
			if (z.Modifiers.Any(
			   x =>
				   (x.Name == "modifier_bounty_hunter_wind_walk" ||
					x.Name == "modifier_riki_permanent_invisibility" ||
					x.Name == "modifier_mirana_moonlight_shadow" || x.Name == "modifier_treant_natures_guise" ||
					x.Name == "modifier_weaver_shukuchi" ||
					x.Name == "modifier_broodmother_spin_web_invisible_applier" ||
					x.Name == "modifier_item_invisibility_edge_windwalk" || x.Name == "modifier_rune_invis" ||
					x.Name == "modifier_clinkz_wind_walk" || x.Name == "modifier_item_shadow_amulet_fade" ||
					x.Name == "modifier_item_silver_edge_windwalk" ||
					x.Name == "modifier_item_edge_windwalk" ||
					x.Name == "modifier_nyx_assassin_vendetta" ||
					x.Name == "modifier_invisible" ||
					x.Name == "modifier_invoker_ghost_walk_enemy")))
				return true;
			return false;
		}
	}
}