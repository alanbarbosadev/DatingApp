using DatingApp.Domain.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Domain.Entities
{
    public class AppUser : Entity
    {
        public string UserName { get; set; } = string.Empty;
    }
}
