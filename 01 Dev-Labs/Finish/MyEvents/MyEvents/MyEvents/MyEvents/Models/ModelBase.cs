using MyEvents.Common;
using System;

namespace MyEvents.Models
{
    public class ModelBase : ObservableObject
    {
        public ModelBase()
        {
            Id = Guid.NewGuid().ToString();
            CreatedDateTime = DateTime.UtcNow;
        }
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string Id { get; set; }
        /// <summary>
        /// Gets or sets the created date time.
        /// </summary>
        /// <value>The created date time.</value>
        public DateTime CreatedDateTime { get; set; }
    }
}
