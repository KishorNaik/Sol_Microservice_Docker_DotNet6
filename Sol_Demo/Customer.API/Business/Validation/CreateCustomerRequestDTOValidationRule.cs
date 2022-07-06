using FluentValidation;

namespace Customer.API.Business.Validation
{
    public class CreateCustomerRequestDTOValidationRule : AbstractValidator<CreateCustomerRequetsDTO>
    {
        public CreateCustomerRequestDTOValidationRule()
        {
            base.RuleFor((model) => model.FullName).NotEmpty();
            base.RuleFor((model) => model.EmailId).EmailAddress();
            base.RuleFor((model) => model.MobileNo).NotEmpty().Must((value) =>
            {
                return value != null;
            });
            base.RuleFor((model) => model.Password).NotEmpty();
        }
    }
}