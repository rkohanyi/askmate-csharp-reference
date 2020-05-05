using System.Collections.Generic;

namespace AskMateWebApp.Models
{
    public class UserListModel
    {
        public List<UserListItemModel> Users { get; set; } = new List<UserListItemModel>();
    }
}
