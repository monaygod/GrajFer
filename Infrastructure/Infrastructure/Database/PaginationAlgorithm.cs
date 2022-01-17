using System;

namespace Infrastructure.Database
{
    public class PaginationAlgorithm
    {
        public int Quantity { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int StartItemsIndex { get; private set; }
        public int EndItemsIndex { get; private set; }

        /// <summary>
        ///  The method is responsible for paging the results.
        ///  It returns the starting and ending index object of the
        ///  searched result and the number of all available pages with the values passed to the method.
        /// <param name="quantity">Number of all results to be paged.</param>
        /// <param name="currentPage">Currently selected page. By default '1'.</param>
        /// <param name="pageSize">Sets the number of lines per page. By default '200'.</param>
        /// </summary>
        public PaginationAlgorithm(
            int quantity,
            int currentPage = 1,
            int pageSize = 200)
        {
            if (quantity <= 0)
            {
                throw new ArgumentException("Parameter cannot be less or equal zero", nameof(quantity));
            }

            const int startValue = 1;

            var totalPages = (int)Math.Ceiling(quantity / (decimal)pageSize);

            if (currentPage < 1)
            {
                currentPage = startValue;
            }
            else if (currentPage > totalPages)
            {
                currentPage = totalPages;
            }

            var startItemsIndex = (currentPage - 1) * pageSize;
            var endItemsIndex = Math.Min(startItemsIndex + pageSize - 1, quantity - 1);

            Quantity = quantity;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartItemsIndex = startItemsIndex;
            EndItemsIndex = endItemsIndex;
        }
    }
}