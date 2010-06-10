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
        private IDictionary _resourceCache;
        private static object _cultureNeutralKey = new object();
        private readonly string _connectionString;
        private readonly string _dbPrefix;

        public SQLiteResourceProvider(string virtualPath, string className)
        {
            _virtualPath = virtualPath;
            _className = className;
            _connectionString = ConfigurationManager.AppSettings["Helpers.Net.Resource.SQLiteProvider-connectionString"] ?? string.Empty;
            _dbPrefix = ConfigurationManager.AppSettings["Helpers.Net.Resource.SQLiteProvider-dbPrefix"] ?? string.Empty;

            if (string.IsNullOrEmpty(_connectionString))
                throw new ArgumentNullException(
                    "Helpers.Net.Resource.SQLiteProvider-connectionString is not specified in AppSettings.");
        }

        IDictionary GetResourceCache(string cultureName)
        {
            object cultureKey;
            if (string.IsNullOrEmpty(cultureName))
                cultureKey = cultureName;
            else
                cultureKey = _cultureNeutralKey;

            if (_resourceCache == null)
                _resourceCache = new ListDictionary();

            IDictionary resourceDict = _resourceCache[cultureKey] as IDictionary;
            if (resourceDict == null)
            {
                resourceDict = SQLiteResourceHelper.GetResources(_virtualPath, _className, cultureName, false, null,
                                                                 _connectionString, _dbPrefix);
                _resourceCache[cultureKey] = resourceDict;
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
            string cultureName = null;
            if (culture != null)
                cultureName = culture.Name;
            else
                cultureName = CultureInfo.CurrentUICulture.Name;

            object value = GetResourceCache(cultureName)[resourceKey];
            if (value == null)
            { // if resource is missing for current culture, use default
                SQLiteResourceHelper.AddResource(resourceKey, _virtualPath, _className, cultureName, _connectionString,
                                                 _dbPrefix);
            }
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