using System.Collections.Generic;

namespace MVCTask.Data.Model
{
    public class Company : Entity
    {
        public string Name { get; set; }
        
        public virtual ICollection<User> Users { get; set; }
    }
}