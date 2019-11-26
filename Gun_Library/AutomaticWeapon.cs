using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gun_Library
{
    public class AutomaticWeapon : Gun
    {
        // Protected Fields.
        protected double coef;
        // Public Properties.
        public double Coef
        {
            get
            {
                return coef;
            }
            set
            {
                coef = value;
            }
        }
        // Constructor of the AutomaticWeapon Class.
        public AutomaticWeapon(int patrons, int damage, double coef)
        {
            Patrons = patrons;
            Damage = damage;
            Coef = coef;
        }
        //Overrided Shoot method of the AutomaticWeapon Class;
        public override int Shoot()
        {
            double probability = random.NextDouble();
            if(probability<coef)
            {
                return 0;
            }
            else
            {
                return damage;
            }
        }
    }
}
