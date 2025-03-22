using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantEducationLogic : BaseLogic<ApplicantEducationPoco>
    {
        public ApplicantEducationLogic(IDataRepository<ApplicantEducationPoco> repository) : base(repository)
        {
        }
        public override void Add(ApplicantEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(ApplicantEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos); 
        }
        protected override void Verify(ApplicantEducationPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();

            foreach (var poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.Major) || poco.Major.Length < 3)
                    exceptions.Add(new ValidationException(107, $"Major for ApplicantEducationLogic {poco.Id} must be at least 3 characters and cannot be null"));

                if (poco.StartDate > DateTime.Today)
                    exceptions.Add(new ValidationException(108, $"StartDate for ApplicantEducationLogic {poco.Id} cannot be greater than today"));

                if (poco.CompletionDate < poco.StartDate)
                    exceptions.Add(new ValidationException(109, $"CompletionDate for ApplicantEducationLogic {poco.Id} cannot be earlier than startDate"));
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
        
    }
}
