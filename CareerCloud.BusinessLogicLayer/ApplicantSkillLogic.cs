using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantSkillLogic : BaseLogic<ApplicantSkillPoco>
    {
        public ApplicantSkillLogic(IDataRepository<ApplicantSkillPoco> repository) : base(repository)
        {
        }

        protected override void Verify(ApplicantSkillPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (var poco in pocos)
            {
                if (poco.StartMonth > 12)
                    exceptions.Add(new ValidationException(101, $"StartMonth for ApplicantSkillLogic {poco.Id} cannot be greater than 12"));

                if (poco.EndMonth > 12)
                    exceptions.Add(new ValidationException(102, $"EndMonth for ApplicantSkillLogic {poco.Id} cannot be greater than 12"));

                if (poco.StartYear < 1900)
                    exceptions.Add(new ValidationException(103, $"StartYear for ApplicantSkillLogic {poco.Id} cannot be less than 1900"));

                if (poco.EndYear < poco.StartYear)
                    exceptions.Add(new ValidationException(104, $"EndYear for ApplicantSkillLogic {poco.Id} cannot be earlier than StartYear"));
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
         }

        public override void Add(ApplicantSkillPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(ApplicantSkillPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}
