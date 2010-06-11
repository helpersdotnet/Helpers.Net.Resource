using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.Resources;
using System.Web.Compilation;

namespace Helpers.Net.Resource
{
    sealed class SQLiteResourceProvider : IResourceProvider
    {
        private readonly string _virtualPath;
        private readonly string _className;
        private static IDictionary _resourceCache;
        private readonly string _connectionString;
        private readonly string _dbPrefix;

        public static void ClearCache()
        {
            _resourceCache = null;
        }

        public SQLiteResourceProvider(string virtualPath, string className)
        {
            _virtualPath = virtualPath;
            _className = className;
            string connectionStringName = ConfigurationManager.AppSettings["Helpers.Net.Resource.SQLiteProvider-connectionStringName"] ?? string.Empty;
            _dbPrefix = ConfigurationManager.AppSettings["Helpers.Net.Resource.SQLiteProvider-dbPrefix"] ?? string.Empty;

            if (string.IsNullOrEmpty(connectionStringName))
                throw new ArgumentNullException(
                    "Helpers.Net.Resource.SQLiteProvider-connectionString is not specified in AppSettings.");
            _connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
        }

        IDictionary GetResourceCache(string cultureName)
        {
            if (cultureName == null)
                cultureName = string.Empty;

            if (_resourceCache == null)
                _resourceCache = new ListDictionary();


            string key = cultureName;
            if (!string.IsNullOrEmpty(_virtualPath))
                key = cultureName + _virtualPath;

            IDictionary resourceDict = _resourceCache[key] as IDictionary;
            if (resourceDict == null)
            {
                if (_resourceCache.Contains(key))
                    resourceDict = _resourceCache[key] as IDictionary;
                else
                    resourceDict = SQLiteResourceHelper.GetResources(_virtualPath, _className, cultureName, false, null,
                                                                     _connectionString, _dbPrefix);
                _resourceCache[key] = resourceDict;
            }
            return resourceDict;
        }

        #region Implementation of IResourceProvider

        /// <summary>
        /// Returns a resource object for the key and culture.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Object"/> that contains the resource value for the <paramref name="resourceKey"/> and <paramref name="culture"/>.
        /// </returns>
        /// <param name="resourceKey">The key identifying a particular resource.
        ///                 </param><param name="culture">The culture identifying a localized value for the resource.
        ///                 </param>
        public object GetObject(string resourceKey, CultureInfo culture)
        {
            string cultureName;
            if (culture != null)
                cultureName = culture.Name;
            else
                cultureName = CultureInfo.CurrentUICulture.Name;

            // try to find original: en-US
            object value = GetResourceCache(cultureName)[resourceKey];

            // if can find try to fall back to generic: en
            if (value == null && cultureName.Length > 3)
                value = GetResourceCache(cultureName.Substring(0, 2))[resourceKey];

            // if not find default
            if (value == null)
                value = GetResourceCache(null)[resourceKey];
            return value;
        }

        /// <summary>
        /// Gets an object to read resource values from a source.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Resources.IResourceReader"/> associated with the current resource provider.
        /// </returns>
        public IResourceReader ResourceReader
        {
            get { return new SQLiteResourceReader(GetResourceCache(null)); }
        }

        #endregion
    }
}