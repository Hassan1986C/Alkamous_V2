//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Controls;
//using System.Windows.Forms;

//namespace Alkamous.Controller
//{
//    internal class Class1
//    {
//        // Define these class-level variables at the top of your form class
//        private int currentPage = 1;
//        private int pageSize = 50; // Consider reducing from 100 to improve performance
//        private bool isLoading = false;
//        private bool endOfData = false;
//        private System.Windows.Forms.Timer _searchTimer;

//        // This method loads the next page of data and appends it to the grid
//        private async Task LoadNextPageAsync()
//        {
//            if (isLoading || endOfData)
//                return;
//            isLoading = true;

//            try
//            {
//                string searchText = TxtSearch.Text.Trim();

//                // Get the next page of data using the current page counter
//                var newData = await LoadData2(currentPage, pageSize, searchText);

//                // Check if we got any data back
//                if (newData == null || newData.Rows.Count == 0)
//                {
//                    endOfData = true;
//                    isLoading = false;
//                    return;
//                }

//                // For first page, just set as DataSource
//                if (DGVProducts.DataSource == null || currentPage == 1)
//                {
//                    DGVProducts.DataSource = newData;
//                }
//                else
//                {
//                    // For subsequent pages, append to existing DataSource
//                    DataTable currentData = (DataTable)DGVProducts.DataSource;

//                    // Check for and exclude any duplicate rows before adding
//                    foreach (DataRow newRow in newData.Rows)
//                    {
//                        // Get the product_Id to check for duplicates
//                        string productId = newRow["product_Id"].ToString();

//                        // Skip if we already have this product_Id
//                        if (currentData.Select($"product_Id = '{productId}'").Length == 0)
//                        {
//                            currentData.ImportRow(newRow);
//                        }
//                    }
//                }

//                // Increment current page for next load
//                currentPage++;
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//            finally
//            {
//                isLoading = false;
//            }
//        }

//        // Reset and perform a new search
//        private async Task PerformSearch()
//        {
//            // Reset pagination state
//            currentPage = 1;
//            endOfData = false;

//            // Clear the DataGridView
//            if (DGVProducts.DataSource != null)
//            {
//                ((DataTable)DGVProducts.DataSource).Clear();
//                DGVProducts.DataSource = null;
//            }

//            // Load first page of new search results
//            await LoadNextPageAsync();
//        }

//        // Improved text search handler with debounce to prevent multiple rapid searches
//        private async void TxtSearch_TextChanged(object sender, EventArgs e)
//        {
//            // Set up debounce timer if not already created
//            if (_searchTimer == null)
//            {
//                _searchTimer = new System.Windows.Forms.Timer();
//                _searchTimer.Interval = 500; // 500ms delay
//                _searchTimer.Tick += async (s, args) => {
//                    _searchTimer.Stop();
//                    await PerformSearch();
//                };
//            }

//            // Restart the timer
//            _searchTimer.Stop();
//            _searchTimer.Start();
//        }

//        // Favorite button handler
//        private async void BtnFavorite_CheckedChanged(object sender, EventArgs e)
//        {
//            BtnFavorite.ForeColor = BtnFavorite.Checked ? Color.Red : Color.Black;

//            // Reset pagination and perform a new search with the favorites filter
//            await PerformSearch();
//        }

//        // Improved scroll handler 
//        private async void DGVProducts_Scroll(object sender, ScrollEventArgs e)
//        {
//            // Only handle vertical scrolling events
//            if (e.ScrollOrientation != ScrollOrientation.VerticalScroll)
//                return;

//            // Don't proceed if already loading or at end of data
//            if (isLoading || endOfData)
//                return;

//            var dgv = (DataGridView)sender;

//            // Calculate how close we are to the bottom - load when within 3 rows
//            int lastVisibleRowIndex = dgv.FirstDisplayedScrollingRowIndex + dgv.DisplayedRowCount(true) - 1;
//            int totalRows = dgv.RowCount;

//            // Load next page when scrolled near the bottom
//            if (totalRows > 0 && lastVisibleRowIndex >= totalRows - 3)
//            {
//                await LoadNextPageAsync();
//            }
//        }

//        // Initial form load handler
//        private async void FormProducts_Load(object sender, EventArgs e)
//        {
//            // Reset pagination state
//            currentPage = 1;
//            endOfData = false;

//            // Load initial data
//            await LoadNextPageAsync();
//        }
//    }
//}
