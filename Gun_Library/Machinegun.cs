using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gun_Library
{
    public class Machinegun : AutomaticWeapon
    {
        protected int lifeTime;
        protected double breaking;
        protected bool isWorking; 

        public int lifeTimeNow { get; protected set; }
        public bool IsBroken { get; protected set; }
        public bool IsWorking
        {
            get
            {
                if (patrons > 0 && !IsBroken)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public int LifeTime
        {
            get
            {
                return lifeTime;
            }
            set
            {
                lifeTime = value;
            }
        }
        public double Breaking
        {
            get
            {
                return breaking;
            }
            set
            {
                breaking = value;
            }
        }

        public Machinegun(int patrons, int damage, double coef, int lifeTime, double breaking) : base(patrons, damage, coef)
        {
            Breaking = breaking;
            lifeTimeNow = lifeTime;
        }
        public override int Shoot()
        {
            if (IsWorking)
            {
                if (lifeTimeNow != 0)
                {
                        lifeTimeNow -= 1;
                }

                int damageNow = base.Shoot();

                if (lifeTimeNow == 0)
                {
                    double breakingProbability = random.NextDouble();
                    if(breakingProbability < breaking)
                    {
                        IsBroken = true;
                        lifeTime = 0;
                    }
                    else
                    {
                        lifeTimeNow = lifeTime;
                    }
                }
                return damageNow;
            }
            else
            {
                return 0;
            }
        }
    }
}
