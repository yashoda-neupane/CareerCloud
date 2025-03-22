using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyJobEducationLogic : BaseLogic<CompanyJobEducationPoco>
    {
        public CompanyJobEducationLogic(IDataRepository<CompanyJobEducationPoco> repository) : base(repository)
        {
        }

        protected override void Verify(CompanyJobEducationPoco[] pocos)
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

                if (string.IsNullOrEmpty(poco.Major))
                {
                    exceptions.Add(new ValidationException(200, "Valid websites must not be empty or null"));
                }
                else if (poco.Major.Length < 2)
                {
                    exceptions.Add(new ValidationException(200, $"Major for CompanyJobEducationLogic {poco.Id} must be at least 2 characters."));
                }

                if (poco.Importance < 0)
                {
                    exceptions.Add(new ValidationException(201, $"Importance for CompanyJobEducationLogic {poco.Id} cannot be less than 0"));
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }

        public override void Add(CompanyJobEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(CompanyJobEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}
