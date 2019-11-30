using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gun_Library
{
    public class Mob
    {
        private int maxDamage;
        public int Entity { get; protected set; }
        public int Health { get; protected set; }
        public int InitialHealth { get; protected set; }
        public int MaxDamage
        {
            get
            {
                maxDamage = Health % (InitialHealth / Entity);
                if (maxDamage == 0) return InitialHealth / Entity;
                else return maxDamage;
            }
            set
            {
                maxDamage = value;
            }
        }

        public Mob(int entity, int health)
        {
            Entity = entity;
            Health = health;
            InitialHealth = health;
        }
    }
}
