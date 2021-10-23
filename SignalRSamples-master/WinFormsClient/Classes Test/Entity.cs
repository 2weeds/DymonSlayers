using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SgClient1.Classes_Test
{
    abstract class Entity : GameClass
    {
        public int Health { get; set; }
        public Weapon Weapon { get; set; }
        public object Personality { get; set; }

        public abstract void TakeDamage(int damage);
    }
}
