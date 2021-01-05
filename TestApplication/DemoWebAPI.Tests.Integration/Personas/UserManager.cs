using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SpecFlowProject1.Personas
{
    public class UserManager
    {
        public static ReadOnlyDictionary<string, IUser> Users
        {
            get
            {
                return new ReadOnlyDictionary<string, IUser>(UsersInternal);
            }
        }

        internal static Dictionary<string, IUser> UsersInternal { get; set; } = new Dictionary<string, IUser>();

        public static void Clear()
        {
            UsersInternal.Clear();
        }
    }
}
