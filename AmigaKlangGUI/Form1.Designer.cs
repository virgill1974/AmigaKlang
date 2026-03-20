namespace AmigaKlangGUI
{

    using System.Runtime.InteropServices;
    using System.Reflection;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using System.Media;
    using System.IO;
    public partial class Form1
    {
        [DllImport("user32.dll")]
        public static extern IntPtr LoadCursorFromFile(string filename);
        public Cursor CursorAmiga = new Cursor(Cursor.Current.Handle);
        public Cursor CursorAtari = new Cursor(Cursor.Current.Handle);

        private void Form1_Load(object sender, EventArgs e)
        {

            string root = Application.StartupPath;
            string filename = root + "/pointer.cur";
            if (File.Exists(filename))
            { 
                IntPtr colorcursorhandle1 = LoadCursorFromFile(@filename);
                CursorAmiga.GetType().InvokeMember("handle", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetField, null, CursorAmiga, new object[] { colorcursorhandle1 });
            }
            filename = root + "/atari.cur";
            if (File.Exists(filename))
            {
                IntPtr colorcursorhandle2 = LoadCursorFromFile(@filename);
                CursorAtari.GetType().InvokeMember("handle", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetField, null, CursorAtari, new object[] { colorcursorhandle2 });
            }

            this.Cursor = CursorAmiga;


            //      List<Control> allControls = GetAllControls(this);
            //      allControls.ForEach(k => k.Font = myFont);

        }
/*
        private List<Control> GetAllControls(Control container, List<Control> list)
        {
            foreach (Control c in container.Controls)
            {

                if (c.Controls.Count > 0)
                    list = GetAllControls(c, list);
                else
                    list.Add(c);
            }

            return list;
        }
        private List<Control> GetAllControls(Control container)
        {
            return GetAllControls(container, new List<Control>());
        }
        */

        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.buttonPlaySample = new System.Windows.Forms.Button();
            this.ComboBoxSampleAuswahl = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.ComboBoxClamp = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.comboBoxVolValue = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.TextBoxTranslateIlen = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.ComboBoxVar1 = new System.Windows.Forms.ComboBox();
            this.ComboBoxVar2 = new System.Windows.Forms.ComboBox();
            this.ComboBoxVar3 = new System.Windows.Forms.ComboBox();
            this.ComboBoxVar4 = new System.Windows.Forms.ComboBox();
            this.ComboBoxVar5 = new System.Windows.Forms.ComboBox();
            this.ComboBoxFunction1 = new System.Windows.Forms.ComboBox();
            this.ComboBoxFunction2 = new System.Windows.Forms.ComboBox();
            this.ComboBoxFunction3 = new System.Windows.Forms.ComboBox();
            this.ComboBoxFunction4 = new System.Windows.Forms.ComboBox();
            this.ComboBoxFunction5 = new System.Windows.Forms.ComboBox();
            this.ButtonEdit1 = new System.Windows.Forms.Button();
            this.ButtonEdit2 = new System.Windows.Forms.Button();
            this.ButtonEdit3 = new System.Windows.Forms.Button();
            this.ButtonEdit4 = new System.Windows.Forms.Button();
            this.ButtonEdit5 = new System.Windows.Forms.Button();
            this.GroupBoxDistortion = new AmigaKlangGUI.MyGroupBox();
            this.label70 = new System.Windows.Forms.Label();
            this.hScrollBarDistortionGainValue = new System.Windows.Forms.HScrollBar();
            this.TextBoxDistortionGainValue = new System.Windows.Forms.TextBox();
            this.ComboBoxDistortionGain = new System.Windows.Forms.ComboBox();
            this.label25 = new System.Windows.Forms.Label();
            this.ComboBoxDistortionVal = new System.Windows.Forms.ComboBox();
            this.GroupBoxVol = new AmigaKlangGUI.MyGroupBox();
            this.label71 = new System.Windows.Forms.Label();
            this.hScrollBarVolGainValue = new System.Windows.Forms.HScrollBar();
            this.TextBoxVolGainValue = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.ComboBoxVolGain = new System.Windows.Forms.ComboBox();
            this.ComboBoxVolVal = new System.Windows.Forms.ComboBox();
            this.GroupBoxOsc_saw = new AmigaKlangGUI.MyGroupBox();
            this.hScrollBarOscsawGainValue = new System.Windows.Forms.HScrollBar();
            this.hScrollBarOscsawFreqValue = new System.Windows.Forms.HScrollBar();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TextBoxOscsawGainValue = new System.Windows.Forms.TextBox();
            this.TextBoxOscsawFreqValue = new System.Windows.Forms.TextBox();
            this.ComboBoxOscsawGain = new System.Windows.Forms.ComboBox();
            this.ComboBoxOscsawFreq = new System.Windows.Forms.ComboBox();
            this.GroupBoxOsc_tri = new AmigaKlangGUI.MyGroupBox();
            this.hScrollBarOsctriGainValue = new System.Windows.Forms.HScrollBar();
            this.hScrollBarOsctriFreqValue = new System.Windows.Forms.HScrollBar();
            this.ComboBoxOsctriFreq = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.ComboBoxOsctriGain = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.TextBoxOsctriFreqValue = new System.Windows.Forms.TextBox();
            this.TextBoxOsctriGainValue = new System.Windows.Forms.TextBox();
            this.GroupBoxOsc_sine = new AmigaKlangGUI.MyGroupBox();
            this.hScrollBarOscsineGainValue = new System.Windows.Forms.HScrollBar();
            this.hScrollBarOscsineFreqValue = new System.Windows.Forms.HScrollBar();
            this.ComboBoxOscsineFreq = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.ComboBoxOscsineGain = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.TextBoxOscsineFreqValue = new System.Windows.Forms.TextBox();
            this.TextBoxOscsineGainValue = new System.Windows.Forms.TextBox();
            this.buttonStopSample = new System.Windows.Forms.Button();
            this.GroupBoxAdd = new AmigaKlangGUI.MyGroupBox();
            this.hScrollBarAddVal2Value = new System.Windows.Forms.HScrollBar();
            this.TextBoxAddVal2Value = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.ComboBoxAddVal2 = new System.Windows.Forms.ComboBox();
            this.ComboBoxAddVal1 = new System.Windows.Forms.ComboBox();
            this.GroupBoxOsc_pulse = new AmigaKlangGUI.MyGroupBox();
            this.hScrollBarOscpulseWidthValue = new System.Windows.Forms.HScrollBar();
            this.hScrollBarOscpulseGainValue = new System.Windows.Forms.HScrollBar();
            this.hScrollBarOscpulseFreqValue = new System.Windows.Forms.HScrollBar();
            this.label32 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ComboBoxOscpulseWidth = new System.Windows.Forms.ComboBox();
            this.TextBoxOscpulseWidthValue = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.ComboBoxOscpulseGain = new System.Windows.Forms.ComboBox();
            this.TextBoxOscpulseGainValue = new System.Windows.Forms.TextBox();
            this.ComboBoxOscpulseFreq = new System.Windows.Forms.ComboBox();
            this.TextBoxOscpulseFreqValue = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.GroupBoxOsc_noise = new AmigaKlangGUI.MyGroupBox();
            this.hScrollBarOscnoiseSeedValue = new System.Windows.Forms.HScrollBar();
            this.TextBoxOscnoiseSeedValue = new System.Windows.Forms.TextBox();
            this.label64 = new System.Windows.Forms.Label();
            this.hScrollBarOscnoiseGainValue = new System.Windows.Forms.HScrollBar();
            this.label37 = new System.Windows.Forms.Label();
            this.ComboBoxOscnoiseGain = new System.Windows.Forms.ComboBox();
            this.TextBoxOscnoiseGainValue = new System.Windows.Forms.TextBox();
            this.GroupBoxEnva = new AmigaKlangGUI.MyGroupBox();
            this.hScrollBarEnvaGainValue = new System.Windows.Forms.HScrollBar();
            this.hScrollBarEnvaAttackValue = new System.Windows.Forms.HScrollBar();
            this.TextBoxEnvaAttackValue = new System.Windows.Forms.TextBox();
            this.label40 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.ComboBoxEnvaGain = new System.Windows.Forms.ComboBox();
            this.TextBoxEnvaGainValue = new System.Windows.Forms.TextBox();
            this.GroupBoxEnvd = new AmigaKlangGUI.MyGroupBox();
            this.hScrollBarEnvdGainValue = new System.Windows.Forms.HScrollBar();
            this.hScrollBarEnvdSustainValue = new System.Windows.Forms.HScrollBar();
            this.hScrollBarEnvdDecayValue = new System.Windows.Forms.HScrollBar();
            this.TextBoxEnvdSustainValue = new System.Windows.Forms.TextBox();
            this.label44 = new System.Windows.Forms.Label();
            this.TextBoxEnvdDecayValue = new System.Windows.Forms.TextBox();
            this.label39 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.ComboBoxEnvdGain = new System.Windows.Forms.ComboBox();
            this.TextBoxEnvdGainValue = new System.Windows.Forms.TextBox();
            this.GroupBoxEnvelope = new AmigaKlangGUI.MyGroupBox();
            this.hScrollBarEnvelopeReleaseValue = new System.Windows.Forms.HScrollBar();
            this.TextBoxEnvelopeReleaseValue = new System.Windows.Forms.TextBox();
            this.hScrollBarEnvelopeAttackValue = new System.Windows.Forms.HScrollBar();
            this.TextBoxEnvelopeAttackValue = new System.Windows.Forms.TextBox();
            this.label77 = new System.Windows.Forms.Label();
            this.label76 = new System.Windows.Forms.Label();
            this.hScrollBarEnvelopeGainValue = new System.Windows.Forms.HScrollBar();
            this.hScrollBarEnvelopeSustainValue = new System.Windows.Forms.HScrollBar();
            this.hScrollBarEnvelopeDecayValue = new System.Windows.Forms.HScrollBar();
            this.TextBoxEnvelopeSustainValue = new System.Windows.Forms.TextBox();
            this.label66 = new System.Windows.Forms.Label();
            this.TextBoxEnvelopeDecayValue = new System.Windows.Forms.TextBox();
            this.label67 = new System.Windows.Forms.Label();
            this.label75 = new System.Windows.Forms.Label();
            this.TextBoxEnvelopeGainValue = new System.Windows.Forms.TextBox();
            this.GroupBoxMul = new AmigaKlangGUI.MyGroupBox();
            this.label30 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.TextBoxMulVal2ValueFloat = new System.Windows.Forms.TextBox();
            this.hScrollBarMulVal2Value = new System.Windows.Forms.HScrollBar();
            this.TextBoxMulVal2Value = new System.Windows.Forms.TextBox();
            this.label45 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.ComboBoxMulVal2 = new System.Windows.Forms.ComboBox();
            this.ComboBoxMulVal1 = new System.Windows.Forms.ComboBox();
            this.GroupBoxDelay = new AmigaKlangGUI.MyGroupBox();
            this.labelDelay = new System.Windows.Forms.Label();
            this.hScrollBarDelayGainValue = new System.Windows.Forms.HScrollBar();
            this.hScrollBarDelayDelayValue = new System.Windows.Forms.HScrollBar();
            this.ComboBoxDelayDelay = new System.Windows.Forms.ComboBox();
            this.TextBoxDelayDelayValue = new System.Windows.Forms.TextBox();
            this.ComboBoxDelayValue = new System.Windows.Forms.ComboBox();
            this.label51 = new System.Windows.Forms.Label();
            this.ComboBoxDelayGain = new System.Windows.Forms.ComboBox();
            this.label52 = new System.Windows.Forms.Label();
            this.TextBoxDelayGainValue = new System.Windows.Forms.TextBox();
            this.hScrollBarSamplelength = new System.Windows.Forms.HScrollBar();
            this.ButtonEdit6 = new System.Windows.Forms.Button();
            this.ComboBoxFunction6 = new System.Windows.Forms.ComboBox();
            this.ComboBoxVar6 = new System.Windows.Forms.ComboBox();
            this.GroupBoxComb = new AmigaKlangGUI.MyGroupBox();
            this.labelComb = new System.Windows.Forms.Label();
            this.hScrollBarCombGainValue = new System.Windows.Forms.HScrollBar();
            this.hScrollBarCombFeedbackValue = new System.Windows.Forms.HScrollBar();
            this.hScrollBarCombDelayValue = new System.Windows.Forms.HScrollBar();
            this.ComboBoxCombDelay = new System.Windows.Forms.ComboBox();
            this.TextBoxCombDelayValue = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.ComboBoxCombFeedback = new System.Windows.Forms.ComboBox();
            this.TextBoxCombFeedbackValue = new System.Windows.Forms.TextBox();
            this.ComboBoxCombValue = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.ComboBoxCombGain = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.TextBoxCombGainValue = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.GroupBoxReverb = new AmigaKlangGUI.MyGroupBox();
            this.labelReverb = new System.Windows.Forms.Label();
            this.hScrollBarReverbGainValue = new System.Windows.Forms.HScrollBar();
            this.hScrollBarReverbFeedbackValue = new System.Windows.Forms.HScrollBar();
            this.label26 = new System.Windows.Forms.Label();
            this.ComboBoxReverbFeedback = new System.Windows.Forms.ComboBox();
            this.TextBoxReverbFeedbackValue = new System.Windows.Forms.TextBox();
            this.ComboBoxReverbValue = new System.Windows.Forms.ComboBox();
            this.label27 = new System.Windows.Forms.Label();
            this.ComboBoxReverbGain = new System.Windows.Forms.ComboBox();
            this.label31 = new System.Windows.Forms.Label();
            this.TextBoxReverbGainValue = new System.Windows.Forms.TextBox();
            this.GroupBoxCtrl = new AmigaKlangGUI.MyGroupBox();
            this.labelCtrl = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.ComboBoxCtrlValue = new System.Windows.Forms.ComboBox();
            this.GroupBoxFilter = new AmigaKlangGUI.MyGroupBox();
            this.hScrollBarFilterResonanceValue = new System.Windows.Forms.HScrollBar();
            this.hScrollBarFilterCutoffValue = new System.Windows.Forms.HScrollBar();
            this.ComboBoxFilterCutoff = new System.Windows.Forms.ComboBox();
            this.TextBoxFilterCutoffValue = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.ComboBoxFilterResonance = new System.Windows.Forms.ComboBox();
            this.TextBoxFilterResonanceValue = new System.Windows.Forms.TextBox();
            this.ComboBoxFilterValue = new System.Windows.Forms.ComboBox();
            this.label35 = new System.Windows.Forms.Label();
            this.ComboBoxFilterMode = new System.Windows.Forms.ComboBox();
            this.label36 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.ButtonEdit7 = new System.Windows.Forms.Button();
            this.ComboBoxFunction7 = new System.Windows.Forms.ComboBox();
            this.ComboBoxVar7 = new System.Windows.Forms.ComboBox();
            this.ButtonEdit8 = new System.Windows.Forms.Button();
            this.ComboBoxFunction8 = new System.Windows.Forms.ComboBox();
            this.ComboBoxVar8 = new System.Windows.Forms.ComboBox();
            this.ButtonEdit9 = new System.Windows.Forms.Button();
            this.ComboBoxFunction9 = new System.Windows.Forms.ComboBox();
            this.ComboBoxVar9 = new System.Windows.Forms.ComboBox();
            this.ButtonEdit10 = new System.Windows.Forms.Button();
            this.ComboBoxFunction10 = new System.Windows.Forms.ComboBox();
            this.ComboBoxVar10 = new System.Windows.Forms.ComboBox();
            this.ButtonEdit11 = new System.Windows.Forms.Button();
            this.ComboBoxFunction11 = new System.Windows.Forms.ComboBox();
            this.ComboBoxVar11 = new System.Windows.Forms.ComboBox();
            this.ButtonEdit12 = new System.Windows.Forms.Button();
            this.ComboBoxFunction12 = new System.Windows.Forms.ComboBox();
            this.ComboBoxVar12 = new System.Windows.Forms.ComboBox();
            this.ButtonEdit13 = new System.Windows.Forms.Button();
            this.ComboBoxFunction13 = new System.Windows.Forms.ComboBox();
            this.ComboBoxVar13 = new System.Windows.Forms.ComboBox();
            this.label48 = new System.Windows.Forms.Label();
            this.ButtonEdit14 = new System.Windows.Forms.Button();
            this.ComboBoxFunction14 = new System.Windows.Forms.ComboBox();
            this.ComboBoxVar14 = new System.Windows.Forms.ComboBox();
            this.ButtonEdit15 = new System.Windows.Forms.Button();
            this.ComboBoxFunction15 = new System.Windows.Forms.ComboBox();
            this.ComboBoxVar15 = new System.Windows.Forms.ComboBox();
            this.ButtonEdit16 = new System.Windows.Forms.Button();
            this.ComboBoxFunction16 = new System.Windows.Forms.ComboBox();
            this.ComboBoxVar16 = new System.Windows.Forms.ComboBox();
            this.checkBoxSampleview = new System.Windows.Forms.CheckBox();
            this.GroupBoxNodes = new AmigaKlangGUI.MyGroupBox();
            this.buttonUp16 = new System.Windows.Forms.Button();
            this.buttonDown15 = new System.Windows.Forms.Button();
            this.buttonUp15 = new System.Windows.Forms.Button();
            this.buttonDown14 = new System.Windows.Forms.Button();
            this.buttonUp14 = new System.Windows.Forms.Button();
            this.buttonDown13 = new System.Windows.Forms.Button();
            this.buttonUp13 = new System.Windows.Forms.Button();
            this.buttonDown12 = new System.Windows.Forms.Button();
            this.buttonUp12 = new System.Windows.Forms.Button();
            this.buttonDown11 = new System.Windows.Forms.Button();
            this.buttonUp11 = new System.Windows.Forms.Button();
            this.buttonDown10 = new System.Windows.Forms.Button();
            this.buttonUp10 = new System.Windows.Forms.Button();
            this.buttonDown9 = new System.Windows.Forms.Button();
            this.buttonUp9 = new System.Windows.Forms.Button();
            this.buttonDown8 = new System.Windows.Forms.Button();
            this.buttonUp8 = new System.Windows.Forms.Button();
            this.buttonDown7 = new System.Windows.Forms.Button();
            this.buttonUp7 = new System.Windows.Forms.Button();
            this.buttonDown6 = new System.Windows.Forms.Button();
            this.buttonUp6 = new System.Windows.Forms.Button();
            this.buttonDown5 = new System.Windows.Forms.Button();
            this.buttonUp5 = new System.Windows.Forms.Button();
            this.buttonDown4 = new System.Windows.Forms.Button();
            this.buttonUp4 = new System.Windows.Forms.Button();
            this.buttonDown3 = new System.Windows.Forms.Button();
            this.buttonUp3 = new System.Windows.Forms.Button();
            this.buttonDown2 = new System.Windows.Forms.Button();
            this.buttonUp2 = new System.Windows.Forms.Button();
            this.buttonDown1 = new System.Windows.Forms.Button();
            this.label78 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.label49 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.groupBoxPlay = new AmigaKlangGUI.MyGroupBox();
            this.label68 = new System.Windows.Forms.Label();
            this.buttonGenerateAll = new System.Windows.Forms.Button();
            this.ComboBoxNotes = new System.Windows.Forms.ComboBox();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.GroupBoxChord = new AmigaKlangGUI.MyGroupBox();
            this.labelChordgen = new System.Windows.Forms.Label();
            this.labelChordName = new System.Windows.Forms.Label();
            this.hScrollBarChordShiftValue = new System.Windows.Forms.HScrollBar();
            this.TextBoxChordShiftValue = new System.Windows.Forms.TextBox();
            this.label58 = new System.Windows.Forms.Label();
            this.ComboBoxChordNote3 = new System.Windows.Forms.ComboBox();
            this.label57 = new System.Windows.Forms.Label();
            this.ComboBoxChordNote2 = new System.Windows.Forms.ComboBox();
            this.label59 = new System.Windows.Forms.Label();
            this.ComboBoxChordShift = new System.Windows.Forms.ComboBox();
            this.label56 = new System.Windows.Forms.Label();
            this.ComboBoxChordNote1 = new System.Windows.Forms.ComboBox();
            this.label55 = new System.Windows.Forms.Label();
            this.ComboBoxChordSamplenr = new System.Windows.Forms.ComboBox();
            this.GroupBoxLoop = new AmigaKlangGUI.MyGroupBox();
            this.TextBoxLoopLengthValue = new System.Windows.Forms.TextBox();
            this.TextBoxLoopOffsetValue = new System.Windows.Forms.TextBox();
            this.label61 = new System.Windows.Forms.Label();
            this.label60 = new System.Windows.Forms.Label();
            this.hScrollBarLoopOffsetValue = new System.Windows.Forms.HScrollBar();
            this.GroupBoxClone = new AmigaKlangGUI.MyGroupBox();
            this.hScrollBarCloneTransposeValue = new System.Windows.Forms.HScrollBar();
            this.TextBoxCloneTransposeValue = new System.Windows.Forms.TextBox();
            this.label79 = new System.Windows.Forms.Label();
            this.ComboBoxCloneTranspose = new System.Windows.Forms.ComboBox();
            this.label69 = new System.Windows.Forms.Label();
            this.TextBoxCloneOffsetValue = new System.Windows.Forms.TextBox();
            this.hScrollBarCloneOffsetValue = new System.Windows.Forms.HScrollBar();
            this.labelCloneName = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.ComboBoxCloneReverse = new System.Windows.Forms.ComboBox();
            this.label50 = new System.Windows.Forms.Label();
            this.ComboBoxCloneSamplenr = new System.Windows.Forms.ComboBox();
            this.hScrollBarZoomScroll = new System.Windows.Forms.HScrollBar();
            this.hScrollBarZoomZoom = new System.Windows.Forms.HScrollBar();
            this.buttonZoomReset = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxSamplelengthDec = new System.Windows.Forms.TextBox();
            this.groupBoxSampleLength = new AmigaKlangGUI.MyGroupBox();
            this.TextBoxTranslateInst = new System.Windows.Forms.TextBox();
            this.TextBoxTranslateIset = new System.Windows.Forms.TextBox();
            this.groupBoxInstruments = new AmigaKlangGUI.MyGroupBox();
            this.labelInstrumentNr = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.GroupBoxSh = new AmigaKlangGUI.MyGroupBox();
            this.hScrollBarShStepValue = new System.Windows.Forms.HScrollBar();
            this.TextBoxShStepValue = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.ComboBoxShStep = new System.Windows.Forms.ComboBox();
            this.ComboBoxShVal = new System.Windows.Forms.ComboBox();
            this.TextBoxImportSampleLength = new System.Windows.Forms.TextBox();
            this.buttonImportSample = new System.Windows.Forms.Button();
            this.GroupBoxImport = new AmigaKlangGUI.MyGroupBox();
            this.labelImportedName = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.ComboBoxImportNumber = new System.Windows.Forms.ComboBox();
            this.groupBoxImportSample = new AmigaKlangGUI.MyGroupBox();
            this.buttonClearImportSample = new System.Windows.Forms.Button();
            this.label63 = new System.Windows.Forms.Label();
            this.TextBoxImportSampleLengthTotal = new System.Windows.Forms.TextBox();
            this.TextBoxImportSampleLengthDec = new System.Windows.Forms.TextBox();
            this.comboBoxSelectImport = new System.Windows.Forms.ComboBox();
            this.TextBoxTranslateIswitch = new System.Windows.Forms.TextBox();
            this.label65 = new System.Windows.Forms.Label();
            this.labelTotalSize = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label47 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.buttonGenerateSingleSample = new System.Windows.Forms.Button();
            this.TextBoxTranslateDan = new System.Windows.Forms.TextBox();
            this.GroupBoxOnepole = new AmigaKlangGUI.MyGroupBox();
            this.hScrollBarOnepoleCutoffValue = new System.Windows.Forms.HScrollBar();
            this.ComboBoxOnepoleCutoff = new System.Windows.Forms.ComboBox();
            this.TextBoxOnepoleCutoffValue = new System.Windows.Forms.TextBox();
            this.ComboBoxOnepoleValue = new System.Windows.Forms.ComboBox();
            this.label72 = new System.Windows.Forms.Label();
            this.ComboBoxOnepoleMode = new System.Windows.Forms.ComboBox();
            this.label73 = new System.Windows.Forms.Label();
            this.label74 = new System.Windows.Forms.Label();
            this.radioButtonColum1 = new System.Windows.Forms.RadioButton();
            this.radioButtonColum2 = new System.Windows.Forms.RadioButton();
            this.radioButtonColum16 = new System.Windows.Forms.RadioButton();
            this.radioButtonOutput = new System.Windows.Forms.RadioButton();
            this.radioButtonColum15 = new System.Windows.Forms.RadioButton();
            this.radioButtonColum3 = new System.Windows.Forms.RadioButton();
            this.radioButtonColum4 = new System.Windows.Forms.RadioButton();
            this.radioButtonColum5 = new System.Windows.Forms.RadioButton();
            this.radioButtonColum6 = new System.Windows.Forms.RadioButton();
            this.radioButtonColum7 = new System.Windows.Forms.RadioButton();
            this.radioButtonColum8 = new System.Windows.Forms.RadioButton();
            this.radioButtonColum9 = new System.Windows.Forms.RadioButton();
            this.radioButtonColum10 = new System.Windows.Forms.RadioButton();
            this.radioButtonColum11 = new System.Windows.Forms.RadioButton();
            this.radioButtonColum12 = new System.Windows.Forms.RadioButton();
            this.radioButtonColum13 = new System.Windows.Forms.RadioButton();
            this.radioButtonColum14 = new System.Windows.Forms.RadioButton();
            this.GroupBoxVocoder = new AmigaKlangGUI.MyGroupBox();
            this.Band5Reso = new System.Windows.Forms.TextBox();
            this.label82 = new System.Windows.Forms.Label();
            this.Band5Cut = new System.Windows.Forms.TextBox();
            this.ComboBoxVocoderCarrier = new System.Windows.Forms.ComboBox();
            this.Band4Reso = new System.Windows.Forms.TextBox();
            this.label80 = new System.Windows.Forms.Label();
            this.Band4Cut = new System.Windows.Forms.TextBox();
            this.ComboBoxVocoderModulator = new System.Windows.Forms.ComboBox();
            this.Band3Reso = new System.Windows.Forms.TextBox();
            this.label81 = new System.Windows.Forms.Label();
            this.Band3Cut = new System.Windows.Forms.TextBox();
            this.Band1Cut = new System.Windows.Forms.TextBox();
            this.Band2Reso = new System.Windows.Forms.TextBox();
            this.Band1Reso = new System.Windows.Forms.TextBox();
            this.Band2Cut = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemLoadInstrument = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemLoadPatch = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSaveInstrument = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSavePatch = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSaveSample = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemExport = new System.Windows.Forms.ToolStripMenuItem();
            this.exportGeneratorFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportBinaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportAtariExeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.clearAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemDesign = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemAmiga = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemAtari = new System.Windows.Forms.ToolStripMenuItem();
            this.GroupBoxDistortion.SuspendLayout();
            this.GroupBoxVol.SuspendLayout();
            this.GroupBoxOsc_saw.SuspendLayout();
            this.GroupBoxOsc_tri.SuspendLayout();
            this.GroupBoxOsc_sine.SuspendLayout();
            this.GroupBoxAdd.SuspendLayout();
            this.GroupBoxOsc_pulse.SuspendLayout();
            this.GroupBoxOsc_noise.SuspendLayout();
            this.GroupBoxEnva.SuspendLayout();
            this.GroupBoxEnvd.SuspendLayout();
            this.GroupBoxEnvelope.SuspendLayout();
            this.GroupBoxMul.SuspendLayout();
            this.GroupBoxDelay.SuspendLayout();
            this.GroupBoxComb.SuspendLayout();
            this.GroupBoxReverb.SuspendLayout();
            this.GroupBoxCtrl.SuspendLayout();
            this.GroupBoxFilter.SuspendLayout();
            this.GroupBoxNodes.SuspendLayout();
            this.groupBoxPlay.SuspendLayout();
            this.GroupBoxChord.SuspendLayout();
            this.GroupBoxLoop.SuspendLayout();
            this.GroupBoxClone.SuspendLayout();
            this.groupBoxSampleLength.SuspendLayout();
            this.groupBoxInstruments.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.GroupBoxSh.SuspendLayout();
            this.GroupBoxImport.SuspendLayout();
            this.groupBoxImportSample.SuspendLayout();
            this.GroupBoxOnepole.SuspendLayout();
            this.GroupBoxVocoder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonPlaySample
            // 
            this.buttonPlaySample.ForeColor = System.Drawing.Color.Black;
            this.buttonPlaySample.Location = new System.Drawing.Point(108, 17);
            this.buttonPlaySample.Name = "buttonPlaySample";
            this.buttonPlaySample.Size = new System.Drawing.Size(98, 23);
            this.buttonPlaySample.TabIndex = 0;
            this.buttonPlaySample.Text = "Play Sample";
            this.buttonPlaySample.UseVisualStyleBackColor = true;
            this.buttonPlaySample.Click += new System.EventHandler(this.buttonPlaySample_Click);
            // 
            // ComboBoxSampleAuswahl
            // 
            this.ComboBoxSampleAuswahl.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboBoxSampleAuswahl.FormattingEnabled = true;
            this.ComboBoxSampleAuswahl.Items.AddRange(new object[] {
            "Instrument_1",
            "Instrument_2",
            "Instrument_3",
            "Instrument_4",
            "Instrument_5",
            "Instrument_6",
            "Instrument_7",
            "Instrument_8",
            "Instrument_9",
            "Instrument_10",
            "Instrument_11",
            "Instrument_12",
            "Instrument_13",
            "Instrument_14",
            "Instrument_15",
            "Instrument_16",
            "Instrument_17",
            "Instrument_18",
            "Instrument_19",
            "Instrument_20",
            "Instrument_21",
            "Instrument_22",
            "Instrument_23",
            "Instrument_24",
            "Instrument_25",
            "Instrument_26",
            "Instrument_27",
            "Instrument_28",
            "Instrument_29",
            "Instrument_30",
            "Instrument_31"});
            this.ComboBoxSampleAuswahl.Location = new System.Drawing.Point(6, 27);
            this.ComboBoxSampleAuswahl.MaxDropDownItems = 31;
            this.ComboBoxSampleAuswahl.Name = "ComboBoxSampleAuswahl";
            this.ComboBoxSampleAuswahl.Size = new System.Drawing.Size(216, 22);
            this.ComboBoxSampleAuswahl.TabIndex = 1;
            this.ComboBoxSampleAuswahl.SelectedIndexChanged += new System.EventHandler(this.ComboBoxSampleAuswahl_SelectedIndexChanged);
            this.ComboBoxSampleAuswahl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ComboBoxSampleAuswahl_KeyDown);
            this.ComboBoxSampleAuswahl.Leave += new System.EventHandler(this.ComboBoxSampleAuswahl_Leave);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(383, 474);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(24, 19);
            this.button2.TabIndex = 2;
            this.button2.Text = "Encode";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ComboBoxClamp
            // 
            this.ComboBoxClamp.Location = new System.Drawing.Point(0, -2);
            this.ComboBoxClamp.Name = "ComboBoxClamp";
            this.ComboBoxClamp.Size = new System.Drawing.Size(121, 21);
            this.ComboBoxClamp.TabIndex = 0;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(0, -2);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(100, 23);
            this.label11.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(92, 25);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(60, 20);
            this.textBox1.TabIndex = 32;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(89, 9);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(29, 13);
            this.label13.TabIndex = 31;
            this.label13.Text = "Gain";
            // 
            // comboBoxVolValue
            // 
            this.comboBoxVolValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxVolValue.FormattingEnabled = true;
            this.comboBoxVolValue.Items.AddRange(new object[] {
            "V1",
            "V2",
            "V3",
            "V4"});
            this.comboBoxVolValue.Location = new System.Drawing.Point(6, 25);
            this.comboBoxVolValue.Name = "comboBoxVolValue";
            this.comboBoxVolValue.Size = new System.Drawing.Size(65, 21);
            this.comboBoxVolValue.TabIndex = 30;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(34, 13);
            this.label12.TabIndex = 0;
            this.label12.Text = "Value";
            // 
            // TextBoxTranslateIlen
            // 
            this.TextBoxTranslateIlen.AcceptsReturn = true;
            this.TextBoxTranslateIlen.AcceptsTab = true;
            this.TextBoxTranslateIlen.Location = new System.Drawing.Point(383, 198);
            this.TextBoxTranslateIlen.Multiline = true;
            this.TextBoxTranslateIlen.Name = "TextBoxTranslateIlen";
            this.TextBoxTranslateIlen.ReadOnly = true;
            this.TextBoxTranslateIlen.Size = new System.Drawing.Size(25, 75);
            this.TextBoxTranslateIlen.TabIndex = 109;
            this.TextBoxTranslateIlen.Visible = false;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(5, 17);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 20);
            this.textBox3.TabIndex = 4;
            this.textBox3.Text = "0";
            this.textBox3.Click += new System.EventHandler(this.textBox3_Click);
            this.textBox3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox3_KeyDown);
            // 
            // ComboBoxVar1
            // 
            this.ComboBoxVar1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxVar1.FormattingEnabled = true;
            this.ComboBoxVar1.Items.AddRange(new object[] {
            "-",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxVar1.Location = new System.Drawing.Point(23, 14);
            this.ComboBoxVar1.Name = "ComboBoxVar1";
            this.ComboBoxVar1.Size = new System.Drawing.Size(45, 21);
            this.ComboBoxVar1.TabIndex = 8;
            this.ComboBoxVar1.SelectedIndexChanged += new System.EventHandler(this.ComboBoxVar1_SelectedIndexChanged);
            // 
            // ComboBoxVar2
            // 
            this.ComboBoxVar2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxVar2.FormattingEnabled = true;
            this.ComboBoxVar2.Items.AddRange(new object[] {
            "-",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxVar2.Location = new System.Drawing.Point(23, 41);
            this.ComboBoxVar2.Name = "ComboBoxVar2";
            this.ComboBoxVar2.Size = new System.Drawing.Size(45, 21);
            this.ComboBoxVar2.TabIndex = 9;
            this.ComboBoxVar2.SelectedIndexChanged += new System.EventHandler(this.ComboBoxVar2_SelectedIndexChanged);
            // 
            // ComboBoxVar3
            // 
            this.ComboBoxVar3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxVar3.FormattingEnabled = true;
            this.ComboBoxVar3.Items.AddRange(new object[] {
            "-",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxVar3.Location = new System.Drawing.Point(23, 68);
            this.ComboBoxVar3.Name = "ComboBoxVar3";
            this.ComboBoxVar3.Size = new System.Drawing.Size(45, 21);
            this.ComboBoxVar3.TabIndex = 10;
            this.ComboBoxVar3.SelectedIndexChanged += new System.EventHandler(this.ComboBoxVar3_SelectedIndexChanged);
            // 
            // ComboBoxVar4
            // 
            this.ComboBoxVar4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxVar4.FormattingEnabled = true;
            this.ComboBoxVar4.Items.AddRange(new object[] {
            "-",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxVar4.Location = new System.Drawing.Point(23, 95);
            this.ComboBoxVar4.Name = "ComboBoxVar4";
            this.ComboBoxVar4.Size = new System.Drawing.Size(45, 21);
            this.ComboBoxVar4.TabIndex = 11;
            this.ComboBoxVar4.SelectedIndexChanged += new System.EventHandler(this.ComboBoxVar4_SelectedIndexChanged);
            // 
            // ComboBoxVar5
            // 
            this.ComboBoxVar5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxVar5.FormattingEnabled = true;
            this.ComboBoxVar5.Items.AddRange(new object[] {
            "-",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxVar5.Location = new System.Drawing.Point(23, 122);
            this.ComboBoxVar5.Name = "ComboBoxVar5";
            this.ComboBoxVar5.Size = new System.Drawing.Size(45, 21);
            this.ComboBoxVar5.TabIndex = 12;
            this.ComboBoxVar5.SelectedIndexChanged += new System.EventHandler(this.ComboBoxVar5_SelectedIndexChanged);
            // 
            // ComboBoxFunction1
            // 
            this.ComboBoxFunction1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxFunction1.FormattingEnabled = true;
            this.ComboBoxFunction1.Items.AddRange(new object[] {
            "-",
            "Volume",
            "Osc Saw",
            "Osc Triangle",
            "Osc Sine",
            "Osc Pulse",
            "Osc Noise",
            "Attack Envelope",
            "Decay Envelope",
            "Add",
            "Multiply",
            "Delay",
            "Comb Filter",
            "Reverb",
            "Ctrl Signal",
            "SV Filter",
            "Distortion",
            "Clone Sample",
            "Chord Gen",
            "Sample and Hold",
            "Imported Sample",
            "1-Pole Filter",
            "---",
            "ADSR Envelope"});
            this.ComboBoxFunction1.Location = new System.Drawing.Point(74, 14);
            this.ComboBoxFunction1.Name = "ComboBoxFunction1";
            this.ComboBoxFunction1.Size = new System.Drawing.Size(104, 21);
            this.ComboBoxFunction1.TabIndex = 13;
            this.ComboBoxFunction1.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxFunction1_SelectionChangeCommitted);
            // 
            // ComboBoxFunction2
            // 
            this.ComboBoxFunction2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxFunction2.FormattingEnabled = true;
            this.ComboBoxFunction2.Items.AddRange(new object[] {
            "-",
            "Volume",
            "Osc Saw",
            "Osc Triangle",
            "Osc Sine",
            "Osc Pulse",
            "Osc Noise",
            "Attack Envelope",
            "Decay Envelope",
            "Add",
            "Multiply",
            "Delay",
            "Comb Filter",
            "Reverb",
            "Ctrl Signal",
            "SV Filter",
            "Distortion",
            "Clone Sample",
            "Chord Gen",
            "Sample and Hold",
            "Imported Sample",
            "1-Pole Filter",
            "---",
            "ADSR Envelope"});
            this.ComboBoxFunction2.Location = new System.Drawing.Point(74, 41);
            this.ComboBoxFunction2.Name = "ComboBoxFunction2";
            this.ComboBoxFunction2.Size = new System.Drawing.Size(104, 21);
            this.ComboBoxFunction2.TabIndex = 14;
            this.ComboBoxFunction2.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxFunction2_SelectionChangeCommitted);
            // 
            // ComboBoxFunction3
            // 
            this.ComboBoxFunction3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxFunction3.FormattingEnabled = true;
            this.ComboBoxFunction3.Items.AddRange(new object[] {
            "-",
            "Volume",
            "Osc Saw",
            "Osc Triangle",
            "Osc Sine",
            "Osc Pulse",
            "Osc Noise",
            "Attack Envelope",
            "Decay Envelope",
            "Add",
            "Multiply",
            "Delay",
            "Comb Filter",
            "Reverb",
            "Ctrl Signal",
            "SV Filter",
            "Distortion",
            "Clone Sample",
            "Chord Gen",
            "Sample and Hold",
            "Imported Sample",
            "1-Pole Filter",
            "---",
            "ADSR Envelope"});
            this.ComboBoxFunction3.Location = new System.Drawing.Point(74, 68);
            this.ComboBoxFunction3.Name = "ComboBoxFunction3";
            this.ComboBoxFunction3.Size = new System.Drawing.Size(104, 21);
            this.ComboBoxFunction3.TabIndex = 15;
            this.ComboBoxFunction3.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxFunction3_SelectionChangeCommitted);
            // 
            // ComboBoxFunction4
            // 
            this.ComboBoxFunction4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxFunction4.FormattingEnabled = true;
            this.ComboBoxFunction4.Items.AddRange(new object[] {
            "-",
            "Volume",
            "Osc Saw",
            "Osc Triangle",
            "Osc Sine",
            "Osc Pulse",
            "Osc Noise",
            "Attack Envelope",
            "Decay Envelope",
            "Add",
            "Multiply",
            "Delay",
            "Comb Filter",
            "Reverb",
            "Ctrl Signal",
            "SV Filter",
            "Distortion",
            "Clone Sample",
            "Chord Gen",
            "Sample and Hold",
            "Imported Sample",
            "1-Pole Filter",
            "---",
            "ADSR Envelope"});
            this.ComboBoxFunction4.Location = new System.Drawing.Point(74, 95);
            this.ComboBoxFunction4.Name = "ComboBoxFunction4";
            this.ComboBoxFunction4.Size = new System.Drawing.Size(104, 21);
            this.ComboBoxFunction4.TabIndex = 16;
            this.ComboBoxFunction4.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxFunction4_SelectionChangeCommitted);
            // 
            // ComboBoxFunction5
            // 
            this.ComboBoxFunction5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxFunction5.FormattingEnabled = true;
            this.ComboBoxFunction5.Items.AddRange(new object[] {
            "-",
            "Volume",
            "Osc Saw",
            "Osc Triangle",
            "Osc Sine",
            "Osc Pulse",
            "Osc Noise",
            "Attack Envelope",
            "Decay Envelope",
            "Add",
            "Multiply",
            "Delay",
            "Comb Filter",
            "Reverb",
            "Ctrl Signal",
            "SV Filter",
            "Distortion",
            "Clone Sample",
            "Chord Gen",
            "Sample and Hold",
            "Imported Sample",
            "1-Pole Filter",
            "---",
            "ADSR Envelope"});
            this.ComboBoxFunction5.Location = new System.Drawing.Point(74, 122);
            this.ComboBoxFunction5.Name = "ComboBoxFunction5";
            this.ComboBoxFunction5.Size = new System.Drawing.Size(104, 21);
            this.ComboBoxFunction5.TabIndex = 17;
            this.ComboBoxFunction5.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxFunction5_SelectionChangeCommitted);
            // 
            // ButtonEdit1
            // 
            this.ButtonEdit1.ForeColor = System.Drawing.Color.Black;
            this.ButtonEdit1.Location = new System.Drawing.Point(182, 14);
            this.ButtonEdit1.Name = "ButtonEdit1";
            this.ButtonEdit1.Size = new System.Drawing.Size(43, 23);
            this.ButtonEdit1.TabIndex = 18;
            this.ButtonEdit1.Text = "Edit";
            this.ButtonEdit1.UseVisualStyleBackColor = true;
            this.ButtonEdit1.Click += new System.EventHandler(this.ButtonEdit1_Click);
            // 
            // ButtonEdit2
            // 
            this.ButtonEdit2.ForeColor = System.Drawing.Color.Black;
            this.ButtonEdit2.Location = new System.Drawing.Point(182, 41);
            this.ButtonEdit2.Name = "ButtonEdit2";
            this.ButtonEdit2.Size = new System.Drawing.Size(43, 23);
            this.ButtonEdit2.TabIndex = 19;
            this.ButtonEdit2.Text = "Edit";
            this.ButtonEdit2.UseVisualStyleBackColor = true;
            this.ButtonEdit2.Click += new System.EventHandler(this.ButtonEdit2_Click);
            // 
            // ButtonEdit3
            // 
            this.ButtonEdit3.ForeColor = System.Drawing.Color.Black;
            this.ButtonEdit3.Location = new System.Drawing.Point(183, 68);
            this.ButtonEdit3.Name = "ButtonEdit3";
            this.ButtonEdit3.Size = new System.Drawing.Size(43, 23);
            this.ButtonEdit3.TabIndex = 20;
            this.ButtonEdit3.Text = "Edit";
            this.ButtonEdit3.UseVisualStyleBackColor = true;
            this.ButtonEdit3.Click += new System.EventHandler(this.ButtonEdit3_Click);
            // 
            // ButtonEdit4
            // 
            this.ButtonEdit4.ForeColor = System.Drawing.Color.Black;
            this.ButtonEdit4.Location = new System.Drawing.Point(183, 95);
            this.ButtonEdit4.Name = "ButtonEdit4";
            this.ButtonEdit4.Size = new System.Drawing.Size(43, 23);
            this.ButtonEdit4.TabIndex = 21;
            this.ButtonEdit4.Text = "Edit";
            this.ButtonEdit4.UseVisualStyleBackColor = true;
            this.ButtonEdit4.Click += new System.EventHandler(this.ButtonEdit4_Click);
            // 
            // ButtonEdit5
            // 
            this.ButtonEdit5.ForeColor = System.Drawing.Color.Black;
            this.ButtonEdit5.Location = new System.Drawing.Point(183, 122);
            this.ButtonEdit5.Name = "ButtonEdit5";
            this.ButtonEdit5.Size = new System.Drawing.Size(43, 23);
            this.ButtonEdit5.TabIndex = 22;
            this.ButtonEdit5.Text = "Edit";
            this.ButtonEdit5.UseVisualStyleBackColor = true;
            this.ButtonEdit5.Click += new System.EventHandler(this.ButtonEdit5_Click);
            // 
            // GroupBoxDistortion
            // 
            this.GroupBoxDistortion.BorderColor = System.Drawing.Color.White;
            this.GroupBoxDistortion.Controls.Add(this.label70);
            this.GroupBoxDistortion.Controls.Add(this.hScrollBarDistortionGainValue);
            this.GroupBoxDistortion.Controls.Add(this.TextBoxDistortionGainValue);
            this.GroupBoxDistortion.Controls.Add(this.ComboBoxDistortionGain);
            this.GroupBoxDistortion.Controls.Add(this.label25);
            this.GroupBoxDistortion.Controls.Add(this.ComboBoxDistortionVal);
            this.GroupBoxDistortion.ForeColor = System.Drawing.Color.White;
            this.GroupBoxDistortion.Location = new System.Drawing.Point(416, 198);
            this.GroupBoxDistortion.Name = "GroupBoxDistortion";
            this.GroupBoxDistortion.Size = new System.Drawing.Size(212, 260);
            this.GroupBoxDistortion.TabIndex = 23;
            this.GroupBoxDistortion.TabStop = false;
            this.GroupBoxDistortion.Text = "Distortion";
            this.GroupBoxDistortion.Visible = false;
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.Location = new System.Drawing.Point(6, 196);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(29, 13);
            this.label70.TabIndex = 65;
            this.label70.Text = "Gain";
            // 
            // hScrollBarDistortionGainValue
            // 
            this.hScrollBarDistortionGainValue.LargeChange = 1;
            this.hScrollBarDistortionGainValue.Location = new System.Drawing.Point(80, 196);
            this.hScrollBarDistortionGainValue.Maximum = 127;
            this.hScrollBarDistortionGainValue.Name = "hScrollBarDistortionGainValue";
            this.hScrollBarDistortionGainValue.Size = new System.Drawing.Size(120, 18);
            this.hScrollBarDistortionGainValue.TabIndex = 64;
            this.hScrollBarDistortionGainValue.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarDistortionGainValue_Scroll);
            // 
            // TextBoxDistortionGainValue
            // 
            this.TextBoxDistortionGainValue.Location = new System.Drawing.Point(80, 214);
            this.TextBoxDistortionGainValue.Name = "TextBoxDistortionGainValue";
            this.TextBoxDistortionGainValue.Size = new System.Drawing.Size(120, 20);
            this.TextBoxDistortionGainValue.TabIndex = 63;
            this.TextBoxDistortionGainValue.Text = "0";
            this.TextBoxDistortionGainValue.Click += new System.EventHandler(this.TextBoxDistortionGainValue_Click);
            this.TextBoxDistortionGainValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxDistortionGainValue_KeyDown);
            // 
            // ComboBoxDistortionGain
            // 
            this.ComboBoxDistortionGain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxDistortionGain.FormattingEnabled = true;
            this.ComboBoxDistortionGain.Items.AddRange(new object[] {
            "Value",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxDistortionGain.Location = new System.Drawing.Point(6, 213);
            this.ComboBoxDistortionGain.Name = "ComboBoxDistortionGain";
            this.ComboBoxDistortionGain.Size = new System.Drawing.Size(62, 21);
            this.ComboBoxDistortionGain.TabIndex = 62;
            this.ComboBoxDistortionGain.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxDistortionGain_SelectionChangeCommitted);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(6, 18);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(34, 13);
            this.label25.TabIndex = 29;
            this.label25.Text = "Value";
            // 
            // ComboBoxDistortionVal
            // 
            this.ComboBoxDistortionVal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxDistortionVal.FormattingEnabled = true;
            this.ComboBoxDistortionVal.Items.AddRange(new object[] {
            "-",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxDistortionVal.Location = new System.Drawing.Point(6, 33);
            this.ComboBoxDistortionVal.Name = "ComboBoxDistortionVal";
            this.ComboBoxDistortionVal.Size = new System.Drawing.Size(62, 21);
            this.ComboBoxDistortionVal.TabIndex = 9;
            this.ComboBoxDistortionVal.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxDistortionVal_SelectionChangeCommitted);
            // 
            // GroupBoxVol
            // 
            this.GroupBoxVol.BorderColor = System.Drawing.Color.White;
            this.GroupBoxVol.Controls.Add(this.label71);
            this.GroupBoxVol.Controls.Add(this.hScrollBarVolGainValue);
            this.GroupBoxVol.Controls.Add(this.TextBoxVolGainValue);
            this.GroupBoxVol.Controls.Add(this.label24);
            this.GroupBoxVol.Controls.Add(this.label23);
            this.GroupBoxVol.Controls.Add(this.ComboBoxVolGain);
            this.GroupBoxVol.Controls.Add(this.ComboBoxVolVal);
            this.GroupBoxVol.ForeColor = System.Drawing.Color.White;
            this.GroupBoxVol.Location = new System.Drawing.Point(416, 198);
            this.GroupBoxVol.Name = "GroupBoxVol";
            this.GroupBoxVol.Size = new System.Drawing.Size(212, 260);
            this.GroupBoxVol.TabIndex = 24;
            this.GroupBoxVol.TabStop = false;
            this.GroupBoxVol.Text = "Volume";
            this.GroupBoxVol.Visible = false;
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.Location = new System.Drawing.Point(6, 239);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(171, 13);
            this.label71.TabIndex = 131;
            this.label71.Text = "Volume > 128 may cause distortion";
            // 
            // hScrollBarVolGainValue
            // 
            this.hScrollBarVolGainValue.LargeChange = 1;
            this.hScrollBarVolGainValue.Location = new System.Drawing.Point(80, 196);
            this.hScrollBarVolGainValue.Maximum = 255;
            this.hScrollBarVolGainValue.Name = "hScrollBarVolGainValue";
            this.hScrollBarVolGainValue.Size = new System.Drawing.Size(120, 18);
            this.hScrollBarVolGainValue.TabIndex = 61;
            this.hScrollBarVolGainValue.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarVolGainValue_Scroll);
            // 
            // TextBoxVolGainValue
            // 
            this.TextBoxVolGainValue.Location = new System.Drawing.Point(80, 214);
            this.TextBoxVolGainValue.Name = "TextBoxVolGainValue";
            this.TextBoxVolGainValue.Size = new System.Drawing.Size(120, 20);
            this.TextBoxVolGainValue.TabIndex = 60;
            this.TextBoxVolGainValue.Text = "0";
            this.TextBoxVolGainValue.Click += new System.EventHandler(this.TextBoxVolGainValue_Click);
            this.TextBoxVolGainValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxVolGainValue_KeyDown);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(6, 198);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(29, 13);
            this.label24.TabIndex = 29;
            this.label24.Text = "Gain";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(6, 18);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(34, 13);
            this.label23.TabIndex = 28;
            this.label23.Text = "Value";
            // 
            // ComboBoxVolGain
            // 
            this.ComboBoxVolGain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxVolGain.FormattingEnabled = true;
            this.ComboBoxVolGain.Items.AddRange(new object[] {
            "-",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxVolGain.Location = new System.Drawing.Point(6, 213);
            this.ComboBoxVolGain.Name = "ComboBoxVolGain";
            this.ComboBoxVolGain.Size = new System.Drawing.Size(62, 21);
            this.ComboBoxVolGain.TabIndex = 11;
            this.ComboBoxVolGain.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxVolGain_SelectionChangeCommitted);
            // 
            // ComboBoxVolVal
            // 
            this.ComboBoxVolVal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxVolVal.FormattingEnabled = true;
            this.ComboBoxVolVal.Items.AddRange(new object[] {
            "-",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxVolVal.Location = new System.Drawing.Point(6, 33);
            this.ComboBoxVolVal.Name = "ComboBoxVolVal";
            this.ComboBoxVolVal.Size = new System.Drawing.Size(62, 21);
            this.ComboBoxVolVal.TabIndex = 10;
            this.ComboBoxVolVal.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxVolVal_SelectionChangeCommitted);
            // 
            // GroupBoxOsc_saw
            // 
            this.GroupBoxOsc_saw.BorderColor = System.Drawing.Color.White;
            this.GroupBoxOsc_saw.Controls.Add(this.hScrollBarOscsawGainValue);
            this.GroupBoxOsc_saw.Controls.Add(this.hScrollBarOscsawFreqValue);
            this.GroupBoxOsc_saw.Controls.Add(this.label3);
            this.GroupBoxOsc_saw.Controls.Add(this.label2);
            this.GroupBoxOsc_saw.Controls.Add(this.TextBoxOscsawGainValue);
            this.GroupBoxOsc_saw.Controls.Add(this.TextBoxOscsawFreqValue);
            this.GroupBoxOsc_saw.Controls.Add(this.ComboBoxOscsawGain);
            this.GroupBoxOsc_saw.Controls.Add(this.ComboBoxOscsawFreq);
            this.GroupBoxOsc_saw.ForeColor = System.Drawing.Color.White;
            this.GroupBoxOsc_saw.Location = new System.Drawing.Point(416, 198);
            this.GroupBoxOsc_saw.Name = "GroupBoxOsc_saw";
            this.GroupBoxOsc_saw.Size = new System.Drawing.Size(212, 260);
            this.GroupBoxOsc_saw.TabIndex = 25;
            this.GroupBoxOsc_saw.TabStop = false;
            this.GroupBoxOsc_saw.Text = "Sawtooth Oscillator";
            this.GroupBoxOsc_saw.Visible = false;
            // 
            // hScrollBarOscsawGainValue
            // 
            this.hScrollBarOscsawGainValue.LargeChange = 1;
            this.hScrollBarOscsawGainValue.Location = new System.Drawing.Point(80, 196);
            this.hScrollBarOscsawGainValue.Maximum = 128;
            this.hScrollBarOscsawGainValue.Name = "hScrollBarOscsawGainValue";
            this.hScrollBarOscsawGainValue.Size = new System.Drawing.Size(120, 18);
            this.hScrollBarOscsawGainValue.TabIndex = 51;
            this.hScrollBarOscsawGainValue.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarOscsawGainValue_Scroll);
            // 
            // hScrollBarOscsawFreqValue
            // 
            this.hScrollBarOscsawFreqValue.LargeChange = 50;
            this.hScrollBarOscsawFreqValue.Location = new System.Drawing.Point(80, 61);
            this.hScrollBarOscsawFreqValue.Maximum = 10000;
            this.hScrollBarOscsawFreqValue.Name = "hScrollBarOscsawFreqValue";
            this.hScrollBarOscsawFreqValue.Size = new System.Drawing.Size(120, 18);
            this.hScrollBarOscsawFreqValue.TabIndex = 50;
            this.hScrollBarOscsawFreqValue.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarOscsawFreqValue_Scroll);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 198);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Gain";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Frequency";
            // 
            // TextBoxOscsawGainValue
            // 
            this.TextBoxOscsawGainValue.Location = new System.Drawing.Point(80, 214);
            this.TextBoxOscsawGainValue.Name = "TextBoxOscsawGainValue";
            this.TextBoxOscsawGainValue.Size = new System.Drawing.Size(120, 20);
            this.TextBoxOscsawGainValue.TabIndex = 14;
            this.TextBoxOscsawGainValue.Text = "0";
            this.TextBoxOscsawGainValue.Click += new System.EventHandler(this.TextBoxOscsawGainValue_Click);
            this.TextBoxOscsawGainValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxOscsawGainValue_KeyDown);
            // 
            // TextBoxOscsawFreqValue
            // 
            this.TextBoxOscsawFreqValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxOscsawFreqValue.Location = new System.Drawing.Point(80, 79);
            this.TextBoxOscsawFreqValue.Name = "TextBoxOscsawFreqValue";
            this.TextBoxOscsawFreqValue.Size = new System.Drawing.Size(120, 20);
            this.TextBoxOscsawFreqValue.TabIndex = 13;
            this.TextBoxOscsawFreqValue.Text = "0";
            this.TextBoxOscsawFreqValue.Click += new System.EventHandler(this.TextBoxOscsawFreqValue_Click);
            this.TextBoxOscsawFreqValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxOscsawFreqValue_KeyDown);
            // 
            // ComboBoxOscsawGain
            // 
            this.ComboBoxOscsawGain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxOscsawGain.FormattingEnabled = true;
            this.ComboBoxOscsawGain.Items.AddRange(new object[] {
            "Value",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxOscsawGain.Location = new System.Drawing.Point(6, 213);
            this.ComboBoxOscsawGain.Name = "ComboBoxOscsawGain";
            this.ComboBoxOscsawGain.Size = new System.Drawing.Size(62, 21);
            this.ComboBoxOscsawGain.TabIndex = 12;
            this.ComboBoxOscsawGain.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxOscsawGain_SelectionChangeCommitted);
            // 
            // ComboBoxOscsawFreq
            // 
            this.ComboBoxOscsawFreq.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxOscsawFreq.FormattingEnabled = true;
            this.ComboBoxOscsawFreq.Items.AddRange(new object[] {
            "Value",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxOscsawFreq.Location = new System.Drawing.Point(6, 78);
            this.ComboBoxOscsawFreq.Name = "ComboBoxOscsawFreq";
            this.ComboBoxOscsawFreq.Size = new System.Drawing.Size(62, 21);
            this.ComboBoxOscsawFreq.TabIndex = 11;
            this.ComboBoxOscsawFreq.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxOscsawFreq_SelectionChangeCommitted);
            // 
            // GroupBoxOsc_tri
            // 
            this.GroupBoxOsc_tri.BorderColor = System.Drawing.Color.White;
            this.GroupBoxOsc_tri.Controls.Add(this.hScrollBarOsctriGainValue);
            this.GroupBoxOsc_tri.Controls.Add(this.hScrollBarOsctriFreqValue);
            this.GroupBoxOsc_tri.Controls.Add(this.ComboBoxOsctriFreq);
            this.GroupBoxOsc_tri.Controls.Add(this.label10);
            this.GroupBoxOsc_tri.Controls.Add(this.ComboBoxOsctriGain);
            this.GroupBoxOsc_tri.Controls.Add(this.label14);
            this.GroupBoxOsc_tri.Controls.Add(this.TextBoxOsctriFreqValue);
            this.GroupBoxOsc_tri.Controls.Add(this.TextBoxOsctriGainValue);
            this.GroupBoxOsc_tri.ForeColor = System.Drawing.Color.White;
            this.GroupBoxOsc_tri.Location = new System.Drawing.Point(416, 198);
            this.GroupBoxOsc_tri.Name = "GroupBoxOsc_tri";
            this.GroupBoxOsc_tri.Size = new System.Drawing.Size(212, 260);
            this.GroupBoxOsc_tri.TabIndex = 26;
            this.GroupBoxOsc_tri.TabStop = false;
            this.GroupBoxOsc_tri.Text = "Triangle Oscillator";
            this.GroupBoxOsc_tri.Visible = false;
            // 
            // hScrollBarOsctriGainValue
            // 
            this.hScrollBarOsctriGainValue.LargeChange = 1;
            this.hScrollBarOsctriGainValue.Location = new System.Drawing.Point(80, 197);
            this.hScrollBarOsctriGainValue.Maximum = 128;
            this.hScrollBarOsctriGainValue.Name = "hScrollBarOsctriGainValue";
            this.hScrollBarOsctriGainValue.Size = new System.Drawing.Size(120, 18);
            this.hScrollBarOsctriGainValue.TabIndex = 52;
            this.hScrollBarOsctriGainValue.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarOsctriGainValue_Scroll);
            // 
            // hScrollBarOsctriFreqValue
            // 
            this.hScrollBarOsctriFreqValue.LargeChange = 50;
            this.hScrollBarOsctriFreqValue.Location = new System.Drawing.Point(80, 61);
            this.hScrollBarOsctriFreqValue.Maximum = 10000;
            this.hScrollBarOsctriFreqValue.Name = "hScrollBarOsctriFreqValue";
            this.hScrollBarOsctriFreqValue.Size = new System.Drawing.Size(120, 18);
            this.hScrollBarOsctriFreqValue.TabIndex = 52;
            this.hScrollBarOsctriFreqValue.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarOsctriFreqValue_Scroll);
            // 
            // ComboBoxOsctriFreq
            // 
            this.ComboBoxOsctriFreq.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxOsctriFreq.FormattingEnabled = true;
            this.ComboBoxOsctriFreq.Items.AddRange(new object[] {
            "Value",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxOsctriFreq.Location = new System.Drawing.Point(6, 78);
            this.ComboBoxOsctriFreq.Name = "ComboBoxOsctriFreq";
            this.ComboBoxOsctriFreq.Size = new System.Drawing.Size(62, 21);
            this.ComboBoxOsctriFreq.TabIndex = 21;
            this.ComboBoxOsctriFreq.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxOsctriFreq_SelectionChangeCommitted);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 198);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 13);
            this.label10.TabIndex = 27;
            this.label10.Text = "Gain";
            // 
            // ComboBoxOsctriGain
            // 
            this.ComboBoxOsctriGain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxOsctriGain.FormattingEnabled = true;
            this.ComboBoxOsctriGain.Items.AddRange(new object[] {
            "Value",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxOsctriGain.Location = new System.Drawing.Point(6, 213);
            this.ComboBoxOsctriGain.Name = "ComboBoxOsctriGain";
            this.ComboBoxOsctriGain.Size = new System.Drawing.Size(62, 21);
            this.ComboBoxOsctriGain.TabIndex = 22;
            this.ComboBoxOsctriGain.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxOsctriGain_SelectionChangeCommitted);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 63);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(57, 13);
            this.label14.TabIndex = 26;
            this.label14.Text = "Frequency";
            // 
            // TextBoxOsctriFreqValue
            // 
            this.TextBoxOsctriFreqValue.Location = new System.Drawing.Point(80, 79);
            this.TextBoxOsctriFreqValue.Name = "TextBoxOsctriFreqValue";
            this.TextBoxOsctriFreqValue.Size = new System.Drawing.Size(120, 20);
            this.TextBoxOsctriFreqValue.TabIndex = 23;
            this.TextBoxOsctriFreqValue.Text = "0";
            this.TextBoxOsctriFreqValue.Click += new System.EventHandler(this.TextBoxOsctriFreqValue_Click);
            this.TextBoxOsctriFreqValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxOsctriFreqValue_KeyDown);
            // 
            // TextBoxOsctriGainValue
            // 
            this.TextBoxOsctriGainValue.Location = new System.Drawing.Point(80, 215);
            this.TextBoxOsctriGainValue.Name = "TextBoxOsctriGainValue";
            this.TextBoxOsctriGainValue.Size = new System.Drawing.Size(120, 20);
            this.TextBoxOsctriGainValue.TabIndex = 24;
            this.TextBoxOsctriGainValue.Text = "0";
            this.TextBoxOsctriGainValue.Click += new System.EventHandler(this.TextBoxOsctriGainValue_Click);
            this.TextBoxOsctriGainValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxOsctriGainValue_KeyDown);
            // 
            // GroupBoxOsc_sine
            // 
            this.GroupBoxOsc_sine.BorderColor = System.Drawing.Color.White;
            this.GroupBoxOsc_sine.Controls.Add(this.hScrollBarOscsineGainValue);
            this.GroupBoxOsc_sine.Controls.Add(this.hScrollBarOscsineFreqValue);
            this.GroupBoxOsc_sine.Controls.Add(this.ComboBoxOscsineFreq);
            this.GroupBoxOsc_sine.Controls.Add(this.label20);
            this.GroupBoxOsc_sine.Controls.Add(this.ComboBoxOscsineGain);
            this.GroupBoxOsc_sine.Controls.Add(this.label21);
            this.GroupBoxOsc_sine.Controls.Add(this.TextBoxOscsineFreqValue);
            this.GroupBoxOsc_sine.Controls.Add(this.TextBoxOscsineGainValue);
            this.GroupBoxOsc_sine.ForeColor = System.Drawing.Color.White;
            this.GroupBoxOsc_sine.Location = new System.Drawing.Point(416, 198);
            this.GroupBoxOsc_sine.Name = "GroupBoxOsc_sine";
            this.GroupBoxOsc_sine.Size = new System.Drawing.Size(212, 260);
            this.GroupBoxOsc_sine.TabIndex = 27;
            this.GroupBoxOsc_sine.TabStop = false;
            this.GroupBoxOsc_sine.Text = "Sine Oscillator";
            this.GroupBoxOsc_sine.Visible = false;
            // 
            // hScrollBarOscsineGainValue
            // 
            this.hScrollBarOscsineGainValue.LargeChange = 1;
            this.hScrollBarOscsineGainValue.Location = new System.Drawing.Point(80, 197);
            this.hScrollBarOscsineGainValue.Maximum = 128;
            this.hScrollBarOscsineGainValue.Name = "hScrollBarOscsineGainValue";
            this.hScrollBarOscsineGainValue.Size = new System.Drawing.Size(120, 18);
            this.hScrollBarOscsineGainValue.TabIndex = 53;
            this.hScrollBarOscsineGainValue.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarOscsineGainValue_Scroll);
            // 
            // hScrollBarOscsineFreqValue
            // 
            this.hScrollBarOscsineFreqValue.LargeChange = 50;
            this.hScrollBarOscsineFreqValue.Location = new System.Drawing.Point(80, 61);
            this.hScrollBarOscsineFreqValue.Maximum = 16000;
            this.hScrollBarOscsineFreqValue.Name = "hScrollBarOscsineFreqValue";
            this.hScrollBarOscsineFreqValue.Size = new System.Drawing.Size(120, 18);
            this.hScrollBarOscsineFreqValue.TabIndex = 53;
            this.hScrollBarOscsineFreqValue.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarOscsineFreqValue_Scroll);
            // 
            // ComboBoxOscsineFreq
            // 
            this.ComboBoxOscsineFreq.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxOscsineFreq.FormattingEnabled = true;
            this.ComboBoxOscsineFreq.Items.AddRange(new object[] {
            "Value",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxOscsineFreq.Location = new System.Drawing.Point(6, 78);
            this.ComboBoxOscsineFreq.Name = "ComboBoxOscsineFreq";
            this.ComboBoxOscsineFreq.Size = new System.Drawing.Size(62, 21);
            this.ComboBoxOscsineFreq.TabIndex = 31;
            this.ComboBoxOscsineFreq.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxOscsineFreq_SelectionChangeCommitted);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(6, 198);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(29, 13);
            this.label20.TabIndex = 37;
            this.label20.Text = "Gain";
            // 
            // ComboBoxOscsineGain
            // 
            this.ComboBoxOscsineGain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxOscsineGain.FormattingEnabled = true;
            this.ComboBoxOscsineGain.Items.AddRange(new object[] {
            "Value",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxOscsineGain.Location = new System.Drawing.Point(6, 213);
            this.ComboBoxOscsineGain.Name = "ComboBoxOscsineGain";
            this.ComboBoxOscsineGain.Size = new System.Drawing.Size(62, 21);
            this.ComboBoxOscsineGain.TabIndex = 32;
            this.ComboBoxOscsineGain.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxOscsineGain_SelectionChangeCommitted);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(6, 63);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(57, 13);
            this.label21.TabIndex = 36;
            this.label21.Text = "Frequency";
            // 
            // TextBoxOscsineFreqValue
            // 
            this.TextBoxOscsineFreqValue.Location = new System.Drawing.Point(80, 79);
            this.TextBoxOscsineFreqValue.Name = "TextBoxOscsineFreqValue";
            this.TextBoxOscsineFreqValue.Size = new System.Drawing.Size(120, 20);
            this.TextBoxOscsineFreqValue.TabIndex = 33;
            this.TextBoxOscsineFreqValue.Text = "0";
            this.TextBoxOscsineFreqValue.Click += new System.EventHandler(this.TextBoxOscsineFreqValue_Click);
            this.TextBoxOscsineFreqValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxOscsineFreqValue_KeyDown);
            // 
            // TextBoxOscsineGainValue
            // 
            this.TextBoxOscsineGainValue.Location = new System.Drawing.Point(80, 215);
            this.TextBoxOscsineGainValue.Name = "TextBoxOscsineGainValue";
            this.TextBoxOscsineGainValue.Size = new System.Drawing.Size(120, 20);
            this.TextBoxOscsineGainValue.TabIndex = 34;
            this.TextBoxOscsineGainValue.Text = "0";
            this.TextBoxOscsineGainValue.Click += new System.EventHandler(this.TextBoxOscsineGainValue_Click);
            this.TextBoxOscsineGainValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxOscsineGainValue_KeyDown);
            // 
            // buttonStopSample
            // 
            this.buttonStopSample.ForeColor = System.Drawing.Color.Black;
            this.buttonStopSample.Location = new System.Drawing.Point(108, 17);
            this.buttonStopSample.Name = "buttonStopSample";
            this.buttonStopSample.Size = new System.Drawing.Size(98, 23);
            this.buttonStopSample.TabIndex = 20;
            this.buttonStopSample.Text = "Stop";
            this.buttonStopSample.UseVisualStyleBackColor = true;
            this.buttonStopSample.Visible = false;
            this.buttonStopSample.Click += new System.EventHandler(this.buttonStopSample_Click);
            // 
            // GroupBoxAdd
            // 
            this.GroupBoxAdd.BorderColor = System.Drawing.Color.White;
            this.GroupBoxAdd.Controls.Add(this.hScrollBarAddVal2Value);
            this.GroupBoxAdd.Controls.Add(this.TextBoxAddVal2Value);
            this.GroupBoxAdd.Controls.Add(this.label17);
            this.GroupBoxAdd.Controls.Add(this.label16);
            this.GroupBoxAdd.Controls.Add(this.ComboBoxAddVal2);
            this.GroupBoxAdd.Controls.Add(this.ComboBoxAddVal1);
            this.GroupBoxAdd.ForeColor = System.Drawing.Color.White;
            this.GroupBoxAdd.Location = new System.Drawing.Point(416, 198);
            this.GroupBoxAdd.Name = "GroupBoxAdd";
            this.GroupBoxAdd.Size = new System.Drawing.Size(212, 260);
            this.GroupBoxAdd.TabIndex = 29;
            this.GroupBoxAdd.TabStop = false;
            this.GroupBoxAdd.Text = "Addition";
            this.GroupBoxAdd.Visible = false;
            // 
            // hScrollBarAddVal2Value
            // 
            this.hScrollBarAddVal2Value.LargeChange = 64;
            this.hScrollBarAddVal2Value.Location = new System.Drawing.Point(80, 61);
            this.hScrollBarAddVal2Value.Maximum = 32830;
            this.hScrollBarAddVal2Value.Minimum = -32768;
            this.hScrollBarAddVal2Value.Name = "hScrollBarAddVal2Value";
            this.hScrollBarAddVal2Value.Size = new System.Drawing.Size(120, 18);
            this.hScrollBarAddVal2Value.TabIndex = 55;
            this.hScrollBarAddVal2Value.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarAddVal2Value_Scroll);
            // 
            // TextBoxAddVal2Value
            // 
            this.TextBoxAddVal2Value.Location = new System.Drawing.Point(80, 79);
            this.TextBoxAddVal2Value.Name = "TextBoxAddVal2Value";
            this.TextBoxAddVal2Value.Size = new System.Drawing.Size(120, 20);
            this.TextBoxAddVal2Value.TabIndex = 34;
            this.TextBoxAddVal2Value.Text = "0";
            this.TextBoxAddVal2Value.Click += new System.EventHandler(this.TextBoxAddVal2Value_Click);
            this.TextBoxAddVal2Value.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxAddVal2Value_KeyDown);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 63);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(43, 13);
            this.label17.TabIndex = 28;
            this.label17.Text = "Value 2";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 18);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(43, 13);
            this.label16.TabIndex = 27;
            this.label16.Text = "Value 1";
            // 
            // ComboBoxAddVal2
            // 
            this.ComboBoxAddVal2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxAddVal2.FormattingEnabled = true;
            this.ComboBoxAddVal2.Items.AddRange(new object[] {
            "Value",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxAddVal2.Location = new System.Drawing.Point(6, 78);
            this.ComboBoxAddVal2.Name = "ComboBoxAddVal2";
            this.ComboBoxAddVal2.Size = new System.Drawing.Size(62, 21);
            this.ComboBoxAddVal2.TabIndex = 11;
            this.ComboBoxAddVal2.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxAddVal2_SelectionChangeCommitted);
            // 
            // ComboBoxAddVal1
            // 
            this.ComboBoxAddVal1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxAddVal1.FormattingEnabled = true;
            this.ComboBoxAddVal1.Items.AddRange(new object[] {
            "-",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxAddVal1.Location = new System.Drawing.Point(6, 33);
            this.ComboBoxAddVal1.Name = "ComboBoxAddVal1";
            this.ComboBoxAddVal1.Size = new System.Drawing.Size(62, 21);
            this.ComboBoxAddVal1.TabIndex = 10;
            this.ComboBoxAddVal1.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxAddVal1_SelectionChangeCommitted);
            // 
            // GroupBoxOsc_pulse
            // 
            this.GroupBoxOsc_pulse.BorderColor = System.Drawing.Color.White;
            this.GroupBoxOsc_pulse.Controls.Add(this.hScrollBarOscpulseWidthValue);
            this.GroupBoxOsc_pulse.Controls.Add(this.hScrollBarOscpulseGainValue);
            this.GroupBoxOsc_pulse.Controls.Add(this.hScrollBarOscpulseFreqValue);
            this.GroupBoxOsc_pulse.Controls.Add(this.label32);
            this.GroupBoxOsc_pulse.Controls.Add(this.label6);
            this.GroupBoxOsc_pulse.Controls.Add(this.ComboBoxOscpulseWidth);
            this.GroupBoxOsc_pulse.Controls.Add(this.TextBoxOscpulseWidthValue);
            this.GroupBoxOsc_pulse.Controls.Add(this.label28);
            this.GroupBoxOsc_pulse.Controls.Add(this.ComboBoxOscpulseGain);
            this.GroupBoxOsc_pulse.Controls.Add(this.TextBoxOscpulseGainValue);
            this.GroupBoxOsc_pulse.Controls.Add(this.ComboBoxOscpulseFreq);
            this.GroupBoxOsc_pulse.Controls.Add(this.TextBoxOscpulseFreqValue);
            this.GroupBoxOsc_pulse.ForeColor = System.Drawing.Color.White;
            this.GroupBoxOsc_pulse.Location = new System.Drawing.Point(416, 198);
            this.GroupBoxOsc_pulse.Name = "GroupBoxOsc_pulse";
            this.GroupBoxOsc_pulse.Size = new System.Drawing.Size(212, 260);
            this.GroupBoxOsc_pulse.TabIndex = 40;
            this.GroupBoxOsc_pulse.TabStop = false;
            this.GroupBoxOsc_pulse.Text = "Pulse Oscillator";
            this.GroupBoxOsc_pulse.Visible = false;
            // 
            // hScrollBarOscpulseWidthValue
            // 
            this.hScrollBarOscpulseWidthValue.LargeChange = 1;
            this.hScrollBarOscpulseWidthValue.Location = new System.Drawing.Point(80, 151);
            this.hScrollBarOscpulseWidthValue.Maximum = 127;
            this.hScrollBarOscpulseWidthValue.Name = "hScrollBarOscpulseWidthValue";
            this.hScrollBarOscpulseWidthValue.Size = new System.Drawing.Size(120, 18);
            this.hScrollBarOscpulseWidthValue.TabIndex = 57;
            this.hScrollBarOscpulseWidthValue.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarOscpulseWidthValue_Scroll);
            // 
            // hScrollBarOscpulseGainValue
            // 
            this.hScrollBarOscpulseGainValue.LargeChange = 1;
            this.hScrollBarOscpulseGainValue.Location = new System.Drawing.Point(80, 196);
            this.hScrollBarOscpulseGainValue.Maximum = 128;
            this.hScrollBarOscpulseGainValue.Name = "hScrollBarOscpulseGainValue";
            this.hScrollBarOscpulseGainValue.Size = new System.Drawing.Size(120, 18);
            this.hScrollBarOscpulseGainValue.TabIndex = 54;
            this.hScrollBarOscpulseGainValue.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarOscpulseGainValue_Scroll);
            // 
            // hScrollBarOscpulseFreqValue
            // 
            this.hScrollBarOscpulseFreqValue.LargeChange = 50;
            this.hScrollBarOscpulseFreqValue.Location = new System.Drawing.Point(80, 61);
            this.hScrollBarOscpulseFreqValue.Maximum = 10000;
            this.hScrollBarOscpulseFreqValue.Name = "hScrollBarOscpulseFreqValue";
            this.hScrollBarOscpulseFreqValue.Size = new System.Drawing.Size(120, 18);
            this.hScrollBarOscpulseFreqValue.TabIndex = 54;
            this.hScrollBarOscpulseFreqValue.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarOscpulseFreqValue_Scroll);
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(6, 153);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(58, 13);
            this.label32.TabIndex = 42;
            this.label32.Text = "Pulsewidth";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 63);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 56;
            this.label6.Text = "Frequency";
            // 
            // ComboBoxOscpulseWidth
            // 
            this.ComboBoxOscpulseWidth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxOscpulseWidth.FormattingEnabled = true;
            this.ComboBoxOscpulseWidth.Items.AddRange(new object[] {
            "Value",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxOscpulseWidth.Location = new System.Drawing.Point(6, 168);
            this.ComboBoxOscpulseWidth.Name = "ComboBoxOscpulseWidth";
            this.ComboBoxOscpulseWidth.Size = new System.Drawing.Size(62, 21);
            this.ComboBoxOscpulseWidth.TabIndex = 40;
            this.ComboBoxOscpulseWidth.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxOscpulseWidth_SelectionChangeCommitted);
            // 
            // TextBoxOscpulseWidthValue
            // 
            this.TextBoxOscpulseWidthValue.Location = new System.Drawing.Point(80, 169);
            this.TextBoxOscpulseWidthValue.Name = "TextBoxOscpulseWidthValue";
            this.TextBoxOscpulseWidthValue.Size = new System.Drawing.Size(120, 20);
            this.TextBoxOscpulseWidthValue.TabIndex = 41;
            this.TextBoxOscpulseWidthValue.Text = "0";
            this.TextBoxOscpulseWidthValue.Click += new System.EventHandler(this.TextBoxOscpulseWidthValue_Click);
            this.TextBoxOscpulseWidthValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxOscpulseWidthValue_KeyDown);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(6, 198);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(29, 13);
            this.label28.TabIndex = 37;
            this.label28.Text = "Gain";
            // 
            // ComboBoxOscpulseGain
            // 
            this.ComboBoxOscpulseGain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxOscpulseGain.FormattingEnabled = true;
            this.ComboBoxOscpulseGain.Items.AddRange(new object[] {
            "Value",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxOscpulseGain.Location = new System.Drawing.Point(6, 213);
            this.ComboBoxOscpulseGain.Name = "ComboBoxOscpulseGain";
            this.ComboBoxOscpulseGain.Size = new System.Drawing.Size(62, 21);
            this.ComboBoxOscpulseGain.TabIndex = 32;
            this.ComboBoxOscpulseGain.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxOscpulseGain_SelectionChangeCommitted);
            // 
            // TextBoxOscpulseGainValue
            // 
            this.TextBoxOscpulseGainValue.Location = new System.Drawing.Point(80, 214);
            this.TextBoxOscpulseGainValue.Name = "TextBoxOscpulseGainValue";
            this.TextBoxOscpulseGainValue.Size = new System.Drawing.Size(120, 20);
            this.TextBoxOscpulseGainValue.TabIndex = 34;
            this.TextBoxOscpulseGainValue.Text = "0";
            this.TextBoxOscpulseGainValue.Click += new System.EventHandler(this.TextBoxOscpulseGainValue_Click);
            this.TextBoxOscpulseGainValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxOscpulseGainValue_KeyDown);
            // 
            // ComboBoxOscpulseFreq
            // 
            this.ComboBoxOscpulseFreq.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxOscpulseFreq.FormattingEnabled = true;
            this.ComboBoxOscpulseFreq.Items.AddRange(new object[] {
            "Value",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxOscpulseFreq.Location = new System.Drawing.Point(6, 78);
            this.ComboBoxOscpulseFreq.Name = "ComboBoxOscpulseFreq";
            this.ComboBoxOscpulseFreq.Size = new System.Drawing.Size(62, 21);
            this.ComboBoxOscpulseFreq.TabIndex = 31;
            this.ComboBoxOscpulseFreq.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxOscpulseFreq_SelectionChangeCommitted);
            // 
            // TextBoxOscpulseFreqValue
            // 
            this.TextBoxOscpulseFreqValue.Location = new System.Drawing.Point(80, 79);
            this.TextBoxOscpulseFreqValue.Name = "TextBoxOscpulseFreqValue";
            this.TextBoxOscpulseFreqValue.Size = new System.Drawing.Size(120, 20);
            this.TextBoxOscpulseFreqValue.TabIndex = 33;
            this.TextBoxOscpulseFreqValue.Text = "0";
            this.TextBoxOscpulseFreqValue.Click += new System.EventHandler(this.TextBoxOscpulseFreqValue_Click);
            this.TextBoxOscpulseFreqValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxOscpulseFreqValue_KeyDown);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(6, 63);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(34, 13);
            this.label29.TabIndex = 36;
            this.label29.Text = "Delay";
            // 
            // GroupBoxOsc_noise
            // 
            this.GroupBoxOsc_noise.BorderColor = System.Drawing.Color.White;
            this.GroupBoxOsc_noise.Controls.Add(this.hScrollBarOscnoiseSeedValue);
            this.GroupBoxOsc_noise.Controls.Add(this.TextBoxOscnoiseSeedValue);
            this.GroupBoxOsc_noise.Controls.Add(this.label64);
            this.GroupBoxOsc_noise.Controls.Add(this.hScrollBarOscnoiseGainValue);
            this.GroupBoxOsc_noise.Controls.Add(this.label37);
            this.GroupBoxOsc_noise.Controls.Add(this.ComboBoxOscnoiseGain);
            this.GroupBoxOsc_noise.Controls.Add(this.TextBoxOscnoiseGainValue);
            this.GroupBoxOsc_noise.ForeColor = System.Drawing.Color.White;
            this.GroupBoxOsc_noise.Location = new System.Drawing.Point(416, 198);
            this.GroupBoxOsc_noise.Name = "GroupBoxOsc_noise";
            this.GroupBoxOsc_noise.Size = new System.Drawing.Size(212, 260);
            this.GroupBoxOsc_noise.TabIndex = 44;
            this.GroupBoxOsc_noise.TabStop = false;
            this.GroupBoxOsc_noise.Text = "Noise Oscillator";
            this.GroupBoxOsc_noise.Visible = false;
            // 
            // hScrollBarOscnoiseSeedValue
            // 
            this.hScrollBarOscnoiseSeedValue.LargeChange = 1;
            this.hScrollBarOscnoiseSeedValue.Location = new System.Drawing.Point(80, 104);
            this.hScrollBarOscnoiseSeedValue.Maximum = 127;
            this.hScrollBarOscnoiseSeedValue.Name = "hScrollBarOscnoiseSeedValue";
            this.hScrollBarOscnoiseSeedValue.Size = new System.Drawing.Size(120, 18);
            this.hScrollBarOscnoiseSeedValue.TabIndex = 123;
            this.hScrollBarOscnoiseSeedValue.Visible = false;
            this.hScrollBarOscnoiseSeedValue.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarOscnoiseSeedValue_Scroll);
            // 
            // TextBoxOscnoiseSeedValue
            // 
            this.TextBoxOscnoiseSeedValue.Location = new System.Drawing.Point(80, 122);
            this.TextBoxOscnoiseSeedValue.Name = "TextBoxOscnoiseSeedValue";
            this.TextBoxOscnoiseSeedValue.ReadOnly = true;
            this.TextBoxOscnoiseSeedValue.Size = new System.Drawing.Size(120, 20);
            this.TextBoxOscnoiseSeedValue.TabIndex = 122;
            this.TextBoxOscnoiseSeedValue.Text = "0";
            this.TextBoxOscnoiseSeedValue.Visible = false;
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Location = new System.Drawing.Point(7, 106);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(32, 13);
            this.label64.TabIndex = 121;
            this.label64.Text = "Seed";
            this.label64.Visible = false;
            // 
            // hScrollBarOscnoiseGainValue
            // 
            this.hScrollBarOscnoiseGainValue.LargeChange = 1;
            this.hScrollBarOscnoiseGainValue.Location = new System.Drawing.Point(80, 196);
            this.hScrollBarOscnoiseGainValue.Maximum = 128;
            this.hScrollBarOscnoiseGainValue.Name = "hScrollBarOscnoiseGainValue";
            this.hScrollBarOscnoiseGainValue.Size = new System.Drawing.Size(120, 18);
            this.hScrollBarOscnoiseGainValue.TabIndex = 55;
            this.hScrollBarOscnoiseGainValue.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarOscnoiseGainValue_Scroll);
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(6, 198);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(29, 13);
            this.label37.TabIndex = 37;
            this.label37.Text = "Gain";
            // 
            // ComboBoxOscnoiseGain
            // 
            this.ComboBoxOscnoiseGain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxOscnoiseGain.FormattingEnabled = true;
            this.ComboBoxOscnoiseGain.Items.AddRange(new object[] {
            "Value",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxOscnoiseGain.Location = new System.Drawing.Point(6, 213);
            this.ComboBoxOscnoiseGain.Name = "ComboBoxOscnoiseGain";
            this.ComboBoxOscnoiseGain.Size = new System.Drawing.Size(62, 21);
            this.ComboBoxOscnoiseGain.TabIndex = 32;
            this.ComboBoxOscnoiseGain.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxOscnoiseGain_SelectionChangeCommitted);
            // 
            // TextBoxOscnoiseGainValue
            // 
            this.TextBoxOscnoiseGainValue.Location = new System.Drawing.Point(80, 214);
            this.TextBoxOscnoiseGainValue.Name = "TextBoxOscnoiseGainValue";
            this.TextBoxOscnoiseGainValue.Size = new System.Drawing.Size(120, 20);
            this.TextBoxOscnoiseGainValue.TabIndex = 34;
            this.TextBoxOscnoiseGainValue.Text = "0";
            this.TextBoxOscnoiseGainValue.Click += new System.EventHandler(this.TextBoxOscnoiseGainValue_Click);
            this.TextBoxOscnoiseGainValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxOscnoiseGainValue_KeyDown);
            // 
            // GroupBoxEnva
            // 
            this.GroupBoxEnva.BorderColor = System.Drawing.Color.White;
            this.GroupBoxEnva.Controls.Add(this.hScrollBarEnvaGainValue);
            this.GroupBoxEnva.Controls.Add(this.hScrollBarEnvaAttackValue);
            this.GroupBoxEnva.Controls.Add(this.TextBoxEnvaAttackValue);
            this.GroupBoxEnva.Controls.Add(this.label40);
            this.GroupBoxEnva.Controls.Add(this.label38);
            this.GroupBoxEnva.Controls.Add(this.ComboBoxEnvaGain);
            this.GroupBoxEnva.Controls.Add(this.TextBoxEnvaGainValue);
            this.GroupBoxEnva.ForeColor = System.Drawing.Color.White;
            this.GroupBoxEnva.Location = new System.Drawing.Point(416, 198);
            this.GroupBoxEnva.Name = "GroupBoxEnva";
            this.GroupBoxEnva.Size = new System.Drawing.Size(212, 260);
            this.GroupBoxEnva.TabIndex = 45;
            this.GroupBoxEnva.TabStop = false;
            this.GroupBoxEnva.Text = "Attack Envelope";
            this.GroupBoxEnva.Visible = false;
            // 
            // hScrollBarEnvaGainValue
            // 
            this.hScrollBarEnvaGainValue.LargeChange = 1;
            this.hScrollBarEnvaGainValue.Location = new System.Drawing.Point(80, 196);
            this.hScrollBarEnvaGainValue.Maximum = 128;
            this.hScrollBarEnvaGainValue.Name = "hScrollBarEnvaGainValue";
            this.hScrollBarEnvaGainValue.Size = new System.Drawing.Size(120, 18);
            this.hScrollBarEnvaGainValue.TabIndex = 59;
            this.hScrollBarEnvaGainValue.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarEnvaGainValue_Scroll);
            // 
            // hScrollBarEnvaAttackValue
            // 
            this.hScrollBarEnvaAttackValue.LargeChange = 1;
            this.hScrollBarEnvaAttackValue.Location = new System.Drawing.Point(80, 22);
            this.hScrollBarEnvaAttackValue.Maximum = 127;
            this.hScrollBarEnvaAttackValue.Name = "hScrollBarEnvaAttackValue";
            this.hScrollBarEnvaAttackValue.Size = new System.Drawing.Size(120, 18);
            this.hScrollBarEnvaAttackValue.TabIndex = 58;
            this.hScrollBarEnvaAttackValue.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarEnvaAttackValue_Scroll);
            // 
            // TextBoxEnvaAttackValue
            // 
            this.TextBoxEnvaAttackValue.Location = new System.Drawing.Point(80, 40);
            this.TextBoxEnvaAttackValue.Name = "TextBoxEnvaAttackValue";
            this.TextBoxEnvaAttackValue.Size = new System.Drawing.Size(120, 20);
            this.TextBoxEnvaAttackValue.TabIndex = 47;
            this.TextBoxEnvaAttackValue.Text = "0";
            this.TextBoxEnvaAttackValue.Click += new System.EventHandler(this.TextBoxEnvaAttackValue_Click);
            this.TextBoxEnvaAttackValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxEnvaAttackValue_KeyDown);
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(6, 18);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(38, 13);
            this.label40.TabIndex = 46;
            this.label40.Text = "Attack";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(6, 198);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(29, 13);
            this.label38.TabIndex = 42;
            this.label38.Text = "Gain";
            // 
            // ComboBoxEnvaGain
            // 
            this.ComboBoxEnvaGain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxEnvaGain.FormattingEnabled = true;
            this.ComboBoxEnvaGain.Items.AddRange(new object[] {
            "Value",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxEnvaGain.Location = new System.Drawing.Point(6, 213);
            this.ComboBoxEnvaGain.Name = "ComboBoxEnvaGain";
            this.ComboBoxEnvaGain.Size = new System.Drawing.Size(62, 21);
            this.ComboBoxEnvaGain.TabIndex = 40;
            this.ComboBoxEnvaGain.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxEnvaGain_SelectionChangeCommitted);
            // 
            // TextBoxEnvaGainValue
            // 
            this.TextBoxEnvaGainValue.Location = new System.Drawing.Point(80, 214);
            this.TextBoxEnvaGainValue.Name = "TextBoxEnvaGainValue";
            this.TextBoxEnvaGainValue.Size = new System.Drawing.Size(120, 20);
            this.TextBoxEnvaGainValue.TabIndex = 41;
            this.TextBoxEnvaGainValue.Text = "0";
            this.TextBoxEnvaGainValue.Click += new System.EventHandler(this.TextBoxEnvaGainValue_Click);
            this.TextBoxEnvaGainValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxEnvaGainValue_KeyDown);
            // 
            // GroupBoxEnvd
            // 
            this.GroupBoxEnvd.BorderColor = System.Drawing.Color.White;
            this.GroupBoxEnvd.Controls.Add(this.hScrollBarEnvdGainValue);
            this.GroupBoxEnvd.Controls.Add(this.hScrollBarEnvdSustainValue);
            this.GroupBoxEnvd.Controls.Add(this.hScrollBarEnvdDecayValue);
            this.GroupBoxEnvd.Controls.Add(this.TextBoxEnvdSustainValue);
            this.GroupBoxEnvd.Controls.Add(this.label44);
            this.GroupBoxEnvd.Controls.Add(this.TextBoxEnvdDecayValue);
            this.GroupBoxEnvd.Controls.Add(this.label39);
            this.GroupBoxEnvd.Controls.Add(this.label42);
            this.GroupBoxEnvd.Controls.Add(this.ComboBoxEnvdGain);
            this.GroupBoxEnvd.Controls.Add(this.TextBoxEnvdGainValue);
            this.GroupBoxEnvd.ForeColor = System.Drawing.Color.White;
            this.GroupBoxEnvd.Location = new System.Drawing.Point(416, 198);
            this.GroupBoxEnvd.Name = "GroupBoxEnvd";
            this.GroupBoxEnvd.Size = new System.Drawing.Size(212, 260);
            this.GroupBoxEnvd.TabIndex = 46;
            this.GroupBoxEnvd.TabStop = false;
            this.GroupBoxEnvd.Text = "Decay Envelope";
            this.GroupBoxEnvd.Visible = false;
            // 
            // hScrollBarEnvdGainValue
            // 
            this.hScrollBarEnvdGainValue.LargeChange = 1;
            this.hScrollBarEnvdGainValue.Location = new System.Drawing.Point(80, 196);
            this.hScrollBarEnvdGainValue.Maximum = 128;
            this.hScrollBarEnvdGainValue.Name = "hScrollBarEnvdGainValue";
            this.hScrollBarEnvdGainValue.Size = new System.Drawing.Size(120, 18);
            this.hScrollBarEnvdGainValue.TabIndex = 61;
            this.hScrollBarEnvdGainValue.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarEnvdGainValue_Scroll);
            // 
            // hScrollBarEnvdSustainValue
            // 
            this.hScrollBarEnvdSustainValue.LargeChange = 1;
            this.hScrollBarEnvdSustainValue.Location = new System.Drawing.Point(80, 61);
            this.hScrollBarEnvdSustainValue.Maximum = 127;
            this.hScrollBarEnvdSustainValue.Name = "hScrollBarEnvdSustainValue";
            this.hScrollBarEnvdSustainValue.Size = new System.Drawing.Size(120, 18);
            this.hScrollBarEnvdSustainValue.TabIndex = 60;
            this.hScrollBarEnvdSustainValue.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarEnvdSustainValue_Scroll);
            // 
            // hScrollBarEnvdDecayValue
            // 
            this.hScrollBarEnvdDecayValue.LargeChange = 1;
            this.hScrollBarEnvdDecayValue.Location = new System.Drawing.Point(80, 16);
            this.hScrollBarEnvdDecayValue.Maximum = 127;
            this.hScrollBarEnvdDecayValue.Name = "hScrollBarEnvdDecayValue";
            this.hScrollBarEnvdDecayValue.Size = new System.Drawing.Size(120, 18);
            this.hScrollBarEnvdDecayValue.TabIndex = 59;
            this.hScrollBarEnvdDecayValue.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarEnvdDecayValue_Scroll);
            // 
            // TextBoxEnvdSustainValue
            // 
            this.TextBoxEnvdSustainValue.Location = new System.Drawing.Point(80, 79);
            this.TextBoxEnvdSustainValue.Name = "TextBoxEnvdSustainValue";
            this.TextBoxEnvdSustainValue.Size = new System.Drawing.Size(120, 20);
            this.TextBoxEnvdSustainValue.TabIndex = 50;
            this.TextBoxEnvdSustainValue.Text = "0";
            this.TextBoxEnvdSustainValue.Click += new System.EventHandler(this.TextBoxEnvdSustainValue_Click);
            this.TextBoxEnvdSustainValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxEnvdSustainValue_KeyDown);
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(6, 63);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(42, 13);
            this.label44.TabIndex = 49;
            this.label44.Text = "Sustain";
            // 
            // TextBoxEnvdDecayValue
            // 
            this.TextBoxEnvdDecayValue.Location = new System.Drawing.Point(80, 34);
            this.TextBoxEnvdDecayValue.Name = "TextBoxEnvdDecayValue";
            this.TextBoxEnvdDecayValue.Size = new System.Drawing.Size(120, 20);
            this.TextBoxEnvdDecayValue.TabIndex = 47;
            this.TextBoxEnvdDecayValue.Text = "0";
            this.TextBoxEnvdDecayValue.Click += new System.EventHandler(this.TextBoxEnvdDecayValue_Click);
            this.TextBoxEnvdDecayValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxEnvdDecayValue_KeyDown);
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(6, 18);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(38, 13);
            this.label39.TabIndex = 46;
            this.label39.Text = "Decay";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(6, 198);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(29, 13);
            this.label42.TabIndex = 42;
            this.label42.Text = "Gain";
            // 
            // ComboBoxEnvdGain
            // 
            this.ComboBoxEnvdGain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxEnvdGain.FormattingEnabled = true;
            this.ComboBoxEnvdGain.Items.AddRange(new object[] {
            "Value",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxEnvdGain.Location = new System.Drawing.Point(6, 213);
            this.ComboBoxEnvdGain.Name = "ComboBoxEnvdGain";
            this.ComboBoxEnvdGain.Size = new System.Drawing.Size(62, 21);
            this.ComboBoxEnvdGain.TabIndex = 40;
            this.ComboBoxEnvdGain.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxEnvdGain_SelectionChangeCommitted);
            // 
            // TextBoxEnvdGainValue
            // 
            this.TextBoxEnvdGainValue.Location = new System.Drawing.Point(80, 214);
            this.TextBoxEnvdGainValue.Name = "TextBoxEnvdGainValue";
            this.TextBoxEnvdGainValue.Size = new System.Drawing.Size(120, 20);
            this.TextBoxEnvdGainValue.TabIndex = 41;
            this.TextBoxEnvdGainValue.Text = "0";
            this.TextBoxEnvdGainValue.Click += new System.EventHandler(this.TextBoxEnvdGainValue_Click);
            this.TextBoxEnvdGainValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxEnvdGainValue_KeyDown);
            // 
            // GroupBoxEnvelope
            // 
            this.GroupBoxEnvelope.BorderColor = System.Drawing.Color.White;
            this.GroupBoxEnvelope.Controls.Add(this.hScrollBarEnvelopeReleaseValue);
            this.GroupBoxEnvelope.Controls.Add(this.TextBoxEnvelopeReleaseValue);
            this.GroupBoxEnvelope.Controls.Add(this.hScrollBarEnvelopeAttackValue);
            this.GroupBoxEnvelope.Controls.Add(this.TextBoxEnvelopeAttackValue);
            this.GroupBoxEnvelope.Controls.Add(this.label77);
            this.GroupBoxEnvelope.Controls.Add(this.label76);
            this.GroupBoxEnvelope.Controls.Add(this.hScrollBarEnvelopeGainValue);
            this.GroupBoxEnvelope.Controls.Add(this.hScrollBarEnvelopeSustainValue);
            this.GroupBoxEnvelope.Controls.Add(this.hScrollBarEnvelopeDecayValue);
            this.GroupBoxEnvelope.Controls.Add(this.TextBoxEnvelopeSustainValue);
            this.GroupBoxEnvelope.Controls.Add(this.label66);
            this.GroupBoxEnvelope.Controls.Add(this.TextBoxEnvelopeDecayValue);
            this.GroupBoxEnvelope.Controls.Add(this.label67);
            this.GroupBoxEnvelope.Controls.Add(this.label75);
            this.GroupBoxEnvelope.Controls.Add(this.TextBoxEnvelopeGainValue);
            this.GroupBoxEnvelope.ForeColor = System.Drawing.Color.White;
            this.GroupBoxEnvelope.Location = new System.Drawing.Point(416, 198);
            this.GroupBoxEnvelope.Name = "GroupBoxEnvelope";
            this.GroupBoxEnvelope.Size = new System.Drawing.Size(212, 260);
            this.GroupBoxEnvelope.TabIndex = 62;
            this.GroupBoxEnvelope.TabStop = false;
            this.GroupBoxEnvelope.Text = "ADSR Envelope";
            // 
            // hScrollBarEnvelopeReleaseValue
            // 
            this.hScrollBarEnvelopeReleaseValue.LargeChange = 1;
            this.hScrollBarEnvelopeReleaseValue.Location = new System.Drawing.Point(80, 150);
            this.hScrollBarEnvelopeReleaseValue.Maximum = 255;
            this.hScrollBarEnvelopeReleaseValue.Name = "hScrollBarEnvelopeReleaseValue";
            this.hScrollBarEnvelopeReleaseValue.Size = new System.Drawing.Size(120, 18);
            this.hScrollBarEnvelopeReleaseValue.TabIndex = 67;
            this.hScrollBarEnvelopeReleaseValue.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarEnvelopeReleaseValue_Scroll);
            // 
            // TextBoxEnvelopeReleaseValue
            // 
            this.TextBoxEnvelopeReleaseValue.Location = new System.Drawing.Point(80, 168);
            this.TextBoxEnvelopeReleaseValue.Name = "TextBoxEnvelopeReleaseValue";
            this.TextBoxEnvelopeReleaseValue.Size = new System.Drawing.Size(120, 20);
            this.TextBoxEnvelopeReleaseValue.TabIndex = 66;
            this.TextBoxEnvelopeReleaseValue.Text = "0";
            this.TextBoxEnvelopeReleaseValue.Click += new System.EventHandler(this.TextBoxEnvelopeReleaseValue_Click);
            this.TextBoxEnvelopeReleaseValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxEnvelopeReleaseValue_KeyDown);
            // 
            // hScrollBarEnvelopeAttackValue
            // 
            this.hScrollBarEnvelopeAttackValue.LargeChange = 1;
            this.hScrollBarEnvelopeAttackValue.Location = new System.Drawing.Point(80, 16);
            this.hScrollBarEnvelopeAttackValue.Maximum = 255;
            this.hScrollBarEnvelopeAttackValue.Name = "hScrollBarEnvelopeAttackValue";
            this.hScrollBarEnvelopeAttackValue.Size = new System.Drawing.Size(120, 18);
            this.hScrollBarEnvelopeAttackValue.TabIndex = 64;
            this.hScrollBarEnvelopeAttackValue.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarEnvelopeAttackValue_Scroll);
            // 
            // TextBoxEnvelopeAttackValue
            // 
            this.TextBoxEnvelopeAttackValue.Location = new System.Drawing.Point(80, 34);
            this.TextBoxEnvelopeAttackValue.Name = "TextBoxEnvelopeAttackValue";
            this.TextBoxEnvelopeAttackValue.Size = new System.Drawing.Size(120, 20);
            this.TextBoxEnvelopeAttackValue.TabIndex = 63;
            this.TextBoxEnvelopeAttackValue.Text = "0";
            this.TextBoxEnvelopeAttackValue.Click += new System.EventHandler(this.TextBoxEnvelopeAttackValue_Click);
            this.TextBoxEnvelopeAttackValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxEnvelopeAttackValue_KeyDown);
            // 
            // label77
            // 
            this.label77.AutoSize = true;
            this.label77.Location = new System.Drawing.Point(6, 152);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(46, 13);
            this.label77.TabIndex = 65;
            this.label77.Text = "Release";
            // 
            // label76
            // 
            this.label76.AutoSize = true;
            this.label76.Location = new System.Drawing.Point(6, 18);
            this.label76.Name = "label76";
            this.label76.Size = new System.Drawing.Size(38, 13);
            this.label76.TabIndex = 62;
            this.label76.Text = "Attack";
            // 
            // hScrollBarEnvelopeGainValue
            // 
            this.hScrollBarEnvelopeGainValue.LargeChange = 1;
            this.hScrollBarEnvelopeGainValue.Location = new System.Drawing.Point(80, 195);
            this.hScrollBarEnvelopeGainValue.Maximum = 128;
            this.hScrollBarEnvelopeGainValue.Name = "hScrollBarEnvelopeGainValue";
            this.hScrollBarEnvelopeGainValue.Size = new System.Drawing.Size(120, 18);
            this.hScrollBarEnvelopeGainValue.TabIndex = 61;
            this.hScrollBarEnvelopeGainValue.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarEnvelopeGainValue_Scroll);
            // 
            // hScrollBarEnvelopeSustainValue
            // 
            this.hScrollBarEnvelopeSustainValue.LargeChange = 1;
            this.hScrollBarEnvelopeSustainValue.Location = new System.Drawing.Point(80, 106);
            this.hScrollBarEnvelopeSustainValue.Maximum = 127;
            this.hScrollBarEnvelopeSustainValue.Name = "hScrollBarEnvelopeSustainValue";
            this.hScrollBarEnvelopeSustainValue.Size = new System.Drawing.Size(120, 18);
            this.hScrollBarEnvelopeSustainValue.TabIndex = 60;
            this.hScrollBarEnvelopeSustainValue.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarEnvelopeSustainValue_Scroll);
            // 
            // hScrollBarEnvelopeDecayValue
            // 
            this.hScrollBarEnvelopeDecayValue.LargeChange = 1;
            this.hScrollBarEnvelopeDecayValue.Location = new System.Drawing.Point(80, 61);
            this.hScrollBarEnvelopeDecayValue.Maximum = 255;
            this.hScrollBarEnvelopeDecayValue.Name = "hScrollBarEnvelopeDecayValue";
            this.hScrollBarEnvelopeDecayValue.Size = new System.Drawing.Size(120, 18);
            this.hScrollBarEnvelopeDecayValue.TabIndex = 59;
            this.hScrollBarEnvelopeDecayValue.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarEnvelopeDecayValue_Scroll);
            // 
            // TextBoxEnvelopeSustainValue
            // 
            this.TextBoxEnvelopeSustainValue.Location = new System.Drawing.Point(80, 124);
            this.TextBoxEnvelopeSustainValue.Name = "TextBoxEnvelopeSustainValue";
            this.TextBoxEnvelopeSustainValue.Size = new System.Drawing.Size(120, 20);
            this.TextBoxEnvelopeSustainValue.TabIndex = 50;
            this.TextBoxEnvelopeSustainValue.Text = "0";
            this.TextBoxEnvelopeSustainValue.Click += new System.EventHandler(this.TextBoxEnvelopeSustainValue_Click);
            this.TextBoxEnvelopeSustainValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxEnvelopeSustainValue_KeyDown);
            // 
            // label66
            // 
            this.label66.AutoSize = true;
            this.label66.Location = new System.Drawing.Point(6, 108);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(71, 13);
            this.label66.TabIndex = 49;
            this.label66.Text = "Sustain Level";
            // 
            // TextBoxEnvelopeDecayValue
            // 
            this.TextBoxEnvelopeDecayValue.Location = new System.Drawing.Point(80, 79);
            this.TextBoxEnvelopeDecayValue.Name = "TextBoxEnvelopeDecayValue";
            this.TextBoxEnvelopeDecayValue.Size = new System.Drawing.Size(120, 20);
            this.TextBoxEnvelopeDecayValue.TabIndex = 47;
            this.TextBoxEnvelopeDecayValue.Text = "0";
            this.TextBoxEnvelopeDecayValue.Click += new System.EventHandler(this.TextBoxEnvelopeDecayValue_Click);
            this.TextBoxEnvelopeDecayValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxEnvelopeDecayValue_KeyDown);
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.Location = new System.Drawing.Point(6, 63);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(38, 13);
            this.label67.TabIndex = 46;
            this.label67.Text = "Decay";
            // 
            // label75
            // 
            this.label75.AutoSize = true;
            this.label75.Location = new System.Drawing.Point(6, 197);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(29, 13);
            this.label75.TabIndex = 42;
            this.label75.Text = "Gain";
            // 
            // TextBoxEnvelopeGainValue
            // 
            this.TextBoxEnvelopeGainValue.Location = new System.Drawing.Point(80, 213);
            this.TextBoxEnvelopeGainValue.Name = "TextBoxEnvelopeGainValue";
            this.TextBoxEnvelopeGainValue.Size = new System.Drawing.Size(120, 20);
            this.TextBoxEnvelopeGainValue.TabIndex = 41;
            this.TextBoxEnvelopeGainValue.Text = "0";
            this.TextBoxEnvelopeGainValue.Click += new System.EventHandler(this.TextBoxEnvelopeGainValue_Click);
            this.TextBoxEnvelopeGainValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxEnvelopeGainValue_KeyDown);
            // 
            // GroupBoxMul
            // 
            this.GroupBoxMul.BorderColor = System.Drawing.Color.White;
            this.GroupBoxMul.Controls.Add(this.label30);
            this.GroupBoxMul.Controls.Add(this.label22);
            this.GroupBoxMul.Controls.Add(this.TextBoxMulVal2ValueFloat);
            this.GroupBoxMul.Controls.Add(this.hScrollBarMulVal2Value);
            this.GroupBoxMul.Controls.Add(this.TextBoxMulVal2Value);
            this.GroupBoxMul.Controls.Add(this.label45);
            this.GroupBoxMul.Controls.Add(this.label46);
            this.GroupBoxMul.Controls.Add(this.ComboBoxMulVal2);
            this.GroupBoxMul.Controls.Add(this.ComboBoxMulVal1);
            this.GroupBoxMul.ForeColor = System.Drawing.Color.White;
            this.GroupBoxMul.Location = new System.Drawing.Point(416, 198);
            this.GroupBoxMul.Name = "GroupBoxMul";
            this.GroupBoxMul.Size = new System.Drawing.Size(212, 260);
            this.GroupBoxMul.TabIndex = 47;
            this.GroupBoxMul.TabStop = false;
            this.GroupBoxMul.Text = "Multiplication";
            this.GroupBoxMul.Visible = false;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(145, 103);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(30, 13);
            this.label30.TabIndex = 59;
            this.label30.Text = "Float";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(145, 85);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(45, 13);
            this.label22.TabIndex = 58;
            this.label22.Text = "Decimal";
            // 
            // TextBoxMulVal2ValueFloat
            // 
            this.TextBoxMulVal2ValueFloat.Location = new System.Drawing.Point(80, 98);
            this.TextBoxMulVal2ValueFloat.Name = "TextBoxMulVal2ValueFloat";
            this.TextBoxMulVal2ValueFloat.ReadOnly = true;
            this.TextBoxMulVal2ValueFloat.Size = new System.Drawing.Size(59, 20);
            this.TextBoxMulVal2ValueFloat.TabIndex = 57;
            this.TextBoxMulVal2ValueFloat.Text = "0";
            // 
            // hScrollBarMulVal2Value
            // 
            this.hScrollBarMulVal2Value.LargeChange = 64;
            this.hScrollBarMulVal2Value.Location = new System.Drawing.Point(80, 61);
            this.hScrollBarMulVal2Value.Maximum = 32830;
            this.hScrollBarMulVal2Value.Minimum = -32768;
            this.hScrollBarMulVal2Value.Name = "hScrollBarMulVal2Value";
            this.hScrollBarMulVal2Value.Size = new System.Drawing.Size(120, 18);
            this.hScrollBarMulVal2Value.TabIndex = 56;
            this.hScrollBarMulVal2Value.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarMulVal2Value_Scroll);
            // 
            // TextBoxMulVal2Value
            // 
            this.TextBoxMulVal2Value.Location = new System.Drawing.Point(80, 79);
            this.TextBoxMulVal2Value.Name = "TextBoxMulVal2Value";
            this.TextBoxMulVal2Value.Size = new System.Drawing.Size(59, 20);
            this.TextBoxMulVal2Value.TabIndex = 34;
            this.TextBoxMulVal2Value.Text = "0";
            this.TextBoxMulVal2Value.Click += new System.EventHandler(this.TextBoxMulVal2Value_Click);
            this.TextBoxMulVal2Value.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxMulVal2Value_KeyDown);
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(6, 63);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(43, 13);
            this.label45.TabIndex = 28;
            this.label45.Text = "Value 2";
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(6, 18);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(43, 13);
            this.label46.TabIndex = 27;
            this.label46.Text = "Value 1";
            // 
            // ComboBoxMulVal2
            // 
            this.ComboBoxMulVal2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxMulVal2.FormattingEnabled = true;
            this.ComboBoxMulVal2.Items.AddRange(new object[] {
            "Value",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxMulVal2.Location = new System.Drawing.Point(6, 78);
            this.ComboBoxMulVal2.Name = "ComboBoxMulVal2";
            this.ComboBoxMulVal2.Size = new System.Drawing.Size(62, 21);
            this.ComboBoxMulVal2.TabIndex = 11;
            this.ComboBoxMulVal2.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxMulVal2_SelectionChangeCommitted);
            // 
            // ComboBoxMulVal1
            // 
            this.ComboBoxMulVal1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxMulVal1.FormattingEnabled = true;
            this.ComboBoxMulVal1.Items.AddRange(new object[] {
            "-",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxMulVal1.Location = new System.Drawing.Point(6, 33);
            this.ComboBoxMulVal1.Name = "ComboBoxMulVal1";
            this.ComboBoxMulVal1.Size = new System.Drawing.Size(62, 21);
            this.ComboBoxMulVal1.TabIndex = 10;
            this.ComboBoxMulVal1.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxMulVal1_SelectionChangeCommitted);
            // 
            // GroupBoxDelay
            // 
            this.GroupBoxDelay.BorderColor = System.Drawing.Color.White;
            this.GroupBoxDelay.Controls.Add(this.labelDelay);
            this.GroupBoxDelay.Controls.Add(this.hScrollBarDelayGainValue);
            this.GroupBoxDelay.Controls.Add(this.hScrollBarDelayDelayValue);
            this.GroupBoxDelay.Controls.Add(this.ComboBoxDelayDelay);
            this.GroupBoxDelay.Controls.Add(this.TextBoxDelayDelayValue);
            this.GroupBoxDelay.Controls.Add(this.ComboBoxDelayValue);
            this.GroupBoxDelay.Controls.Add(this.label51);
            this.GroupBoxDelay.Controls.Add(this.ComboBoxDelayGain);
            this.GroupBoxDelay.Controls.Add(this.label52);
            this.GroupBoxDelay.Controls.Add(this.TextBoxDelayGainValue);
            this.GroupBoxDelay.Controls.Add(this.label29);
            this.GroupBoxDelay.ForeColor = System.Drawing.Color.White;
            this.GroupBoxDelay.Location = new System.Drawing.Point(416, 198);
            this.GroupBoxDelay.Name = "GroupBoxDelay";
            this.GroupBoxDelay.Size = new System.Drawing.Size(212, 260);
            this.GroupBoxDelay.TabIndex = 48;
            this.GroupBoxDelay.TabStop = false;
            this.GroupBoxDelay.Text = "Delay";
            this.GroupBoxDelay.Visible = false;
            // 
            // labelDelay
            // 
            this.labelDelay.AutoSize = true;
            this.labelDelay.Location = new System.Drawing.Point(6, 239);
            this.labelDelay.Name = "labelDelay";
            this.labelDelay.Size = new System.Drawing.Size(170, 13);
            this.labelDelay.TabIndex = 132;
            this.labelDelay.Text = "Max. 8 comb/delay per instrument!";
            // 
            // hScrollBarDelayGainValue
            // 
            this.hScrollBarDelayGainValue.LargeChange = 1;
            this.hScrollBarDelayGainValue.Location = new System.Drawing.Point(80, 196);
            this.hScrollBarDelayGainValue.Maximum = 128;
            this.hScrollBarDelayGainValue.Name = "hScrollBarDelayGainValue";
            this.hScrollBarDelayGainValue.Size = new System.Drawing.Size(120, 18);
            this.hScrollBarDelayGainValue.TabIndex = 59;
            this.hScrollBarDelayGainValue.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarDelayGainValue_Scroll);
            // 
            // hScrollBarDelayDelayValue
            // 
            this.hScrollBarDelayDelayValue.LargeChange = 1;
            this.hScrollBarDelayDelayValue.Location = new System.Drawing.Point(80, 61);
            this.hScrollBarDelayDelayValue.Maximum = 2047;
            this.hScrollBarDelayDelayValue.Name = "hScrollBarDelayDelayValue";
            this.hScrollBarDelayDelayValue.Size = new System.Drawing.Size(120, 18);
            this.hScrollBarDelayDelayValue.TabIndex = 57;
            this.hScrollBarDelayDelayValue.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarDelayDelayValue_Scroll);
            // 
            // ComboBoxDelayDelay
            // 
            this.ComboBoxDelayDelay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxDelayDelay.FormattingEnabled = true;
            this.ComboBoxDelayDelay.Items.AddRange(new object[] {
            "Value",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxDelayDelay.Location = new System.Drawing.Point(6, 78);
            this.ComboBoxDelayDelay.Name = "ComboBoxDelayDelay";
            this.ComboBoxDelayDelay.Size = new System.Drawing.Size(62, 21);
            this.ComboBoxDelayDelay.TabIndex = 55;
            this.ComboBoxDelayDelay.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxDelayDelay_SelectionChangeCommitted);
            // 
            // TextBoxDelayDelayValue
            // 
            this.TextBoxDelayDelayValue.Location = new System.Drawing.Point(80, 79);
            this.TextBoxDelayDelayValue.Name = "TextBoxDelayDelayValue";
            this.TextBoxDelayDelayValue.Size = new System.Drawing.Size(120, 20);
            this.TextBoxDelayDelayValue.TabIndex = 56;
            this.TextBoxDelayDelayValue.Text = "0";
            this.TextBoxDelayDelayValue.Click += new System.EventHandler(this.TextBoxDelayDelayValue_Click);
            this.TextBoxDelayDelayValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxDelayDelayValue_KeyDown);
            // 
            // ComboBoxDelayValue
            // 
            this.ComboBoxDelayValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxDelayValue.FormattingEnabled = true;
            this.ComboBoxDelayValue.Items.AddRange(new object[] {
            "-",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxDelayValue.Location = new System.Drawing.Point(6, 33);
            this.ComboBoxDelayValue.Name = "ComboBoxDelayValue";
            this.ComboBoxDelayValue.Size = new System.Drawing.Size(62, 21);
            this.ComboBoxDelayValue.TabIndex = 44;
            this.ComboBoxDelayValue.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxDelayValue_SelectionChangeCommitted);
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(6, 198);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(29, 13);
            this.label51.TabIndex = 37;
            this.label51.Text = "Gain";
            // 
            // ComboBoxDelayGain
            // 
            this.ComboBoxDelayGain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxDelayGain.FormattingEnabled = true;
            this.ComboBoxDelayGain.Items.AddRange(new object[] {
            "Value",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxDelayGain.Location = new System.Drawing.Point(6, 213);
            this.ComboBoxDelayGain.Name = "ComboBoxDelayGain";
            this.ComboBoxDelayGain.Size = new System.Drawing.Size(62, 21);
            this.ComboBoxDelayGain.TabIndex = 32;
            this.ComboBoxDelayGain.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxDelayGain_SelectionChangeCommitted);
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(6, 18);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(34, 13);
            this.label52.TabIndex = 36;
            this.label52.Text = "Value";
            // 
            // TextBoxDelayGainValue
            // 
            this.TextBoxDelayGainValue.Location = new System.Drawing.Point(80, 214);
            this.TextBoxDelayGainValue.Name = "TextBoxDelayGainValue";
            this.TextBoxDelayGainValue.Size = new System.Drawing.Size(120, 20);
            this.TextBoxDelayGainValue.TabIndex = 34;
            this.TextBoxDelayGainValue.Text = "0";
            this.TextBoxDelayGainValue.Click += new System.EventHandler(this.TextBoxDelayGainValue_Click);
            this.TextBoxDelayGainValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxDelayGainValue_KeyDown);
            // 
            // hScrollBarSamplelength
            // 
            this.hScrollBarSamplelength.LargeChange = 64;
            this.hScrollBarSamplelength.Location = new System.Drawing.Point(6, 38);
            this.hScrollBarSamplelength.Maximum = 65596;
            this.hScrollBarSamplelength.Name = "hScrollBarSamplelength";
            this.hScrollBarSamplelength.Size = new System.Drawing.Size(200, 21);
            this.hScrollBarSamplelength.SmallChange = 2;
            this.hScrollBarSamplelength.TabIndex = 49;
            this.hScrollBarSamplelength.Value = 1000;
            this.hScrollBarSamplelength.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarSamplelength_Scroll);
            // 
            // ButtonEdit6
            // 
            this.ButtonEdit6.ForeColor = System.Drawing.Color.Black;
            this.ButtonEdit6.Location = new System.Drawing.Point(183, 149);
            this.ButtonEdit6.Name = "ButtonEdit6";
            this.ButtonEdit6.Size = new System.Drawing.Size(43, 23);
            this.ButtonEdit6.TabIndex = 52;
            this.ButtonEdit6.Text = "Edit";
            this.ButtonEdit6.UseVisualStyleBackColor = true;
            this.ButtonEdit6.Click += new System.EventHandler(this.ButtonEdit6_Click);
            // 
            // ComboBoxFunction6
            // 
            this.ComboBoxFunction6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxFunction6.FormattingEnabled = true;
            this.ComboBoxFunction6.Items.AddRange(new object[] {
            "-",
            "Volume",
            "Osc Saw",
            "Osc Triangle",
            "Osc Sine",
            "Osc Pulse",
            "Osc Noise",
            "Attack Envelope",
            "Decay Envelope",
            "Add",
            "Multiply",
            "Delay",
            "Comb Filter",
            "Reverb",
            "Ctrl Signal",
            "SV Filter",
            "Distortion",
            "Clone Sample",
            "Chord Gen",
            "Sample and Hold",
            "Imported Sample",
            "1-Pole Filter",
            "---",
            "ADSR Envelope"});
            this.ComboBoxFunction6.Location = new System.Drawing.Point(74, 149);
            this.ComboBoxFunction6.Name = "ComboBoxFunction6";
            this.ComboBoxFunction6.Size = new System.Drawing.Size(104, 21);
            this.ComboBoxFunction6.TabIndex = 51;
            this.ComboBoxFunction6.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxFunction6_SelectionChangeCommitted);
            // 
            // ComboBoxVar6
            // 
            this.ComboBoxVar6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxVar6.FormattingEnabled = true;
            this.ComboBoxVar6.Items.AddRange(new object[] {
            "-",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxVar6.Location = new System.Drawing.Point(23, 149);
            this.ComboBoxVar6.Name = "ComboBoxVar6";
            this.ComboBoxVar6.Size = new System.Drawing.Size(45, 21);
            this.ComboBoxVar6.TabIndex = 50;
            this.ComboBoxVar6.SelectedIndexChanged += new System.EventHandler(this.ComboBoxVar6_SelectedIndexChanged);
            // 
            // GroupBoxComb
            // 
            this.GroupBoxComb.BorderColor = System.Drawing.Color.White;
            this.GroupBoxComb.Controls.Add(this.labelComb);
            this.GroupBoxComb.Controls.Add(this.hScrollBarCombGainValue);
            this.GroupBoxComb.Controls.Add(this.hScrollBarCombFeedbackValue);
            this.GroupBoxComb.Controls.Add(this.hScrollBarCombDelayValue);
            this.GroupBoxComb.Controls.Add(this.ComboBoxCombDelay);
            this.GroupBoxComb.Controls.Add(this.TextBoxCombDelayValue);
            this.GroupBoxComb.Controls.Add(this.label7);
            this.GroupBoxComb.Controls.Add(this.ComboBoxCombFeedback);
            this.GroupBoxComb.Controls.Add(this.TextBoxCombFeedbackValue);
            this.GroupBoxComb.Controls.Add(this.ComboBoxCombValue);
            this.GroupBoxComb.Controls.Add(this.label8);
            this.GroupBoxComb.Controls.Add(this.ComboBoxCombGain);
            this.GroupBoxComb.Controls.Add(this.label9);
            this.GroupBoxComb.Controls.Add(this.TextBoxCombGainValue);
            this.GroupBoxComb.Controls.Add(this.label19);
            this.GroupBoxComb.ForeColor = System.Drawing.Color.White;
            this.GroupBoxComb.Location = new System.Drawing.Point(416, 198);
            this.GroupBoxComb.Name = "GroupBoxComb";
            this.GroupBoxComb.Size = new System.Drawing.Size(212, 260);
            this.GroupBoxComb.TabIndex = 53;
            this.GroupBoxComb.TabStop = false;
            this.GroupBoxComb.Text = "Comb Filter";
            this.GroupBoxComb.Visible = false;
            // 
            // labelComb
            // 
            this.labelComb.AutoSize = true;
            this.labelComb.Location = new System.Drawing.Point(7, 239);
            this.labelComb.Name = "labelComb";
            this.labelComb.Size = new System.Drawing.Size(170, 13);
            this.labelComb.TabIndex = 131;
            this.labelComb.Text = "Max. 8 comb/delay per instrument!";
            // 
            // hScrollBarCombGainValue
            // 
            this.hScrollBarCombGainValue.LargeChange = 1;
            this.hScrollBarCombGainValue.Location = new System.Drawing.Point(80, 196);
            this.hScrollBarCombGainValue.Maximum = 128;
            this.hScrollBarCombGainValue.Name = "hScrollBarCombGainValue";
            this.hScrollBarCombGainValue.Size = new System.Drawing.Size(120, 18);
            this.hScrollBarCombGainValue.TabIndex = 59;
            this.hScrollBarCombGainValue.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarCombGainValue_Scroll);
            // 
            // hScrollBarCombFeedbackValue
            // 
            this.hScrollBarCombFeedbackValue.LargeChange = 1;
            this.hScrollBarCombFeedbackValue.Location = new System.Drawing.Point(80, 106);
            this.hScrollBarCombFeedbackValue.Maximum = 127;
            this.hScrollBarCombFeedbackValue.Name = "hScrollBarCombFeedbackValue";
            this.hScrollBarCombFeedbackValue.Size = new System.Drawing.Size(120, 18);
            this.hScrollBarCombFeedbackValue.TabIndex = 58;
            this.hScrollBarCombFeedbackValue.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarCombFeedbackValue_Scroll);
            // 
            // hScrollBarCombDelayValue
            // 
            this.hScrollBarCombDelayValue.LargeChange = 1;
            this.hScrollBarCombDelayValue.Location = new System.Drawing.Point(80, 61);
            this.hScrollBarCombDelayValue.Maximum = 2047;
            this.hScrollBarCombDelayValue.Name = "hScrollBarCombDelayValue";
            this.hScrollBarCombDelayValue.Size = new System.Drawing.Size(120, 18);
            this.hScrollBarCombDelayValue.TabIndex = 57;
            this.hScrollBarCombDelayValue.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarCombDelayValue_Scroll);
            // 
            // ComboBoxCombDelay
            // 
            this.ComboBoxCombDelay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxCombDelay.FormattingEnabled = true;
            this.ComboBoxCombDelay.Items.AddRange(new object[] {
            "Value",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxCombDelay.Location = new System.Drawing.Point(6, 78);
            this.ComboBoxCombDelay.Name = "ComboBoxCombDelay";
            this.ComboBoxCombDelay.Size = new System.Drawing.Size(62, 21);
            this.ComboBoxCombDelay.TabIndex = 55;
            this.ComboBoxCombDelay.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxCombDelay_SelectionChangeCommitted);
            // 
            // TextBoxCombDelayValue
            // 
            this.TextBoxCombDelayValue.Location = new System.Drawing.Point(80, 79);
            this.TextBoxCombDelayValue.Name = "TextBoxCombDelayValue";
            this.TextBoxCombDelayValue.Size = new System.Drawing.Size(120, 20);
            this.TextBoxCombDelayValue.TabIndex = 56;
            this.TextBoxCombDelayValue.Text = "0";
            this.TextBoxCombDelayValue.Click += new System.EventHandler(this.TextBoxCombDelayValue_Click);
            this.TextBoxCombDelayValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxCombDelayValue_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 108);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 47;
            this.label7.Text = "Feedback";
            // 
            // ComboBoxCombFeedback
            // 
            this.ComboBoxCombFeedback.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxCombFeedback.FormattingEnabled = true;
            this.ComboBoxCombFeedback.Items.AddRange(new object[] {
            "Value",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxCombFeedback.Location = new System.Drawing.Point(6, 123);
            this.ComboBoxCombFeedback.Name = "ComboBoxCombFeedback";
            this.ComboBoxCombFeedback.Size = new System.Drawing.Size(62, 21);
            this.ComboBoxCombFeedback.TabIndex = 45;
            this.ComboBoxCombFeedback.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxCombFeedback_SelectionChangeCommitted);
            // 
            // TextBoxCombFeedbackValue
            // 
            this.TextBoxCombFeedbackValue.Location = new System.Drawing.Point(80, 124);
            this.TextBoxCombFeedbackValue.Name = "TextBoxCombFeedbackValue";
            this.TextBoxCombFeedbackValue.Size = new System.Drawing.Size(120, 20);
            this.TextBoxCombFeedbackValue.TabIndex = 46;
            this.TextBoxCombFeedbackValue.Text = "0";
            this.TextBoxCombFeedbackValue.Click += new System.EventHandler(this.TextBoxCombFeedbackValue_Click);
            this.TextBoxCombFeedbackValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxCombFeedbackValue_KeyDown);
            // 
            // ComboBoxCombValue
            // 
            this.ComboBoxCombValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxCombValue.FormattingEnabled = true;
            this.ComboBoxCombValue.Items.AddRange(new object[] {
            "-",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxCombValue.Location = new System.Drawing.Point(6, 33);
            this.ComboBoxCombValue.Name = "ComboBoxCombValue";
            this.ComboBoxCombValue.Size = new System.Drawing.Size(62, 21);
            this.ComboBoxCombValue.TabIndex = 44;
            this.ComboBoxCombValue.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxCombValue_SelectionChangeCommitted);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 198);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 13);
            this.label8.TabIndex = 37;
            this.label8.Text = "Gain";
            // 
            // ComboBoxCombGain
            // 
            this.ComboBoxCombGain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxCombGain.FormattingEnabled = true;
            this.ComboBoxCombGain.Items.AddRange(new object[] {
            "Value",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxCombGain.Location = new System.Drawing.Point(6, 213);
            this.ComboBoxCombGain.Name = "ComboBoxCombGain";
            this.ComboBoxCombGain.Size = new System.Drawing.Size(62, 21);
            this.ComboBoxCombGain.TabIndex = 32;
            this.ComboBoxCombGain.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxCombGain_SelectionChangeCommitted);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 18);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(34, 13);
            this.label9.TabIndex = 36;
            this.label9.Text = "Value";
            // 
            // TextBoxCombGainValue
            // 
            this.TextBoxCombGainValue.Location = new System.Drawing.Point(80, 214);
            this.TextBoxCombGainValue.Name = "TextBoxCombGainValue";
            this.TextBoxCombGainValue.Size = new System.Drawing.Size(120, 20);
            this.TextBoxCombGainValue.TabIndex = 34;
            this.TextBoxCombGainValue.Text = "0";
            this.TextBoxCombGainValue.Click += new System.EventHandler(this.TextBoxCombGainValue_Click);
            this.TextBoxCombGainValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxCombGainValue_KeyDown);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(6, 63);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(34, 13);
            this.label19.TabIndex = 36;
            this.label19.Text = "Delay";
            // 
            // GroupBoxReverb
            // 
            this.GroupBoxReverb.BorderColor = System.Drawing.Color.White;
            this.GroupBoxReverb.Controls.Add(this.labelReverb);
            this.GroupBoxReverb.Controls.Add(this.hScrollBarReverbGainValue);
            this.GroupBoxReverb.Controls.Add(this.hScrollBarReverbFeedbackValue);
            this.GroupBoxReverb.Controls.Add(this.label26);
            this.GroupBoxReverb.Controls.Add(this.ComboBoxReverbFeedback);
            this.GroupBoxReverb.Controls.Add(this.TextBoxReverbFeedbackValue);
            this.GroupBoxReverb.Controls.Add(this.ComboBoxReverbValue);
            this.GroupBoxReverb.Controls.Add(this.label27);
            this.GroupBoxReverb.Controls.Add(this.ComboBoxReverbGain);
            this.GroupBoxReverb.Controls.Add(this.label31);
            this.GroupBoxReverb.Controls.Add(this.TextBoxReverbGainValue);
            this.GroupBoxReverb.ForeColor = System.Drawing.Color.White;
            this.GroupBoxReverb.Location = new System.Drawing.Point(416, 198);
            this.GroupBoxReverb.Name = "GroupBoxReverb";
            this.GroupBoxReverb.Size = new System.Drawing.Size(212, 260);
            this.GroupBoxReverb.TabIndex = 54;
            this.GroupBoxReverb.TabStop = false;
            this.GroupBoxReverb.Text = "Reverb";
            this.GroupBoxReverb.Visible = false;
            // 
            // labelReverb
            // 
            this.labelReverb.AutoSize = true;
            this.labelReverb.Location = new System.Drawing.Point(7, 239);
            this.labelReverb.Name = "labelReverb";
            this.labelReverb.Size = new System.Drawing.Size(162, 13);
            this.labelReverb.TabIndex = 131;
            this.labelReverb.Text = "Only use 1 reverb per instrument!";
            // 
            // hScrollBarReverbGainValue
            // 
            this.hScrollBarReverbGainValue.LargeChange = 1;
            this.hScrollBarReverbGainValue.Location = new System.Drawing.Point(80, 196);
            this.hScrollBarReverbGainValue.Maximum = 128;
            this.hScrollBarReverbGainValue.Name = "hScrollBarReverbGainValue";
            this.hScrollBarReverbGainValue.Size = new System.Drawing.Size(120, 18);
            this.hScrollBarReverbGainValue.TabIndex = 59;
            this.hScrollBarReverbGainValue.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarReverbGainValue_Scroll);
            // 
            // hScrollBarReverbFeedbackValue
            // 
            this.hScrollBarReverbFeedbackValue.LargeChange = 1;
            this.hScrollBarReverbFeedbackValue.Location = new System.Drawing.Point(80, 106);
            this.hScrollBarReverbFeedbackValue.Maximum = 127;
            this.hScrollBarReverbFeedbackValue.Name = "hScrollBarReverbFeedbackValue";
            this.hScrollBarReverbFeedbackValue.Size = new System.Drawing.Size(120, 18);
            this.hScrollBarReverbFeedbackValue.TabIndex = 58;
            this.hScrollBarReverbFeedbackValue.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarReverbFeedbackValue_Scroll);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(6, 108);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(55, 13);
            this.label26.TabIndex = 47;
            this.label26.Text = "Feedback";
            // 
            // ComboBoxReverbFeedback
            // 
            this.ComboBoxReverbFeedback.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxReverbFeedback.FormattingEnabled = true;
            this.ComboBoxReverbFeedback.Items.AddRange(new object[] {
            "Value",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxReverbFeedback.Location = new System.Drawing.Point(6, 123);
            this.ComboBoxReverbFeedback.Name = "ComboBoxReverbFeedback";
            this.ComboBoxReverbFeedback.Size = new System.Drawing.Size(62, 21);
            this.ComboBoxReverbFeedback.TabIndex = 45;
            this.ComboBoxReverbFeedback.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxReverbFeedback_SelectionChangeCommitted);
            // 
            // TextBoxReverbFeedbackValue
            // 
            this.TextBoxReverbFeedbackValue.Location = new System.Drawing.Point(80, 124);
            this.TextBoxReverbFeedbackValue.Name = "TextBoxReverbFeedbackValue";
            this.TextBoxReverbFeedbackValue.Size = new System.Drawing.Size(120, 20);
            this.TextBoxReverbFeedbackValue.TabIndex = 46;
            this.TextBoxReverbFeedbackValue.Text = "0";
            this.TextBoxReverbFeedbackValue.Click += new System.EventHandler(this.TextBoxReverbFeedbackValue_Click);
            this.TextBoxReverbFeedbackValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxReverbFeedbackValue_KeyDown);
            // 
            // ComboBoxReverbValue
            // 
            this.ComboBoxReverbValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxReverbValue.FormattingEnabled = true;
            this.ComboBoxReverbValue.Items.AddRange(new object[] {
            "-",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxReverbValue.Location = new System.Drawing.Point(6, 33);
            this.ComboBoxReverbValue.Name = "ComboBoxReverbValue";
            this.ComboBoxReverbValue.Size = new System.Drawing.Size(62, 21);
            this.ComboBoxReverbValue.TabIndex = 44;
            this.ComboBoxReverbValue.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxReverbValue_SelectionChangeCommitted);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(6, 198);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(29, 13);
            this.label27.TabIndex = 37;
            this.label27.Text = "Gain";
            // 
            // ComboBoxReverbGain
            // 
            this.ComboBoxReverbGain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxReverbGain.FormattingEnabled = true;
            this.ComboBoxReverbGain.Items.AddRange(new object[] {
            "Value",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxReverbGain.Location = new System.Drawing.Point(6, 213);
            this.ComboBoxReverbGain.Name = "ComboBoxReverbGain";
            this.ComboBoxReverbGain.Size = new System.Drawing.Size(62, 21);
            this.ComboBoxReverbGain.TabIndex = 32;
            this.ComboBoxReverbGain.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxReverbGain_SelectionChangeCommitted);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(6, 18);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(34, 13);
            this.label31.TabIndex = 36;
            this.label31.Text = "Value";
            // 
            // TextBoxReverbGainValue
            // 
            this.TextBoxReverbGainValue.Location = new System.Drawing.Point(80, 214);
            this.TextBoxReverbGainValue.Name = "TextBoxReverbGainValue";
            this.TextBoxReverbGainValue.Size = new System.Drawing.Size(120, 20);
            this.TextBoxReverbGainValue.TabIndex = 34;
            this.TextBoxReverbGainValue.Text = "0";
            this.TextBoxReverbGainValue.Click += new System.EventHandler(this.TextBoxReverbGainValue_Click);
            this.TextBoxReverbGainValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxReverbGainValue_KeyDown);
            // 
            // GroupBoxCtrl
            // 
            this.GroupBoxCtrl.BorderColor = System.Drawing.Color.White;
            this.GroupBoxCtrl.Controls.Add(this.labelCtrl);
            this.GroupBoxCtrl.Controls.Add(this.label33);
            this.GroupBoxCtrl.Controls.Add(this.ComboBoxCtrlValue);
            this.GroupBoxCtrl.ForeColor = System.Drawing.Color.White;
            this.GroupBoxCtrl.Location = new System.Drawing.Point(416, 198);
            this.GroupBoxCtrl.Name = "GroupBoxCtrl";
            this.GroupBoxCtrl.Size = new System.Drawing.Size(212, 260);
            this.GroupBoxCtrl.TabIndex = 55;
            this.GroupBoxCtrl.TabStop = false;
            this.GroupBoxCtrl.Text = "Control Signal";
            this.GroupBoxCtrl.Visible = false;
            // 
            // labelCtrl
            // 
            this.labelCtrl.AutoSize = true;
            this.labelCtrl.Location = new System.Drawing.Point(6, 227);
            this.labelCtrl.Name = "labelCtrl";
            this.labelCtrl.Size = new System.Drawing.Size(201, 26);
            this.labelCtrl.TabIndex = 130;
            this.labelCtrl.Text = "Skaling an audio signal down to a control\r\nsignal.  (-32768...32767) -> (0...127)" +
    "";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(6, 18);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(34, 13);
            this.label33.TabIndex = 29;
            this.label33.Text = "Value";
            // 
            // ComboBoxCtrlValue
            // 
            this.ComboBoxCtrlValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxCtrlValue.FormattingEnabled = true;
            this.ComboBoxCtrlValue.Items.AddRange(new object[] {
            "-",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxCtrlValue.Location = new System.Drawing.Point(6, 33);
            this.ComboBoxCtrlValue.Name = "ComboBoxCtrlValue";
            this.ComboBoxCtrlValue.Size = new System.Drawing.Size(62, 21);
            this.ComboBoxCtrlValue.TabIndex = 9;
            this.ComboBoxCtrlValue.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxCtrlValue_SelectionChangeCommitted);
            // 
            // GroupBoxFilter
            // 
            this.GroupBoxFilter.BorderColor = System.Drawing.Color.White;
            this.GroupBoxFilter.Controls.Add(this.hScrollBarFilterResonanceValue);
            this.GroupBoxFilter.Controls.Add(this.hScrollBarFilterCutoffValue);
            this.GroupBoxFilter.Controls.Add(this.ComboBoxFilterCutoff);
            this.GroupBoxFilter.Controls.Add(this.TextBoxFilterCutoffValue);
            this.GroupBoxFilter.Controls.Add(this.label34);
            this.GroupBoxFilter.Controls.Add(this.ComboBoxFilterResonance);
            this.GroupBoxFilter.Controls.Add(this.TextBoxFilterResonanceValue);
            this.GroupBoxFilter.Controls.Add(this.ComboBoxFilterValue);
            this.GroupBoxFilter.Controls.Add(this.label35);
            this.GroupBoxFilter.Controls.Add(this.ComboBoxFilterMode);
            this.GroupBoxFilter.Controls.Add(this.label36);
            this.GroupBoxFilter.Controls.Add(this.label43);
            this.GroupBoxFilter.ForeColor = System.Drawing.Color.White;
            this.GroupBoxFilter.Location = new System.Drawing.Point(416, 198);
            this.GroupBoxFilter.Name = "GroupBoxFilter";
            this.GroupBoxFilter.Size = new System.Drawing.Size(212, 260);
            this.GroupBoxFilter.TabIndex = 56;
            this.GroupBoxFilter.TabStop = false;
            this.GroupBoxFilter.Text = "SV Filter";
            this.GroupBoxFilter.Visible = false;
            // 
            // hScrollBarFilterResonanceValue
            // 
            this.hScrollBarFilterResonanceValue.LargeChange = 1;
            this.hScrollBarFilterResonanceValue.Location = new System.Drawing.Point(80, 106);
            this.hScrollBarFilterResonanceValue.Maximum = 127;
            this.hScrollBarFilterResonanceValue.Name = "hScrollBarFilterResonanceValue";
            this.hScrollBarFilterResonanceValue.Size = new System.Drawing.Size(120, 18);
            this.hScrollBarFilterResonanceValue.TabIndex = 58;
            this.hScrollBarFilterResonanceValue.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarFilterResonanceValue_Scroll);
            // 
            // hScrollBarFilterCutoffValue
            // 
            this.hScrollBarFilterCutoffValue.LargeChange = 1;
            this.hScrollBarFilterCutoffValue.Location = new System.Drawing.Point(80, 61);
            this.hScrollBarFilterCutoffValue.Maximum = 127;
            this.hScrollBarFilterCutoffValue.Name = "hScrollBarFilterCutoffValue";
            this.hScrollBarFilterCutoffValue.Size = new System.Drawing.Size(120, 18);
            this.hScrollBarFilterCutoffValue.TabIndex = 57;
            this.hScrollBarFilterCutoffValue.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarFilterCutoffValue_Scroll);
            // 
            // ComboBoxFilterCutoff
            // 
            this.ComboBoxFilterCutoff.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxFilterCutoff.FormattingEnabled = true;
            this.ComboBoxFilterCutoff.Items.AddRange(new object[] {
            "Value",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxFilterCutoff.Location = new System.Drawing.Point(6, 78);
            this.ComboBoxFilterCutoff.Name = "ComboBoxFilterCutoff";
            this.ComboBoxFilterCutoff.Size = new System.Drawing.Size(62, 21);
            this.ComboBoxFilterCutoff.TabIndex = 55;
            this.ComboBoxFilterCutoff.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxFilterCutoff_SelectionChangeCommitted);
            // 
            // TextBoxFilterCutoffValue
            // 
            this.TextBoxFilterCutoffValue.Location = new System.Drawing.Point(80, 79);
            this.TextBoxFilterCutoffValue.Name = "TextBoxFilterCutoffValue";
            this.TextBoxFilterCutoffValue.Size = new System.Drawing.Size(120, 20);
            this.TextBoxFilterCutoffValue.TabIndex = 56;
            this.TextBoxFilterCutoffValue.Text = "0";
            this.TextBoxFilterCutoffValue.Click += new System.EventHandler(this.TextBoxFilterCutoffValue_Click);
            this.TextBoxFilterCutoffValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxFilterCutoffValue_KeyDown);
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(6, 108);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(62, 13);
            this.label34.TabIndex = 47;
            this.label34.Text = "Resonance";
            // 
            // ComboBoxFilterResonance
            // 
            this.ComboBoxFilterResonance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxFilterResonance.FormattingEnabled = true;
            this.ComboBoxFilterResonance.Items.AddRange(new object[] {
            "Value",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxFilterResonance.Location = new System.Drawing.Point(6, 123);
            this.ComboBoxFilterResonance.Name = "ComboBoxFilterResonance";
            this.ComboBoxFilterResonance.Size = new System.Drawing.Size(62, 21);
            this.ComboBoxFilterResonance.TabIndex = 45;
            this.ComboBoxFilterResonance.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxFilterResonance_SelectionChangeCommitted);
            // 
            // TextBoxFilterResonanceValue
            // 
            this.TextBoxFilterResonanceValue.Location = new System.Drawing.Point(80, 124);
            this.TextBoxFilterResonanceValue.Name = "TextBoxFilterResonanceValue";
            this.TextBoxFilterResonanceValue.Size = new System.Drawing.Size(120, 20);
            this.TextBoxFilterResonanceValue.TabIndex = 46;
            this.TextBoxFilterResonanceValue.Text = "0";
            this.TextBoxFilterResonanceValue.Click += new System.EventHandler(this.TextBoxFilterResonanceValue_Click);
            this.TextBoxFilterResonanceValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxFilterResonanceValue_KeyDown);
            // 
            // ComboBoxFilterValue
            // 
            this.ComboBoxFilterValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxFilterValue.FormattingEnabled = true;
            this.ComboBoxFilterValue.Items.AddRange(new object[] {
            "-",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxFilterValue.Location = new System.Drawing.Point(6, 33);
            this.ComboBoxFilterValue.Name = "ComboBoxFilterValue";
            this.ComboBoxFilterValue.Size = new System.Drawing.Size(62, 21);
            this.ComboBoxFilterValue.TabIndex = 44;
            this.ComboBoxFilterValue.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxFilterValue_SelectionChangeCommitted);
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(6, 153);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(34, 13);
            this.label35.TabIndex = 37;
            this.label35.Text = "Mode";
            // 
            // ComboBoxFilterMode
            // 
            this.ComboBoxFilterMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxFilterMode.FormattingEnabled = true;
            this.ComboBoxFilterMode.Items.AddRange(new object[] {
            "Lowpass",
            "Highpass",
            "Bandpass",
            "Notch"});
            this.ComboBoxFilterMode.Location = new System.Drawing.Point(6, 168);
            this.ComboBoxFilterMode.Name = "ComboBoxFilterMode";
            this.ComboBoxFilterMode.Size = new System.Drawing.Size(62, 21);
            this.ComboBoxFilterMode.TabIndex = 32;
            this.ComboBoxFilterMode.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxFilterMode_SelectionChangeCommitted);
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(6, 18);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(34, 13);
            this.label36.TabIndex = 36;
            this.label36.Text = "Value";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(6, 63);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(35, 13);
            this.label43.TabIndex = 36;
            this.label43.Text = "Cutoff";
            // 
            // ButtonEdit7
            // 
            this.ButtonEdit7.ForeColor = System.Drawing.Color.Black;
            this.ButtonEdit7.Location = new System.Drawing.Point(183, 176);
            this.ButtonEdit7.Name = "ButtonEdit7";
            this.ButtonEdit7.Size = new System.Drawing.Size(43, 23);
            this.ButtonEdit7.TabIndex = 59;
            this.ButtonEdit7.Text = "Edit";
            this.ButtonEdit7.UseVisualStyleBackColor = true;
            this.ButtonEdit7.Click += new System.EventHandler(this.ButtonEdit7_Click);
            // 
            // ComboBoxFunction7
            // 
            this.ComboBoxFunction7.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxFunction7.FormattingEnabled = true;
            this.ComboBoxFunction7.Items.AddRange(new object[] {
            "-",
            "Volume",
            "Osc Saw",
            "Osc Triangle",
            "Osc Sine",
            "Osc Pulse",
            "Osc Noise",
            "Attack Envelope",
            "Decay Envelope",
            "Add",
            "Multiply",
            "Delay",
            "Comb Filter",
            "Reverb",
            "Ctrl Signal",
            "SV Filter",
            "Distortion",
            "Clone Sample",
            "Chord Gen",
            "Sample and Hold",
            "Imported Sample",
            "1-Pole Filter",
            "---",
            "ADSR Envelope"});
            this.ComboBoxFunction7.Location = new System.Drawing.Point(74, 176);
            this.ComboBoxFunction7.Name = "ComboBoxFunction7";
            this.ComboBoxFunction7.Size = new System.Drawing.Size(104, 21);
            this.ComboBoxFunction7.TabIndex = 58;
            this.ComboBoxFunction7.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxFunction7_SelectionChangeCommitted);
            // 
            // ComboBoxVar7
            // 
            this.ComboBoxVar7.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxVar7.FormattingEnabled = true;
            this.ComboBoxVar7.Items.AddRange(new object[] {
            "-",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxVar7.Location = new System.Drawing.Point(23, 176);
            this.ComboBoxVar7.Name = "ComboBoxVar7";
            this.ComboBoxVar7.Size = new System.Drawing.Size(45, 21);
            this.ComboBoxVar7.TabIndex = 57;
            this.ComboBoxVar7.SelectedIndexChanged += new System.EventHandler(this.ComboBoxVar7_SelectedIndexChanged);
            // 
            // ButtonEdit8
            // 
            this.ButtonEdit8.ForeColor = System.Drawing.Color.Black;
            this.ButtonEdit8.Location = new System.Drawing.Point(183, 203);
            this.ButtonEdit8.Name = "ButtonEdit8";
            this.ButtonEdit8.Size = new System.Drawing.Size(43, 23);
            this.ButtonEdit8.TabIndex = 62;
            this.ButtonEdit8.Text = "Edit";
            this.ButtonEdit8.UseVisualStyleBackColor = true;
            this.ButtonEdit8.Click += new System.EventHandler(this.ButtonEdit8_Click);
            // 
            // ComboBoxFunction8
            // 
            this.ComboBoxFunction8.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxFunction8.FormattingEnabled = true;
            this.ComboBoxFunction8.Items.AddRange(new object[] {
            "-",
            "Volume",
            "Osc Saw",
            "Osc Triangle",
            "Osc Sine",
            "Osc Pulse",
            "Osc Noise",
            "Attack Envelope",
            "Decay Envelope",
            "Add",
            "Multiply",
            "Delay",
            "Comb Filter",
            "Reverb",
            "Ctrl Signal",
            "SV Filter",
            "Distortion",
            "Clone Sample",
            "Chord Gen",
            "Sample and Hold",
            "Imported Sample",
            "1-Pole Filter",
            "---",
            "ADSR Envelope"});
            this.ComboBoxFunction8.Location = new System.Drawing.Point(74, 203);
            this.ComboBoxFunction8.Name = "ComboBoxFunction8";
            this.ComboBoxFunction8.Size = new System.Drawing.Size(104, 21);
            this.ComboBoxFunction8.TabIndex = 61;
            this.ComboBoxFunction8.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxFunction8_SelectionChangeCommitted);
            // 
            // ComboBoxVar8
            // 
            this.ComboBoxVar8.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxVar8.FormattingEnabled = true;
            this.ComboBoxVar8.Items.AddRange(new object[] {
            "-",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxVar8.Location = new System.Drawing.Point(23, 203);
            this.ComboBoxVar8.Name = "ComboBoxVar8";
            this.ComboBoxVar8.Size = new System.Drawing.Size(45, 21);
            this.ComboBoxVar8.TabIndex = 60;
            this.ComboBoxVar8.SelectedIndexChanged += new System.EventHandler(this.ComboBoxVar8_SelectedIndexChanged);
            // 
            // ButtonEdit9
            // 
            this.ButtonEdit9.ForeColor = System.Drawing.Color.Black;
            this.ButtonEdit9.Location = new System.Drawing.Point(183, 230);
            this.ButtonEdit9.Name = "ButtonEdit9";
            this.ButtonEdit9.Size = new System.Drawing.Size(43, 23);
            this.ButtonEdit9.TabIndex = 65;
            this.ButtonEdit9.Text = "Edit";
            this.ButtonEdit9.UseVisualStyleBackColor = true;
            this.ButtonEdit9.Click += new System.EventHandler(this.ButtonEdit9_Click);
            // 
            // ComboBoxFunction9
            // 
            this.ComboBoxFunction9.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxFunction9.FormattingEnabled = true;
            this.ComboBoxFunction9.Items.AddRange(new object[] {
            "-",
            "Volume",
            "Osc Saw",
            "Osc Triangle",
            "Osc Sine",
            "Osc Pulse",
            "Osc Noise",
            "Attack Envelope",
            "Decay Envelope",
            "Add",
            "Multiply",
            "Delay",
            "Comb Filter",
            "Reverb",
            "Ctrl Signal",
            "SV Filter",
            "Distortion",
            "Clone Sample",
            "Chord Gen",
            "Sample and Hold",
            "Imported Sample",
            "1-Pole Filter",
            "---",
            "ADSR Envelope"});
            this.ComboBoxFunction9.Location = new System.Drawing.Point(74, 230);
            this.ComboBoxFunction9.Name = "ComboBoxFunction9";
            this.ComboBoxFunction9.Size = new System.Drawing.Size(104, 21);
            this.ComboBoxFunction9.TabIndex = 64;
            this.ComboBoxFunction9.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxFunction9_SelectionChangeCommitted);
            // 
            // ComboBoxVar9
            // 
            this.ComboBoxVar9.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxVar9.FormattingEnabled = true;
            this.ComboBoxVar9.Items.AddRange(new object[] {
            "-",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxVar9.Location = new System.Drawing.Point(23, 230);
            this.ComboBoxVar9.Name = "ComboBoxVar9";
            this.ComboBoxVar9.Size = new System.Drawing.Size(45, 21);
            this.ComboBoxVar9.TabIndex = 63;
            this.ComboBoxVar9.SelectedIndexChanged += new System.EventHandler(this.ComboBoxVar9_SelectedIndexChanged);
            // 
            // ButtonEdit10
            // 
            this.ButtonEdit10.ForeColor = System.Drawing.Color.Black;
            this.ButtonEdit10.Location = new System.Drawing.Point(183, 257);
            this.ButtonEdit10.Name = "ButtonEdit10";
            this.ButtonEdit10.Size = new System.Drawing.Size(43, 23);
            this.ButtonEdit10.TabIndex = 68;
            this.ButtonEdit10.Text = "Edit";
            this.ButtonEdit10.UseVisualStyleBackColor = true;
            this.ButtonEdit10.Click += new System.EventHandler(this.ButtonEdit10_Click);
            // 
            // ComboBoxFunction10
            // 
            this.ComboBoxFunction10.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxFunction10.FormattingEnabled = true;
            this.ComboBoxFunction10.Items.AddRange(new object[] {
            "-",
            "Volume",
            "Osc Saw",
            "Osc Triangle",
            "Osc Sine",
            "Osc Pulse",
            "Osc Noise",
            "Attack Envelope",
            "Decay Envelope",
            "Add",
            "Multiply",
            "Delay",
            "Comb Filter",
            "Reverb",
            "Ctrl Signal",
            "SV Filter",
            "Distortion",
            "Clone Sample",
            "Chord Gen",
            "Sample and Hold",
            "Imported Sample",
            "1-Pole Filter",
            "---",
            "ADSR Envelope"});
            this.ComboBoxFunction10.Location = new System.Drawing.Point(74, 257);
            this.ComboBoxFunction10.Name = "ComboBoxFunction10";
            this.ComboBoxFunction10.Size = new System.Drawing.Size(104, 21);
            this.ComboBoxFunction10.TabIndex = 67;
            this.ComboBoxFunction10.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxFunction10_SelectionChangeCommitted);
            // 
            // ComboBoxVar10
            // 
            this.ComboBoxVar10.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxVar10.FormattingEnabled = true;
            this.ComboBoxVar10.Items.AddRange(new object[] {
            "-",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxVar10.Location = new System.Drawing.Point(23, 257);
            this.ComboBoxVar10.Name = "ComboBoxVar10";
            this.ComboBoxVar10.Size = new System.Drawing.Size(45, 21);
            this.ComboBoxVar10.TabIndex = 66;
            this.ComboBoxVar10.SelectedIndexChanged += new System.EventHandler(this.ComboBoxVar10_SelectedIndexChanged);
            // 
            // ButtonEdit11
            // 
            this.ButtonEdit11.ForeColor = System.Drawing.Color.Black;
            this.ButtonEdit11.Location = new System.Drawing.Point(183, 284);
            this.ButtonEdit11.Name = "ButtonEdit11";
            this.ButtonEdit11.Size = new System.Drawing.Size(43, 23);
            this.ButtonEdit11.TabIndex = 71;
            this.ButtonEdit11.Text = "Edit";
            this.ButtonEdit11.UseVisualStyleBackColor = true;
            this.ButtonEdit11.Click += new System.EventHandler(this.ButtonEdit11_Click);
            // 
            // ComboBoxFunction11
            // 
            this.ComboBoxFunction11.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxFunction11.FormattingEnabled = true;
            this.ComboBoxFunction11.Items.AddRange(new object[] {
            "-",
            "Volume",
            "Osc Saw",
            "Osc Triangle",
            "Osc Sine",
            "Osc Pulse",
            "Osc Noise",
            "Attack Envelope",
            "Decay Envelope",
            "Add",
            "Multiply",
            "Delay",
            "Comb Filter",
            "Reverb",
            "Ctrl Signal",
            "SV Filter",
            "Distortion",
            "Clone Sample",
            "Chord Gen",
            "Sample and Hold",
            "Imported Sample",
            "1-Pole Filter",
            "---",
            "ADSR Envelope"});
            this.ComboBoxFunction11.Location = new System.Drawing.Point(74, 284);
            this.ComboBoxFunction11.Name = "ComboBoxFunction11";
            this.ComboBoxFunction11.Size = new System.Drawing.Size(104, 21);
            this.ComboBoxFunction11.TabIndex = 70;
            this.ComboBoxFunction11.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxFunction11_SelectionChangeCommitted);
            // 
            // ComboBoxVar11
            // 
            this.ComboBoxVar11.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxVar11.FormattingEnabled = true;
            this.ComboBoxVar11.Items.AddRange(new object[] {
            "-",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxVar11.Location = new System.Drawing.Point(23, 284);
            this.ComboBoxVar11.Name = "ComboBoxVar11";
            this.ComboBoxVar11.Size = new System.Drawing.Size(45, 21);
            this.ComboBoxVar11.TabIndex = 69;
            this.ComboBoxVar11.SelectedIndexChanged += new System.EventHandler(this.ComboBoxVar11_SelectedIndexChanged);
            // 
            // ButtonEdit12
            // 
            this.ButtonEdit12.ForeColor = System.Drawing.Color.Black;
            this.ButtonEdit12.Location = new System.Drawing.Point(183, 311);
            this.ButtonEdit12.Name = "ButtonEdit12";
            this.ButtonEdit12.Size = new System.Drawing.Size(43, 23);
            this.ButtonEdit12.TabIndex = 74;
            this.ButtonEdit12.Text = "Edit";
            this.ButtonEdit12.UseVisualStyleBackColor = true;
            this.ButtonEdit12.Click += new System.EventHandler(this.ButtonEdit12_Click);
            // 
            // ComboBoxFunction12
            // 
            this.ComboBoxFunction12.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxFunction12.FormattingEnabled = true;
            this.ComboBoxFunction12.Items.AddRange(new object[] {
            "-",
            "Volume",
            "Osc Saw",
            "Osc Triangle",
            "Osc Sine",
            "Osc Pulse",
            "Osc Noise",
            "Attack Envelope",
            "Decay Envelope",
            "Add",
            "Multiply",
            "Delay",
            "Comb Filter",
            "Reverb",
            "Ctrl Signal",
            "SV Filter",
            "Distortion",
            "Clone Sample",
            "Chord Gen",
            "Sample and Hold",
            "Imported Sample",
            "1-Pole Filter",
            "---",
            "ADSR Envelope"});
            this.ComboBoxFunction12.Location = new System.Drawing.Point(74, 311);
            this.ComboBoxFunction12.Name = "ComboBoxFunction12";
            this.ComboBoxFunction12.Size = new System.Drawing.Size(104, 21);
            this.ComboBoxFunction12.TabIndex = 73;
            this.ComboBoxFunction12.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxFunction12_SelectionChangeCommitted);
            // 
            // ComboBoxVar12
            // 
            this.ComboBoxVar12.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxVar12.FormattingEnabled = true;
            this.ComboBoxVar12.Items.AddRange(new object[] {
            "-",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxVar12.Location = new System.Drawing.Point(23, 311);
            this.ComboBoxVar12.Name = "ComboBoxVar12";
            this.ComboBoxVar12.Size = new System.Drawing.Size(45, 21);
            this.ComboBoxVar12.TabIndex = 72;
            this.ComboBoxVar12.SelectedIndexChanged += new System.EventHandler(this.ComboBoxVar12_SelectedIndexChanged);
            // 
            // ButtonEdit13
            // 
            this.ButtonEdit13.ForeColor = System.Drawing.Color.Black;
            this.ButtonEdit13.Location = new System.Drawing.Point(183, 338);
            this.ButtonEdit13.Name = "ButtonEdit13";
            this.ButtonEdit13.Size = new System.Drawing.Size(43, 23);
            this.ButtonEdit13.TabIndex = 77;
            this.ButtonEdit13.Text = "Edit";
            this.ButtonEdit13.UseVisualStyleBackColor = true;
            this.ButtonEdit13.Click += new System.EventHandler(this.ButtonEdit13_Click);
            // 
            // ComboBoxFunction13
            // 
            this.ComboBoxFunction13.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxFunction13.FormattingEnabled = true;
            this.ComboBoxFunction13.Items.AddRange(new object[] {
            "-",
            "Volume",
            "Osc Saw",
            "Osc Triangle",
            "Osc Sine",
            "Osc Pulse",
            "Osc Noise",
            "Attack Envelope",
            "Decay Envelope",
            "Add",
            "Multiply",
            "Delay",
            "Comb Filter",
            "Reverb",
            "Ctrl Signal",
            "SV Filter",
            "Distortion",
            "Clone Sample",
            "Chord Gen",
            "Sample and Hold",
            "Imported Sample",
            "1-Pole Filter",
            "---",
            "ADSR Envelope"});
            this.ComboBoxFunction13.Location = new System.Drawing.Point(74, 338);
            this.ComboBoxFunction13.Name = "ComboBoxFunction13";
            this.ComboBoxFunction13.Size = new System.Drawing.Size(104, 21);
            this.ComboBoxFunction13.TabIndex = 76;
            this.ComboBoxFunction13.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxFunction13_SelectionChangeCommitted);
            // 
            // ComboBoxVar13
            // 
            this.ComboBoxVar13.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxVar13.FormattingEnabled = true;
            this.ComboBoxVar13.Items.AddRange(new object[] {
            "-",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxVar13.Location = new System.Drawing.Point(23, 338);
            this.ComboBoxVar13.Name = "ComboBoxVar13";
            this.ComboBoxVar13.Size = new System.Drawing.Size(45, 21);
            this.ComboBoxVar13.TabIndex = 75;
            this.ComboBoxVar13.SelectedIndexChanged += new System.EventHandler(this.ComboBoxVar13_SelectedIndexChanged);
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.ForeColor = System.Drawing.Color.White;
            this.label48.Location = new System.Drawing.Point(27, 592);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(67, 13);
            this.label48.TabIndex = 79;
            this.label48.Text = "Sample view";
            // 
            // ButtonEdit14
            // 
            this.ButtonEdit14.ForeColor = System.Drawing.Color.Black;
            this.ButtonEdit14.Location = new System.Drawing.Point(183, 365);
            this.ButtonEdit14.Name = "ButtonEdit14";
            this.ButtonEdit14.Size = new System.Drawing.Size(43, 23);
            this.ButtonEdit14.TabIndex = 82;
            this.ButtonEdit14.Text = "Edit";
            this.ButtonEdit14.UseVisualStyleBackColor = true;
            this.ButtonEdit14.Click += new System.EventHandler(this.ButtonEdit14_Click);
            // 
            // ComboBoxFunction14
            // 
            this.ComboBoxFunction14.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxFunction14.FormattingEnabled = true;
            this.ComboBoxFunction14.Items.AddRange(new object[] {
            "-",
            "Volume",
            "Osc Saw",
            "Osc Triangle",
            "Osc Sine",
            "Osc Pulse",
            "Osc Noise",
            "Attack Envelope",
            "Decay Envelope",
            "Add",
            "Multiply",
            "Delay",
            "Comb Filter",
            "Reverb",
            "Ctrl Signal",
            "SV Filter",
            "Distortion",
            "Clone Sample",
            "Chord Gen",
            "Sample and Hold",
            "Imported Sample",
            "1-Pole Filter",
            "---",
            "ADSR Envelope",
            "Vocoder"});
            this.ComboBoxFunction14.Location = new System.Drawing.Point(74, 365);
            this.ComboBoxFunction14.Name = "ComboBoxFunction14";
            this.ComboBoxFunction14.Size = new System.Drawing.Size(104, 21);
            this.ComboBoxFunction14.TabIndex = 81;
            this.ComboBoxFunction14.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxFunction14_SelectionChangeCommitted);
            // 
            // ComboBoxVar14
            // 
            this.ComboBoxVar14.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxVar14.FormattingEnabled = true;
            this.ComboBoxVar14.Items.AddRange(new object[] {
            "-",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxVar14.Location = new System.Drawing.Point(23, 365);
            this.ComboBoxVar14.Name = "ComboBoxVar14";
            this.ComboBoxVar14.Size = new System.Drawing.Size(45, 21);
            this.ComboBoxVar14.TabIndex = 80;
            this.ComboBoxVar14.SelectedIndexChanged += new System.EventHandler(this.ComboBoxVar14_SelectedIndexChanged);
            // 
            // ButtonEdit15
            // 
            this.ButtonEdit15.ForeColor = System.Drawing.Color.Black;
            this.ButtonEdit15.Location = new System.Drawing.Point(183, 392);
            this.ButtonEdit15.Name = "ButtonEdit15";
            this.ButtonEdit15.Size = new System.Drawing.Size(43, 23);
            this.ButtonEdit15.TabIndex = 85;
            this.ButtonEdit15.Text = "Edit";
            this.ButtonEdit15.UseVisualStyleBackColor = true;
            this.ButtonEdit15.Click += new System.EventHandler(this.ButtonEdit15_Click);
            // 
            // ComboBoxFunction15
            // 
            this.ComboBoxFunction15.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxFunction15.FormattingEnabled = true;
            this.ComboBoxFunction15.Items.AddRange(new object[] {
            "-",
            "Volume",
            "Osc Saw",
            "Osc Triangle",
            "Osc Sine",
            "Osc Pulse",
            "Osc Noise",
            "Attack Envelope",
            "Decay Envelope",
            "Add",
            "Multiply",
            "Delay",
            "Comb Filter",
            "Reverb",
            "Ctrl Signal",
            "SV Filter",
            "Distortion",
            "Clone Sample",
            "Chord Gen",
            "Sample and Hold",
            "Imported Sample",
            "1-Pole Filter",
            "---",
            "ADSR Envelope"});
            this.ComboBoxFunction15.Location = new System.Drawing.Point(74, 392);
            this.ComboBoxFunction15.Name = "ComboBoxFunction15";
            this.ComboBoxFunction15.Size = new System.Drawing.Size(104, 21);
            this.ComboBoxFunction15.TabIndex = 84;
            this.ComboBoxFunction15.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxFunction15_SelectionChangeCommitted);
            // 
            // ComboBoxVar15
            // 
            this.ComboBoxVar15.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxVar15.FormattingEnabled = true;
            this.ComboBoxVar15.Items.AddRange(new object[] {
            "-",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxVar15.Location = new System.Drawing.Point(23, 392);
            this.ComboBoxVar15.Name = "ComboBoxVar15";
            this.ComboBoxVar15.Size = new System.Drawing.Size(45, 21);
            this.ComboBoxVar15.TabIndex = 83;
            this.ComboBoxVar15.SelectedIndexChanged += new System.EventHandler(this.ComboBoxVar15_SelectedIndexChanged);
            // 
            // ButtonEdit16
            // 
            this.ButtonEdit16.ForeColor = System.Drawing.Color.Black;
            this.ButtonEdit16.Location = new System.Drawing.Point(183, 419);
            this.ButtonEdit16.Name = "ButtonEdit16";
            this.ButtonEdit16.Size = new System.Drawing.Size(43, 23);
            this.ButtonEdit16.TabIndex = 88;
            this.ButtonEdit16.Text = "Edit";
            this.ButtonEdit16.UseVisualStyleBackColor = true;
            this.ButtonEdit16.Click += new System.EventHandler(this.ButtonEdit16_Click);
            // 
            // ComboBoxFunction16
            // 
            this.ComboBoxFunction16.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxFunction16.FormattingEnabled = true;
            this.ComboBoxFunction16.Items.AddRange(new object[] {
            "-",
            "Volume",
            "Osc Saw",
            "Osc Triangle",
            "Osc Sine",
            "Osc Pulse",
            "Osc Noise",
            "Attack Envelope",
            "Decay Envelope",
            "Add",
            "Multiply",
            "Delay",
            "Comb Filter",
            "Reverb",
            "Ctrl Signal",
            "SV Filter",
            "Distortion",
            "Clone Sample",
            "Chord Gen",
            "Sample and Hold",
            "Imported Sample",
            "1-Pole Filter",
            "Loop Generator",
            "ADSR Envelope"});
            this.ComboBoxFunction16.Location = new System.Drawing.Point(74, 419);
            this.ComboBoxFunction16.Name = "ComboBoxFunction16";
            this.ComboBoxFunction16.Size = new System.Drawing.Size(104, 21);
            this.ComboBoxFunction16.TabIndex = 87;
            this.ComboBoxFunction16.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxFunction16_SelectionChangeCommitted);
            // 
            // ComboBoxVar16
            // 
            this.ComboBoxVar16.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxVar16.FormattingEnabled = true;
            this.ComboBoxVar16.Items.AddRange(new object[] {
            "-",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxVar16.Location = new System.Drawing.Point(23, 419);
            this.ComboBoxVar16.Name = "ComboBoxVar16";
            this.ComboBoxVar16.Size = new System.Drawing.Size(45, 21);
            this.ComboBoxVar16.TabIndex = 86;
            this.ComboBoxVar16.SelectedIndexChanged += new System.EventHandler(this.ComboBoxVar16_SelectedIndexChanged);
            // 
            // checkBoxSampleview
            // 
            this.checkBoxSampleview.AutoSize = true;
            this.checkBoxSampleview.Checked = true;
            this.checkBoxSampleview.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSampleview.ForeColor = System.Drawing.Color.White;
            this.checkBoxSampleview.Location = new System.Drawing.Point(96, 592);
            this.checkBoxSampleview.Name = "checkBoxSampleview";
            this.checkBoxSampleview.Size = new System.Drawing.Size(70, 17);
            this.checkBoxSampleview.TabIndex = 89;
            this.checkBoxSampleview.Text = "Line style";
            this.checkBoxSampleview.UseVisualStyleBackColor = true;
            this.checkBoxSampleview.CheckedChanged += new System.EventHandler(this.checkBoxSampleview_CheckedChanged);
            // 
            // GroupBoxNodes
            // 
            this.GroupBoxNodes.BorderColor = System.Drawing.Color.White;
            this.GroupBoxNodes.Controls.Add(this.buttonUp16);
            this.GroupBoxNodes.Controls.Add(this.buttonDown15);
            this.GroupBoxNodes.Controls.Add(this.buttonUp15);
            this.GroupBoxNodes.Controls.Add(this.buttonDown14);
            this.GroupBoxNodes.Controls.Add(this.buttonUp14);
            this.GroupBoxNodes.Controls.Add(this.buttonDown13);
            this.GroupBoxNodes.Controls.Add(this.buttonUp13);
            this.GroupBoxNodes.Controls.Add(this.buttonDown12);
            this.GroupBoxNodes.Controls.Add(this.buttonUp12);
            this.GroupBoxNodes.Controls.Add(this.buttonDown11);
            this.GroupBoxNodes.Controls.Add(this.buttonUp11);
            this.GroupBoxNodes.Controls.Add(this.buttonDown10);
            this.GroupBoxNodes.Controls.Add(this.buttonUp10);
            this.GroupBoxNodes.Controls.Add(this.buttonDown9);
            this.GroupBoxNodes.Controls.Add(this.buttonUp9);
            this.GroupBoxNodes.Controls.Add(this.buttonDown8);
            this.GroupBoxNodes.Controls.Add(this.buttonUp8);
            this.GroupBoxNodes.Controls.Add(this.buttonDown7);
            this.GroupBoxNodes.Controls.Add(this.buttonUp7);
            this.GroupBoxNodes.Controls.Add(this.buttonDown6);
            this.GroupBoxNodes.Controls.Add(this.buttonUp6);
            this.GroupBoxNodes.Controls.Add(this.buttonDown5);
            this.GroupBoxNodes.Controls.Add(this.buttonUp5);
            this.GroupBoxNodes.Controls.Add(this.buttonDown4);
            this.GroupBoxNodes.Controls.Add(this.buttonUp4);
            this.GroupBoxNodes.Controls.Add(this.buttonDown3);
            this.GroupBoxNodes.Controls.Add(this.buttonUp3);
            this.GroupBoxNodes.Controls.Add(this.buttonDown2);
            this.GroupBoxNodes.Controls.Add(this.buttonUp2);
            this.GroupBoxNodes.Controls.Add(this.buttonDown1);
            this.GroupBoxNodes.Controls.Add(this.ComboBoxVar1);
            this.GroupBoxNodes.Controls.Add(this.ComboBoxVar2);
            this.GroupBoxNodes.Controls.Add(this.ButtonEdit16);
            this.GroupBoxNodes.Controls.Add(this.ComboBoxVar3);
            this.GroupBoxNodes.Controls.Add(this.ComboBoxFunction16);
            this.GroupBoxNodes.Controls.Add(this.ComboBoxVar4);
            this.GroupBoxNodes.Controls.Add(this.ComboBoxVar16);
            this.GroupBoxNodes.Controls.Add(this.ComboBoxVar5);
            this.GroupBoxNodes.Controls.Add(this.ButtonEdit15);
            this.GroupBoxNodes.Controls.Add(this.ComboBoxFunction1);
            this.GroupBoxNodes.Controls.Add(this.ComboBoxFunction15);
            this.GroupBoxNodes.Controls.Add(this.ComboBoxFunction2);
            this.GroupBoxNodes.Controls.Add(this.ComboBoxVar15);
            this.GroupBoxNodes.Controls.Add(this.ComboBoxFunction3);
            this.GroupBoxNodes.Controls.Add(this.ButtonEdit14);
            this.GroupBoxNodes.Controls.Add(this.ComboBoxFunction4);
            this.GroupBoxNodes.Controls.Add(this.ComboBoxFunction14);
            this.GroupBoxNodes.Controls.Add(this.ComboBoxFunction5);
            this.GroupBoxNodes.Controls.Add(this.ComboBoxVar14);
            this.GroupBoxNodes.Controls.Add(this.ButtonEdit1);
            this.GroupBoxNodes.Controls.Add(this.ButtonEdit2);
            this.GroupBoxNodes.Controls.Add(this.ButtonEdit13);
            this.GroupBoxNodes.Controls.Add(this.ButtonEdit3);
            this.GroupBoxNodes.Controls.Add(this.ComboBoxFunction13);
            this.GroupBoxNodes.Controls.Add(this.ButtonEdit4);
            this.GroupBoxNodes.Controls.Add(this.ComboBoxVar13);
            this.GroupBoxNodes.Controls.Add(this.ButtonEdit5);
            this.GroupBoxNodes.Controls.Add(this.ButtonEdit12);
            this.GroupBoxNodes.Controls.Add(this.ComboBoxVar6);
            this.GroupBoxNodes.Controls.Add(this.ComboBoxFunction12);
            this.GroupBoxNodes.Controls.Add(this.ComboBoxFunction6);
            this.GroupBoxNodes.Controls.Add(this.ComboBoxVar12);
            this.GroupBoxNodes.Controls.Add(this.ButtonEdit6);
            this.GroupBoxNodes.Controls.Add(this.ButtonEdit11);
            this.GroupBoxNodes.Controls.Add(this.ComboBoxVar7);
            this.GroupBoxNodes.Controls.Add(this.ComboBoxFunction11);
            this.GroupBoxNodes.Controls.Add(this.ComboBoxFunction7);
            this.GroupBoxNodes.Controls.Add(this.ComboBoxVar11);
            this.GroupBoxNodes.Controls.Add(this.ButtonEdit7);
            this.GroupBoxNodes.Controls.Add(this.ButtonEdit10);
            this.GroupBoxNodes.Controls.Add(this.ComboBoxVar8);
            this.GroupBoxNodes.Controls.Add(this.ComboBoxFunction10);
            this.GroupBoxNodes.Controls.Add(this.ComboBoxFunction8);
            this.GroupBoxNodes.Controls.Add(this.ComboBoxVar10);
            this.GroupBoxNodes.Controls.Add(this.ButtonEdit8);
            this.GroupBoxNodes.Controls.Add(this.ButtonEdit9);
            this.GroupBoxNodes.Controls.Add(this.ComboBoxVar9);
            this.GroupBoxNodes.Controls.Add(this.ComboBoxFunction9);
            this.GroupBoxNodes.ForeColor = System.Drawing.Color.White;
            this.GroupBoxNodes.Location = new System.Drawing.Point(28, 118);
            this.GroupBoxNodes.Name = "GroupBoxNodes";
            this.GroupBoxNodes.Size = new System.Drawing.Size(324, 449);
            this.GroupBoxNodes.TabIndex = 90;
            this.GroupBoxNodes.TabStop = false;
            // 
            // buttonUp16
            // 
            this.buttonUp16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonUp16.ForeColor = System.Drawing.Color.Black;
            this.buttonUp16.Location = new System.Drawing.Point(229, 419);
            this.buttonUp16.Name = "buttonUp16";
            this.buttonUp16.Size = new System.Drawing.Size(43, 23);
            this.buttonUp16.TabIndex = 119;
            this.buttonUp16.Text = "Up";
            this.buttonUp16.UseVisualStyleBackColor = true;
            this.buttonUp16.Click += new System.EventHandler(this.buttonUp16_Click);
            // 
            // buttonDown15
            // 
            this.buttonDown15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDown15.ForeColor = System.Drawing.Color.Black;
            this.buttonDown15.Location = new System.Drawing.Point(275, 392);
            this.buttonDown15.Name = "buttonDown15";
            this.buttonDown15.Size = new System.Drawing.Size(43, 23);
            this.buttonDown15.TabIndex = 118;
            this.buttonDown15.Text = "Down";
            this.buttonDown15.UseVisualStyleBackColor = true;
            this.buttonDown15.Click += new System.EventHandler(this.buttonDown15_Click);
            // 
            // buttonUp15
            // 
            this.buttonUp15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonUp15.ForeColor = System.Drawing.Color.Black;
            this.buttonUp15.Location = new System.Drawing.Point(229, 392);
            this.buttonUp15.Name = "buttonUp15";
            this.buttonUp15.Size = new System.Drawing.Size(43, 23);
            this.buttonUp15.TabIndex = 117;
            this.buttonUp15.Text = "Up";
            this.buttonUp15.UseVisualStyleBackColor = true;
            this.buttonUp15.Click += new System.EventHandler(this.buttonUp15_Click);
            // 
            // buttonDown14
            // 
            this.buttonDown14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDown14.ForeColor = System.Drawing.Color.Black;
            this.buttonDown14.Location = new System.Drawing.Point(275, 365);
            this.buttonDown14.Name = "buttonDown14";
            this.buttonDown14.Size = new System.Drawing.Size(43, 23);
            this.buttonDown14.TabIndex = 116;
            this.buttonDown14.Text = "Down";
            this.buttonDown14.UseVisualStyleBackColor = true;
            this.buttonDown14.Click += new System.EventHandler(this.buttonDown14_Click);
            // 
            // buttonUp14
            // 
            this.buttonUp14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonUp14.ForeColor = System.Drawing.Color.Black;
            this.buttonUp14.Location = new System.Drawing.Point(229, 365);
            this.buttonUp14.Name = "buttonUp14";
            this.buttonUp14.Size = new System.Drawing.Size(43, 23);
            this.buttonUp14.TabIndex = 115;
            this.buttonUp14.Text = "Up";
            this.buttonUp14.UseVisualStyleBackColor = true;
            this.buttonUp14.Click += new System.EventHandler(this.buttonUp14_Click);
            // 
            // buttonDown13
            // 
            this.buttonDown13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDown13.ForeColor = System.Drawing.Color.Black;
            this.buttonDown13.Location = new System.Drawing.Point(275, 338);
            this.buttonDown13.Name = "buttonDown13";
            this.buttonDown13.Size = new System.Drawing.Size(43, 23);
            this.buttonDown13.TabIndex = 114;
            this.buttonDown13.Text = "Down";
            this.buttonDown13.UseVisualStyleBackColor = true;
            this.buttonDown13.Click += new System.EventHandler(this.buttonDown13_Click);
            // 
            // buttonUp13
            // 
            this.buttonUp13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonUp13.ForeColor = System.Drawing.Color.Black;
            this.buttonUp13.Location = new System.Drawing.Point(229, 338);
            this.buttonUp13.Name = "buttonUp13";
            this.buttonUp13.Size = new System.Drawing.Size(43, 23);
            this.buttonUp13.TabIndex = 113;
            this.buttonUp13.Text = "Up";
            this.buttonUp13.UseVisualStyleBackColor = true;
            this.buttonUp13.Click += new System.EventHandler(this.buttonUp13_Click);
            // 
            // buttonDown12
            // 
            this.buttonDown12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDown12.ForeColor = System.Drawing.Color.Black;
            this.buttonDown12.Location = new System.Drawing.Point(275, 311);
            this.buttonDown12.Name = "buttonDown12";
            this.buttonDown12.Size = new System.Drawing.Size(43, 23);
            this.buttonDown12.TabIndex = 112;
            this.buttonDown12.Text = "Down";
            this.buttonDown12.UseVisualStyleBackColor = true;
            this.buttonDown12.Click += new System.EventHandler(this.buttonDown12_Click);
            // 
            // buttonUp12
            // 
            this.buttonUp12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonUp12.ForeColor = System.Drawing.Color.Black;
            this.buttonUp12.Location = new System.Drawing.Point(229, 311);
            this.buttonUp12.Name = "buttonUp12";
            this.buttonUp12.Size = new System.Drawing.Size(43, 23);
            this.buttonUp12.TabIndex = 111;
            this.buttonUp12.Text = "Up";
            this.buttonUp12.UseVisualStyleBackColor = true;
            this.buttonUp12.Click += new System.EventHandler(this.buttonUp12_Click);
            // 
            // buttonDown11
            // 
            this.buttonDown11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDown11.ForeColor = System.Drawing.Color.Black;
            this.buttonDown11.Location = new System.Drawing.Point(275, 284);
            this.buttonDown11.Name = "buttonDown11";
            this.buttonDown11.Size = new System.Drawing.Size(43, 23);
            this.buttonDown11.TabIndex = 110;
            this.buttonDown11.Text = "Down";
            this.buttonDown11.UseVisualStyleBackColor = true;
            this.buttonDown11.Click += new System.EventHandler(this.buttonDown11_Click);
            // 
            // buttonUp11
            // 
            this.buttonUp11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonUp11.ForeColor = System.Drawing.Color.Black;
            this.buttonUp11.Location = new System.Drawing.Point(229, 284);
            this.buttonUp11.Name = "buttonUp11";
            this.buttonUp11.Size = new System.Drawing.Size(43, 23);
            this.buttonUp11.TabIndex = 109;
            this.buttonUp11.Text = "Up";
            this.buttonUp11.UseVisualStyleBackColor = true;
            this.buttonUp11.Click += new System.EventHandler(this.buttonUp11_Click);
            // 
            // buttonDown10
            // 
            this.buttonDown10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDown10.ForeColor = System.Drawing.Color.Black;
            this.buttonDown10.Location = new System.Drawing.Point(275, 257);
            this.buttonDown10.Name = "buttonDown10";
            this.buttonDown10.Size = new System.Drawing.Size(43, 23);
            this.buttonDown10.TabIndex = 108;
            this.buttonDown10.Text = "Down";
            this.buttonDown10.UseVisualStyleBackColor = true;
            this.buttonDown10.Click += new System.EventHandler(this.buttonDown10_Click);
            // 
            // buttonUp10
            // 
            this.buttonUp10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonUp10.ForeColor = System.Drawing.Color.Black;
            this.buttonUp10.Location = new System.Drawing.Point(229, 257);
            this.buttonUp10.Name = "buttonUp10";
            this.buttonUp10.Size = new System.Drawing.Size(43, 23);
            this.buttonUp10.TabIndex = 107;
            this.buttonUp10.Text = "Up";
            this.buttonUp10.UseVisualStyleBackColor = true;
            this.buttonUp10.Click += new System.EventHandler(this.buttonUp10_Click);
            // 
            // buttonDown9
            // 
            this.buttonDown9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDown9.ForeColor = System.Drawing.Color.Black;
            this.buttonDown9.Location = new System.Drawing.Point(275, 230);
            this.buttonDown9.Name = "buttonDown9";
            this.buttonDown9.Size = new System.Drawing.Size(43, 23);
            this.buttonDown9.TabIndex = 106;
            this.buttonDown9.Text = "Down";
            this.buttonDown9.UseVisualStyleBackColor = true;
            this.buttonDown9.Click += new System.EventHandler(this.buttonDown9_Click);
            // 
            // buttonUp9
            // 
            this.buttonUp9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonUp9.ForeColor = System.Drawing.Color.Black;
            this.buttonUp9.Location = new System.Drawing.Point(229, 230);
            this.buttonUp9.Name = "buttonUp9";
            this.buttonUp9.Size = new System.Drawing.Size(43, 23);
            this.buttonUp9.TabIndex = 105;
            this.buttonUp9.Text = "Up";
            this.buttonUp9.UseVisualStyleBackColor = true;
            this.buttonUp9.Click += new System.EventHandler(this.buttonUp9_Click);
            // 
            // buttonDown8
            // 
            this.buttonDown8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDown8.ForeColor = System.Drawing.Color.Black;
            this.buttonDown8.Location = new System.Drawing.Point(275, 203);
            this.buttonDown8.Name = "buttonDown8";
            this.buttonDown8.Size = new System.Drawing.Size(43, 23);
            this.buttonDown8.TabIndex = 104;
            this.buttonDown8.Text = "Down";
            this.buttonDown8.UseVisualStyleBackColor = true;
            this.buttonDown8.Click += new System.EventHandler(this.buttonDown8_Click);
            // 
            // buttonUp8
            // 
            this.buttonUp8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonUp8.ForeColor = System.Drawing.Color.Black;
            this.buttonUp8.Location = new System.Drawing.Point(229, 203);
            this.buttonUp8.Name = "buttonUp8";
            this.buttonUp8.Size = new System.Drawing.Size(43, 23);
            this.buttonUp8.TabIndex = 103;
            this.buttonUp8.Text = "Up";
            this.buttonUp8.UseVisualStyleBackColor = true;
            this.buttonUp8.Click += new System.EventHandler(this.buttonUp8_Click);
            // 
            // buttonDown7
            // 
            this.buttonDown7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDown7.ForeColor = System.Drawing.Color.Black;
            this.buttonDown7.Location = new System.Drawing.Point(275, 176);
            this.buttonDown7.Name = "buttonDown7";
            this.buttonDown7.Size = new System.Drawing.Size(43, 23);
            this.buttonDown7.TabIndex = 102;
            this.buttonDown7.Text = "Down";
            this.buttonDown7.UseVisualStyleBackColor = true;
            this.buttonDown7.Click += new System.EventHandler(this.buttonDown7_Click);
            // 
            // buttonUp7
            // 
            this.buttonUp7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonUp7.ForeColor = System.Drawing.Color.Black;
            this.buttonUp7.Location = new System.Drawing.Point(229, 176);
            this.buttonUp7.Name = "buttonUp7";
            this.buttonUp7.Size = new System.Drawing.Size(43, 23);
            this.buttonUp7.TabIndex = 101;
            this.buttonUp7.Text = "Up";
            this.buttonUp7.UseVisualStyleBackColor = true;
            this.buttonUp7.Click += new System.EventHandler(this.buttonUp7_Click);
            // 
            // buttonDown6
            // 
            this.buttonDown6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDown6.ForeColor = System.Drawing.Color.Black;
            this.buttonDown6.Location = new System.Drawing.Point(275, 149);
            this.buttonDown6.Name = "buttonDown6";
            this.buttonDown6.Size = new System.Drawing.Size(43, 23);
            this.buttonDown6.TabIndex = 100;
            this.buttonDown6.Text = "Down";
            this.buttonDown6.UseVisualStyleBackColor = true;
            this.buttonDown6.Click += new System.EventHandler(this.buttonDown6_Click);
            // 
            // buttonUp6
            // 
            this.buttonUp6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonUp6.ForeColor = System.Drawing.Color.Black;
            this.buttonUp6.Location = new System.Drawing.Point(229, 149);
            this.buttonUp6.Name = "buttonUp6";
            this.buttonUp6.Size = new System.Drawing.Size(43, 23);
            this.buttonUp6.TabIndex = 99;
            this.buttonUp6.Text = "Up";
            this.buttonUp6.UseVisualStyleBackColor = true;
            this.buttonUp6.Click += new System.EventHandler(this.buttonUp6_Click);
            // 
            // buttonDown5
            // 
            this.buttonDown5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDown5.ForeColor = System.Drawing.Color.Black;
            this.buttonDown5.Location = new System.Drawing.Point(275, 122);
            this.buttonDown5.Name = "buttonDown5";
            this.buttonDown5.Size = new System.Drawing.Size(43, 23);
            this.buttonDown5.TabIndex = 98;
            this.buttonDown5.Text = "Down";
            this.buttonDown5.UseVisualStyleBackColor = true;
            this.buttonDown5.Click += new System.EventHandler(this.buttonDown5_Click);
            // 
            // buttonUp5
            // 
            this.buttonUp5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonUp5.ForeColor = System.Drawing.Color.Black;
            this.buttonUp5.Location = new System.Drawing.Point(229, 122);
            this.buttonUp5.Name = "buttonUp5";
            this.buttonUp5.Size = new System.Drawing.Size(43, 23);
            this.buttonUp5.TabIndex = 97;
            this.buttonUp5.Text = "Up";
            this.buttonUp5.UseVisualStyleBackColor = true;
            this.buttonUp5.Click += new System.EventHandler(this.buttonUp5_Click);
            // 
            // buttonDown4
            // 
            this.buttonDown4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDown4.ForeColor = System.Drawing.Color.Black;
            this.buttonDown4.Location = new System.Drawing.Point(275, 95);
            this.buttonDown4.Name = "buttonDown4";
            this.buttonDown4.Size = new System.Drawing.Size(43, 23);
            this.buttonDown4.TabIndex = 96;
            this.buttonDown4.Text = "Down";
            this.buttonDown4.UseVisualStyleBackColor = true;
            this.buttonDown4.Click += new System.EventHandler(this.buttonDown4_Click);
            // 
            // buttonUp4
            // 
            this.buttonUp4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonUp4.ForeColor = System.Drawing.Color.Black;
            this.buttonUp4.Location = new System.Drawing.Point(229, 95);
            this.buttonUp4.Name = "buttonUp4";
            this.buttonUp4.Size = new System.Drawing.Size(43, 23);
            this.buttonUp4.TabIndex = 95;
            this.buttonUp4.Text = "Up";
            this.buttonUp4.UseVisualStyleBackColor = true;
            this.buttonUp4.Click += new System.EventHandler(this.buttonUp4_Click);
            // 
            // buttonDown3
            // 
            this.buttonDown3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDown3.ForeColor = System.Drawing.Color.Black;
            this.buttonDown3.Location = new System.Drawing.Point(275, 68);
            this.buttonDown3.Name = "buttonDown3";
            this.buttonDown3.Size = new System.Drawing.Size(43, 23);
            this.buttonDown3.TabIndex = 94;
            this.buttonDown3.Text = "Down";
            this.buttonDown3.UseVisualStyleBackColor = true;
            this.buttonDown3.Click += new System.EventHandler(this.buttonDown3_Click);
            // 
            // buttonUp3
            // 
            this.buttonUp3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonUp3.ForeColor = System.Drawing.Color.Black;
            this.buttonUp3.Location = new System.Drawing.Point(229, 68);
            this.buttonUp3.Name = "buttonUp3";
            this.buttonUp3.Size = new System.Drawing.Size(43, 23);
            this.buttonUp3.TabIndex = 93;
            this.buttonUp3.Text = "Up";
            this.buttonUp3.UseVisualStyleBackColor = true;
            this.buttonUp3.Click += new System.EventHandler(this.buttonUp3_Click);
            // 
            // buttonDown2
            // 
            this.buttonDown2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDown2.ForeColor = System.Drawing.Color.Black;
            this.buttonDown2.Location = new System.Drawing.Point(275, 41);
            this.buttonDown2.Name = "buttonDown2";
            this.buttonDown2.Size = new System.Drawing.Size(43, 23);
            this.buttonDown2.TabIndex = 92;
            this.buttonDown2.Text = "Down";
            this.buttonDown2.UseVisualStyleBackColor = true;
            this.buttonDown2.Click += new System.EventHandler(this.buttonDown2_Click);
            // 
            // buttonUp2
            // 
            this.buttonUp2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonUp2.ForeColor = System.Drawing.Color.Black;
            this.buttonUp2.Location = new System.Drawing.Point(229, 41);
            this.buttonUp2.Name = "buttonUp2";
            this.buttonUp2.Size = new System.Drawing.Size(43, 23);
            this.buttonUp2.TabIndex = 91;
            this.buttonUp2.Text = "Up";
            this.buttonUp2.UseVisualStyleBackColor = true;
            this.buttonUp2.Click += new System.EventHandler(this.buttonUp2_Click);
            // 
            // buttonDown1
            // 
            this.buttonDown1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDown1.ForeColor = System.Drawing.Color.Black;
            this.buttonDown1.Location = new System.Drawing.Point(275, 14);
            this.buttonDown1.Name = "buttonDown1";
            this.buttonDown1.Size = new System.Drawing.Size(43, 23);
            this.buttonDown1.TabIndex = 90;
            this.buttonDown1.Text = "Down";
            this.buttonDown1.UseVisualStyleBackColor = true;
            this.buttonDown1.Click += new System.EventHandler(this.buttonDown1_Click);
            // 
            // label78
            // 
            this.label78.AutoSize = true;
            this.label78.ForeColor = System.Drawing.Color.White;
            this.label78.Location = new System.Drawing.Point(49, 112);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(45, 13);
            this.label78.TabIndex = 122;
            this.label78.Text = "Variable";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.ForeColor = System.Drawing.Color.White;
            this.label41.Location = new System.Drawing.Point(101, 112);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(48, 13);
            this.label41.TabIndex = 119;
            this.label41.Text = "Function";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Title = "Amigaklang GUI";
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.ForeColor = System.Drawing.Color.White;
            this.label49.Location = new System.Drawing.Point(63, 47);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(45, 13);
            this.label49.TabIndex = 96;
            this.label49.Text = "Octave:";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.ForeColor = System.Drawing.Color.White;
            this.radioButton1.Location = new System.Drawing.Point(108, 45);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(31, 17);
            this.radioButton1.TabIndex = 97;
            this.radioButton1.Text = "1";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.ForeColor = System.Drawing.Color.White;
            this.radioButton2.Location = new System.Drawing.Point(143, 45);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(31, 17);
            this.radioButton2.TabIndex = 98;
            this.radioButton2.Text = "2";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // groupBoxPlay
            // 
            this.groupBoxPlay.BorderColor = System.Drawing.Color.White;
            this.groupBoxPlay.Controls.Add(this.label68);
            this.groupBoxPlay.Controls.Add(this.buttonGenerateAll);
            this.groupBoxPlay.Controls.Add(this.ComboBoxNotes);
            this.groupBoxPlay.Controls.Add(this.radioButton4);
            this.groupBoxPlay.Controls.Add(this.buttonPlaySample);
            this.groupBoxPlay.Controls.Add(this.label49);
            this.groupBoxPlay.Controls.Add(this.radioButton2);
            this.groupBoxPlay.Controls.Add(this.radioButton1);
            this.groupBoxPlay.Controls.Add(this.buttonStopSample);
            this.groupBoxPlay.ForeColor = System.Drawing.Color.White;
            this.groupBoxPlay.Location = new System.Drawing.Point(416, 118);
            this.groupBoxPlay.Name = "groupBoxPlay";
            this.groupBoxPlay.Size = new System.Drawing.Size(212, 75);
            this.groupBoxPlay.TabIndex = 59;
            this.groupBoxPlay.TabStop = false;
            this.groupBoxPlay.Text = "Render (F1)";
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.ForeColor = System.Drawing.Color.White;
            this.label68.Location = new System.Drawing.Point(111, 0);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(48, 13);
            this.label68.TabIndex = 122;
            this.label68.Text = "Play (F2)";
            // 
            // buttonGenerateAll
            // 
            this.buttonGenerateAll.ForeColor = System.Drawing.Color.Black;
            this.buttonGenerateAll.Location = new System.Drawing.Point(6, 17);
            this.buttonGenerateAll.Name = "buttonGenerateAll";
            this.buttonGenerateAll.Size = new System.Drawing.Size(102, 23);
            this.buttonGenerateAll.TabIndex = 99;
            this.buttonGenerateAll.Text = "Render Samples";
            this.buttonGenerateAll.UseVisualStyleBackColor = true;
            this.buttonGenerateAll.Click += new System.EventHandler(this.buttonGenerateAll_Click);
            // 
            // ComboBoxNotes
            // 
            this.ComboBoxNotes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxNotes.FormattingEnabled = true;
            this.ComboBoxNotes.Items.AddRange(new object[] {
            "C",
            "C#",
            "D",
            "D#",
            "E",
            "F",
            "F#",
            "G",
            "G#",
            "A",
            "A#",
            "B"});
            this.ComboBoxNotes.Location = new System.Drawing.Point(7, 42);
            this.ComboBoxNotes.Name = "ComboBoxNotes";
            this.ComboBoxNotes.Size = new System.Drawing.Size(47, 21);
            this.ComboBoxNotes.TabIndex = 107;
            this.ComboBoxNotes.SelectedIndexChanged += new System.EventHandler(this.ComboBoxNotes_SelectedIndexChanged);
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Checked = true;
            this.radioButton4.ForeColor = System.Drawing.Color.White;
            this.radioButton4.Location = new System.Drawing.Point(177, 45);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(31, 17);
            this.radioButton4.TabIndex = 100;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "3";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // GroupBoxChord
            // 
            this.GroupBoxChord.BorderColor = System.Drawing.Color.White;
            this.GroupBoxChord.Controls.Add(this.labelChordgen);
            this.GroupBoxChord.Controls.Add(this.labelChordName);
            this.GroupBoxChord.Controls.Add(this.hScrollBarChordShiftValue);
            this.GroupBoxChord.Controls.Add(this.TextBoxChordShiftValue);
            this.GroupBoxChord.Controls.Add(this.label58);
            this.GroupBoxChord.Controls.Add(this.ComboBoxChordNote3);
            this.GroupBoxChord.Controls.Add(this.label57);
            this.GroupBoxChord.Controls.Add(this.ComboBoxChordNote2);
            this.GroupBoxChord.Controls.Add(this.label59);
            this.GroupBoxChord.Controls.Add(this.ComboBoxChordShift);
            this.GroupBoxChord.Controls.Add(this.label56);
            this.GroupBoxChord.Controls.Add(this.ComboBoxChordNote1);
            this.GroupBoxChord.Controls.Add(this.label55);
            this.GroupBoxChord.Controls.Add(this.ComboBoxChordSamplenr);
            this.GroupBoxChord.ForeColor = System.Drawing.Color.White;
            this.GroupBoxChord.Location = new System.Drawing.Point(416, 198);
            this.GroupBoxChord.Name = "GroupBoxChord";
            this.GroupBoxChord.Size = new System.Drawing.Size(212, 260);
            this.GroupBoxChord.TabIndex = 93;
            this.GroupBoxChord.TabStop = false;
            this.GroupBoxChord.Text = "Chord Generator";
            // 
            // labelChordgen
            // 
            this.labelChordgen.AutoSize = true;
            this.labelChordgen.Location = new System.Drawing.Point(6, 239);
            this.labelChordgen.Name = "labelChordgen";
            this.labelChordgen.Size = new System.Drawing.Size(192, 13);
            this.labelChordgen.TabIndex = 131;
            this.labelChordgen.Text = "Length should be 1/2 of source sample";
            // 
            // labelChordName
            // 
            this.labelChordName.AutoSize = true;
            this.labelChordName.ForeColor = System.Drawing.Color.White;
            this.labelChordName.Location = new System.Drawing.Point(93, 36);
            this.labelChordName.Name = "labelChordName";
            this.labelChordName.Size = new System.Drawing.Size(0, 13);
            this.labelChordName.TabIndex = 105;
            // 
            // hScrollBarChordShiftValue
            // 
            this.hScrollBarChordShiftValue.LargeChange = 1;
            this.hScrollBarChordShiftValue.Location = new System.Drawing.Point(80, 196);
            this.hScrollBarChordShiftValue.Maximum = 127;
            this.hScrollBarChordShiftValue.Name = "hScrollBarChordShiftValue";
            this.hScrollBarChordShiftValue.Size = new System.Drawing.Size(120, 18);
            this.hScrollBarChordShiftValue.TabIndex = 60;
            this.hScrollBarChordShiftValue.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarChordShiftValue_Scroll);
            // 
            // TextBoxChordShiftValue
            // 
            this.TextBoxChordShiftValue.Location = new System.Drawing.Point(80, 214);
            this.TextBoxChordShiftValue.Name = "TextBoxChordShiftValue";
            this.TextBoxChordShiftValue.Size = new System.Drawing.Size(120, 20);
            this.TextBoxChordShiftValue.TabIndex = 59;
            this.TextBoxChordShiftValue.Text = "0";
            this.TextBoxChordShiftValue.Click += new System.EventHandler(this.TextBoxChordShiftValue_Click);
            this.TextBoxChordShiftValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxChordShiftValue_KeyDown);
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Location = new System.Drawing.Point(6, 153);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(39, 13);
            this.label58.TabIndex = 50;
            this.label58.Text = "Note 3";
            // 
            // ComboBoxChordNote3
            // 
            this.ComboBoxChordNote3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxChordNote3.FormattingEnabled = true;
            this.ComboBoxChordNote3.Items.AddRange(new object[] {
            "-",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12"});
            this.ComboBoxChordNote3.Location = new System.Drawing.Point(6, 168);
            this.ComboBoxChordNote3.Name = "ComboBoxChordNote3";
            this.ComboBoxChordNote3.Size = new System.Drawing.Size(62, 21);
            this.ComboBoxChordNote3.TabIndex = 49;
            this.ComboBoxChordNote3.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxChordNote3_SelectionChangeCommitted);
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Location = new System.Drawing.Point(6, 108);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(39, 13);
            this.label57.TabIndex = 48;
            this.label57.Text = "Note 2";
            // 
            // ComboBoxChordNote2
            // 
            this.ComboBoxChordNote2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxChordNote2.FormattingEnabled = true;
            this.ComboBoxChordNote2.Items.AddRange(new object[] {
            "-",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12"});
            this.ComboBoxChordNote2.Location = new System.Drawing.Point(6, 123);
            this.ComboBoxChordNote2.Name = "ComboBoxChordNote2";
            this.ComboBoxChordNote2.Size = new System.Drawing.Size(62, 21);
            this.ComboBoxChordNote2.TabIndex = 47;
            this.ComboBoxChordNote2.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxChordNote2_SelectionChangeCommitted);
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Location = new System.Drawing.Point(6, 198);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(28, 13);
            this.label59.TabIndex = 46;
            this.label59.Text = "Shift";
            // 
            // ComboBoxChordShift
            // 
            this.ComboBoxChordShift.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxChordShift.FormattingEnabled = true;
            this.ComboBoxChordShift.Items.AddRange(new object[] {
            "Value",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxChordShift.Location = new System.Drawing.Point(6, 213);
            this.ComboBoxChordShift.Name = "ComboBoxChordShift";
            this.ComboBoxChordShift.Size = new System.Drawing.Size(62, 21);
            this.ComboBoxChordShift.TabIndex = 45;
            this.ComboBoxChordShift.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxChordShift_SelectionChangeCommitted);
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Location = new System.Drawing.Point(6, 63);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(39, 13);
            this.label56.TabIndex = 40;
            this.label56.Text = "Note 1";
            // 
            // ComboBoxChordNote1
            // 
            this.ComboBoxChordNote1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxChordNote1.FormattingEnabled = true;
            this.ComboBoxChordNote1.Items.AddRange(new object[] {
            "-",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12"});
            this.ComboBoxChordNote1.Location = new System.Drawing.Point(6, 78);
            this.ComboBoxChordNote1.Name = "ComboBoxChordNote1";
            this.ComboBoxChordNote1.Size = new System.Drawing.Size(62, 21);
            this.ComboBoxChordNote1.TabIndex = 39;
            this.ComboBoxChordNote1.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxChordNote1_SelectionChangeCommitted);
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Location = new System.Drawing.Point(6, 18);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(57, 13);
            this.label55.TabIndex = 38;
            this.label55.Text = "Sample nr.";
            // 
            // ComboBoxChordSamplenr
            // 
            this.ComboBoxChordSamplenr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxChordSamplenr.FormattingEnabled = true;
            this.ComboBoxChordSamplenr.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30"});
            this.ComboBoxChordSamplenr.Location = new System.Drawing.Point(6, 33);
            this.ComboBoxChordSamplenr.Name = "ComboBoxChordSamplenr";
            this.ComboBoxChordSamplenr.Size = new System.Drawing.Size(62, 21);
            this.ComboBoxChordSamplenr.TabIndex = 37;
            this.ComboBoxChordSamplenr.DropDown += new System.EventHandler(this.ComboBoxChordSamplenr_DropDown);
            this.ComboBoxChordSamplenr.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxChordSamplenr_SelectionChangeCommitted);
            // 
            // GroupBoxLoop
            // 
            this.GroupBoxLoop.BorderColor = System.Drawing.Color.White;
            this.GroupBoxLoop.Controls.Add(this.TextBoxLoopLengthValue);
            this.GroupBoxLoop.Controls.Add(this.TextBoxLoopOffsetValue);
            this.GroupBoxLoop.Controls.Add(this.label61);
            this.GroupBoxLoop.Controls.Add(this.label60);
            this.GroupBoxLoop.Controls.Add(this.hScrollBarLoopOffsetValue);
            this.GroupBoxLoop.ForeColor = System.Drawing.Color.White;
            this.GroupBoxLoop.Location = new System.Drawing.Point(416, 198);
            this.GroupBoxLoop.Name = "GroupBoxLoop";
            this.GroupBoxLoop.Size = new System.Drawing.Size(212, 260);
            this.GroupBoxLoop.TabIndex = 94;
            this.GroupBoxLoop.TabStop = false;
            this.GroupBoxLoop.Text = "Loop Generator";
            // 
            // TextBoxLoopLengthValue
            // 
            this.TextBoxLoopLengthValue.Location = new System.Drawing.Point(6, 110);
            this.TextBoxLoopLengthValue.Name = "TextBoxLoopLengthValue";
            this.TextBoxLoopLengthValue.ReadOnly = true;
            this.TextBoxLoopLengthValue.Size = new System.Drawing.Size(200, 20);
            this.TextBoxLoopLengthValue.TabIndex = 103;
            this.TextBoxLoopLengthValue.Text = "0";
            // 
            // TextBoxLoopOffsetValue
            // 
            this.TextBoxLoopOffsetValue.Location = new System.Drawing.Point(6, 53);
            this.TextBoxLoopOffsetValue.Name = "TextBoxLoopOffsetValue";
            this.TextBoxLoopOffsetValue.Size = new System.Drawing.Size(200, 20);
            this.TextBoxLoopOffsetValue.TabIndex = 97;
            this.TextBoxLoopOffsetValue.Text = "0";
            this.TextBoxLoopOffsetValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxLoopOffsetValue_KeyDown);
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.ForeColor = System.Drawing.Color.White;
            this.label61.Location = new System.Drawing.Point(6, 95);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(78, 13);
            this.label61.TabIndex = 100;
            this.label61.Text = "Repeat Length";
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.ForeColor = System.Drawing.Color.White;
            this.label60.Location = new System.Drawing.Point(6, 18);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(73, 13);
            this.label60.TabIndex = 99;
            this.label60.Text = "Repeat Offset";
            // 
            // hScrollBarLoopOffsetValue
            // 
            this.hScrollBarLoopOffsetValue.LargeChange = 2;
            this.hScrollBarLoopOffsetValue.Location = new System.Drawing.Point(6, 34);
            this.hScrollBarLoopOffsetValue.Maximum = 32767;
            this.hScrollBarLoopOffsetValue.Name = "hScrollBarLoopOffsetValue";
            this.hScrollBarLoopOffsetValue.Size = new System.Drawing.Size(200, 18);
            this.hScrollBarLoopOffsetValue.SmallChange = 2;
            this.hScrollBarLoopOffsetValue.TabIndex = 97;
            this.hScrollBarLoopOffsetValue.Value = 2;
            this.hScrollBarLoopOffsetValue.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarLoopOffsetValue_Scroll);
            // 
            // GroupBoxClone
            // 
            this.GroupBoxClone.BorderColor = System.Drawing.Color.White;
            this.GroupBoxClone.Controls.Add(this.hScrollBarCloneTransposeValue);
            this.GroupBoxClone.Controls.Add(this.TextBoxCloneTransposeValue);
            this.GroupBoxClone.Controls.Add(this.label79);
            this.GroupBoxClone.Controls.Add(this.ComboBoxCloneTranspose);
            this.GroupBoxClone.Controls.Add(this.label69);
            this.GroupBoxClone.Controls.Add(this.TextBoxCloneOffsetValue);
            this.GroupBoxClone.Controls.Add(this.hScrollBarCloneOffsetValue);
            this.GroupBoxClone.Controls.Add(this.labelCloneName);
            this.GroupBoxClone.Controls.Add(this.label54);
            this.GroupBoxClone.Controls.Add(this.ComboBoxCloneReverse);
            this.GroupBoxClone.Controls.Add(this.label50);
            this.GroupBoxClone.Controls.Add(this.ComboBoxCloneSamplenr);
            this.GroupBoxClone.ForeColor = System.Drawing.Color.White;
            this.GroupBoxClone.Location = new System.Drawing.Point(416, 197);
            this.GroupBoxClone.Name = "GroupBoxClone";
            this.GroupBoxClone.Size = new System.Drawing.Size(212, 260);
            this.GroupBoxClone.TabIndex = 96;
            this.GroupBoxClone.TabStop = false;
            this.GroupBoxClone.Text = "Clone Sample";
            // 
            // hScrollBarCloneTransposeValue
            // 
            this.hScrollBarCloneTransposeValue.LargeChange = 64;
            this.hScrollBarCloneTransposeValue.Location = new System.Drawing.Point(80, 111);
            this.hScrollBarCloneTransposeValue.Maximum = 32830;
            this.hScrollBarCloneTransposeValue.Minimum = -16384;
            this.hScrollBarCloneTransposeValue.Name = "hScrollBarCloneTransposeValue";
            this.hScrollBarCloneTransposeValue.Size = new System.Drawing.Size(120, 18);
            this.hScrollBarCloneTransposeValue.SmallChange = 64;
            this.hScrollBarCloneTransposeValue.TabIndex = 112;
            this.hScrollBarCloneTransposeValue.Visible = false;
            this.hScrollBarCloneTransposeValue.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarCloneTransposeValue_Scroll);
            // 
            // TextBoxCloneTransposeValue
            // 
            this.TextBoxCloneTransposeValue.Location = new System.Drawing.Point(80, 129);
            this.TextBoxCloneTransposeValue.Name = "TextBoxCloneTransposeValue";
            this.TextBoxCloneTransposeValue.Size = new System.Drawing.Size(120, 20);
            this.TextBoxCloneTransposeValue.TabIndex = 111;
            this.TextBoxCloneTransposeValue.Text = "0";
            this.TextBoxCloneTransposeValue.Visible = false;
            this.TextBoxCloneTransposeValue.Click += new System.EventHandler(this.TextBoxCloneTransposeValue_Click);
            this.TextBoxCloneTransposeValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxCloneTransposeValue_KeyDown);
            // 
            // label79
            // 
            this.label79.AutoSize = true;
            this.label79.Location = new System.Drawing.Point(4, 113);
            this.label79.Name = "label79";
            this.label79.Size = new System.Drawing.Size(57, 13);
            this.label79.TabIndex = 110;
            this.label79.Text = "Transpose";
            this.label79.Visible = false;
            // 
            // ComboBoxCloneTranspose
            // 
            this.ComboBoxCloneTranspose.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxCloneTranspose.FormattingEnabled = true;
            this.ComboBoxCloneTranspose.Items.AddRange(new object[] {
            "Value",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxCloneTranspose.Location = new System.Drawing.Point(6, 128);
            this.ComboBoxCloneTranspose.Name = "ComboBoxCloneTranspose";
            this.ComboBoxCloneTranspose.Size = new System.Drawing.Size(62, 21);
            this.ComboBoxCloneTranspose.TabIndex = 109;
            this.ComboBoxCloneTranspose.Visible = false;
            this.ComboBoxCloneTranspose.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxCloneTranspose_SelectionChangeCommitted);
            // 
            // label69
            // 
            this.label69.AutoSize = true;
            this.label69.Location = new System.Drawing.Point(6, 179);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(35, 13);
            this.label69.TabIndex = 108;
            this.label69.Text = "Offset";
            // 
            // TextBoxCloneOffsetValue
            // 
            this.TextBoxCloneOffsetValue.Location = new System.Drawing.Point(6, 217);
            this.TextBoxCloneOffsetValue.Name = "TextBoxCloneOffsetValue";
            this.TextBoxCloneOffsetValue.ReadOnly = true;
            this.TextBoxCloneOffsetValue.Size = new System.Drawing.Size(200, 20);
            this.TextBoxCloneOffsetValue.TabIndex = 107;
            this.TextBoxCloneOffsetValue.Text = "0";
            // 
            // hScrollBarCloneOffsetValue
            // 
            this.hScrollBarCloneOffsetValue.LargeChange = 2;
            this.hScrollBarCloneOffsetValue.Location = new System.Drawing.Point(6, 196);
            this.hScrollBarCloneOffsetValue.Maximum = 32767;
            this.hScrollBarCloneOffsetValue.Name = "hScrollBarCloneOffsetValue";
            this.hScrollBarCloneOffsetValue.Size = new System.Drawing.Size(200, 18);
            this.hScrollBarCloneOffsetValue.SmallChange = 2;
            this.hScrollBarCloneOffsetValue.TabIndex = 106;
            this.hScrollBarCloneOffsetValue.Value = 2;
            this.hScrollBarCloneOffsetValue.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarCloneOffsetValue_Scroll);
            // 
            // labelCloneName
            // 
            this.labelCloneName.AutoSize = true;
            this.labelCloneName.ForeColor = System.Drawing.Color.White;
            this.labelCloneName.Location = new System.Drawing.Point(93, 36);
            this.labelCloneName.Name = "labelCloneName";
            this.labelCloneName.Size = new System.Drawing.Size(0, 13);
            this.labelCloneName.TabIndex = 105;
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Location = new System.Drawing.Point(6, 63);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(47, 13);
            this.label54.TabIndex = 38;
            this.label54.Text = "Reverse";
            // 
            // ComboBoxCloneReverse
            // 
            this.ComboBoxCloneReverse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxCloneReverse.FormattingEnabled = true;
            this.ComboBoxCloneReverse.Items.AddRange(new object[] {
            "No",
            "Yes"});
            this.ComboBoxCloneReverse.Location = new System.Drawing.Point(6, 78);
            this.ComboBoxCloneReverse.Name = "ComboBoxCloneReverse";
            this.ComboBoxCloneReverse.Size = new System.Drawing.Size(62, 21);
            this.ComboBoxCloneReverse.TabIndex = 37;
            this.ComboBoxCloneReverse.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxCloneReverse_SelectionChangeCommitted);
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(6, 18);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(57, 13);
            this.label50.TabIndex = 36;
            this.label50.Text = "Sample nr.";
            // 
            // ComboBoxCloneSamplenr
            // 
            this.ComboBoxCloneSamplenr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxCloneSamplenr.FormattingEnabled = true;
            this.ComboBoxCloneSamplenr.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30"});
            this.ComboBoxCloneSamplenr.Location = new System.Drawing.Point(6, 33);
            this.ComboBoxCloneSamplenr.Name = "ComboBoxCloneSamplenr";
            this.ComboBoxCloneSamplenr.Size = new System.Drawing.Size(62, 21);
            this.ComboBoxCloneSamplenr.TabIndex = 31;
            this.ComboBoxCloneSamplenr.DropDown += new System.EventHandler(this.ComboBoxCloneSamplenr_DropDown);
            this.ComboBoxCloneSamplenr.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxCloneSamplenr_SelectionChangeCommitted);
            // 
            // hScrollBarZoomScroll
            // 
            this.hScrollBarZoomScroll.LargeChange = 1;
            this.hScrollBarZoomScroll.Location = new System.Drawing.Point(168, 589);
            this.hScrollBarZoomScroll.Maximum = 10000;
            this.hScrollBarZoomScroll.Name = "hScrollBarZoomScroll";
            this.hScrollBarZoomScroll.Size = new System.Drawing.Size(293, 21);
            this.hScrollBarZoomScroll.TabIndex = 97;
            this.hScrollBarZoomScroll.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarZoomScroll_Scroll);
            // 
            // hScrollBarZoomZoom
            // 
            this.hScrollBarZoomZoom.LargeChange = 1;
            this.hScrollBarZoomZoom.Location = new System.Drawing.Point(465, 589);
            this.hScrollBarZoomZoom.Maximum = 16383;
            this.hScrollBarZoomZoom.Minimum = 600;
            this.hScrollBarZoomZoom.Name = "hScrollBarZoomZoom";
            this.hScrollBarZoomZoom.Size = new System.Drawing.Size(123, 21);
            this.hScrollBarZoomZoom.TabIndex = 100;
            this.hScrollBarZoomZoom.Value = 600;
            this.hScrollBarZoomZoom.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarZoomZoom_Scroll);
            // 
            // buttonZoomReset
            // 
            this.buttonZoomReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonZoomReset.Location = new System.Drawing.Point(591, 588);
            this.buttonZoomReset.Name = "buttonZoomReset";
            this.buttonZoomReset.Size = new System.Drawing.Size(39, 23);
            this.buttonZoomReset.TabIndex = 101;
            this.buttonZoomReset.Text = "Reset";
            this.buttonZoomReset.UseVisualStyleBackColor = true;
            this.buttonZoomReset.Click += new System.EventHandler(this.buttonZoomReset_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(275, -1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 104;
            this.label1.Text = "Thrash";
            // 
            // textBoxSamplelengthDec
            // 
            this.textBoxSamplelengthDec.Location = new System.Drawing.Point(106, 17);
            this.textBoxSamplelengthDec.Name = "textBoxSamplelengthDec";
            this.textBoxSamplelengthDec.Size = new System.Drawing.Size(101, 20);
            this.textBoxSamplelengthDec.TabIndex = 105;
            this.textBoxSamplelengthDec.Text = "0";
            this.textBoxSamplelengthDec.Click += new System.EventHandler(this.textBoxSamplelengthDec_Click);
            this.textBoxSamplelengthDec.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxSamplelengthDec_KeyDown);
            // 
            // groupBoxSampleLength
            // 
            this.groupBoxSampleLength.BorderColor = System.Drawing.Color.White;
            this.groupBoxSampleLength.Controls.Add(this.textBoxSamplelengthDec);
            this.groupBoxSampleLength.Controls.Add(this.hScrollBarSamplelength);
            this.groupBoxSampleLength.Controls.Add(this.textBox3);
            this.groupBoxSampleLength.ForeColor = System.Drawing.Color.White;
            this.groupBoxSampleLength.Location = new System.Drawing.Point(416, 41);
            this.groupBoxSampleLength.Name = "groupBoxSampleLength";
            this.groupBoxSampleLength.Size = new System.Drawing.Size(212, 68);
            this.groupBoxSampleLength.TabIndex = 106;
            this.groupBoxSampleLength.TabStop = false;
            this.groupBoxSampleLength.Text = "Samplelength (Hex/Dec)";
            // 
            // TextBoxTranslateInst
            // 
            this.TextBoxTranslateInst.AcceptsReturn = true;
            this.TextBoxTranslateInst.AcceptsTab = true;
            this.TextBoxTranslateInst.Location = new System.Drawing.Point(383, 124);
            this.TextBoxTranslateInst.Multiline = true;
            this.TextBoxTranslateInst.Name = "TextBoxTranslateInst";
            this.TextBoxTranslateInst.ReadOnly = true;
            this.TextBoxTranslateInst.Size = new System.Drawing.Size(24, 69);
            this.TextBoxTranslateInst.TabIndex = 108;
            this.TextBoxTranslateInst.Visible = false;
            // 
            // TextBoxTranslateIset
            // 
            this.TextBoxTranslateIset.AcceptsReturn = true;
            this.TextBoxTranslateIset.AcceptsTab = true;
            this.TextBoxTranslateIset.Location = new System.Drawing.Point(383, 279);
            this.TextBoxTranslateIset.Multiline = true;
            this.TextBoxTranslateIset.Name = "TextBoxTranslateIset";
            this.TextBoxTranslateIset.ReadOnly = true;
            this.TextBoxTranslateIset.Size = new System.Drawing.Size(25, 75);
            this.TextBoxTranslateIset.TabIndex = 110;
            this.TextBoxTranslateIset.Visible = false;
            // 
            // groupBoxInstruments
            // 
            this.groupBoxInstruments.BorderColor = System.Drawing.Color.White;
            this.groupBoxInstruments.Controls.Add(this.labelInstrumentNr);
            this.groupBoxInstruments.Controls.Add(this.label4);
            this.groupBoxInstruments.Controls.Add(this.ComboBoxSampleAuswahl);
            this.groupBoxInstruments.Controls.Add(this.pictureBox2);
            this.groupBoxInstruments.Controls.Add(this.label1);
            this.groupBoxInstruments.ForeColor = System.Drawing.Color.White;
            this.groupBoxInstruments.Location = new System.Drawing.Point(27, 41);
            this.groupBoxInstruments.Name = "groupBoxInstruments";
            this.groupBoxInstruments.Size = new System.Drawing.Size(325, 68);
            this.groupBoxInstruments.TabIndex = 111;
            this.groupBoxInstruments.TabStop = false;
            this.groupBoxInstruments.Text = "Instrument";
            // 
            // labelInstrumentNr
            // 
            this.labelInstrumentNr.AutoSize = true;
            this.labelInstrumentNr.ForeColor = System.Drawing.Color.White;
            this.labelInstrumentNr.Location = new System.Drawing.Point(248, 31);
            this.labelInstrumentNr.Name = "labelInstrumentNr";
            this.labelInstrumentNr.Size = new System.Drawing.Size(19, 13);
            this.labelInstrumentNr.TabIndex = 112;
            this.labelInstrumentNr.Text = "31";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(229, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 13);
            this.label4.TabIndex = 112;
            this.label4.Text = "Nr.";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::AmigaKlangGUI.Properties.Resources.amiga_trash;
            this.pictureBox2.Location = new System.Drawing.Point(275, 13);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(42, 47);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 102;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // GroupBoxSh
            // 
            this.GroupBoxSh.BorderColor = System.Drawing.Color.White;
            this.GroupBoxSh.Controls.Add(this.hScrollBarShStepValue);
            this.GroupBoxSh.Controls.Add(this.TextBoxShStepValue);
            this.GroupBoxSh.Controls.Add(this.label5);
            this.GroupBoxSh.Controls.Add(this.label15);
            this.GroupBoxSh.Controls.Add(this.ComboBoxShStep);
            this.GroupBoxSh.Controls.Add(this.ComboBoxShVal);
            this.GroupBoxSh.ForeColor = System.Drawing.Color.White;
            this.GroupBoxSh.Location = new System.Drawing.Point(416, 198);
            this.GroupBoxSh.Name = "GroupBoxSh";
            this.GroupBoxSh.Size = new System.Drawing.Size(212, 260);
            this.GroupBoxSh.TabIndex = 112;
            this.GroupBoxSh.TabStop = false;
            this.GroupBoxSh.Text = "Sample and Hold";
            this.GroupBoxSh.Visible = false;
            // 
            // hScrollBarShStepValue
            // 
            this.hScrollBarShStepValue.LargeChange = 1;
            this.hScrollBarShStepValue.Location = new System.Drawing.Point(80, 196);
            this.hScrollBarShStepValue.Maximum = 127;
            this.hScrollBarShStepValue.Name = "hScrollBarShStepValue";
            this.hScrollBarShStepValue.Size = new System.Drawing.Size(120, 18);
            this.hScrollBarShStepValue.TabIndex = 61;
            this.hScrollBarShStepValue.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarShStepValue_Scroll);
            // 
            // TextBoxShStepValue
            // 
            this.TextBoxShStepValue.Location = new System.Drawing.Point(80, 214);
            this.TextBoxShStepValue.Name = "TextBoxShStepValue";
            this.TextBoxShStepValue.Size = new System.Drawing.Size(120, 20);
            this.TextBoxShStepValue.TabIndex = 60;
            this.TextBoxShStepValue.Text = "0";
            this.TextBoxShStepValue.Click += new System.EventHandler(this.TextBoxShStepValue_Click);
            this.TextBoxShStepValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxShStepValue_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 198);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 29;
            this.label5.Text = "Step";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 18);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(34, 13);
            this.label15.TabIndex = 28;
            this.label15.Text = "Value";
            // 
            // ComboBoxShStep
            // 
            this.ComboBoxShStep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxShStep.FormattingEnabled = true;
            this.ComboBoxShStep.Items.AddRange(new object[] {
            "Value",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxShStep.Location = new System.Drawing.Point(6, 213);
            this.ComboBoxShStep.Name = "ComboBoxShStep";
            this.ComboBoxShStep.Size = new System.Drawing.Size(62, 21);
            this.ComboBoxShStep.TabIndex = 11;
            this.ComboBoxShStep.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxShStep_SelectionChangeCommitted);
            // 
            // ComboBoxShVal
            // 
            this.ComboBoxShVal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxShVal.FormattingEnabled = true;
            this.ComboBoxShVal.Items.AddRange(new object[] {
            "-",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxShVal.Location = new System.Drawing.Point(6, 33);
            this.ComboBoxShVal.Name = "ComboBoxShVal";
            this.ComboBoxShVal.Size = new System.Drawing.Size(62, 21);
            this.ComboBoxShVal.TabIndex = 10;
            this.ComboBoxShVal.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxShVal_SelectionChangeCommitted);
            // 
            // TextBoxImportSampleLength
            // 
            this.TextBoxImportSampleLength.Location = new System.Drawing.Point(6, 40);
            this.TextBoxImportSampleLength.Name = "TextBoxImportSampleLength";
            this.TextBoxImportSampleLength.ReadOnly = true;
            this.TextBoxImportSampleLength.Size = new System.Drawing.Size(100, 20);
            this.TextBoxImportSampleLength.TabIndex = 114;
            this.TextBoxImportSampleLength.Text = "0";
            // 
            // buttonImportSample
            // 
            this.buttonImportSample.ForeColor = System.Drawing.Color.Black;
            this.buttonImportSample.Location = new System.Drawing.Point(6, 73);
            this.buttonImportSample.Name = "buttonImportSample";
            this.buttonImportSample.Size = new System.Drawing.Size(50, 23);
            this.buttonImportSample.TabIndex = 116;
            this.buttonImportSample.Text = "Load";
            this.buttonImportSample.UseVisualStyleBackColor = true;
            this.buttonImportSample.Click += new System.EventHandler(this.buttonImportSample_Click);
            // 
            // GroupBoxImport
            // 
            this.GroupBoxImport.BorderColor = System.Drawing.Color.White;
            this.GroupBoxImport.Controls.Add(this.labelImportedName);
            this.GroupBoxImport.Controls.Add(this.label18);
            this.GroupBoxImport.Controls.Add(this.ComboBoxImportNumber);
            this.GroupBoxImport.ForeColor = System.Drawing.Color.White;
            this.GroupBoxImport.Location = new System.Drawing.Point(416, 198);
            this.GroupBoxImport.Name = "GroupBoxImport";
            this.GroupBoxImport.Size = new System.Drawing.Size(212, 260);
            this.GroupBoxImport.TabIndex = 117;
            this.GroupBoxImport.TabStop = false;
            this.GroupBoxImport.Text = "Import Sample";
            this.GroupBoxImport.Visible = false;
            // 
            // labelImportedName
            // 
            this.labelImportedName.AutoSize = true;
            this.labelImportedName.Location = new System.Drawing.Point(92, 37);
            this.labelImportedName.Name = "labelImportedName";
            this.labelImportedName.Size = new System.Drawing.Size(0, 13);
            this.labelImportedName.TabIndex = 120;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(5, 18);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(57, 13);
            this.label18.TabIndex = 119;
            this.label18.Text = "Sample nr.";
            // 
            // ComboBoxImportNumber
            // 
            this.ComboBoxImportNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxImportNumber.FormattingEnabled = true;
            this.ComboBoxImportNumber.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.ComboBoxImportNumber.Location = new System.Drawing.Point(7, 33);
            this.ComboBoxImportNumber.Name = "ComboBoxImportNumber";
            this.ComboBoxImportNumber.Size = new System.Drawing.Size(62, 21);
            this.ComboBoxImportNumber.TabIndex = 118;
            this.ComboBoxImportNumber.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxImportNumber_SelectionChangeCommitted);
            // 
            // groupBoxImportSample
            // 
            this.groupBoxImportSample.BorderColor = System.Drawing.Color.White;
            this.groupBoxImportSample.Controls.Add(this.buttonClearImportSample);
            this.groupBoxImportSample.Controls.Add(this.label63);
            this.groupBoxImportSample.Controls.Add(this.TextBoxImportSampleLengthTotal);
            this.groupBoxImportSample.Controls.Add(this.TextBoxImportSampleLengthDec);
            this.groupBoxImportSample.Controls.Add(this.comboBoxSelectImport);
            this.groupBoxImportSample.Controls.Add(this.TextBoxImportSampleLength);
            this.groupBoxImportSample.Controls.Add(this.buttonImportSample);
            this.groupBoxImportSample.ForeColor = System.Drawing.Color.White;
            this.groupBoxImportSample.Location = new System.Drawing.Point(416, 463);
            this.groupBoxImportSample.Name = "groupBoxImportSample";
            this.groupBoxImportSample.Size = new System.Drawing.Size(212, 104);
            this.groupBoxImportSample.TabIndex = 118;
            this.groupBoxImportSample.TabStop = false;
            this.groupBoxImportSample.Text = "Import Sample  (32767 total max)  ";
            // 
            // buttonClearImportSample
            // 
            this.buttonClearImportSample.ForeColor = System.Drawing.Color.Black;
            this.buttonClearImportSample.Location = new System.Drawing.Point(56, 73);
            this.buttonClearImportSample.Name = "buttonClearImportSample";
            this.buttonClearImportSample.Size = new System.Drawing.Size(50, 23);
            this.buttonClearImportSample.TabIndex = 121;
            this.buttonClearImportSample.Text = "Clear";
            this.buttonClearImportSample.UseVisualStyleBackColor = true;
            this.buttonClearImportSample.Click += new System.EventHandler(this.buttonClearImportSample_Click);
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.Location = new System.Drawing.Point(105, 62);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(31, 13);
            this.label63.TabIndex = 120;
            this.label63.Text = "Total";
            // 
            // TextBoxImportSampleLengthTotal
            // 
            this.TextBoxImportSampleLengthTotal.Location = new System.Drawing.Point(107, 75);
            this.TextBoxImportSampleLengthTotal.Name = "TextBoxImportSampleLengthTotal";
            this.TextBoxImportSampleLengthTotal.ReadOnly = true;
            this.TextBoxImportSampleLengthTotal.Size = new System.Drawing.Size(99, 20);
            this.TextBoxImportSampleLengthTotal.TabIndex = 118;
            this.TextBoxImportSampleLengthTotal.Text = "0";
            // 
            // TextBoxImportSampleLengthDec
            // 
            this.TextBoxImportSampleLengthDec.Location = new System.Drawing.Point(107, 40);
            this.TextBoxImportSampleLengthDec.Name = "TextBoxImportSampleLengthDec";
            this.TextBoxImportSampleLengthDec.ReadOnly = true;
            this.TextBoxImportSampleLengthDec.Size = new System.Drawing.Size(99, 20);
            this.TextBoxImportSampleLengthDec.TabIndex = 117;
            this.TextBoxImportSampleLengthDec.Text = "0";
            // 
            // comboBoxSelectImport
            // 
            this.comboBoxSelectImport.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxSelectImport.FormattingEnabled = true;
            this.comboBoxSelectImport.Items.AddRange(new object[] {
            "Sample_1",
            "Sample_2",
            "Sample_3",
            "Sample_4",
            "Sample_5",
            "Sample_6",
            "Sample_7",
            "Sample_8"});
            this.comboBoxSelectImport.Location = new System.Drawing.Point(6, 17);
            this.comboBoxSelectImport.MaxDropDownItems = 31;
            this.comboBoxSelectImport.Name = "comboBoxSelectImport";
            this.comboBoxSelectImport.Size = new System.Drawing.Size(200, 22);
            this.comboBoxSelectImport.TabIndex = 1;
            this.comboBoxSelectImport.SelectedIndexChanged += new System.EventHandler(this.comboBoxSelectImport_SelectedIndexChanged);
            this.comboBoxSelectImport.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBoxSelectImport_KeyDown);
            this.comboBoxSelectImport.Leave += new System.EventHandler(this.comboBoxSelectImport_Leave);
            // 
            // TextBoxTranslateIswitch
            // 
            this.TextBoxTranslateIswitch.AcceptsReturn = true;
            this.TextBoxTranslateIswitch.AcceptsTab = true;
            this.TextBoxTranslateIswitch.Location = new System.Drawing.Point(383, 360);
            this.TextBoxTranslateIswitch.Multiline = true;
            this.TextBoxTranslateIswitch.Name = "TextBoxTranslateIswitch";
            this.TextBoxTranslateIswitch.ReadOnly = true;
            this.TextBoxTranslateIswitch.Size = new System.Drawing.Size(25, 75);
            this.TextBoxTranslateIswitch.TabIndex = 119;
            this.TextBoxTranslateIswitch.Visible = false;
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.ForeColor = System.Drawing.Color.White;
            this.label65.Location = new System.Drawing.Point(420, 570);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(27, 13);
            this.label65.TabIndex = 121;
            this.label65.Text = "total";
            // 
            // labelTotalSize
            // 
            this.labelTotalSize.AutoSize = true;
            this.labelTotalSize.ForeColor = System.Drawing.Color.White;
            this.labelTotalSize.Location = new System.Drawing.Point(448, 570);
            this.labelTotalSize.Name = "labelTotalSize";
            this.labelTotalSize.Size = new System.Drawing.Size(13, 13);
            this.labelTotalSize.TabIndex = 122;
            this.labelTotalSize.Text = "0";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.ForeColor = System.Drawing.Color.White;
            this.label47.Location = new System.Drawing.Point(53, 571);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(64, 13);
            this.label47.TabIndex = 125;
            this.label47.Text = "V1 = Output";
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(383, 499);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(24, 22);
            this.button3.TabIndex = 126;
            this.button3.Text = "Decode";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // buttonGenerateSingleSample
            // 
            this.buttonGenerateSingleSample.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonGenerateSingleSample.Location = new System.Drawing.Point(383, 527);
            this.buttonGenerateSingleSample.Name = "buttonGenerateSingleSample";
            this.buttonGenerateSingleSample.Size = new System.Drawing.Size(24, 22);
            this.buttonGenerateSingleSample.TabIndex = 127;
            this.buttonGenerateSingleSample.Text = "generate";
            this.buttonGenerateSingleSample.UseVisualStyleBackColor = true;
            this.buttonGenerateSingleSample.Visible = false;
            // 
            // TextBoxTranslateDan
            // 
            this.TextBoxTranslateDan.AcceptsReturn = true;
            this.TextBoxTranslateDan.AcceptsTab = true;
            this.TextBoxTranslateDan.Location = new System.Drawing.Point(358, 124);
            this.TextBoxTranslateDan.Multiline = true;
            this.TextBoxTranslateDan.Name = "TextBoxTranslateDan";
            this.TextBoxTranslateDan.ReadOnly = true;
            this.TextBoxTranslateDan.Size = new System.Drawing.Size(24, 69);
            this.TextBoxTranslateDan.TabIndex = 129;
            this.TextBoxTranslateDan.Visible = false;
            // 
            // GroupBoxOnepole
            // 
            this.GroupBoxOnepole.BorderColor = System.Drawing.Color.White;
            this.GroupBoxOnepole.Controls.Add(this.hScrollBarOnepoleCutoffValue);
            this.GroupBoxOnepole.Controls.Add(this.ComboBoxOnepoleCutoff);
            this.GroupBoxOnepole.Controls.Add(this.TextBoxOnepoleCutoffValue);
            this.GroupBoxOnepole.Controls.Add(this.ComboBoxOnepoleValue);
            this.GroupBoxOnepole.Controls.Add(this.label72);
            this.GroupBoxOnepole.Controls.Add(this.ComboBoxOnepoleMode);
            this.GroupBoxOnepole.Controls.Add(this.label73);
            this.GroupBoxOnepole.Controls.Add(this.label74);
            this.GroupBoxOnepole.ForeColor = System.Drawing.Color.White;
            this.GroupBoxOnepole.Location = new System.Drawing.Point(416, 198);
            this.GroupBoxOnepole.Name = "GroupBoxOnepole";
            this.GroupBoxOnepole.Size = new System.Drawing.Size(212, 260);
            this.GroupBoxOnepole.TabIndex = 59;
            this.GroupBoxOnepole.TabStop = false;
            this.GroupBoxOnepole.Text = "1-Pole Filter";
            this.GroupBoxOnepole.Visible = false;
            // 
            // hScrollBarOnepoleCutoffValue
            // 
            this.hScrollBarOnepoleCutoffValue.LargeChange = 1;
            this.hScrollBarOnepoleCutoffValue.Location = new System.Drawing.Point(80, 61);
            this.hScrollBarOnepoleCutoffValue.Maximum = 127;
            this.hScrollBarOnepoleCutoffValue.Name = "hScrollBarOnepoleCutoffValue";
            this.hScrollBarOnepoleCutoffValue.Size = new System.Drawing.Size(120, 18);
            this.hScrollBarOnepoleCutoffValue.TabIndex = 57;
            this.hScrollBarOnepoleCutoffValue.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBarOnepoleCutoffValue_Scroll);
            // 
            // ComboBoxOnepoleCutoff
            // 
            this.ComboBoxOnepoleCutoff.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxOnepoleCutoff.FormattingEnabled = true;
            this.ComboBoxOnepoleCutoff.Items.AddRange(new object[] {
            "Value",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxOnepoleCutoff.Location = new System.Drawing.Point(6, 78);
            this.ComboBoxOnepoleCutoff.Name = "ComboBoxOnepoleCutoff";
            this.ComboBoxOnepoleCutoff.Size = new System.Drawing.Size(62, 21);
            this.ComboBoxOnepoleCutoff.TabIndex = 55;
            this.ComboBoxOnepoleCutoff.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxOnepoleCutoff_SelectionChangeCommitted);
            // 
            // TextBoxOnepoleCutoffValue
            // 
            this.TextBoxOnepoleCutoffValue.Location = new System.Drawing.Point(80, 79);
            this.TextBoxOnepoleCutoffValue.Name = "TextBoxOnepoleCutoffValue";
            this.TextBoxOnepoleCutoffValue.Size = new System.Drawing.Size(120, 20);
            this.TextBoxOnepoleCutoffValue.TabIndex = 56;
            this.TextBoxOnepoleCutoffValue.Text = "0";
            this.TextBoxOnepoleCutoffValue.Click += new System.EventHandler(this.TextBoxOnepoleCutoffValue_Click);
            this.TextBoxOnepoleCutoffValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxOnepoleCutoffValue_KeyDown);
            // 
            // ComboBoxOnepoleValue
            // 
            this.ComboBoxOnepoleValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxOnepoleValue.FormattingEnabled = true;
            this.ComboBoxOnepoleValue.Items.AddRange(new object[] {
            "-",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxOnepoleValue.Location = new System.Drawing.Point(6, 33);
            this.ComboBoxOnepoleValue.Name = "ComboBoxOnepoleValue";
            this.ComboBoxOnepoleValue.Size = new System.Drawing.Size(62, 21);
            this.ComboBoxOnepoleValue.TabIndex = 44;
            this.ComboBoxOnepoleValue.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxOnepoleValue_SelectionChangeCommitted);
            // 
            // label72
            // 
            this.label72.AutoSize = true;
            this.label72.Location = new System.Drawing.Point(6, 153);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(34, 13);
            this.label72.TabIndex = 37;
            this.label72.Text = "Mode";
            // 
            // ComboBoxOnepoleMode
            // 
            this.ComboBoxOnepoleMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxOnepoleMode.FormattingEnabled = true;
            this.ComboBoxOnepoleMode.Items.AddRange(new object[] {
            "Lowpass",
            "Highpass"});
            this.ComboBoxOnepoleMode.Location = new System.Drawing.Point(6, 168);
            this.ComboBoxOnepoleMode.Name = "ComboBoxOnepoleMode";
            this.ComboBoxOnepoleMode.Size = new System.Drawing.Size(62, 21);
            this.ComboBoxOnepoleMode.TabIndex = 32;
            this.ComboBoxOnepoleMode.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxOnepoleMode_SelectionChangeCommitted);
            // 
            // label73
            // 
            this.label73.AutoSize = true;
            this.label73.Location = new System.Drawing.Point(6, 18);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(34, 13);
            this.label73.TabIndex = 36;
            this.label73.Text = "Value";
            // 
            // label74
            // 
            this.label74.AutoSize = true;
            this.label74.Location = new System.Drawing.Point(6, 63);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(35, 13);
            this.label74.TabIndex = 36;
            this.label74.Text = "Cutoff";
            // 
            // radioButtonColum1
            // 
            this.radioButtonColum1.AutoSize = true;
            this.radioButtonColum1.Location = new System.Drawing.Point(35, 135);
            this.radioButtonColum1.Name = "radioButtonColum1";
            this.radioButtonColum1.Size = new System.Drawing.Size(14, 13);
            this.radioButtonColum1.TabIndex = 130;
            this.radioButtonColum1.TabStop = true;
            this.radioButtonColum1.UseVisualStyleBackColor = true;
            this.radioButtonColum1.CheckedChanged += new System.EventHandler(this.radioButtonColum1_CheckedChanged);
            // 
            // radioButtonColum2
            // 
            this.radioButtonColum2.AutoSize = true;
            this.radioButtonColum2.Location = new System.Drawing.Point(35, 162);
            this.radioButtonColum2.Name = "radioButtonColum2";
            this.radioButtonColum2.Size = new System.Drawing.Size(14, 13);
            this.radioButtonColum2.TabIndex = 131;
            this.radioButtonColum2.TabStop = true;
            this.radioButtonColum2.UseVisualStyleBackColor = true;
            this.radioButtonColum2.CheckedChanged += new System.EventHandler(this.radioButtonColum2_CheckedChanged);
            // 
            // radioButtonColum16
            // 
            this.radioButtonColum16.AutoSize = true;
            this.radioButtonColum16.Location = new System.Drawing.Point(35, 540);
            this.radioButtonColum16.Name = "radioButtonColum16";
            this.radioButtonColum16.Size = new System.Drawing.Size(14, 13);
            this.radioButtonColum16.TabIndex = 132;
            this.radioButtonColum16.TabStop = true;
            this.radioButtonColum16.UseVisualStyleBackColor = true;
            this.radioButtonColum16.CheckedChanged += new System.EventHandler(this.radioButtonColum16_CheckedChanged);
            // 
            // radioButtonOutput
            // 
            this.radioButtonOutput.AutoSize = true;
            this.radioButtonOutput.Location = new System.Drawing.Point(35, 569);
            this.radioButtonOutput.Name = "radioButtonOutput";
            this.radioButtonOutput.Size = new System.Drawing.Size(14, 13);
            this.radioButtonOutput.TabIndex = 133;
            this.radioButtonOutput.TabStop = true;
            this.radioButtonOutput.UseVisualStyleBackColor = true;
            this.radioButtonOutput.CheckedChanged += new System.EventHandler(this.radioButtonOutput_CheckedChanged);
            // 
            // radioButtonColum15
            // 
            this.radioButtonColum15.AutoSize = true;
            this.radioButtonColum15.Location = new System.Drawing.Point(35, 513);
            this.radioButtonColum15.Name = "radioButtonColum15";
            this.radioButtonColum15.Size = new System.Drawing.Size(14, 13);
            this.radioButtonColum15.TabIndex = 134;
            this.radioButtonColum15.TabStop = true;
            this.radioButtonColum15.UseVisualStyleBackColor = true;
            this.radioButtonColum15.CheckedChanged += new System.EventHandler(this.radioButtonColum15_CheckedChanged);
            // 
            // radioButtonColum3
            // 
            this.radioButtonColum3.AutoSize = true;
            this.radioButtonColum3.Location = new System.Drawing.Point(35, 189);
            this.radioButtonColum3.Name = "radioButtonColum3";
            this.radioButtonColum3.Size = new System.Drawing.Size(14, 13);
            this.radioButtonColum3.TabIndex = 135;
            this.radioButtonColum3.TabStop = true;
            this.radioButtonColum3.UseVisualStyleBackColor = true;
            this.radioButtonColum3.CheckedChanged += new System.EventHandler(this.radioButtonColum3_CheckedChanged);
            // 
            // radioButtonColum4
            // 
            this.radioButtonColum4.AutoSize = true;
            this.radioButtonColum4.Location = new System.Drawing.Point(35, 216);
            this.radioButtonColum4.Name = "radioButtonColum4";
            this.radioButtonColum4.Size = new System.Drawing.Size(14, 13);
            this.radioButtonColum4.TabIndex = 136;
            this.radioButtonColum4.TabStop = true;
            this.radioButtonColum4.UseVisualStyleBackColor = true;
            this.radioButtonColum4.CheckedChanged += new System.EventHandler(this.radioButtonColum4_CheckedChanged);
            // 
            // radioButtonColum5
            // 
            this.radioButtonColum5.AutoSize = true;
            this.radioButtonColum5.Location = new System.Drawing.Point(35, 243);
            this.radioButtonColum5.Name = "radioButtonColum5";
            this.radioButtonColum5.Size = new System.Drawing.Size(14, 13);
            this.radioButtonColum5.TabIndex = 137;
            this.radioButtonColum5.TabStop = true;
            this.radioButtonColum5.UseVisualStyleBackColor = true;
            this.radioButtonColum5.CheckedChanged += new System.EventHandler(this.radioButtonColum5_CheckedChanged);
            // 
            // radioButtonColum6
            // 
            this.radioButtonColum6.AutoSize = true;
            this.radioButtonColum6.Location = new System.Drawing.Point(35, 270);
            this.radioButtonColum6.Name = "radioButtonColum6";
            this.radioButtonColum6.Size = new System.Drawing.Size(14, 13);
            this.radioButtonColum6.TabIndex = 138;
            this.radioButtonColum6.TabStop = true;
            this.radioButtonColum6.UseVisualStyleBackColor = true;
            this.radioButtonColum6.CheckedChanged += new System.EventHandler(this.radioButtonColum6_CheckedChanged);
            // 
            // radioButtonColum7
            // 
            this.radioButtonColum7.AutoSize = true;
            this.radioButtonColum7.Location = new System.Drawing.Point(35, 297);
            this.radioButtonColum7.Name = "radioButtonColum7";
            this.radioButtonColum7.Size = new System.Drawing.Size(14, 13);
            this.radioButtonColum7.TabIndex = 139;
            this.radioButtonColum7.TabStop = true;
            this.radioButtonColum7.UseVisualStyleBackColor = true;
            this.radioButtonColum7.CheckedChanged += new System.EventHandler(this.radioButtonColum7_CheckedChanged);
            // 
            // radioButtonColum8
            // 
            this.radioButtonColum8.AutoSize = true;
            this.radioButtonColum8.Location = new System.Drawing.Point(35, 325);
            this.radioButtonColum8.Name = "radioButtonColum8";
            this.radioButtonColum8.Size = new System.Drawing.Size(14, 13);
            this.radioButtonColum8.TabIndex = 140;
            this.radioButtonColum8.TabStop = true;
            this.radioButtonColum8.UseVisualStyleBackColor = true;
            this.radioButtonColum8.CheckedChanged += new System.EventHandler(this.radioButtonColum8_CheckedChanged);
            // 
            // radioButtonColum9
            // 
            this.radioButtonColum9.AutoSize = true;
            this.radioButtonColum9.Location = new System.Drawing.Point(35, 351);
            this.radioButtonColum9.Name = "radioButtonColum9";
            this.radioButtonColum9.Size = new System.Drawing.Size(14, 13);
            this.radioButtonColum9.TabIndex = 141;
            this.radioButtonColum9.TabStop = true;
            this.radioButtonColum9.UseVisualStyleBackColor = true;
            this.radioButtonColum9.CheckedChanged += new System.EventHandler(this.radioButtonColum9_CheckedChanged);
            // 
            // radioButtonColum10
            // 
            this.radioButtonColum10.AutoSize = true;
            this.radioButtonColum10.Location = new System.Drawing.Point(35, 378);
            this.radioButtonColum10.Name = "radioButtonColum10";
            this.radioButtonColum10.Size = new System.Drawing.Size(14, 13);
            this.radioButtonColum10.TabIndex = 142;
            this.radioButtonColum10.TabStop = true;
            this.radioButtonColum10.UseVisualStyleBackColor = true;
            this.radioButtonColum10.CheckedChanged += new System.EventHandler(this.radioButtonColum10_CheckedChanged);
            // 
            // radioButtonColum11
            // 
            this.radioButtonColum11.AutoSize = true;
            this.radioButtonColum11.Location = new System.Drawing.Point(35, 405);
            this.radioButtonColum11.Name = "radioButtonColum11";
            this.radioButtonColum11.Size = new System.Drawing.Size(14, 13);
            this.radioButtonColum11.TabIndex = 143;
            this.radioButtonColum11.TabStop = true;
            this.radioButtonColum11.UseVisualStyleBackColor = true;
            this.radioButtonColum11.CheckedChanged += new System.EventHandler(this.radioButtonColum11_CheckedChanged);
            // 
            // radioButtonColum12
            // 
            this.radioButtonColum12.AutoSize = true;
            this.radioButtonColum12.Location = new System.Drawing.Point(35, 432);
            this.radioButtonColum12.Name = "radioButtonColum12";
            this.radioButtonColum12.Size = new System.Drawing.Size(14, 13);
            this.radioButtonColum12.TabIndex = 144;
            this.radioButtonColum12.TabStop = true;
            this.radioButtonColum12.UseVisualStyleBackColor = true;
            this.radioButtonColum12.CheckedChanged += new System.EventHandler(this.radioButtonColum12_CheckedChanged);
            // 
            // radioButtonColum13
            // 
            this.radioButtonColum13.AutoSize = true;
            this.radioButtonColum13.Location = new System.Drawing.Point(35, 459);
            this.radioButtonColum13.Name = "radioButtonColum13";
            this.radioButtonColum13.Size = new System.Drawing.Size(14, 13);
            this.radioButtonColum13.TabIndex = 145;
            this.radioButtonColum13.TabStop = true;
            this.radioButtonColum13.UseVisualStyleBackColor = true;
            this.radioButtonColum13.CheckedChanged += new System.EventHandler(this.radioButtonColum13_CheckedChanged);
            // 
            // radioButtonColum14
            // 
            this.radioButtonColum14.AutoSize = true;
            this.radioButtonColum14.Location = new System.Drawing.Point(35, 486);
            this.radioButtonColum14.Name = "radioButtonColum14";
            this.radioButtonColum14.Size = new System.Drawing.Size(14, 13);
            this.radioButtonColum14.TabIndex = 146;
            this.radioButtonColum14.TabStop = true;
            this.radioButtonColum14.UseVisualStyleBackColor = true;
            this.radioButtonColum14.CheckedChanged += new System.EventHandler(this.radioButtonColum14_CheckedChanged);
            // 
            // GroupBoxVocoder
            // 
            this.GroupBoxVocoder.BorderColor = System.Drawing.Color.White;
            this.GroupBoxVocoder.Controls.Add(this.Band5Reso);
            this.GroupBoxVocoder.Controls.Add(this.label82);
            this.GroupBoxVocoder.Controls.Add(this.Band5Cut);
            this.GroupBoxVocoder.Controls.Add(this.ComboBoxVocoderCarrier);
            this.GroupBoxVocoder.Controls.Add(this.Band4Reso);
            this.GroupBoxVocoder.Controls.Add(this.label80);
            this.GroupBoxVocoder.Controls.Add(this.Band4Cut);
            this.GroupBoxVocoder.Controls.Add(this.ComboBoxVocoderModulator);
            this.GroupBoxVocoder.Controls.Add(this.Band3Reso);
            this.GroupBoxVocoder.Controls.Add(this.label81);
            this.GroupBoxVocoder.Controls.Add(this.Band3Cut);
            this.GroupBoxVocoder.Controls.Add(this.Band1Cut);
            this.GroupBoxVocoder.Controls.Add(this.Band2Reso);
            this.GroupBoxVocoder.Controls.Add(this.Band1Reso);
            this.GroupBoxVocoder.Controls.Add(this.Band2Cut);
            this.GroupBoxVocoder.ForeColor = System.Drawing.Color.White;
            this.GroupBoxVocoder.Location = new System.Drawing.Point(416, 197);
            this.GroupBoxVocoder.Name = "GroupBoxVocoder";
            this.GroupBoxVocoder.Size = new System.Drawing.Size(212, 260);
            this.GroupBoxVocoder.TabIndex = 147;
            this.GroupBoxVocoder.TabStop = false;
            this.GroupBoxVocoder.Text = "Vocoder";
            this.GroupBoxVocoder.Visible = false;
            // 
            // Band5Reso
            // 
            this.Band5Reso.Location = new System.Drawing.Point(131, 216);
            this.Band5Reso.Name = "Band5Reso";
            this.Band5Reso.Size = new System.Drawing.Size(51, 20);
            this.Band5Reso.TabIndex = 157;
            this.Band5Reso.Text = "50";
            // 
            // label82
            // 
            this.label82.AutoSize = true;
            this.label82.Location = new System.Drawing.Point(92, 61);
            this.label82.Name = "label82";
            this.label82.Size = new System.Drawing.Size(38, 13);
            this.label82.TabIndex = 60;
            this.label82.Text = "TEST!";
            // 
            // Band5Cut
            // 
            this.Band5Cut.Location = new System.Drawing.Point(74, 216);
            this.Band5Cut.Name = "Band5Cut";
            this.Band5Cut.Size = new System.Drawing.Size(51, 20);
            this.Band5Cut.TabIndex = 156;
            this.Band5Cut.Text = "170";
            // 
            // ComboBoxVocoderCarrier
            // 
            this.ComboBoxVocoderCarrier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxVocoderCarrier.FormattingEnabled = true;
            this.ComboBoxVocoderCarrier.Items.AddRange(new object[] {
            "-",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxVocoderCarrier.Location = new System.Drawing.Point(6, 77);
            this.ComboBoxVocoderCarrier.Name = "ComboBoxVocoderCarrier";
            this.ComboBoxVocoderCarrier.Size = new System.Drawing.Size(62, 21);
            this.ComboBoxVocoderCarrier.TabIndex = 59;
            this.ComboBoxVocoderCarrier.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxVocoderCarrier_SelectionChangeCommitted);
            // 
            // Band4Reso
            // 
            this.Band4Reso.Location = new System.Drawing.Point(131, 194);
            this.Band4Reso.Name = "Band4Reso";
            this.Band4Reso.Size = new System.Drawing.Size(51, 20);
            this.Band4Reso.TabIndex = 155;
            this.Band4Reso.Text = "64";
            // 
            // label80
            // 
            this.label80.AutoSize = true;
            this.label80.Location = new System.Drawing.Point(6, 62);
            this.label80.Name = "label80";
            this.label80.Size = new System.Drawing.Size(37, 13);
            this.label80.TabIndex = 58;
            this.label80.Text = "Carrier";
            // 
            // Band4Cut
            // 
            this.Band4Cut.Location = new System.Drawing.Point(74, 193);
            this.Band4Cut.Name = "Band4Cut";
            this.Band4Cut.Size = new System.Drawing.Size(51, 20);
            this.Band4Cut.TabIndex = 154;
            this.Band4Cut.Text = "120";
            // 
            // ComboBoxVocoderModulator
            // 
            this.ComboBoxVocoderModulator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxVocoderModulator.FormattingEnabled = true;
            this.ComboBoxVocoderModulator.Items.AddRange(new object[] {
            "-",
            "V1",
            "V2",
            "V3",
            "V4"});
            this.ComboBoxVocoderModulator.Location = new System.Drawing.Point(6, 33);
            this.ComboBoxVocoderModulator.Name = "ComboBoxVocoderModulator";
            this.ComboBoxVocoderModulator.Size = new System.Drawing.Size(62, 21);
            this.ComboBoxVocoderModulator.TabIndex = 44;
            this.ComboBoxVocoderModulator.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxVocoderModulator_SelectionChangeCommitted);
            // 
            // Band3Reso
            // 
            this.Band3Reso.Location = new System.Drawing.Point(131, 170);
            this.Band3Reso.Name = "Band3Reso";
            this.Band3Reso.Size = new System.Drawing.Size(51, 20);
            this.Band3Reso.TabIndex = 153;
            this.Band3Reso.Text = "64";
            // 
            // label81
            // 
            this.label81.AutoSize = true;
            this.label81.Location = new System.Drawing.Point(6, 18);
            this.label81.Name = "label81";
            this.label81.Size = new System.Drawing.Size(54, 13);
            this.label81.TabIndex = 36;
            this.label81.Text = "Modulator";
            // 
            // Band3Cut
            // 
            this.Band3Cut.Location = new System.Drawing.Point(74, 169);
            this.Band3Cut.Name = "Band3Cut";
            this.Band3Cut.Size = new System.Drawing.Size(51, 20);
            this.Band3Cut.TabIndex = 152;
            this.Band3Cut.Text = "70";
            // 
            // Band1Cut
            // 
            this.Band1Cut.Location = new System.Drawing.Point(74, 123);
            this.Band1Cut.Name = "Band1Cut";
            this.Band1Cut.Size = new System.Drawing.Size(51, 20);
            this.Band1Cut.TabIndex = 148;
            this.Band1Cut.Text = "25";
            // 
            // Band2Reso
            // 
            this.Band2Reso.Location = new System.Drawing.Point(131, 147);
            this.Band2Reso.Name = "Band2Reso";
            this.Band2Reso.Size = new System.Drawing.Size(52, 20);
            this.Band2Reso.TabIndex = 151;
            this.Band2Reso.Text = "64";
            // 
            // Band1Reso
            // 
            this.Band1Reso.Location = new System.Drawing.Point(139, 120);
            this.Band1Reso.Name = "Band1Reso";
            this.Band1Reso.Size = new System.Drawing.Size(51, 20);
            this.Band1Reso.TabIndex = 149;
            this.Band1Reso.Text = "64";
            // 
            // Band2Cut
            // 
            this.Band2Cut.Location = new System.Drawing.Point(74, 146);
            this.Band2Cut.Name = "Band2Cut";
            this.Band2Cut.Size = new System.Drawing.Size(52, 20);
            this.Band2Cut.TabIndex = 150;
            this.Band2Cut.Text = "50";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(132)))), ((int)(((byte)(5)))));
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(27, 613);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(603, 129);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.menuStrip1.BackgroundImage = global::AmigaKlangGUI.Properties.Resources.Unbenannt;
            this.menuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuStrip1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(85)))), ((int)(((byte)(173)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(656, 24);
            this.menuStrip1.TabIndex = 92;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemLoadInstrument,
            this.toolStripMenuItemLoadPatch});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(45, 20);
            this.toolStripMenuItem1.Text = "Load";
            // 
            // toolStripMenuItemLoadInstrument
            // 
            this.toolStripMenuItemLoadInstrument.Name = "toolStripMenuItemLoadInstrument";
            this.toolStripMenuItemLoadInstrument.Size = new System.Drawing.Size(161, 22);
            this.toolStripMenuItemLoadInstrument.Text = "Load Instrument";
            this.toolStripMenuItemLoadInstrument.Click += new System.EventHandler(this.toolStripMenuItemLoadInstrument_Click);
            // 
            // toolStripMenuItemLoadPatch
            // 
            this.toolStripMenuItemLoadPatch.Name = "toolStripMenuItemLoadPatch";
            this.toolStripMenuItemLoadPatch.Size = new System.Drawing.Size(161, 22);
            this.toolStripMenuItemLoadPatch.Text = "Load Patch";
            this.toolStripMenuItemLoadPatch.Click += new System.EventHandler(this.toolStripMenuItemLoadPatch_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemSaveInstrument,
            this.toolStripMenuItemSavePatch,
            this.toolStripMenuItemSaveSample,
            this.toolStripMenuItemExport,
            this.exportGeneratorFilesToolStripMenuItem,
            this.exportBinaryToolStripMenuItem,
            this.exportScriptToolStripMenuItem,
            this.exportAtariExeToolStripMenuItem});
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(43, 20);
            this.toolStripMenuItem2.Text = "Save";
            // 
            // toolStripMenuItemSaveInstrument
            // 
            this.toolStripMenuItemSaveInstrument.Name = "toolStripMenuItemSaveInstrument";
            this.toolStripMenuItemSaveInstrument.Size = new System.Drawing.Size(197, 22);
            this.toolStripMenuItemSaveInstrument.Text = "Save Instrument";
            this.toolStripMenuItemSaveInstrument.Click += new System.EventHandler(this.toolStripMenuItemSaveInstrument_Click);
            // 
            // toolStripMenuItemSavePatch
            // 
            this.toolStripMenuItemSavePatch.Name = "toolStripMenuItemSavePatch";
            this.toolStripMenuItemSavePatch.Size = new System.Drawing.Size(197, 22);
            this.toolStripMenuItemSavePatch.Text = "Save Patch";
            this.toolStripMenuItemSavePatch.Click += new System.EventHandler(this.toolStripMenuItemSavePatch_Click);
            // 
            // toolStripMenuItemSaveSample
            // 
            this.toolStripMenuItemSaveSample.Name = "toolStripMenuItemSaveSample";
            this.toolStripMenuItemSaveSample.Size = new System.Drawing.Size(197, 22);
            this.toolStripMenuItemSaveSample.Text = "Save Sample";
            this.toolStripMenuItemSaveSample.Click += new System.EventHandler(this.toolStripMenuItemSaveSample_Click);
            // 
            // toolStripMenuItemExport
            // 
            this.toolStripMenuItemExport.Name = "toolStripMenuItemExport";
            this.toolStripMenuItemExport.Size = new System.Drawing.Size(197, 22);
            this.toolStripMenuItemExport.Text = "Export Samples to mod";
            this.toolStripMenuItemExport.Click += new System.EventHandler(this.toolStripMenuItemExport_Click);
            // 
            // exportGeneratorFilesToolStripMenuItem
            // 
            this.exportGeneratorFilesToolStripMenuItem.Name = "exportGeneratorFilesToolStripMenuItem";
            this.exportGeneratorFilesToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.exportGeneratorFilesToolStripMenuItem.Text = "Export Amiga Exe";
            this.exportGeneratorFilesToolStripMenuItem.Click += new System.EventHandler(this.exportGeneratorFilesToolStripMenuItem_Click);
            // 
            // exportBinaryToolStripMenuItem
            // 
            this.exportBinaryToolStripMenuItem.Name = "exportBinaryToolStripMenuItem";
            this.exportBinaryToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.exportBinaryToolStripMenuItem.Text = "Export Amiga Binary";
            this.exportBinaryToolStripMenuItem.Click += new System.EventHandler(this.exportBinaryToolStripMenuItem_Click);
            // 
            // exportScriptToolStripMenuItem
            // 
            this.exportScriptToolStripMenuItem.Name = "exportScriptToolStripMenuItem";
            this.exportScriptToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.exportScriptToolStripMenuItem.Text = "Export ASM Source";
            this.exportScriptToolStripMenuItem.Click += new System.EventHandler(this.exportScriptToolStripMenuItem_Click_1);
            // 
            // exportAtariExeToolStripMenuItem
            // 
            this.exportAtariExeToolStripMenuItem.Name = "exportAtariExeToolStripMenuItem";
            this.exportAtariExeToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.exportAtariExeToolStripMenuItem.Text = "Export Atari Exe";
            this.exportAtariExeToolStripMenuItem.Click += new System.EventHandler(this.exportAtariExeToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemInfo,
            this.clearAllToolStripMenuItem,
            this.ToolStripMenuItemDesign});
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(40, 20);
            this.toolStripMenuItem3.Text = "Info";
            // 
            // toolStripMenuItemInfo
            // 
            this.toolStripMenuItemInfo.Name = "toolStripMenuItemInfo";
            this.toolStripMenuItemInfo.Size = new System.Drawing.Size(116, 22);
            this.toolStripMenuItemInfo.Text = "Info";
            this.toolStripMenuItemInfo.Click += new System.EventHandler(this.toolStripMenuItemInfo_Click);
            // 
            // clearAllToolStripMenuItem
            // 
            this.clearAllToolStripMenuItem.Name = "clearAllToolStripMenuItem";
            this.clearAllToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.clearAllToolStripMenuItem.Text = "Clear all";
            this.clearAllToolStripMenuItem.Click += new System.EventHandler(this.clearAllToolStripMenuItem_Click);
            // 
            // ToolStripMenuItemDesign
            // 
            this.ToolStripMenuItemDesign.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemAmiga,
            this.ToolStripMenuItemAtari});
            this.ToolStripMenuItemDesign.Name = "ToolStripMenuItemDesign";
            this.ToolStripMenuItemDesign.Size = new System.Drawing.Size(116, 22);
            this.ToolStripMenuItemDesign.Text = "Design";
            // 
            // ToolStripMenuItemAmiga
            // 
            this.ToolStripMenuItemAmiga.Checked = true;
            this.ToolStripMenuItemAmiga.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ToolStripMenuItemAmiga.Name = "ToolStripMenuItemAmiga";
            this.ToolStripMenuItemAmiga.Size = new System.Drawing.Size(130, 22);
            this.ToolStripMenuItemAmiga.Text = "Amiga 500";
            this.ToolStripMenuItemAmiga.Click += new System.EventHandler(this.ToolStripMenuItemAmiga_Click);
            // 
            // ToolStripMenuItemAtari
            // 
            this.ToolStripMenuItemAtari.Name = "ToolStripMenuItemAtari";
            this.ToolStripMenuItemAtari.Size = new System.Drawing.Size(130, 22);
            this.ToolStripMenuItemAtari.Text = "Atari ST";
            this.ToolStripMenuItemAtari.Click += new System.EventHandler(this.ToolStripMenuItemAtari_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(85)))), ((int)(((byte)(173)))));
            this.ClientSize = new System.Drawing.Size(656, 761);
            this.Controls.Add(this.label78);
            this.Controls.Add(this.GroupBoxVocoder);
            this.Controls.Add(this.radioButtonColum14);
            this.Controls.Add(this.radioButtonColum13);
            this.Controls.Add(this.label41);
            this.Controls.Add(this.radioButtonColum12);
            this.Controls.Add(this.radioButtonColum11);
            this.Controls.Add(this.radioButtonColum10);
            this.Controls.Add(this.radioButtonColum9);
            this.Controls.Add(this.radioButtonColum8);
            this.Controls.Add(this.radioButtonColum7);
            this.Controls.Add(this.radioButtonColum6);
            this.Controls.Add(this.radioButtonColum5);
            this.Controls.Add(this.radioButtonColum4);
            this.Controls.Add(this.radioButtonColum3);
            this.Controls.Add(this.radioButtonColum15);
            this.Controls.Add(this.radioButtonOutput);
            this.Controls.Add(this.radioButtonColum16);
            this.Controls.Add(this.radioButtonColum2);
            this.Controls.Add(this.radioButtonColum1);
            this.Controls.Add(this.TextBoxTranslateDan);
            this.Controls.Add(this.buttonGenerateSingleSample);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label47);
            this.Controls.Add(this.labelTotalSize);
            this.Controls.Add(this.label65);
            this.Controls.Add(this.TextBoxTranslateIswitch);
            this.Controls.Add(this.groupBoxImportSample);
            this.Controls.Add(this.groupBoxInstruments);
            this.Controls.Add(this.TextBoxTranslateIset);
            this.Controls.Add(this.TextBoxTranslateInst);
            this.Controls.Add(this.groupBoxSampleLength);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.buttonZoomReset);
            this.Controls.Add(this.hScrollBarZoomZoom);
            this.Controls.Add(this.hScrollBarZoomScroll);
            this.Controls.Add(this.groupBoxPlay);
            this.Controls.Add(this.GroupBoxNodes);
            this.Controls.Add(this.checkBoxSampleview);
            this.Controls.Add(this.label48);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.TextBoxTranslateIlen);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.GroupBoxClone);
            this.Controls.Add(this.GroupBoxChord);
            this.Controls.Add(this.GroupBoxOnepole);
            this.Controls.Add(this.GroupBoxFilter);
            this.Controls.Add(this.GroupBoxCtrl);
            this.Controls.Add(this.GroupBoxReverb);
            this.Controls.Add(this.GroupBoxComb);
            this.Controls.Add(this.GroupBoxEnva);
            this.Controls.Add(this.GroupBoxDelay);
            this.Controls.Add(this.GroupBoxMul);
            this.Controls.Add(this.GroupBoxEnvelope);
            this.Controls.Add(this.GroupBoxEnvd);
            this.Controls.Add(this.GroupBoxOsc_noise);
            this.Controls.Add(this.GroupBoxOsc_tri);
            this.Controls.Add(this.GroupBoxOsc_sine);
            this.Controls.Add(this.GroupBoxOsc_pulse);
            this.Controls.Add(this.GroupBoxAdd);
            this.Controls.Add(this.GroupBoxVol);
            this.Controls.Add(this.GroupBoxLoop);
            this.Controls.Add(this.GroupBoxImport);
            this.Controls.Add(this.GroupBoxDistortion);
            this.Controls.Add(this.GroupBoxSh);
            this.Controls.Add(this.GroupBoxOsc_saw);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = " AmigaKlang V1.01";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.Move += new System.EventHandler(this.Form1_Move);
            this.GroupBoxDistortion.ResumeLayout(false);
            this.GroupBoxDistortion.PerformLayout();
            this.GroupBoxVol.ResumeLayout(false);
            this.GroupBoxVol.PerformLayout();
            this.GroupBoxOsc_saw.ResumeLayout(false);
            this.GroupBoxOsc_saw.PerformLayout();
            this.GroupBoxOsc_tri.ResumeLayout(false);
            this.GroupBoxOsc_tri.PerformLayout();
            this.GroupBoxOsc_sine.ResumeLayout(false);
            this.GroupBoxOsc_sine.PerformLayout();
            this.GroupBoxAdd.ResumeLayout(false);
            this.GroupBoxAdd.PerformLayout();
            this.GroupBoxOsc_pulse.ResumeLayout(false);
            this.GroupBoxOsc_pulse.PerformLayout();
            this.GroupBoxOsc_noise.ResumeLayout(false);
            this.GroupBoxOsc_noise.PerformLayout();
            this.GroupBoxEnva.ResumeLayout(false);
            this.GroupBoxEnva.PerformLayout();
            this.GroupBoxEnvd.ResumeLayout(false);
            this.GroupBoxEnvd.PerformLayout();
            this.GroupBoxEnvelope.ResumeLayout(false);
            this.GroupBoxEnvelope.PerformLayout();
            this.GroupBoxMul.ResumeLayout(false);
            this.GroupBoxMul.PerformLayout();
            this.GroupBoxDelay.ResumeLayout(false);
            this.GroupBoxDelay.PerformLayout();
            this.GroupBoxComb.ResumeLayout(false);
            this.GroupBoxComb.PerformLayout();
            this.GroupBoxReverb.ResumeLayout(false);
            this.GroupBoxReverb.PerformLayout();
            this.GroupBoxCtrl.ResumeLayout(false);
            this.GroupBoxCtrl.PerformLayout();
            this.GroupBoxFilter.ResumeLayout(false);
            this.GroupBoxFilter.PerformLayout();
            this.GroupBoxNodes.ResumeLayout(false);
            this.groupBoxPlay.ResumeLayout(false);
            this.groupBoxPlay.PerformLayout();
            this.GroupBoxChord.ResumeLayout(false);
            this.GroupBoxChord.PerformLayout();
            this.GroupBoxLoop.ResumeLayout(false);
            this.GroupBoxLoop.PerformLayout();
            this.GroupBoxClone.ResumeLayout(false);
            this.GroupBoxClone.PerformLayout();
            this.groupBoxSampleLength.ResumeLayout(false);
            this.groupBoxSampleLength.PerformLayout();
            this.groupBoxInstruments.ResumeLayout(false);
            this.groupBoxInstruments.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.GroupBoxSh.ResumeLayout(false);
            this.GroupBoxSh.PerformLayout();
            this.GroupBoxImport.ResumeLayout(false);
            this.GroupBoxImport.PerformLayout();
            this.groupBoxImportSample.ResumeLayout(false);
            this.groupBoxImportSample.PerformLayout();
            this.GroupBoxOnepole.ResumeLayout(false);
            this.GroupBoxOnepole.PerformLayout();
            this.GroupBoxVocoder.ResumeLayout(false);
            this.GroupBoxVocoder.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        
        private System.Windows.Forms.Button buttonPlaySample;
        private System.Windows.Forms.ComboBox ComboBoxSampleAuswahl;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox ComboBoxClamp;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox comboBoxVolValue;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox TextBoxTranslateIlen;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox ComboBoxVar1;
        private System.Windows.Forms.ComboBox ComboBoxVar2;
        private System.Windows.Forms.ComboBox ComboBoxVar3;
        private System.Windows.Forms.ComboBox ComboBoxVar4;
        private System.Windows.Forms.ComboBox ComboBoxVar5;
        private System.Windows.Forms.ComboBox ComboBoxFunction1;
        private System.Windows.Forms.ComboBox ComboBoxFunction2;
        private System.Windows.Forms.ComboBox ComboBoxFunction3;
        private System.Windows.Forms.ComboBox ComboBoxFunction4;
        private System.Windows.Forms.ComboBox ComboBoxFunction5;
        private System.Windows.Forms.Button ButtonEdit1;
        private System.Windows.Forms.Button ButtonEdit2;
        private System.Windows.Forms.Button ButtonEdit3;
        private System.Windows.Forms.Button ButtonEdit4;
        private System.Windows.Forms.Button ButtonEdit5;
        private System.Windows.Forms.ComboBox ComboBoxDistortionVal;
        private System.Windows.Forms.ComboBox ComboBoxVolGain;
        private System.Windows.Forms.ComboBox ComboBoxVolVal;
        private System.Windows.Forms.ComboBox ComboBoxOscsawGain;
        private System.Windows.Forms.ComboBox ComboBoxOscsawFreq;
        private System.Windows.Forms.TextBox TextBoxOscsawGainValue;
        private System.Windows.Forms.TextBox TextBoxOscsawFreqValue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ComboBoxOsctriFreq;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox ComboBoxOsctriGain;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox TextBoxOsctriFreqValue;
        private System.Windows.Forms.TextBox TextBoxOsctriGainValue;
        private System.Windows.Forms.Button buttonStopSample;
        private System.Windows.Forms.ComboBox ComboBoxAddVal2;
        private System.Windows.Forms.ComboBox ComboBoxAddVal1;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox ComboBoxOscsineFreq;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox ComboBoxOscsineGain;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox TextBoxOscsineFreqValue;
        private System.Windows.Forms.TextBox TextBoxOscsineGainValue;
        private System.Windows.Forms.TextBox TextBoxAddVal2Value;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.ComboBox ComboBoxOscpulseWidth;
        private System.Windows.Forms.TextBox TextBoxOscpulseWidthValue;
        private System.Windows.Forms.ComboBox ComboBoxOscpulseFreq;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.ComboBox ComboBoxOscpulseGain;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox TextBoxOscpulseFreqValue;
        private System.Windows.Forms.TextBox TextBoxOscpulseGainValue;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.ComboBox ComboBoxOscnoiseGain;
        private System.Windows.Forms.TextBox TextBoxOscnoiseGainValue;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.ComboBox ComboBoxEnvaGain;
        private System.Windows.Forms.TextBox TextBoxEnvaGainValue;
        private System.Windows.Forms.TextBox TextBoxEnvaAttackValue;
        private System.Windows.Forms.TextBox TextBoxEnvdSustainValue;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.TextBox TextBoxEnvdDecayValue;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.ComboBox ComboBoxEnvdGain;
        private System.Windows.Forms.TextBox TextBoxEnvdGainValue;
        private System.Windows.Forms.TextBox TextBoxMulVal2Value;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.ComboBox ComboBoxMulVal2;
        private System.Windows.Forms.ComboBox ComboBoxMulVal1;
        private System.Windows.Forms.ComboBox ComboBoxDelayValue;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.ComboBox ComboBoxDelayGain;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.TextBox TextBoxDelayGainValue;
        private System.Windows.Forms.HScrollBar hScrollBarSamplelength;
        private System.Windows.Forms.HScrollBar hScrollBarOscsawFreqValue;
        private System.Windows.Forms.HScrollBar hScrollBarOscsawGainValue;
        private System.Windows.Forms.HScrollBar hScrollBarOsctriGainValue;
        private System.Windows.Forms.HScrollBar hScrollBarOsctriFreqValue;
        private System.Windows.Forms.HScrollBar hScrollBarOscsineGainValue;
        private System.Windows.Forms.HScrollBar hScrollBarOscsineFreqValue;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.HScrollBar hScrollBarOscpulseWidthValue;
        private System.Windows.Forms.HScrollBar hScrollBarOscpulseGainValue;
        private System.Windows.Forms.HScrollBar hScrollBarOscpulseFreqValue;
        private System.Windows.Forms.HScrollBar hScrollBarOscnoiseGainValue;
        private System.Windows.Forms.HScrollBar hScrollBarEnvaGainValue;
        private System.Windows.Forms.HScrollBar hScrollBarEnvaAttackValue;
        private System.Windows.Forms.HScrollBar hScrollBarEnvdGainValue;
        private System.Windows.Forms.HScrollBar hScrollBarEnvdSustainValue;
        private System.Windows.Forms.HScrollBar hScrollBarEnvdDecayValue;
        private System.Windows.Forms.Button ButtonEdit6;
        private System.Windows.Forms.ComboBox ComboBoxFunction6;
        private System.Windows.Forms.ComboBox ComboBoxVar6;
        private System.Windows.Forms.HScrollBar hScrollBarAddVal2Value;
        private System.Windows.Forms.HScrollBar hScrollBarMulVal2Value;
        private System.Windows.Forms.HScrollBar hScrollBarDelayGainValue;
        private System.Windows.Forms.HScrollBar hScrollBarDelayDelayValue;
        private System.Windows.Forms.ComboBox ComboBoxDelayDelay;
        private System.Windows.Forms.TextBox TextBoxDelayDelayValue;
        private System.Windows.Forms.HScrollBar hScrollBarCombGainValue;
        private System.Windows.Forms.HScrollBar hScrollBarCombFeedbackValue;
        private System.Windows.Forms.HScrollBar hScrollBarCombDelayValue;
        private System.Windows.Forms.ComboBox ComboBoxCombDelay;
        private System.Windows.Forms.TextBox TextBoxCombDelayValue;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox ComboBoxCombFeedback;
        private System.Windows.Forms.TextBox TextBoxCombFeedbackValue;
        private System.Windows.Forms.ComboBox ComboBoxCombValue;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox ComboBoxCombGain;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox TextBoxCombGainValue;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.HScrollBar hScrollBarReverbGainValue;
        private System.Windows.Forms.HScrollBar hScrollBarReverbFeedbackValue;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.ComboBox ComboBoxReverbFeedback;
        private System.Windows.Forms.TextBox TextBoxReverbFeedbackValue;
        private System.Windows.Forms.ComboBox ComboBoxReverbValue;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.ComboBox ComboBoxReverbGain;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox TextBoxReverbGainValue;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.ComboBox ComboBoxCtrlValue;
        private System.Windows.Forms.HScrollBar hScrollBarFilterResonanceValue;
        private System.Windows.Forms.HScrollBar hScrollBarFilterCutoffValue;
        private System.Windows.Forms.ComboBox ComboBoxFilterCutoff;
        private System.Windows.Forms.TextBox TextBoxFilterCutoffValue;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.ComboBox ComboBoxFilterResonance;
        private System.Windows.Forms.TextBox TextBoxFilterResonanceValue;
        private System.Windows.Forms.ComboBox ComboBoxFilterValue;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.ComboBox ComboBoxFilterMode;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.HScrollBar hScrollBarVolGainValue;
        private System.Windows.Forms.TextBox TextBoxVolGainValue;
        private System.Windows.Forms.Button ButtonEdit7;
        private System.Windows.Forms.ComboBox ComboBoxFunction7;
        private System.Windows.Forms.ComboBox ComboBoxVar7;
        private System.Windows.Forms.Button ButtonEdit8;
        private System.Windows.Forms.ComboBox ComboBoxFunction8;
        private System.Windows.Forms.ComboBox ComboBoxVar8;
        private System.Windows.Forms.Button ButtonEdit9;
        private System.Windows.Forms.ComboBox ComboBoxFunction9;
        private System.Windows.Forms.ComboBox ComboBoxVar9;
        private System.Windows.Forms.Button ButtonEdit10;
        private System.Windows.Forms.ComboBox ComboBoxFunction10;
        private System.Windows.Forms.ComboBox ComboBoxVar10;
        private System.Windows.Forms.Button ButtonEdit11;
        private System.Windows.Forms.ComboBox ComboBoxFunction11;
        private System.Windows.Forms.ComboBox ComboBoxVar11;
        private System.Windows.Forms.Button ButtonEdit12;
        private System.Windows.Forms.ComboBox ComboBoxFunction12;
        private System.Windows.Forms.ComboBox ComboBoxVar12;
        private System.Windows.Forms.Button ButtonEdit13;
        private System.Windows.Forms.ComboBox ComboBoxFunction13;
        private System.Windows.Forms.ComboBox ComboBoxVar13;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Button ButtonEdit14;
        private System.Windows.Forms.ComboBox ComboBoxFunction14;
        private System.Windows.Forms.ComboBox ComboBoxVar14;
        private System.Windows.Forms.Button ButtonEdit15;
        private System.Windows.Forms.ComboBox ComboBoxFunction15;
        private System.Windows.Forms.ComboBox ComboBoxVar15;
        private System.Windows.Forms.Button ButtonEdit16;
        private System.Windows.Forms.ComboBox ComboBoxFunction16;
        private System.Windows.Forms.ComboBox ComboBoxVar16;
        private System.Windows.Forms.CheckBox checkBoxSampleview;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemLoadInstrument;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemLoadPatch;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSaveInstrument;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSavePatch;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemInfo;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSaveSample;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExport;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ComboBox ComboBoxCloneSamplenr;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.ComboBox ComboBoxCloneReverse;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.ComboBox ComboBoxChordSamplenr;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.ComboBox ComboBoxChordNote3;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.ComboBox ComboBoxChordNote2;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.ComboBox ComboBoxChordShift;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.ComboBox ComboBoxChordNote1;
        private System.Windows.Forms.HScrollBar hScrollBarChordShiftValue;
        private System.Windows.Forms.TextBox TextBoxChordShiftValue;
        private System.Windows.Forms.TextBox TextBoxLoopLengthValue;
        private System.Windows.Forms.TextBox TextBoxLoopOffsetValue;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.HScrollBar hScrollBarLoopOffsetValue;
        private System.Windows.Forms.HScrollBar hScrollBarZoomScroll;
        private System.Windows.Forms.Button buttonGenerateAll;
        private System.Windows.Forms.HScrollBar hScrollBarZoomZoom;
        private System.Windows.Forms.Button buttonZoomReset;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label1;
        private TextBox textBoxSamplelengthDec;
        private ComboBox ComboBoxNotes;
        private TextBox TextBoxTranslateInst;
        private ToolStripMenuItem exportGeneratorFilesToolStripMenuItem;
        private TextBox TextBoxTranslateIset;
        private Button buttonDown3;
        private Button buttonUp3;
        private Button buttonDown2;
        private Button buttonUp2;
        private Button buttonDown1;
        private Button buttonUp15;
        private Button buttonDown14;
        private Button buttonUp14;
        private Button buttonDown13;
        private Button buttonUp13;
        private Button buttonDown12;
        private Button buttonUp12;
        private Button buttonDown11;
        private Button buttonUp11;
        private Button buttonDown10;
        private Button buttonUp10;
        private Button buttonDown9;
        private Button buttonUp9;
        private Button buttonDown8;
        private Button buttonUp8;
        private Button buttonDown7;
        private Button buttonUp7;
        private Button buttonDown6;
        private Button buttonUp6;
        private Button buttonDown5;
        private Button buttonUp5;
        private Button buttonDown4;
        private Button buttonUp4;
        private Button buttonUp16;
        private Button buttonDown15;
        private Label labelInstrumentNr;
        private Label label4;
        private Label labelCloneName;
        private Label labelChordName;
        private HScrollBar hScrollBarShStepValue;
        private TextBox TextBoxShStepValue;
        private Label label5;
        private Label label15;
        private ComboBox ComboBoxShStep;
        private ComboBox ComboBoxShVal;
        private TextBox TextBoxImportSampleLength;
        private Button buttonImportSample;
        private Label label18;
        private ComboBox ComboBoxImportNumber;
        private ComboBox comboBoxSelectImport;
        private TextBox TextBoxMulVal2ValueFloat;
        private Label label30;
        private Label label22;
        private Label label41;
        private ToolStripMenuItem exportBinaryToolStripMenuItem;
        private TextBox TextBoxTranslateIswitch;
        private ToolStripMenuItem clearAllToolStripMenuItem;
        private Label label63;
        private TextBox TextBoxImportSampleLengthTotal;
        private TextBox TextBoxImportSampleLengthDec;
        private Button buttonClearImportSample;
        private HScrollBar hScrollBarOscnoiseSeedValue;
        private TextBox TextBoxOscnoiseSeedValue;
        private Label label64;
        private Label label65;
        private Label labelTotalSize;
        private Timer timer1;
        private Label label47;
        private Button button3;
        private Label label68;
        private Button buttonGenerateSingleSample;
        private Label label69;
        private TextBox TextBoxCloneOffsetValue;
        private HScrollBar hScrollBarCloneOffsetValue;
        private TextBox TextBoxTranslateDan;
        private ToolStripMenuItem exportScriptToolStripMenuItem;
        private Label label70;
        private HScrollBar hScrollBarDistortionGainValue;
        private TextBox TextBoxDistortionGainValue;
        private ComboBox ComboBoxDistortionGain;
        private HScrollBar hScrollBarOnepoleCutoffValue;
        private ComboBox ComboBoxOnepoleCutoff;
        private TextBox TextBoxOnepoleCutoffValue;
        private ComboBox ComboBoxOnepoleValue;
        private Label label72;
        private ComboBox ComboBoxOnepoleMode;
        private Label label73;
        private Label label74;
        private Label labelImportedName;
        private Label label71;
        private Label labelDelay;
        private Label labelComb;
        private Label labelReverb;
        private Label labelCtrl;
        private Label labelChordgen;
        private HScrollBar hScrollBarEnvelopeAttackValue;
        private TextBox TextBoxEnvelopeAttackValue;
        private Label label76;
        private HScrollBar hScrollBarEnvelopeGainValue;
        private HScrollBar hScrollBarEnvelopeSustainValue;
        private HScrollBar hScrollBarEnvelopeDecayValue;
        private TextBox TextBoxEnvelopeSustainValue;
        private Label label66;
        private TextBox TextBoxEnvelopeDecayValue;
        private Label label67;
        private Label label75;
        private TextBox TextBoxEnvelopeGainValue;
        private HScrollBar hScrollBarEnvelopeReleaseValue;
        private TextBox TextBoxEnvelopeReleaseValue;
        private Label label77;
        private RadioButton radioButtonColum1;
        private RadioButton radioButtonColum2;
        private RadioButton radioButtonColum16;
        private RadioButton radioButtonOutput;
        private RadioButton radioButtonColum15;
        private RadioButton radioButtonColum3;
        private RadioButton radioButtonColum4;
        private RadioButton radioButtonColum5;
        private RadioButton radioButtonColum6;
        private RadioButton radioButtonColum7;
        private RadioButton radioButtonColum8;
        private RadioButton radioButtonColum9;
        private RadioButton radioButtonColum10;
        private RadioButton radioButtonColum11;
        private RadioButton radioButtonColum12;
        private RadioButton radioButtonColum13;
        private RadioButton radioButtonColum14;
        private Label label78;
        private HScrollBar hScrollBarCloneTransposeValue;
        private TextBox TextBoxCloneTransposeValue;
        private Label label79;
        private ComboBox ComboBoxCloneTranspose;
        private ComboBox ComboBoxVocoderCarrier;
        private Label label80;
        private ComboBox ComboBoxVocoderModulator;
        private Label label81;
        private Label label82;
        private TextBox Band1Cut;
        private TextBox Band1Reso;
        private TextBox Band2Reso;
        private TextBox Band2Cut;
        private TextBox Band3Reso;
        private TextBox Band3Cut;
        private TextBox Band4Reso;
        private TextBox Band4Cut;
        private TextBox Band5Reso;
        private TextBox Band5Cut;
        private ToolStripMenuItem ToolStripMenuItemDesign;
        private ToolStripMenuItem ToolStripMenuItemAmiga;
        private ToolStripMenuItem ToolStripMenuItemAtari;
        private ToolStripMenuItem exportAtariExeToolStripMenuItem;
        private MyGroupBox GroupBoxDistortion;
        private MyGroupBox GroupBoxVol;
        private MyGroupBox GroupBoxOsc_saw;
        private MyGroupBox GroupBoxOsc_tri;
        private MyGroupBox GroupBoxOsc_sine;
        private MyGroupBox GroupBoxAdd;
        private MyGroupBox GroupBoxOsc_pulse;
        private MyGroupBox GroupBoxOsc_noise;
        private MyGroupBox GroupBoxEnva;
        private MyGroupBox GroupBoxEnvd;
        private MyGroupBox GroupBoxMul;
        private MyGroupBox GroupBoxDelay;
        private MyGroupBox GroupBoxComb;
        private MyGroupBox GroupBoxReverb;
        private MyGroupBox GroupBoxCtrl;
        private MyGroupBox GroupBoxFilter;
        private MyGroupBox GroupBoxNodes;
        private MyGroupBox groupBoxPlay;
        private MyGroupBox GroupBoxChord;
        private MyGroupBox GroupBoxLoop;
        private MyGroupBox GroupBoxClone;
        private MyGroupBox groupBoxSampleLength;
        private MyGroupBox groupBoxInstruments;
        private MyGroupBox GroupBoxSh;
        private MyGroupBox GroupBoxImport;
        private MyGroupBox groupBoxImportSample;
        private MyGroupBox GroupBoxOnepole;
        private MyGroupBox GroupBoxEnvelope;
        private MyGroupBox GroupBoxVocoder;
    }
}

