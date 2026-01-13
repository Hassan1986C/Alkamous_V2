using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Alkamous.InterfaceForAllClass;

namespace Alkamous.Tests
{
    public class MockDataAccessLayer : IDataAccessLayer
    {
        public List<string> CalledProcedures { get; private set; } = new List<string>();
        public List<SortedList> CapturedParameters { get; private set; } = new List<SortedList>();
        public List<List<SortedList>> CapturedBulkParameters { get; private set; } = new List<List<SortedList>>();

        public int ReturnValue { get; set; } = 1;
        public DataTable ReturnDataTable { get; set; } = new DataTable();

        public bool Is_SQLServer_connection_setting_successful(string txtServerName, string txtDataBase, string txtPassword, string txtUserName, string ConnectTimeout)
        {
            return true;
        }

        public bool Is_SQLServer_connection_setting_successful()
        {
            return true;
        }

        public int RunProcedure(string StoredProcedureName, SortedList ParVal)
        {
            CalledProcedures.Add(StoredProcedureName);
            CapturedParameters.Add(ParVal);
            return ReturnValue;
        }

        public int RunProcedureBulk(string StoredProcedureName, List<SortedList> parameterValues)
        {
            CalledProcedures.Add(StoredProcedureName);
            CapturedBulkParameters.Add(parameterValues);
            return ReturnValue;
        }

        public Task<int> RunProcedureasync(string StoredProcedureName, SortedList ParVal)
        {
            CalledProcedures.Add(StoredProcedureName);
            CapturedParameters.Add(ParVal);
            return Task.FromResult(ReturnValue);
        }

        public DataTable SelectDB(string StoredProcedureName, SortedList ParVal)
        {
            CalledProcedures.Add(StoredProcedureName);
            CapturedParameters.Add(ParVal);
            return ReturnDataTable;
        }

        public Task<DataTable> SelectDBasync(string StoredProcedureName, SortedList ParVal)
        {
            CalledProcedures.Add(StoredProcedureName);
            CapturedParameters.Add(ParVal);
            return Task.FromResult(ReturnDataTable);
        }
    }
}
