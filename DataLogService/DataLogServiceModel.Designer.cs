﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.4200
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::System.Data.Objects.DataClasses.EdmSchemaAttribute()]

// Original file name:
// Generation date: 4/26/2010 11:14:38 PM
namespace DataLogService
{
    
    /// <summary>
    /// There are no comments for tinajaEntities2 in the schema.
    /// </summary>
    public partial class tinajaEntities2 : global::System.Data.Objects.ObjectContext
    {
        /// <summary>
        /// Initializes a new tinajaEntities2 object using the connection string found in the 'tinajaEntities2' section of the application configuration file.
        /// </summary>
        public tinajaEntities2() : 
                base("name=tinajaEntities2", "tinajaEntities2")
        {
            this.OnContextCreated();
        }
        /// <summary>
        /// Initialize a new tinajaEntities2 object.
        /// </summary>
        public tinajaEntities2(string connectionString) : 
                base(connectionString, "tinajaEntities2")
        {
            this.OnContextCreated();
        }
        /// <summary>
        /// Initialize a new tinajaEntities2 object.
        /// </summary>
        public tinajaEntities2(global::System.Data.EntityClient.EntityConnection connection) : 
                base(connection, "tinajaEntities2")
        {
            this.OnContextCreated();
        }
        partial void OnContextCreated();
        /// <summary>
        /// There are no comments for datalog in the schema.
        /// </summary>
        public global::System.Data.Objects.ObjectQuery<datalog> datalog
        {
            get
            {
                if ((this._datalog == null))
                {
                    this._datalog = base.CreateQuery<datalog>("[datalog]");
                }
                return this._datalog;
            }
        }
        private global::System.Data.Objects.ObjectQuery<datalog> _datalog;
        /// <summary>
        /// There are no comments for datalog in the schema.
        /// </summary>
        public void AddTodatalog(datalog datalog)
        {
            base.AddObject("datalog", datalog);
        }
    }
    /// <summary>
    /// There are no comments for tinajaLogModel.datalog in the schema.
    /// </summary>
    /// <KeyProperties>
    /// id
    /// </KeyProperties>
    [global::System.Data.Objects.DataClasses.EdmEntityTypeAttribute(NamespaceName="tinajaLogModel", Name="datalog")]
    [global::System.Runtime.Serialization.DataContractAttribute(IsReference=true)]
    [global::System.Serializable()]
    public partial class datalog : global::System.Data.Objects.DataClasses.EntityObject
    {
        /// <summary>
        /// Create a new datalog object.
        /// </summary>
        /// <param name="id">Initial value of id.</param>
        public static datalog Createdatalog(global::System.Guid id)
        {
            datalog datalog = new datalog();
            datalog.id = id;
            return datalog;
        }
        /// <summary>
        /// There are no comments for Property id in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public global::System.Guid id
        {
            get
            {
                return this._id;
            }
            set
            {
                this.OnidChanging(value);
                this.ReportPropertyChanging("id");
                this._id = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("id");
                this.OnidChanged();
            }
        }
        private global::System.Guid _id;
        partial void OnidChanging(global::System.Guid value);
        partial void OnidChanged();
        /// <summary>
        /// There are no comments for Property logtime in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public global::System.Nullable<global::System.DateTime> logtime
        {
            get
            {
                return this._logtime;
            }
            set
            {
                this.OnlogtimeChanging(value);
                this.ReportPropertyChanging("logtime");
                this._logtime = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("logtime");
                this.OnlogtimeChanged();
            }
        }
        private global::System.Nullable<global::System.DateTime> _logtime;
        partial void OnlogtimeChanging(global::System.Nullable<global::System.DateTime> value);
        partial void OnlogtimeChanged();
        /// <summary>
        /// There are no comments for Property logvalue in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public global::System.Nullable<decimal> logvalue
        {
            get
            {
                return this._logvalue;
            }
            set
            {
                this.OnlogvalueChanging(value);
                this.ReportPropertyChanging("logvalue");
                this._logvalue = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("logvalue");
                this.OnlogvalueChanged();
            }
        }
        private global::System.Nullable<decimal> _logvalue;
        partial void OnlogvalueChanging(global::System.Nullable<decimal> value);
        partial void OnlogvalueChanged();
        /// <summary>
        /// There are no comments for Property apikey in the schema.
        /// </summary>
        [global::System.Data.Objects.DataClasses.EdmScalarPropertyAttribute()]
        [global::System.Runtime.Serialization.DataMemberAttribute()]
        public string apikey
        {
            get
            {
                return this._apikey;
            }
            set
            {
                this.OnapikeyChanging(value);
                this.ReportPropertyChanging("apikey");
                this._apikey = global::System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("apikey");
                this.OnapikeyChanged();
            }
        }
        private string _apikey;
        partial void OnapikeyChanging(string value);
        partial void OnapikeyChanged();
    }
}