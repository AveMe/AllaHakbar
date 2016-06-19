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
	public class JungleCamps
	{
		public Unit Unit { get; set; }
		public Vector3 Position { get; set; }
		public Vector3 StackPosition { get; set; }
		public Vector3 WaitPosition { get; set; }
		public int Team { get; set; }
		public int Id { get; set; }
		public bool Farming { get; set; }
		public bool Visible { get; set; }
		public bool Stacking { get; set; }
		public bool Stacked { get; set; }
		public bool Ancients { get; set; }
		public bool Empty { get; set; }
		public int State { get; set; }
		public int AttackTime { get; set; }
		public int Creepscount { get; set; }
		public int Starttime { get; set; }
	}
	public class Stack
	{
		private static Ability Q, W, E, R;
		private static Item Manta;
		private static Hero _me;
		private static bool _stackKey, _load;
		private static readonly Menu Menu = new Menu("Stack Camp's", "Stack Camp's", true);
		private static MenuItem _subMenu1, _subMenu0;
		private static List<JungleCamps> Camps = new List<JungleCamps>();
		private static int _seconds;

		public static void StackInject()
		{
			Camps.Add(new JungleCamps { Position = new Vector3(-3675, 764, 600), StackPosition = new Vector3(-5313, 236, 600), WaitPosition = new Vector3(-3966, 597, 600), Team = 2, Id = 1, Farming = false, Empty = false, Visible = false, Stacking = false, Stacked = false, Starttime = (int)54.1 });

			Camps.Add(new JungleCamps { Position = new Vector3(-4446, 3541, 600), StackPosition = new Vector3(-3953, 4954, 600), WaitPosition = new Vector3(-4251, 3760, 600), Team = 2, Id = 2, Farming = false, Empty = false, Visible = false, Stacking = false, Stacked = false, Starttime = 53 });

			Camps.Add(new JungleCamps { Position = new Vector3(-2981, 4591, 600), StackPosition = new Vector3(-3248, 5993, 600), WaitPosition = new Vector3(-3055, 4837, 600), Team = 2, Id = 4, Farming = false, Empty = false, Visible = false, Stacking = false, Stacked = false, Starttime = 54 });

			Camps.Add(new JungleCamps { Position = new Vector3(-1524, 2641, 256), StackPosition = new Vector3(-1266, 4273, 600), WaitPosition = new Vector3(-1465, 2908, 256), Team = 2, Id = 3, Farming = false, Empty = false, Visible = false, Stacking = false, Stacked = false, Starttime = 53 });

			Camps.Add(new JungleCamps { Position = new Vector3(3016, -4692, 600), StackPosition = new Vector3(4777, -4954, 600), WaitPosition = new Vector3(3074, -4955, 600), Team = 2, Id = 5, Farming = false, Empty = false, Visible = false, Stacking = false, Stacked = false, Starttime = 53 });

			Camps.Add(new JungleCamps { Position = new Vector3(1098, 3338, 600), StackPosition = new Vector3(910, 5003, 600), WaitPosition = new Vector3(975, 3586, 600), Team = 2, Id = 6, Farming = false, Empty = false, Visible = false, Stacking = false, Stacked = false, Starttime = 53 });

			Camps.Add(new JungleCamps { Position = new Vector3(3960, -767, 256), StackPosition = new Vector3(1947, -1068, 256), WaitPosition = new Vector3(3454, -741, 256), Team = 3, Id = 7, Farming = false, Empty = false, Visible = false, Stacking = false, Stacked = false, Starttime = 54 });

			Camps.Add(new JungleCamps { Position = new Vector3(4141, 554, 600), StackPosition = new Vector3(3337, 1638, 256), WaitPosition = new Vector3(3876, 506, 600), Team = 3, Id = 8, Farming = false, Empty = false, Visible = false, Stacking = false, Stacked = false, Starttime = (int)56.42 });

			Camps.Add(new JungleCamps { Position = new Vector3(4474, -3598, 600), StackPosition = new Vector3(2755, -4001, 600), WaitPosition = new Vector3(4121, -3902, 600), Team = 3, Id = 9, Farming = false, Empty = false, Visible = false, Stacking = false, Stacked = false, Starttime = (int)53.5 });

			Camps.Add(new JungleCamps { Position = new Vector3(1656, -3714, 600), StackPosition = new Vector3(1263, -6041, 600), WaitPosition = new Vector3(1612, -4277, 600), Team = 3, Id = 10, Farming = false, Empty = false, Visible = false, Stacking = false, Stacked = false, Starttime = (int)53.99 });

			Camps.Add(new JungleCamps { Position = new Vector3(-392, 3652, 600), StackPosition = new Vector3(-224, 5088, 600), WaitPosition = new Vector3(-503, 3955, 600), Team = 3, Id = 11, Farming = false, Empty = false, Visible = false, Stacking = false, Stacked = false, Starttime = (int)56.6 });

			Camps.Add(new JungleCamps { Position = new Vector3(-1708, -4284, 256), StackPosition = new Vector3(-2776, -3144, 256), WaitPosition = new Vector3(-1971, -3949, 256), Team = 3, Id = 12, Farming = false, Empty = false, Visible = false, Stacking = false, Stacked = false, Starttime = (int)55.5 });

			Camps.Add(new JungleCamps { Position = new Vector3(-266, -3176, 256), StackPosition = new Vector3(-522, -1351, 256), WaitPosition = new Vector3(-325, -2699, 256), Team = 2, Id = 11, Farming = false, Empty = false, Visible = false, Stacking = false, Stacked = false, Starttime = (int)56 });

			Camps.Add(new JungleCamps { Position = new Vector3(-2906, 76, 600), StackPosition = new Vector3(-2699, 1242, 256), WaitPosition = new Vector3(-2494, 178, 600), Team = 3, Id = 12, Farming = false, Empty = false, Visible = false, Stacking = false, Stacked = false, Starttime = 53 });


			Events.OnLoad += Events_OnLoad;
			Events.OnClose += Events_OnClose;
		}
		private static void Events_OnClose(object sender, EventArgs e)
		{
			Game.OnUpdate -= Game_OnUpdate;
			Drawing.OnDraw -= Drawing_OnDraw;
			Game.OnWndProc -= Game_OnWndProc;
			Game.OnUpdate -= Game_Stack;
			_load = false;
		}
		private static void Events_OnLoad(object sender, EventArgs e)
		{
			_load = false;
			Game.OnUpdate += Game_OnUpdate;
			Drawing.OnDraw += Drawing_OnDraw;
			Game.OnWndProc += Game_OnWndProc;
			Game.OnUpdate += Game_Stack;

			Menu.AddItem(new MenuItem("Stack", "Stack Camp's").SetValue(new KeyBind('T', KeyBindType.Toggle)));
			Menu.AddItem(new MenuItem("mepos", "Stack Meepo's Camp's").SetValue(false));
			Menu.AddItem(new MenuItem("mestack", "Stack Me Camp's").SetValue(false));
			Menu.AddToMainMenu();
			OnLoadMessage();
			_me = ObjectManager.LocalHero;
		}
		private static void Game_Stack(EventArgs args)
		{
			if (!Menu.Item("Stack").GetValue<KeyBind>().Active ||
				!Game.IsInGame || _me == null || Game.IsPaused || Game.IsChatOpen
				)
				return;

			if (!Utils.SleepCheck("wait")) return;

			try
			{
				foreach (var camp in Camps)
				{
					if (camp.Unit == null) continue;
					if (camp.Unit.IsAlive) continue;
					camp.Unit = null;
					camp.State = 0;
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("1 " + e);
			}
			foreach (var camp in Camps.Where(camp => camp.Stacked && camp.Unit != null))
			{
				var unit = camp.Unit;
				var time = (int)(camp.Starttime - 9 - (unit.Distance2D(camp.WaitPosition) / unit.MovementSpeed) + Game.Ping / 1000);
				switch (camp.State)
				{
					case 0:
						if (_seconds < time) continue;
						//Game.PrintMessage("case 0", MessageType.LogMessage);
						if (unit.Distance2D(camp.WaitPosition) < 5)
							camp.State = 1;
						else
							unit.Move(camp.WaitPosition);
						Utils.Sleep(500, "wait");
						break;
					case 1:
						if (_seconds < time) continue;
						//Game.PrintMessage("case 1", MessageType.LogMessage);
						var creepscount = CreepCount(unit, 800);
						if (creepscount == 0)
						{
							camp.Empty = true;
							camp.Unit = null;
							camp.State = 0;
							return;
						}
						else if (_seconds >= camp.Starttime - 5)
						{
							var closestNeutral = GetNearestCreepToPull(unit, 800);
							var moveTime = camp.Starttime -
										   (unit.Distance2D(closestNeutral) -
											(closestNeutral.IsRanged
												? Math.Max(closestNeutral.AttackRange, unit.AttackRange) - 100
												: closestNeutral.RingRadius)) / Math.Min(unit.MovementSpeed, closestNeutral.MovementSpeed);
							camp.AttackTime = (int)moveTime;
							camp.State = 2;
						}
						Utils.Sleep(500, "wait");
						break;
					case 2:
						if (_seconds < time) continue;
						//Game.PrintMessage("case 2", MessageType.LogMessage);
						if (_seconds >= camp.AttackTime)
						{
							var closestNeutral = GetNearestCreepToPull(unit, 800);
							unit.Attack(closestNeutral);
							camp.State = 3;
							var tWait =
								(int)(((unit.Distance2D(closestNeutral) -
											(closestNeutral.IsRanged
												? Math.Max(closestNeutral.AttackRange, unit.AttackRange) - 100
												: closestNeutral.RingRadius)) / Math.Max(unit.MovementSpeed, closestNeutral.MovementSpeed)) * 1000 + Game.Ping);
							Utils.Sleep(tWait, "" + unit.Handle);
						}
						break;
					case 3:
						//Game.PrintMessage("case 3", MessageType.LogMessage);
						var closest = GetNearestCreepToPull(unit, 800);
						if (Utils.SleepCheck("" + unit.Handle) || closest.IsMoving)
						{
							unit.Move(camp.StackPosition);
							camp.State = 4;
						}
						break;
					case 4:
						if (_seconds == 0)
							unit.Move(camp.WaitPosition);
						else if (_seconds < 30 && _seconds > 10)
							camp.State = 0;
						Utils.Sleep(1000, "wait");
						break;
					default:
						camp.State = 0;
						break;
				}
			}
		}
		private static void Game_OnUpdate(EventArgs args)
		{
			if (!Game.IsInGame || _me == null || Game.IsPaused || Game.IsChatOpen || !Menu.Item("Stack").GetValue<KeyBind>().Active && Utils.SleepCheck("wait"))
			{
				return;
			}
			_seconds = ((int)Game.GameTime) % 60;
			Q = _me.Spellbook.Spell1;
			W = _me.Spellbook.Spell2;
			E = _me.Spellbook.Spell3;
			R = _me.Spellbook.Spell4;

			Manta = _me.FindItem("item_manta");

			switch (_me.ClassID)
			{
				case ClassID.CDOTA_Unit_Hero_Lycan:
					if (!_load)
					{
						var dict = new Dictionary<string, bool>
						{
							{"lycan_summon_wolves", true}
						};
						_subMenu0 =
							Menu.AddItem(new MenuItem("enabledAbilities", "Autouse? ").SetValue(new AbilityToggler(dict)));
						_load = true;
					}
					var enabledAbilities = _subMenu0.GetValue<AbilityToggler>().IsEnabled("lycan_summon_wolves");
					if (GetFurtherCamp(_me) != null)
					{
						var time =
							(int)
								(GetFurtherCamp(_me).Starttime - (_me.Distance2D(GetFurtherCamp(_me).WaitPosition) / 440) -
								 5 + Game.Ping / 1000);

						if (enabledAbilities && Q.CanBeCasted() && Utils.SleepCheck("Q") && Q != null && time < _seconds)
						{
							Q.UseAbility();
							Utils.Sleep(4000 + Game.Ping, "Q");
						}
					}
					break;
			}
			var baseNpcCreeps = ObjectManager.GetEntities<Unit>().Where(x => x.Team == _me.Team
			&& (
			(x.Handle == _me.Handle && Menu.Item("mestack").IsActive())
		   || (x.ClassID == ClassID.CDOTA_Unit_Hero_Meepo && Menu.Item("mepos").IsActive() && x.Handle != _me.Handle)
		   || x.ClassID == ClassID.CDOTA_BaseNPC_Creep_Neutral
		   || x.ClassID == ClassID.CDOTA_BaseNPC_Tusk_Sigil
		   || x.ClassID == ClassID.CDOTA_BaseNPC_Invoker_Forged_Spirit
		   || x.ClassID == ClassID.CDOTA_BaseNPC_Warlock_Golem
		   || x.ClassID == ClassID.CDOTA_BaseNPC_Creep
		   || x.ClassID == ClassID.CDOTA_Unit_SpiritBear
		   || x.ClassID == ClassID.CDOTA_Unit_VisageFamiliar
		   || x.ClassID == ClassID.CDOTA_Unit_Brewmaster_PrimalEarth
		   || x.ClassID == ClassID.CDOTA_Unit_Brewmaster_PrimalStorm
		   || x.ClassID == ClassID.CDOTA_BaseNPC_Creep_Lane
		   || x.ClassID == ClassID.CDOTA_Unit_Broodmother_Spiderling
		   || x.ClassID == ClassID.CDOTA_Unit_Brewmaster_PrimalFire
		   || x.IsIllusion)
		   && x.IsAlive && x.IsSpawned && x.IsControllable).ToList();
			if (baseNpcCreeps != null)
			{
				UnitsS(baseNpcCreeps);
			}
		}
		private static void Game_OnWndProc(WndEventArgs args)
		{
			if (args.Msg == (ulong)Utils.WindowsMessages.WM_LBUTTONDOWN)
			{
				foreach (var camp in from camp in Camps
									 let position = Drawing.WorldToScreen(camp.Position)
									 where Utils.IsUnderRectangle(Game.MouseScreenPosition, position.X, position.Y, 110, 40)
									 select camp)
				{
					camp.Stacked = (camp.Stacked == true) ? false : true;
					camp.Unit = null;
				}
			}
			//else if(args.Msg == (ulong) Utils.WindowsMessages.WM_RBUTTONDOWN)
			//{
			//    foreach (var camp in from camp in Camps
			//                         let position = Drawing.WorldToScreen(camp.Position)
			//                         where Utils.IsUnderRectangle(Game.MouseScreenPosition, position.X, position.Y, 40, 40)
			//                         select camp)
			//    {
			//        if (!camp.Stacked) return;
			//        camp.Unit = ObjectManager.GetEntities<Unit>().FirstOrDefault(x => x.IsAlive && x.);

			//    }
			//}
		}

		private static void Drawing_OnDraw(EventArgs args)
		{
			//try
			//{
			//    var pos = Drawing.WorldToScreen(Game.MousePosition);
			//    var unit = ObjectManager.GetEntities<Unit>().FirstOrDefault(x => x.IsAlive && x.Distance2D(Game.MousePosition) < 50);
			//    Drawing.DrawText(unit. + "", "", new Vector2(pos.X, pos.Y + 20), new Vector2(40), Color.AliceBlue, FontFlags.Outline);
			//}
			//catch (Exception)
			//{

			//}

			foreach (var camp in Camps)
			{

				var position = Drawing.WorldToScreen(camp.Position);
				var text = "Stack Off";
				var color = Color.Black;
				if (camp.Stacked)
				{
					text = "Stack On";
					color = Color.Coral;
				}

				Drawing.DrawText(text, "", new Vector2(position.X + 8, position.Y - 3), new Vector2(25), color, FontFlags.Outline);
				//Drawing.DrawText(camp.State.ToString(), "", new Vector2(position.X +38, position.Y - 3), new Vector2(40), color, FontFlags.Outline);
			}
		}
		private static void OnLoadMessage()
		{
			Game.PrintMessage("<font face='verdana' color='#00FF00'>Stack Camp's loaded !</font>", MessageType.LogMessage);
		}
		private static JungleCamps GetClosestCamp(Unit h)
		{
			JungleCamps[] closest =
			{
				new JungleCamps {WaitPosition = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue)}
			};

			foreach (var camp in Camps)
			{
				if (camp.Unit != null || camp.Empty || !camp.Stacked) continue;
				if (h.Distance2D(camp.WaitPosition) < h.Distance2D(closest[0].WaitPosition))
					closest[0] = camp;
			}

			if (closest[0].Id == 0)
				return null;

			foreach (var camp in Camps)
			{
				if (camp.Unit != null && camp.Unit.Handle == h.Handle)
				{
					if (h.Distance2D(camp.WaitPosition) - 10 > h.Distance2D(closest[0].WaitPosition) && camp.State < 3)
					{
						camp.Unit = null;
						camp.State = 0;
						return closest[0];
					}
					else
					{
						return null;
					}
				}
			}

			return closest[0];
		}
		private static JungleCamps GetFurtherCamp(Unit h)
		{
			JungleCamps[] further =
			{
				new JungleCamps {WaitPosition = h.Position}
			};
			try
			{
				foreach (var camp in Camps.Where(camp => camp.Unit == null && !camp.Empty && camp.Stacked).Where(camp => h.Distance2D(camp.WaitPosition) > h.Distance2D(further[0].WaitPosition)))
				{
					further[0] = camp;
				}
			}
			catch (Exception)
			{
				Console.WriteLine("3 Error");
			}
			if (further[0].Id == 0)
				return null;


			foreach (var camp in Camps)
			{
				if (camp.Unit != null && camp.Unit.Handle == h.Handle)
				{
					if (h.Distance2D(camp.WaitPosition) + 10 < h.Distance2D(further[0].WaitPosition) && camp.State < 3)
					{
						camp.Unit = null;
						camp.State = 0;
						return further[0];
					}
					else
					{
						return null;
					}
				}
			}
			return further[0];
		}

		private static void UnitsS(List<Unit> h)
		{
			if (h == null) return;
			try
			{
				foreach (var baseNpcCreep in h)
				{
					if (GetClosestCamp(baseNpcCreep) == null) continue;
					foreach (var camp in Camps)
					{
						if (camp.Id != GetClosestCamp(baseNpcCreep).Id) continue;
						camp.State = 0;
						camp.Unit = baseNpcCreep;
					}
				}
			}
			catch (Exception)
			{
				Console.WriteLine("Unit Detected");
			}
		}
		private static int CreepCount(Unit h, float radius)
		{
			return
				ObjectManager.GetEntities<Unit>()
					.Where(x => x.Team == Team.Neutral && x.IsSpawned && x.IsVisible && h.Distance2D(x) <= radius)
					.ToList().Count;
		}
		private static Unit GetNearestCreepToPull(Unit h, float radius)
		{
			var neutrals = ObjectManager.GetEntities<Unit>().Where(x => x.Team == Team.Neutral && x.IsSpawned && x.IsVisible && h.Distance2D(x) <= radius).ToList();
			Unit bestCreep = null;
			var bestDistance = float.MaxValue;
			foreach (var neutral in neutrals.Where(neutral => h.Distance2D(neutral) < bestDistance))
			{
				bestDistance = h.Distance2D(neutral);
				bestCreep = neutral;
			}
			return bestCreep;
		}
		public static void RoundedRectangle(float x, float y, float w, float h, int iSmooth, Color color)
		{
			var pt = new Vector2[4];

			// Get all corners 
			pt[0].X = x + (w - iSmooth);
			pt[0].Y = y + (h - iSmooth);

			pt[1].X = x + iSmooth;
			pt[1].Y = y + (h - iSmooth);

			pt[2].X = x + iSmooth;
			pt[2].Y = y + iSmooth;

			pt[3].X = x + w - iSmooth;
			pt[3].Y = y + iSmooth;

			// Draw cross 
			Drawing.DrawRect(new Vector2(x, y + iSmooth), new Vector2(w, h - (iSmooth * 2)), color);

			Drawing.DrawRect(new Vector2(x + iSmooth, y), new Vector2(w - (iSmooth * 2), h), color);

			float fDegree = 0;

			for (var i = 0; i < 4; i++)
			{
				for (var k = fDegree; k < fDegree + ((Math.PI * 2) / 4f); k += (float)(1 * (Math.PI / 180.0f)))
				{
					// Draw quarter circles on every corner
					Drawing.DrawLine(
						new Vector2(pt[i].X, pt[i].Y),
						new Vector2(pt[i].X + (float)(Math.Cos(k) * iSmooth), pt[i].Y + (float)(Math.Sin(k) * iSmooth)),
						color);
				}

				fDegree += (float)(Math.PI * 2) / 4; // quarter circle offset 
			}
		}
	}
}
