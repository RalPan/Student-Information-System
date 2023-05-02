using MVC_SIS_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_SIS_UI.Models
{
    public class AddStateVM: IValidatableObject
    {
        public State currentState { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (currentState == null || currentState.StateName == "" || currentState.StateName == null 
                || currentState.StateAbbreviation == "" || currentState.StateAbbreviation == null)
            {
                errors.Add(new ValidationResult("Please enter a state name and it's abbreviation.",
                    new[] { "currentState.StateName" }));
            }

            return errors;
        }
    }
}