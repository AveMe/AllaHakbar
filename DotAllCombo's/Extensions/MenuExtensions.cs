namespace DotAllCombo.Extensions
{
    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;
    using Ensage.Common.Menu;
    using SharpDX;

    class MenuExtensions
    {
	

		public static void RegisterMenu(string heroname)
        {
            try {
                Variables.OptionsMenu.DisplayName = heroname;

                Variables.MainMenu.AddSubMenu(Variables.OptionsMenu);
                Variables.MainMenu.AddItem(new MenuItem("author.label", heroname + " by " + AssemblyExtensions.GetAuthor(), true).SetFontStyle(
                    System.Drawing.FontStyle.Bold,
                    SharpDX.Color.Coral));
                Variables.MainMenu.AddItem(new MenuItem("version.label", "Version " + Variables.Version).SetFontStyle(
                    System.Drawing.FontStyle.Bold,
                    SharpDX.Color.Coral));

                ReloadMenu();
            }
            catch { ErrorExtensions.ThrowError(ErrorExtensions.Error.MENU_REGISTER_ERROR); }
        }

        public static Menu RegisterModuleMenu(string moduleName, string moduleAuthor)
        {
            Menu ModuleMenu = new Menu(moduleName + " by " + moduleAuthor, moduleName + "." + moduleAuthor);
            try
            {
                Variables.MainMenu.AddSubMenu(ModuleMenu);
                Variables.MainMenu.AddToMainMenu();

                ReloadMenu();
            }
            catch { ErrorExtensions.ThrowError(ErrorExtensions.Error.MENU_REGISTER_ERROR); }
            return ModuleMenu;
        }
		public static Menu GetMenu()
		{
			return Variables.OptionsMenu;
		}

		public static void ReloadMenu()
        {
            Variables.MainMenu.RemoveFromMainMenu();
            Variables.MainMenu.AddToMainMenu();
        }

        public static void DeleteMenu()
        {
            Variables.MainMenu.RemoveSubMenu("options");
            Variables.MainMenu.RemoveFromMainMenu();
        }
    }
}
