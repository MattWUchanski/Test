using DataAccessNamespace;
using DataWizPro.Models;
using DataWizPro.ProductServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataWizPro.DataServices
{
    public static class QueryDefinitions
    {
        public static readonly string GetSpecificMenagerWithQuery = "SELECT [pi],[imie],[nazwisko],[alias],[L2],[L3],[L4] FROM [net_app].[L2_Menedzer] WHERE pi = @pi";
        public static readonly string GetSpecificMenagerWithQueryPi = "SELECT TOP 1 [pi],[imie],[nazwisko],[alias],[L2],[L3],[L4] FROM [net_app].[L2_Menedzer] WHERE pi = @pi";
    }

    public class AuthorizationService
    {
        private readonly DataAccess _dataAccess;
        private readonly SqlParameterManager _parameterManager;

        public AuthorizationService()
        {
            _parameterManager = new SqlParameterManager();
            AddProductServiceParameters();

            _dataAccess = new DataAccess(_parameterManager);
        }

        private void AddProductServiceParameters()
        {
            var specificMenagerWithQueryParams = new List<ParameterDefinition>
            {
                new ParameterDefinition("@pi", SqlDbType.VarChar),
            };

            _parameterManager.AddParameters(QueryDefinitions.GetSpecificMenagerWithQuery, specificMenagerWithQueryParams);
            _parameterManager.AddParameters(QueryDefinitions.GetSpecificMenagerWithQueryPi, specificMenagerWithQueryParams);
        }

        public List<MenagerAuthorization> GetAllRecordsOfManagerWithQuery(string pi)
        {
            string query = QueryDefinitions.GetSpecificMenagerWithQuery;
            var parameters = new Dictionary<string, object>
            {
                { "@pi", pi }
            };
            return ErrorHandler.ExecuteWithHandling(() =>
            {
                DataTable dataTable = _dataAccess.CallQueryForDt(query, parameters);
                return DataTransformer.ConvertToList<MenagerAuthorization>(dataTable);
            }, new List<MenagerAuthorization>());
        }

        public MenagerAuthorization GetSpecificMenagerWithQueryPi(string pi)
        {
            string query = QueryDefinitions.GetSpecificMenagerWithQueryPi;
            var parameters = new Dictionary<string, object>
            {
                { "@pi", pi }
            };
            return ErrorHandler.ExecuteWithHandling(() =>
            {
                DataTable dataTable = _dataAccess.CallQueryForDt(query, parameters);
                DataRow dataRow = dataTable.Rows[0];
                return DataTransformer.ConvertToClass<MenagerAuthorization>(dataRow);
            }, null);
        }
    }

}
