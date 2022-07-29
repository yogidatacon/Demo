using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usermngt.DAL;
using Usermngt.Entities;

namespace Usermngt.BL
{   
    
    #region BL_cm_article_category
    public class BL_cm_article_category
    {
        public static bool InsertArticleCategory(cm_article_category article_category)
        {
            return DL_cm_article_category.InserArticleCategory(article_category);
        }
        public static List<cm_article_category> GetListILike(string texts, string colname)
        {
            return DL_cm_article_category.GetListILike(texts, colname);
        }
        public static List<cm_article_category> GetList()
        {
            return DL_cm_article_category.GetList();
        }

        public static bool UpdateArticle(cm_article_category article)
        {
            return DL_cm_article_category.UpdateArticle(article);
        }

    }
    #endregion BL_cm_article_category

    #region BL_cm_article_name
    public class BL_cm_article_name
    {
        public static bool InsertArticleName(cm_article_name article_name)
        {
            return DL_cm_article_name.InsertArticleName(article_name);
        }
        public static List<cm_article_name> GetListILike(string texts, string colname)
        {
            return DL_cm_article_name.GetListILike(texts, colname);
        }
        public static List<cm_article_name> GetList()
        {
            return DL_cm_article_name.GetList();
        }

        public static bool UpdateArticle(cm_article_name articlename)
        {
            return DL_cm_article_name.UpdateArticle(articlename);
        }
    }
    #endregion BL_cm_article_name

    #region BL_cm_article_subcategory
    public class BL_cm_article_subcategory
    {
        public static bool InsertArticleSubCategory(cm_article_subcategory article_subcategory)
        {
            return DL_cm_article_subcategory.InsertArticleSubCategory(article_subcategory);
        }
        public static List<cm_article_subcategory> GetListILIke(string texts, string colname)
        {
            return DL_cm_article_subcategory.GetListILike(texts, colname);
        }
        public static List<cm_article_subcategory> GetList()
        {
            return DL_cm_article_subcategory.GetList();
        }

        public static bool UpdateArticlesub(cm_article_subcategory article_subcategory)
        {
            return DL_cm_article_subcategory.UpdateArticlesub(article_subcategory);
        }
    }
    #endregion BL_cm_article_subcategory

    #region BL_cm_bail_type
    public class BL_cm_bail_type
    {
        public static bool InsertBailType(cm_bail_type bail_type)
        {
            return DL_cm_bail_type.InsertBailType(bail_type);
        }
        public static List<cm_bail_type> GetListILike(string texts, string colname)
        {
            return DL_cm_bail_type.GetListILike(texts, colname);
        }

        public static List<cm_bail_type> GetList()
        {
            return DL_cm_bail_type.GetList();
        }

        public static bool UpdateBail(cm_bail_type Bail)
        {
            return DL_cm_bail_type.UpdateBail(Bail);
        }
    }
    #endregion BL_cm_bail_type

    #region BL_cm_caste
    public class BL_cm_caste
    {
        public static bool InsertCaste(cm_caste caste)
        {
            return DL_cm_caste.InsertCaste(caste);
        }
        public static List<cm_caste> GetListILike(string texts, string colname)
        {
            return DL_cm_caste.GetListILike(texts, colname);
        }
        public static List<cm_caste> GetList()
        {
            return DL_cm_caste.GetList();
        }

        public static bool UpdateCaste(cm_caste caste)
        {
            return DL_cm_caste.UpdateCaste(caste);
        }
    }
    #endregion BL_cm_caste

    #region BL_cm_court
    public class BL_cm_court
    {
        public static bool InsertCourt(cm_court court)
        {
            return DL_cm_court.InsertCourt(court);
        }
        public static List<cm_court> GetListILike(string texts, string colname)
        {
            return DL_cm_court.GetListILike(texts, colname);
        }
        public static bool InsertDisCourt(cm_court court)
        {
            return DL_cm_court.InsertDistrictCourt(court);
        }

        public static bool InsertDMEntry(cm_court obj)
        {
            return DL_cm_court.InsertDmCaseEntry(obj);
        }

        public static bool InsertDMHEntry(cm_court obj)
        {
            return DL_cm_court.InsertDmHCaseEntry(obj);
        }

        public static bool InsertEXHEntry(cm_court obj)
        {
            return DL_cm_court.InsertEXHEntry(obj);
        }

        public static bool InsertExCaseRegEntry(cm_court obj)
        {
            return DL_cm_court.InsertExCaseRegEntry(obj);
        }

