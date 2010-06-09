using System.Web.Compilation;

namespace Helpers.Net.Resource
{
    public class SqliteResourceProviderFactory : ResourceProviderFactory
    {
        #region Overrides of ResourceProviderFactory

        /// <summary>
        /// When overridden in a derived class, creates a global resource provider. 
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Web.Compilation.IResourceProvider"/>.
        /// </returns>
        /// <param name="classKey">The name of the resource class.
        ///                 </param>
        public override IResourceProvider CreateGlobalResourceProvider(string classKey)
        {
            return new SqliteResourceProvider(null, classKey);
        }

        /// <summary>
        /// When overridden in a derived class, creates a local resource provider. 
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Web.Compilation.IResourceProvider"/>.
        /// </returns>
        /// <param name="virtualPath">The path to a resource file.
        ///                 </param>
        public override IResourceProvider CreateLocalResourceProvider(string virtualPath)
        {
            virtualPath = System.IO.Path.GetFileName(virtualPath);
            return new SqliteResourceProvider(virtualPath, null);
        }

        #endregion
    }
}
