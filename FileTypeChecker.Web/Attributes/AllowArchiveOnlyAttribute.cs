﻿namespace FileTypeChecker.Web.Attributes
{
    using FileTypeChecker.Extensions;
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Threading.Tasks;

    public class AllowArchiveOnlyAttribute : FileTypeValidationBaseAttribute
    {
        /// <summary>
        /// Determines whether a specified object is valid. (Overrides <see cref = "ValidationAttribute.IsValid(object)" />)
        /// </summary>
        /// <remarks>
        /// This method returns <c>true</c> if the <paramref name = "value" /> is null.  
        /// It is assumed the <see cref = "RequiredAttribute" /> is used if the value may not be null.
        /// </remarks>
        /// <param name = "value">The object to validate.</param>
        /// <returns><c>true</c> if the value is null or valid, otherwise <c>false</c></returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!(value is IFormFile file))
            {
                return ValidationResult.Success;
            }

            using var stream = new MemoryStream();
            file.CopyTo(stream);

            if (!FileTypeValidator.IsTypeRecognizable(stream))
            {
                return new ValidationResult(this.UnsupportedFileErrorMessage);
            }

            if (!stream.IsArchiveAsync().ConfigureAwait(false).GetAwaiter().GetResult())
            {
                return new ValidationResult(this.ErrorMessage ?? this.InvalidFileTypeErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}