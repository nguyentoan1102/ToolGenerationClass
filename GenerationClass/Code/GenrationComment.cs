using System;

namespace GenerationClass.Code
{
    public class GenrationComment
    {
        private string strCreateResult;
        private string strDLLAPIResult;
        private string strDLLDataAccessResult;
        private string strDLLContractResult;
        private string strDLLServiceResult;
        private string strDLLBLLResult;

        public string GetDefaultCreate()
        {
            strCreateResult += " " + Environment.NewLine;
            strCreateResult += "//////////////////////////////////////////////////////////////////////////////////////////////////////" + Environment.NewLine;
            strCreateResult += "//                                                                                                  // " + Environment.NewLine;
            strCreateResult += "// Ngày tạo:" + DateTime.Now.ToString() + "                                                     //" + Environment.NewLine;
            strCreateResult += "//                                                                                                  //" + Environment.NewLine;
            strCreateResult += "//////////////////////////////////////////////////////////////////////////////////////////////////////" + Environment.NewLine;
            return strCreateResult;
        }

        public string GetDLLAPICreate()
        {
            strDLLAPIResult += " " + Environment.NewLine;
            strDLLAPIResult += "using System;" + Environment.NewLine;
            strDLLAPIResult += "using System.Collections.Generic;" + Environment.NewLine;
            strDLLAPIResult += "using System.Linq;" + Environment.NewLine;
            strDLLAPIResult += "using System.Text;" + Environment.NewLine;
            strDLLAPIResult += "using System.Runtime.Serialization;" + Environment.NewLine;
            strDLLAPIResult += "using System.Data.SqlClient;" + Environment.NewLine;
            strCreateResult += " " + Environment.NewLine;
            return strDLLAPIResult;
        }

        public string GetDLLDataAccessCreate(string namespaceIn)
        {
            strDLLDataAccessResult += " " + Environment.NewLine;
            strDLLDataAccessResult += "using System;" + Environment.NewLine;
            strDLLDataAccessResult += "using System.Collections.Generic;" + Environment.NewLine;
            strDLLDataAccessResult += "using System.Linq;" + Environment.NewLine;
            strDLLDataAccessResult += "using System.Text;" + Environment.NewLine;
            strDLLDataAccessResult += "using System.Data;" + Environment.NewLine;
            strDLLDataAccessResult += "using System.Data.SqlClient;" + Environment.NewLine;
            strDLLDataAccessResult += "using Public;" + Environment.NewLine;
            strDLLDataAccessResult += "using System.Reflection;" + Environment.NewLine;
            strCreateResult += " " + Environment.NewLine;
            return strDLLDataAccessResult;
        }

        public string GetDLLBLLCreate(string namespaceIn)
        {
            strDLLBLLResult += " " + Environment.NewLine;
            strDLLBLLResult += "using System;" + Environment.NewLine;
            strDLLBLLResult += "using System.Collections.Generic;" + Environment.NewLine;
            strDLLBLLResult += "using System.Linq;" + Environment.NewLine;
            strDLLBLLResult += "using System.Text;" + Environment.NewLine;
            strDLLBLLResult += "using System.Data;" + Environment.NewLine;
            strDLLBLLResult += "using System.Windows.Forms;" + Environment.NewLine;
            strDLLBLLResult += "using DAL;" + Environment.NewLine;
            strDLLBLLResult += "using Public;" + Environment.NewLine;
            strDLLBLLResult += "using System.Reflection;" + Environment.NewLine;
            strCreateResult += " " + Environment.NewLine;
            return strDLLBLLResult;
        }
    }
}