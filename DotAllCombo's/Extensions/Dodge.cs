#pragma warning disable CS0642
namespace DotAllCombo.Extensions
{
	using System.Reflection;
	using Ensage;
	using Ensage.Common;
	using Ensage.Common.Extensions;
	using Ensage.Common.Menu;
	using SharpDX;
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using SharpDX.Direct3D9;
	using System.Windows.Input;


	public class Dodge
	{
		public static Ability Q, W, E, R, F, D;
		public static Hero me;
		public static Font txt;
		public static Font not;
		private static readonly Menu skills = new Menu("AutoDodgeSpells", "Auto Dodge All Spell's");
		public static Menu menu;
		public static void DodgeInject()
		{
			menu = Variables.MainMenu;
			Game.OnUpdate += Game_OnUpdate;
			Console.WriteLine("> DisableAllHero # loaded!");

			txt = new Font(
				Drawing.Direct3DDevice9,
				new FontDescription
				{
					FaceName = "Tahoma",
					Height = 12,
					OutputPrecision = FontPrecision.Default,
					Quality = FontQuality.Default
				});

			not = new Font(
				Drawing.Direct3DDevice9,
				new FontDescription
				{
					FaceName = "Tahoma",
					Height = 20,
					OutputPrecision = FontPrecision.Default,
					Quality = FontQuality.Default
				});

			Drawing.OnPreReset += Drawing_OnPreReset;
			Drawing.OnPostReset += Drawing_OnPostReset;
			Drawing.OnEndScene += Drawing_OnEndScene;
			AppDomain.CurrentDomain.DomainUnload += CurrentDomain_DomainUnload;
			skills.AddItem(new MenuItem("dodge", "Auto Dodge Spell's").SetValue(true));
			menu.AddSubMenu(skills);
		}

		static double GetDistance2D(Vector3 A, Vector3 B)
		{
			return Math.Sqrt(Math.Pow(A.X - B.X, 2) + Math.Pow(A.Y - B.Y, 2));
		}
		public static void Game_OnUpdate(EventArgs args)
		{
			me = ObjectManager.LocalHero;



			List<Hero> enemy = ObjectManager.GetEntities<Hero>().Where(x => x.IsAlive && x.Team != me.Team && !x.IsIllusion).ToList();

			if (!Game.IsInGame || Game.IsWatchingGame)
			{
				return;
			}
			if (!me.IsAlive)
				return;
			if (menu.Item("dodge").IsActive() && me.IsAlive && (!me.IsInvisible() || me.IsVisibleToEnemies))
			{
				foreach (var e in enemy)
				{
					//spell
					Q = e.Spellbook.SpellQ;

					W = e.Spellbook.SpellW;

					E = e.Spellbook.SpellE;

					R = e.Spellbook.SpellR;

					D = e.Spellbook.SpellD;

					F = e.Spellbook.SpellF;

					float distance = me.Distance2D(e);

					if (e == null)
						return;

					if (!e.IsIllusion && e.IsVisible)
					{
						if (e.ClassID == ClassID.CDOTA_Unit_Hero_AntiMage)
						{
							if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qquseSilence(e)) ;
									else
									if (qqallHex(e)) ;
									else
									if (qquseOrhid(e)) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qqTemplarMeld()) ;
									else
									if (qquseBlinkHome()) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qqskySilence(e)) ;
									qquseLotus();
									goto leave;
								}

								else if (distance < 300)
								{
									if (qqaxeCall()) ;
									else
									if (qqpuckW()) ;
									else
										goto leave;

								}
							}
							if (e.NetworkActivity == NetworkActivity.Attack)
							{
								if (distance < 200)
								{
									if (qquseManta()) ;
									else
									if (qquseHelbard(e)) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseColba(e)) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqpuckW()) ;
									else
									if (qqPLDoppleganger()) ;
									else
									if (qqNyx()) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqlegionPress()) ;
									else
									if (qqjugerOmniTarget(e)) ;
									else
									if (qqemberhains()) ;
									else
									if (qqClinkzWindwalk()) ;
									else
									if (qqBountyhunterWindwalk()) ;
									else
									if (qqaxeCall()) ;
									else
									if (qqabadonWme()) ;
									else

