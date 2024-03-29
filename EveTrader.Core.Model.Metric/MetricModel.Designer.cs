﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.EntityClient;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;

[assembly: EdmSchemaAttribute()]

namespace EveTrader.Core.Model.Metric
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class MetricModel : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new MetricModel object using the connection string found in the 'MetricModel' section of the application configuration file.
        /// </summary>
        public MetricModel() : base("name=MetricModel", "MetricModel")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new MetricModel object.
        /// </summary>
        public MetricModel(string connectionString) : base(connectionString, "MetricModel")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new MetricModel object.
        /// </summary>
        public MetricModel(EntityConnection connection) : base(connection, "MetricModel")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<ItemPrices> ItemPrices
        {
            get
            {
                if ((_ItemPrices == null))
                {
                    _ItemPrices = base.CreateObjectSet<ItemPrices>("ItemPrices");
                }
                return _ItemPrices;
            }
        }
        private ObjectSet<ItemPrices> _ItemPrices;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Cache> Cache
        {
            get
            {
                if ((_Cache == null))
                {
                    _Cache = base.CreateObjectSet<Cache>("Cache");
                }
                return _Cache;
            }
        }
        private ObjectSet<Cache> _Cache;

        #endregion
        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the ItemPrices EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToItemPrices(ItemPrices itemPrices)
        {
            base.AddObject("ItemPrices", itemPrices);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Cache EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToCache(Cache cache)
        {
            base.AddObject("Cache", cache);
        }

        #endregion
    }
    

    #endregion
    
    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="MetricsModel", Name="Cache")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Cache : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Cache object.
        /// </summary>
        /// <param name="id">Initial value of the ID property.</param>
        /// <param name="requestString">Initial value of the RequestString property.</param>
        /// <param name="data">Initial value of the Data property.</param>
        /// <param name="requestDate">Initial value of the RequestDate property.</param>
        public static Cache CreateCache(global::System.Int64 id, global::System.String requestString, global::System.String data, global::System.DateTime requestDate)
        {
            Cache cache = new Cache();
            cache.ID = id;
            cache.RequestString = requestString;
            cache.Data = data;
            cache.RequestDate = requestDate;
            return cache;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int64 ID
        {
            get
            {
                return _ID;
            }
            set
            {
                if (_ID != value)
                {
                    OnIDChanging(value);
                    ReportPropertyChanging("ID");
                    _ID = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("ID");
                    OnIDChanged();
                }
            }
        }
        private global::System.Int64 _ID;
        partial void OnIDChanging(global::System.Int64 value);
        partial void OnIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String RequestString
        {
            get
            {
                return _RequestString;
            }
            set
            {
                OnRequestStringChanging(value);
                ReportPropertyChanging("RequestString");
                _RequestString = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("RequestString");
                OnRequestStringChanged();
            }
        }
        private global::System.String _RequestString;
        partial void OnRequestStringChanging(global::System.String value);
        partial void OnRequestStringChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Data
        {
            get
            {
                return _Data;
            }
            set
            {
                OnDataChanging(value);
                ReportPropertyChanging("Data");
                _Data = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Data");
                OnDataChanged();
            }
        }
        private global::System.String _Data;
        partial void OnDataChanging(global::System.String value);
        partial void OnDataChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.DateTime RequestDate
        {
            get
            {
                return _RequestDate;
            }
            set
            {
                OnRequestDateChanging(value);
                ReportPropertyChanging("RequestDate");
                _RequestDate = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("RequestDate");
                OnRequestDateChanged();
            }
        }
        private global::System.DateTime _RequestDate;
        partial void OnRequestDateChanging(global::System.DateTime value);
        partial void OnRequestDateChanged();

        #endregion
    
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="MetricsModel", Name="ItemPrices")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class ItemPrices : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new ItemPrices object.
        /// </summary>
        /// <param name="typeID">Initial value of the TypeID property.</param>
        /// <param name="lastUpload">Initial value of the LastUpload property.</param>
        /// <param name="minimumBuy">Initial value of the MinimumBuy property.</param>
        /// <param name="minimumSell">Initial value of the MinimumSell property.</param>
        /// <param name="maximumBuy">Initial value of the MaximumBuy property.</param>
        /// <param name="maximumSell">Initial value of the MaximumSell property.</param>
        /// <param name="medianBuy">Initial value of the MedianBuy property.</param>
        /// <param name="medianSell">Initial value of the MedianSell property.</param>
        /// <param name="averageBuy">Initial value of the AverageBuy property.</param>
        /// <param name="averageSell">Initial value of the AverageSell property.</param>
        /// <param name="kurtosisBuy">Initial value of the KurtosisBuy property.</param>
        /// <param name="kurtosisSell">Initial value of the KurtosisSell property.</param>
        /// <param name="skewBuy">Initial value of the SkewBuy property.</param>
        /// <param name="skewSell">Initial value of the SkewSell property.</param>
        /// <param name="varianceBuy">Initial value of the VarianceBuy property.</param>
        /// <param name="varianceSell">Initial value of the VarianceSell property.</param>
        /// <param name="standardDeviationBuy">Initial value of the StandardDeviationBuy property.</param>
        /// <param name="standardDeviationSell">Initial value of the StandardDeviationSell property.</param>
        /// <param name="simulatedBuy">Initial value of the SimulatedBuy property.</param>
        /// <param name="simulatedSell">Initial value of the SimulatedSell property.</param>
        /// <param name="regionID">Initial value of the RegionID property.</param>
        public static ItemPrices CreateItemPrices(global::System.Int64 typeID, global::System.DateTime lastUpload, global::System.Decimal minimumBuy, global::System.Decimal minimumSell, global::System.Decimal maximumBuy, global::System.Decimal maximumSell, global::System.Decimal medianBuy, global::System.Decimal medianSell, global::System.Decimal averageBuy, global::System.Decimal averageSell, global::System.Decimal kurtosisBuy, global::System.Decimal kurtosisSell, global::System.Decimal skewBuy, global::System.Decimal skewSell, global::System.Decimal varianceBuy, global::System.Decimal varianceSell, global::System.Decimal standardDeviationBuy, global::System.Decimal standardDeviationSell, global::System.Decimal simulatedBuy, global::System.Decimal simulatedSell, global::System.Int64 regionID)
        {
            ItemPrices itemPrices = new ItemPrices();
            itemPrices.TypeID = typeID;
            itemPrices.LastUpload = lastUpload;
            itemPrices.MinimumBuy = minimumBuy;
            itemPrices.MinimumSell = minimumSell;
            itemPrices.MaximumBuy = maximumBuy;
            itemPrices.MaximumSell = maximumSell;
            itemPrices.MedianBuy = medianBuy;
            itemPrices.MedianSell = medianSell;
            itemPrices.AverageBuy = averageBuy;
            itemPrices.AverageSell = averageSell;
            itemPrices.KurtosisBuy = kurtosisBuy;
            itemPrices.KurtosisSell = kurtosisSell;
            itemPrices.SkewBuy = skewBuy;
            itemPrices.SkewSell = skewSell;
            itemPrices.VarianceBuy = varianceBuy;
            itemPrices.VarianceSell = varianceSell;
            itemPrices.StandardDeviationBuy = standardDeviationBuy;
            itemPrices.StandardDeviationSell = standardDeviationSell;
            itemPrices.SimulatedBuy = simulatedBuy;
            itemPrices.SimulatedSell = simulatedSell;
            itemPrices.RegionID = regionID;
            return itemPrices;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int64 TypeID
        {
            get
            {
                return _TypeID;
            }
            set
            {
                if (_TypeID != value)
                {
                    OnTypeIDChanging(value);
                    ReportPropertyChanging("TypeID");
                    _TypeID = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("TypeID");
                    OnTypeIDChanged();
                }
            }
        }
        private global::System.Int64 _TypeID;
        partial void OnTypeIDChanging(global::System.Int64 value);
        partial void OnTypeIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.DateTime LastUpload
        {
            get
            {
                return _LastUpload;
            }
            set
            {
                OnLastUploadChanging(value);
                ReportPropertyChanging("LastUpload");
                _LastUpload = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("LastUpload");
                OnLastUploadChanged();
            }
        }
        private global::System.DateTime _LastUpload;
        partial void OnLastUploadChanging(global::System.DateTime value);
        partial void OnLastUploadChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Decimal MinimumBuy
        {
            get
            {
                return _MinimumBuy;
            }
            set
            {
                OnMinimumBuyChanging(value);
                ReportPropertyChanging("MinimumBuy");
                _MinimumBuy = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("MinimumBuy");
                OnMinimumBuyChanged();
            }
        }
        private global::System.Decimal _MinimumBuy;
        partial void OnMinimumBuyChanging(global::System.Decimal value);
        partial void OnMinimumBuyChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Decimal MinimumSell
        {
            get
            {
                return _MinimumSell;
            }
            set
            {
                OnMinimumSellChanging(value);
                ReportPropertyChanging("MinimumSell");
                _MinimumSell = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("MinimumSell");
                OnMinimumSellChanged();
            }
        }
        private global::System.Decimal _MinimumSell;
        partial void OnMinimumSellChanging(global::System.Decimal value);
        partial void OnMinimumSellChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Decimal MaximumBuy
        {
            get
            {
                return _MaximumBuy;
            }
            set
            {
                OnMaximumBuyChanging(value);
                ReportPropertyChanging("MaximumBuy");
                _MaximumBuy = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("MaximumBuy");
                OnMaximumBuyChanged();
            }
        }
        private global::System.Decimal _MaximumBuy;
        partial void OnMaximumBuyChanging(global::System.Decimal value);
        partial void OnMaximumBuyChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Decimal MaximumSell
        {
            get
            {
                return _MaximumSell;
            }
            set
            {
                OnMaximumSellChanging(value);
                ReportPropertyChanging("MaximumSell");
                _MaximumSell = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("MaximumSell");
                OnMaximumSellChanged();
            }
        }
        private global::System.Decimal _MaximumSell;
        partial void OnMaximumSellChanging(global::System.Decimal value);
        partial void OnMaximumSellChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Decimal MedianBuy
        {
            get
            {
                return _MedianBuy;
            }
            set
            {
                OnMedianBuyChanging(value);
                ReportPropertyChanging("MedianBuy");
                _MedianBuy = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("MedianBuy");
                OnMedianBuyChanged();
            }
        }
        private global::System.Decimal _MedianBuy;
        partial void OnMedianBuyChanging(global::System.Decimal value);
        partial void OnMedianBuyChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Decimal MedianSell
        {
            get
            {
                return _MedianSell;
            }
            set
            {
                OnMedianSellChanging(value);
                ReportPropertyChanging("MedianSell");
                _MedianSell = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("MedianSell");
                OnMedianSellChanged();
            }
        }
        private global::System.Decimal _MedianSell;
        partial void OnMedianSellChanging(global::System.Decimal value);
        partial void OnMedianSellChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Decimal AverageBuy
        {
            get
            {
                return _AverageBuy;
            }
            set
            {
                OnAverageBuyChanging(value);
                ReportPropertyChanging("AverageBuy");
                _AverageBuy = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("AverageBuy");
                OnAverageBuyChanged();
            }
        }
        private global::System.Decimal _AverageBuy;
        partial void OnAverageBuyChanging(global::System.Decimal value);
        partial void OnAverageBuyChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Decimal AverageSell
        {
            get
            {
                return _AverageSell;
            }
            set
            {
                OnAverageSellChanging(value);
                ReportPropertyChanging("AverageSell");
                _AverageSell = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("AverageSell");
                OnAverageSellChanged();
            }
        }
        private global::System.Decimal _AverageSell;
        partial void OnAverageSellChanging(global::System.Decimal value);
        partial void OnAverageSellChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Decimal KurtosisBuy
        {
            get
            {
                return _KurtosisBuy;
            }
            set
            {
                OnKurtosisBuyChanging(value);
                ReportPropertyChanging("KurtosisBuy");
                _KurtosisBuy = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("KurtosisBuy");
                OnKurtosisBuyChanged();
            }
        }
        private global::System.Decimal _KurtosisBuy;
        partial void OnKurtosisBuyChanging(global::System.Decimal value);
        partial void OnKurtosisBuyChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Decimal KurtosisSell
        {
            get
            {
                return _KurtosisSell;
            }
            set
            {
                OnKurtosisSellChanging(value);
                ReportPropertyChanging("KurtosisSell");
                _KurtosisSell = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("KurtosisSell");
                OnKurtosisSellChanged();
            }
        }
        private global::System.Decimal _KurtosisSell;
        partial void OnKurtosisSellChanging(global::System.Decimal value);
        partial void OnKurtosisSellChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Decimal SkewBuy
        {
            get
            {
                return _SkewBuy;
            }
            set
            {
                OnSkewBuyChanging(value);
                ReportPropertyChanging("SkewBuy");
                _SkewBuy = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("SkewBuy");
                OnSkewBuyChanged();
            }
        }
        private global::System.Decimal _SkewBuy;
        partial void OnSkewBuyChanging(global::System.Decimal value);
        partial void OnSkewBuyChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Decimal SkewSell
        {
            get
            {
                return _SkewSell;
            }
            set
            {
                OnSkewSellChanging(value);
                ReportPropertyChanging("SkewSell");
                _SkewSell = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("SkewSell");
                OnSkewSellChanged();
            }
        }
        private global::System.Decimal _SkewSell;
        partial void OnSkewSellChanging(global::System.Decimal value);
        partial void OnSkewSellChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Decimal VarianceBuy
        {
            get
            {
                return _VarianceBuy;
            }
            set
            {
                OnVarianceBuyChanging(value);
                ReportPropertyChanging("VarianceBuy");
                _VarianceBuy = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("VarianceBuy");
                OnVarianceBuyChanged();
            }
        }
        private global::System.Decimal _VarianceBuy;
        partial void OnVarianceBuyChanging(global::System.Decimal value);
        partial void OnVarianceBuyChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Decimal VarianceSell
        {
            get
            {
                return _VarianceSell;
            }
            set
            {
                OnVarianceSellChanging(value);
                ReportPropertyChanging("VarianceSell");
                _VarianceSell = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("VarianceSell");
                OnVarianceSellChanged();
            }
        }
        private global::System.Decimal _VarianceSell;
        partial void OnVarianceSellChanging(global::System.Decimal value);
        partial void OnVarianceSellChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Decimal StandardDeviationBuy
        {
            get
            {
                return _StandardDeviationBuy;
            }
            set
            {
                OnStandardDeviationBuyChanging(value);
                ReportPropertyChanging("StandardDeviationBuy");
                _StandardDeviationBuy = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("StandardDeviationBuy");
                OnStandardDeviationBuyChanged();
            }
        }
        private global::System.Decimal _StandardDeviationBuy;
        partial void OnStandardDeviationBuyChanging(global::System.Decimal value);
        partial void OnStandardDeviationBuyChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Decimal StandardDeviationSell
        {
            get
            {
                return _StandardDeviationSell;
            }
            set
            {
                OnStandardDeviationSellChanging(value);
                ReportPropertyChanging("StandardDeviationSell");
                _StandardDeviationSell = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("StandardDeviationSell");
                OnStandardDeviationSellChanged();
            }
        }
        private global::System.Decimal _StandardDeviationSell;
        partial void OnStandardDeviationSellChanging(global::System.Decimal value);
        partial void OnStandardDeviationSellChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Decimal SimulatedBuy
        {
            get
            {
                return _SimulatedBuy;
            }
            set
            {
                OnSimulatedBuyChanging(value);
                ReportPropertyChanging("SimulatedBuy");
                _SimulatedBuy = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("SimulatedBuy");
                OnSimulatedBuyChanged();
            }
        }
        private global::System.Decimal _SimulatedBuy;
        partial void OnSimulatedBuyChanging(global::System.Decimal value);
        partial void OnSimulatedBuyChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Decimal SimulatedSell
        {
            get
            {
                return _SimulatedSell;
            }
            set
            {
                OnSimulatedSellChanging(value);
                ReportPropertyChanging("SimulatedSell");
                _SimulatedSell = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("SimulatedSell");
                OnSimulatedSellChanged();
            }
        }
        private global::System.Decimal _SimulatedSell;
        partial void OnSimulatedSellChanging(global::System.Decimal value);
        partial void OnSimulatedSellChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int64 RegionID
        {
            get
            {
                return _RegionID;
            }
            set
            {
                if (_RegionID != value)
                {
                    OnRegionIDChanging(value);
                    ReportPropertyChanging("RegionID");
                    _RegionID = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("RegionID");
                    OnRegionIDChanged();
                }
            }
        }
        private global::System.Int64 _RegionID;
        partial void OnRegionIDChanging(global::System.Int64 value);
        partial void OnRegionIDChanged();

        #endregion
    
    }

    #endregion
    
}
