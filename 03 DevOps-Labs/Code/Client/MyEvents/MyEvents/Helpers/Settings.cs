// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace MyEvents.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string isLoggedInKey = "isLoggedIn_key";
        private static readonly bool isloggedInDefault = false;

        private const string userIdKey = "userId_key";
        private static readonly string userIdDefault = string.Empty;

        private const string authTokenKey = "authToken_key";
        private static readonly string authtokenDefault = string.Empty;

        #endregion


        public static bool IsLoggedIn
        {
            get
            {
                return AppSettings.GetValueOrDefault<bool>(isLoggedInKey, isloggedInDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue<bool>(isLoggedInKey, value);
            }
        }

        public static string UserId
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(userIdKey, userIdDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue<string>(userIdKey, value);
            }
        }

        public static string AuthToken
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(authTokenKey, authtokenDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue<string>(authTokenKey, value);
            }
        }

    }
}