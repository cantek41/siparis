﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace siparis.Models
{

    using System;
    using System.Configuration;
    using System.Data.Entity;
    using System.Data.Entity.Core.EntityClient;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public partial class VdbSoftEntities : DbContext
    {
        public VdbSoftEntities(string Dbname)
            : base(ConnectionString(Dbname))
        {
        }

        public VdbSoftEntities()
        {
            // TODO: Complete member initialization
        }
        public static string ConnectionString(string dbname)
        {
            string _connectionString;


            string baseConnectionString = ConfigurationManager.ConnectionStrings["VdbSoftEntities"].ConnectionString;
            string connString = baseConnectionString.Replace("VdbSoft", dbname);
            var entityBuilder = new EntityConnectionStringBuilder
            {
                //Provider = "System.Data.SqlClient",
                //ProviderConnectionString = connString
                //   Metadata = @"res://*/Models.Data.csdl|res://*/Models.Data.ssdl|res://*/Models.Data.msl"
            };

            _connectionString = entityBuilder.ToString();

            return connString;

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<ACTIVITY> ACTIVITies { get; set; }
        public virtual DbSet<ACTIVITYCC> ACTIVITYCCs { get; set; }
        public virtual DbSet<ACTIVITYCCDETAIL> ACTIVITYCCDETAILs { get; set; }
        public virtual DbSet<ACTIVITYCONFLICT> ACTIVITYCONFLICTs { get; set; }
        public virtual DbSet<ACTIVITYGROUPACTIVITYTYPERELATION> ACTIVITYGROUPACTIVITYTYPERELATIONs { get; set; }
        public virtual DbSet<ACTIVITYMAILREPORT> ACTIVITYMAILREPORTs { get; set; }
        public virtual DbSet<ACTIVITYMAPLOCATIONS_silll> ACTIVITYMAPLOCATIONS_silll { get; set; }
        public virtual DbSet<ACTIVITYMEETINGJOINER> ACTIVITYMEETINGJOINERs { get; set; }
        public virtual DbSet<ACTIVITYMEETINGRESULT> ACTIVITYMEETINGRESULTs { get; set; }
        public virtual DbSet<ACTIVITYPOLL> ACTIVITYPOLLs { get; set; }
        public virtual DbSet<ACTIVITYSUBJECT> ACTIVITYSUBJECTs { get; set; }
        public virtual DbSet<ADDITIONALAREA> ADDITIONALAREAs { get; set; }
        public virtual DbSet<ADDRESS> ADDRESSes { get; set; }
        public virtual DbSet<AREMINDER> AREMINDERs { get; set; }
        public virtual DbSet<AREPEATER> AREPEATERs { get; set; }
        public virtual DbSet<aspnet_Applications> aspnet_Applications { get; set; }
        public virtual DbSet<aspnet_Membership> aspnet_Membership { get; set; }
        public virtual DbSet<aspnet_Paths> aspnet_Paths { get; set; }
        public virtual DbSet<aspnet_PersonalizationAllUsers> aspnet_PersonalizationAllUsers { get; set; }
        public virtual DbSet<aspnet_PersonalizationPerUser> aspnet_PersonalizationPerUser { get; set; }
        public virtual DbSet<aspnet_Profile> aspnet_Profile { get; set; }
        public virtual DbSet<aspnet_Roles> aspnet_Roles { get; set; }
        public virtual DbSet<aspnet_SchemaVersions> aspnet_SchemaVersions { get; set; }
        public virtual DbSet<aspnet_Users> aspnet_Users { get; set; }
        public virtual DbSet<aspnet_WebEvent_Events> aspnet_WebEvent_Events { get; set; }
        public virtual DbSet<ASSOCIATION> ASSOCIATIONs { get; set; }
        public virtual DbSet<CAR> CARs { get; set; }
        public virtual DbSet<CITEM> CITEMS { get; set; }
        public virtual DbSet<CITY> CITies { get; set; }
        public virtual DbSet<CITY2> CITY2 { get; set; }
        public virtual DbSet<CLUP> CLUPs { get; set; }
        public virtual DbSet<COMPANY> COMPANies { get; set; }
        public virtual DbSet<COMPANYGROUP> COMPANYGROUPs { get; set; }
        public virtual DbSet<COMPANYRIVALCOMPANY> COMPANYRIVALCOMPANies { get; set; }
        public virtual DbSet<COMPANYUSERRELATION> COMPANYUSERRELATIONs { get; set; }
        public virtual DbSet<COMPANYVALUE> COMPANYVALUEs { get; set; }
        public virtual DbSet<CONTACT> CONTACTs { get; set; }
        public virtual DbSet<CONTACTGROUP> CONTACTGROUPs { get; set; }
        public virtual DbSet<CONTACTUSERRELATION> CONTACTUSERRELATIONs { get; set; }
        public virtual DbSet<CONTRACT> CONTRACTs { get; set; }
        public virtual DbSet<CONTRACT_DETAIL> CONTRACT_DETAIL { get; set; }
        public virtual DbSet<COUNTRY> COUNTRies { get; set; }
        public virtual DbSet<CURTYPE> CURTYPEs { get; set; }
        public virtual DbSet<ENTEGRATION> ENTEGRATIONs { get; set; }
        public virtual DbSet<ENVIRONMENT> ENVIRONMENTs { get; set; }
        public virtual DbSet<EPARAMETER> EPARAMETERs { get; set; }
        public virtual DbSet<EXCELD> EXCELDS { get; set; }
        public virtual DbSet<EXPENSEDETAIL> EXPENSEDETAILs { get; set; }
        public virtual DbSet<EXPENSEDETAIL_SIL> EXPENSEDETAIL_SIL { get; set; }
        public virtual DbSet<FIHRIST> FIHRISTs { get; set; }
        public virtual DbSet<FIHRISTD> FIHRISTDs { get; set; }
        public virtual DbSet<FOOTTEXT> FOOTTEXTs { get; set; }
        public virtual DbSet<FORMADDITIONALAREA> FORMADDITIONALAREAs { get; set; }
        public virtual DbSet<FORMADDITIONALAREAVALUE> FORMADDITIONALAREAVALUEs { get; set; }
        public virtual DbSet<FORMDESIGN> FORMDESIGNs { get; set; }
        public virtual DbSet<FORMDESIGNERRELATION> FORMDESIGNERRELATIONs { get; set; }
        public virtual DbSet<FORMREPLACMENT> FORMREPLACMENTs { get; set; }
        public virtual DbSet<FTP> FTPs { get; set; }
        public virtual DbSet<GRIDDESIGNE> GRIDDESIGNEs { get; set; }
        public virtual DbSet<GROUPDE> GROUPDES { get; set; }
        public virtual DbSet<GROUPMULTI> GROUPMULTIs { get; set; }
        public virtual DbSet<GROUP> GROUPS { get; set; }
        public virtual DbSet<LANGUAGE> LANGUAGES { get; set; }
        public virtual DbSet<LEVELOPPORTUNITY> LEVELOPPORTUNITies { get; set; }
        public virtual DbSet<LEVELPROJECT> LEVELPROJECTs { get; set; }
        public virtual DbSet<LEVEL> LEVELS { get; set; }
        public virtual DbSet<LICENSE_PLATE> LICENSE_PLATE { get; set; }
        public virtual DbSet<LOGREPORT> LOGREPORTs { get; set; }
        public virtual DbSet<MACHINECARD> MACHINECARDs { get; set; }
        public virtual DbSet<MACHINEGROUP> MACHINEGROUPs { get; set; }
        public virtual DbSet<MACHINEPARK> MACHINEPARKs { get; set; }
        public virtual DbSet<MAINLANGUAGE> MAINLANGUAGEs { get; set; }
        public virtual DbSet<MAPLOCATION> MAPLOCATIONS { get; set; }
        public virtual DbSet<MENUD> MENUDs { get; set; }
        public virtual DbSet<NOTE> NOTES { get; set; }
        public virtual DbSet<OPPORTUNITYDETAIL> OPPORTUNITYDETAILs { get; set; }
        public virtual DbSet<OPPORTUNITYDETAILVERSION> OPPORTUNITYDETAILVERSIONs { get; set; }
        public virtual DbSet<OPPORTUNITYHISTORY> OPPORTUNITYHISTORies { get; set; }
        public virtual DbSet<OPPORTUNITYMASTER> OPPORTUNITYMASTERs { get; set; }
        public virtual DbSet<PFORM> PFORMs { get; set; }
        public virtual DbSet<PHONE> PHONEs { get; set; }
        public virtual DbSet<PHONECODE> PHONECODEs { get; set; }
        public virtual DbSet<PIVOTCOL> PIVOTCOLs { get; set; }
        public virtual DbSet<PIVOTMASTER> PIVOTMASTERs { get; set; }
        public virtual DbSet<PIVOTMEASURE> PIVOTMEASUREs { get; set; }
        public virtual DbSet<PIVOTROW> PIVOTROWs { get; set; }
        public virtual DbSet<PROGRAMPREFIX> PROGRAMPREFIXes { get; set; }
        public virtual DbSet<PROJECT> PROJECTS { get; set; }
        public virtual DbSet<RE> RES { get; set; }
        public virtual DbSet<RESDE> RESDES { get; set; }
        public virtual DbSet<REVENUEDETAIL> REVENUEDETAILs { get; set; }
        public virtual DbSet<REVENUEMASTER> REVENUEMASTERs { get; set; }
        public virtual DbSet<RIVALCOMPANY> RIVALCOMPANies { get; set; }
        public virtual DbSet<RIVALPRODUCT> RIVALPRODUCTs { get; set; }
        public virtual DbSet<RIVALPRODUCTDETAIL> RIVALPRODUCTDETAILs { get; set; }
        public virtual DbSet<SMARTMASTER> SMARTMASTERs { get; set; }
        public virtual DbSet<SMARTUSERBASE> SMARTUSERBASEs { get; set; }
        public virtual DbSet<SMARTUSERRELATION> SMARTUSERRELATIONs { get; set; }
        public virtual DbSet<SPEC> SPECs { get; set; }
        public virtual DbSet<SPECDESCIRIPTION> SPECDESCIRIPTIONs { get; set; }
        public virtual DbSet<SPEC1> SPECS1 { get; set; }
        public virtual DbSet<STOKACTUAL> STOKACTUALs { get; set; }
        public virtual DbSet<STOKACTUALORDER> STOKACTUALORDERs { get; set; }
        public virtual DbSet<STOKBODY> STOKBODies { get; set; }
        public virtual DbSet<STOKBRAND> STOKBRANDs { get; set; }
        public virtual DbSet<STOKCARD> STOKCARDs { get; set; }
        public virtual DbSet<STOKCARDPICTURE> STOKCARDPICTUREs { get; set; }
        public virtual DbSet<STOKCOLOR> STOKCOLORs { get; set; }
        public virtual DbSet<STOKGROUP> STOKGROUPs { get; set; }
        public virtual DbSet<STOKSEASON> STOKSEASONs { get; set; }
        public virtual DbSet<STOKWAREHOUSE> STOKWAREHOUSEs { get; set; }
        public virtual DbSet<STOKWAREHOUSEPRODUCT> STOKWAREHOUSEPRODUCTs { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TFILE> TFILEs { get; set; }
        public virtual DbSet<TRANSFER> TRANSFERs { get; set; }
        public virtual DbSet<TRANSFERFILE> TRANSFERFILEs { get; set; }
        public virtual DbSet<TRANSFERUSER> TRANSFERUSERS { get; set; }
        public virtual DbSet<USERACTIVITYTYPE> USERACTIVITYTYPEs { get; set; }
        public virtual DbSet<USERCOMPANYREGIONRELATION> USERCOMPANYREGIONRELATIONs { get; set; }
        public virtual DbSet<USERCOMPANYRELATION> USERCOMPANYRELATIONs { get; set; }
        public virtual DbSet<USERINFO> USERINFOes { get; set; }
        public virtual DbSet<USERREGIONRELATION> USERREGIONRELATIONs { get; set; }
        public virtual DbSet<USERRIGHTBASE> USERRIGHTBASEs { get; set; }
        public virtual DbSet<USER> USERS { get; set; }
        public virtual DbSet<USERSALARY> USERSALARies { get; set; }
        public virtual DbSet<VMMAILFILEREPORT> VMMAILFILEREPORTs { get; set; }
        public virtual DbSet<ACTIVITYDOCUMENT> ACTIVITYDOCUMENTs { get; set; }
        public virtual DbSet<ACTIVITYJOINER> ACTIVITYJOINERs { get; set; }
        public virtual DbSet<ACTIVITYPICTURE> ACTIVITYPICTUREs { get; set; }
        public virtual DbSet<ACTIVITYPROJECT> ACTIVITYPROJECTs { get; set; }
        public virtual DbSet<ACTIVITYRESOURCE> ACTIVITYRESOURCEs { get; set; }
        public virtual DbSet<ACTIVITYSHELF> ACTIVITYSHELves { get; set; }
        public virtual DbSet<ARDELETE> ARDELETEs { get; set; }
        public virtual DbSet<ARREMINDER> ARREMINDERs { get; set; }
        public virtual DbSet<BACKGROUNDCOLOR> BACKGROUNDCOLORs { get; set; }
        public virtual DbSet<BACKGROUNDPICTURE> BACKGROUNDPICTUREs { get; set; }
        public virtual DbSet<COMPANYSECTION> COMPANYSECTIONs { get; set; }
        public virtual DbSet<COMPANYSOCIALMEDIA> COMPANYSOCIALMEDIAs { get; set; }
        public virtual DbSet<CUR> CURs { get; set; }
        public virtual DbSet<EXCELD1> EXCELDs1 { get; set; }
        public virtual DbSet<MENUBACKGROUNDPICTURE> MENUBACKGROUNDPICTUREs { get; set; }
        public virtual DbSet<MENUEXE> MENUEXEs { get; set; }
        public virtual DbSet<MENULOAD> MENULOADs { get; set; }
        public virtual DbSet<MENUREPORTLOAD> MENUREPORTLOADs { get; set; }
        public virtual DbSet<PROJECTMODULE> PROJECTMODULEs { get; set; }
        public virtual DbSet<PSQL> PSQLs { get; set; }
        public virtual DbSet<RU> RUs { get; set; }
        public virtual DbSet<SELLER_BALANCE> SELLER_BALANCE { get; set; }
        public virtual DbSet<SELLER_DETAIL> SELLER_DETAIL { get; set; }
        public virtual DbSet<STOKCARDUSERPRICE> STOKCARDUSERPRICEs { get; set; }
        public virtual DbSet<USERBANK> USERBANKs { get; set; }
        public virtual DbSet<USERCASH> USERCASHes { get; set; }
        public virtual DbSet<USERWHAREHOUSE> USERWHAREHOUSEs { get; set; }
    }
}
