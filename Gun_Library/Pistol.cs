using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gun_Library
{
    /// <summary>
    /// Class contains the overrided method SHoot and fields for Pistol charachteristics.
    /// </summary>
    public class Pistol : Gun
    {
        public Pistol(int patrons, int damage)
        {
            Patrons = patrons;
            Damage = damage;
        }
        public bool IsWorking
        {
            get
            {
                if (patrons > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        // Returns the pure damage of the gun.
        public override int Shoot()
        {
            return damage;
        }
    }
}
