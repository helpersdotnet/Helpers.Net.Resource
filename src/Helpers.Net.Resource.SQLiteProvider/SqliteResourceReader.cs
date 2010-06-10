using System.Collections;
using System.Resources;

namespace Helpers.Net.Resource
{
    sealed class SQLiteResourceReader : IResourceReader
    {
        private readonly IDictionary _resources;

        public SQLiteResourceReader(IDictionary resources)
        {
            _resources = resources;
        }

        #region Implementation of IEnumerable

        /// <summary>
        /// Closes the resource reader after releasing any resources associated with it.
        /// </summary>
        public void Close()
        {
        }

        /// <summary>
        /// Returns an <see cref="T:System.Collections.IDictionaryEnumerator"/> of the resources for this reader.
        /// </summary>
        /// <returns>
        /// A dictionary enumerator for the resources for this reader.
        /// </returns>
        public IDictionaryEnumerator GetEnumerator()
        {
            return _resources.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _resources.GetEnumerator();
        }

        #endregion

        #region Implementation of IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
        }

        #endregion
    }
}