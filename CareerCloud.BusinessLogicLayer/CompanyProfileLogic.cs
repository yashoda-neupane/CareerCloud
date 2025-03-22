using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyProfileLogic : BaseLogic<CompanyProfilePoco>
    {
        public CompanyProfileLogic(IDataRepository<CompanyProfilePoco> repository) : base(repository)
        {
        }

        protected override void Verify(CompanyProfilePoco[] pocos)
        {
            
            List<ValidationException> exceptions = new List<ValidationException>();

            if (pocos == null || pocos.Length == 0)
            {
                throw new ArgumentException("The pocos array cannot be null or empty.");
            }
            foreach (var poco in pocos)
            {
                if (poco == null)
                {
                    throw new ArgumentException("One of the pocos is null.");
                }
               
                if (string.IsNullOrEmpty(poco.CompanyWebsite))
                {
                    exceptions.Add(new ValidationException(600, "Valid websites must not be empty or null"));
                }
                else if (!poco.CompanyWebsite.EndsWith(".ca", StringComparison.OrdinalIgnoreCase) &&
                         !poco.CompanyWebsite.EndsWith(".com", StringComparison.OrdinalIgnoreCase) &&
                         !poco.CompanyWebsite.EndsWith(".biz", StringComparison.OrdinalIgnoreCase))
                {
                    exceptions.Add(new ValidationException(600, "Valid websites must end with the following extensions: .ca, .com, .biz"));
                }

                if (string.IsNullOrEmpty(poco.ContactPhone))
                {
                    exceptions.Add(new ValidationException(601, $"ContactPhone for CompanyProfileLogic {poco.Id} is required"));
                }
                else
                {
                    var phoneComponents = poco.ContactPhone.Split('-');
                    if (phoneComponents.Length != 3 || phoneComponents[0].Length != 3 || phoneComponents[1].Length != 3 || phoneComponents[2].Length != 4)
                    {
                        exceptions.Add(new ValidationException(601, $"ContactPhone for CompanyProfileLogic {poco.Id} must be in the format XXX-XXX-XXXX"));
                    }
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }

        public override void Add(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);

        }

        public override void Update(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}

