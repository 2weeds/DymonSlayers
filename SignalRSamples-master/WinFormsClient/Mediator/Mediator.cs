using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SgClient1.Classes_Test;

namespace SgClient1.Mediator
{
    class Mediator:IMediator
    {
        public PlayerClass player;
        public Unit unit;
        public Zombie zombie;
        
        public Mediator(PlayerClass player, Zombie zombie)
        {
            this.player = player;
            this.zombie = zombie;
        }

        public Mediator()
        {
        }
        public void Interaction(object obj)
        {
            if (obj is PlayerClass)
            {
                player.TakeDamage(zombie.ZombieDamageToPlayer);
            }   
            else if (obj is Zombie)
            {
                zombie.Health -= player.Weapon.GetWeaponDamage();
            }
        }
        public int Interaction(string type)
        {
            if (type == "ammo"|| type == "healthKit"|| type == "fireWall")
                return 5;
            if (type == "ammo1"|| type == "healthKit1" || type == "fireWall1")
                return 10;
            if (type == "ammo2" || type == "healthKit2" || type == "fireWall2")
                return 15;
            else return 0;
        }
    }
}
