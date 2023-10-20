using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListCollection.Models
{
    public interface ICRUD<T>
    {
        /// <summary>
        /// Create new
        /// </summary>
        /// <param name="obj">New object to be created</param>
        /// <returns>Returns true if success</returns>
        bool Create(T obj);

        /// <summary>
        /// Get all rows data
        /// </summary>
        /// <returns>Returns all found rows else will return the seeded row</returns>
        List<T> RetrieveAll();

        /// <summary>
        /// Get one row data - Object
        /// </summary>
        /// <param name="key">Primary key value</param>
        /// <returns>Returns row data if found</returns>
        T Retrieve(string key);

        /// <summary>
        /// Edit row data by primary key
        /// </summary>
        /// <param name="obj">Object to be updated</param>
        /// <param name="key">Primary key value</param>
        /// <returns>Returns true if success</returns>
        bool Update(T obj, string key);

        /// <summary>
        /// Delete row data by primary key
        /// </summary>
        /// <param name="key">Primary key value</param>
        /// <returns>Returns true if success</returns>
        bool Delete(string key);
    }
}
