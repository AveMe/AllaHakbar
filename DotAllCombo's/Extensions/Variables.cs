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

	public class Variables
	{
		public static Hero me;
		// Strings
		public static string OnInjectingNotes = "DotAllCombo created by NeverMore running: PRIVATE(Evervolv1337 scelet core)";
		public static string LoadMessage = " > DotAllCombo is now running";
		public static string UnloadMessage = " > DotAllCombo is waiting for the next game to start.";
		public static string HeroName;

		public static string Version = Assembly.GetExecutingAssembly().GetName().Version + " STABLE";
		// Menu
		public static Menu MainMenu = new Menu("DotAllCombo", "Combo", true);
		public static Menu OptionsMenu = new Menu("Options", "options", false, HeroName, HeroName != null);
		// Bools
		public static bool DeveloperMode = true;
		public static bool InGame = false;
		public static bool IsHookedHero = false;
		public static bool IsFoundedHeroClass = false;
		public static bool IsNotified = false;
		// Ints
		public static int IsModuleRunning = 0;

		public class Settings
		{
			//public static string ActivationKey = "EC917249326345";
			public static string User;
		}
	}
}