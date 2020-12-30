using Microsoft.Extensions.Localization;
using Microsoft.Extensions.DependencyInjection;
using RSoft.Framework.Cross.Abstractions;
using RSoft.Framework.Cross.Entities;
using RSoft.Framework.Domain.Contracts;
using RSoft.Framework.Domain.Entities;
using System;

namespace RSoft.Finances.Domain.Entities
{

    public class Account : EntityIdNameAuditBase<Guid, Account>, IEntity, IAuditAuthor<Guid>, IActive
    {

        #region Constructors

        /// <summary>
        /// Create a new Account instance
        /// </summary>
        public Account() : base(Guid.NewGuid(), null)
        {
            Initialize();
        }

        /// <summary>
        /// Create a new Account instance
        /// </summary>
        /// <param name="id">Account id value</param>
        public Account(Guid id) : base(id, null)
        {
            Initialize();
        }

        /// <summary>
        /// Create a new Account instance
        /// </summary>
        /// <param name="id">Account id text</param>
        /// <exception cref="System.ArgumentNullException">The exception that is thrown when a null reference (Nothing in Visual Basic) is passed to a method that does not accept it as a valid argument.</exception>
        /// <exception cref="System.FormatException">The exception that is thrown when the format of an argument is invalid, or when a composite format string is not well formed.</exception>
        /// <exception cref="System.OverflowException">The exception that is thrown when an arithmetic, casting, or conversion operation in a checked context results in an overflow.</exception>
        public Account(string id) : base()
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
        /// Account description
        /// </summary>
        public string Description { get; set; }

        #endregion

        #region Navigation Lazy

        /// <summary>
        /// Category data
        /// </summary>
        public Category Category { get; set; }

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
            IStringLocalizer<Account> localizer = ServiceActivator.GetScope().ServiceProvider.GetService<IStringLocalizer<Account>>();
            if (CreatedAuthor != null) AddNotifications(CreatedAuthor.Notifications);
            if (ChangedAuthor != null) AddNotifications(ChangedAuthor.Notifications);
            AddNotifications(new SimpleStringValidationContract(Name, nameof(Name), true, 3, 50).Contract.Notifications);
            AddNotifications(new SimpleStringValidationContract(Description, nameof(Description), true, 3, 150).Contract.Notifications);
        }

        #endregion

    }
}
