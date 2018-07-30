using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCodingVine.Model.Attributes
{
    public class ValidDateTimeAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is null)
            {
                return true;
            }
            else if (value is DateTime)
            {
                DateTime todayDate = DateTime.Today;

                if ((DateTime)value <= DateTime.MaxValue && (DateTime)value >= todayDate)
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

