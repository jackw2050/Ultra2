﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SerialPortTerminal.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("COM1")]
        public string PortName {
            get {
                return ((string)(this["PortName"]));
            }
            set {
                this["PortName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("9600")]
        public int BaudRate {
            get {
                return ((int)(this["BaudRate"]));
            }
            set {
                this["BaudRate"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("8")]
        public int DataBits {
            get {
                return ((int)(this["DataBits"]));
            }
            set {
                this["DataBits"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("None")]
        public global::System.IO.Ports.Parity Parity {
            get {
                return ((global::System.IO.Ports.Parity)(this["Parity"]));
            }
            set {
                this["Parity"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Hex")]
        public global::SerialPortTerminal.DataMode DataMode {
            get {
                return ((global::SerialPortTerminal.DataMode)(this["DataMode"]));
            }
            set {
                this["DataMode"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("One")]
        public global::System.IO.Ports.StopBits StopBits {
            get {
                return ((global::System.IO.Ports.StopBits)(this["StopBits"]));
            }
            set {
                this["StopBits"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool ClearOnOpen {
            get {
                return ((bool)(this["ClearOnOpen"]));
            }
            set {
                this["ClearOnOpen"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool ClearWithDTR {
            get {
                return ((bool)(this["ClearWithDTR"]));
            }
            set {
                this["ClearWithDTR"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("S67-tab.csv")]
        public string calFileName {
            get {
                return ((string)(this["calFileName"]));
            }
            set {
                this["calFileName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("E:\\ZLS\\")]
        public string calFilePath {
            get {
                return ((string)(this["calFilePath"]));
            }
            set {
                this["calFilePath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("sampleConfigFile.csv")]
        public string configFileName {
            get {
                return ((string)(this["configFileName"]));
            }
            set {
                this["configFileName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("e:\\ZLS\\")]
        public string configFilePath {
            get {
                return ((string)(this["configFilePath"]));
            }
            set {
                this["configFilePath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("2")]
        public int fileDateFormat {
            get {
                return ((int)(this["fileDateFormat"]));
            }
            set {
                this["fileDateFormat"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("\"E:\\\\ZLS\\\\data\\\\\"")]
        public string filePath {
            get {
                return ((string)(this["filePath"]));
            }
            set {
                this["filePath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string fileType {
            get {
                return ((string)(this["fileType"]));
            }
            set {
                this["fileType"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("\"E:\\\\ZLS\\\\data\\\\\"")]
        public string dataFilePath {
            get {
                return ((string)(this["dataFilePath"]));
            }
            set {
                this["dataFilePath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1.298317")]
        public double beamScale {
            get {
                return ((double)(this["beamScale"]));
            }
            set {
                this["beamScale"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("\"S67\"")]
        public string meterNumber {
            get {
                return ((string)(this["meterNumber"]));
            }
            set {
                this["meterNumber"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("\"zls\"")]
        public string engPassword {
            get {
                return ((string)(this["engPassword"]));
            }
            set {
                this["engPassword"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("8.4E-06")]
        public double crossPeriod {
            get {
                return ((double)(this["crossPeriod"]));
            }
            set {
                this["crossPeriod"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("8.4E-06")]
        public double longPeriod {
            get {
                return ((double)(this["longPeriod"]));
            }
            set {
                this["longPeriod"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0.0915")]
        public double crossDampFactor {
            get {
                return ((double)(this["crossDampFactor"]));
            }
            set {
                this["crossDampFactor"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0.0915")]
        public double longDampFactor {
            get {
                return ((double)(this["longDampFactor"]));
            }
            set {
                this["longDampFactor"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0.2")]
        public double crossGain {
            get {
                return ((double)(this["crossGain"]));
            }
            set {
                this["crossGain"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0.2")]
        public double longGain {
            get {
                return ((double)(this["longGain"]));
            }
            set {
                this["longGain"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0.45")]
        public double crossLead {
            get {
                return ((double)(this["crossLead"]));
            }
            set {
                this["crossLead"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0.45")]
        public double longLead {
            get {
                return ((double)(this["longLead"]));
            }
            set {
                this["longLead"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("20000")]
        public double springTensionMax {
            get {
                return ((double)(this["springTensionMax"]));
            }
            set {
                this["springTensionMax"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("\"marine\"")]
        public string modeSwitch {
            get {
                return ((string)(this["modeSwitch"]));
            }
            set {
                this["modeSwitch"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public double crossBias {
            get {
                return ((double)(this["crossBias"]));
            }
            set {
                this["crossBias"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public double longBias {
            get {
                return ((double)(this["longBias"]));
            }
            set {
                this["longBias"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string dataFileName {
            get {
                return ((string)(this["dataFileName"]));
            }
            set {
                this["dataFileName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Hi-Res")]
        public string dataAquisitionMode {
            get {
                return ((string)(this["dataAquisitionMode"]));
            }
            set {
                this["dataAquisitionMode"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public double springTension {
            get {
                return ((double)(this["springTension"]));
            }
            set {
                this["springTension"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("-2")]
        public double kFactor {
            get {
                return ((double)(this["kFactor"]));
            }
            set {
                this["kFactor"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("FOG pack")]
        public string gyroType {
            get {
                return ((string)(this["gyroType"]));
            }
            set {
                this["gyroType"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public double crossCompFactor_4 {
            get {
                return ((double)(this["crossCompFactor_4"]));
            }
            set {
                this["crossCompFactor_4"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public double crossCompPhase_4 {
            get {
                return ((double)(this["crossCompPhase_4"]));
            }
            set {
                this["crossCompPhase_4"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public double crossCompFactor_16 {
            get {
                return ((double)(this["crossCompFactor_16"]));
            }
            set {
                this["crossCompFactor_16"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public double crossCompPhase_16 {
            get {
                return ((double)(this["crossCompPhase_16"]));
            }
            set {
                this["crossCompPhase_16"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public double longCompFactor_4 {
            get {
                return ((double)(this["longCompFactor_4"]));
            }
            set {
                this["longCompFactor_4"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public double longCompPhase_4 {
            get {
                return ((double)(this["longCompPhase_4"]));
            }
            set {
                this["longCompPhase_4"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public double longCompFactor_16 {
            get {
                return ((double)(this["longCompFactor_16"]));
            }
            set {
                this["longCompFactor_16"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public double longCompPhase_16 {
            get {
                return ((double)(this["longCompPhase_16"]));
            }
            set {
                this["longCompPhase_16"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public double CML_Fact {
            get {
                return ((double)(this["CML_Fact"]));
            }
            set {
                this["CML_Fact"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public double AL_Fact {
            get {
                return ((double)(this["AL_Fact"]));
            }
            set {
                this["AL_Fact"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public double AX_Fact {
            get {
                return ((double)(this["AX_Fact"]));
            }
            set {
                this["AX_Fact"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public double VE_Fact {
            get {
                return ((double)(this["VE_Fact"]));
            }
            set {
                this["VE_Fact"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public double CMX_Fact {
            get {
                return ((double)(this["CMX_Fact"]));
            }
            set {
                this["CMX_Fact"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public double XACC2_Fact {
            get {
                return ((double)(this["XACC2_Fact"]));
            }
            set {
                this["XACC2_Fact"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public double LACC2_Fact {
            get {
                return ((double)(this["LACC2_Fact"]));
            }
            set {
                this["LACC2_Fact"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0.217")]
        public double XACC_Phase {
            get {
                return ((double)(this["XACC_Phase"]));
            }
            set {
                this["XACC_Phase"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0.6196")]
        public double LAXX_AL_Phase {
            get {
                return ((double)(this["LAXX_AL_Phase"]));
            }
            set {
                this["LAXX_AL_Phase"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0.195")]
        public double LACC_CML_Phase {
            get {
                return ((double)(this["LACC_CML_Phase"]));
            }
            set {
                this["LACC_CML_Phase"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0.185")]
        public double LACC_CMX_Phase {
            get {
                return ((double)(this["LACC_CMX_Phase"]));
            }
            set {
                this["LACC_CMX_Phase"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("password")]
        public string userPassword {
            get {
                return ((string)(this["userPassword"]));
            }
            set {
                this["userPassword"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("password")]
        public string managerPassword {
            get {
                return ((string)(this["managerPassword"]));
            }
            set {
                this["managerPassword"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("password")]
        public string zlsPassword {
            get {
                return ((string)(this["zlsPassword"]));
            }
            set {
                this["zlsPassword"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public double crossCouplingFactors1 {
            get {
                return ((double)(this["crossCouplingFactors1"]));
            }
            set {
                this["crossCouplingFactors1"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public double crossCouplingFactors2 {
            get {
                return ((double)(this["crossCouplingFactors2"]));
            }
            set {
                this["crossCouplingFactors2"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public double crossCouplingFactors3 {
            get {
                return ((double)(this["crossCouplingFactors3"]));
            }
            set {
                this["crossCouplingFactors3"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public double crossCouplingFactors4 {
            get {
                return ((double)(this["crossCouplingFactors4"]));
            }
            set {
                this["crossCouplingFactors4"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public double crossCouplingFactors5 {
            get {
                return ((double)(this["crossCouplingFactors5"]));
            }
            set {
                this["crossCouplingFactors5"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0.035199999809265137")]
        public double crossCouplingFactors6_VCC {
            get {
                return ((double)(this["crossCouplingFactors6_VCC"]));
            }
            set {
                this["crossCouplingFactors6_VCC"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("-0.0032800000626593828")]
        public double crossCouplingFactors7_AL {
            get {
                return ((double)(this["crossCouplingFactors7_AL"]));
            }
            set {
                this["crossCouplingFactors7_AL"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("-0.058649998158216476")]
        public double crossCouplingFactors8_AX {
            get {
                return ((double)(this["crossCouplingFactors8_AX"]));
            }
            set {
                this["crossCouplingFactors8_AX"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0.0094400001689791679")]
        public double crossCouplingFactors9_VE {
            get {
                return ((double)(this["crossCouplingFactors9_VE"]));
            }
            set {
                this["crossCouplingFactors9_VE"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public double crossCouplingFactors10_AX2 {
            get {
                return ((double)(this["crossCouplingFactors10_AX2"]));
            }
            set {
                this["crossCouplingFactors10_AX2"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public double crossCouplingFactors11_XACC2 {
            get {
                return ((double)(this["crossCouplingFactors11_XACC2"]));
            }
            set {
                this["crossCouplingFactors11_XACC2"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public double crossCouplingFactors12_LACC2 {
            get {
                return ((double)(this["crossCouplingFactors12_LACC2"]));
            }
            set {
                this["crossCouplingFactors12_LACC2"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("-0.0011992008658125997")]
        public double crossCouplingFactors13_CrossAxisCompensation4 {
            get {
                return ((double)(this["crossCouplingFactors13_CrossAxisCompensation4"]));
            }
            set {
                this["crossCouplingFactors13_CrossAxisCompensation4"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("-0.0013000000035390258")]
        public double crossCouplingFactors14_LongAxisCompensation4 {
            get {
                return ((double)(this["crossCouplingFactors14_LongAxisCompensation4"]));
            }
            set {
                this["crossCouplingFactors14_LongAxisCompensation4"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public double crossCouplingFactors15_CrossAxisCompensation16 {
            get {
                return ((double)(this["crossCouplingFactors15_CrossAxisCompensation16"]));
            }
            set {
                this["crossCouplingFactors15_CrossAxisCompensation16"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public double crossCouplingFactors16_LongAxisCompensation16 {
            get {
                return ((double)(this["crossCouplingFactors16_LongAxisCompensation16"]));
            }
            set {
                this["crossCouplingFactors16_LongAxisCompensation16"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public double crossCouplingFactors0 {
            get {
                return ((double)(this["crossCouplingFactors0"]));
            }
            set {
                this["crossCouplingFactors0"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public double analogFilter0 {
            get {
                return ((double)(this["analogFilter0"]));
            }
            set {
                this["analogFilter0"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0.22400000691413879")]
        public double analogFilter1_AXPhase {
            get {
                return ((double)(this["analogFilter1_AXPhase"]));
            }
            set {
                this["analogFilter1_AXPhase"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0.24250000715255737")]
        public double analogFilter2_ALPhase {
            get {
                return ((double)(this["analogFilter2_ALPhase"]));
            }
            set {
                this["analogFilter2_ALPhase"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0.20000000298023224")]
        public double analogFilter3 {
            get {
                return ((double)(this["analogFilter3"]));
            }
            set {
                this["analogFilter3"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0.28999999165534973")]
        public double analogFilter4_VCCPhase {
            get {
                return ((double)(this["analogFilter4_VCCPhase"]));
            }
            set {
                this["analogFilter4_VCCPhase"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0.60000002384185791")]
        public double analogFilter5_CrossAxisCompensationPhase4 {
            get {
                return ((double)(this["analogFilter5_CrossAxisCompensationPhase4"]));
            }
            set {
                this["analogFilter5_CrossAxisCompensationPhase4"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0.60000002384185791")]
        public double analogFilter6_LongAxisCompensationPhase4 {
            get {
                return ((double)(this["analogFilter6_LongAxisCompensationPhase4"]));
            }
            set {
                this["analogFilter6_LongAxisCompensationPhase4"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public double analogFilter8_LongAxisCompensationPhase16 {
            get {
                return ((double)(this["analogFilter8_LongAxisCompensationPhase16"]));
            }
            set {
                this["analogFilter8_LongAxisCompensationPhase16"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1")]
        public double analogFilter8 {
            get {
                return ((double)(this["analogFilter8"]));
            }
            set {
                this["analogFilter8"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public double Setting {
            get {
                return ((double)(this["Setting"]));
            }
            set {
                this["Setting"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1.0")]
        public string version {
            get {
                return ((string)(this["version"]));
            }
            set {
                this["version"] = value;
            }
        }
    }
}
