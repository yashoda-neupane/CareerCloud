﻿using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyJobSkillLogic : BaseLogic<CompanyJobSkillPoco>
    {
        public CompanyJobSkillLogic(IDataRepository<CompanyJobSkillPoco> repository) : base(repository)
        {
        }

        protected override void Verify(CompanyJobSkillPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();

            foreach (var poco in pocos)
            {
                if (poco.Importance < 0)
                {
                    exceptions.Add(new ValidationException(400, $"Importance for CompanyJobSkillLogic {poco.Id} cannot be less than 0"));
                }
                
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }

    public override void Add(CompanyJobSkillPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(CompanyJobSkillPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}