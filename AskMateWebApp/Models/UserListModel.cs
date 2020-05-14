using System.Collections.Generic;

namespace AskMate.Web.Models
{
    public class UserListModel
    {
        public List<UserListItemModel> Users { get; set; } = new List<UserListItemModel>();
    }
}
