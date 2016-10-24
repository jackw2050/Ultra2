using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SerialPortTerminal
{
    public partial class Parameters : Form
    {

        public static Boolean updateConfigData = false;
        public Parameters()
        {
            InitializeComponent();
        }

        private void textBox27_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void crossDampingCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (crossDampingCheckBox.Checked)
            {
                crossDampingTextBox.Enabled = true;
            }
            else
            {
                crossDampingTextBox.Enabled = false;
            }
        }

        private void crossPeriodCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (crossPeriodCheckBox.Checked)
            {
                crossPeriodTextBox.Enabled = true;
            }
            else
            {
                crossPeriodTextBox.Enabled = false;
            }
        }

        private void crossGainCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (crossGainCheckBox.Checked)
            {
                crossGainTextBox.Enabled = true;
            }
            else
            {
                crossGainTextBox.Enabled = false;
            }
        }

        private void crossLeadCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (crossLeadCheckBox.Checked)
            {
                crossLeadTextBox.Enabled = true;
            }
            else
            {
                crossLeadTextBox.Enabled = false;
            }
        }

        private void crossCompFactor4CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (crossCompFactor4CheckBox.Checked)
            {
                crossCompFactor4TextBox.Enabled = true;
            }
            else
            {
                crossCompFactor4TextBox.Enabled = false;
            }
        }

        private void crossCompPhase4CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (crossCompPhase4CheckBox.Checked)
            {
                crossCompPhase4TextBox.Enabled = true;
            }
            else
            {
                crossCompPhase4TextBox.Enabled = false;
            }
        }

        private void crossCompFactor16CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (crossCompFactor16CheckBox.Checked)
            {
                crossCompFactor16TextBox.Enabled = true;
            }
            else
            {
                crossCompFactor16TextBox.Enabled = false;
            }
        }

        private void crossCompPhase16CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (crossCompPhase16CheckBox.Checked)
            {
                crossCompPhase16TextBox.Enabled = true;
            }
            else
            {
                crossCompPhase16TextBox.Enabled = false;
            }
        }

        private void longPeriodCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (longPeriodCheckBox.Checked)
            {
                longPeriodTextBox.Enabled = true;
            }
            else
            {
                longPeriodTextBox.Enabled = false;
            }
        }

        private void longDampingCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (longDampingCheckBox.Checked)
            {
                longDampingTextBox.Enabled = true;
            }
            else
            {
                longDampingTextBox.Enabled = false;
            }
        }

        private void longGainCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (longGainCheckBox.Checked )
            {
                longGainTextBox.Enabled = true;
            }
            else
            {
                longGainTextBox.Enabled = false;
            }
        }

        private void longLeadCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (longLeadCheckBox.Checked)
            {
                longLeadTextBox.Enabled = true;
            }
            else
            {
                longLeadTextBox.Enabled = false;
            }
        }

        private void longCompFactor4CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (longCompFactor4CheckBox.Checked)
            {
                longCompFactor4TextBox.Enabled = true;
            }
            else
            {
                longCompFactor4TextBox.Enabled = false;
            }
        }

        private void longCompPhase4CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (longCompPhase4CheckBox.Checked)
            {
                longCompPhase4TextBox.Enabled = true;
            }
            else
            {
                longCompPhase4TextBox.Enabled = false;
            }
        }

        private void longCompFactor16CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (longCompFactor16CheckBox.Checked)
            {
                longCompFactor16TextBox.Enabled = true;
            }
            else
            {
                longCompFactor16TextBox.Enabled = false;
            }
        }

        private void longCompPhase16CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (longCompPhase16CheckBox.Checked)
            {
                longCompPhase16TextBox.Enabled = true;
            }
            else
            {
                longCompPhase16TextBox.Enabled = false;
            }
        }

        private void CMLFactorCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (CMLFactorCheckBox.Checked)
            {
                CMLFactorTextBox.Enabled = true;
            }
            else
            {
                CMLFactorTextBox.Enabled = false;
            }
        }

        private void ALFactorCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ALFactorCheckBox.Checked)
            {
                ALFactorTextBox.Enabled = true;
            }
            else
            {
                ALFactorTextBox.Enabled = false;
            }
        }

        private void AXFactorCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (AXFactorCheckBox.Checked)
            {
                AXFactorTextBox.Enabled = true;
            }
            else
            {
                AXFactorTextBox.Enabled = false;
            }
        }

        private void VEFactorCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (VEFactorCheckBox.Checked)
            {
                VEFactorTextBox.Enabled = true;
            }
            else
            {
                VEFactorTextBox.Enabled = false;
            }
        }

        private void CMXFactorCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (CMXFactorCheckBox.Checked)
            {
                CMXFactorTextBox.Enabled = true;
            }
            else
            {
                CMXFactorTextBox.Enabled = false;
            }
        }

        private void XACC2FactorCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (XACC2FactorCheckBox.Checked)
            {
                XACC2FactorTextBox.Enabled = true;
            }
            else
            {
                XACC2FactorTextBox.Enabled = false;
            }
        }

        private void LACC2FactorCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (LACC2FactorCheckBox.Checked)
            {
                LACC2FactorTextBox.Enabled = true;
            }
            else
            {
                LACC2FactorTextBox.Enabled = false;
            }
        }

        private void XACCPhaseCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (XACCPhaseCheckBox.Checked)
            {
                XACCPhasetextBox.Enabled = true;
            }
            else
            {
                XACCPhasetextBox.Enabled = false;
            }
        }

        private void LACC_AL_PhaseCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (LACC_AL_PhaseCheckBox.Checked)
            {
                LACC_AL_PhaseTextBox.Enabled = true;
            }
            else
            {
                LACC_AL_PhaseTextBox.Enabled = true;
            }
        }

        private void LACC_CML_PhaseCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (LACC_CML_PhaseCheckBox.Checked)
            {
                LACC_CML_PhaseTextBox.Enabled = true;
            }
            else
            {
                LACC_CML_PhaseTextBox.Enabled = false;
            }
        }

        private void LACC_CMX_PhaseCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (LACC_CMX_PhaseCheckBox.Checked)
            {
                LACC_CMX_PhaseTextBox.Enabled = true;
            }
            else
            {
                LACC_CMX_PhaseTextBox.Enabled = false;
            }
        }

        private void maxSpringTensionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (maxSpringTensionCheckBox.Checked)
            {
                maxSpringTensionTextBox.Enabled = true;
            }
            else
            {
                maxSpringTensionTextBox.Enabled = false;
            }
        }

        private void gyroTypeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (gyroTypeCheckBox.Checked)
            {
                gyroTypeComboBox.Enabled = true;
            }
            else
            {
                gyroTypeComboBox.Enabled = false;
            }
        }

        private void meterNumberCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (meterNumberCheckBox.Checked)
            {
                meterNumberTextBox.Enabled = true;
            }
            else
            {
                meterNumberTextBox.Enabled = false;
            }
        }

        private void kFactorCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (kFactorCheckBox.Checked)
            {
                kFactorTextBox.Enabled = true;
            }
            else
            {
                kFactorTextBox.Enabled = false;
            }
        }

        private void screenDisplayFilterCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (screenDisplayFilterCheckBox.Checked)
            {
                screenDisplayFilterTextBox.Enabled = true;
            }
            else
            {
                screenDisplayFilterTextBox.Enabled = false;
            }
        }

        private void updateMeterButton_Click(object sender, EventArgs e)
        {
            UpdateConfigValues();
            updateConfigData = true;



        }

        private void Parameters_Load(object sender, EventArgs e)
        {

            updateConfigData = true;
        }

        private void UpdateConfigValues()
        {
            // Load configuration data from enabled parameter text boxes
            

            //============  Cross Axis Parameters ================
            if (crossPeriodCheckBox.Checked)
            {
                ConfigData.crossPeriod = Convert.ToSingle(crossPeriodTextBox.Text.Trim());
            }
            if (crossDampingCheckBox.Checked)
            {
                ConfigData.crossDampFactor = Convert.ToSingle(crossDampingTextBox.Text.Trim());
            }
            if (crossGainCheckBox.Checked)
            {
                ConfigData.crossGain = Convert.ToSingle(crossGainTextBox.Text.Trim());
            }
            if (crossLeadCheckBox.Checked)
            {
                ConfigData.crossLead = Convert.ToSingle(crossPeriodTextBox.Text.Trim());
            }
            //============  Long Axis Parameters ================

            if (longPeriodCheckBox.Checked)
            {
                ConfigData.longPeriod = Convert.ToSingle(longPeriodTextBox.Text.Trim());
            }
            if (longDampingCheckBox.Checked)
            {
                ConfigData.longDampFactor = Convert.ToSingle(longDampingTextBox.Text.Trim());
            }
            if (longGainCheckBox.Checked)
            {
                ConfigData.longGain = Convert.ToSingle(longGainTextBox.Text.Trim());
            }
            if (longLeadCheckBox.Checked)
            {
                ConfigData.longLead = Convert.ToSingle(longLeadTextBox.Text.Trim());
            }


            if (CMLFactorCheckBox.Checked)
            {
                ConfigData.CML_Fact = Convert.ToSingle(CMLFactorTextBox.Text.Trim());
            }
            if (CMXFactorCheckBox.Checked)
            {
                ConfigData.CMX_Fact = Convert.ToSingle(CMXFactorTextBox.Text.Trim());
            }
            if (ALFactorCheckBox.Checked)
            {
                ConfigData.AL_Fact = Convert.ToSingle(ALFactorTextBox.Text.Trim());
            }
            if (AXFactorCheckBox.Checked)
            {
                ConfigData.AX_Fact = Convert.ToSingle(AXFactorTextBox.Text.Trim());
            }
            if (VEFactorCheckBox.Checked)
            {
                ConfigData.VE_Fact = Convert.ToSingle(VEFactorTextBox.Text.Trim());
            }
            if (XACC2FactorCheckBox.Checked)
            {
                ConfigData.XACC2_Fact = Convert.ToSingle(XACC2FactorTextBox.Text.Trim());
            }
            if (LACC2FactorCheckBox.Checked)
            {
                ConfigData.LACC2_Fact = Convert.ToSingle(LACC2FactorTextBox.Text.Trim());
            }
            if (XACCPhaseCheckBox.Checked)
            {
                ConfigData.XACC_Phase = Convert.ToSingle(XACCPhasetextBox.Text.Trim());
            }
            if (LACC_AL_PhaseCheckBox.Checked)
            {
                ConfigData.LACC_AL_Phase = Convert.ToSingle(LACC_AL_PhaseTextBox.Text.Trim());
            }
            if (LACC_CML_PhaseCheckBox.Checked)
            {
                ConfigData.LACC_CML_Phase = Convert.ToSingle(LACC_CML_PhaseTextBox.Text.Trim());
            }
            if (LACC_CMX_PhaseCheckBox.Checked)
            {
                ConfigData.LACC_CMX_Phase = Convert.ToSingle(LACC_CMX_PhaseTextBox.Text.Trim());
            }

            //============  Misc Parameters ================

            if (meterNumberCheckBox.Checked)
            {
                ConfigData.meterNumber = meterNumberTextBox.Text.Trim();
            }
            if (maxSpringTensionCheckBox.Checked)
            {
                ConfigData.springTensionMax = Convert.ToSingle(maxSpringTensionTextBox.Text.Trim());
            }
            if (kFactorCheckBox.Checked)
            {
                ConfigData.kFactor = Convert.ToSingle(kFactorTextBox.Text.Trim());
            }
            if (gyroTypeCheckBox.Checked)
            {
                ConfigData.gyroType = gyroTypeComboBox.SelectedText;
            }


            
        }



    }
}
