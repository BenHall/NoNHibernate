using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using AutoMapper;

namespace NoNHibernate
{
    public class DatabaseQueryExecutor
    {
        public IEnumerable<T> Execute<T>(Query<T> query)
        {
            string conn = "ConnectionString";
            IEnumerable<T> resultCollection;
            var connection = new SqlConnection(conn);
            try
            {
                resultCollection = ExecuteQuery(connection, query);
            }
            finally
            {
                connection.Close();
            }
            return resultCollection;
        }

        private IEnumerable<T> ExecuteQuery<T>(SqlConnection connection, Query<T> query) {
            connection.Open();

            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader sqlDataReader = command.ExecuteReader();

            IEnumerable<T> resultCollection = Mapper.Map<IDataReader, IList<T>>(sqlDataReader);
            sqlDataReader.Close();
            return resultCollection;
        }
    }
}