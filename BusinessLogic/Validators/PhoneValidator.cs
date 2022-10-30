using BusinessLogic.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Validators
{
    public class PhoneValidator : FluentValidation.AbstractValidator<PhoneDTO>
    {
        public PhoneValidator()
        {
            RuleFor(x => x.Model)
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0);
            RuleFor(x => x.ImagePath)
                .NotNull()
                .NotEmpty()
                .Must(IsUrl).WithMessage("The property {PropertyName} must be a valid URL address.");
            RuleFor(x => x.Description)
                .MaximumLength(1000);
            RuleFor(x => x.Memory)
                .GreaterThanOrEqualTo(0);
        }

        private static bool IsUrl(string link)
        {
            if (string.IsNullOrWhiteSpace(link))
            {
                return false;
            }

            Uri outUri;
            return Uri.TryCreate(link, UriKind.Absolute, out outUri)
                   && (outUri.Scheme == Uri.UriSchemeHttp || outUri.Scheme == Uri.UriSchemeHttps);
        }
    }
}
