using Alkamous.InterfaceForAllClass;
using Alkamous.Model;
using System;
using System.Collections;
using System.Data;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Alkamous.Controller
{
    public class ClsOperationsofBanks : IBaseOperation<CTB_Banks>, IBankAccounts
    {
        private readonly IDataAccessLayer DAL;
        private readonly string ProcedureName = "SP_TB_Bank";

        public ClsOperationsofBanks(IDataAccessLayer _DAL)
        {
            DAL = _DAL;
        }

        private SortedList AssignValuesToSortedList(CTB_Banks item, string @Check)
        {
            SortedList SL = new SortedList
            {
                { "@Check", @Check },
                { "@Bank_Definition", item.Bank_Definition  },
                { "@Bank_Beneficiary_Name ", item.Bank_Beneficiary_Name },
                { "@Bank_Bank_Name", item.Bank_Bank_Name  },
                { "@Bank_Branch", item. Bank_Branch  },
                { "@Bank_Branch_Code", item.Bank_Branch_Code  },
                { "@Bank_Bank_Address", item.Bank_Bank_Address  },
                { "@Bank_Swift_Code ", item.Bank_Swift_Code  },
                { "@Bank_Account_Number", item.Bank_Account_Number},
                { "@Bank_IBAN_Number", item.Bank_IBAN_Number},
                { "@Bank_COUNTRY", item.Bank_COUNTRY  },
                { "@Bank_Account_currency", item.Bank_Account_currency  },
            };
            return SL;
        }
        
        public async Task<bool> AddNewAsync(CTB_Banks item)
        {
            SortedList SL = AssignValuesToSortedList(item, "Add_NewAccount");
            int result = await DAL.RunProcedureasync(ProcedureName, SL);
            return result == 1;
        }

        public async Task<bool> DeleteAsync(string Text)
        {
            SortedList SL = new SortedList
            {
                { "@Check", "Delete_Account" },
                { "@Bank_Definition",Text},
            };
            int result = await DAL.RunProcedureasync(ProcedureName, SL);
            return result == 1;
        }
        
        public async Task<bool> UpdateAsync(CTB_Banks item)
        {
            SortedList SL = AssignValuesToSortedList(item, "Update_Account");
            int result = await DAL.RunProcedureasync(ProcedureName, SL);
            return result == 1;
        }


        public DataTable Get_All()
        {
            SortedList SL = new SortedList
            {
                { "@Check", "Get_ALLAccount" },

            };
            DataTable dt = DAL.SelectDB(ProcedureName, SL);
            return dt;

        }

        public DataTable Get_BySearch(string Text)
        {
            SortedList SL = new SortedList
            {
                { "@Check", "Get_BySearchAccount" },
                { "@Bank_Bank_Name", Text },

            };
            DataTable dt = DAL.SelectDB(ProcedureName, SL);
            return dt;
        }

        public DataTable Get_ByBank_Definition(string Text)
        {
            SortedList SL = new SortedList
            {
                { "@Check", "Get_ByBank_Definition" },
                { "@Bank_Definition", Text },

            };
            DataTable dt = DAL.SelectDB(ProcedureName, SL);
            return dt;
        }

        public bool Check_Bank_DefinitionNotDuplicate(string Text)
        {
            SortedList SL = new SortedList
            {
                { "@Check", "Check_Bank_DefinitionNotDuplicate" },
                { "@Bank_Definition", Text },

            };
            DataTable dt = DAL.SelectDB(ProcedureName, SL);
            int result = dt.Rows.Count;
            return result > 0;

        }

    }
}
