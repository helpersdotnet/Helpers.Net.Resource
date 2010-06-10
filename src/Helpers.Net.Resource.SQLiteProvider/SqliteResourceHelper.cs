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

            //neither virtualPath or className was provided
            //unknown if Local or Global resource

            throw new Exception("virtualPath or className missing from parameters.");
        }

        private static IDictionary GetGlobalResource(string className, string cultureName, bool designMode, IServiceProvider serviceProvider, string connectionString, string dbPrefix)
        {
            using (SQLiteConnection cn = new SQLiteConnection(connectionString))
            {
                SQLiteCommand cmd = new SQLiteCommand(cn);
                if (string.IsNullOrEmpty(cultureName))
                {
                    cmd.CommandText =
                        string.Format(
                            "SELECT ResourceName,ResourceValue FROM {0}Globalization WHERE (CultureName IS NULL OR CultureName='') AND ClassName=@className",
                            dbPrefix);
                    cmd.Parameters.AddWithValue("@className", className);
                }
                else
                {
                    cmd.CommandText =
                       string.Format(
                           "SELECT ResourceName,ResourceValue FROM {0}Globalization WHERE CultureName=@cultureName AND ClassName=@className",
                           dbPrefix);
                    cmd.Parameters.AddWithValue("@cultureName", cultureName);
                    cmd.Parameters.AddWithValue("@className", className);
                }

                ListDictionary resources = new ListDictionary();
                cn.Open();
                IDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                    resources.Add(reader[0], reader[1]);

                return resources;
            }
        }

        private static ListDictionary GetLocalResource(string virtualPath, string cultureName, bool designMode, IServiceProvider serviceProvider, string connectionString, string dbPrefix)
        {
            using (SQLiteConnection cn = new SQLiteConnection(connectionString))
            {
                SQLiteCommand cmd = new SQLiteCommand(cn);
                if (string.IsNullOrEmpty(cultureName))
                {
                    cmd.CommandText =
                        string.Format(
                            "SELECT ResourceName,ResourceValue FROM {0}Localization WHERE (CultureName IS NULL OR CultureName='') AND VirtualPath=@virtualPath",
                            dbPrefix);
                    cmd.Parameters.AddWithValue("@virtualPath", virtualPath);
                }
                else
                {
                    cmd.CommandText =
                       string.Format(
                           "SELECT ResourceName,ResourceValue FROM {0}Localization WHERE CultureName=@cultureName AND VirtualPath=@virtualPath",
                           dbPrefix);
                    cmd.Parameters.AddWithValue("@cultureName", cultureName);
                    cmd.Parameters.AddWithValue("@virtualPath", virtualPath);
                }

                ListDictionary resources = new ListDictionary();
                cn.Open();
                IDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                    resources.Add(reader[0], reader[1]);

                return resources;
            }
        }
    }
}