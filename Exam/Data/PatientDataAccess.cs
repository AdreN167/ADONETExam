using Exam.Models;
using System;
using System.Collections.Generic;

namespace Exam.Data
{
    public class PatientDataAccess : DbDataAccess<Patient>
    {
        public override void Insert(Patient entity)
        {
            var sqlPatient = $"Insert into Patients(Id, FullName, BirthDate) values (@Id, @FullName, @BirthDate);";

            using (var command = factory.CreateCommand())
            {
                command.CommandText = sqlPatient;
                command.Connection = connection;

                var idParameter = factory.CreateParameter();
                idParameter.DbType = System.Data.DbType.Guid;
                idParameter.Value = entity.Id;
                idParameter.ParameterName = "Id";

                command.Parameters.Add(idParameter);

                var fullNameParameter = factory.CreateParameter();
                fullNameParameter.DbType = System.Data.DbType.String;
                fullNameParameter.Value = entity.FullName;
                fullNameParameter.ParameterName = "FullName";

                command.Parameters.Add(fullNameParameter);

                var birthDateParameter = factory.CreateParameter();
                birthDateParameter.DbType = System.Data.DbType.DateTime2;
                birthDateParameter.Value = entity.BirthDate;
                birthDateParameter.ParameterName = "BirthDate";

                command.Parameters.Add(birthDateParameter);

                command.ExecuteNonQuery();
            }
        }

        public override ICollection<Patient> Select()
        {
            var sqlPatient = $"Select * from Patients;";

            var patients = new List<Patient>();

            var command = factory.CreateCommand();
            command.CommandText = sqlPatient;
            command.Connection = connection;

            using (var dataReader = command.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    patients.Add(new Patient
                    {
                        Id = Guid.Parse(dataReader["Id"].ToString()),
                        FullName = dataReader["FullName"].ToString(),
                        BirthDate = DateTime.Parse(dataReader["BirthDate"].ToString())
                    });
                }
            }

            command.Dispose();

            return patients;
        }

        public override void Update(Patient entity) { }

        public override void Delete(Patient entity) { }
    }
}
