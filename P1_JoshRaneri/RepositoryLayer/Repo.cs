using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public class Repo
    {
        private ShopContext _shopContext;
        public Repo(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }
    }
}
