
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alkamous.Controller
{
    public class LazyLoading
    {


        // Configuration properties - now instance-based instead of static
        public int Page { get; private set; } = 1;
        public int PageSize { get; set; } = 50;
        public bool IsLoading { get; private set; } = false;
        public bool EndOfData { get; private set; } = false;
        public string TotalCount { get; private set; } = "0";

        // Reset pagination state
        public void Reset()
        {
            Page = 1;
            IsLoading = false;
            EndOfData = false;
            TotalCount = "0";
        }
        public async Task LoadNextPageAsyncTEST(
             string product_Id,
             string search,
             bool isFavorite,
             DataGridView targetDGV,
             Func<int, int, Task<DataTable>> GetAllProductsFunc,
             Func<string, int, int, Task<DataTable>> GetProductsBySearchFunc,
             Func<string, int, int, Task<DataTable>> GetProductsBySearchFavoriteFunc

            )
        {
            if (IsLoading || EndOfData) return;
            IsLoading = true;

            try
            {               

                DataTable newData = new DataTable(); // Initialize empty DataTable
                if (string.IsNullOrEmpty(search) && !isFavorite)
                {
                    // No search term - get all products with pagination
                    newData= await GetAllProductsFunc(Page, PageSize);
                }
                else if (!isFavorite)
                {
                    // Apply search with pagination
                    newData= await GetProductsBySearchFunc(search, Page, PageSize);
                }
                else
                {
                    // Apply search + favorite with pagination
                    newData = await GetProductsBySearchFavoriteFunc(search, Page, PageSize);
                }


                // Check if we got any data back
                if (newData == null || newData.Rows.Count == 0)
                {
                    EndOfData = true;
                    IsLoading = false;
                    TotalCount = targetDGV.RowCount.ToString();
                    return;
                }

                TotalCount = newData.Rows[0]["TotalCount"].ToString();
                // For first page, just set as DataSource
                if (targetDGV.DataSource == null || Page == 1)
                {
                    targetDGV.DataSource = newData;
                }
                else
                {
                    // For subsequent pages, append to existing DataSource
                    DataTable currentData = (DataTable)targetDGV.DataSource;

                    // Check for and exclude any duplicate rows before adding
                    foreach (DataRow newRow in newData.Rows)
                    {
                        // Get the product_Id to check for duplicates
                        string productId = newRow[product_Id].ToString();

                        // Skip if we already have this product_Id
                        if (currentData.Select($"product_Id = '{productId}'").Length == 0)
                        {
                            currentData.ImportRow(newRow);
                        }
                    }
                }

                // Increment current page for next load
                Page++;
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally 
            {
                IsLoading = false; 
            }



        }

    }
}










//using System;
//using System.Data;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace Alkamous.Controller
//{
//    /// <summary>
//    /// Handles pagination and lazy loading of data for grid controls
//    /// </summary>
//    public class LazyLoading
//    {
//        // Configuration properties - now instance-based instead of static
//        public int CurrentPage { get; private set; } = 1;
//        public int PageSize { get; set; } = 50;
//        public bool IsLoading { get; private set; } = false;
//        public bool EndOfData { get; private set; } = false;
//        public string TotalCount { get; private set; } = "0";

//        // Reset pagination state
//        public void Reset()
//        {
//            CurrentPage = 1;
//            IsLoading = false;
//            EndOfData = false;
//            TotalCount = "0";
//        }

//        /// <summary>
//        /// Fetches product data based on search criteria and favorite status
//        /// </summary>
//        public async Task<DataTable> GetProductsAsync(
//            string search,
//            bool isFavorite,
//            Func<int, int, Task<DataTable>> getAllProductsFunc,
//            Func<string, int, int, Task<DataTable>> getProductsBySearchFunc,
//            Func<string, int, int, Task<DataTable>> getProductsBySearchFavoriteFunc)
//        {
//            try
//            {
//                // Select the appropriate data retrieval strategy based on criteria
//                if (string.IsNullOrEmpty(search) && !isFavorite)
//                {
//                    return await getAllProductsFunc(CurrentPage, PageSize);
//                }
//                else if (!isFavorite)
//                {
//                    return await getProductsBySearchFunc(search, CurrentPage, PageSize);
//                }
//                else
//                {
//                    return await getProductsBySearchFavoriteFunc(search, CurrentPage, PageSize);
//                }
//            }
//            catch (Exception ex)
//            {
//                // Log the exception or handle it as needed
//                MessageBox.Show($"Error retrieving product data: {ex.Message}",
//                    "Data Retrieval Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return new DataTable(); // Return empty table on error
//            }
//        }

//        /// <summary>
//        /// Loads the next page of data and updates the DataGridView
//        /// </summary>
//        public async Task<bool> LoadNextPageAsync(
//            string productIdColumnName,
//            string search,
//            bool isFavorite,
//            DataGridView targetDGV,
//            Func<int, int, Task<DataTable>> getAllProductsFunc,
//            Func<string, int, int, Task<DataTable>> getProductsBySearchFunc,
//            Func<string, int, int, Task<DataTable>> getProductsBySearchFavoriteFunc)
//        {
//            // Don't proceed if we're already loading or have reached the end of data
//            if (IsLoading || EndOfData) return false;

//            IsLoading = true;

//            try
//            {
//                // Get the next page of data
//                var newData = await GetProductsAsync(
//                    search,
//                    isFavorite,
//                    getAllProductsFunc,
//                    getProductsBySearchFunc,
//                    getProductsBySearchFavoriteFunc
//                );

//                // Check if we received any data
//                if (newData == null || newData.Rows.Count == 0)
//                {
//                    EndOfData = true;
//                    return false;
//                }

//                // Update total count if available in the result set
//                if (newData.Columns.Contains("TotalCount") && newData.Rows.Count > 0)
//                {
//                    TotalCount = newData.Rows[0]["TotalCount"].ToString();
//                }

//                // Handle first page differently than subsequent pages
//                if (targetDGV.DataSource == null || CurrentPage == 1)
//                {
//                    targetDGV.DataSource = newData;
//                }
//                else
//                {
//                    // For subsequent pages, append to existing data without duplicates
//                    MergeDataTables(
//                        (DataTable)targetDGV.DataSource,
//                        newData,
//                        productIdColumnName
//                    );
//                }

//                // Increment page counter for next load
//                CurrentPage++;
//                return true;
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Error loading data: {ex.Message}",
//                    "Pagination Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return false;
//            }
//            finally
//            {
//                IsLoading = false;
//            }
//        }

//        /// <summary>
//        /// Merges new data rows into the target DataTable, avoiding duplicates
//        /// </summary>
//        private void MergeDataTables(DataTable targetTable, DataTable sourceTable, string keyColumnName)
//        {
//            if (targetTable == null || sourceTable == null || !targetTable.Columns.Contains(keyColumnName))
//                return;

//            foreach (DataRow newRow in sourceTable.Rows)
//            {
//                // Skip the TotalCount metadata row if present
//                if (newRow.Table.Columns.Contains("TotalCount"))
//                    continue;

//                // Get the product ID value to check for duplicates
//                string keyValue = newRow[keyColumnName]?.ToString();

//                // Only import the row if it doesn't already exist in the target
//                if (!string.IsNullOrEmpty(keyValue) &&
//                    targetTable.Select($"{keyColumnName} = '{keyValue.Replace("'", "''")}'").Length == 0)
//                {
//                    targetTable.ImportRow(newRow);
//                }
//            }
//        }
//    }
//}