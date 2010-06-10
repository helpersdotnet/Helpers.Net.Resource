using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Data.SQLite;

namespace Helpers.Net.Resource
{
    class SQLiteResourceHelper
    {
        public static IDictionary GetResources(string virtualPath, string className, string cultureName, bool designMode, IServiceProvider serviceProvider, string connectionString, string dbPrefix)
        {
            if (!string.IsNullOrEmpty(virtualPath))
            {
                return GetLocalResource(virtualPath, cultureName, designMode, serviceProvider,
                                         connectionString, dbPrefix);
            }
            if (!string.IsNullOrEmpty(className))
            {
                return GetGlobalResource(className, cultureName, designMode, serviceProvider, connectionString, dbPrefix);
            }

            // neither virtualPath or className was provided
            // unknown if Local or Global resource

            throw new Exception("virtualPath or className missing from parameters.");
        }

        private static IDictionary GetGlobalResource(string className, string cultureName, bool designMode, IServiceProvider serviceProvider, string connectionString, string dbPrefix)
        {
            throw new NotImplementedException();
        }

        private static ListDictionary GetLocalResource(string virtualPath, string cultureName, bool designMode, IServiceProvider serviceProvider, string connectionString, string dbPrefix)
        {
            throw new NotImplementedException();
        }

        public static void AddResource(string resourceName, string virtualPath, string className, string cultureName, string connectionString, string dbPrefix)
        {
            throw new NotImplementedException();
        }
    }
}