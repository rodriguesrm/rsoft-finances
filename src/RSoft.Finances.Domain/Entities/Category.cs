using Microsoft.Extensions.Localization;
using Microsoft.Extensions.DependencyInjection;
using RSoft.Framework.Cross.Abstractions;
using RSoft.Framework.Cross.Entities;
using RSoft.Framework.Domain.Contracts;
using RSoft.Framework.Domain.Entities;
using System;

namespace RSoft.Finances.Domain.Entities
{

    public class Category : EntityIdNameAuditBase<Guid, Category>, IEntity, IAuditAuthor<Guid>, IActive
    {

        #region Constructors

        /// <summary>
        /// Create a new Category instance
        /// </summary>
        public Category() : base(Guid.NewGuid(), null)
        {
            Initialize();
        }

        /// <summary>
        /// Create a new Category instance
        /// </summary>
        /// <param name="id">Category id value</param>
        public Category(Guid id) : base(id, null)
        {
            Initialize();
        }

        /// <summary>
        /// Create a new Category instance
        /// </summary>
        /// <param name="id">Category id text</param>
        /// <exception cref="System.ArgumentNullException">The exception that is thrown when a null reference (Nothing in Visual Basic) is passed to a method that does not accept it as a valid argument.</exception>
        /// <exception cref="System.FormatException">The exception that is thrown when the format of an argument is invalid, or when a composite format string is not well formed.</exception>
        /// <exception cref="System.OverflowException">The exception that is thrown when an arithmetic, casting, or conversion operation in a checked context results in an overflow.</exception>
        public Category(string id) : base()
        {
            Id = new Guid(id);
            Initialize();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Indicate if entity is active
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Category description
        /// </summary>
        public string Description { get; set; }

        #endregion

        #region Navigation Lazy

        #endregion

        #region Local Methods

        /// <summary>
        /// Iniatialize objects/properties/fields with default values
        /// </summary>
        private void Initialize()
        {
            IsActive = true;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Validate entity
        /// </summary>
        public override void Validate()
        {
            IStringLocalizer<Category> localizer = ServiceActivator.GetScope().ServiceProvider.GetService<IStringLocalizer<Category>>();
            if (CreatedAuthor != null) AddNotifications(CreatedAuthor.Notifications);
            if (ChangedAuthor != null) AddNotifications(ChangedAuthor.Notifications);
            AddNotifications(new SimpleStringValidationContract(Name, nameof(Name), true, 3, 50).Contract.Notifications);
            AddNotifications(new SimpleStringValidationContract(Description, nameof(Description), true, 3, 150).Contract.Notifications);
        }

        #endregion

    }
}
