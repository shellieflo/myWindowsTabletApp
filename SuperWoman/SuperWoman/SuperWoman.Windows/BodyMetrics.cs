using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperWoman
{
    public class BodyMetrics : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public decimal weight;
        public decimal height;
        public decimal age;
        public decimal waist;
        public decimal hips;
        public decimal ratio;
        public decimal bodyMass;
        public decimal digit = 1.2M;
        public decimal digit2 = 0.23M;
        public decimal digit3 = 5.4M;

        public decimal Weight
        {        
            set
            {
                weight = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("BMIresult"));
                }
            }
            get
            {
                return weight;
            }
        }

        public decimal Height
        {
            set
            {
                height = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("BMIresult"));
                }
            }
            get
            {
                return height;
            }
        }

       
        public decimal BMIresult
        {

            set
            {
                bodyMass = Convert.ToInt32(Math.Round(weight,2) / (height * height));
            }
            get
            {
                return bodyMass;
            }
        }

        public decimal Age
        {
            get
            {
                return age;
            }
            set
            {
                age = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("BodyFatresult"));
                }
            }
        }//end 

        public decimal bodyFat=0;
        public decimal fatAge;
        public decimal body;
        public decimal body2;
        public decimal LeanBody=0;

        public decimal BodyFat
        {//adult body fat % = (1.20 × BMI) + (0.23 × Age) − (10.8 × 0(for female,1 for male) − 5.4 
            get
            {
                return bodyFat;
            }
            set
            {
                body = Math.Round(decimal.Multiply(bodyMass, digit));
                fatAge = Math.Round(decimal.Multiply(age, digit2));
                body2 = body + fatAge;
                bodyFat = decimal.Subtract(body, digit3);
            }

        }//end bodyfat method



        public decimal LeanBodyMass
        {//lean body mass = body weight -(body weight x body fat %)
            get
            {
                return LeanBody;
            }
            set
            {
                decimal fat = weight * bodyFat;
                LeanBody = weight - fat;
            }
        }//end leanBodyMass method

        public decimal Waist
        {
            get
            {
                return waist;
            }
            set
            {
                waist = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("HipWaistRatio"));
                }
            }
        }//end 

        public decimal Hips
        {
            get
            {
                return hips;
            }
            set
            {
                hips = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("HipWaistRatio"));
                }
            }
        }//end 


        public decimal HipWaistRatio
        {//ratio is calculated by waist/hips measurement in cms
            get
            {
                return ratio;
            }
            set
            {
                ratio = waist / hips;
            }
        }//end method hipWaist
    }
}


    
