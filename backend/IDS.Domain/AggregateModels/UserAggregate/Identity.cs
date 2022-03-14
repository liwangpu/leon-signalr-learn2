using Base.Domain;
using Base.Domain.Common;
using System;

namespace IDS.Domain.AggregateModels.UserAggregate
{
    public class Identity : Entity
    {
        public string Id { get; protected set; }
        public string Username { get; protected set; }
        public string Password { get; protected set; }
        public string Name { get; protected set; }
        public string Email { get; protected set; }
        public string Phone { get; protected set; }
        public string Creator { get; protected set; }
        public string Modifier { get; protected set; }
        public long CreatedTime { get; protected set; }
        public long ModifiedTime { get; protected set; }

        #region ctor
        protected Identity()
        { }
        public Identity(string username, string password, string name, string email, string phone, string creator)
        {
            Id = GuidGen.NewGUID();
            Username = username;
            Password = MD5Gen.CalcString(password);
            Name = name;
            Email = email;
            Phone = phone;
            Creator = creator;
            Modifier = Creator;
            CreatedTime = DateTime.UtcNow.ToUnixTimeSeconds();
            ModifiedTime = CreatedTime;
        }
        #endregion

        public void InitializeId(string id)
        {
            Id = id;
        }
    }
}
