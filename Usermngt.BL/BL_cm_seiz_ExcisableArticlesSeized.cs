using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{
    #region BL_cm_seiz_ExcisableArticlesSeized
    public class BL_cm_seiz_ExcisableArticlesSeized
    {
        public static bool InsertExcisableArticlesSeized(cm_seiz_ExcisableArticlesSeized obj)
        {
            return DL_cm_seiz_ExcisableArticlesSeized.InsertSeiz_ExcisableArticlesSeized(obj);
        }

        public static List<cm_seiz_ExcisableArticlesSeized> GetList(string seizureNo)
        {
            return DL_cm_seiz_ExcisableArticlesSeized.GetList(seizureNo);
        }

        public static List<cm_court> GetFilterList(string distcode,string fromdate)
        {
            return DL_cm_seiz_ExcisableArticlesSeized.GetFilterLists(distcode,fromdate);
        }

        public static bool Update(cm_seiz_ExcisableArticlesSeized cm_obj)
        {
            return DL_cm_seiz_ExcisableArticlesSeized.Update_ExcisableArticlesSeized(cm_obj);
        }

        public static List<cm_seiz_ExcisableArticlesSeized> ArticleSearch(string _Article)
        {
            return DL_cm_seiz_ExcisableArticlesSeized.ArticleSearch(_Article);
        }

        public static cm_seiz_ExcisableArticlesSeized GetDetails(string tableId)
        {
            return DL_cm_seiz_ExcisableArticlesSeized.GetDetails(tableId);
        }

        public static List<cm_seiz_ExcisableArticlesSeized> GetArticleSearch(string artical_code)
        {
            throw new NotImplementedException();
        }

       
    }
    #endregion BL_cm_seiz_ExcisableArticlesSeized
}
