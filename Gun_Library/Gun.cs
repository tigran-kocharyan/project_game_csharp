using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gun_Library
{
    public abstract class Gun
    {
        public static Random random = new Random();
        protected int patrons;
        protected int damage;

        public int Patrons
        {
            get
            {
                return patrons;
            }
            set
            {
                patrons = value;
            }
        }
        public int Damage
        {
            get
            {
                return damage;
            }
            set
            {
                damage = value;
            }
        }

        abstract public int Shoot();
    }
}