        public static bool UpdateDMEntry(cm_court obj)
        {
            return DL_cm_court.UpdateDMEntry(obj);
        }

        public static List<cm_court> GetList()
        {
            return DL_cm_court.GetList();
        }

        public static List<cm_court> GetRoleLevelList()
        {
            return DL_cm_court.GetRoleLevelList();
        }

        public static List<cm_court> GetDistrictCourtList()
        {
            return DL_cm_court.GetDistrictCourtList();
        }

        public static bool UpdateCourt(cm_court court)
        {
            return DL_cm_court.UpdateCourt(court);
        }

        public static cm_court GetDMDetails(string id)
        {
            return DL_cm_court.GetDMDetails(id);
        }
        public static cm_court GetEXDetails(string id)
        {
            return DL_cm_court.GetEXDetails(id);
        }

        public static bool UpdateEXEntry(cm_court obj)
        {
            return DL_cm_court.UpdateEXEntry(obj);
        }

        public static cm_court GetSECDetails(string id)
        {
            return DL_cm_court.GetSECDetails(id);
        }

        public static bool UpdateSECEntry(cm_court obj)
        {
            return DL_cm_court.UpdateSECEntry(obj);
        }
    }
    #endregion BL_cm_court

    #region BL_cm_designation
    public class BL_cm_designation
    {
        public static bool InsertDesignation(cm_designation designation)
        {
            return DL_cm_designation.InsertDesignation(designation);
        }
        public static List<cm_designation> GetListILike(string texts, string colname)
        {
            return DL_cm_designation.GetListILike(texts, colname);
        }
        public static List<cm_designation> GetList()
        {
            return DL_cm_designation.GetList();
        }

        public static bool UpdateDesignation(cm_designation designation)
        {
            return DL_cm_designation.UpdateDesignation(designation);
        }
    }
    #endregion BL_cm_designation

    #region BL_cm_designation_type
    public class BL_cm_designation_type
    {
        public static bool InsertDesignationType(cm_designation_type designation_type)
        {
            return DL_cm_designation_type.InsertDesignationType(designation_type);
        }
        public static List<cm_designation_type> GetListILike(string texts, string colname)
        {
            return DL_cm_designation_type.GetListILike(texts, colname);
        }
        public static List<cm_designation_type> GetList()
        {
            return DL_cm_designation_type.GetList();
        }

        public static bool UpdateDesignation(cm_designation_type designation_type)
        {
            return DL_cm_designation_type.UpdateDesignation(designation_type);
        }
    }
    #endregion BL_cm_designation_type

    #region BL_cm_disposal_of_property
    public class BL_cm_disposal_of_property
    {
        public static bool InsertDisposalOfProperty(cm_disposal_of_property disposal_of_property)
        {
            return DL_cm_disposal_of_property.InsertDisposalOfProperty(disposal_of_property);
        }

        public static List<cm_disposal_of_property> GetList()
        {
            return DL_cm_disposal_of_property.GetList();
        }

        public static bool UpdateDisposalType(cm_disposal_of_property disposal_of_property)
        {
            return DL_cm_disposal_of_property.UpdateDisposalType(disposal_of_property);
        }
    }
    #endregion BL_cm_disposal_of_property

    #region BL_cm_gender
    public class BL_cm_gender
    {
        public static bool InsertGender(cm_gender gender)
        {
            return DL_cm_gender.InsertGender(gender);
        }

        public static List<cm_gender> GetList()
        {
            return DL_cm_gender.GetList();
        }

        public static bool UpdateGender(cm_gender gender)
        {
            return DL_cm_gender.UpdateGender(gender);
        }
        public static List<cm_gender> GetListILike(string texts, string colname)
        {
            return DL_cm_gender.GetListILike(texts, colname);
        }
    }
    #endregion BL_cm_gender

    #region BL_cm_idproof
    public class BL_cm_idproof
    {
        public static bool InsertIDProof(cm_idproof idproof)
        {
            return DL_cm_idproof.InsertIDProof(idproof);
        }
        public static List<cm_idproof> GetListILike(string texts, string colname)
        {
            return DL_cm_idproof.GetListILike(texts, colname);
        }
        public static List<cm_idproof> GetList()
        {
            return DL_cm_idproof.GetList();
        }

        public static bool UpdateIDproof(cm_idproof idproof)
        {
            return DL_cm_idproof.UpdateIDproof(idproof);
        }
    }
    #endregion BL_cm_idproof

