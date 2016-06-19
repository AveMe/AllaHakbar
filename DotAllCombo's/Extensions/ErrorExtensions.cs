namespace DotAllCombo.Extensions
{
    class ErrorExtensions
    {
        public enum Error
        {
            UNDEFINED_ERROR, 
            NOT_FOUNDED_HERO_CONTROLLER, 
            NOT_FOUNDED_HERO_ON_UPDATE, 
            INVALID_HERO_CONTROLLER,
            BOOTSTRAP_ERROR,
            INJECTION_ERROR,
            HERO_HOOK_ERROR,
            DEBUG_ERROR,
            PRINTING_ERROR,
            MENU_ERROR,
            MENU_REGISTER_ERROR,
            ASSEMBLY_NOT_INITIALIZED,
            ASSEMBLY_INITIALIZED_ALREADY,
            ACTIVATION_ERROR,
            NOT_ACTIVATED,
            ACTIVATION_KEY_BANNED
        }

        public static void ThrowError(Error e, bool isRooted = false)
        {
            if (isRooted)
            {
               DebugExtensions.PrintError(e);
               DebugExtensions.Chat.PrintError("[DotAllCombo] " + e + " #" + (int)e);
            }
            DebugExtensions.PrintError(e);
            if (Variables.DeveloperMode)
               DebugExtensions.Chat.PrintError("[DotAllCombo] " + e + " #" + (int)e);

            return;
        }
    }
}
