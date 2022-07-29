using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
    #region cm_article_category

    public class daily_diary_raid
    {
        public string daily_dairy_raid_id { get; set; }
        public string raid_entry_date { get; set; }
        public string place_of_raid { get; set; }
        public string distance_of_travelled { get; set; }
        public string raid_team_leader { get; set; }
        public string raid_recovery { get; set; }
        public string no_of_arrested { get; set; }
        public string no_of_absconding { get; set; }
        public string no_of_case_instituted { get; set; }
        public string other_recovery { get; set; }
        public string record_active { get; set; }
        public string lastmodified_date { get; set; }
        public string creation_date { get; set; }
        public string user_id { get; set; }
        public string record_status { get; set; }
        public string gender { get; set; }
        public string record_deleted { get; set; }
        public string district_code { get; set; }
        public string division_code { get; set; }
        public List<Seizure_Docs> docs { get; set; }
        public List<daily_dairy_recovery> recovery { get; set; }
        public List<seizure_gender> genderlist { get; set; }
    }


    public class seizure_gender
    {
        public string gender_id { get; set; }
        public int arresting { get; set; }
        public string gender_code { get; set; }
        public string gender_name { get; set; }
        public string daily_dairy_raid_id { get; set; }
        public string record_active { get; set; }
        public string lastmodified_date { get; set; }
        public string creation_date { get; set; }
        public string user_id { get; set; }
        public string record_status { get; set; }
        public string record_deleted { get; set; }
        public string district_code { get; set; }

    }
    public class daily_dairy_recovery
    {
        public string daily_dairy_recovery_id { get; set; }
        public string daily_dairy_raid_id { get; set; }
        public string recovery_type { get; set; }
        public string recovery_particulars_id { get; set; }
        public string recovery_particulars_name { get; set; }
        public string recovery_description { get; set; }
        public string recovery_qty { get; set; }
        public string uom_code { get; set; }
        public string uom_name { get; set; }
        public string record_active { get; set; }
        public string lastmodified_date { get; set; }
        public string creation_date { get; set; }
        public string user_id { get; set; }
        public string record_status { get; set; }
        public string record_deleted { get; set; }
        public string district_code { get; set; }

    }
    public class daily_diary_entry_otherthan_raid
    {
        public string daily_dairy_otherthan_raid_id { get; set; }
        public string raid_entry_date { get; set; }
        public string intelligence_gathering { get; set; }
        public string petrolling { get; set; }
        public string vehicle_check { get; set; }
        public string liquor_destruction { get; set; }
        public string witness_appearance_in_court { get; set; }
        public string division_code { get; set; }
        public string others { get; set; }
        public string meeting { get; set; }
        public string raid_recovery { get; set; }
        public string record_active { get; set; }
        public string lastmodified_date { get; set; }
        public string uom_code { get; set; }
        public string uom_name { get; set; }
        public double quantity { get; set; }
        public string creation_date { get; set; }
        public string user_id { get; set; }
        public string record_status { get; set; }
        public string district_code { get; set; }
        public List<Seizure_Docs> docs { get; set; }
    }
    public class cm_article_category
    {
        public int article_category_master_id { get; set; }
        public string article_category_code { get; set; }
        public string article_category_name { get; set; }
        public string lastmodified_date { get; set; }
        public string user_id { get; set; }
        public string creation_date { get; set; }
        public string record_status { get; set; }
        public bool record_deleted { get; set; }
    }
    #endregion cm_article_category

    #region cm_article_name
    public class cm_article_name
    {
        public int article_name_master_id { get; set; }
        public string article_name_code { get; set; }
        public string article_sub_category_code { get; set; }
        public string article_category_code { get; set; }
        public string article_sub_category_name { get; set; }
        public string article_category_name { get; set; }
        public string article_name { get; set; }
        public string lastmodified_date { get; set; }
        public string user_id { get; set; }
        public string creation_date { get; set; }
        public string record_status { get; set; }
        public bool record_deleted { get; set; }
    }
    #endregion cm_article_name

    #region cm_article_subcategory
    public class cm_article_subcategory
    {
        public int article_sub_category_master_id { get; set; }
        public string article_sub_category_code { get; set; }
        public string article_sub_category_name { get; set; }
        public string article_category_code { get; set; }
        public string article_category_name { get; set; }
        public string lastmodified_date { get; set; }
        public string user_id { get; set; }
        public string creation_date { get; set; }
        public string record_status { get; set; }
        public bool record_deleted { get; set; }
    }
    #endregion cm_article_subcategory

    #region cm_bail_type
    public class cm_bail_type
    {
        public int bail_type_master_id { get; set; }
        public string bail_type_master_code { get; set; }
        public string bail_type_master_name { get; set; }
        public string lastmodified_date { get; set; }
        public string user_id { get; set; }
        public string creation_date { get; set; }
        public string record_status { get; set; }
        public bool record_deleted { get; set; }
    }
    #endregion cm_bail_type

    #region cm_caste
    public class cm_caste
    {
        public int caste_master_id { get; set; }
        public string caste_code { get; set; }
        public string caste_name { get; set; }
        public string category_code { get; set; }
        public string religion_code { get; set; }
        public string religion_name { get; set; }
        public string lastmodified_date { get; set; }
        public string user_id { get; set; }
        public string creation_date { get; set; }
        public string record_status { get; set; }
        public bool record_deleted { get; set; }
    }
    #endregion cm_caste

    #region cm_court
    public class cm_court
    {
        public int court_master_id { get; set; }
        public string court_master_code { get; set; }
        public string court_master_name { get; set; }
        public string lastmodified_date { get; set; }
        public string user_id { get; set; }
        public string creation_date { get; set; }
        public string record_status { get; set; }
        public string hearing_status { get; set; }
        public bool record_deleted { get; set; }
       
        public string district_code { get; set; }

        public int district_court_master_id { get; set; }

        public int dmcase_registration_id { get; set; }
        public int seccase_registration_id { get; set; }
        public int excase_registration_id { get; set; }
        public int seizure_fir_no { get; set; }

        public string raidby { get; set; }
        public string district_name { get; set; }

        public string proposed_letterno { get; set; }

        public string proposed_letterdate { get; set; }


        public string case_type { get; set; }
        public string caseno { get; set; }
       
        public string case_hearingdate { get; set; }
        public string next_hearingdate { get; set; }

        public int seizureno { get; set; }

        public string prfirno { get; set; }
        public string thana_name { get; set; }
        public string thana_code { get; set; }

        public string vp { get; set; }

        public string case_registerdate { get; set; }
        public List<Seizure_Docs> docs { get; set; }
        public List<Seizure_Docs> docs1 { get; set; }
        public List<Seizure_Docs> docs2 { get; set; }
        public List<Seizure_Docs> docs3 { get; set; }
        public List<Seizure_Docs> docs4 { get; set; }
        public List<Seizure_Docs> docs5 { get; set; }
        public List<cm_hearings> hearings { get; set;}
        public string case_action { get; set; }
        public string confiscation_code { get; set; }
        public string confiscationorderno { get; set; }

        public string confiscationorderdate { get; set; }

        public string role_level_code { get; set; }

        public string role_level_name { get; set; }

        public string appealno { get; set; }

        public string appealdate { get; set; }

        public string appellant_name { get; set; }

        public string appellant_contact { get; set; }
        public string remarks { get; set; }
        public string hremarks { get; set; }
    }
    #endregion cm_court
    public class cm_hearings
    {
        public int hearing_id { get; set; }
        public string hearing_date { get; set; }
        public string next_hearing_date { get; set; }
        public string hearing_remarks { get; set; }
        public string case_action { get; set; }
        public string lastmodified_date { get; set; }
        public string user_id { get; set; }
        public string creation_date { get; set; }
        public string record_status { get; set; }
        public bool record_deleted { get; set; }
    }
    #region cm_designation
    public class cm_designation
    {
        public int designation_master_id { get; set; }
        public string designation_code { get; set; }
        public string designation_type_code { get; set; }
        public string designation_type_name { get; set; }
        public string designation_name { get; set; }
        public string _designation { get; set; }
        public string lastmodified_date { get; set; }
        public string user_id { get; set; }
        public string creation_date { get; set; }
        public string record_status { get; set; }
        public bool record_deleted { get; set; }
    }
    #endregion cm_designation

    #region cm_designation_type
    public class cm_designation_type
    {
        public int designation_type_master_id { get; set; }
        public string designation_type_code { get; set; }
        public string designation_type_name { get; set; }
        public string lastmodified_date { get; set; }
        public string user_id { get; set; }
        public string creation_date { get; set; }
        public string record_status { get; set; }
        public bool record_deleted { get; set; }
    }
    #endregion cm_designation_type

    #region cm_disposal_of_property
    public class cm_disposal_of_property
    {
        public int disposal_of_property_id { get; set; }
        public string disposal_of_property_code { get; set; }
        public string disposal_of_property_name { get; set; }
        public string lastmodified_date { get; set; }
        public string user_id { get; set; }
        public string creation_date { get; set; }
        public string record_status { get; set; }
        public bool record_deleted { get; set; }
    }
    #endregion cm_disposal_of_property

    #region cm_gender
    public class cm_gender
    {
        public int gender_master_id { get; set; }
        public string gender_code { get; set; }
        public string gender_name { get; set; }
        public string lastmodified_date { get; set; }
        public string user_id { get; set; }
        public string creation_date { get; set; }
        public string record_status { get; set; }
        public bool record_deleted { get; set; }
    }
    #endregion cm_gender

    #region cm_idproof
    public class cm_idproof
    {
        public int idproof_master_id { get; set; }
        public string idproof_code { get; set; }
        public string idproof_name { get; set; }
        public string lastmodified_date { get; set; }
        public string user_id { get; set; }
        public string creation_date { get; set; }
        public string record_status { get; set; }
        public bool record_deleted { get; set; }
    }
    #endregion cm_idproof

    #region cm_offence
    public class cm_offence
    {
        public int offence_master_id { get; set; }
        public string offence_code { get; set; }
        public string offence_name { get; set; }
        public string offence_type_code { get; set; }
        public string offence_type_name { get; set; }
        public string lastmodified_date { get; set; }
        public string user_id { get; set; }
        public string creation_date { get; set; }
        public string record_status { get; set; }
        public bool record_deleted { get; set; }
    }
    #endregion cm_offence

    #region cm_offence_type
    public class cm_offence_type
    {
        public int offence_master_id { get; set; }
        public string offence_code { get; set; }
        public string offence_name { get; set; }
        public string lastmodified_date { get; set; }
        public string user_id { get; set; }
        public string creation_date { get; set; }
        public string record_status { get; set; }
        public bool record_deleted { get; set; }
    }
    public class cm_offence_sections
    {
        public int offence_section_master_id { get; set; }
        public string offence_section_code { get; set; }
        public string offence_section_name { get; set; }
        public string lastmodified_date { get; set; }
        public string user_id { get; set; }
        public string creation_date { get; set; }
        public string record_status { get; set; }
        public bool record_deleted { get; set; }
    }
    #endregion cm_offence_type

    #region cm_religion
    public class cm_religion
    {
        public int religion_master_id { get; set; }
        public string religion_code { get; set; }
        public string religion_name { get; set; }
        public string lastmodified_date { get; set; }
        public string user_id { get; set; }
        public string creation_date { get; set; }
        public string record_status { get; set; }
        public bool record_deleted { get; set; }
    }
    #endregion cm_religion

    #region cm_seizure_stage
    public class cm_seizure_stage
    {
        public int seizure_stage_id { get; set; }
        public string seizure_stage_code { get; set; }
        public string seizure_stage_name { get; set; }
        public int seizure_stage_sequence { get; set; }
        public string lastmodified_date { get; set; }
        public string user_id { get; set; }
        public string creation_date { get; set; }
        public string record_status { get; set; }
        public bool record_deleted { get; set; }
    }
    #endregion cm_seizure_stage

    #region cm_seizure_status
    public class cm_seizure_status
    {
        public int seizure_status_id { get; set; }
        public string seizure_status_code { get; set; }
        public string seizure_status_name { get; set; }
        public bool edit_seizure { get; set; }
        public string lastmodified_date { get; set; }
        public string user_id { get; set; }
        public string creation_date { get; set; }
        public string record_status { get; set; }
        public bool record_deleted { get; set; }
    }
    #endregion cm_seizure_status

    #region cm_property_type
    public class cm_property_type
    {
        public int product_type_master_id { get; set; }
        public string product_type_code { get; set; }
        public string product_type_name { get; set; }
        public string lastmodified_date { get; set; }
        public string user_id { get; set; }
        public string creation_date { get; set; }
        public string record_status { get; set; }
        public bool record_deleted { get; set; }
    }
    #endregion cm_property_type

    #region cm_Vehicle_type
    public class cm_Vehicle_type
    {
        public int vehicle_type_id { get; set; }
        public string vehicle_type_code { get; set; }
        public string vehicle_type { get; set; }
        public string lastmodified_date { get; set; }
        public string user_id { get; set; }
        public string creation_date { get; set; }
        public string record_status { get; set; }
        public bool record_deleted { get; set; }
    }
    #endregion cm_Vehicle_type

    #region CaseManagement Entity Tables

    #region cm_seizure
    public class cm_seizure
    {
        public int seizure_id { get; set; }
        public int seizureno { get; set; }
        public string finalseizureno { get; set; }
        public string seizure_status_code { get; set; }
        public int seizure_stage_code { get; set; }
        public string raidby { get; set; }
        public string casefiled { get; set; }
        public string caseno { get; set; }
        public DateTime casefileddate { get; set; }
        public string casestatus { get; set; }
        public string casecourt { get; set; }
        public bool excisable_article_seized { get; set; }

        public DateTime lastmodified_date { get; set; }
        public string user_id { get; set; }
        public DateTime creation_date { get; set; }
        public string record_status { get; set; }
        public bool record_deleted { get; set; }
    }
    #endregion cm_seizure

    #region cm_seiz_BasicIformation
    public class cm_seiz_BasicIformation
    {
        public int basicinfo_id { get; set; }
        public int seizureno { get; set; }
        public string recoverytype { get; set; }
        public string recoveryname { get; set; }
        public string manualseizureno { get; set; }
        public string raidby { get; set; }
        public string raid_date { get; set; }
        public string raid_time { get; set; }
        public string raid_location { get; set; }
        public string division_code { get; set; }
        public string district_code { get; set; }
        public string thana_code { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string ipaddress { get; set; }
        public string remarks { get; set; }

        public string thanaName { get; set; }

        public DateTime lastmodified_date { get; set; }
        public string user_id { get; set; }
        public DateTime creation_date { get; set; }
        public string record_status { get; set; }
        public bool record_deleted { get; set; }
        public string prfirno { get; set; }

        public List<Seizure_Docs> docs { get; set; }
    }
    #endregion cm_seiz_BasicIformation

    #region cm_seiz_ExcisableArticlesSeized
    public class cm_seiz_ExcisableArticlesSeized
    {
        public int seizure_excisable_articles_id { get; set; }
        public int seizureno { get; set; }
        public string Different_Liquor { get; set; }
        public string raidby { get; set; }
        public string article_category_code { get; set; }
        public string article_category_name{ get; set; }
        public string article_sub_category_code { get; set; }
        public string article_sub_category_name { get; set; }
        public string article_name_code { get; set; }
        public string article_name { get; set; }
        public string manufacturer_code { get; set; }
        public string uom_code { get; set; }
        public string packingsize_code { get; set; }
        public string quantity { get; set; }
        public string farmingsize { get; set; }
        public int order_no { get; set; }
        public DateTime order_date { get; set; }
        public DateTime auction_or_releasedate { get; set; }
        public string ipaddress { get; set; }
        public string date_of_destruction { get; set; }
        public string actioncompleted { get; set; }
        public string remarks { get; set; }
        public DateTime actioncompleted_date { get; set; }
        public DateTime actioncompletion_entered_date { get; set; }
        public DateTime lastmodified_date { get; set; }
        public string user_id { get; set; }
        public DateTime creation_date { get; set; }
        public string record_status { get; set; }
        public bool record_deleted { get; set; }
        public List<Seizure_Docs> docs { get; set; }
        public string manufacturing_date { get; set; }
        public string batchno { get; set; }
        public string prod_state_code { get; set; }
        public string sale_state_code { get; set; }
    }
    #endregion cm_seiz_ExcisableArticlesSeized

    #region cm_seiz_OtherExcisableArticles
    /// <summary>
    /// exciseautomation.OtherExcisableArticles
    /// </summary>
    public class cm_seiz_OtherExcisableArticles
    {
        public int seizure_apparatusdetails_id { get; set; }
        public int seizureno { get; set; }
        public string raidby { get; set; }
        public int apparatus_type_code { get; set; }
        public int manufacturer_code { get; set; }
        public string apparatus_name { get; set; }
        public string makemodel { get; set; }
        public string ownername { get; set; }
        public string presentaddress { get; set; }
        public string permanentaddress { get; set; }
        public string contactno { get; set; }
        public string ipaddress { get; set; }
        public string challan_no { get; set; }
        public DateTime challan_date { get; set; }
        public string order_no { get; set; }
        public DateTime order_date { get; set; }
        public DateTime auction_or_releasedate { get; set; }
        public int auctionreleaseamount { get; set; }
        public string infavourof { get; set; }
        public DateTime date_of_destruction { get; set; }
        public bool actioncompleted { get; set; }
        public DateTime actioncompleted_date { get; set; }
        public DateTime actioncompletion_entered_date { get; set; }
        public string vehicle_number { get; set; }
        public DateTime lastmodified_date { get; set; }
        public string user_id { get; set; }
        public DateTime creation_date { get; set; }
        public string record_status { get; set; }
        public bool record_deleted { get; set; }
    }
    #endregion cm_seiz_OtherExcisableArticles

    #region seizure_vehicledetails
    /// <summary>
    /// exciseautomation.seizure_vehicledetails
    /// </summary>
    public class cm_seiz_vehicledetails
    {
        public int seizure_vehicledetails_id { get; set; }
        public int seizureno { get; set; }
        public string SDR_CAF { get; set; }
        public string raidby { get; set; }
        public string vehicle_type_code { get; set; }
        public string vehicle_type { get; set; }
        public string manufacturer_code { get; set; }
        public string vehiclename { get; set; }
        public string makemodel { get; set; }
        public string chasisno { get; set; }
        public string engineno { get; set; }
        public string gpscompany { get; set; }
        public string imeino { get; set; }
        public string simno { get; set; }
        public string remarks { get; set; }
        public string registrationno { get; set; }
        public string ownername { get; set; }
        public string presentaddress { get; set; }
        public string permanentaddress { get; set; }
        public string contactno { get; set; }
        public string ipaddress { get; set; }
        public string challan_no { get; set; }
        public string challan_date { get; set; }
        public string order_no { get; set; }
        public DateTime order_date { get; set; }
        public string auction_or_releasedate { get; set; }
        public string auctionreleaseamount { get; set; }
        public string infavourof { get; set; }
        public DateTime date_of_destruction { get; set; }
        public string actioncompleted { get; set; }
        public DateTime actioncompleted_date { get; set; }
        public DateTime actioncompletion_entered_date { get; set; }
        public string vehicle_number { get; set; }
        public DateTime lastmodified_date { get; set; }
        public string user_id { get; set; }
        public DateTime creation_date { get; set; }
        public string record_status { get; set; }
        public bool record_deleted { get; set; }
    }
    #endregion seizure_vehicledetails

    #region cm_seiz_Apparatus
    public class cm_seiz_Apparatus
    {
        public int seizure_apparatusdetails_id { get; set; }
        public int seizureno { get; set; }
        public string raidby { get; set; }
        public string apparatus_type_code { get; set; }
        public string apparatus_type { get; set; }
        public string manufacturer_code { get; set; }
        public string apparatus_name { get; set; }
        public string makemodel { get; set; }
        public string ownername { get; set; }
        public string presentaddress { get; set; }
        public string permanentaddress { get; set; }
        public string contactno { get; set; }
        public string ipaddress { get; set; }
        public string challan_no { get; set; }
        public DateTime challan_date { get; set; }
        public string order_no { get; set; }
        public DateTime order_date { get; set; }
        public DateTime auction_or_releasedate { get; set; }
        public int auctionreleaseamount { get; set; }
        public string infavourof { get; set; }
        public string date_of_destruction { get; set; }
        public DateTime lastmodified_date { get; set; }
        public string user_id { get; set; }
        public DateTime creation_date { get; set; }
        public string record_status { get; set; }
        public bool record_deleted { get; set; }
        public string imeino { get; set; }
        public string actioncompleted { get; set; }

    }
    #endregion cm_seiz_Apparatus

    

    #region cm_seiz_Property
    public class cm_seiz_Property
    {
        public int seizure_propertydetails_id { get; set; }
        public int seizureno { get; set; }
        public string raidby { get; set; }
        public string property_type_code { get; set; }
        public string propertyaddress { get; set; }
        public string propertylocation { get; set; }
        public string propertylandmark { get; set; }
        public string propertycriclename { get; set; }
        public string propertymauzaname { get; set; }
        public string propertykhatano { get; set; }
        public string propertykhasrano { get; set; }
        public string propertythanano { get; set; }
        public string ownername { get; set; }
        public string presentaddress { get; set; }
        public string permanentaddress { get; set; }
        public string contactno { get; set; }
        public string ipaddress { get; set; }
        public string challan_no { get; set; }
        public DateTime challan_date { get; set; }
        public string order_no { get; set; }
        public DateTime order_date { get; set; }
        public string auction_or_releasedate { get; set; }
        public int auctionreleaseamount { get; set; }
        public string infavourof { get; set; }
        public string date_of_destruction { get; set; }
       
        public DateTime actioncompleted_date { get; set; }
        public DateTime actioncompletion_entered_date { get; set; }
        public string property_type { get; set; }
        public DateTime lastmodified_date { get; set; }
        public string user_id { get; set; }
        public DateTime creation_date { get; set; }
        public string record_status { get; set; }
        public bool record_deleted { get; set; }
        public string actioncompleted { get; set; }
    }
    #endregion cm_seiz_Property

    #region cm_seiz_Money
    public class cm_seiz_Money
    {
        public int seizure_moneydetails_id { get; set; }
        public int seizureno { get; set; }
        public string raidby { get; set; }
        public double? total_amount { get; set; }
        public string remarks { get; set; }
        public string ipaddress { get; set; }
        public string challan_no { get; set; }
        public string challan_date { get; set; }
        public string order_no { get; set; }
        public DateTime order_date { get; set; }
        public DateTime lastmodified_date { get; set; }
        public string user_id { get; set; }
        public DateTime creation_date { get; set; }
        public string record_status { get; set; }
        public bool record_deleted { get; set; }
        public double? currency { get; set; }
        public double? coins { get; set; }
        public string actioncompleted { get; set; }
        public string date_of_destruction { get; set; }

        public List<cm_seiz_CurrencyCoins> currencyCoins { get; set; }
    }
    #endregion cm_seiz_Money

    public class apparatus_type_master
    {
        public string apparatus_type_code { get; set; }
        public string apparatus_type { get; set; }
    }

    #region cm_seiz_CaseHistory
    /// <summary>
    /// seizure_accusedcasehistory
    /// </summary>
    public class cm_seiz_CaseHistory
    {
        public int seizure_accusedcasehistory_id { get; set; }
        public string seizure_accused_details_id { get; set; }
        public int seizureno { get; set; }
        public string case_id { get; set; }
        public string raidby { get; set; }
        public string case_details { get; set; }
        public string idproof_code { get; set; }
        public string idno { get; set; }
        public string ipaddress { get; set; }
        public string lastmodified_date { get; set; }
        public string user_id { get; set; }
        public DateTime creation_date { get; set; }
        public string record_status { get; set; }
        public bool record_deleted { get; set; }
    }
    #endregion cm_seiz_CaseHistory

    #region cm_seiz_FIR
    public class cm_seiz_FIR
    {
        public int seizure_fir_id { get; set; }
        public int seizureno { get; set; }
        public string raidby { get; set; }
        public string designation_code { get; set; }
       
        public string prfirno { get; set; }
        public string prfirdate { get; set; }
        public string manualprfirno { get; set; }
        public string manualbookdate { get; set; }
        public string raidorderby { get; set; }
        public string complaintno { get; set; }
        public string complaintdate { get; set; }
        public string infotocourtdate { get; set; }
        public string finalseizureno { get; set; }
        public string ipaddress { get; set; }
        public string lastmodified_date { get; set; }
        public string user_id { get; set; }
        public string creation_date { get; set; }
        public string record_status { get; set; }
        public string record_deleted { get; set; }
        public List<cm_seiz_AccusedDetails> accuseDetailsList { get; set; }        
    }
    # endregion cm_seiz_FIR


    #region cm_seiz_ChargeSheet
    public class cm_seiz_ChargeSheet
    {
        public int seizure_chargesheet_id { get; set; }
        public int seizureno { get; set; }
        public string raidby { get; set; }
        public int disposalmode_code { get; set; }
        public string disposalmode_name { get; set; }
        public string evidenceproof { get; set; }
        public string placeof_seizedpropertykept { get; set; }
        public string chargesheet_date { get; set; }
        public string producedatcourt_date { get; set; }
        public string producedatcourt_time { get; set; }
        public string chargesheet_remarks { get; set; }
        public string finalseizureno { get; set; }
        public string ipaddress { get; set; }
        public string lastmodified_date { get; set; }
        public string user_id { get; set; }
        public string creation_date { get; set; }
        public string record_status { get; set; }
        public string record_deleted { get; set; }

        public string prfirno { get; set; }
        public string prfirdate { get; set; }
        public string vehicle_verification { get; set; }
        public string vehicle_fsl { get; set; }
        public string liquor_test { get; set; }
        public string liquor_fsl { get; set; }

        public List<Seizure_Docs> docs { get; set; }

        public List<cm_seiz_AccusedDetails> accuseDetailsList { get; set; }

    }
    # endregion cm_seiz_ChargeSheet

    #region cm_seiz_Bail
    public class cm_seiz_Bail
    {
        public int seizure_bail_id { get; set; }
        public int seizureno { get; set; }
        public string raidby { get; set; }
        public string court_master_code { get; set; }
        public string seizure_accused_details_id { get; set; }
        public string bail_type_master_code { get; set; }
        public string bailgranted { get; set; }
        public string bailno { get; set; }
        public string bailrequestdate { get; set; }
        public string bailgranteddate { get; set; }
        public string bailreason { get; set; }
        public string bailer { get; set; }
        public string ipaddress { get; set; }
        public string lastmodified_date { get; set; }
        public string user_id { get; set; }
        public string creation_date { get; set; }
        public string record_status { get; set; }
        public string record_deleted { get; set; }
        public string prfirno { get; set; }
        public string prfirdate { get; set; }
        public string accusedname { get; set; }
        public string finalseizureno { get; set; }
        public string court_master_name { get; set; }
        public string bail_type_master_name { get; set; }
    }
    # endregion cm_seiz_Bail


    #region cm_seiz_Cognizance
    public class cm_seiz_Cognizance
    {
        public int seizure_trial_id { get; set; }
        public int seizureno { get; set; }
        public string raidby { get; set; }
        public int trialstage_code { get; set; }
        public DateTime currentstagedate { get; set; }
        public string chargedundersection { get; set; }
        public string witnessdetails { get; set; }
        public string accusedstatement { get; set; }
        public DateTime nexthearingdate { get; set; }
        public string remarks { get; set; }
        public string finalseizureno { get; set; }

        public DateTime lastmodified_date { get; set; }
        public string user_id { get; set; }
        public DateTime creation_date { get; set; }
        public string record_status { get; set; }
        public bool record_deleted { get; set; }

        public int currentstage { get; set; }
        public string columnconviction { get; set; }
        public string fine { get; set; }
        public string punishment { get; set; }
    }
    # endregion cm_seiz_Cognizance

    #region cm_seiz_trial
    public class cm_seiz_trial
    {
        public int seizure_trial_id { get; set; }
        public int seizureno { get; set; }
        public string raidby { get; set; }
        public int trialstage_code { get; set; }
        public string currentstagedate { get; set; }
        public string chargedundersection { get; set; }
        public string witnessdetails { get; set; }
        public string accusedstatement { get; set; }
        public string nexthearingdate { get; set; }
        public string remarks { get; set; }
        public string finalseizureno { get; set; }
        public string lastmodified_date { get; set; }
        public string user_id { get; set; }
        public string creation_date { get; set; }
        public string record_status { get; set; }
        public string record_deleted { get; set; }
        public int currentstage { get; set; }
        public string columnconviction { get; set; }
        public string fine { get; set; }
        public string punishment { get; set; }
        public string accusedId { get; set; }
        public string judgementType { get; set; }
        public List<Seizure_Docs> docs { get; set; }
    }
    #endregion cm_seiz_trial

    #region cm_seiz_AppealDetails
    public class cm_seiz_AppealDetails
    {
        public int seizure_appealdetails_id { get; set; }
        public int seizureno { get; set; }
        public string raidby { get; set; }
        public string court_master_code { get; set; }
        public string seizure_accused_details_id { get; set; }
        public string accusedstatus_code { get; set; }
        public string accusedstatus_name { get; set; }
        public string appealno { get; set; }
        public string appealdate { get; set; }
        public string appealby { get; set; }
        public string appealresult { get; set; }
        public string resultdate { get; set; }
        public string finalseizureno { get; set; }

        public DateTime lastmodified_date { get; set; }
        public string user_id { get; set; }
        public DateTime creation_date { get; set; }
        public string record_status { get; set; }
        public bool record_deleted { get; set; }
    }
    # endregion cm_seiz_AppealDetails

    #region cm_seiz_Dmconfiscation
    public class cm_seiz_Dmconfiscation
    {
        public int seizure_dmconfiscation_id { get; set; }
        public int seizureno { get; set; }
        public string raidby { get; set; }
        public string prfirno { get; set; }
        public string appl_letterno { get; set; }
        public string appl_letterdate { get; set; }
        public string dmordertype { get; set; }
        public string dmorderno { get; set; }
        public string dmorderdate { get; set; }
        public string dmremarks { get; set; }
        public string confiscation_caseno { get; set; }
        public string confiscationtype { get; set; }
        public string date_fixed { get; set; }
        public string magistratename { get; set; }
        public string date_executedon { get; set; }
        public int amountreceived { get; set; }
        public string highauthority_date { get; set; }
        public string highauthority_name { get; set; }
        public string highauthority_remarks { get; set; }
        public string finalseizureno { get; set; }

        public string lastmodified_date { get; set; }
        public string user_id { get; set; }
        public string creation_date { get; set; }
        public string record_status { get; set; }
        public bool record_deleted { get; set; }

        public string appealed_to_ha { get; set; }
        public List<cm_seiz_ExcisableArticlesSeized> articals { get; set; }
        public List<cm_seiz_vehicledetails> vehicals { get; set; }
        public List<cm_seiz_Apparatus> Apparatus { get; set; }
        public List<cm_seiz_Property> Property { get; set; }
        public List<cm_seiz_Money> Money { get; set; }
    }
    # endregion cm_seiz_Dmconfiscation

    #region cm_seiz_Excisecom
    public class cm_seiz_Excisecom
    {
        public int seizure_excisecom_id { get; set; }
        public int seizureno { get; set; }
        public string raidby { get; set; }
        public string prfirno { get; set; }
        public string appl_letterno { get; set; }
        public string appl_letterdate { get; set; }
        public string ecordertype { get; set; }
        public string ecorderno { get; set; }
        public string ecorderdate { get; set; }
        public string ecremarks { get; set; }
        public string confiscationtype { get; set; }
        public string date_fixed { get; set; }
        public string magistratename { get; set; }
        public string date_executedon { get; set; }
        public string amountreceived { get; set; }
        public string highauthority_date { get; set; }
        public string highauthority_name { get; set; }
        public string highauthority_remarks { get; set; }
        public string finalseizureno { get; set; }
        public string lastmodified_date { get; set; }
        public string user_id { get; set; }
        public string creation_date { get; set; }
        public string record_status { get; set; }
        public bool record_deleted { get; set; }
        public string confiscation_caseno { get; set; }
        public string appealed_to_ha { get; set; }
        public string auction_orderby { get; set; }
        public string destruction_orderby { get; set; }
        public List<cm_seiz_ExcisableArticlesSeized> articals { get; set; }
        public List<cm_seiz_vehicledetails> vehicals { get; set; }
    }
    #endregion cm_seiz_Excisecom

    #endregion CaseManagement Entity Tables


}