    #region BL_cm_offence
    public class BL_cm_offence
    {
        public static bool InsertOffencey(cm_offence offence)
        {
            return DL_cm_offence.InsertOffencey(offence);
        }
        public static List<cm_offence> GetListILike(string texts, string colname)
        {
            return DL_cm_offence.GetListILike(texts, colname);
        }
        public static List<cm_offence> GetList()
        {
            return DL_cm_offence.GetList();
        }

        public static bool Updateoffence(cm_offence offence)
        {
            return DL_cm_offence.Updateoffence(offence);
        }
    }
    #endregion BL_cm_offence

    #region BL_cm_offence_type
    public class BL_cm_offence_type
    {
        public static bool InsertOffenceType(cm_offence_type offence_type)
        {
            return DL_cm_offence_type.InsertOffenceType(offence_type);
        }
        public static List<cm_offence_type> GetListILike(string texts, string colname)
        {
            return DL_cm_offence_type.GetListILike(texts, colname);
        }
        public static List<cm_offence_type> GetList()
        {
            return DL_cm_offence_type.GetList();
        }

        //public static List<districtdata> GetDistrictList()
        //{
        //    return DL_cm_offence_type.GetDistrictdatas();
        //}

        public static bool UpdateOffenceType(cm_offence_type offence_type)
        {
            return DL_cm_offence_type.UpdateOffenceType(offence_type);
        }

        public static List<cm_offence_sections> GetSectionList()
        {
            return DL_cm_offence_type.GetSectionList();
        }
    }
    #endregion BL_cm_offence_type

    #region BL_cm_religion
    public class BL_cm_religion
    {
        public static bool InsertReligion(cm_religion religion)
        {
            return DL_cm_religion.InsertReligion(religion);
        }

        public static List<cm_religion> GetList()
        {
            return DL_cm_religion.GetList();
        }

        public static bool UpdateReligion(cm_religion religion)
        {
            return DL_cm_religion.UpdateReligion(religion);
        }
        public static List<cm_religion> GetListILike(string texts, string colname)
        {
            return DL_cm_religion.GetListILike(texts, colname);
        }
    }
    #endregion BL_cm_religion

    #region BL_cm_seizure_stage
    public class BL_cm_seizure_stage
    {
        public static bool InsertSeizureStage(cm_seizure_stage seizure_stage)
        {
            return DL_cm_seizure_stage.InsertSeizureStage(seizure_stage);
        }

        public static List<cm_seizure_stage> GetList()
        {
            return DL_cm_seizure_stage.GetList();
        }

        public static bool UpdateSeizurestage(cm_seizure_stage seizure_stage)
        {
            return DL_cm_seizure_stage.UpdateSeizurestage(seizure_stage);
        }
    }
    #endregion BL_cm_seizure_stage

    #region BL_cm_seizure_status
    public class BL_cm_seizure_status
    {
        public static bool InsertSeizureStatus(cm_seizure_status seizure_status)
        {
            return DL_cm_seizure_status.InsertSeizureStatus(seizure_status);
        }

        public static List<cm_seizure_status> GetList()
        {
            return DL_cm_seizure_status.GetList();
        }
    }
    #endregion BL_cm_seizure_status

    #region BL_cm_property_type
    public class BL_cm_property_type
    {
        public static bool InsertPropertyType(cm_property_type property_type)
        {
            return DL_cm_property_type.InsertPropertyType(property_type);
        }
        public static List<cm_property_type> GetListILike(string texts, string colname)
        {
            return DL_cm_property_type.GetListILike(texts, colname);
        }
        public static List<cm_property_type> GetList()
        {
            return DL_cm_property_type.GetList();
        }

        public static bool Updatepropertytype(cm_property_type property_type)
        {
            return DL_cm_property_type.Updateproperty_type(property_type);
        }
    }
    #endregion BL_cm_property_type

    #region BL_cm_Vehicle_type
    public class BL_cm_Vehicle_type
    {
        public static bool InsertVehicleType(cm_Vehicle_type Vehicle_type)
        {
            return DL_cm_Vehicle_type.InsertVehicleType(Vehicle_type);
        }
        public static List<cm_Vehicle_type> GetListILike(string texts, string colname)
        {
            return DL_cm_Vehicle_type.GetListILike(texts, colname);
        }
        public static List<cm_Vehicle_type> GetList()
        {
            return DL_cm_Vehicle_type.GetList();
        }

        public static bool UpdateVehicletype(cm_Vehicle_type vehicletype)
        {
            return DL_cm_Vehicle_type.UpdateVehicletype(vehicletype);
        }
    }
    #endregion BL_cm_Vehicle_type

}
