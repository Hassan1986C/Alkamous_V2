using Alkamous.InterfaceForAllClass;
using Alkamous.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Alkamous.Controller
{
    // this interface created to apply the Solid principle Design pattern
    public class ClsOperationsofUsers : IBaseOperation<CTB_Users>, IUsers<CTB_Users>
    {
        private readonly IDataAccessLayer DAL;
        private readonly string ProcedureName = "SP_TB_Users";

        public ClsOperationsofUsers(IDataAccessLayer _DAL)
        {
            DAL = _DAL;
        }

        private SortedList AssignValuesToSortedList(CTB_Users item, string @Check)
        {
            SortedList SL = new SortedList
            {
                { "@Check", @Check },
                { "@UserName", item.UserName },
                { "@UserPassword", item.UserPassword  },
                { "@UserAESKey", item.UserAESKey  },
                { "@UserAESIV", item.UserAESIV  },
                { "@UserEmail", item.UserEmail },

            };
            return SL;
        } 
        public async Task<bool> AddNewAsync(CTB_Users item)
        {
            SortedList SL = AssignValuesToSortedList(item, "Add_NewUser");
            int result =await DAL.RunProcedureasync(ProcedureName, SL);
            return result == 1;
        }

        public async Task<bool> DeleteAsync(string Text)
        {
            SortedList SL = new SortedList
            {
                { "@Check", "Delete_User" },
                { "@UserName", Text},

            };
            int result = await DAL.RunProcedureasync(ProcedureName, SL);
            return result == 1;
        }
        
        public async Task<bool> UpdateAsync(CTB_Users item)
        {
            SortedList SL = AssignValuesToSortedList(item, "Update_User");
            int result = await DAL.RunProcedureasync(ProcedureName, SL);
            return result == 1;
        }


        public DataTable Get_ALLData()
        {
            SortedList SL = new SortedList
            {
                { "@Check", "Get_ALLUsers" },

            };
            DataTable dt = DAL.SelectDB(ProcedureName, SL);
            return dt;

        }


        public List<CTB_Users> Get_ALL()
        {
            SortedList SL = new SortedList
            {
                { "@Check", "Get_ALLUsers" },

            };
            DataTable dt = DAL.SelectDB(ProcedureName, SL);

            if (dt.Rows.Count > 0)
            {        
                var Mylist = dt.ConvertDataTableToList<CTB_Users>();

                return Mylist;
            }
            else
            {
                return null;
            }
        }

        public CTB_Users Get_AllBySearch(string Text)
        {
            SortedList SL = new SortedList
            {
                { "@Check", "Get_UsersByUserName" },
                { "@UserName", Text },

            };
            DataTable dt = DAL.SelectDB(ProcedureName, SL);
            if (dt.Rows.Count > 0)
            {
                var Mylist = dt.ConvertDataTableToList<CTB_Users>();
                return Mylist[0];
            }
            else
            {
                return null;
            }
        }

        public CTB_Users Get_ByID(string Text)
        {
            throw new NotImplementedException();
        }

        public int Get_CountUsersLog()
        {
            throw new NotImplementedException();
        }

        
    }
}
