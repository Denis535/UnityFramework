#nullable enable
namespace System.StateMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IUserData<TUserData> {

        public TUserData UserData { get; }

    }
}
