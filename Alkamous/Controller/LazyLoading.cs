
using Alkamous.InterfaceForAllClass;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alkamous.Controller
{
    public class LazyLoading : ILazyLoading
    {


        // Configuration properties - now instance-based instead of static
        public int Page { get; private set; } = 1;
        public int PageSize { get; set; } = 50;
        public bool IsLoading { get; private set; } = false;
        public bool EndOfData { get; private set; } = false;
        public string TotalCount { get; private set; } = "0";

        private CancellationTokenSource _cancellationTokenSource;


        /// <summary>
        /// Loads the next page of data into a DataGridView with pagination and duplicate prevention.
        /// </summary>
        /// <param name="uniqueIdColumnName">Database column name used as unique identifier</param>
        /// <param name="search">Search term filter (optional)</param>
        /// <param name="isFavorite">Filter for favorite items only</param>
        /// <param name="targetDGV">Target DataGridView to load data into</param>
        /// <param name="GetAllFunc">Function to get all data: (page, pageSize) => DataTable</param>
        /// <param name="GetBySearchFunc">Function to search data: (search, page, pageSize) => DataTable</param>
        /// <param name="GetBySearchFavoriteFunc">Function to search favorites: (search, page, pageSize) => DataTable</param>
        public async Task LoadNextPageAsync(
             string uniqueIdColumnName,
             string search,
             bool isFavorite,
             DataGridView targetDGV,
             Func<int, int, Task<DataTable>> GetAllFunc,
             Func<string, int, int, Task<DataTable>> GetBySearchFunc,
             Func<string, int, int, Task<DataTable>> GetBySearchFavoriteFunc
             
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
                    newData= await GetAllFunc(Page, PageSize);
                }
                else if (!isFavorite)
                {
                    // Apply search with pagination
                    newData= await GetBySearchFunc(search, Page, PageSize);
                }
                else
                {
                    // Apply search + favorite with pagination
                    newData = await GetBySearchFavoriteFunc(search, Page, PageSize);
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
                        string GetuniqueId = newRow[uniqueIdColumnName].ToString();

                        // Skip if we already have this product_Id
                        if (currentData.Select($"{uniqueIdColumnName} = '{GetuniqueId}'").Length == 0)
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



        // Reset and perform a new search
        public async Task<bool> PerformSearchAsync(DataGridView targetDGV)
        {
            Page = 1;
            IsLoading = false;
            EndOfData = false;
            TotalCount = "0";

            if (targetDGV.DataSource != null)
            {
                ((DataTable)targetDGV.DataSource).Clear();
                targetDGV.DataSource = null;
            }

            await Task.CompletedTask; // أو استبدلها بعملية حقيقية لاحقًا
            return true;
        }

        public async Task TxtSearch_Fun(DataGridView targetDGV,Func<Task> CallLoadNextPageAsync)
        {

            _cancellationTokenSource?.Cancel(); // إلغاء المهمة السابقة إن وجدت
            _cancellationTokenSource = new CancellationTokenSource();
            var token = _cancellationTokenSource.Token;

            try
            {
                await Task.Delay(400, token); // انتظار 400 مللي ثانية

                if (!token.IsCancellationRequested)
                {
                    if (await PerformSearchAsync(targetDGV))
                    {
                        // Load first page of new search results
                       await  CallLoadNextPageAsync();
                    }
                }
            }
            catch (TaskCanceledException)
            {
                // تم الإلغاء، لا داعي لشيء هنا غالبًا
            }

        }

        
    }
}