								 if (me.Mana >= (me.MaximumMana * 0.7) || (me.MaximumMana <= 400))
									{
										if (qquseGhost()) ;
										else
											goto leave;
									}
									goto leave;
								}
							}
							if (W.IsInAbilityPhase)
							{
								if (distance < 500)
								{
									if (qquseOrhid(e)) ;
									else
									if (qqallHex(e)) ;
									else
									if (qquseSilence(e)) ;
									else
									if (qquseSheep(e)) ;
									else
									if (qquseEulEnem(e)) ;
									else
										goto leave;
								}
							}
						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Abaddon)
						{
							if (W.IsInAbilityPhase)
							{
								if (distance < 400)
								{
									if (qqDamageNull()) ;
									else
									if (qqusePipe()) ;
									else
									if (qqpuckW()) ;
									else
										goto leave;
								}

							}
							if (e.NetworkActivity == NetworkActivity.Attack)
							{
								if (distance < 200)
								{
									if (qquseManta()) ;
									else
									if (qquseHelbard(e)) ;
									else
									if (qquseGhost()) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseColba(e)) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqpuckW()) ;
									else
									if (qqPLDoppleganger()) ;
									else
									if (qqNyx()) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqlegionPress()) ;
									else
									if (qqjugerOmniTarget(e)) ;
									else
									if (qqemberhains()) ;
									else
									if (qqClinkzWindwalk()) ;
									else
									if (qqBountyhunterWindwalk()) ;
									else
									if (qqaxeCall()) ;
									else
									if (qqabadonWme()) ;
									else
										goto leave;
								}
							}
						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Axe)
						{
							if (Q.IsInAbilityPhase)
							{
								if (distance < 400)
								{
									if (qqPuckShift()) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqslarkPounce()) ;
									else
									if (qqslarkDarkPact()) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qqTemplarMeld()) ;
									else
									if (qquseAbyssal(e)) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qquseCrimson()) ;
									else
									if (qquseOrhid(e)) ;
									else
									if (qquseStuffTarget(e)) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qqpuckW()) ;
									else
									if (qqallHex(e)) ;
									else
									if (qqNyx()) ;
									else
										goto leave;

								}
							}
							if (R.IsInAbilityPhase)
							{
								if (distance <= 700)
								{

									if (qquseStuffTarget(e)) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseCrimson()) ;
									else
									if (qquseOrhid(e)) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqallHex(e)) ;
								}
								else if (distance < 400)
								{
									if (qqallHex(e)) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqslarkPounce()) ;
									else
									if (qqslarkDarkPact()) ;
									else
									if (qqTemplarMeld()) ;
									else
									if (qquseAbyssal(e)) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qqpuckW()) ;
									else
									if (qqNyx()) ;
									else
										goto leave;
								}
							}

						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Bane)
						{
							if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									qquseLotus();
									if (qqskySilence(e)) ;
									else
									if (qquseSDisription(me)) ;
									else
									if (qqSandkinSandstorm()) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqNyx()) ;
									else
									if (qqClinkzWindwalk()) ;
									else
									if (qqallHex(e)) ;
									else
									if (qqabadonWme()) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqslarkDarkPact()) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qquseBlinkHome()) ;
									else
									if (qquseOrhid(e)) ;

								}
								else if (distance < 350)
								{
									if (qquseStuffTarget(e)) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseColba(e)) ;
									else
									if (qqTemplarMeld()) ;
									else
									if (qqsilencerLastWord(e)) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqpuckW()) ;
									else
									if (qqNyx()) ;
									else
									if (qqGoInvis()) ;
									else
									if (qqemberE()) ;
									else
										goto leave;
								}
							}


						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Batrider)
						{
							if (R.IsInAbilityPhase)
							{
								if (distance <= 700)
								{
									if (qqemberE()) ;
									else
									if (qqallHex(e)) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qquseColba(e)) ;
									else
									if (qquseOrhid(e)) ;
								}
								else if (distance < 500)
								{
									if (qquseStuffTarget(e)) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qqEmberWTarget(e)) ;
									else
									if (qqClinkzWindwalk()) ;
								}
								else if (distance < 350)
								{
									if (qqemberhains()) ;
									else
									if (qqJuggernautfury()) ;
									else
									if (qqNyx()) ;
									else
									if (qqpuckW()) ;
									else
									if (qquseSDisription(me)) ;
									else
									if (qqSandkinSandstorm()) ;
									else
									if (qqslardarCrush()) ;
									else
									if (qqslarkDarkPact()) ;
									else
									if (qqStormUltMe()) ;
									else
									if (qqTemplarMeld()) ;
									else
									if (qqtuskSnowBallTarget(e)) ;
									else
									if (qquseAbyssal(e)) ;
									else
									if (qquseColba(e)) ;
									else
									if (qquseEulEnem(e)) ;
									qquseLotus();
									if (qqusePipe()) ;
									else
									if (qquseShiva()) ;
									else
									if (qqWeaverShukuchi()) ;
								}

								goto leave;
							}
						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Beastmaster)
						{
							if (R.IsInAbilityPhase)
							{
								if (distance < 500)
								{
									if (qqNyx()) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqGoInvis()) ;
									else
									if (qqemberE()) ;
									else
									if (qqblinkHomeHero()) ;
									qquseLotus();
									if (qquseOrhid(e)) ;
									else
									if (qqTemplarRefraction()) ;
								}
								goto leave;
							}
						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Bloodseeker)
						{
							if (W.IsInAbilityPhase)
							{
								if (distance < 500)
								{
									if (qqTemplarRefraction()) ;
									else
									if (qqabadonWme()) ;
									else
									if (qquseEulEnem(e)) ;
								}
							}
							if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qquseSB()) ;
									else
									if (qquseOrhid(e)) ;
									else
									if (qquseColba(e)) ;
									else
									if (qquseAmulet()) ;
									else
									if (qqsilencerLastWord(e)) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqGoInvis()) ;
								}
								if (distance < 300)
								{
									if (qqemberhains()) ;
									else
									if (qqaxeCall()) ;
									else
									if (qqAlchemistRage()) ;
									else
									if (qqabadonWme()) ;
									else
									if (qqallHex(e)) ;
									else
									if (qqNyx()) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqWeaverShukuchi()) ;
								}
							}
							if (e.NetworkActivity == NetworkActivity.Attack)
							{
								if (distance < 200)
								{
									if (qquseManta()) ;
									else
									if (qquseHelbard(e)) ;
									else
									if (qquseGhost()) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseColba(e)) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqpuckW()) ;
									else
									if (qqPLDoppleganger()) ;
									else
									if (qqNyx()) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqlegionPress()) ;
									else
									if (qqjugerOmniTarget(e)) ;
									else
									if (qqemberhains()) ;
									else
									if (qqClinkzWindwalk()) ;
									else
									if (qqBountyhunterWindwalk()) ;
									else
									if (qqaxeCall()) ;
									else
									if (qqabadonWme()) ;
									else
										goto leave;
								}
							}


						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_BountyHunter)
						{
							if (Q.IsInAbilityPhase)
							{
								if (distance < 600 )
								{
									if (qqWeaverShukuchi()) ;
									else
									if (qquseManta()) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseColba(e)) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqNyx()) ;
									else
									if (qqJuggernautfury()) ;
									else
									if (qqemberE()) ;
									else
									if (qqabadonWme()) ;
									else
										goto leave;
								}
							}
						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Brewmaster)
						{
							if (Q.IsInAbilityPhase)
							{
								if (distance < 350)
								{
									if (qqaxeCall()) ;
									else
									if (qqblinkHomeHero()) ;
									else
									if (qqemberhains()) ;
									else
									if (qqemberE()) ;
									else
									if (qqJuggernautfury()) ;
									else
									if (qqLifestealerrage()) ;
									else
									if (qqNyx()) ;
									else
									if (qqpuckW()) ;
									else
									if (qquseSDisription(me)) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qquseAbyssal(e)) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseOrhid(e)) ;
									else
									if (qquseStuffTarget(e)) ;
									else
										goto leave;
								}

							}
							if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qquseSheep(e)) ;
									else
									if (qquseOrhid(e)) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseColba(e)) ;
									else
									if (qqDoom(e)) ;
									else
									if (qqskySilence(e)) ;
								}
								if (distance < 350)
								{
									if (qqemberE()) ;
									else
									if (qqemberhains()) ;
									else
									if (qqpuckW()) ;
									else
									if (qqaxeCall()) ;
									else
										goto leave;
								}
							}
							if (e.NetworkActivity == NetworkActivity.Attack)
							{
								if (distance < 200)
								{
									if (qquseManta()) ;
									else
									if (qquseHelbard(e)) ;
									else
									if (qquseGhost()) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseColba(e)) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqpuckW()) ;
									else
									if (qqPLDoppleganger()) ;
									else
									if (qqNyx()) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqlegionPress()) ;
									else
									if (qqjugerOmniTarget(e)) ;
									else
									if (qqemberhains()) ;
									else
									if (qqClinkzWindwalk()) ;
									else
									if (qqBountyhunterWindwalk()) ;
									else
									if (qqaxeCall()) ;
									else
									if (qqabadonWme()) ;
									else
										goto leave;
								}
							}

						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Bristleback)
						{
							if (W.IsInAbilityPhase)
							{
								if (distance < 400)
								{
									if (qqabadonWme()) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqNyx()) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qquseBladeMail()) ;
									else
										goto leave;
								}
							}

						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Broodmother)
						{
							if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qquseHelbard(e)) ;
									else
									if (qquseOrhid(e)) ;
									else
									if (qquseShiva()) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqallHex(e)) ;
									else
										goto leave;
								}

							}
							if (e.NetworkActivity == NetworkActivity.Attack)
							{
								if (distance < 200)
								{
									if (qquseManta()) ;
									else
									if (qquseHelbard(e)) ;
									else
									if (qquseGhost()) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseColba(e)) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqpuckW()) ;
									else
									if (qqPLDoppleganger()) ;
									else
									if (qqNyx()) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqlegionPress()) ;
									else
									if (qqjugerOmniTarget(e)) ;
									else
									if (qqemberhains()) ;
									else
									if (qqClinkzWindwalk()) ;
									else
									if (qqBountyhunterWindwalk()) ;
									else
									if (qqaxeCall()) ;
									else
									if (qqabadonWme()) ;
									else
										goto leave;
								}
							}

						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Centaur)
						{
							if (W.IsInAbilityPhase || Q.IsInAbilityPhase)
							{
								if (distance < 300)
								{
									if (qqabadonWme()) ;
									else
									if (qqAlchemistRage()) ;
									else
									if (qqaxeCall()) ;
									else
									if (qqblinkHomeHero()) ;
									else
									if (qqemberE()) ;
									else
									if (qqJuggernautfury()) ;
									else
									if (qqNyx()) ;
									else
									if (qqpuckW()) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqslarkDarkPact()) ;
									else
									if (qquseAbyssal(e)) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qquseColba(e)) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseOrhid(e)) ;
									else
									if (qqusePipe()) ;
									else
									if (qquseStuffTarget(e)) ;
									else
									if (qquseSheep(e)) ;
									else
										goto leave;
								}
							}

						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_ChaosKnight)
						{
							if (Q.IsInAbilityPhase || W.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qqabadonWme()) ;
									else
									if (qqAlchemistRage()) ;
									else
									if (qqallHex(e)) ;
									else
									if (qqblinkHomeHero()) ;
									else
									if (qqBountyhunterWindwalk()) ;
									else
									if (qqClinkzWindwalk()) ;
									else
									if (qqemberE()) ;
									else
									if (qqEmberWTarget(e)) ;
									else
									if (qqGoInvis()) ;
									else
									if (qqJuggernautfury()) ;
									else
									if (qqNyx()) ;
									else
									if (qqodImprisomentMe(me)) ;
									else
									if (qqPLDoppleganger()) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqStormUltMe()) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseHelbard(e)) ;
									else
									if (qquseOrhid(e)) ;
									else
									if (qqWeaverShukuchi()) ;
									else
										goto leave;
								}
								else if (distance < 350)
								{
									if (qquseManta()) ;
									else
									if (qquseAmulet()) ;
									else
									if (qqslarkDarkPact()) ;
									else
									if (qqslardarCrush()) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqpuckW()) ;
									else
									if (qqNyx()) ;
									else
									if (qqaxeCall()) ;
									else
										goto leave;
								}
							}
							if (e.NetworkActivity == NetworkActivity.Attack)
							{
								if (distance < 200)
								{
									if (qquseManta()) ;
									else
									if (qquseHelbard(e)) ;
									else
									if (qquseGhost()) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseColba(e)) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqpuckW()) ;
									else
									if (qqPLDoppleganger()) ;
									else
									if (qqNyx()) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqlegionPress()) ;
									else
									if (qqjugerOmniTarget(e)) ;
									else
									if (qqemberhains()) ;
									else
									if (qqClinkzWindwalk()) ;
									else
									if (qqBountyhunterWindwalk()) ;
									else
									if (qqaxeCall()) ;
									else
									if (qqabadonWme()) ;
									else
										goto leave;
								}
							}

						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Clinkz)
						{
							if (W.IsInAbilityPhase)
							{
								goto leave;
							}

						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Rattletrap)
						{
							if (Q.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qqabadonWme()) ;
									else
									if (qqAlchemistRage()) ;
									else
									if (qqallHex(e)) ;
									else
									if (qqDoom(e)) ;
									else
									if (qqEmberWTarget(e)) ;
									else
									if (qqlegionPress()) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqNyx()) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqSandkinSandstorm()) ;
									else
									if (qqsilencerLastWord(e)) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qquseColba(e)) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseCrimson()) ;
									else
									if (qquseHelbard(e)) ;
									else
									if (qquseSB()) ;
									else
									if (qquseSB()) ;
									else
									if (qquseShiva()) ;
									else
									if (qqGoInvis()) ;
									else
									if (qquseBlinkHome()) ;
									else
										goto leave;
								}
								else if (distance < 300)
								{
									if (qqaxeCall()) ;
									else
									if (qqBountyhunterWindwalk()) ;
									else
									if (qqemberhains()) ;
									else
									if (qqpuckW()) ;
									else
									if (qqTemplarMeld()) ;
									else
									if (qqtuskSnowBallTarget(e)) ;
									else
									if (qquseStuffTarget(e)) ;
									else
										goto leave;
								}
							}
						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_CrystalMaiden)
						{
							if (W.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qqabadonWme()) ;
									else
									if (qqemberE()) ;
									else
									if (qqEmberWTarget(e)) ;
									else
									if (qqJuggernautfury()) ;
									else
									if (qqlegionPress()) ;
									else
									if (qqNyx()) ;
									else
									if (qqomniknightRepelMe(me)) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqsilencerLastWord(e)) ;
									else
									if (qqslardarCrush()) ;
									else
									if (qqslarkDarkPact()) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qquseBlinkHome()) ;
									else
									if (qquseManta()) ;
									else
									if (qquseOrhid(e)) ;
									else
									if (qquseStuffTarget(e)) ;
									else
										goto leave;
								}
							}
							if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qquseSheep(e)) ;
									else
									if (qquseSilence(e)) ;
									else
									if (qqusePipe()) ;
									qquseLotus();
									if (qqallHex(e)) ;
									else
									if (qqDoom(e)) ;
									else
									if (qqEmberWTarget(e)) ;
									else
									if (qqJuggernautfury()) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqNyx()) ;
									else
									if (qqomniknightRepelMe(me)) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqPLDoppleganger()) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqsilencerUlt()) ;
									else
									if (qqLifestealerrage()) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseOrhid(e)) ;
									else
										goto leave;
								}
								else if (distance < 350)
								{
									if (qqaxeCall()) ;
									else
									if (qqemberhains()) ;
									else
									if (qqjugerOmniTarget(e)) ;
									else
									if (qqpuckW()) ;
									else
									if (qqsilencerLastWord(e)) ;
									else
									if (qqslardarCrush()) ;
									else
									if (qquseAbyssal(e)) ;
									else
										goto leave;
								}
							}
						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_DarkSeer)
						{
							if (W.IsInAbilityPhase)
							{
								goto leave;
							}

						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Dazzle)
						{
							if (W.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qqEmberWTarget(e)) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qquseColba(e)) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseOrhid(e)) ;
									else
									if (qquseSheep(e)) ;
									else
									if (qquseSilence(e)) ;
									else
										goto leave;

								}
								else if (distance < 350)
								{
									if (qqaxeCall()) ;
									else
									if (qqpuckW()) ;
									else
										goto leave;
								}
							}

						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_DeathProphet)
						{
							if (W.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qquseStaffMe(me)) ;
									else
									if (qquseSilence(e)) ;
									else
									if (qqabadonWme()) ;
									else
									if (qqClinkzWindwalk()) ;
									else
									if (qqEmberWTarget(e)) ;
									else
									if (qqJuggernautfury()) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqDamageNull()) ;
									else
									if (qquseSDisription(me)) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqslarkDarkPact()) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qquseBlinkHome()) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseSheep(e)) ;
									else
										goto leave;
								}
							}

							if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qqallHex(e)) ;
									else
									if (qquseSilence(e)) ;
									else
									if (qqsilencerUlt()) ;
									else
									if (qqabadonWme()) ;
									else
									if (qqClinkzWindwalk()) ;
									else
									if (qqJuggernautfury()) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqDamageNull()) ;
									else
									if (qquseSDisription(me)) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qquseBlinkHome()) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseSheep(e)) ;
									else
									if (qqDoom(e)) ;
									else
									if (qquseOrhid(e)) ;
									else
										goto leave;
								}
								else if (distance < 350)
								{
									if (qqaxeCall()) ;
									else
									if (qqemberhains()) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqpuckW()) ;
									else
									if (qquseAbyssal(e)) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qquseCrimson()) ;
									else
										goto leave;
								}
							}

						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Disruptor)
						{
							if (W.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qqEmberWTarget(e)) ;
									else
									if (qqJuggernautfury()) ;
									else
									if (qqLoneDruidUlt()) ;
									else
									if (qqNyx()) ;
									else
									if (qqPuckShift()) ;
									else
									if (qquseSDisription(me)) ;
									else
									if (qqslarkDarkPact()) ;
									else
									if (qqStormUltMe()) ;
									else
									if (qquseEulEnem(e)) ;
									else
										goto leave;
								}

							}
							if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qqAlchemistRage()) ;
									else
									if (qqemberE()) ;
									else
									if (qqEmberWTarget(e)) ;
									else
									if (qqJuggernautfury()) ;
									else
									if (qqNyx()) ;
									else
									if (qqPhoenixsupernova()) ;
									else
									if (qqPuckShift()) ;
									else
									if (qquseSDisription(me)) ;
									else
									if (qqsilencerUlt()) ;
									else
									if (qqStormUltTarget(e)) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qqtuskSnowBallTarget(e)) ;
									else
									if (qquseBKB()) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qquseBlinkHome()) ;
									else
									if (qquseEulMe(me)) ;
									else
									if (qqusePipe()) ;
									else
									if (qquseStaffMe(me)) ;
									else
									if (qqLifestealerrage()) ;
									qquseLotus();
									if (qquseOrhid(e)) ;
									else
										goto leave;
								}
								if (distance < 300)
								{
									if (qqabadonWme()) ;
									else
									if (qqaxeCall()) ;
									else
									if (qqemberhains()) ;
									else
									if (qqjugerOmniTarget(e)) ;
									else
									if (qqLifestealerrage()) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqslardarCrush()) ;
									else
									if (qquseAbyssal(e)) ;
									else
									if (qquseEulEnem(e)) ;
									else
										goto leave;
								}
							}

						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_DoomBringer)
						{
							if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qqabadonWme()) ;
									else
									if (qqallHex(e)) ;
									else
									if (qqblinkHomeHero()) ;
									else
									if (qqBountyhunterWindwalk()) ;
									else
									if (qqClinkzWindwalk()) ;
									else
									if (qqEmberWTarget(e)) ;
									else
									if (qqJuggernautfury()) ;
									else
									if (qqLifestealerrage()) ;
									else
									if (qqlegionPress()) ;
									else
									if (qqNyx()) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqPhoenixsupernova()) ;
									else
									if (qqSandkinSandstorm()) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqslarkShadowDance()) ;
									else
									if (qqTemplarMeld()) ;
									else
									if (qqtuskSnowBallTarget(e)) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qquseBlinkHome()) ;
									else
									if (qquseEulEnem(e)) ;
									qquseLotus();
									if (qquseManta()) ;
									else
									if (qquseOrhid(e)) ;
									else
									if (qquseSheep(e)) ;
									else
									if (qquseSB()) ;
									else
									if (qquseSilence(e)) ;
									else
										goto leave;
								}
								if (distance < 300)
								{
									if (qqaxeCall()) ;
									else
									if (qqjugerOmniTarget(e)) ;
									else
									if (qqLifestealerrage()) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqslardarCrush()) ;
									else
									if (qquseAbyssal(e)) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qqWeaverShukuchi()) ;
									else
										goto leave;
								}
							}
							if (e.NetworkActivity == NetworkActivity.Attack)
							{
								if (distance < 200)
								{
									if (qquseManta()) ;
									else
									if (qquseHelbard(e)) ;
									else
									if (qquseGhost()) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseColba(e)) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqpuckW()) ;
									else
									if (qqPLDoppleganger()) ;
									else
									if (qqNyx()) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqlegionPress()) ;
									else
									if (qqjugerOmniTarget(e)) ;
									else
									if (qqemberhains()) ;
									else
									if (qqClinkzWindwalk()) ;
									else
									if (qqBountyhunterWindwalk()) ;
									else
									if (qqaxeCall()) ;
									else
									if (qqabadonWme()) ;
									else
										goto leave;
								}
							}

						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_DragonKnight)
						{
							if (Q.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qquseOrhid(e)) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqPLDoppleganger()) ;
									else
									if (qqNyx()) ;
									else
									if (qqEmberWTarget(e)) ;
									else
									if (qqabadonWme()) ;
									else
										goto leave;
								}
							}
							if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qquseHelbard(e)) ;
									else
									if (qquseCrimson()) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqoracleFateTarget(e)) ;
									else
										goto leave;
								}
							}

						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_DrowRanger)
						{
							if (Q.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qqoracleFateTarget(e)) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qquseHelbard(e)) ;
									else
										goto leave;
								}
							}

						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_EarthSpirit)
						{
							if (W.IsInAbilityPhase)
							{
								if (distance < 350)
								{
									if (qqaxeCall()) ;
									else
									if (qqemberhains()) ;
									else
									if (qqpuckW()) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseOrhid(e)) ;
									else
										goto leave;
								}
							}
							if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qqusePipe()) ;
									else
									if (qquseOrhid(e)) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qqabadonWme()) ;
									else
										goto leave;
								}
							}
							if (Q.IsInAbilityPhase)
							{

								goto leave;
							}

						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Earthshaker)
						{
							if (W.IsInAbilityPhase)
							{
								if (distance < 350)
								{
									if (qqaxeCall()) ;
									else
									if (qqemberE()) ;
									else
									if (qqjugerOmniTarget(e)) ;
									else
									if (qqNyx()) ;
									else
									if (qqtuskSnowBallTarget(e)) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qquseOrhid(e)) ;
									else
									if (qquseStuffTarget(e)) ;
									else
										goto leave;
								}
							}
							if (W.IsInAbilityPhase)
							{
								if (distance < 350)
								{
									if (qqaxeCall()) ;
									else
									if (qqemberE()) ;
									else
									if (qqjugerOmniTarget(e)) ;
									else
									if (qqNyx()) ;
									else
									if (qqtuskSnowBallTarget(e)) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qquseOrhid(e)) ;
									else
									if (qquseStuffTarget(e)) ;
									else
										goto leave;
								}
								if (distance < 700)
								{
									if (qquseOrhid(e)) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseBlinkHome()) ;
									else
									if (qqslarkDarkPact()) ;
									else
									if (qqPLDoppleganger()) ;
									else
									if (qqAlchemistRage()) ;
									else
										goto leave;
								}

							}

						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Elder_Titan)
						{
							if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{

									goto leave;
								}
							}

						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_EmberSpirit)
						{
							if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qqallAltStun(e)) ;
									else
									if (qqallHex(e)) ;
									else
									if (qqAlchemistRage()) ;
									else
									if (qqJuggernautfury()) ;
									else
									if (qqNyx()) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qqtuskSnowBallTarget(e)) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseOrhid(e)) ;
									else
									if (qquseSheep(e)) ;
									else
										goto leave;
								}
							}
							if (e.NetworkActivity == NetworkActivity.Attack)
							{
								if (distance < 200)
								{
									if (qquseManta()) ;
									else
									if (qquseHelbard(e)) ;
									else
									if (qquseGhost()) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseColba(e)) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqpuckW()) ;
									else
									if (qqPLDoppleganger()) ;
									else
									if (qqNyx()) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqlegionPress()) ;
									else
									if (qqjugerOmniTarget(e)) ;
									else
									if (qqemberhains()) ;
									else
									if (qqClinkzWindwalk()) ;
									else
									if (qqBountyhunterWindwalk()) ;
									else
									if (qqaxeCall()) ;
									else
									if (qqabadonWme()) ;
									else
										goto leave;
								}
							}

						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Enchantress)
						{
							if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qquseEulEnem(e)) ;
									else
									if (qquseHelbard(e)) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
										goto leave;
								}
							}


						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Enigma)
						{
							if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qqAlchemistRage()) ;
									else
									if (qqallHex(e)) ;
									else
									if (qqDoom(e)) ;
									else
									if (qqEmberWTarget(e)) ;
									else
									if (qqJuggernautfury()) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qquseEulEnem(e)) ;
									qquseLotus();
									if (qquseOrhid(e)) ;
									else
									if (qquseSheep(e)) ;
									else
										goto leave;
								}
							}

						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_FacelessVoid)
						{
							if (Q.IsInAbilityPhase)
							{
								if (distance < 400)
								{
									if (qqAlchemistRage()) ;
									else
									if (qqallHex(e)) ;
									else
									if (qqblinkHomeHero()) ;
									else
									if (qqDoom(e)) ;
									else
									if (qqEmberWTarget(e)) ;
									else
									if (qqjugerOmniTarget(e)) ;
									else
									if (qqlegionPress()) ;
									else
									if (qqNyx()) ;
									else
									if (qqoracleFateTarget(e)) ;
									else
									if (qqPhoenixsupernova()) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qquseAbyssal(e)) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseHelbard(e)) ;
									else
									if (qquseSheep(e)) ;
									else
										goto leave;
								}

							}
							if (e.NetworkActivity == NetworkActivity.Attack)
							{
								if (distance < 200)
								{
									if (qquseManta()) ;
									else
									if (qquseHelbard(e)) ;
									else
									if (qquseGhost()) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseColba(e)) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqpuckW()) ;
									else
									if (qqPLDoppleganger()) ;
									else
									if (qqNyx()) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqlegionPress()) ;
									else
									if (qqjugerOmniTarget(e)) ;
									else
									if (qqemberhains()) ;
									else
									if (qqClinkzWindwalk()) ;
									else
									if (qqBountyhunterWindwalk()) ;
									else
									if (qqaxeCall()) ;
									else
									if (qqabadonWme()) ;
									else
										goto leave;
								}
							}
							if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qqAlchemistRage()) ;
									else
									if (qqallHex(e)) ;
									else
									if (qqblinkHomeHero()) ;
									else
									if (qqDoom(e)) ;
									else
									if (qqEmberWTarget(e)) ;
									else
									if (qqjugerOmniTarget(e)) ;
									else
									if (qqlegionPress()) ;
									else
									if (qqNyx()) ;
									else
									if (qqoracleFateTarget(e)) ;
									else
									if (qqPhoenixsupernova()) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qquseAbyssal(e)) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseHelbard(e)) ;
									else
									if (qquseSheep(e)) ;
									else
										goto leave;
								}
							}

						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Gyrocopter)
						{

						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Huskar)
						{
							if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qquseEulEnem(e)) ;
									else
									if (qquseHelbard(e)) ;
									else
									if (qquseBlinkHome()) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qqtuskSnowBallTarget(e)) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqsilencerLastWord(e)) ;
									else
									if (qqPLDoppleganger()) ;
									else
									if (qqoracleFateTarget(e)) ;
									else
									if (qqNyx()) ;
									else
									if (qqLifestealerrage()) ;
									else
									if (qqjugerOmniTarget(e)) ;
									else
									if (qqDoom(e)) ;
									else
									if (qqAlchemistRage()) ;
									qquseLotus();
									goto leave;
								}
							}
						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Invoker)
						{
							if (D.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qqAlchemistRage()) ;
									else
									if (qqDoom(e)) ;
									else
									if (qqEmberWTarget(e)) ;
									else
									if (qqJuggernautfury()) ;
									else
									if (qqLifestealerrage()) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqNyx()) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqsilencerLastWord(e)) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qquseBladeMail()) ;
									else
										if (qquseColba(e)) ;
									else
										if (qquseEulEnem(e)) ;
									qquseLotus();
									if (qquseOrhid(e)) ;
									else
										if (qquseStuffTarget(e)) ;
									else
										if (qqskySilence(e)) ;
									else
										goto leave;
								}
								if (distance < 350)
								{
									if (qqaxeCall()) ;
									else
									if (qqemberhains()) ;
									else
									if (qqPLDoppleganger()) ;
									else
									if (qqpuckW()) ;
									else
									if (qqslardarCrush()) ;
									else
									if (qqtuskSnowBallTarget(e)) ;
									else
									if (qquseSheep(e)) ;
									else
										goto leave;
								}
							}
							if (F.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qqAlchemistRage()) ;
									else
									if (qqDoom(e)) ;
									else
									if (qqEmberWTarget(e)) ;
									else
									if (qqJuggernautfury()) ;
									else
									if (qqLifestealerrage()) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqNyx()) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqsilencerLastWord(e)) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qquseColba(e)) ;
									else
									if (qquseEulEnem(e)) ;
									qquseLotus();
									if (qquseOrhid(e)) ;
									else
									if (qquseStuffTarget(e)) ;
									else
										goto leave;
								}
								if (distance < 350)
								{
									if (qqaxeCall()) ;
									else
									if (qqemberhains()) ;
									else
									if (qqPLDoppleganger()) ;
									else
									if (qqpuckW()) ;
									else
									if (qqslardarCrush()) ;
									else
									if (qqtuskSnowBallTarget(e)) ;
									else
									if (qquseSheep(e)) ;
									else
										goto leave;
								}
							}

						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Wisp)
						{
							if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qqDoom(e)) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qquseColba(e)) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseOrhid(e)) ;
									else
									if (qquseSilence(e)) ;
									else
										goto leave;
								}
							}

						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Jakiro)
						{
							if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qqabadonWme()) ;
									else
									if (qqAlchemistRage()) ;
									else
									if (qqblinkHomeHero()) ;
									else
									if (qqemberE()) ;
									else
									if (qqJuggernautfury()) ;
									else
									if (qqLifestealerrage()) ;
									else
									if (qqslarkPounce()) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qquseEulEnem(e)) ;
									qquseLotus();
									if (qquseOrhid(e)) ;
									else
									if (qquseStuffTarget(e)) ;
									else
										goto leave;
								}
							}


						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Juggernaut)
						{
							if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qqAlchemistRage()) ;
									else
									if (qqblinkHomeHero()) ;
									else
									if (qqEmberFastRemn(e)) ;
									else
									if (qqEmberWTarget(e)) ;
									else
									if (qqlegionPress()) ;
									else
									if (qqNyxVendetta()) ;
									else
									if (qqBountyhunterWindwalk()) ;
									else
									if (qqClinkzWindwalk()) ;
									else
									if (qqGoInvis()) ;
									else
									if (qqoracleFateEdict(me)) ;
									else
									if (qqPLDoppleganger()) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqSandkinSandstorm()) ;
									else
									if (qqStormUltMe()) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qqTemplarMeld()) ;
									else
									if (qqtuskSnowBallTarget(e)) ;
									else
									if (qquseAmulet()) ;
									else
									if (qquseBlinkHome()) ;
									else
									if (qquseSB()) ;
									else
									if (qqWeaverShukuchi()) ;
									else
										goto leave;
								}
								if (distance < 300)
								{
									if (qqaxeCall()) ;
									else
									if (qqLoneDruidUlt()) ;
									else
									if (qqodImprisomentMe(me)) ;
									else
									if (qqtuskSnowBallTarget(e)) ;
									else
									if (qquseEulMe(me)) ;
									else
									if (qquseGhost()) ;
									else
									if (qquseCrimson()) ;
									else
									if (qquseEthernal(e)) ;
									else
										goto leave;
								}
							}
							if (e.NetworkActivity == NetworkActivity.Attack)
							{
								if (distance < 200)
								{
									if (qquseManta()) ;
									else
									if (qquseHelbard(e)) ;
									else
									if (qquseGhost()) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseColba(e)) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqpuckW()) ;
									else
									if (qqPLDoppleganger()) ;
									else
									if (qqNyx()) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqlegionPress()) ;
									else
									if (qqjugerOmniTarget(e)) ;
									else
									if (qqemberhains()) ;
									else
									if (qqClinkzWindwalk()) ;
									else
									if (qqBountyhunterWindwalk()) ;
									else
									if (qqaxeCall()) ;
									else
									if (qqabadonWme()) ;
									else
										goto leave;
								}
							}


						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_KeeperOfTheLight)
						{
							if (W.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qquseEulEnem(e)) ;
									else
									if (qquseOrhid(e)) ;
									else
									if (qqNyx()) ;
									else
										goto leave;
								}
							}


						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Kunkka)
						{
							if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{

									goto leave;
								}
							}
							if (e.NetworkActivity == NetworkActivity.Attack)
							{
								if (distance < 200)
								{
									if (qquseManta()) ;
									else
									if (qquseHelbard(e)) ;
									else
									if (qquseGhost()) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseColba(e)) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqpuckW()) ;
									else
									if (qqPLDoppleganger()) ;
									else
									if (qqNyx()) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqlegionPress()) ;
									else
									if (qqjugerOmniTarget(e)) ;
									else
									if (qqemberhains()) ;
									else
									if (qqClinkzWindwalk()) ;
									else
									if (qqBountyhunterWindwalk()) ;
									else
									if (qqaxeCall()) ;
									else
									if (qqabadonWme()) ;
									else
										goto leave;
								}
							}


						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Legion_Commander)
						{
							if (R.IsInAbilityPhase)
							{
								if (distance < 300)
								{
									if (qqDoom(e)) ;
									else
									if (qquseMagnet(me)) ;
									else
									if (qqBountyhunterWindwalk()) ;
									else
									if (qqAlchemistRage()) ;
									else
									if (qqDoom(e)) ;
									else
									if (qqEmberWTarget(e)) ;
									else
									if (qqLifestealerrage()) ;
									else
									if (qqNyx()) ;
									else
									if (qqoracleFateTarget(e)) ;
									else
									if (qqpuckW()) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqslarkDarkPact()) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qqTemplarMeld()) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseOrhid(e)) ;
									else
									if (qquseSheep(e)) ;
									else
										goto leave;
								}
								if (distance < 300)
								{
									if (qqaxeCall()) ;
									else
									if (qqemberhains()) ;
									else
									if (qqjugerOmniTarget(e)) ;
									else
									if (qqNyxVendetta()) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqslardarCrush()) ;
									else
									if (qqslarkShadowDance()) ;
									else
									if (qqStormUltTarget(e)) ;
									else
									if (qqtuskSnowBallTarget(e)) ;
									else
									if (qquseCrimson()) ;
									else
									if (qquseGhost()) ;
									qquseLotus();
									if (qquseManta()) ;
									else
									if (qquseSB()) ;
									else
									if (qquseSheep(e)) ;
									else
									if (qquseShiva()) ;
									else
									if (qquseStaffMe(me)) ;
									else
										goto leave;
								}
							}
							if (e.NetworkActivity == NetworkActivity.Attack)
							{
								if (distance < 200)
								{
									if (qquseManta()) ;
									else
									if (qquseHelbard(e)) ;
									else
									if (qquseGhost()) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseColba(e)) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqpuckW()) ;
									else
									if (qqPLDoppleganger()) ;
									else
									if (qqNyx()) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqlegionPress()) ;
									else
									if (qqjugerOmniTarget(e)) ;
									else
									if (qqemberhains()) ;
									else
									if (qqClinkzWindwalk()) ;
									else
									if (qqBountyhunterWindwalk()) ;
									else
									if (qqaxeCall()) ;
									else
									if (qqabadonWme()) ;
									else
										goto leave;
								}
							}


						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Leshrac)
						{
							if (R.IsInAbilityPhase)
							{
								if (distance < 400)
								{
									if (qqabadonWme()) ;
									else
									if (qqblinkHomeHero()) ;
									else
									if (qqDoom(e)) ;
									else
									if (qqemberE()) ;
									else
									if (qqEmberWTarget(e)) ;
									else
									if (qqJuggernautfury()) ;
									else
									if (qqLifestealerrage()) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qqtuskSnowBallTarget(e)) ;
									else
										goto leave;
								}
							}
							if (Q.IsInAbilityPhase)
							{
								if (distance < 500)
								{
									if (qqabadonWme()) ;
									else
									if (qqAlchemistRage()) ;
									else
									if (qqallHex(e)) ;
									else
									if (qqJuggernautfury()) ;
									else
									if (qqLifestealerrage()) ;
									else
									if (qqNyx()) ;
									else
									if (qqomniknightRepelMe(me)) ;
									else
									if (qqoracleFateEdict(me)) ;
									else
									if (qqPLDoppleganger()) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqslarkPounce()) ;
									else
									if (qqslarkDarkPact()) ;
									else
									if (qqtuskSnowBallTarget(e)) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qquseManta()) ;
									else
									if (qquseOrhid(e)) ;
									else
									if (qquseSheep(e)) ;
									else
									if (qquseStaffMe(me)) ;
									else
									if (qqskySilence(e)) ;
									else
										goto leave;
								}
							}
						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Lich)
						{

							if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									qquseLotus();
									if (qqNyx()) ;
									else
									if (qqBountyhunterWindwalk()) ;
									else
									if (qqClinkzWindwalk()) ;
									else
									if (qqDoom(e)) ;
									else
									if (qqemberE()) ;
									else
									if (qqEmberWTarget(e)) ;
									else
									if (qqJuggernautfury()) ;
									else
									if (qqLifestealerrage()) ;
									else
									if (qqoracleFateEdict(me)) ;
									else
									if (qqPLDoppleganger()) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqSandkinSandstorm()) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qqTemplarMeld()) ;
									else
									if (qqtuskSnowBallTarget(e)) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qquseBlinkHome()) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseOrhid(e)) ;
									else
									if (qqusePipe()) ;
									else
									if (qquseSheep(e)) ;
									else
									if (qquseSilence(e)) ;
									else
									if (qquseStaffMe(me)) ;
									else
									if (qqWeaverShukuchi()) ;
									else
										goto leave;
								}
							}

						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Life_Stealer)
						{
							if (me.Modifiers.Any(y => y.Name == "modifier_life_stealer_open_wounds"))
							{
								if (distance < 700)
								{
									if (qqabadonWme()) ;
									else
									if (qqAlchemistRage()) ;
									else
									if (qqallHex(e)) ;
									else
									if (qqblinkHomeHero()) ;
									else
									if (qqBountyhunterWindwalk()) ;
									else
									if (qqClinkzWindwalk()) ;
									else
									if (qqDoom(e)) ;
									else
									if (qqEmberWTarget(e)) ;
									else
									if (qqlegionPress()) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqNyx()) ;
									else
									if (qqNyxVendetta()) ;
									else
									if (qqoracleFateTarget(e)) ;
									else
									if (qqPLDoppleganger()) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqSandkinSandstorm()) ;
									else
									if (qqsilencerLastWord(e)) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqslarkDarkPact()) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qquseBlinkHome()) ;
									else
									if (qquseColba(e)) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseHelbard(e)) ;
									else
									if (qquseManta()) ;
									else
									if (qquseSB()) ;
									else
									if (qquseSheep(e)) ;
									else
									if (qqWeaverShukuchi()) ;
									else
										goto leave;
								}
							}
							if (e.NetworkActivity == NetworkActivity.Attack)
							{
								if (distance < 200)
								{
									if (qquseManta()) ;
									else
									if (qquseHelbard(e)) ;
									else
									if (qquseGhost()) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseColba(e)) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqpuckW()) ;
									else
									if (qqPLDoppleganger()) ;
									else
									if (qqNyx()) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqlegionPress()) ;
									else
									if (qqjugerOmniTarget(e)) ;
									else
									if (qqemberhains()) ;
									else
									if (qqClinkzWindwalk()) ;
									else
									if (qqBountyhunterWindwalk()) ;
									else
									if (qqaxeCall()) ;
									else
									if (qqabadonWme()) ;
									else
										goto leave;
								}
							}


						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Lina)
						{
							if (R.IsInAbilityPhase && !e.AghanimState())
							{
								if (distance < 700)
								{

									if (qqStormUltTarget(e)) ;
									else
									if (qqskySilence(e)) ;
									qquseLotus();
									if (qquseBladeMail()) ;
									else
									if (qquseSilence(e)) ;
									else
									if (qquseSheep(e)) ;
									else
									if (qqabadonWme()) ;
									else
									if (qqAlchemistRage()) ;
									else
									if (qqblinkHomeHero()) ;
									else
									if (qqBountyhunterWindwalk()) ;
									else
									if (qqClinkzWindwalk()) ;
									else
									if (qqDoom(e)) ;
									else
									if (qqemberE()) ;
									else
									if (qqEmberWTarget(e)) ;
									else
									if (qqJuggernautfury()) ;
									else
									if (qqlegionPress()) ;
									else
									if (qqLifestealerrage()) ;
									else
									if (qqLoneDruidUlt()) ;
									else
									if (qqNyx()) ;
									else
									if (qqomniknightRepelMe(me)) ;
									else
									if (qqoracleFateEdict(me)) ;
									else
									if (qqPLDoppleganger()) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqSandkinSandstorm()) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qqusePipe()) ;
									else
									if (qqWeaverShukuchi()) ;
									else
									if (qqtuskSnowBallTarget(e)) ;
									else
										goto leave;
								}
								if (distance < 300)
								{
									if (qqaxeCall()) ;
									else
									if (qqjugerOmniTarget(e)) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqsilencerUlt()) ;
									else
									if (qquseManta()) ;
									else
									if (qquseStuffTarget(e)) ;
									else
										goto leave;
								}
							}
							if (R.IsInAbilityPhase && e.AghanimState())
							{
								if (distance < 700)
								{
									if (qquseSilence(e)) ;
									else
									if (qqskySilence(e)) ;
									qquseLotus();
									if (qqabadonWme()) ;
									else
									  if (qqAlchemistRage()) ;
									else
									  if (qqblinkHomeHero()) ;
									else
									  if (qqBountyhunterWindwalk()) ;
									else
									  if (qqClinkzWindwalk()) ;
									else
									  if (qqLoneDruidUlt()) ;
									else
									  if (qqDoom(e)) ;
									else
									  if (qqEmberWTarget(e)) ;
									else
									  if (qqlegionPress()) ;
									else
									  if (qqtuskSnowBallTarget(e)) ;
									else
									  if (qqNyx()) ;
									else
									  if (qqoracleFateEdict(me)) ;
									else
									  if (qqPLDoppleganger()) ;
									else
									  if (qqPuckShift()) ;
									else
									  if (qqSandkinSandstorm()) ;
									else
									  if (qqTemplarRefraction()) ;
									else
									  if (qquseBladeMail()) ;
									else
									  if (qquseEulEnem(e)) ;
									else
									  if (qquseSheep(e)) ;
									else
									  if (qqWeaverShukuchi()) ;
									else
										goto leave;
								}
								if (distance < 300)
								{
									if (qqaxeCall()) ;
									else
									if (qqjugerOmniTarget(e)) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqsilencerUlt()) ;
									else
									if (qquseManta()) ;
									else
									if (qquseStuffTarget(e)) ;
									else
										goto leave;
								}
							}
							foreach (var mod in me.Modifiers)
							{
								if (me.Modifiers.Any(y => y.Name == "modifier_lina_laguna_blade"))
								{
									if (qqblinkHomeHero()) ;
									else
									if (qqabaddonUlt()) ;
									else
									if (qqEmberWTarget(e)) ;
									else
									if (qqLoneDruidUlt()) ;
									else
									if (qqNyx()) ;
									else
									if (qquseEulMe(me)) ;
									qquseLotus();
									if (qqslarkPounce()) ;
									else
										goto leave;
								}
							}


						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Lion)
						{

							if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qqabadonWme()) ;
									else
									if (qqAlchemistRage()) ;
									else
									if (qqblinkHomeHero()) ;
									else
									if (qqBountyhunterWindwalk()) ;
									else
									if (qqClinkzWindwalk()) ;
									else
									if (qqDoom(e)) ;
									else
									if (qqemberE()) ;
									else
									if (qqEmberWTarget(e)) ;
									else
									if (qqJuggernautfury()) ;
									else
									if (qqlegionPress()) ;
									else
									if (qqLifestealerrage()) ;
									else
									if (qqLoneDruidUlt()) ;
									else
									if (qqNyx()) ;
									else
									if (qqomniknightRepelMe(me)) ;
									else
									if (qqoracleFateEdict(me)) ;
									else
									if (qqPLDoppleganger()) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqSandkinSandstorm()) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qquseEulEnem(e)) ;
									qquseLotus();
									if (qquseBladeMail()) ;
									else
									if (qqusePipe()) ;
									else
									if (qquseSheep(e)) ;
									else
									if (qquseSilence(e)) ;
									else
									if (qqWeaverShukuchi()) ;
									else
									if (qqtuskSnowBallTarget(e)) ;
									else
										goto leave;
								}
								if (distance < 300)
								{
									if (qqaxeCall()) ;
									else
									if (qqjugerOmniTarget(e)) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqsilencerUlt()) ;
									else
									if (qquseManta()) ;
									else
									if (qquseStuffTarget(e)) ;
									else
										goto leave;
								}
							}
							foreach (var mod in me.Modifiers)
							{
								if (me.Modifiers.Any(y => y.Name == "modifier_lion_finger_of_death"))
								{
									if (qqblinkHomeHero()) ;
									else
									if (qqEmberWTarget(e)) ;
									else
									if (qqemberE()) ;
									else
									if (qqLoneDruidUlt()) ;
									else
									if (qqNyx()) ;
									else
									if (qqPLDoppleganger()) ;
									else
									if (qquseEulMe(me)) ;
									qquseLotus();
									if (qqslarkPounce()) ;
									else
										goto leave;
								}
							}

						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_LoneDruid)
						{

							if (Q.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qquseSilence(e)) ;
									else
									if (qquseOrhid(e)) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqallHex(e)) ;
									else
										goto leave;
								}
							}

						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Luna)
						{
							if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qqAlchemistRage()) ;
									else
									if (qqallHex(e)) ;
									else
									if (qqblinkHomeHero()) ;
									else
									if (qqBountyhunterWindwalk()) ;
									else
									if (qqClinkzWindwalk()) ;
									else
									if (qqDoom(e)) ;
									else
									if (qqemberE()) ;
									else
									if (qqEmberWTarget(e)) ;
									else
									if (qqJuggernautfury()) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqNyx()) ;
									else
									if (qqPLDoppleganger()) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqSandkinSandstorm()) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qquseBlinkHome()) ;
									else
									if (qquseHelbard(e)) ;
									else
									if (qqusePipe()) ;
									else
									if (qqWeaverShukuchi()) ;
									else
										goto leave;
								}
							}


						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Lycan)
						{
							if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qqallHex(e)) ;
									else
									if (qqDoom(e)) ;
									else
									if (qqEmberWTarget(e)) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqtuskSnowBallTarget(e)) ;
									else
									if (qquseColba(e)) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseOrhid(e)) ;
									else
										goto leave;
								}
								if (distance < 300)
								{
									if (qqaxeCall()) ;
									else
									if (qqemberhains()) ;
									else
									if (qqjugerOmniTarget(e)) ;
									else
									if (qqpuckW()) ;
									else
									if (qquseAbyssal(e)) ;
									else
									if (qquseSilence(e)) ;
									else
										goto leave;
								}
							}
							if (e.NetworkActivity == NetworkActivity.Attack)
							{
								if (distance < 200)
								{
									if (qquseManta()) ;
									else
									if (qquseHelbard(e)) ;
									else
									if (qquseGhost()) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseColba(e)) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqpuckW()) ;
									else
									if (qqPLDoppleganger()) ;
									else
									if (qqNyx()) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqlegionPress()) ;
									else
									if (qqjugerOmniTarget(e)) ;
									else
									if (qqemberhains()) ;
									else
									if (qqClinkzWindwalk()) ;
									else
									if (qqBountyhunterWindwalk()) ;
									else
									if (qqaxeCall()) ;
									else
									if (qqabadonWme()) ;
									else
										goto leave;
								}
							}


						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Magnataur)
						{
							if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qqDoom(e)) ;
									else
									if (qqEmberWTarget(e)) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqsilencerLastWord(e)) ;
									else
									if (qqsilencerUlt()) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqtuskSnowBallTarget(e)) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qquseColba(e)) ;
									else
									if (qquseCrimson()) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseHelbard(e)) ;
									else
									if (qquseOrhid(e)) ;
									else
									if (qqusePipe()) ;
									else
									if (qquseSheep(e)) ;
									else
										goto leave;
								}
								if (distance < 400)
								{
									if (qquseStuffTarget(e)) ;
									else
									if (qqabadonWme()) ;
									else
									if (qqAlchemistRage()) ;
									else
									if (qqblinkHomeHero()) ;
									else
									if (qqemberE()) ;
									else
									if (qqjugerOmniTarget(e)) ;
									else
									if (qqLifestealerrage()) ;
									else
									if (qqNyx()) ;
									else
									if (qqPhoenixsupernova()) ;
									else
									if (qqpuckW()) ;
									else
									if (qqPLDoppleganger()) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqslarkDarkPact()) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qquseBKB()) ;
									else
									if (qquseColba(e)) ;
									else
									if (qquseCrimson()) ;
									else
									if (qquseEulEnem(e)) ;
									qquseLotus();
									if (qquseManta()) ;
									else
										goto leave;
								}
								if (distance < 300)
								{
									if (qqaxeCall()) ;
									else
									if (qqBountyhunterWindwalk()) ;
									else
									if (qqClinkzWindwalk()) ;
									else
									if (qqemberhains()) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqslardarCrush()) ;
									else
									if (qquseSDisription(e)) ;
									else
										goto leave;
								}
							}


						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Medusa)
						{

							if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qqallHex(e)) ;
									else
									if (qqBountyhunterWindwalk()) ;
									else
									if (qqClinkzWindwalk()) ;
									else
									if (qqDoom(e)) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqNyxVendetta()) ;
									else
									if (qqoracleFateTarget(e)) ;
									else
									if (qqPhoenixsupernova()) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqDamageNull()) ;
									else
									if (qquseSDisription(e)) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qquseAmulet()) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qquseBlinkHome()) ;
									else
									if (qquseColba(e)) ;
									else
									if (qquseCrimson()) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseHelbard(e)) ;
									else
									if (qquseSheep(e)) ;
									else
									if (qqWeaverShukuchi()) ;
									else
										goto leave;
								}
								if (distance < 300)
								{
									if (qqAlchemistRage()) ;
									else
									if (qqaxeCall()) ;
									else
									if (qqblinkHomeHero()) ;
									else
									if (qqemberhains()) ;
									else
									if (qqjugerOmniTarget(e)) ;
									else
									if (qqlegionPress()) ;
									else
									if (qqNyx()) ;
									else
									if (qqpuckW()) ;
									else
									if (qqDamageNull()) ;
									else
									if (qquseSDisription(e)) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qquseAbyssal(e)) ;
									else
									if (qquseShiva()) ;
									else
										goto leave;
								}
							}

						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Meepo)
						{
							if (W.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qqallHex(e)) ;
									else
									if (qqDoom(e)) ;
									else
									if (qqEmberWTarget(e)) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qquseSDisription(e)) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qquseColba(e)) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseOrhid(e)) ;
									else
									if (qquseSheep(e)) ;
									else
										goto leave;
								}
							}


						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Mirana)
						{
							if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{

									goto leave;
								}
							}
						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Morphling)
						{
							if (D.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qquseSheep(e)) ;
									else
									if (qquseOrhid(e)) ;
									else
									if (qqsilencerLastWord(e)) ;
									else
									if (qqDoom(e)) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qquseSilence(e)) ;
									else
										goto leave;
								}
								if (distance < 300)
								{
									if (qqaxeCall()) ;
									else
									if (qqjugerOmniTarget(e)) ;
									else
									if (qqpuckW()) ;
									else
									if (qquseSDisription(e)) ;
									else
									if (qqslardarCrush()) ;
									else
									if (qquseAbyssal(e)) ;
									else
										goto leave;
								}
							}



						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Naga_Siren)
						{
							if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qqallHex(e)) ;
									else
									if (qqDoom(e)) ;
									else
									if (qqEmberWTarget(e)) ;
									else
									if (qqJuggernautfury()) ;
									else
									if (qqLifestealerrage()) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqsilencerUlt()) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qquseColba(e)) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseOrhid(e)) ;
									else
									if (qquseSilence(e)) ;
									else
									if (qquseSheep(e)) ;
									else
										goto leave;
								}
								if (distance < 300)
								{
									if (qqaxeCall()) ;
									else
									if (qqemberhains()) ;
									else
									if (qqjugerOmniTarget(e)) ;
									else
									if (qqPhoenixsupernova()) ;
									else
									if (qqslardarCrush()) ;
									else
										goto leave;
								}
							}


						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Furion)
						{

							if (R.IsInAbilityPhase)
							{
								if (distance > 1025 && me.IsVisibleToEnemies)
								{
									if (qqSmoke(e)) ;
									else
									if (qqTemplarMeld()) ;
									else
									if (qqBountyhunterWindwalk()) ;
									else
									if (qqClinkzWindwalk()) ;
									else
									if (qqPuckShift()) ;
									else
									if (qquseSDisription(me)) ;
									else
									if (qqSandkinSandstorm()) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qquseEulMe(me)) ;
									else
									if (qqWeaverShukuchi()) ;
									else
									if (qqabadonWme()) ;
									else
										goto leave;
								}
								if (distance < 700)
								{
									if (qquseOrhid(e)) ;
									else
									if (qquseColba(e)) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqPLDoppleganger()) ;
									else
									if (qqabadonWme()) ;
									else
										goto leave;
								}
							}

						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Necrolyte)
						{

							if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qqabaddonUlt()) ;
									else
									if (qqblinkHomeHero()) ;
									else
									if (qqallHex(e)) ;
									else
									if (qqemberE()) ;
									else
									if (qqLifestealerrage()) ;
									else
									if (qqNyx()) ;
									else
									if (qqoracleFateEdict(me)) ;
									else
									if (qqPLDoppleganger()) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqSandkinSandstorm()) ;
									else
									if (qquseSDisription(e)) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqslarkShadowDance()) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qquseColba(e)) ;
									else
									if (qquseEulEnem(e)) ;
									qquseLotus();
									if (qquseOrhid(e)) ;
									else
									if (qquseManta()) ;
									else
									if (qqusePipe()) ;
									else
									if (qquseSheep(e)) ;
									else
									if (qquseSilence(e)) ;
									else
									if (qqWeaverShukuchi()) ;
									else
									if (qqtuskSnowBallTarget(e)) ;
									else
										goto leave;
								}
							}

						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_NightStalker)
						{
							if (e.NetworkActivity == NetworkActivity.Attack)
							{
								if (distance < 200)
								{
									if (qquseManta()) ;
									else
									if (qquseHelbard(e)) ;
									else
									if (qquseGhost()) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseColba(e)) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqpuckW()) ;
									else
									if (qqPLDoppleganger()) ;
									else
									if (qqNyx()) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqlegionPress()) ;
									else
									if (qqjugerOmniTarget(e)) ;
									else
									if (qqemberhains()) ;
									else
									if (qqClinkzWindwalk()) ;
									else
									if (qqBountyhunterWindwalk()) ;
									else
									if (qqaxeCall()) ;
									else
									if (qqabadonWme()) ;
									else
										goto leave;
								}
							}


						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Nyx_Assassin)
						{
							if (E.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qquseSDisription(e)) ;
									else
									if (qquseEulEnem(e)) ;
									else
										goto leave;
								}
							}


						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Ogre_Magi)
						{
							if (Q.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qquseColba(e)) ;
									else
									if (qqallHex(e)) ;
									else
									if (qqDoom(e)) ;
									else
									if (qqBountyhunterWindwalk()) ;
									else
									if (qqClinkzWindwalk()) ;
									else
									if (qqLifestealerrage()) ;
									else
									if (qqNyx()) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqsilencerLastWord(e)) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqslarkDarkPact()) ;
									else
									if (qqTemplarMeld()) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qqblinkHomeHero()) ;
									else
									if (qqemberE()) ;
									else
									if (qqEmberWTarget(e)) ;
									else
									if (qqWeaverShukuchi()) ;
									else
									if (qquseSB()) ;
									else
									if (qquseOrhid(e)) ;
									else
									if (qquseManta()) ;
									else
										goto leave;
								}
							}
						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Omniknight)
						{
							if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{

									goto leave;
								}
							}


						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Oracle)
						{
							if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{

									{
										if (qquseOrhid(e)) ;
										else
										if (qqallHex(e)) ;
										else
										if (qqDoom(e)) ;
										else
										if (qquseSDisription(e)) ;
										else
										if (qqskySilence(e)) ;
										else
										if (qquseEulEnem(e)) ;
										else
										if (qquseSheep(e)) ;
										else
											goto leave;
									}
								}
							}


						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Obsidian_Destroyer)
						{
							if (R.IsInAbilityPhase)
							{
								if (distance < 700 && me.TotalIntelligence < e.TotalIntelligence && e.ClassID == ClassID.CDOTA_Unit_Hero_Obsidian_Destroyer)
								{
									if (qquseOrhid(e)) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseBlinkHome()) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qquseBKB()) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qqtuskSnowBallTarget(e)) ;
									else
									if (qqStormUltTarget(e)) ;
									else
									if (qqslarkPounce()) ;
									else
									if (qqsilencerUlt()) ;
									else
									if (qquseSDisription(e)) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqoracleFateEdict(me)) ;
									else
									if (qqomniknightRepelMe(me)) ;
									else
									if (qqNyx()) ;
									else
									if (qqLoneDruidUlt()) ;
									else
									if (qqLifestealerrage()) ;
									else
									if (qqEmberWTarget(e)) ;
									else
									if (qqemberE()) ;
									else
									if (qqDoom(e)) ;
									else
									if (qqblinkHomeHero()) ;
									else
									if (qqallHex(e)) ;
									else
									if (qqabaddonUlt()) ;
									else
									if (qqabadonWme()) ;
									else
										goto leave;
								}
							}
						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_PhantomAssassin)
						{
							if (e.NetworkActivity == NetworkActivity.Attack)
							{
								if (distance < 200)
								{
									if (qquseManta()) ;
									else
									if (qquseHelbard(e)) ;
									else
									if (qquseGhost()) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseColba(e)) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqpuckW()) ;
									else
									if (qqPLDoppleganger()) ;
									else
									if (qqNyx()) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqlegionPress()) ;
									else
									if (qqjugerOmniTarget(e)) ;
									else
									if (qqemberhains()) ;
									else
									if (qqClinkzWindwalk()) ;
									else
									if (qqBountyhunterWindwalk()) ;
									else
									if (qqaxeCall()) ;
									else
									if (qqabadonWme()) ;
									else
										goto leave;
								}
							}
						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_PhantomLancer)
						{
							if (W.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qqallHex(e)) ;
									else
									if (qqDoom(e)) ;
									else
									if (qqEmberWTarget(e)) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qquseSDisription(e)) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qquseColba(e)) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseOrhid(e)) ;
									else
									if (qquseSheep(e)) ;
									else
										goto leave;
								}
								if (distance < 450)
								{
									if (qqpuckW()) ;
								}
								if (distance < 350)
								{
									if (qqslardarCrush()) ;
									else
										goto leave;
								}
							}
							if (e.NetworkActivity == NetworkActivity.Attack)
							{
								if (distance < 200)
								{
									if (qquseManta()) ;
									else
									if (qquseHelbard(e)) ;
									else
									if (qquseGhost()) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseColba(e)) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqpuckW()) ;
									else
									if (qqPLDoppleganger()) ;
									else
									if (qqNyx()) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqlegionPress()) ;
									else
									if (qqjugerOmniTarget(e)) ;
									else
									if (qqemberhains()) ;
									else
									if (qqClinkzWindwalk()) ;
									else
									if (qqBountyhunterWindwalk()) ;
									else
									if (qqaxeCall()) ;
									else
									if (qqabadonWme()) ;
									else
										goto leave;
								}
							}
						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Phoenix)
						{
							if (Q.IsInAbilityPhase || e.Modifiers.Any(y => y.Name == "modifier_phoenix_icarus_dive"))
							{
								if (distance < 700)
								{
									if (qqallHex(e)) ;
									else
									if (qqallAltStun(e)) ;
									else
									if (qqallStun(e)) ;
									else
									if (qquseEulEnem(e)) ;
									else
										goto leave;
								}
							}
							if (me.Modifiers.Any(y => y.Name == "modifier_phoenix_supernova_hiding"))
							{
								if (distance < 700)
								{
									if (qqAlchemistRage()) ;
									else
									if (qqLifestealerrage()) ;
									else
									if (qqlegionPress()) ;
									else
										goto leave;
								}
							}
						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Pudge)
						{
							if (R.IsInAbilityPhase)
							{
								if (distance < 350)
								{
									if (qqallStun(e)) ;
									else
									if (qqaxeCall()) ;
									else
									if (qqDoom(e)) ;
									else
									if (qqJuggernautfury()) ;
									else
									if (qqNyx()) ;
									else
									if (qqoracleFateEdict(me)) ;
									else
									if (qqpuckW()) ;
									else
									if (qqDamageNull()) ;
									else
									if (qquseSDisription(e)) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqslardarCrush()) ;
									else
									if (qqslarkDarkPact()) ;
									else
									if (qqStormUltMe()) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qqtuskSnowBallTarget(e)) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qquseColba(e)) ;
									else
									if (qquseEulEnem(e)) ;
									qquseLotus();
									if (qquseManta()) ;
									else
									if (qquseOrhid(e)) ;
									else
									if (qquseSheep(e)) ;
									else
									if (qquseStuffTarget(e)) ;
									else
										goto leave;
								}
							}



						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Pugna)
						{
							if (R.IsInAbilityPhase)
							{
								if (distance < 400)
								{
									if (qqabadonWme()) ;
									else
									if (qqallAltStun(e)) ;
									else
									if (qqallStun(e)) ;
									else
									if (qqemberE()) ;
									else
									if (qqNyx()) ;
									else
									if (qqDoom(e)) ;
									else
									if (qqemberE()) ;
									else
									if (qqEmberWTarget(e)) ;
									else
									if (qqLifestealerrage()) ;
									else
									if (qqtuskSnowBallTarget(e)) ;
									else
										goto leave;
								}
							}


						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_QueenOfPain)
						{
							if (Q.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qqabadonWme()) ;
									else
									if (qqJuggernautfury()) ;
									else
									if (qqPuckShift()) ;
									else
									if (qquseSDisription(e)) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqStormUltMe()) ;
									else
									if (qqTemplarMeld()) ;
									else
									if (qquseManta()) ;
									else
									if (qquseOrhid(e)) ;
									else
									if (qquseStuffTarget(e)) ;
									else
									if (qqWeaverShukuchi()) ;
									else
									if (qqBountyhunterWindwalk()) ;
									else
									if (qqClinkzWindwalk()) ;
								}
							}
							if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qqallAltStun(e)) ;
									else
									if (qqallHex(e)) ;
									else
									if (qqallStun(e)) ;
									else
									if (qqblinkHomeHero()) ;
									else
									if (qqDoom(e)) ;
									else
									if (qqEmberWTarget(e)) ;
									else
									if (qqNyx()) ;
									else
									if (qqoracleFateEdict(me)) ;
									else
									if (qqPuckShift()) ;
									else
									if (qquseSDisription(e)) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qqStormUltTarget(e)) ;
									else
									if (qqtuskSnowBallTarget(e)) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qquseBlinkHome()) ;
									else
									if (qquseEulEnem(e)) ;
									qquseLotus();
									if (qquseOrhid(e)) ;
									else
									if (qquseSheep(e)) ;
									else
									if (qquseStuffTarget(e)) ;
								}
								if (distance < 360)
								{
									if (qqemberhains()) ;
									else
									if (qqjugerOmniTarget(e)) ;
									else
									if (qqpuckW()) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqslardarCrush()) ;
								}
							}
							if (W.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qqallAltStun(e)) ;
									else
									if (qqallHex(e)) ;
									else
									if (qqallStun(e)) ;
									else
									if (qquseOrhid(e)) ;
									else
									if (qquseSheep(e)) ;
									else
									if (qqskySilence(e)) ;
								}
							}

						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Razor)
						{
							if (W.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qqallAltStun(e)) ;
									else
									if (qqallHex(e)) ;
									else
									if (qqallStun(e)) ;
									else
									if (qquseOrhid(e)) ;
									else
									if (qquseSheep(e)) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqDamageNullTarget(e)) ;
									else
									if (qqEmberWTarget(e)) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqodImprisomentMe(e)) ;
									else
									if (qquseColba(e)) ;
									else
									if (qquseHelbard(e)) ;
									else
									if(qquseSilence(e));
									else
										goto leave;
								}
								
                            }
							if(me.Modifiers.Any(y => y.Name == "modifier_razor_static_link_debuff") && distance <= 700)
							{
								if (qqWeaverShukuchi()) ;
								else
									if (qquseShiva()) ;
								else
									if (qquseSB()) ;
								else
									if (qquseManta()) ;
								else
									if (qquseHelbard(e)) ;
								else
									if (qquseEulEnem(e)) ;
								else
									if (qqTemplarMeld()) ;
								else
									if (qqStormUltMe()) ;
								else 
                                    if (qqsilencerLastWord(e)) ;
								else
									if (qquseSDisription(e)) ;
							}

						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Riki)
						{
							if (e.NetworkActivity == NetworkActivity.Attack)
							{
								if (distance < 200)
								{
									if (qquseManta()) ;
									else
									if (qquseHelbard(e)) ;
									else
									if (qquseGhost()) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseColba(e)) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqpuckW()) ;
									else
									if (qqPLDoppleganger()) ;
									else
									if (qqNyx()) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqlegionPress()) ;
									else
									if (qqjugerOmniTarget(e)) ;
									else
									if (qqemberhains()) ;
									else
									if (qqClinkzWindwalk()) ;
									else
									if (qqBountyhunterWindwalk()) ;
									else
									if (qqaxeCall()) ;
									else
									if (qqabadonWme()) ;
									else
										goto leave;
								}
							}


						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Rubick)
						{
							if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									goto leave;

								}
							}


						}

						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_SandKing)
						{
							if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qqallAltStun(e)) ;
									else
									if (qqallHex(e)) ;
									else
									if (qqallStun(e)) ;
									else
									if (qqDoom(e)) ;
									else
									if (qqEmberWTarget(e)) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qquseSDisription(e)) ;
									else
									if (qqsilencerLastWord(e)) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qquseColba(e)) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseOrhid(e)) ;
									else
									if (qquseSheep(e)) ;
									else
									if (qquseSilence(e)) ;
									else
										goto leave;
								}
							}


						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Shadow_Demon)
						{
							if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qqskySilence(e)) ;
									else

										goto leave;
								}
							}


						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_ShadowShaman)
						{
							if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qqskySilence(e)) ;
									else
									if (qqallAltStun(e)) ;
									else
									if (qqabadonWme()) ;
									else
									if (qqallHex(e)) ;
									else
									if (qqallStun(e)) ;
									else
									if (qqblinkHomeHero()) ;
									else
									if (qquseSDisription(e)) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qquseColba(e)) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseOrhid(e)) ;
									else
									if (qquseSheep(e)) ;
									else
									if (qquseStaffMe(me)) ;
								}
							}
							if (E.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qqskySilence(e)) ;
									else
									if (qqallAltStun(e)) ;
									else
									if (qqabadonWme()) ;
									else
									if (qqallHex(e)) ;
									else
									if (qqallStun(e)) ;
									else
									if (qqblinkHomeHero()) ;
									else
									if (qquseSDisription(e)) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qquseColba(e)) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseOrhid(e)) ;
									else
									if (qquseSheep(e)) ;
									else
										goto leave;
								}
							}
							if (W.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qqskySilence(e)) ;
									else
									if (qqallAltStun(e)) ;
									else
									if (qqabadonWme()) ;
									else
									if (qqallHex(e)) ;
									else
									if (qqallStun(e)) ;
									else
									if (qqblinkHomeHero()) ;
									else
									if (qquseSDisription(e)) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qquseColba(e)) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseOrhid(e)) ;
									else
									if (qquseSheep(e)) ;
									else
										goto leave;
								}
							}

						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Silencer)
						{
							if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qqskySilence(e)) ;
									else
									if (qqallAltStun(e)) ;
									else
									if (qqabadonWme()) ;
									else
									if (qqallHex(e)) ;
									else
									if (qqAlchemistRage()) ;
									else
									if (qqabadonWme()) ;
									else
									if (qqJuggernautfury()) ;
									else
									if (qqEmberWTarget(e)) ;
									else
									if (qqemberE()) ;
									else
									if (qqDoom(e)) ;
									else
									if (qqBountyhunterWindwalk()) ;
									else
									if (qqClinkzWindwalk()) ;
									else
									if (qqDoom(e)) ;
									else
									if (qqlegionPress()) ;
									else
									if (qqLifestealerrage()) ;
									else
									if (qqLoneDruidUlt()) ;
									else
									if (qqNyx()) ;
									else
									if (qqoracleFateTarget(e)) ;
									else
									if (qqPLDoppleganger()) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqSandkinSandstorm()) ;
									else
									if (qquseSDisription(me)) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqStormUltTarget(e)) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qqtuskSnowBallTarget(e)) ;
									else
									if (qquseOrhid(e)) ;
									else
									if (qquseSheep(e)) ;
									else
									if (qquseSilence(e)) ;
									else
									if (qqWeaverShukuchi()) ;
									else
									if (qqallStun(e)) ;
									else
									if (qqblinkHomeHero()) ;
									else
									if (qquseSDisription(e)) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qquseColba(e)) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseOrhid(e)) ;
									else
									if (qquseSheep(e)) ;
									else
									if (qquseStaffMe(me)) ;
								}
								if (qqslarkDarkPact()) ;
								else
								if (qquseEulMe(me)) ;
								else
									goto leave;
							}


						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Skywrath_Mage)
						{
							if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{

									goto leave;
								}
							}


						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Slardar)
						{
							if (e.NetworkActivity == NetworkActivity.Attack)
							{
								if (distance < 200)
								{
									if (qquseManta()) ;
									else
									if (qquseHelbard(e)) ;
									else
									if (qquseGhost()) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseColba(e)) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqpuckW()) ;
									else
									if (qqPLDoppleganger()) ;
									else
									if (qqNyx()) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqlegionPress()) ;
									else
									if (qqjugerOmniTarget(e)) ;
									else
									if (qqemberhains()) ;
									else
									if (qqClinkzWindwalk()) ;
									else
									if (qqBountyhunterWindwalk()) ;
									else
									if (qqaxeCall()) ;
									else
									if (qqabadonWme()) ;
									else
										goto leave;
								}
							}


						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Slark)
						{
							if (e.NetworkActivity == NetworkActivity.Attack)
							{
								if (distance < 200)
								{
									if (qquseManta()) ;
									else
									if (qquseHelbard(e)) ;
									else
									if (qquseGhost()) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseColba(e)) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqpuckW()) ;
									else
									if (qqPLDoppleganger()) ;
									else
									if (qqNyx()) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqlegionPress()) ;
									else
									if (qqjugerOmniTarget(e)) ;
									else
									if (qqemberhains()) ;
									else
									if (qqaxeCall()) ;
									else
									if (qqabadonWme()) ;
									else
										goto leave;
								}
							}


						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Sniper)
						{
							if (e.NetworkActivity == NetworkActivity.Attack)
							{
								if (distance <= 399)
								{
									if (qquseHelbard(e)) ;
									else
									if (qquseGhost()) ;
									else
									if (qquseColba(e)) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqpuckW()) ;
									else
									if (qqNyx()) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqlegionPress()) ;
									else
									if (qqjugerOmniTarget(e)) ;
									else
									if (qqemberhains()) ;
									else
									if (qqaxeCall()) ;
									else
									if (qqabadonWme()) ;
									else
									if (qquseEulEnem(e)) ;
									goto leave;
								}
							}


						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Spectre)
						{
							if (e.NetworkActivity == NetworkActivity.Attack)
							{
								if (distance < 200)
								{
									if (qquseManta()) ;
									else
									if (qquseHelbard(e)) ;
									else
									if (qquseGhost()) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseColba(e)) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqpuckW()) ;
									else
									if (qqPLDoppleganger()) ;
									else
									if (qqNyx()) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqlegionPress()) ;
									else
									if (qqjugerOmniTarget(e)) ;
									else
									if (qqemberhains()) ;
									else
									if (qqClinkzWindwalk()) ;
									else
									if (qqBountyhunterWindwalk()) ;
									else
									if (qqaxeCall()) ;
									else
									if (qqabadonWme()) ;
									else
										goto leave;
								}
							}


						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_SpiritBreaker)
						{
							if (e.NetworkActivity == NetworkActivity.Attack)
							{
								if (distance < 200)
								{
									if (qquseManta()) ;
									else
									if (qquseHelbard(e)) ;
									else
									if (qquseGhost()) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseColba(e)) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqpuckW()) ;
									else
									if (qqPLDoppleganger()) ;
									else
									if (qqNyx()) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqlegionPress()) ;
									else
									if (qqjugerOmniTarget(e)) ;
									else
									if (qqemberhains()) ;
									else
									if (qqClinkzWindwalk()) ;
									else
									if (qqBountyhunterWindwalk()) ;
									else
									if (qqaxeCall()) ;
									else
									if (qqabadonWme()) ;
									else
										goto leave;
								}
							}
							if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qqallAltStun(e)) ;
									else
									if (qqAlchemistRage()) ;
									else
									if (qqallHex(e)) ;
									else
									if (qqallStun(e)) ;
									else
									if (qqblinkHomeHero()) ;
									else
									if (qqClinkzWindwalk()) ;
									else
									if (qqBountyhunterWindwalk()) ;
									else
									if (qqDoom(e)) ;
									else
									if (qqEmberWTarget(e)) ;
									else
									if (qqJuggernautfury()) ;
									else
									if (qqLifestealerrage()) ;
									else
									if (qqLoneDruidUlt()) ;
									else
									if (qqNyx()) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqoracleFateTarget(e)) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqSandkinSandstorm()) ;
									else
									if (qquseSDisription(e)) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqsilencerLastWord(e)) ;
									else
									if (qqslarkDarkPact()) ;
									else
									if (qqStormUltMe()) ;
									else
									if (qqTemplarMeld()) ;
									else
									if (qqtuskSnowBallTarget(e)) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseEthernal(e)) ;
									else
									if (qquseGhost()) ;
									else
									if (qquseManta()) ;
									else
									if (qquseOrhid(e)) ;
									else
									if (qquseSheep(e)) ;
									else
									if (qqWeaverShukuchi()) ;
									else
										goto leave;
								}
							}
						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_StormSpirit)
						{
							if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qqallAltStun(e)) ;
									else
									if (qqAlchemistRage()) ;
									else
									if (qqallHex(e)) ;
									else
									if (qqallStun(e)) ;
									else
									if (qqDoom(e)) ;
									else
									if (qqEmberWTarget(e)) ;
									else
									if (qqJuggernautfury()) ;
									else
									if (qqLifestealerrage()) ;
									else
									if (qqLoneDruidUlt()) ;
									else
									if (qqNyx()) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqoracleFateTarget(e)) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqSandkinSandstorm()) ;
									else
									if (qquseSDisription(e)) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqsilencerLastWord(e)) ;
									else
									if (qqslarkDarkPact()) ;
									else
									if (qqStormUltMe()) ;
									else
									if (qqtuskSnowBallTarget(e)) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseEthernal(e)) ;
									else
									if (qquseGhost()) ;
									else
									if (qquseManta()) ;
									else
									if (qquseOrhid(e)) ;
									else
									if (qquseSheep(e)) ;
									else
										goto leave;

								}
							}


						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Sven)
						{

							if (Q.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									qquseLotus();
									if (qquseEulEnem(e)) ;
									else
									if (qquseEthernal(e)) ;
									else
									if (qquseGhost()) ;
									else
									if (qquseManta()) ;
									else
									if (qquseOrhid(e)) ;
									else
									if (qquseSheep(e)) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqWeaverShukuchi()) ;
									else
									if (qqoracleFateTarget(e)) ;
									else
									if (qqSandkinSandstorm()) ;
									else
									if (qquseSDisription(e)) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqClinkzWindwalk()) ;
									else
									if (qqBountyhunterWindwalk()) ;
									else
									if (qqDoom(e)) ;
									else
									if (qqsilencerLastWord(e)) ;
									else
									if (qqslarkDarkPact()) ;
									else
									if (qqStormUltMe()) ;
									else
									if (qqTemplarMeld()) ;
									else
									if (qqEmberWTarget(e)) ;
									else
									if (qqJuggernautfury()) ;
									else
									if (qqLifestealerrage()) ;
									else
									if (qqLoneDruidUlt()) ;
									else
									if (qqallHex(e)) ;
									else
									if (qqallAltStun(e)) ;
									else
									if (qqallStun(e)) ;
									else
									if (qqNyx()) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqAlchemistRage()) ;
									else
									if (qquseBlinkTarget(me)) ;
									else
									if (qqtuskSnowBallTarget(e)) ;
									else
									if (qquseBladeMail()) ;
									else
										goto leave;
								}
							}
							if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qqallAltStun(e)) ;
									else
									if (qqAlchemistRage()) ;
									else
									if (qqallHex(e)) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qquseHelbard(e)) ;
									else
									if (qqallStun(e)) ;
									else
									if (qqblinkHomeHero()) ;
									else
									if (qqClinkzWindwalk()) ;
									else
									if (qqBountyhunterWindwalk()) ;
									else
									if (qqDoom(e)) ;
									else
									if (qqEmberWTarget(e)) ;
									else
									if (qqJuggernautfury()) ;
									else
									if (qqLifestealerrage()) ;
									else
									if (qqLoneDruidUlt()) ;
									else
									if (qqNyx()) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqoracleFateTarget(e)) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqSandkinSandstorm()) ;
									else
									if (qquseSDisription(e)) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqsilencerLastWord(e)) ;
									else
									if (qqtuskSnowBallTarget(e)) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseEthernal(e)) ;
									else
									if (qquseGhost()) ;
									else
									if (qquseManta()) ;
									else
									if (qquseOrhid(e)) ;
									else
									if (qquseSheep(e)) ;
									else
									if (qqWeaverShukuchi()) ;
									else
										goto leave;
								}
							}

							if (e.NetworkActivity == NetworkActivity.Attack)
							{
								if (distance < 200)
								{
									if (qquseManta()) ;
									else
									if (qquseHelbard(e)) ;
									else
									if (qquseGhost()) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseColba(e)) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqpuckW()) ;
									else
									if (qqPLDoppleganger()) ;
									else
									if (qqNyx()) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqlegionPress()) ;
									else
									if (qqjugerOmniTarget(e)) ;
									else
									if (qqemberhains()) ;
									else
									if (qqClinkzWindwalk()) ;
									else
									if (qqBountyhunterWindwalk()) ;
									else
									if (qqaxeCall()) ;
									else
									if (qqabadonWme()) ;
									else
										goto leave;
								}
							}


						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Techies)
						{
							if (E.Cooldown <=0)
							{
								if (distance < 300)
								{

								}
							}
									if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{

									goto leave;
								}
							}


						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_TemplarAssassin)
						{
							if (e.NetworkActivity == NetworkActivity.Attack)
							{
								if (distance < 200)
								{
									if (qquseManta()) ;
									else
									if (qquseHelbard(e)) ;
									else
									if (qquseGhost()) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseColba(e)) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqpuckW()) ;
									else
									if (qqPLDoppleganger()) ;
									else
									if (qqNyx()) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqlegionPress()) ;
									else
									if (qqjugerOmniTarget(e)) ;
									else
									if (qqemberhains()) ;
									else
									if (qqClinkzWindwalk()) ;
									else
									if (qqBountyhunterWindwalk()) ;
									else
									if (qqaxeCall()) ;
									else
									if (qqabadonWme()) ;
									else
										goto leave;
								}
							}


						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Terrorblade)
						{
							if (e.NetworkActivity == NetworkActivity.Attack)
							{
								if (distance < 200)
								{
									if (qquseManta()) ;
									else
									if (qquseHelbard(e)) ;
									else
									if (qquseGhost()) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseColba(e)) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqpuckW()) ;
									else
									if (qqPLDoppleganger()) ;
									else
									if (qqNyx()) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqlegionPress()) ;
									else
									if (qqjugerOmniTarget(e)) ;
									else
									if (qqemberhains()) ;
									else
									if (qqClinkzWindwalk()) ;
									else
									if (qqBountyhunterWindwalk()) ;
									else
									if (qqaxeCall()) ;
									else
									if (qqabadonWme()) ;
									else
										goto leave;
								}
							}
						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Tidehunter)
						{
							if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{

									goto leave;
								}
							}
						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Shredder)
						{
							if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{

									goto leave;
								}
							}
						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Tinker)
						{
							if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{

									goto leave;
								}
							}
						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Tiny)
						{
							if (Q.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qqallAltStun(e)) ;
									else
									if (qqAlchemistRage()) ;
									else
									if (qqallHex(e)) ;
									else
									if (qqallStun(e)) ;
									else
									if (qqDoom(e)) ;
									else
									if (qqEmberWTarget(e)) ;
									else
									if (qqNyx()) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqoracleFateTarget(e)) ;
									else

									if (qqDamageNull()) ;
									else if (qquseColba(e)) ;
									else if (qquseEulEnem(e)) ;
									else if (qqusePipe()) ;
									else if (qquseSheep(e)) ;
									else if (qquseShiva()) ;
									else if (qquseSilence(e)) ;

								}
								if (distance < 350)
								{
									if (qqGoInvis()) ;
									else if (qqPuckShift()) ;
									else if (qqLoneDruidUlt()) ;
									else if (qqJuggernautfury()) ;
									else if (qqblinkHomeHero()) ;
									else if (qqemberE()) ;
									else if (qqLifestealerrage()) ;
									else if (qqNyx()) ;
									else goto leave;
								}
							}
							if (W.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qqallAltStun(e)) ;
									else if (qqAlchemistRage()) ;
									else if (qqallHex(e)) ;
									else if (qqallStun(e)) ;
									else if (qqDoom(e)) ;
									else if (qqEmberWTarget(e)) ;
									else if (qqNyx()) ;
									else if (qqNagaEnsnare(e)) ;
									else if (qqoracleFateTarget(e)) ;
									else if (qqDamageNull()) ;
									else if (qquseColba(e)) ;
									else if (qquseEulEnem(e)) ;
									else if (qqusePipe()) ;
									else if (qquseSheep(e)) ;
									else if (qquseShiva()) ;
									else if (qquseSilence(e)) ;

								}
								if (distance < 350)
								{
									if (qqGoInvis()) ;
									else if (qqPuckShift()) ;
									else if (qqLoneDruidUlt()) ;
									else if (qqJuggernautfury()) ;
									else if (qqblinkHomeHero()) ;
									else if (qqemberE()) ;
									else if (qqLifestealerrage()) ;
									else if (qqNyx()) ;
									else goto leave;
								}
							}
							if (e.NetworkActivity == NetworkActivity.Attack)
							{
								if (distance < 200)
								{
									if (qquseManta()) ;
									else
									if (qquseHelbard(e)) ;
									else
									if (qquseGhost()) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseColba(e)) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqpuckW()) ;
									else
									if (qqPLDoppleganger()) ;
									else
									if (qqNyx()) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqlegionPress()) ;
									else
									if (qqjugerOmniTarget(e)) ;
									else
									if (qqemberhains()) ;
									else
									if (qqClinkzWindwalk()) ;
									else
									if (qqaxeCall()) ;
									else
									if (qqabadonWme()) ;
									else
										goto leave;
								}
							}


						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Treant)
						{
							if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{

									goto leave;
								}
							}


						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_TrollWarlord)
						{
							if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{

									goto leave;
								}
							}


						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Tusk)
						{
							if (e.NetworkActivity == NetworkActivity.Attack)
							{
								if (distance < 200)
								{
									if (qquseManta()) ;
									else
									if (qquseHelbard(e)) ;
									else
									if (qquseGhost()) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseColba(e)) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqpuckW()) ;
									else
									if (qqPLDoppleganger()) ;
									else
									if (qqNyx()) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqlegionPress()) ;
									else
									if (qqjugerOmniTarget(e)) ;
									else
									if (qqemberhains()) ;
									else
									if (qqClinkzWindwalk()) ;
									else
									if (qqBountyhunterWindwalk()) ;
									else
									if (qqaxeCall()) ;
									else
									if (qqabadonWme()) ;
									else
										goto leave;
								}
							}


						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Undying)
						{
							if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{

									goto leave;
								}
							}


						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Ursa)
						{
							if (e.NetworkActivity == NetworkActivity.Attack)
							{
								if (distance < 200)
								{
									if (qquseManta()) ;
									else
									if (qquseHelbard(e)) ;
									else
									if (qquseGhost()) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseColba(e)) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqpuckW()) ;
									else
									if (qqPLDoppleganger()) ;
									else
									if (qqNyx()) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqlegionPress()) ;
									else
									if (qqjugerOmniTarget(e)) ;
									else
									if (qqemberhains()) ;
									else
									if (qqClinkzWindwalk()) ;
									else
									if (qqBountyhunterWindwalk()) ;
									else
									if (qqaxeCall()) ;
									else
									if (qqabadonWme()) ;
									else
										goto leave;
								}
							}


						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_VengefulSpirit)
						{
							if (Q.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									qquseLotus();
									if (qqallAltStun(e)) ;
									else
									if (qqAlchemistRage()) ;
									else
									if (qqallHex(e)) ;
									else
									if (qqallStun(e)) ;
									else
									if (qqblinkHomeHero()) ;
									else
									if (qqClinkzWindwalk()) ;
									else
									if (qqBountyhunterWindwalk()) ;
									else
									if (qqDoom(e)) ;
									else
									if (qqEmberWTarget(e)) ;
									else
									if (qqJuggernautfury()) ;
									else
									if (qqLifestealerrage()) ;
									else
									if (qqLoneDruidUlt()) ;
									else
									if (qqNyx()) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqoracleFateTarget(e)) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqSandkinSandstorm()) ;
									else
									if (qquseSDisription(e)) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqsilencerLastWord(e)) ;
									else
									if (qqslarkDarkPact()) ;
									else
									if (qqStormUltMe()) ;
									else
									if (qqTemplarMeld()) ;
									else
									if (qqtuskSnowBallTarget(e)) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseEthernal(e)) ;
									else
									if (qquseGhost()) ;
									else
									if (qquseManta()) ;
									else
									if (qquseOrhid(e)) ;
									else
									if (qquseSheep(e)) ;
									else
									if (qqWeaverShukuchi()) ;
									else
										goto leave;
								}
							}
						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Venomancer)
						{
							if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qquseBlinkHome()) ;
									else
									if (qqblinkHomeHero()) ;
									else
										goto leave;
								}
							}
						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Viper)
						{
							if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									qquseLotus();
									if (qquseBlinkTarget(me)) ;
									goto leave;
								}
							}
						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Visage)
						{
							if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{

									goto leave;
								}
							}
							if (W.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									qquseLotus();
									if (qquseBlinkTarget(me)) ;
									else
										if (qqallAltStun(e)) ;
									else if (qqallStun(e)) ;
									else if (qqTemplarRefraction()) ;
									goto leave;
								}
							}

						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Warlock)
						{
							if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qqskySilence(e)) ;
									else if (qqDoom(e)) ;
									else if (qqallHex(e)) ;
									else if (qqallStun(e)) ;
									goto leave;
								}
							}
						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Weaver)
						{
							if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{

									goto leave;
								}
							}


						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Winter_Wyvern)
						{
							if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{

									goto leave;
								}
							}
						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_WitchDoctor)
						{
							if (R.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qqallAltStun(e)) ;
									else
									if (qqAlchemistRage()) ;
									else
									if (qqallHex(e)) ;
									else
									if (qqallStun(e)) ;
									else
									if (qqblinkHomeHero()) ;
									else
									if (qquseColba(e)) ;
									else
									if (qqClinkzWindwalk()) ;
									else
									if (qqBountyhunterWindwalk()) ;
									else
									if (qqDoom(e)) ;
									else
									if (qqEmberWTarget(e)) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqSandkinSandstorm()) ;
									else
									if (qquseSDisription(e)) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqsilencerLastWord(e)) ;
									else
									if (qqTemplarMeld()) ;
									else
									if (qqtuskSnowBallTarget(e)) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseEthernal(e)) ;
									else
									if (qquseGhost()) ;
									else
									if (qquseManta()) ;
									else
									if (qquseOrhid(e)) ;
									else
									if (qquseSheep(e)) ;
									else
									if (qqWeaverShukuchi()) ;
								}
								if (distance < 350)
								{
									if (qqaxeCall()) ;
									else
									if (qqjugerOmniTarget(e)) ;
									else
									if (qqpuckW()) ;
									else
									if (qquseAbyssal(e)) ;
									else
										goto leave;

								}
							}


						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_SkeletonKing)
						{
							if (e.NetworkActivity == NetworkActivity.Attack)
							{
								if (distance < 200)
								{
									if (qquseManta()) ;
									else
									if (qquseHelbard(e)) ;
									else
									if (qquseGhost()) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseColba(e)) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qqTemplarRefraction()) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqpuckW()) ;
									else
									if (qqPLDoppleganger()) ;
									else
									if (qqNyx()) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqlegionPress()) ;
									else
									if (qqjugerOmniTarget(e)) ;
									else
									if (qqemberhains()) ;
									else
									if (qqClinkzWindwalk()) ;
									else
									if (qqBountyhunterWindwalk()) ;
									else
									if (qqaxeCall()) ;
									else
									if (qqabadonWme()) ;
								}
							}
							if (Q.IsInAbilityPhase)
							{
								if (distance < 700)
								{
									if (qqallAltStun(e)) ;
									else
									if (qqAlchemistRage()) ;
									else
									if (qqallHex(e)) ;
									else
									if (qqallStun(e)) ;
									else
									if (qquseBlinkTarget(me)) ;
									else
									if (qqClinkzWindwalk()) ;
									else
									if (qqBountyhunterWindwalk()) ;
									else
									if (qqDoom(e)) ;
									else
									if (qqEmberWTarget(e)) ;
									else
									if (qqJuggernautfury()) ;
									else
									if (qqLifestealerrage()) ;
									else
									if (qqLoneDruidUlt()) ;
									else
									if (qqNyx()) ;
									else
									if (qqNagaEnsnare(e)) ;
									else
									if (qqoracleFateTarget(e)) ;
									else
									if (qqPuckShift()) ;
									else
									if (qqDamageNull()) ;
									else
									if (qqSandkinSandstorm()) ;
									else
									if (qquseSDisription(e)) ;
									else
									if (qqskySilence(e)) ;
									else
									if (qqsilencerLastWord(e)) ;
									else
									if (qqslarkDarkPact()) ;
									else
									if (qqStormUltMe()) ;
									else
									if (qqTemplarMeld()) ;
									else
									if (qqtuskSnowBallTarget(e)) ;
									else
									if (qquseBladeMail()) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseEthernal(e)) ;
									else
									if (qquseGhost()) ;
									else
									if (qquseManta()) ;
									else
									if (qquseOrhid(e)) ;
									else
									if (qquseSheep(e)) ;
									else
									if (qqWeaverShukuchi()) ;
									else
										goto leave;
								}
							}
						}
						else if (e.ClassID == ClassID.CDOTA_Unit_Hero_Zuus)
						{
							if (R.IsInAbilityPhase)
							{
								if (distance > 1025)
								{
									if (qqSmoke(e)) ;
								}
								if (qqBountyhunterWindwalk()) ;
								else
									if (qqClinkzWindwalk()) ;
								else
									if (qqemberE()) ;
								else
									if (qqJuggernautfury()) ;
								else
									if (qqLoneDruidUlt()) ;
								else
									if (qqNyx()) ;
								else
									if (qqPuckShift()) ;
								else
									if (qqSandkinSandstorm()) ;
								else
									if (qquseSDisription(me)) ;
								else
									if (qquseEulMe(me)) ;
								else
									if (qqGoInvis()) ;
								else
									if (qqWeaverShukuchi()) ;

								if (distance <= 700)
								{
									if (qqallAltStun(e)) ;
									else
									if (qqallHex(e)) ;
									else
									if (qqallStun(e)) ;
									else
									if (qqDoom(e)) ;
									else
									if (qqEmberWTarget(e)) ;
									else
									if (qqStormUltTarget(e)) ;
									else
									if (qqtuskSnowBallTarget(e)) ;
									else
									if (qquseColba(e)) ;
									else
									if (qquseEulEnem(e)) ;
									else
									if (qquseOrhid(e)) ;
									else
									if (qquseSheep(e)) ;
									else
										goto leave;
								}
								if (distance <= 350)
								{
									if (qqaxeCall()) ;
									else
									if (qqemberhains()) ;
									else
									if (qqjugerOmniTarget(e)) ;
									else
									if (qqpuckW()) ;
									else
									if (qqslardarCrush()) ;
									else
										goto leave;

								}
							}
						}
						var blink = e.FindItem("item_blink");
						if (blink != null && blink.Cooldown > 11)
						{
							if (me.Distance2D(e) <= 350)
							{
								qquseLotus();
								if (qquseShiva()) ;
								else if (qquseAbyssal(e)) ;
								else if (qqaxeCall()) ;
								else if (qqemberhains()) ;
								else if (qqabadonWme()) ;
								else if (qqemberE()) ;
								else if (qqjugerOmniTarget(e)) ;
								else if (qqJuggernautfury()) ;
								else if (qqLifestealerrage()) ;
								else if (qqlegionPress()) ;
								else if (qqNyx()) ;
								else if (qqodImprisomentMe(e)) ;
								else if (qqomniknightRepelMe(me)) ;
								else if (qqpuckW()) ;
								else if (qquseSDisription(e)) ;
								else if (qqsilencerLastWord(e)) ;
								else if (qqslardarCrush()) ;
								else if (qqTemplarRefraction()) ;
								else if (qqtuskSnowBallTarget(e)) ;
							}
							if (me.Distance2D(e) <= 700)
							{
								if (qqallStun(e)) ;
								else if (qqallHex(e)) ;
								else if (qqallAltStun(e)) ;
								else if (qqallStun(e)) ;
								else if (qqEmberWTarget(e)) ;
								else if (qqskySilence(e)) ;
								else if (qqtuskSnowBallTarget(e)) ;
								else if (qquseOrhid(e)) ;
								else if (qquseSheep(e)) ;
								goto leave;
							}

						}
					}


					foreach (var mod in me.Modifiers)
					{
						if (me.Modifiers.Any(y => y.Name == "modifier_sniper_assassinate") && mod.RemainingTime < 0.5)
						{
							if (qqblinkHomeHero()) ;
							else
							if (qqabadonWme()) ;
							else
							if (qqEmberWTarget(e)) ;
							else
							if (qqJuggernautfury()) ;
							else
							if (qqLifestealerrage()) ;
							else
							if (qqLoneDruidUlt()) ;
							else
							if (qqNyx()) ;
							else
							if (qqemberE()) ;
							else
							if (qqPLDoppleganger()) ;
							else
							if (qquseEulMe(me)) ;
							qquseLotus();
							if (qqslarkPounce()) ;
							else
								goto leave;
						}
					}
					if (me.Modifiers.Any(y => y.Name == "modifier_skywrath_mystic_flare_aura_effect"))
					{
						if (distance < 1300)
						{
							if (qquseStaffMe(me)) ;
							else
							if (qquseEulMe(me)) ;
							else
							if (qqTemplarRefraction()) ;
							else
							if (qqslarkShadowDance()) ;
							else
							if (qquseSDisription(me)) ;
							else
							if (qqPuckShift()) ;
							else
							if (qqPLDoppleganger()) ;
							else
							if (qqNyx()) ;
							else
							if (qqLifestealerrage()) ;
							else
							if (qqJuggernautfury()) ;
							else
							if (qqemberE()) ;
							else
							if (qqAlchemistRage()) ;
							else
							if (qqabadonWme()) ;
							else
							if (qquseBKB()) ;
							else
								goto leave;
						}
					}
					if (me.Modifiers.Any(y => y.Name == "modifier_spirit_breaker_charge_of_darkness_vision") && distance <= 500)
					{
						if (qquseEulEnem(e)) ;
						else
						if (qqabadonWme()) ;
						else
						if (qquseColba(e)) ;
						else
						if (qqallAltStun(e)) ;
						else
						if (qqallHex(e)) ;
						else
						if (qqallStun(e)) ;
						else
						if (qqaxeCall()) ;
						else
						if (qqBountyhunterWindwalk()) ;
						else
						if (qqClinkzWindwalk()) ;
						else
						if (qqDoom(e)) ;
						else
						if (qqemberhains()) ;
						else
						if (qqJuggernautfury()) ;
						else
						if (qqLifestealerrage()) ;
						else
						if (qqomniknightRepelMe(me)) ;
						else
						if (qqoracleFateTarget(e)) ;
						else
						if (qqPLDoppleganger()) ;
						else
						if (qqPuckShift()) ;
						else
						if (qqDamageNull()) ;
						else
						if (qquseSDisription(e)) ;
						else
						if (qqslarkDarkPact()) ;
						else
						if (qqTemplarRefraction()) ;
						else
						if (qqTemplarMeld()) ;
						else
						if (qqtuskSnowBallTarget(e)) ;
						else
						if (qquseBladeMail()) ;
						else
						if (qquseHelbard(e)) ;
						else
						if (qquseOrhid(e)) ;
						else
						if (qquseStuffTarget(e)) ;
						else
						if (qqWeaverShukuchi()) ;
						else
						if (qqEmberWTarget(e)) ;
						else
						if (qqJuggernautfury()) ;
						else
						if (qqLifestealerrage()) ;
						else
						if (qqLoneDruidUlt()) ;
						else
						if (qqNyx()) ;
						else
						if (qqemberE()) ;
						else
						if (qqPLDoppleganger()) ;
						else
						if (qquseEulMe(me)) ;
						qquseLotus();
						if (qqslarkPounce()) ;
						else
							goto leave;
					}

					if (me.IsSilenced() && !me.Modifiers.Any(
			x => x.Name == "modifier_riki_smoke_screen" || x.Name == "modifier_disruptor_static_storm"))
					{
						if (qquseAmulet()) ;
						else
						if (qquseManta()) ;
						else
						if (qquseSB()) ;
						else
						if (qqpurgeDebuff(me)) ;
						else
							goto leave;
					}
					if (me.Modifiers.Any(y => y.Name == "modifier_lion_finger_of_death"))
					{

						if (qquseBladeMail()) ;
						else
						if (qqTemplarRefraction()) ;
						qquseLotus();
						if (qquseBKB()) ;
						else
							goto leave;
					}
					if (me.Modifiers.Any(y => y.Name == "modifier_juggernaut_omnislash"))
					{
						if (qquseGhost()) ;
						else
						if (qquseEulMe(me)) ;
						else
						if (qqWeaverShukuchi()) ;
						else
						if (qqPuckShift()) ;
						else
						if (qqPLDoppleganger()) ;
						else
						if (qqClinkzWindwalk()) ;
						else
						if (qqBountyhunterWindwalk()) ;
						else
						if (qqaxeCall()) ;
						qquseLotus();
						goto leave;
					}
					if (me.Modifiers.Any(y => y.Name == "modifier_nyx_assassin_vendetta_duration"))
					{
						if (qqBountyhunterWindwalk()) ;
						else
						if (qqClinkzWindwalk()) ;
						else
						if (qqjugerOmniTarget(e)) ;
						else
						if (qqemberE()) ;
						else
						if (qqemberhains()) ;
						else
						if (qqGoInvis()) ;
						else
						if (qqJuggernautfury()) ;
						else
						if (qqLifestealerrage()) ;
						else
						if (qqoracleFateEdict(me)) ;
						else
						if (qqDamageNull()) ;
						else
						if (qquseSDisription(e)) ;
						else
						if (qqskySilence(e)) ;
						else
						if (qqslardarCrush()) ;
						else
						if (qqaxeCall()) ;
						else
						if (qqDoom(e)) ;
						else
						if (qquseBladeMail()) ;
						else
						if (qquseColba(e)) ;
						else
						if (qquseEulEnem(e)) ;
						else
						if (qquseManta()) ;
						else
						if (qquseOrhid(e)) ;
						else
						if (qqusePipe()) ;
						else
						if (qquseStaffMe(me)) ;
						else
							goto leave;
					}
					if (distance <= 650)
					{
						if (e.Modifiers.Any(y => y.Name == "modifier_omniknight_repel"))
						{
							if (qqpurgeDebuffEnemy(e)) ;
							else
								goto leave;
						}
						if (e.Modifiers.Any(y => y.Name == "modifier_legion_commander_press_the_attack"))
						{
							if (distance <= 290)
							{
								if (qquseMagnet(me)) ;
							}
						}
						if (e.Modifiers.Any(y => y.Name == "modifier_ursa_overpower"))
						{
							if (distance <= 290)
							{
								if (qquseMagnet(me)) ;
								if (qqaxeCall()) ;

							}
						}
						if (e.Modifiers.Any(y => y.Name == "modifier_faceless_void_time_walk"))
						{
							if (qqallAltStun(e)) ;
							else if (qqallHex(e)) ;
							else if (qqDamageNull()) ;
							else if (qqDamageNullTarget(e)) ;
							else if (qqDoom(e)) ;
							else if (qqNagaEnsnare(e)) ;
							else if (qqNyx()) ;
							else if (qqskySilence(e)) ;
							else if (qqTemplarRefraction()) ;
							else if (qqtuskSnowBallTarget(e)) ;
							else if (qquseBladeMail()) ;
							else if (qquseColba(e)) ;
							else if (qquseHelbard(e)) ;
							else if (qquseOrhid(e)) ;
							else if (qquseSheep(e)) ;
							else if (qquseShiva()) ;
							else
								goto leave;
						}
						if ((me.Modifiers.Any(y => y.Name == "modifier_slardar_amplify_damage") || me.Modifiers.Any(y => y.Name == "modifier_bounty_hunter_track") || me.Modifiers.Any(y => y.Name == "modifier_smoke_of_deceit")) && invUnit(me) && me.Distance2D(e) <= e.AttackRange + 100)
						{
							if (qqpurgeDebuff(me)) ;
							goto leave;
						}
						if (me.Modifiers.Any(y => y.Name == "modifier_riki_smoke_screen"))
						{
							if (qquseStaffMe(me)) ;
							else
							if (qquseEulEnem(e)) ;
							goto leave;
						}
						/*
						if (me.Distance2D(e) <= 700 && me.Modifiers.Any(y => y.Name == "modifier_lich_chain_frost_thinker") && e.ClassID == ClassID.CDOTA_Unit_Hero_Lich)
						{
							if (qqStormUltTarget(e)) ;
						}
						*/
					}
					uint[] QDistance = { 275, 375, 475, 575 };
					if (me.Modifiers.Any(y => y.Name == "modifier_winter_wyvern_arctic_burn_flight"))
					{
						if (distance <= me.AttackRange + QDistance[Q.Level - 1])
						{
							if (!e.Modifiers.Any(y => y.Name == "modifier_winter_wyvern_arctic_burn_slow") && Utils.SleepCheck("q"))
							{
								me.Attack(e);
								Utils.Sleep(250, "q");
							}
						}
					}


				}
				/*
				for (int i = 0; i < enemy.Count(); i++)
				{
					var enemupos = enemy[i].Position;

					var meposs = Drawing.WorldToScreen(me.Position);

					var epos = Drawing.WorldToScreen(enemupos);

					if ((meposs.X == 0 && meposs.Y == 0) || (epos.X == 0 && epos.Y == 0)) continue;
					//Print($"w2shero: {w2shero.X}/{w2shero.Y}");
					//Print($"w2sPos: {w2sPos.X}/{w2sPos.Y}");
					if(me.Position.Distance2D(enemy[i])<= 1200)
					Drawing.DrawLine(meposs, epos, Color.DarkRed);
				}
				*/

				//Lich/timber UnitState: Invulnerable, Unselectable, NotOnMinimapForEnemies, NoHealthbar, Flying, NoCollision
				//Apparat1 UnitState: Invisible, Invulnerable, Unselectable, NoHealthbar, Flying, NoCollision
				//Apparat2 UnitState: Invisible, Invulnerable, Unselectable, NotOnMinimapForEnemies, NoHealthbar, Flying, NoCollision
				var baseDota =
				ObjectManager.GetEntities<Unit>().Where(unit => unit.Name == "npc_dota_base" && unit.Team != me.Team).ToList();

				if (baseDota != null)
				{
					bool iscreated = false;
					if (Utils.SleepCheck("Effects"))
					{
						for (int i = 0; i < baseDota.Count(); i++)
						{
							List<ParticleEffect> Effects = new List<ParticleEffect>();
							string EffectPath = @"particles\range_display_gold.vpcf";
							if (!iscreated)
							{
								ParticleEffect effect3 = new ParticleEffect(EffectPath, baseDota[i]);
								effect3.SetControlPoint(1, new Vector3(500, 0, 0));
								Effects.Add(effect3);
								break;
							}
						}
						iscreated = true;
						Utils.Sleep(150, "Effects");
					}
				}

				var greenmine =
				ObjectManager.GetEntities<Unit>().Where(unit => unit.Name == "npc_dota_techies_remote_mine" && unit.Team != me.Team && unit.IsAlive).ToList();
				if (greenmine != null)
				{
					for (int i = 0; i < greenmine.Count(); i++)
					{
						float angle = me.FindAngleBetween(greenmine[i].Position, true);
						Vector3 pos = new Vector3((float)(greenmine[i].Position.X - 540 * Math.Cos(angle)), (float)(greenmine[i].Position.Y - 540 * Math.Sin(angle)), 0);

						if (me.Distance2D(greenmine[i]) <= 520)
						{
							if (Utils.SleepCheck("q"))
							{
								me.Move(pos);
								Utils.Sleep(100, "q");
							}
							if (me.Distance2D(greenmine[i]) <= 500)
							{
							if (qqStormUltDodge(greenmine[i])) ;
								else
							if (qqemberE()) ;
								else
							if (qqLifestealerrage()) ;
								else
							if (qqTemplarRefraction()) ;
                            }
						}
					}
				}

				var redmine =
				ObjectManager.GetEntities<Unit>().Where(unit => unit.Name == "npc_dota_techies_land_mine" && unit.Team != me.Team && unit.IsAlive).ToList();
				if (redmine != null)
				{
					for (int i = 0; i < redmine.Count(); i++)
					{
						float angle = me.FindAngleBetween(redmine[i].Position, true);
						Vector3 pos = new Vector3((float)(redmine[i].Position.X - 266 * Math.Cos(angle)), (float)(redmine[i].Position.Y - 266 * Math.Sin(angle)), 0);

						if (me.Distance2D(redmine[i]) <= 255)
						{
							if (Utils.SleepCheck("q"))
							{
								me.Move(pos);
								Utils.Sleep(100, "q");
							}
						}
					}
				}

					var thinker =
				ObjectManager.GetEntities<Unit>().Where(unit => unit.Name == "npc_dota_thinker" && unit.Team != me.Team).ToList();


				if (thinker != null)
				{
					for (int i = 0; i < thinker.Count(); i++)
					{
						var nomodif = me.Modifiers.All(y => y.Name == "modifier_puck_phase_shift");

						if (me.Distance2D(thinker[i]) <= 230 && thinker[i].Modifiers.Any(y => y.Name == "modifier_leshrac_split_earth_thinker"))
						{
							float angle = me.FindAngleBetween(thinker[i].Position, true);
							Vector3 pos = new Vector3((float)(thinker[i].Position.X - 230 * Math.Cos(angle)), (float)(thinker[i].Position.Y - 230 * Math.Sin(angle)), 0);
							if (qqStormUltDodge(thinker[i])) ;
							else
							if (qqemberE()) ;
							else
							if (qqlegionPress()) ;
							else
							if (qqLifestealerrage()) ;
							else
							if (qqNyx()) ;
							else
							if (qqPuckShift() && !nomodif && !me.IsChanneling()) ;
							else
							if (qqslarkPounce()) ;
							else
							if (qqTemplarRefraction()) ;
							else
							if (qquseBladeMail()) ;
							else
							if (Utils.SleepCheck("q") && !nomodif && !me.IsChanneling())
							{
								me.Move(pos);
								Utils.Sleep(100, "q");
							}
							else if (qquseStaffMe(me)) ;
							break;
						}
						else if (me.Distance2D(thinker[i]) <= 230 && thinker[i].Modifiers.Any(y => y.Name == "modifier_lina_light_strike_array"))
						{
							float angle = me.FindAngleBetween(thinker[i].Position, true);
							Vector3 pos = new Vector3((float)(thinker[i].Position.X - 230 * Math.Cos(angle)), (float)(thinker[i].Position.Y - 230 * Math.Sin(angle)), 0);

							if (qqStormUltDodge(thinker[i])) ;
							else
							if (qqemberE()) ;
							else
							if (qqlegionPress()) ;
							else
							if (qqLifestealerrage()) ;
							else
							if (qqNyx()) ;
							else
							if (qqPuckShift() && !nomodif && !me.IsChanneling()) ;
							else
							if (qqslarkPounce()) ;
							else
							if (qqTemplarRefraction()) ;
							else
							if (qquseBladeMail()) ;
							else
							if (Utils.SleepCheck("q") && !nomodif && !me.IsChanneling())
							{
								me.Move(pos);
								Utils.Sleep(100, "q");
							}
							else if (qquseStaffMe(me)) ;
							break;
						}
						else if (me.Distance2D(thinker[i]) <= 210 && thinker[i].Modifiers.Any(y => y.Name == "modifier_invoker_sun_strike"))
						{
							float angle = me.FindAngleBetween(thinker[i].Position, true);
							Vector3 pos = new Vector3((float)(thinker[i].Position.X - 210 * Math.Cos(angle)), (float)(thinker[i].Position.Y - 210 * Math.Sin(angle)), 0);

							if (qqNyx()) ;
							else
							if (qqPuckShift() && !nomodif) ;
							else
							if (qqslarkPounce()) ;
							else
							if (qqTemplarRefraction()) ;
							else
							if (Utils.SleepCheck("q") && !nomodif && !me.IsChanneling())
							{
								me.Move(pos);
								Utils.Sleep(100, "q");
							}
							else if (qquseBladeMail()) ;
							break; //TODO: tested need
						}
						else if (me.Distance2D(thinker[i]) <= 255 && thinker[i].Modifiers.Any(y => y.Name == "modifier_kunkka_torrent_thinker"))
						{
							float angle = me.FindAngleBetween(thinker[i].Position, true);
							Vector3 pos = new Vector3((float)(thinker[i].Position.X - 255 * Math.Cos(angle)), (float)(thinker[i].Position.Y - 255 * Math.Sin(angle)), 0);

							if (qqNyx()) ;
							else
							if (qqslarkPounce()) ;
							else
							if (qqTemplarRefraction()) ;
							else
							if (qquseBladeMail()) ;
							else
							if (Utils.SleepCheck("q") && !nomodif && !me.IsChanneling())
							{
								me.Move(pos);
								Utils.Sleep(100, "q");
							}
							else if (qquseStaffMe(me)) ;

							break; //TODO: tested need
						}
						else if (me.Distance2D(thinker[i]) <= 512
							&& thinker[i].Modifiers.Any(y => y.Name == "modifier_faceless_void_chronosphere") && me.ClassID != ClassID.CDOTA_Unit_Hero_FacelessVoid)
						{
							float angle = me.FindAngleBetween(thinker[i].Position, true);
							Vector3 pos = new Vector3((float)(thinker[i].Position.X - 515 * Math.Cos(angle)), (float)(thinker[i].Position.Y - 515 * Math.Sin(angle)), 0);

							if (Utils.SleepCheck("q"))
							{
								me.Move(pos);
								Utils.Sleep(50, "q");
							}
						}
					}
					bool iscreated = false;
					if (Utils.SleepCheck("Effects"))
					{
						for (int i = 0; i < thinker.Count(); i++)
						{
							List<ParticleEffect> Effects = new List<ParticleEffect>();
							string EffectPath = @"particles\range_display_gold.vpcf";
							if (!iscreated)
							{
								ParticleEffect effect3 = new ParticleEffect(EffectPath, thinker[i]);
								effect3.SetControlPoint(1, new Vector3(230, 0, 0));
								Effects.Add(effect3);
							}
						}
						iscreated = true;
						Utils.Sleep(150, "Effects");
					}
				}
			}
			leave:;
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
		public static bool qqStormUltDodge(Unit x)
		{
			float angle = me.FindAngleBetween(x.Position, true);
			Vector3 pos = new Vector3((float)(x.Position.X - 500 * Math.Cos(angle)), (float)(x.Position.Y - 500 * Math.Sin(angle)), 0);

			var lightning = me.FindSpell("storm_spirit_ball_lightning");
			if (lightning != null && lightning.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				lightning.UseAbility(pos);
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qqSmoke(Hero x)
		{
			var smoke = me.FindItem("item_smoke_of_deceit");
			if (smoke != null && smoke.CanBeCasted() && me.Distance2D(x) >= 1026 &&
				Utils.SleepCheck("cast"))
			{
				smoke.UseAbility();
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qqGoInvis()
		{
			var SB = me.FindItem("item_invis_sword") ?? me.FindItem("item_silver_edge") ?? me.FindItem("item_glimmer_cape");
			if (SB != null && SB.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				SB.UseAbility();
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qquseAmulet()
		{

			var _amulet = me.FindItem("item_shadow_amulet");
			if (_amulet != null && _amulet.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				_amulet.UseAbility(me);
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static void qquseLotus()
		{
			var lotus = me.FindItem("item_lotus_orb") ?? me.FindItem("pugna_nether_ward");


			if (lotus != null && lotus.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				lotus.UseAbility(me);
				lotus.UseAbility(me.Position);
				Utils.Sleep(100, "cast");
			}

			var mail = me.FindItem("item_blade_mail");
			if (mail != null && mail.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				mail.UseAbility();
				Utils.Sleep(100, "cast");
			}
		}

		public static bool qquseSB()
		{

			var invis = me.FindItem("item_silver_edge") ?? me.FindItem("item_invis_sword");
			if (invis != null && invis.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				invis.UseAbility();
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;

		}

		public static bool qquseManta()
		{
			var manta = me.FindItem("item_manta");
			if (manta != null && manta.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				manta.UseAbility();
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;

		}

		public static bool qqpurgeDebuff(Hero x)
		{
			var purge = me.FindItem("item_diffusal_blade") ?? me.FindItem("item_diffusal_blade_2") ?? me.FindSpell("satyr_trickster_purge");
			if (purge != null && purge.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				purge.UseAbility(x);
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;

		}

		public static bool qqpurgeDebuffEnemy(Hero x)
		{
			var purge = me.FindItem("item_diffusal_blade") ?? me.FindItem("item_diffusal_blade_2") ?? me.FindSpell("satyr_trickster_purge");
			if (purge != null && purge.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				purge.UseAbility(x);
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qquseBKB()
		{

			var _bar = me.FindItem("item_black_king_bar");
			if (_bar != null && _bar.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				_bar.UseAbility();
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;

		}

		public static bool qquseEthernal(Hero x)
		{
			var ghost = me.FindItem("item_ethereal_blade") ?? me.FindSpell("pugna_decrepify");
			if (ghost != null && ghost.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				ghost.UseAbility(x);
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qquseHelbard(Hero x)
		{
			var halberd = me.FindItem("item_heavens_halberd");
			if (halberd != null && halberd.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				halberd.UseAbility(x);
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qquseAbyssal(Hero x)
		{



			var abyssal = me.FindItem("item_abyssal_blade");
			if (abyssal != null && abyssal.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				abyssal.UseAbility(x);
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qquseOrhid(Hero x)
		{
			var orchid = me.FindItem("item_orchid");
			if (orchid != null && orchid.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				orchid.UseAbility(x);
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;

		}

		public static bool qquseSheep(Hero x)
		{
			var sheepstick = me.FindItem("item_sheepstick");
			if (sheepstick != null && sheepstick.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				sheepstick.UseAbility(x);
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qquseEulEnem(Hero x)
		{

			var cyclone = me.FindItem("item_cyclone");
			if (cyclone != null && cyclone.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				cyclone.UseAbility(x);
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qquseBloodstone(Hero x)
		{


			var bloodstone = me.FindItem("item_bloodstone");
			if (bloodstone != null && bloodstone.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				bloodstone.UseAbility(x);
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qquseEulMe(Hero x)
		{

			var cyclone = me.FindItem("item_cyclone");
			if (cyclone != null && cyclone.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				cyclone.UseAbility(x);
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}


		public static bool qquseGhost()
		{
			var ghost = me.FindItem("item_ghost");
			if (ghost != null && ghost.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				ghost.UseAbility();
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qquseBlinkHome()
		{

			{
				List<Unit> Fountain = ObjectManager.GetEntities<Unit>().Where(f => (f.ClassID == ClassID.CDOTA_Unit_Fountain) && f.Team == me.Team).ToList();
				var blink = me.FindItem("item_blink") ?? me.FindSpell("antimage_blink") ?? me.FindSpell("queenofpain_blink");
				if (blink != null && blink.CanBeCasted() &&
					Utils.SleepCheck("cast"))
				{
					blink.UseAbility(Fountain.FirstOrDefault());
					Utils.Sleep(1000, "cast");
					return true;
				}
				return false;
			}

		}

		public static bool qquseBlinkAnal()
		{
			List<Unit> Fountain = ObjectManager.GetEntities<Unit>().Where(f => (f.ClassID == ClassID.CDOTA_Unit_Fountain) && f.Team != me.Team).ToList();
			var blink = me.FindItem("item_blink") ?? me.FindSpell("antimage_blink") ?? me.FindSpell("queenofpain_blink");
			if (blink != null && blink.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				blink.UseAbility(Fountain.FirstOrDefault());
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qquseBlinkTarget(Hero x)
		{
			var blink = me.FindItem("item_blink") ?? me.FindSpell("antimage_blink") ?? me.FindSpell("queenofpain_blink");
			if (blink != null && blink.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				blink.UseAbility(x.Position);
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qquseBladeMail()
		{
			var mail = me.FindItem("item_blade_mail");
			if (mail != null && mail.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				mail.UseAbility();
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qquseCrimson()
		{
			var crimson = me.FindItem("item_crimson_guard");
			if (crimson != null && crimson.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				crimson.UseAbility();
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;

		}

		public static bool qqusePipe()
		{
			var pipe = me.FindItem("item_pipe");
			if (pipe != null && pipe.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				pipe.UseAbility();
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;

		}

		public static bool qquseStaffMe(Hero x)
		{
			var force = me.FindItem("item_force_staff");
			if (force != null && force.CanBeCasted())
			{
				force.UseAbility(x);
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;

		}

		public static bool qquseStuffTarget(Hero x)
		{
			var force = me.FindItem("item_force_staff");
			if (force != null && force.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				force.UseAbility(x);
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qquseShiva()
		{
			var shivas = me.FindItem("item_shivas_guard");
			if (shivas != null && shivas.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				shivas.UseAbility();
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qquseSilence(Hero x)
		{
			var silence = me.FindSpell("silencer_last_word") ?? me.FindSpell("death_prophet_silence") ?? me.FindSpell("drow_ranger_silence") ?? me.FindSpell("skywrath_mage_ancient_seal") ?? me.FindItem("item_orchid");

			if (silence != null && silence.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				silence.UseAbility(x);
				silence.UseAbility(x.Position);
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qquseMagnet(Hero x)
		{
			var magnetic = me.FindSpell("arc_warden_magnetic_field");
			if (magnetic != null && magnetic.CanBeCasted() && !me.IsMagicImmune() &&
				Utils.SleepCheck("cast"))
			{
				magnetic.UseAbility(x.Position);
				Utils.Sleep(500, "cast");
				return true;
			}
			return false;
		}
		public static bool qquseColba(Hero x)
		{
			var frostbite = me.FindSpell("crystal_maiden_frostbite");
			if (frostbite != null && frostbite.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				frostbite.UseAbility(x);
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qqoracleFalsePromise(Hero x)
		{
			var promise = me.FindSpell("oracle_false_promise");
			if (promise != null && promise.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				promise.UseAbility(x);
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qqoracleFateTarget(Hero x)
		{
			var fate = me.FindSpell("oracle_fates_edict");
			if (fate != null && fate.CanBeCasted() && me.Distance2D(x) <= 700 &&
				Utils.SleepCheck("cast"))
			{
				fate.UseAbility(x);
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qqoracleFateEdict(Hero x)
		{
			var fates = me.FindSpell("oracle_fates_edict");
			if (fates != null && fates.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				fates.UseAbility(x);
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qqNyx()
		{
			var carapace = me.FindSpell("nyx_assassin_spiked_carapace");
			var vendetta = me.Modifiers.All(y => y.Name == "nyx_assassin_vendetta");
			if (carapace != null && carapace.CanBeCasted() && !vendetta && !me.IsInvisible() &&
				Utils.SleepCheck("cast"))
			{
				carapace.UseAbility();
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qqsilencerUlt()
		{
			var silence = me.FindSpell("silencer_global_silence");
			if (silence != null && silence.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				silence.UseAbility();
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qqsilencerLastWord(Hero x)
		{
			var word = me.FindSpell("silencer_last_word");
			if (word != null && word.CanBeCasted() && me.Distance2D(x) <= 920 &&
				Utils.SleepCheck("cast"))
			{
				word.UseAbility(x);
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qqabaddonUlt()
		{
			var borrowed = me.FindSpell("abaddon_borrowed_time");
			if (borrowed != null && borrowed.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				borrowed.UseAbility();
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qqomniknightRepelMe(Hero x)
		{
			var omniknight = me.FindSpell("omniknight_repel");
			if (omniknight != null && omniknight.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				omniknight.UseAbility(x);
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qquseSDisription(Hero x)
		{
			var disruption = me.FindSpell("shadow_demon_disruption");
			if (disruption != null && disruption.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				disruption.UseAbility(x);
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

	

		public static bool qqodImprisomentMe(Hero x)
		{
			var astral = me.FindSpell("obsidian_destroyer_astral_imprisonment");
			if (astral != null && astral.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				astral.UseAbility(x);
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qqslarkShadowDance()
		{
			var dance = me.FindSpell("slark_shadow_dance");
			if (dance != null && dance.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				dance.UseAbility();
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qqslarkPounce()
		{

			var pounce = me.FindSpell("slark_pounce") ?? me.FindSpell("mirana_leap");
			if (pounce != null && pounce.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				pounce.UseAbility();
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qqslarkDarkPact()
		{
			var dark = me.FindSpell("slark_dark_pact");
			if (dark != null && dark.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				dark.UseAbility();
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qqslardarCrush()
		{
			var crush = me.FindSpell("slardar_slithereen_crush");
			if (crush != null && crush.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				crush.UseAbility();
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qqpuckW()
		{
			var ift = me.FindSpell("puck_waning_rift");
			if (ift != null && ift.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				ift.UseAbility(me);
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qqPuckShift()
		{
			var shift = me.FindSpell("puck_phase_shift");

			if (shift != null && shift.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				shift.UseAbility();
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qqaxeCall()
		{
			var call = me.FindSpell("axe_berserkers_call");
			if (call != null && call.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				call.UseAbility();
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qqlegionPress()
		{
			var press_the_attack = me.FindSpell("legion_commander_press_the_attack");
			if (press_the_attack != null && press_the_attack.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				press_the_attack.UseAbility(me);
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qqabadonWme()
		{
			var shield = me.FindSpell("abaddon_aphotic_shield");
			if (shield != null && shield.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				shield.UseAbility(me);
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qqblinkHomeHero()
		{
			var Home = ObjectManager.GetEntities<Unit>().Where(x => (x.ClassID == ClassID.CDOTA_Unit_Fountain)
						&& x.Team == me.Team).FirstOrDefault();
			var blink = me.FindItem("item_blink");
			if (blink != null && blink.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				blink.UseAbility(Home.Position);
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qqStormUltMe()
		{
			var lightning = me.FindSpell("storm_spirit_ball_lightning");
			if (lightning != null && lightning.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				lightning.UseAbility(me.Position);
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		/*public static bool StormUlPos()
		{
			var lightning = me.FindSpell("storm_spirit_ball_lightning");
				if (lightning != null && lightning.CanBeCasted() &&
					Utils.SleepCheck("cast"))
				{
					lightning.UseAbility(me.Position);
					Utils.Sleep(1000, "cast");
				}
				return;

		}*/

		public static bool qqStormUltTarget(Hero x)
		{
			var lightning = me.FindSpell("storm_spirit_ball_lightning");
			if (lightning != null && lightning.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				lightning.UseAbility(x.Position);
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qqEmberFastRemn(Hero x)
		{
			var remnant = me.FindSpell("ember_spirit_activate_fire_remnant");
			if (remnant != null && remnant.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				remnant.UseAbility(x.Position);
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qqemberE()
		{
			var guard = me.FindSpell("ember_spirit_flame_guard");
			if (guard != null && guard.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				guard.UseAbility();
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		/*public static bool qqEmberWaNal()
			{

				{
					var purge = me.FindItem("item_diffusal_blade") ?? me.FindItem("item_diffusal_blade_2") ?? me.FindItem("item_smoke_of_deceit") ?? me.FindSpell("satyr_trickster_purge");
					if (purge != null && purge.CanBeCasted() &&
						Utils.SleepCheck("cast"))
					{
						purge.UseAbility(me);
						Utils.Sleep(1000, "cast");
					}
				}return;
				ember_spirit_sleight_of_fist
			}*/
		public static bool qqEmberWTarget(Hero x)
		{
			var sleight = me.FindSpell("ember_spirit_sleight_of_fist");
			if (sleight != null && sleight.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				sleight.UseAbility(x.Position);
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qqemberhains()
		{
			var chains = me.FindSpell("ember_spirit_searing_chains");
			if (chains != null && chains.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				chains.UseAbility();
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qqskySilence(Hero x)
		{
			var seal = me.FindSpell("skywrath_mage_ancient_seal");
			if (seal != null && seal.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				seal.UseAbility(x);
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qqDoom(Hero x)
		{
			var doom = me.FindSpell("doombringer_doom");
			if (doom != null && doom.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				doom.UseAbility(x);
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qqjugerOmniTarget(Hero x)
		{
			var omnislash = me.FindSpell("juggernaut_omni_slash");
			if (omnislash != null && omnislash.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				omnislash.UseAbility(x);
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qqtuskSnowBallTarget(Hero x)
		{
			var snowball = me.FindSpell("tusk_snowball");
			var ModifW = me.Modifiers.Any(z => z.Name == "modifier_tusk_snowball_movement");

			var teamarm = ObjectManager.GetEntities<Hero>().Where(ally =>
						 ally.Team == me.Team && ally.IsAlive && me.Distance2D(ally) <= 400
						 && !ally.Modifiers.Any(z => z.Name == "modifier_tusk_snowball_movement_friendly")).ToList();
			if (snowball != null && snowball.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				snowball.UseAbility(x);
				Utils.Sleep(1000, "cast");
			}

			if (ModifW)
			{
				for (int v = 0; v < teamarm.Count(); v++)
				{
					if (ModifW && teamarm[v].Distance2D(me) < 350
						&& !teamarm[v].Modifiers.Any(z => z.Name == "modifier_tusk_snowball_movement_friendly")
						&& !teamarm[v].IsInvul()
						&& !teamarm[v].IsAttackImmune()
						&& teamarm[v].IsAlive
						&& Utils.SleepCheck(teamarm[v].Handle + "w"))
					{
						me.Attack(teamarm[v]);
						Utils.Sleep(50, teamarm[v].Handle + "w");
						break; //TODO:Проверить работу.
					}
				}

				return true;
			}
			return false;
		}

		public static bool qqNagaEnsnare(Hero x)
		{
			var ensnare = me.FindSpell("naga_siren_ensnare");
			if (ensnare != null && ensnare.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				ensnare.UseAbility(x);
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qqAlchemistRage()
		{
			var rage = me.FindSpell("alchemist_chemical_rage");
			if (rage != null && rage.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				rage.UseAbility();
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qqBountyhunterWindwalk()
		{
			var hunterwalk = me.FindSpell("bounty_hunter_wind_walk");
			if (hunterwalk != null && hunterwalk.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				hunterwalk.UseAbility();
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qqPLDoppleganger()
		{
			var doppelwalk = me.FindSpell("phantom_lancer_doppelwalk");
			if (doppelwalk != null && doppelwalk.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				doppelwalk.UseAbility(me.Position);
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qqClinkzWindwalk()
		{
			var skeleton = me.FindSpell("clinkz_skeleton_walk");
			if (skeleton != null && skeleton.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				skeleton.UseAbility();
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qqSandkinSandstorm()
		{
			var sandstorm = me.FindSpell("sandking_sandstorm");
			if (sandstorm != null && sandstorm.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				sandstorm.UseAbility();
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qqWeaverShukuchi()
		{
			var shukuchi = me.FindSpell("weaver_shukuchi");
			if (shukuchi != null && shukuchi.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				shukuchi.UseAbility();
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qqNyxVendetta()
		{
			var vendetta = me.FindSpell("nyx_assassin_vendetta");
			if (vendetta != null && vendetta.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				vendetta.UseAbility();
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}
		public static bool qqTemplarRefraction()
		{
			var refraction = me.FindSpell("templar_assassin_refraction");
			if (refraction != null && refraction.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				refraction.UseAbility();
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qqTemplarMeld()
		{
			var meld = me.FindSpell("templar_assassin_meld");
			if (meld != null && meld.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				meld.UseAbility();
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qqDamageNull()
		{

			var link = me.FindSpell("windrunner_windrun");
			if (link != null && link.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				link.UseAbility();
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}
		public static bool qqDamageNullTarget(Hero x)
		{

			var link = me.FindSpell("razor_static_link") ?? me.FindSpell("brewmaster_drunken_haze");
			if (link != null && link.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				link.UseAbility(x);
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}
		public static bool qqDamageNullMe(Hero x)
		{

			var link = me.FindSpell("lich_frost_armor");
			if (link != null && link.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				link.UseAbility(x);
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qqPhoenixsupernova()
		{
			var nova = me.FindSpell("phoenix_supernova");
			if (nova != null && nova.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				nova.UseAbility();
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;

		}

		public static bool qqJuggernautfury()
		{
			var fury = me.FindSpell("juggernaut_blade_fury");
			if (fury != null && fury.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				fury.UseAbility();
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qqLoneDruidUlt()
		{
			var form = me.FindSpell("lone_druid_true_form");
			var formD = me.FindSpell("lone_druid_true_form_druid");
			if (form != null && form.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				form.UseAbility();
				Utils.Sleep(1000, "cast");
			}
			else if (formD != null && formD.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				formD.UseAbility();
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qqLifestealerrage()
		{
			var rage = me.FindSpell("life_stealer_rage");
			if (rage != null && rage.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				rage.UseAbility();
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qqallHex(Hero x)
		{
			var Hex = me.FindItem("item_sheepstick") ?? me.FindSpell("lion_voodoo") ?? me.FindSpell("shadow_shaman_voodoo");
			if (Hex != null && Hex.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				Hex.UseAbility(x);
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qqallStun(Hero x)
		{

			var stun = me.FindSpell("skeleton_king_hellfire_blast")
				?? me.FindSpell("sven_storm_bolt")
				?? me.FindSpell("luna_lucent_beam")
				?? me.FindSpell("lion_impale")
				?? me.FindSpell("vengefulspirit_magic_missile")
				?? me.FindSpell("sandking_burrowstrike");
			if (stun != null && stun.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				stun.UseAbility(x);
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}

		public static bool qqallAltStun(Hero x)
		{
			var stun = me.FindSpell("bane_nightmare") ?? me.FindSpell("dragon_knight_dragon_tail") ?? me.FindSpell("crystal_maiden_frostbite") ?? me.FindSpell("shadow_shaman_shackles") ?? me.FindSpell("dazzle_poison_touch") ?? me.FindSpell("ember_spirit_sleight_of_fist") ?? me.FindSpell("enigma_malefice") ?? me.FindSpell("invoker_cold_snap") ?? me.FindSpell("kun_return") ?? me.FindSpell("morphling_adaptive_strike") ?? me.FindSpell("keeper_of_the_light_mana_leak") ?? me.FindSpell("rubick_telekinesis") ?? me.FindSpell("windrunner_shackleshot") ?? me.FindSpell("witch_doctor_paralyzing_cask") ?? me.FindSpell("storm_spirit_electric_vortex");
			if (stun != null && stun.CanBeCasted() &&
				Utils.SleepCheck("cast"))
			{
				stun.UseAbility(x);
				Utils.Sleep(1000, "cast");
				return true;
			}
			return false;
		}


		static void CurrentDomain_DomainUnload(object sender, EventArgs e)
		{
			txt.Dispose();
			not.Dispose();
		}

		static void Drawing_OnEndScene(EventArgs args)
		{
			if (Drawing.Direct3DDevice9 == null || Drawing.Direct3DDevice9.IsDisposed || !Game.IsInGame)
				return;

			var player = ObjectManager.LocalPlayer;
			var me = ObjectManager.LocalHero;
			if (player == null || player.Team == Team.Observer)
				return;

			if (menu.Item("dodge").IsActive())
			{
				txt.DrawText(null, "Auto Dodge Spell's: On!", 1200, 40, Color.Coral);
			}

			if (!menu.Item("dodge").IsActive())
			{
				txt.DrawText(null, "Auto Dodge Spell's: Off!", 1200, 40, Color.Orchid);
			}
		}

		static void Drawing_OnPostReset(EventArgs args)
		{
			txt.OnResetDevice();
			not.OnResetDevice();
		}

		static void Drawing_OnPreReset(EventArgs args)
		{
			txt.OnLostDevice();
			not.OnLostDevice();
		}
	}
}
