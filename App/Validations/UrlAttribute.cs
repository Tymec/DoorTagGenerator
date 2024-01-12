using System;
using System.ComponentModel.DataAnnotations;

namespace App.Validations;


[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public sealed class UrlAttribute : ValidationAttribute {
    public override bool IsValid(object? value) {
        if (value is null) return true;

        return Uri.TryCreate(value.ToString(), UriKind.Absolute, out var uri) && (
            uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps
        );
    }
}
