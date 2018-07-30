using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCodingVine.Model.Attributes
{
    public class ValidDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime)
            {
                DateTime todayDate = DateTime.Today;

                int thisDate = todayDate.Day;


                if ((DateTime)value >= todayDate && (DateTime)value <= DateTime.MaxValue)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}
