using System.ComponentModel.DataAnnotations;
using PharmaMgmtApi.Helpers;

namespace PharmaMgmtApi.Commons.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class MaxFileSizeAttribute : ValidationAttribute
{
    private readonly int _maxFileSize;

    public MaxFileSizeAttribute(int maxFileSize)
    {
        _maxFileSize = maxFileSize;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is IFormFile file)
        {
            return FileSizeHelper.ByteToMb(file.Length) > _maxFileSize ? new ValidationResult($"Image must be less than {_maxFileSize} MB") : ValidationResult.Success;
        }
        else return new ValidationResult("The file can not be null");
    }
}