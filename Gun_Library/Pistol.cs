using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gun_Library
{
    public class Pistol : Gun
    {
        public Pistol(int patrons, int damage)
        {
            Patrons = patrons;
            Damage = damage;
        }
        public override int Shoot()
        {
            return damage;
        }
    }
}
