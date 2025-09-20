#nullable enable
namespace System.TreeMachine.Pro {
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IUserData<TUserData> {

        public TUserData UserData { get; }

    }
}
