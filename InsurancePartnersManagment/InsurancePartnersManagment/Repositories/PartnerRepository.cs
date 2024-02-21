using Dapper;
using InsurancePartnersManagment.Models;
using InsurancePartnersManagment.Models.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace InsurancePartnersManagment.Repositories
{
    public class PartnerRepository : IPartnerRepository
    {
        private readonly string _connectionString;

        public PartnerRepository()
        {
            _connectionString= ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        public async Task<Partner> createPartner(Partner partner)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {                 
                    var existsSql = @"SELECT COUNT(1) FROM Partner WHERE ExternalCode = @ExternalCode;";
                    var exists = await db.ExecuteScalarAsync<bool>(existsSql, new { partner.ExternalCode });
                    if (exists)
                    {
                        throw new Exception($"A partner with the ExternalCode {partner.ExternalCode} already exists.");
                    }

                    var sql = @"
INSERT INTO Partner (FirstName, LastName, Address, PartnerNumber, CroatianPIN, PartnerTypeId, CreateByUser, IsForeign, ExternalCode, Gender)
VALUES (@FirstName, @LastName, @Address, @PartnerNumber, @CroatianPIN, @PartnerTypeId, @CreateByUser, @IsForeign, @ExternalCode, @Gender);
SELECT CAST(SCOPE_IDENTITY() as int);
";
                    
                    int newId = await db.QuerySingleAsync<int>(sql, partner);
                    partner.IdPartner = newId;
                    return partner;
                }
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
               
                throw new Exception($"Error while creating partner: A partner with the same unique key value already exists.", ex);
            }
            catch (Exception ex)
            {
                
                throw new Exception($"Error while creating partner: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeletePartner(int id)
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    var sql = "DELETE FROM Partner WHERE IdPartner = @Id;";
                    int affectedRows = await db.ExecuteAsync(sql, new { Id = id });
                    return affectedRows > 0;
                }
            }
            catch (Exception ex)
            {

                throw new Exception($"Error while deleting partner: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<PartnerListDTO>> GetAllPartners()
        {
            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    var sql = @"
                     SELECT IdPartner, FirstName, LastName, PartnerNumber, PartnerTypeId, IsForeign, Gender, CreatedAtUtc 
                     FROM Partner
                     ORDER BY CreatedAtUtc DESC;"; 
                    var partners = await db.QueryAsync<PartnerListDTO>(sql);
                    return partners;
                }
            }
            catch (Exception ex)
            {

                throw new Exception($"Error while getting all partners: {ex.Message}", ex);
            }
        }

        public async Task<Partner> GetPartnerById(int id)
        {

            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    var sql = "SELECT * FROM Partner WHERE IdPartner = @Id;";
                    return await db.QueryFirstOrDefaultAsync<Partner>(sql, new { Id = id });
                }
            }
            catch (Exception ex)
            {

                throw new Exception($"Error while getting partner: {ex.Message}", ex);
            }
        }

        public async Task<bool> UpdatePartner(Partner partner)
        {

            try
            {
                using (var db = new SqlConnection(_connectionString))
                {
                    var sql = @"
                UPDATE Partner
                SET FirstName = @FirstName, 
                    LastName = @LastName, 
                    Address = @Address, 
                    PartnerNumber = @PartnerNumber, 
                    CroatianPIN = @CroatianPIN, 
                    PartnerTypeId = @PartnerTypeId, 
                    CreateByUser = @CreateByUser, 
                    IsForeign = @IsForeign, 
                    ExternalCode = @ExternalCode, 
                    Gender = @Gender
                WHERE IdPartner = @IdPartner;";
                    int affectedRows = await db.ExecuteAsync(sql, partner);
                    return affectedRows > 0;
                }
            }
            catch (Exception ex)
            {

                throw new Exception($"Error while updating partner: {ex.Message}", ex);
            }
        }
        public async Task<IEnumerable<PartnerPolicyInfoDTO>> GetPartnerPolicyInfo()
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var sql = @"
                    SELECT 
                         Partner.IdPartner,
                         Partner.FirstName,
                            Partner.LastName,
                COUNT(InsurancePolicy.IdPolicy) AS PolicyCount,
             SUM(InsurancePolicy.Amount) AS TotalAmount
                FROM 
    Partner
LEFT JOIN 
    InsurancePolicy ON Partner.IdPartner = InsurancePolicy.PartnerId
GROUP BY 
    Partner.IdPartner, Partner.FirstName, Partner.LastName";

                return await db.QueryAsync<PartnerPolicyInfoDTO>(sql);
            }
        }
    }
}