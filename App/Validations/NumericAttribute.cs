using System;
using System.ComponentModel.DataAnnotations;

namespace App.Validations;


[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public sealed class NumericAttribute : ValidationAttribute {
    public override bool IsValid(object? value) {
        if (value is null) {
            return true;
        }

        return int.TryParse(value.ToString(), out _);
    }
}
