using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyDescriptionLogic : BaseLogic<CompanyDescriptionPoco>
    {
        public CompanyDescriptionLogic(IDataRepository<CompanyDescriptionPoco> repository) : base(repository)
        {
        }

        protected override void Verify(CompanyDescriptionPoco[] pocos)
        {
            if (pocos == null || pocos.Length == 0)
            {
                throw new ArgumentException("The pocos array cannot be null or empty.");
            }
            List<ValidationException> exceptions = new List<ValidationException>();

            foreach (var poco in pocos)
            {
                if (poco == null)
                {
                    throw new ArgumentException("One of the pocos is null.");
                }
                if (string.IsNullOrEmpty(poco.CompanyDescription) || poco.CompanyDescription.Length < 3)
                {
                    string errorMessage = string.IsNullOrEmpty(poco.CompanyDescription)
                        ? "CompanyDescription must not be empty or null"
                        : $"CompanyDescription for CompanyDescriptionLogic {poco.Id} must be greater than 2 characters.";

                    exceptions.Add(new ValidationException(107, errorMessage));
                }
                if (string.IsNullOrEmpty(poco.CompanyName) || poco.CompanyName.Length < 3)
                {
                    string errorMessage = string.IsNullOrEmpty(poco.CompanyName)
                        ? $"CompanyName for CompanyDescriptionLogic {poco.Id} must not be empty"
                        : $"CompanyName for CompanyDescriptionLogic {poco.Id} must be greater than 2 characters.";

                    exceptions.Add(new ValidationException(106, errorMessage));
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }

        public override void Add(CompanyDescriptionPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(CompanyDescriptionPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}
