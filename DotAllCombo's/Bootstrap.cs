namespace DotAllCombo
{
    using System;
    using System.Reflection;
    using Extensions;

    using Ensage;

    internal class Bootstrap
    {
        //public static string a2ee688cda9e724885e23cd2cfdee = "392654";

        static void Main(string[] args)
        {
            Game.OnUpdate += Game_OnUpdate;

            DebugExtensions.Print.Encolored(Variables.OnInjectingNotes, ConsoleColor.Cyan);
        }

        public static void Game_OnUpdate(EventArgs args)
        {
            Variables.me = ObjectManager.LocalHero;

            if (!Variables.InGame)
            {
                if (!Game.IsInGame || Variables.me == null)
                    return;
                Variables.InGame = true;
				//Activation.Init();
				Stack.StackInject();
                Dodge.DodgeInject();
				// Loading controller
				DebugExtensions.Print.Success(Variables.LoadMessage);
            }

            if ((!Game.IsInGame || Variables.me == null) && Variables.InGame)
            {
                ForceUnload();
                return;
            }

            Variables.HeroName = Utility.FirstUpper(Utility.GetHeroName(Variables.me.Name)).Replace("_", "");

            Type heroClass = Type.GetType("DotAllCombo.Heroes." + Variables.HeroName + "Controller");
            if (heroClass != null && !Variables.IsFoundedHeroClass && !Variables.IsHookedHero /*&& Activation.IsActivated()*/)
            {
                if (Variables.DeveloperMode)
                    DebugExtensions.Print.Success("DotAllCombo.Heroes." + Variables.HeroName + "Controller] BOOTSTRAP: founded!");

                Variables.IsFoundedHeroClass = true;

                // Invoke hero method.
                try
                {
                    object Instance = Activator.CreateInstance(heroClass);
                    MethodInfo HeroMethod = heroClass.GetMethod("Init");
                    HeroMethod.Invoke(Instance, null);                
                }
                catch
                {
                    ErrorExtensions.ThrowError(ErrorExtensions.Error.BOOTSTRAP_ERROR);
                }
            }
            
            else if (!Variables.IsFoundedHeroClass /*&& !Variables.IsHookedHero*/ && !Variables.IsNotified)
            {
                //if (Activation.IsActivated())
                //{
                    if (Variables.DeveloperMode)
                        DebugExtensions.Print.Error("[DotAllCombo.Heroes." + Variables.HeroName + "Controller] not founded!");
                    ErrorExtensions.ThrowError(ErrorExtensions.Error.NOT_FOUNDED_HERO_CONTROLLER);
                    DebugExtensions.Chat.PrintError("[DotAllCombo] Your hero is not supported!");
                /*}
                else
                {
                    if (Activation.GetActivationState() == 0)
                    {
                        if (Variables.DeveloperMode)
                            DebugExtensions.Print.Error("[DotAllCombo.Activation] Not activated!");
                        ErrorExtensions.ThrowError(ErrorExtensions.Error.NOT_ACTIVATED);
                        DebugExtensions.Chat.PrintError("[DotAllCombo.Activation] Not activated!");
                    }
                    else if (Activation.GetActivationState() == 2)
                    {
                        if (Variables.DeveloperMode)
                            DebugExtensions.Print.Error("[DotAllCombo.Activation] Your key has been banned!");
                        ErrorExtensions.ThrowError(ErrorExtensions.Error.ACTIVATION_KEY_BANNED);
                        DebugExtensions.Chat.PrintError("[DotAllCombo.Activation] Your key has been banned!");
                    }
                }
                */

                Variables.IsNotified = true;
            }
            
        }

        public static void ForceUnload()
        {
            Variables.InGame = false;
            Variables.IsHookedHero = false;
            Variables.IsFoundedHeroClass = false;
            Variables.IsNotified = false;
            Variables.IsModuleRunning = 0;

            AssemblyExtensions.Dispose();

            MenuExtensions.GetMenu().DisplayName = "Unloaded";
            MenuExtensions.DeleteMenu();

            DebugExtensions.Print.Encolored(Variables.UnloadMessage, ConsoleColor.Yellow);
            return;
        }
    }
		
}



