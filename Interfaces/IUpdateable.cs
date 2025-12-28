using policechase.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace policechase.Interfaces
{

    public interface IUpdatable
    {
        // Method to update the object
        void Update(GameTime gameTime);
    }
}