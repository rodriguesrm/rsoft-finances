using Microsoft.Extensions.Localization;
using Microsoft.Extensions.DependencyInjection;
using RSoft.Framework.Cross.Abstractions;
using RSoft.Framework.Cross.Entities;
using RSoft.Framework.Domain.Contracts;
using RSoft.Framework.Domain.Entities;
using RSoft.Framework.Domain.ValueObjects;
using System;

namespace RSoft.Finances.Domain.Entities
{

    /// <summary>
    /// User of the eco-system applications
    /// </summary>
    public class User : EntityIdAuditBase<Guid, User>, IEntity, IAuditAuthor<Guid>, IActive
    {

        #region Constructors

        /// <summary>
        /// Create a new user instance
        /// </summary>
        public User() : base(Guid.NewGuid())
        {
            Initialize();
        }

        /// <summary>
        /// Create a new user instance
        /// </summary>
        /// <param name="id">User id value</param>
        public User(Guid id) : base(id)
        {
            Initialize();
        }

        /// <summary>
        /// Create a new user instance
        /// </summary>
        /// <param name="id">User id text</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.FormatException"></exception>
        /// <exception cref="System.OverflowException"></exception>
        public User(string id) : base()
        {
            Id = new Guid(id);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Indicate if entity is active
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Document number (withou mask)
        /// </summary>
        public string Document { get; set; }

        /// <summary>
        /// User full name
        /// </summary>
        public Name Name { get; set; }

        /// <summary>
        /// User's date of birth
        /// </summary>
        public DateTime? BornDate { get; set; }

        /// <summary>
        /// User e-mail
        /// </summary>
        public Email Email { get; set; }

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
            IStringLocalizer<User> localizer = ServiceActivator.GetScope().ServiceProvider.GetService<IStringLocalizer<User>>();
            if (CreatedAuthor != null) AddNotifications(CreatedAuthor.Notifications);
            if (ChangedAuthor != null) AddNotifications(ChangedAuthor.Notifications);
            AddNotifications(Name.Notifications);
            AddNotifications(Email.Notifications);
            AddNotifications(new RequiredValidationContract<string>(Email?.Address, $"Email.{nameof(Email.Address)}", localizer["EMAIL_REQUIRED"]).Contract.Notifications);
            AddNotifications(new PastDateValidationContract(BornDate, "Born date", localizer["BORN_DATE_REQUIRED"]).Contract.Notifications);
            AddNotifications(new BrasilianCpfValidationContract(Document, nameof(Document), true).Contract.Notifications);
        }

        #endregion

    }

}
