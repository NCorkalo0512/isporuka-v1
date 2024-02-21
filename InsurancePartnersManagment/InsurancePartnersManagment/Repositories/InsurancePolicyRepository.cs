using Antlr.Runtime;
using Dapper;
using InsurancePartnersManagment.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace InsurancePartnersManagment.Repositories
{
    public class InsurancePolicyRepository : IInsurancePolicyRepository
    {
        private readonly string _connectionString;

        public InsurancePolicyRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        public async Task<InsurancePolicy> createInsurancePolicy(InsurancePolicy insurancePolicy)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    var sql = @"
                INSERT INTO InsurancePolicy (PartnerId, PolicyNumber, Amount) 
                VALUES (@PartnerId, @PolicyNumber, @Amount);
                SELECT CAST(SCOPE_IDENTITY() as int);";

                    var id = await db.QuerySingleAsync<int>(sql, new
                    {
                        PartnerId = insurancePolicy.PartnerId,
                        PolicyNumber = insurancePolicy.PolicyNumber,
                        Amount = insurancePolicy.Amount
                    });

                    insurancePolicy.IdPolicy = id;
                    return insurancePolicy;
                }
            }
            catch (Exception ex)
            {

                throw new Exception($"Error while creating policy: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeletePolicy(int id)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    var sql = "DELETE FROM InsurancePolicy WHERE IdPolicy = @Id;";
                    int affectedRows = await db.ExecuteAsync(sql, new { Id = id });
                    return affectedRows > 0;
                }
            }
            catch (Exception ex)
            {

                throw new Exception($"Error while deleting policy: {ex.Message}", ex);
            }
}
        public async Task<IEnumerable<InsurancePolicy>> GetAllInsurancePolicies()
        {
            try
            {
                using (var db= new SqlConnection(_connectionString))
                {
                    var sql = @"SELECT * FROM InsurancePolicy;";
                    return await db.QueryAsync<InsurancePolicy>(sql);
                }
            }
            catch (Exception ex)
            {

                throw new Exception($"Error: {ex.Message}", ex);
            }

        }

        public async Task<InsurancePolicy> GetInsurancePolicyById(int id)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    var sql = "SELECT * FROM InsurancePolicy WHERE IdPolicy = @Id;";
                    return await db.QueryFirstOrDefaultAsync<InsurancePolicy>(sql, new { Id = id });
                }
            }
            catch (Exception ex)
            {

                throw new Exception($"Error : {ex.Message}", ex);
            }
        }

        public async Task<bool> UpdatePolicy(InsurancePolicy insurancePolicy)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    var sql = @"
                UPDATE InsurancePolicy 
                SET PartnerId = @PartnerId, 
                    PolicyNumber = @PolicyNumber, 
                    Amount = @Amount 
                WHERE IdPolicy = @IdPolicy;";

                    int affectedRows = await db.ExecuteAsync(sql, new
                    {
                        IdPolicy = insurancePolicy.IdPolicy,
                        PartnerId = insurancePolicy.PartnerId,
                        PolicyNumber = insurancePolicy.PolicyNumber,
                        Amount = insurancePolicy.Amount
                    });

                    return affectedRows > 0;
                }
            }
            catch (Exception ex)
            {

                throw new Exception($"Error : {ex.Message}", ex);
            }
        }
    }
}