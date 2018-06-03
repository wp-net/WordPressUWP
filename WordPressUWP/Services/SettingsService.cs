using System;
using System.Collections.Generic;
using Windows.Storage;
using WordPressUWP.Models;

namespace WordPressUWP.Services
{
    /// <summary>
    /// UWP implementation of settings service using <see cref="ApplicationData">ApplicationData</see>
    /// </summary>
    public class SettingsService
    {
        /// <summary>
        /// Cache for settings that were already accessed
        /// </summary>
        private readonly Dictionary<string, object> _settingCache = new Dictionary<string, object>();

        /// <summary>
        /// Gets a application setting
        /// </summary>
        /// <typeparam name="T">Type of the value stored</typeparam>
        /// <param name="key">Key of the setting</param>
        /// <param name="defaultValueBuilder">Builder of the default value</param>
        /// <param name="locality">Setting locality</param>
        /// <param name="forceResetCache">Forces the cache to be reset</param>
        /// <returns></returns>
        public T GetSetting<T>(
            string key,
            Func<T> defaultValueBuilder,
            SettingLocality locality = SettingLocality.Local,
            bool forceResetCache = false)
        {
            object result = null;
            if (forceResetCache || !_settingCache.TryGetValue(key, out result))
            {
                var container = locality == SettingLocality.Roamed ?
                ApplicationData.Current.RoamingSettings :
                ApplicationData.Current.LocalSettings;
                _settingCache[key] = RetrieveSettingFromApplicationData(key, defaultValueBuilder, container);
            }
            return (T)_settingCache[key];
        }

        /// <summary>
        /// Retrieves a setting from application data or 
        /// returns a default based on a given builder Func
        /// </summary>
        /// <typeparam name="T">Type of setting to retrieve</typeparam>        
        /// <param name="key">Key of the setting</param>
        /// <param name="defaultValueBuilder">Returns the default value if the setting is not present</param>
        /// <param name="container">Settings container</param>
        /// <returns></returns>
        private T RetrieveSettingFromApplicationData<T>(string key, Func<T> defaultValueBuilder, ApplicationDataContainer container)
        {
            object result = null;
            if (container.Values.TryGetValue(key, out result))
            {
                //get existing
                try
                {
                    return (T)result;
                }
                catch
                {
                    //invalid value for the given type, remove
                    container.Values.Remove(key);
                }
            }
            return defaultValueBuilder();
        }

        public void SetSetting<T>(string key, T value, SettingLocality locality = SettingLocality.Local)
        {
            var container = locality == SettingLocality.Roamed ? ApplicationData.Current.RoamingSettings : ApplicationData.Current.LocalSettings;
            container.Values[key] = value;
            //ensure cache is updated
            _settingCache.Remove(key);
        }
    }
}
