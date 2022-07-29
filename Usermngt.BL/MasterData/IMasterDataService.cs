using System;
using System.Collections.Generic;
using Usermngt.Entities;

namespace Usermngt.BL.Service
{
    public interface IMasterDataService
    {
        #region District User Methods  
        List<DistrictUser> DistrictUsers(string userId);
        Tuple<bool, string> SaveDistrictUser(DistrictUser districtUser);
        Tuple<bool, string> UpdateDistrictUser(DistrictUser ditrictUser);
        DistrictUser LoadDistrictUserDetails(int v);
        #endregion

        #region Thana User Methods
        List<Thana> ThanaListByDistrictCode(string district_code);
        List<Thana> ThanaList(string userId);
        Tuple<bool, string> SaveThana(Thana thana);
        Tuple<bool, string> UpdateThana(Thana thana);
        Thana LoadThanaDetails(int thana_id);
        bool deleteThana(string thana_id);
        #endregion

        #region Distillery Methods
        List<Distillery> DistilleryList(string userId);
        Tuple<bool, string> SaveDistillery(Distillery distillery);
        Tuple<bool, string> UpdateDistillery(Distillery distillery);
        Distillery LoadDistilleryDetails(int distillery_id);
        #endregion

        #region Type of Liquor Methods
        List<TypeOfLiquor> TypeOfLiquorList(string userId);
        Tuple<bool, string> SaveTypeOfLiquor(TypeOfLiquor typeOfLiquor);
        Tuple<bool, string> UpdateTypeOfLiquor(TypeOfLiquor typeofLiquor);
        TypeOfLiquor LoadTypeOfLiquorDetails(int typeofLiquor_id);
        bool deleteLiqType(string typeofLiquor_id);
        #endregion

        #region Sub Liquor Type Methods
        List<SubLiquor> SubLiquorList(string userId);
        Tuple<bool, string> SaveSubLiquor(SubLiquor subLiquor);
        Tuple<bool, string> UpdateSubLiquor(SubLiquor subLiquor);
        SubLiquor LoadSubLiquorDetails(int typeofLiquor_id);
        List<SubLiquor> SubLiquorListByLiquorType(int type_of_liquor_id);
        bool DeleteSubType(string sub_type_id);
        #endregion

        #region Parameter Methods
        List<ParameterMaster> ParameterList(string userId);
        Tuple<bool, string> SaveParameter(ParameterMaster parameter);
        Tuple<bool, string> UpdateParameter(ParameterMaster parameter);
        ParameterMaster LoadParameterDetails(int parameter_master_id);
        //Bhavin
        bool deleteParameter(string parameter_master_id); 
        //End 
        #endregion

        #region Size Master Methods
        List<SizeMaster> SizeMasterList(string userId);
        Tuple<bool, string> SaveSize(SizeMaster sizeMaster);
        Tuple<bool, string> UpdateSize(SizeMaster sizeMaster);
        SizeMaster LoadSizeDetails(int size_master_id);
        bool deleteSize(string size_id);
        #endregion

        #region Brand Master Methods
        List<BrandMaster> BrandMasterList(string userId);
        Tuple<bool, string> SaveBrand(BrandMaster brandMaster);
        Tuple<bool, string> UpdateBrand(BrandMaster brandMaster);
        BrandMaster LoadBrandDetails(int brand_master_id);
        bool deleteBrand(string brand_id);
        #endregion

        #region Assign Parameter
        List<AssignParameter> ListAssignedParameters(string userId);
        Tuple<bool, string> SaveAssignedParameter(AssignParameterPostInput assignParameterInput, string user);
        Tuple<bool, string> UpdateAssignedParameter(AssignParameterPostInput parameterInfo, string user); 

        AssignUnAssignParameter GetAssignedParameterInfo(int liquorId, int subLiquorId);
        #endregion

        #region Lookups
        IDictionary<string, string> DistrictList();
        IDictionary<string, string> DepartmentList();
        #endregion


        #region Compactor
        IDictionary<int, string> UserList();
        Tuple<bool, string> SaveCompactor(Compactor compactor);
        Tuple<bool, string> UpdateCompactor(Compactor compactor);
        List<Compactor> CompactorList(string userId);
        Compactor LoadCompactorDetails(int compactor_id);
        bool deleteCompactor(string compactor_id);
        #endregion
    }
}
