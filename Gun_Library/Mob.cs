using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gun_Library
{
    /// <summary>
    /// Class allows to store tha main chars of the mob squad.
    /// </summary>
    public class Mob
    {
        // Fields for the MOB class.
        public int Entity { get; protected set; }
        public int Health { get; protected set; }
        public int InitialAverage { get; protected set; }

        public bool IsDead { get; protected set; }
        public int MaxDamage
        {
            get
            {
                if (Entity == 1) return Health;
                else if (Health % InitialAverage == 0) return InitialAverage;
                else return Health % InitialAverage;
            }
        }

        public Mob(int entity, int health)
        {
            Entity = entity;
            Health = health;
            InitialAverage = health/entity;
        }

        public void ReceiveDamage(int damage)
        {
            if (damage>=MaxDamage)
            {
                Health -= MaxDamage;
                Entity--;
                if (Entity == 0)
                {
                    IsDead = true;
                }
            }
            else
            {
                Health -= damage;
            }
        }
    }
}
