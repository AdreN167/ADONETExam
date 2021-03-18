using Exam.Models;
using System;
using System.Collections.Generic;

namespace Exam.Data
{
    public class DoctorsSpecializationDataAccess : DbDataAccess<DoctorsSpecialization>
    {
        public override ICollection<DoctorsSpecialization> Select()
        {
            var sqlDoctorsSpecializations = $"Select * from DoctorsSpecializations;";

            var specializations = new List<DoctorsSpecialization>();

            var command = factory.CreateCommand();
            command.CommandText = sqlDoctorsSpecializations;
            command.Connection = connection;

            using (var dataReader = command.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    specializations.Add(new DoctorsSpecialization
                    {
                        Id = Guid.Parse(dataReader["Id"].ToString()),
                        Name = dataReader["Name"].ToString(),
                    });
                }
            }

            command.Dispose();

            return specializations;
        }

        public override void Update(DoctorsSpecialization entity) { }

        public override void Delete(DoctorsSpecialization entity) { }

        public override void Insert(DoctorsSpecialization entity) { }
    }
}
