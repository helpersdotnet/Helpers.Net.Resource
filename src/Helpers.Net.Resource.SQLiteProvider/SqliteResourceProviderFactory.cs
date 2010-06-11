using System.Web.Compilation;

namespace Helpers.Net.Resource
{
    /// <summary>
    /// Resource provider factory for SQLite
    /// </summary>
    public class SQLiteResourceProviderFactory : ResourceProviderFactory
    {
        /// <summary>
        /// Removes SQLite resource cache.
        /// </summary>
        public static void ClearCache()
        {
            SQLiteResourceProvider.ClearCache();
        }

        #region Overrides of ResourceProviderFactory

        /// <summary>
        /// Creates a global resource provider.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Web.Compilation.IResourceProvider"/>.
        /// </returns>
        /// <param name="classKey">The name of the resource class.</param>
        public override IResourceProvider CreateGlobalResourceProvider(string classKey)
        {
            return new SQLiteResourceProvider(null, classKey);
        }

        /// <summary>
        /// Creates a local resource provider. 
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Web.Compilation.IResourceProvider"/>.
        /// </returns>
        /// <param name="virtualPath">The path to a resource file.</param>
        public override IResourceProvider CreateLocalResourceProvider(string virtualPath)
        {
            virtualPath = System.IO.Path.GetFileName(virtualPath);
            return new SQLiteResourceProvider(virtualPath, null);
        }

        #endregion
    }
}
