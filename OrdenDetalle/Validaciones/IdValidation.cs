using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;

namespace OrdenDetalle.Validaciones
{
    public class IdValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if(value != null)
            {
                int id = 0;

                try
                {
                    id = Convert.ToInt32(value);
                }
                catch (Exception)
                {
                    return new ValidationResult(false, "Solo se aceptan entero");
                }

                if(id >= 0)
                {
                    return ValidationResult.ValidResult;
                }
                else
                {
                    return new ValidationResult(false, "Id debe ser Positivo");
                }
            }
                
            return new ValidationResult(false, "Id Vacio");
        }
    }
}
