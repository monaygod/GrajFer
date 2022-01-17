namespace Infrastructure.Database
{
    public static class SqlPaginationWrapper
    {
        /// <summary>
        /// Method that wraps a sql query to a temporary table to extract a designated number of rows , in a standardized way.
        /// The TOP constraint sets the maximum range for the number of rows in the table, speeding up the pagination process significantly.
        /// </summary>
        /// <param name="sqlQuery">A query designed for pagination.</param>
        /// <param name="sqlOrderBy"> Select the column in the table by which you want to sort along with the sorting type.
        /// In the default setting "(select null) asc". This means sorting a view or table randomly based on the result of a query.
        /// This parameter can not remain in its current form, the change is mandatory and its use is only to avoid generating an error.
        /// </param>
        /// <param name="pageNumber">Currently selected page. By default '1'.</param>
        /// <param name="rowsPerPage">Sets the number of lines per page. By default '200'.</param>
        public static string WrapSqlForPaging(string sqlQuery, string sqlOrderBy = "(select null) asc",
            int pageNumber = 1, int rowsPerPage = 200)
        {
            return $"SELECT" +
                   "*" +
                   "FROM (" +
                   " SELECT " +
                   $" TOP ({rowsPerPage} * {pageNumber}) ROW_NUMBER() OVER (ORDER BY {sqlOrderBy}) AS Row# ," +
                   $"{sqlQuery}" +
                   "with (NOLOCK)" +
                   $") x WHERE Row# BETWEEN ({rowsPerPage} * ({pageNumber} - 1) + 1) AND " +
                   $" (({rowsPerPage} * ({pageNumber} - 1) + 1) + {rowsPerPage} -1 )";
        }

        /// <summary>
        /// Method that wraps a sql query to a temporary table to extract a designated number of rows , in a standardized way.
        /// The TOP constraint sets the maximum range for the number of rows in the table, speeding up the pagination process significantly.
        /// </summary>
        /// <param name="sqlQuerySelect">The part of the sql query responsible for the 'Select' to 'From' area. </param>
        /// <param name="sqlQueryFrom">The part of the sql query responsible for the area after the "From" keyword. </param>
        /// <param name="sqlOrderBy"> Select the column in the table by which you want to sort along with the sorting type.
        /// In the default setting "(select null) asc". This means sorting a view or table randomly based on the result of a query.
        /// This parameter can not remain in its current form, the change is mandatory and its use is only to avoid generating an error.
        /// </param>
        /// <param name="pageNumber">Currently selected page. By default '1'.</param>
        /// <param name="rowsPerPage">Sets the number of lines per page. By default '200'.</param>
        public static string WrapSqlForPaging(string sqlQuerySelect, string sqlQueryFrom,
            string sqlOrderBy = "(select null) asc",
            int pageNumber = 1, int rowsPerPage = 200)
        {
            return $"SELECT" +
                   "*" +
                   "FROM (" +
                   " SELECT " +
                   $" TOP ({rowsPerPage} * {pageNumber}) ROW_NUMBER() OVER (ORDER BY {sqlOrderBy}) AS Row# ," +
                   $"{sqlQuerySelect} FROM {sqlQueryFrom}" +
                   $") x WHERE Row# BETWEEN ({rowsPerPage} * ({pageNumber} - 1) + 1) AND " +
                   $" (({rowsPerPage} * ({pageNumber} - 1) + 1) + {rowsPerPage} -1 )";
        }

        /// <summary>
        ///  Returns the number of rows that matches a specified criterion.
        /// </summary>
        /// <param name="sqlQueryFrom">The part of the sql query responsible for the area after the "From" keyword. </param>
        /// <param name="countAlias">Alias to the column that returns the number of query elements 'Count'.</param>
        public static string WrapSqlForCount(string sqlQueryFrom , string countAlias)
        {
            return $"SELECT count(*) AS {countAlias} FROM {sqlQueryFrom}";
        }
    }
}