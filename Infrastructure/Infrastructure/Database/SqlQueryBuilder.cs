#nullable enable
using System.Text;

namespace Infrastructure.Database
{
    public class SqlQueryBuilder
    {
        public StringBuilder With { get; private set; }
        public StringBuilder Select { get; private set; }
        public StringBuilder From { get; private set; }
        public StringBuilder? OrderBy { get; private set; }
        public string CountAlias { get; private set; }
        private string orderByDefault = "(select null) asc";
        public int? PageNumber { get; private set; }
        public int RowsPerPage { get; private set; }

        /// <summary>
        /// Creates 'SQL' variables without biasing the query results.
        /// </summary>
        /// <param name="select">The part of the sql query responsible for the 'Select' to 'From' area. </param>
        /// <param name="from">The part of the sql query responsible for the area after the "From" keyword. </param>
        /// <param name="orderBy"> Select the column in the table by which you want to sort along with the sorting type.
        /// In the default setting "(select null) asc". This means sorting a view or table randomly based on the result of a query.
        /// This parameter can not remain in its current form, the change is mandatory and its use is only to avoid generating an error.
        /// </param>
        /// <param name="countAlias">Alias to the column that returns the number of query elements 'Count'.</param>
        public SqlQueryBuilder(StringBuilder with, StringBuilder select, StringBuilder from, StringBuilder orderBy, string countAlias)
        {
            With = with;
            Select = select;
            From = from;
            OrderBy = orderBy;
            CountAlias = countAlias;
        }

        /// <summary>
        /// Creates 'SQL' variables along with paging of query results.
        /// </summary>
        /// <param name="select">The part of the sql query responsible for the 'Select' to 'From' area. </param>
        /// <param name="from">The part of the sql query responsible for the area after the "From" keyword. </param>
        /// <param name="orderBy"> Select the column in the table by which you want to sort along with the sorting type.
        /// In the default setting "(select null) asc". This means sorting a view or table randomly based on the result of a query.
        /// This parameter can not remain in its current form, the change is mandatory and its use is only to avoid generating an error.
        /// </param>
        /// <param name="pageNumber">Currently selected page. By default '1'.</param>
        /// <param name="countAlias">Alias to the column that returns the number of query elements 'Count'.</param>
        /// <param name="rowsPerPage">Sets the number of lines per page. By default '200'.</param>
        public SqlQueryBuilder(StringBuilder with, StringBuilder select, StringBuilder from, StringBuilder? orderBy, int? pageNumber, string countAlias, int rowsPerPage = 500)
        {
            With = with;
            Select = select;
            From = from;
            CountAlias = countAlias;
            RowsPerPage = rowsPerPage;
            PageNumber = pageNumber ?? 1;
            OrderBy = orderBy ?? new StringBuilder(orderByDefault);
        }
        
        /// <summary>
        /// Method that wraps a sql query to a temporary table to extract a designated number of rows , in a standardized way.
        /// The TOP constraint sets the maximum range for the number of rows in the table, speeding up the pagination process significantly.
        /// </summary>
        public string WrapSqlForPaging()
        {
            return $"{With} SELECT" +
                   "*" +
                   "FROM (" +
                   " SELECT " +
                   $" TOP ({RowsPerPage} * {PageNumber}) ROW_NUMBER() OVER (ORDER BY {OrderBy}) AS Row# ," +
                   $"{Select} FROM {From}" +
                   $") x WHERE Row# BETWEEN ({RowsPerPage} * ({PageNumber} - 1) + 1) AND " +
                   $" (({RowsPerPage} * ({PageNumber} - 1) + 1) + {RowsPerPage} -1 )";
        }

        public string WrapSqlForExecute()
        {
            return $"{With} " +
                   $"SELECT {Select} FROM {From} " +
                   $"ORDER BY {OrderBy}";
        }

        /// <summary>
        ///  Returns the number of rows that matches a specified criterion.
        /// </summary>
        public string WrapSqlForCount()
        {
            return $"SELECT count(*) AS {CountAlias} FROM {From}";
        }
        
    }
}