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
using System.Diagnostics;
using System.Drawing.Text;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

//        <requestedExecutionLevel level="asInvoker" uiAccess="false" />
// todo:  

// check looplength and loopoffset values while exporting           (done)

// translate:   check and limit clone sample length somehow         (done)
// translate:   maybe check for errors while parsing                (done, some checks still missing.. when var is missing)
// translate:   only translate used instruments                     (done)

// export:      create path for empty.mod                           (done)
// export:      handle msgboxes correct                             (done)
// export:      export samplelength + loop params to mod            (done)
// export:      calculate max alloc memory for mod+smp              (wip)     
// export:      author and song name


// form:        
// input in text boxes with keyboard                                (done)  
// use Function keys to render and play samples                     (done)


// generate:    reset arrays and noise gen variables                (done)

// nodes:       comb filter has silence at start ?                  (done)
// nodes:       create s&h node                                     (done)
// nodes:       create distortion node                              (done)
// nodes:       seed for noise osc ?
// nodes:       1pole filter                                        (done)
// nodes:       transpose on clonesample                            (done, dan has to update aklang2asm)
// nodes:       ADSR Envelope                                       (done)   
// nodes:       Vocoder                                             (wip)

namespace AmigaKlangGUI
{
    using static Amigaklang;

    public partial class Form1 : Form
    {

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
        IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);

        private PrivateFontCollection fonts = new PrivateFontCollection();

        Font myFont;

        // Amiga & Atari Design
        public static Color col1Atari = Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(206)))), ((int)(((byte)(10)))));     //green 
        public static Color col1Amiga = Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(85)))), ((int)(((byte)(173)))));      //blue
        public static Color col2Atari = Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));   //white
        public static Color col2Amiga = Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(95)))), ((int)(((byte)(0)))));      //orange
        
        public Color colSamplebox = col2Amiga;
        public Color colLines = Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
        public Color colCirclesBg = col1Amiga;
        public Color colCirclesErr = col2Amiga;

        // arrays with texts for the translator
        string[] arrayvartext = new string[] { "", "v1", "v2", "v3", "v4" };
        string[] arrayfunctiontext = new string[] { "", "vol", "osc_saw", "osc_tri", "osc_sine", "osc_pulse", "osc_noise", "enva", "envd", "add", "mul", "dly_cyc", "cmb_flt_n", "reverb", "ctrl", "sv_flt_n", "distortion", "", "chordgen", "sh", "", "onepole_flt", "", "adsr", "vocoder" };

        public bool clonewithtranspose;
        // instrument number, function colum
        int[,] arrayvar = new int[32, 20];
        int[,] arrayfunction = new int[32, 20];
        byte[,] arrayinstance = new byte[32, 20];
        short[,] arrayfrequency = new short[32, 20];
        short[,] arrayfrequencyval = new short[32, 20];
        byte[,] arraygain = new byte[32, 20];
        byte[,] arraygainval = new byte[32, 20];
        byte[,] arraywidth = new byte[32, 20];
        byte[,] arraywidthval = new byte[32, 20];
        short[,] arrayval1 = new short[32, 20];
        short[,] arrayval1value = new short[32, 20];
        short[,] arrayval2 = new short[32, 20];
        short[,] arrayval2value = new short[32, 20];

        // 1d array for samplelength [sample number]
        static int[] samplelength = new int[0x20];
        static int[] loopoffset = new int[0x20];
        static int[] looplength = new int[0x20];
        static int[] importedlength = new int[0x9];

        // samplerate
        int samplerate = 0;
        public static WaveOutEvent waveout = new WaveOutEvent();

        // buffer for amiga mod
        byte[] amigamod = new byte[1000000];

        // used in the generator
        short[] variable = new short[0x20];

        // other variables
        public static int edit = 0;
        public static int selectedindex = 0;
        public static string selecteditem = " ";
        public static int selectedindex2 = 0;
        public static string selecteditem2 = " ";

        // 2d array for samples [sample buffer, sample number]
        static byte[,] sample = new byte[0x3ffff, 0x20];
        static byte[,] importedsample = new byte[0x1ffff, 0x20];
        static byte[] importedsample1d = new byte[0x1ffff * 0x20];
        static byte[] actsample = new byte[0x1ffff];
        static byte[,,] visu = new byte[0x1ffff, 0x20,17];
        static byte[] visuakt = new byte[0x1ffff];

        // working variables
        short v1;
        short v2;
        short v3;
        short v4;

        // initialize
        public Form1()
        {
            
            clonewithtranspose = false;
            InitializeComponent();
            this.DoubleBuffered = true;

            if (clonewithtranspose == true)
            {
                ComboBoxCloneTranspose.Visible = true;
                hScrollBarCloneTransposeValue.Visible = true;
                TextBoxCloneTransposeValue.Visible = true;
            }
            /*
            // use new font
            byte[] fontData = Properties.Resources.Topaznew;
            IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
            System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            uint dummy = 0;
            fonts.AddMemoryFont(fontPtr, Properties.Resources.Topaznew.Length);
            AddFontMemResourceEx(fontPtr, (uint)Properties.Resources.Topaznew.Length, IntPtr.Zero, ref dummy);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);
            myFont = new Font(fonts.Families[0], 9.0F);
            groupBox1.Font = myFont;
            GroupBoxAdd.Font = myFont;
            GroupBoxChord.Font = myFont;
            GroupBoxDistortion.Font = myFont;
            GroupBoxClone.Font = myFont;
            GroupBoxComb.Font = myFont;
            GroupBoxCtrl.Font = myFont;
            GroupBoxDelay.Font = myFont;
            GroupBoxEnva.Font = myFont;
            GroupBoxEnvd.Font = myFont;
            GroupBoxFilter.Font = myFont;
            groupBoxInstruments.Font = myFont;
            GroupBoxLoop.Font = myFont;
            GroupBoxMul.Font = myFont;
            GroupBoxNodes.Font = myFont;
            GroupBoxOsc_noise.Font = myFont;
            GroupBoxOsc_pulse.Font = myFont;
            GroupBoxOsc_saw.Font = myFont;
            GroupBoxOsc_sine.Font = myFont;
            GroupBoxOsc_tri.Font = myFont;
            groupBoxPlay.Font = myFont;
            */

            // select first instrument
            ComboBoxSampleAuswahl.SelectedIndex = 0;
            labelInstrumentNr.Text = 1.ToString();

            // select first import
            comboBoxSelectImport.SelectedIndex = 0;


            // initialize samplelengths & looplengths to 0x0002
            for (int i = 0; i < 31; i++)
            {
                samplelength[i] = 2;
                looplength[i] = 2;
            }
            for (int i = 0; i < 8; i++)
            {
                importedlength[i] = 0;
            }


            hScrollBarSamplelength.Value = samplelength[ComboBoxSampleAuswahl.SelectedIndex];
            textBox3.Text = hScrollBarSamplelength.Value.ToString("X");
            textBoxSamplelengthDec.Text = hScrollBarSamplelength.Value.ToString();

            // default note when playback pressed
            ComboBoxNotes.SelectedIndex = 0; // C
            updatetotalsize();
        }


        // draw border
        private void Form1_Shown(object sender, EventArgs e)
        {
            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(colLines);
            System.Drawing.Graphics formGraphics;
            formGraphics = this.CreateGraphics();
            formGraphics.FillRectangle(myBrush, new Rectangle(0, 0, 5, 800));  //672*834
            formGraphics.FillRectangle(myBrush, new Rectangle(650, 0, 672, 800));

            formGraphics.FillRectangle(myBrush, new Rectangle(0, 756, 672, 800));
            myBrush.Dispose();
            formGraphics.Dispose();
        }

        private void Form1_Move(object sender, EventArgs e)
        {
            Refresh();
        }

        // draw border
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(colLines);
            System.Drawing.Graphics formGraphics;
            formGraphics = this.CreateGraphics();
            formGraphics.FillRectangle(myBrush, new Rectangle(0, 0, 5, 800));  //672*834
            formGraphics.FillRectangle(myBrush, new Rectangle(650, 0, 672, 800));

            formGraphics.FillRectangle(myBrush, new Rectangle(0, 756, 672, 800));
            myBrush.Dispose();
            formGraphics.Dispose();
        }



        // check how many instruments are used
        int getnumberofinstruments()
        {
            int temp = 0;
            for (int i = 0; i < 31; i++)
            {
                if (samplelength[i] > 2) temp++;
            }
            return temp;
        }

        // check highest instrument number
        int getnumberofhighestinstrument()
        {
            int temp = 0;
            for (int i = 0; i < 31; i++)
            {
                if (samplelength[i] > 2) temp = i;
            }
            temp++;
            return temp;
        }

        // calculate the samplerate
        void calcsamplerate()
        {
            int temp = 0;
            switch (ComboBoxNotes.SelectedIndex)
            {
                case 0: temp = 856; break;
                case 1: temp = 808; break;
                case 2: temp = 762; break;
                case 3: temp = 720; break;
                case 4: temp = 678; break;
                case 5: temp = 640; break;
                case 6: temp = 604; break;
                case 7: temp = 570; break;
                case 8: temp = 538; break;
                case 9: temp = 508; break;
                case 10: temp = 480; break;
                case 11: temp = 453; break;
            }
            samplerate = (int)(7093789.2 / (float)temp * 2);

            if (radioButton1.Checked == true) samplerate = samplerate / 4;
            if (radioButton2.Checked == true) samplerate = samplerate / 2;
            if (radioButton4.Checked == true) samplerate = samplerate;
        }

        // function generate
        public byte generate(int instr, int smp)
        {
            short stemp1 = 0;
            short stemp2 = 0;
            byte btemp1 = 0;
            byte btemp2 = 0;
            byte btemp3 = 0;
            byte btemp4 = 0;

            for (int i = 0; i < 16; i++) // number of colums
            {
                switch (arrayfunction[instr, i])
                {
                    case 0: break;
                    case 1: // vol
                        {
                            stemp1 = variable[arrayval1[instr, i]];
                            btemp1 = arraygainval[instr, i];
                            if (arraygain[instr, i] > 0) btemp1 = (byte)variable[arraygain[instr, i]];
                            variable[arrayvar[instr, i]] = vol(stemp1, btemp1);
                            break;
                        }
                    case 2: // osc_saw
                        {
                            stemp1 = arrayfrequencyval[instr, i];
                            if (arrayfrequency[instr, i] > 0) stemp1 = variable[arrayfrequency[instr, i]];
                            btemp1 = arraygainval[instr, i];
                            if (arraygain[instr, i] > 0) btemp1 = (byte)variable[arraygain[instr, i]];
                            variable[arrayvar[instr, i]] = osc_saw((byte)i, stemp1, btemp1);
                            break;
                        }
                    case 3: // osc_tri
                        {
                            stemp1 = arrayfrequencyval[instr, i];
                            if (arrayfrequency[instr, i] > 0) stemp1 = variable[arrayfrequency[instr, i]];
                            btemp1 = arraygainval[instr, i];
                            if (arraygain[instr, i] > 0) btemp1 = (byte)variable[arraygain[instr, i]];
                            variable[arrayvar[instr, i]] = osc_tri((byte)i, stemp1, btemp1);
                            break;
                        }
                    case 4: // osc_sine
                        {
                            stemp1 = arrayfrequencyval[instr, i];
                            if (arrayfrequency[instr, i] > 0) stemp1 = variable[arrayfrequency[instr, i]];
                            btemp1 = arraygainval[instr, i];
                            if (arraygain[instr, i] > 0) btemp1 = (byte)variable[arraygain[instr, i]];
                            variable[arrayvar[instr, i]] = osc_sine((byte)i, stemp1, btemp1);
                            break;
                        }
                    case 5: // osc_pulse
                        {
                            stemp1 = arrayfrequencyval[instr, i];
                            if (arrayfrequency[instr, i] > 0) stemp1 = variable[arrayfrequency[instr, i]];
                            btemp1 = arraygainval[instr, i];
                            if (arraygain[instr, i] > 0) btemp1 = (byte)variable[arraygain[instr, i]];
                            btemp2 = arraywidthval[instr, i];
                            if (arraywidth[instr, i] > 0) btemp2 = (byte)variable[arraywidth[instr, i]];
                            variable[arrayvar[instr, i]] = osc_pulse((byte)i, stemp1, btemp1, btemp2);
                            break;
                        }
                    case 6: // osc_noise
                        {
                            btemp1 = arraygainval[instr, i];
                            if (arraygain[instr, i] > 0) btemp1 = (byte)variable[arraygain[instr, i]];
                            //btemp3 = arraywidthval[instr, i];//seed
                            variable[arrayvar[instr, i]] = osc_noise(smp, btemp1);
                            break;
                        }
                    case 7: // enva
                        {
                            btemp1 = arraygainval[instr, i];
                            if (arraygain[instr, i] > 0) btemp1 = (byte)variable[arraygain[instr, i]];
                            btemp2 = (byte)arrayval1value[instr, i];
                            variable[arrayvar[instr, i]] = enva(smp, btemp2, 0, btemp1);
                            break;
                        }
                    case 8: // envd
                        {
                            btemp1 = arraygainval[instr, i];
                            if (arraygain[instr, i] > 0) btemp1 = (byte)variable[arraygain[instr, i]];
                            btemp2 = (byte)arrayval1value[instr, i];//decay
                            btemp3 = (byte)arrayval2value[instr, i];//sustain
                            variable[arrayvar[instr, i]] = envd(smp, btemp2, btemp3, btemp1);
                            break;
                        }
                    case 9: // add
                        {
                            stemp1 = variable[arrayval1[instr, i]];
                            stemp2 = arrayval2value[instr, i];
                            if (arrayval2[instr, i] > 0) stemp2 = variable[arrayval2[instr, i]];
                            variable[arrayvar[instr, i]] = add(stemp1, stemp2);
                            break;
                        }
                    case 10: // mul
                        {
                            stemp1 = variable[arrayval1[instr, i]];
                            stemp2 = arrayval2value[instr, i];
                            if (arrayval2[instr, i] > 0) stemp2 = variable[arrayval2[instr, i]];
                            variable[arrayvar[instr, i]] = mul(stemp1, stemp2);
                            break;
                        }
                    case 11: // delay
                        {
                            stemp1 = variable[arrayval1[instr, i]]; //value
                            stemp2 = arrayfrequencyval[instr, i]; //delay 
                            if (arrayfrequency[instr, i] > 0) stemp2 = variable[arrayfrequency[instr, i]];
                            btemp1 = arraygainval[instr, i]; //gain
                            if (arraygain[instr, i] > 0) btemp1 = (byte)variable[arraygain[instr, i]];
                            variable[arrayvar[instr, i]] = dly_cyc((byte)i, stemp1, stemp2, btemp1);
                            break;
                        }
                    case 12: // comb filter
                        {
                            stemp1 = variable[arrayval1[instr, i]]; //value
                            stemp2 = arrayfrequencyval[instr, i]; //delay 
                            if (arrayfrequency[instr, i] > 0) stemp2 = variable[arrayfrequency[instr, i]];
                            btemp2 = (byte)arrayval2value[instr, i]; //feedback
                            if (arrayval2[instr, i] > 0) btemp2 = (byte)variable[arrayval2[instr, i]];
                            btemp1 = arraygainval[instr, i]; //gain
                            if (arraygain[instr, i] > 0) btemp1 = (byte)variable[arraygain[instr, i]];
                            variable[arrayvar[instr, i]] = cmb_flt_n((byte)i, stemp1, stemp2, btemp2, btemp1);
                            break;
                        }
                    case 13: // reverb
                        {
                            stemp1 = variable[arrayval1[instr, i]]; //value
                            btemp2 = (byte)arrayval2value[instr, i]; //feedback
                            if (arrayval2[instr, i] > 0) btemp2 = (byte)variable[arrayval2[instr, i]];
                            btemp1 = arraygainval[instr, i]; //gain
                            if (arraygain[instr, i] > 0) btemp1 = (byte)variable[arraygain[instr, i]];
                            variable[arrayvar[instr, i]] = reverb(stemp1, btemp2, btemp1);
                            break;
                        }
                    case 14: // ctrl
                        {
                            stemp1 = variable[arrayval1[instr, i]];
                            variable[arrayvar[instr, i]] = ctrl(stemp1);
                            break;
                        }
                    case 15: // sv filter
                        {
                            stemp1 = variable[arrayval1[instr, i]]; //value
                            stemp2 = arrayfrequencyval[instr, i]; //cutoff
                            if (arrayfrequency[instr, i] > 0) stemp2 = variable[arrayfrequency[instr, i]];
                            btemp2 = (byte)arrayval2value[instr, i]; //resonance
                            if (arrayval2[instr, i] > 0) btemp2 = (byte)variable[arrayval2[instr, i]];
                            btemp1 = arraygain[instr, i];//mode   
                            variable[arrayvar[instr, i]] = sv_flt_n((byte)i, stemp1, stemp2, btemp2, btemp1);
                            break;
                        }
                    case 16: // clamp distort
                        {
                            stemp1 = variable[arrayval1[instr, i]];
                            btemp1 = arraygainval[instr, i];
                            if (arraygain[instr, i] > 0) btemp1 = (byte)variable[arraygain[instr, i]];
                            variable[arrayvar[instr, i]] = distort(stemp1, btemp1);
                            break;

                        }
                       
                    case 17: // clone sample another try without new fuction. Works.
                        {
                            uint length = (uint)samplelength[arraygain[instr, i]];

                            int transpose = arrayfrequencyval[instr, i];
                            if (arrayfrequency[instr, i] > 0) transpose = variable[arrayfrequency[instr, i]]; //get transpose value
                            transpose += 32768; //add to shift to normal

                            uint pointer = (uint)(smp * transpose) >> 15;

                            ushort offset = (ushort)arrayval2value[instr, i];

                            if ((pointer + offset) < length)
                            {
                                if (arraygainval[instr, i] == 0) // forward
                                {
                                    variable[arrayvar[instr, i]] = (short)(sample[pointer + offset, arraygain[instr, i]] << 8);
                                }
                                if (arraygainval[instr, i] == 1) // reverse
                                {
                                    variable[arrayvar[instr, i]] = (short)(sample[length - pointer - offset, arraygain[instr, i]] << 8);
                                }
                            }
                            else variable[arrayvar[instr, i]] = 0;
                            break;
                        }
                    /*
                                        case 17: // clone sample (without function in Amigaklang.cs)
                                            {
                                              int stempo = arrayfrequencyval[instr, i];

                                                if (arrayfrequency[instr, i] > 0) stempo = variable[arrayfrequency[instr, i]]; //get transpose value
                                                stempo += 32768; //add to shift to normal
                                                int stemp = (smp * (stempo))>>15;


                                                if (arraygainval[instr, i] == 0)
                                                {
                                                    int temp = arrayval2value[instr, i];
                                                    variable[arrayvar[instr, i]] = (short)(sample[stemp+ temp, arraygain[instr, i]] << 8);
                                                }
                                                if (arraygainval[instr, i] == 1) //reverse
                                                {
                                                    int temp = samplelength[arraygain[instr, i]] - (stemp + (int)arrayval2value[instr, i]);
                                                    if (temp < 0) temp = 0;
                                                    variable[arrayvar[instr, i]] = (short)(sample[temp, arraygain[instr, i]] << 8);
                                                }
                                                break;
                                            }

                    */
                    /*
                                        case 17: // clone sample (old)
                                            {
                                                if (arraygainval[instr, i] == 0)
                                                {
                                                    variable[arrayvar[instr, i]] = (short)(sample[smp, arraygain[instr, i]] << 8);
                                                }
                                                if (arraygainval[instr, i] == 1) //reverse
                                                {
                                                    int temp = samplelength[arraygain[instr, i]] - smp;
                                                    if (temp < 0) temp = 0;
                                                    variable[arrayvar[instr, i]] = (short)(sample[temp, arraygain[instr, i]] << 8);
                                                }
                                                break;
                                            }
                    */

                    case 18: // chord generator
                        {

                            btemp1 = (byte)arrayfrequency[instr, i]; //note1
                            btemp2 = (byte)arraywidth[instr, i]; //note2
                            btemp3 = (byte)arrayval1[instr, i]; //note3
                            btemp4 = (byte)arrayval2value[instr, i];
                            if (arrayval2[instr, i] > 0) btemp4 = (byte)variable[arrayval2[instr, i]];
                            variable[arrayvar[instr, i]] = chordgen(smp, arraygain[instr, i], sample, btemp1, btemp2, btemp3, btemp4);
                            break;
                        }
                    case 19: // sh
                        {
                            stemp1 = variable[arrayval1[instr, i]];
                            btemp1 = arraygainval[instr, i];
                            if (arraygain[instr, i] > 0) btemp1 = (byte)variable[arraygain[instr, i]];
                            variable[arrayvar[instr, i]] = sh((byte)i, stemp1, btemp1);
                            break;
                        }
                    case 20: // imported sample
                        {
                            if (importedlength[arraygain[instr, i]] >= smp) // check length
                                variable[arrayvar[instr, i]] = (short)(importedsample[smp, arraygain[instr, i]] << 8);
                            else variable[arrayvar[instr, i]] = 0;
                            break;
                        }

                    case 21: // one pole filter
                        {
                            stemp1 = variable[arrayval1[instr, i]]; //value
                            btemp2 = (byte)arrayfrequencyval[instr, i]; //cutoff
                            if (arrayfrequency[instr, i] > 0) btemp2 = (byte)variable[arrayfrequency[instr, i]];
                            btemp1 = arraygain[instr, i];//mode   
                            variable[arrayvar[instr, i]] = onepole_flt((byte)i, stemp1, btemp2, btemp1);
                            break;
                        }
                    case 23: // envelope
                        {
                            // OPERATOR IN THE GUI HAS:
                            int attackLength= (arrayval2value[instr, i]<<8)+1;              // number of samples attack happens over (can't be zero)
                            int decayLength= (arrayval1value[instr, i]<<8)+1;               // number of samples decay happens over (can't be zero)
                            int releaseLength = (arrayfrequencyval[instr, i] << 8) + 1;     // number of samples release happens over (can't be zero)
                            int sustainLength = samplelength[instr] - attackLength - decayLength - releaseLength;//arrayval2value[instr, i] << 8;              // number of samples sustain happens over
                            byte gain = (arraygainval[instr, i] );                          // gain value
                            short sustainValue= (short)(arraywidthval[instr, i]<<8);        // sustain value
                            // from these operator values we calculate the following values.We need full divide here, but we only calculate these in the GUI,
                            // not on the amiga : )
                            int peak = (32767 * gain) << 1;
                            int sustainLevel = (sustainValue * gain) << 1;
                            int attackAmount = peak / attackLength;
                            int decayAmount = (peak - sustainLevel) / decayLength;
                            int releaseAmount = sustainLevel / releaseLength;

                            variable[arrayvar[instr, i]] = adsr((byte)i, attackAmount, decayAmount, sustainLevel, sustainLength, releaseAmount, peak);
                            break;
                        }
                        
                    case 24: // vocoder
                        {
                            stemp1 = variable[arrayval1[instr, i]];
                            stemp2 = variable[arrayval2[instr, i]];
                            variable[arrayvar[instr, i]] = vocoder(stemp1, stemp2, Convert.ToInt16(Band1Cut.Text), Convert.ToInt16(Band1Reso.Text), Convert.ToInt16(Band2Cut.Text), Convert.ToInt16(Band2Reso.Text),
                            Convert.ToInt16(Band3Cut.Text), Convert.ToInt16(Band3Reso.Text), Convert.ToInt16(Band4Cut.Text), Convert.ToInt16(Band4Reso.Text), Convert.ToInt16(Band5Cut.Text), Convert.ToInt16(Band5Reso.Text));
                            break;
                        }
                        
                }
                visu[smp,instr,i]= (byte)(variable[arrayvar[instr, i]]>>8); // for visualisation of single columns
            }
            return (byte)(variable[1] >> 8); //v1 back (already 8bit)
        }

        public void checkloopparams()
        {
            for (int i = 0; i < 31; i++)
            {
                if (loopoffset[i] < samplelength[i] / 2) loopoffset[i] = (samplelength[i] / 2) - 1;
                looplength[i] = samplelength[i] - loopoffset[i];
                //   looplength[i] = 2;
                //   loopoffset[i] = 0;
            }
        }


        // function loop generator
        public void loopgen(int instr)
        {

            int smplen = samplelength[instr];
            int replen = looplength[instr]; // 15=last column
            int offs = loopoffset[instr] + 2;

            int rampup = 0;
            int rampdown = 32767 << 8;

            int delta = (32767 << 8) / replen;
            for (int smp = 0; smp < replen; smp++)
            {
                short a = (short)(rampup >> 8);
                short b = (short)(rampdown >> 8);
                //  exponential:
                //  short b = (short)(32767-mul(ax, ax));
                //  short a = (short)(32767-mul(bx, bx));
                v1 = (short)(b * (sbyte)sample[(offs + smp), instr] >> 7);          // rampdown sample
                v2 = (short)(a * (sbyte)sample[(offs - replen + smp), instr] >> 7);     // rampup sample
                v1 = add(v2, v1);
                sample[(offs + smp), instr] = (byte)(v1 >> 8);
                rampup += delta;
                rampdown -= delta;
            }
        }


        private void initarrays()
        {
            // initialize working variables
            v1 = 0;
            v2 = 0;
            v3 = 0;
            v4 = 0;
            // initialize static instances
            Array.Clear(counter_saw, 0, counter_saw.Length);
            Array.Clear(counter_tri, 0, counter_tri.Length);
            Array.Clear(counter_sine, 0, counter_sine.Length);
            Array.Clear(counter_pulse, 0, counter_pulse.Length);
            Array.Clear(counter_sh, 0, counter_sh.Length);
            Array.Clear(buffer_sh, 0, buffer_sh.Length);
            Array.Clear(buffercyc, 0, buffercyc.Length);
            Array.Clear(buffern, 0, buffern.Length);
            Array.Clear(lpf, 0, lpf.Length);
            Array.Clear(hpf, 0, hpf.Length);
            Array.Clear(bpf, 0, bpf.Length);
            Array.Clear(pole, 0, pole.Length);
            Array.Clear(variable, 0, variable.Length);
            Array.Clear(ADSR_Mode, 0, ADSR_Mode.Length);
            Array.Clear(ADSR_Value, 0, ADSR_Value.Length);
            Array.Clear(ADSR_SustainCounter, 0, ADSR_SustainCounter.Length);       
        }



        // button generate all samples
        private void buttonGenerateAll_Click(object sender, EventArgs e)
        {
            waveout.Stop();
            buttonStopSample.Visible = false;
            // reset random seeds
            g_x1 = 0x67452301;
            g_x2 = 0xEFCDAB89;
            g_x3 = 0;
            // clear all samples
         //   Array.Clear(sample, 0, sample.Length);

            for (int i = 0; i < 31; i++)
            {
                initarrays();
                // render instruments loop
                for (int smp = 0; smp < samplelength[i]; smp++)
                {
                    sample[smp, i] = generate(i, smp);
                }
                if (arrayfunction[i, 15] == 22) loopgen(i);
            }
            updateVisu(ComboBoxSampleAuswahl.SelectedIndex);
            pictureBox1.Invalidate();
        }

        // clear values of a slot (when function selection changed)
        void clearslot(int instrument, byte column)
        {
            byte j = (byte)instrument;
            byte i = column;

            arrayfrequency[j, i] = 0;
            arrayfrequencyval[j, i] = 0;
            arraygain[j, i] = 0;
            arraygainval[j, i] = 0;
            arraywidth[j, i] = 0;
            arraywidthval[j, i] = 0;
            arrayval1[j, i] = 0;
            arrayval1value[j, i] = 0;
            arrayval2[j, i] = 0;
            arrayval2value[j, i] = 0;
            if (i == 15)
            {
                loopoffset[j] = (samplelength[j] / 2) - 1;
                looplength[j] = samplelength[j] - loopoffset[j];
            }
        }


        // Sampleauswahl combobox changed
        private void ComboBoxSampleAuswahl_SelectedIndexChanged(object sender, EventArgs e)
        {
            // set last radiobutton for visu
            radioButtonOutput.Checked = true;

            // show instrument number
            labelInstrumentNr.Text = (ComboBoxSampleAuswahl.SelectedIndex + 1).ToString();

            // hide groupboxes
            hideGroupBoxes();

            // reset zoom
            buttonZoomReset_Click(sender, e);

            // safe selected index
            selectedindex = ComboBoxSampleAuswahl.SelectedIndex;
            selecteditem = ComboBoxSampleAuswahl.SelectedItem.ToString();

            // set text for sample length
            if (ComboBoxSampleAuswahl.SelectedIndex >= 0 || ComboBoxSampleAuswahl.SelectedIndex <= 0x20)
            {
                hScrollBarSamplelength.Value = samplelength[ComboBoxSampleAuswahl.SelectedIndex];
                textBox3.Text = hScrollBarSamplelength.Value.ToString("X");
                textBoxSamplelengthDec.Text = hScrollBarSamplelength.Value.ToString();
            }

            // set comboboxes var from array
            ComboBoxVar1.SelectedIndex = arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 0];
            ComboBoxVar2.SelectedIndex = arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 1];
            ComboBoxVar3.SelectedIndex = arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 2];
            ComboBoxVar4.SelectedIndex = arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 3];
            ComboBoxVar5.SelectedIndex = arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 4];
            ComboBoxVar6.SelectedIndex = arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 5];
            ComboBoxVar7.SelectedIndex = arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 6];
            ComboBoxVar8.SelectedIndex = arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 7];
            ComboBoxVar9.SelectedIndex = arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 8];
            ComboBoxVar10.SelectedIndex = arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 9];
            ComboBoxVar11.SelectedIndex = arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 10];
            ComboBoxVar12.SelectedIndex = arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 11];
            ComboBoxVar13.SelectedIndex = arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 12];
            ComboBoxVar14.SelectedIndex = arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 13];
            ComboBoxVar15.SelectedIndex = arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 14];
            ComboBoxVar16.SelectedIndex = arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 15];

            ComboBoxFunction1.SelectedIndex = arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 0];
            ComboBoxFunction2.SelectedIndex = arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 1];
            ComboBoxFunction3.SelectedIndex = arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 2];
            ComboBoxFunction4.SelectedIndex = arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 3];
            ComboBoxFunction5.SelectedIndex = arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 4];
            ComboBoxFunction6.SelectedIndex = arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 5];
            ComboBoxFunction7.SelectedIndex = arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 6];
            ComboBoxFunction8.SelectedIndex = arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 7];
            ComboBoxFunction9.SelectedIndex = arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 8];
            ComboBoxFunction10.SelectedIndex = arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 9];
            ComboBoxFunction11.SelectedIndex = arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 10];
            ComboBoxFunction12.SelectedIndex = arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 11];
            ComboBoxFunction13.SelectedIndex = arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 12];
            ComboBoxFunction14.SelectedIndex = arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 13];
            ComboBoxFunction15.SelectedIndex = arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 14];
            ComboBoxFunction16.SelectedIndex = arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 15];

            hScrollBarLoopOffsetValue.Minimum = ((samplelength[ComboBoxSampleAuswahl.SelectedIndex]) / 2) - 1;
            CheckError();
        }

        private void comboBoxSelectImport_SelectedIndexChanged(object sender, EventArgs e)
        {
            // safe selected index
            selectedindex2 = comboBoxSelectImport.SelectedIndex;
            selecteditem2 = comboBoxSelectImport.SelectedItem.ToString();

            // set text for imported length
            if (comboBoxSelectImport.SelectedIndex >= 0 || comboBoxSelectImport.SelectedIndex <= 0x20)
            {
                TextBoxImportSampleLength.Text = importedlength[comboBoxSelectImport.SelectedIndex].ToString("X");
                TextBoxImportSampleLengthDec.Text = importedlength[comboBoxSelectImport.SelectedIndex].ToString();
                int total = 0;
                for (int i = 0; i < 8; i++)
                {
                    total += importedlength[i];
                }
                TextBoxImportSampleLengthTotal.Text = total.ToString();
                if (total > 32767)
                {
                    TextBoxImportSampleLengthTotal.BackColor = TextBoxImportSampleLengthTotal.BackColor;
                    TextBoxImportSampleLengthTotal.ForeColor = Color.Red;
                }
                else
                {
                    TextBoxImportSampleLengthTotal.BackColor = TextBoxImportSampleLengthTotal.BackColor;
                    TextBoxImportSampleLengthTotal.ForeColor = Color.Black;
                }
            }
        }

        private void updateVisu(int instrument)
        {
            
            for (int i = 0; i < 65536; i++)
            {
                if (radioButtonColum1.Checked == true) visuakt[i] = visu[i, instrument, 0];
                if (radioButtonColum2.Checked == true) visuakt[i] = visu[i, instrument, 1];
                if (radioButtonColum3.Checked == true) visuakt[i] = visu[i, instrument, 2];
                if (radioButtonColum4.Checked == true) visuakt[i] = visu[i, instrument, 3];
                if (radioButtonColum5.Checked == true) visuakt[i] = visu[i, instrument, 4];
                if (radioButtonColum6.Checked == true) visuakt[i] = visu[i, instrument, 5];
                if (radioButtonColum7.Checked == true) visuakt[i] = visu[i, instrument, 6];
                if (radioButtonColum8.Checked == true) visuakt[i] = visu[i, instrument, 7];
                if (radioButtonColum9.Checked == true) visuakt[i] = visu[i, instrument, 8];
                if (radioButtonColum10.Checked == true) visuakt[i] = visu[i, instrument, 9];
                if (radioButtonColum11.Checked == true) visuakt[i] = visu[i, instrument, 10];
                if (radioButtonColum12.Checked == true) visuakt[i] = visu[i, instrument, 11];
                if (radioButtonColum13.Checked == true) visuakt[i] = visu[i, instrument, 12];
                if (radioButtonColum14.Checked == true) visuakt[i] = visu[i, instrument, 13];
                if (radioButtonColum15.Checked == true) visuakt[i] = visu[i, instrument, 14];
                if (radioButtonColum16.Checked == true) visuakt[i] = visu[i, instrument, 15];
                if (radioButtonOutput.Checked == true) visuakt[i] = sample[i, instrument];
            }
            pictureBox1.Invalidate();
        }


        private void updatetotalsize()
        {
            int totalsmp = 0;
            for (int i = 0; i < 31; i++)
            {
                totalsmp += samplelength[i];
            }
            labelTotalSize.Text = (totalsmp).ToString();
        }


        private void updateloopbars()
        {
            // update Loop bars
            // set max length for loop repeat and offset bars
            int temp = samplelength[selectedindex] - 2;
            if (temp < 0) temp = 0;
            hScrollBarLoopOffsetValue.Maximum = temp;

            if (hScrollBarLoopOffsetValue.Value < (samplelength[selectedindex]) / 2)
                hScrollBarLoopOffsetValue.Value = ((samplelength[selectedindex]) / 2) - 1;
            // only even values
            if (hScrollBarLoopOffsetValue.Value % 2 == 1) hScrollBarLoopOffsetValue.Value += 1;

            TextBoxLoopOffsetValue.Text = hScrollBarLoopOffsetValue.Value.ToString("X");
            TextBoxLoopLengthValue.Text = (samplelength[ComboBoxSampleAuswahl.SelectedIndex] - hScrollBarLoopOffsetValue.Value).ToString("X");
            loopoffset[ComboBoxSampleAuswahl.SelectedIndex] = (short)hScrollBarLoopOffsetValue.Value;
            looplength[ComboBoxSampleAuswahl.SelectedIndex] = (short)(samplelength[ComboBoxSampleAuswahl.SelectedIndex] - hScrollBarLoopOffsetValue.Value);

            hScrollBarLoopOffsetValue.Minimum = ((samplelength[selectedindex]) / 2) - 1;

            updatetotalsize();

            //update clone sample offset
            if (temp < 0) temp = 0;
            hScrollBarCloneOffsetValue.Maximum = temp;
            if (hScrollBarCloneOffsetValue.Value > temp) hScrollBarCloneOffsetValue.Value = temp;
            hScrollBarCloneOffsetValue.PerformLayout(); //redraw
            TextBoxCloneOffsetValue.Text = Convert.ToString(hScrollBarCloneOffsetValue.Value);
        }


        // set array from samplelength scrollbar
        private void hScrollBarSamplelength_Scroll(object sender, ScrollEventArgs e)
        {
            if (hScrollBarSamplelength.Value % 2 == 1) hScrollBarSamplelength.Value += 1;
            if (hScrollBarSamplelength.Value < 2) hScrollBarSamplelength.Value = 2;
            samplelength[ComboBoxSampleAuswahl.SelectedIndex] = hScrollBarSamplelength.Value;
            textBox3.Text = hScrollBarSamplelength.Value.ToString("X");
            textBoxSamplelengthDec.Text = hScrollBarSamplelength.Value.ToString();
            updateloopbars();
        }




        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(colLines, 1);
            Pen looppen = new Pen(Color.Black, 2);
            SolidBrush loopbrush = new SolidBrush(Color.FromArgb(128, 128, 128, 128));

            Point looppoint1 = new Point(0, 0);
            Point looppoint2 = new Point(0, 0);
            Point pt1 = new Point(0, 0);
            Point pt2 = new Point(0, 0);
            Point pt2old = new Point(0, 0);
            Pen linepen = new Pen(Color.FromArgb(128, 128, 128), 2);
            Point linepoint1 = new Point(0, 64);
            Point linepoint2 = new Point(601, 64);

            if (ComboBoxSampleAuswahl.SelectedIndex >= 0 && ComboBoxSampleAuswahl.SelectedIndex <= 0x1f)
            {
                e.Graphics.Clear(colSamplebox);
                e.Graphics.DrawLine(linepen, linepoint1, linepoint2);
                int step, temp;
                float temp2, faktor;
                // draw sample
                for (int smp = 0; smp < 600; smp++)
                {
                    pt1.X = smp;
                    pt1.Y = 64;
                    pt2.X = smp;
                    step = ((samplelength[ComboBoxSampleAuswahl.SelectedIndex] * smp) / hScrollBarZoomZoom.Value) + hScrollBarZoomScroll.Value;
                    //temp = (((255 - sample[step, ComboBoxSampleAuswahl.SelectedIndex]) / 2) - 64);
                    temp = (((255 - visuakt[step]) / 2) - 64);
                    pt2old.Y = pt2.Y;
                    pt2.Y = temp;
                    if (temp < 0) pt2.Y = temp + 128;
                    if (checkBoxSampleview.Checked == true && smp > 1) pt1.Y = pt2old.Y - 1;
                    e.Graphics.DrawLine(pen, pt1, pt2);
                }

                // draw looplines
                if (arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 15] == 22 && radioButtonOutput.Checked == true)
                {
                    temp2 = (float)samplelength[ComboBoxSampleAuswahl.SelectedIndex] / hScrollBarZoomZoom.Value;
                    faktor = hScrollBarZoomScroll.Value / temp2;
                    looppoint1.X = (int)((float)(loopoffset[ComboBoxSampleAuswahl.SelectedIndex] / temp2) - faktor);
                    looppoint2.X = looppoint1.X;
                    looppoint1.Y = 0; //constant
                    looppoint2.Y = 128; //constant
                    e.Graphics.DrawLine(looppen, looppoint1, looppoint2);
                    e.Graphics.FillRectangle(loopbrush, looppoint1.X, looppoint1.Y, 600 - looppoint1.X, 128);
                    looppoint1.X = (int)((float)(((loopoffset[ComboBoxSampleAuswahl.SelectedIndex] + looplength[ComboBoxSampleAuswahl.SelectedIndex]) / temp2) - faktor) + 0.5);
                    looppoint2.X = looppoint1.X;
                    e.Graphics.DrawLine(looppen, looppoint1, looppoint2);
                }
            }
        }


        // Scrollbar update Showwaveform
        private void hScrollBarZoomZoom_Scroll(object sender, ScrollEventArgs e)
        {
            hScrollBarZoomScroll.Maximum = ((samplelength[ComboBoxSampleAuswahl.SelectedIndex]) * (hScrollBarZoomZoom.Value - 600) / hScrollBarZoomZoom.Value);
            pictureBox1.Invalidate();
        }
        // Scrollbar update Showwaveform
        private void hScrollBarZoomScroll_Scroll(object sender, ScrollEventArgs e)
        {
            pictureBox1.Invalidate();
        }

        // checkbox sample view style Showwaveform
        private void checkBoxSampleview_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }

        private void buttonZoomReset_Click(object sender, EventArgs e)
        {
            hScrollBarZoomScroll.Value = 0;
            hScrollBarZoomZoom.Value = 600;
            hScrollBarZoomScroll.Maximum = ((samplelength[ComboBoxSampleAuswahl.SelectedIndex]) * (hScrollBarZoomZoom.Value - 600) / hScrollBarZoomZoom.Value);
            buttonGenerateAll_Click(sender, e); // generate selected sample
        }

        private void hideGroupBoxes()
        {
            GroupBoxVol.Visible = false;        // 1
            GroupBoxOsc_saw.Visible = false;    // 2
            GroupBoxOsc_tri.Visible = false;    // 3
            GroupBoxOsc_sine.Visible = false;   // 4
            GroupBoxOsc_pulse.Visible = false;  // 5
            GroupBoxOsc_noise.Visible = false;  // 6
            GroupBoxEnva.Visible = false;       // 7
            GroupBoxEnvd.Visible = false;       // 8
            GroupBoxAdd.Visible = false;        // 9
            GroupBoxMul.Visible = false;        // 10
            GroupBoxDelay.Visible = false;      // 11
            GroupBoxComb.Visible = false;       // 12
            GroupBoxReverb.Visible = false;     // 13
            GroupBoxCtrl.Visible = false;       // 14
            GroupBoxFilter.Visible = false;     // 15
            GroupBoxDistortion.Visible = false; // 16
            GroupBoxClone.Visible = false;      // 17
            GroupBoxChord.Visible = false;      // 18
            GroupBoxSh.Visible = false;         // 19
            GroupBoxImport.Visible = false;     // 20
            GroupBoxOnepole.Visible = false;    // 21
            GroupBoxLoop.Visible = false;       // 22
            GroupBoxEnvelope.Visible = false;   // 23
            GroupBoxVocoder.Visible = false;    // 24    

            update_visibility();
        }

        private void selectGroupBox(int index, int colum)
        {
            switch (index)
            {
                case 0: break;
                case 1: // volume
                    {
                        GroupBoxVol.Visible = true;
                        ComboBoxVolVal.SelectedIndex = arrayval1[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        ComboBoxVolGain.SelectedIndex = arraygain[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        TextBoxVolGainValue.Text = Convert.ToString(arraygainval[ComboBoxSampleAuswahl.SelectedIndex, colum]);
                        hScrollBarVolGainValue.Value = arraygainval[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        break;
                    }
                case 2: // osc_saw
                    {
                        GroupBoxOsc_saw.Visible = true;
                        ComboBoxOscsawFreq.SelectedIndex = arrayfrequency[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        ComboBoxOscsawGain.SelectedIndex = arraygain[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        TextBoxOscsawFreqValue.Text = Convert.ToString(arrayfrequencyval[ComboBoxSampleAuswahl.SelectedIndex, colum]);
                        hScrollBarOscsawFreqValue.Value = arrayfrequencyval[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        TextBoxOscsawGainValue.Text = Convert.ToString(arraygainval[ComboBoxSampleAuswahl.SelectedIndex, colum]);
                        hScrollBarOscsawGainValue.Value = arraygainval[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        break;
                    }
                case 3: // osc_tri
                    {
                        GroupBoxOsc_tri.Visible = true;
                        ComboBoxOsctriFreq.SelectedIndex = arrayfrequency[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        ComboBoxOsctriGain.SelectedIndex = arraygain[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        TextBoxOsctriFreqValue.Text = Convert.ToString(arrayfrequencyval[ComboBoxSampleAuswahl.SelectedIndex, colum]);
                        hScrollBarOsctriFreqValue.Value = arrayfrequencyval[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        TextBoxOsctriGainValue.Text = Convert.ToString(arraygainval[ComboBoxSampleAuswahl.SelectedIndex, colum]);
                        hScrollBarOsctriGainValue.Value = arraygainval[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        break;
                    }
                case 4: // osc_sine
                    {
                        GroupBoxOsc_sine.Visible = true;
                        ComboBoxOscsineFreq.SelectedIndex = arrayfrequency[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        ComboBoxOscsineGain.SelectedIndex = arraygain[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        TextBoxOscsineFreqValue.Text = Convert.ToString(arrayfrequencyval[ComboBoxSampleAuswahl.SelectedIndex, colum]);
                        hScrollBarOscsineFreqValue.Value = arrayfrequencyval[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        TextBoxOscsineGainValue.Text = Convert.ToString(arraygainval[ComboBoxSampleAuswahl.SelectedIndex, colum]);
                        hScrollBarOscsineGainValue.Value = arraygainval[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        break;
                    }
                case 5: // osc_pulse
                    {
                        GroupBoxOsc_pulse.Visible = true;
                        ComboBoxOscpulseFreq.SelectedIndex = arrayfrequency[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        ComboBoxOscpulseGain.SelectedIndex = arraygain[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        ComboBoxOscpulseWidth.SelectedIndex = arraywidth[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        TextBoxOscpulseFreqValue.Text = Convert.ToString(arrayfrequencyval[ComboBoxSampleAuswahl.SelectedIndex, colum]);
                        hScrollBarOscpulseFreqValue.Value = arrayfrequencyval[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        TextBoxOscpulseGainValue.Text = Convert.ToString(arraygainval[ComboBoxSampleAuswahl.SelectedIndex, colum]);
                        hScrollBarOscpulseGainValue.Value = arraygainval[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        TextBoxOscpulseWidthValue.Text = Convert.ToString(arraywidthval[ComboBoxSampleAuswahl.SelectedIndex, colum]);
                        hScrollBarOscpulseWidthValue.Value = arraywidthval[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        break;
                    }
                case 6: // osc_noise
                    {
                        GroupBoxOsc_noise.Visible = true;
                        ComboBoxOscnoiseGain.SelectedIndex = arraygain[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        TextBoxOscnoiseGainValue.Text = Convert.ToString(arraygainval[ComboBoxSampleAuswahl.SelectedIndex, colum]);
                        hScrollBarOscnoiseGainValue.Value = arraygainval[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        break;
                    }
                case 7: // enva
                    {
                        GroupBoxEnva.Visible = true;
                        ComboBoxEnvaGain.SelectedIndex = arraygain[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        TextBoxEnvaAttackValue.Text = Convert.ToString(arrayval1value[ComboBoxSampleAuswahl.SelectedIndex, colum]);
                        hScrollBarEnvaAttackValue.Value = arrayval1value[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        TextBoxEnvaGainValue.Text = Convert.ToString(arraygainval[ComboBoxSampleAuswahl.SelectedIndex, colum]);
                        hScrollBarEnvaGainValue.Value = arraygainval[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        break;
                    }
                case 8: // envd
                    {
                        GroupBoxEnvd.Visible = true;
                        ComboBoxEnvdGain.SelectedIndex = arraygain[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        TextBoxEnvdDecayValue.Text = Convert.ToString(arrayval1value[ComboBoxSampleAuswahl.SelectedIndex, colum]);
                        hScrollBarEnvdDecayValue.Value = arrayval1value[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        if (arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, colum] > hScrollBarEnvdSustainValue.Maximum) arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, colum] = (short)hScrollBarEnvdSustainValue.Maximum;
                        TextBoxEnvdSustainValue.Text = Convert.ToString(arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, colum]);
                        hScrollBarEnvdSustainValue.Value = arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        TextBoxEnvdGainValue.Text = Convert.ToString(arraygainval[ComboBoxSampleAuswahl.SelectedIndex, colum]);
                        hScrollBarEnvdGainValue.Value = arraygainval[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        break;
                    }
                case 9: // add
                    {
                        GroupBoxAdd.Visible = true;
                        ComboBoxAddVal1.SelectedIndex = arrayval1[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        ComboBoxAddVal2.SelectedIndex = arrayval2[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        TextBoxAddVal2Value.Text = Convert.ToString(arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, colum]);
                        hScrollBarAddVal2Value.Value = arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        break;
                    }
                case 10: // mul
                    {
                        GroupBoxMul.Visible = true;
                        ComboBoxMulVal1.SelectedIndex = arrayval1[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        ComboBoxMulVal2.SelectedIndex = arrayval2[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        TextBoxMulVal2Value.Text = Convert.ToString(arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, colum]);
                        float tempfloat = (float)arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        tempfloat = (1.0f / 32767.0f) * tempfloat;
                        TextBoxMulVal2ValueFloat.Text = tempfloat.ToString("F4");
                        hScrollBarMulVal2Value.Value = arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        break;
                    }
                case 11: // delay
                    {
                        GroupBoxDelay.Visible = true;
                        ComboBoxDelayValue.SelectedIndex = arrayval1[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        ComboBoxDelayDelay.SelectedIndex = arrayfrequency[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        ComboBoxDelayGain.SelectedIndex = arraygain[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        if (arrayfrequencyval[ComboBoxSampleAuswahl.SelectedIndex, colum] > hScrollBarDelayDelayValue.Maximum) arrayfrequencyval[ComboBoxSampleAuswahl.SelectedIndex, colum] = (short)hScrollBarDelayDelayValue.Maximum;
                        TextBoxDelayDelayValue.Text = Convert.ToString(arrayfrequencyval[ComboBoxSampleAuswahl.SelectedIndex, colum]);
                        hScrollBarDelayDelayValue.Value = arrayfrequencyval[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        TextBoxDelayGainValue.Text = Convert.ToString(arraygainval[ComboBoxSampleAuswahl.SelectedIndex, colum]);
                        hScrollBarDelayGainValue.Value = arraygainval[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        break;
                    }
                case 12: // comb filter
                    {
                        GroupBoxComb.Visible = true;
                        ComboBoxCombValue.SelectedIndex = arrayval1[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        ComboBoxCombDelay.SelectedIndex = arrayfrequency[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        ComboBoxCombFeedback.SelectedIndex = arrayval2[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        ComboBoxCombGain.SelectedIndex = arraygain[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        if (arrayfrequencyval[ComboBoxSampleAuswahl.SelectedIndex, colum] > hScrollBarCombDelayValue.Maximum) arrayfrequencyval[ComboBoxSampleAuswahl.SelectedIndex, colum] = (short)hScrollBarCombDelayValue.Maximum;
                        TextBoxCombDelayValue.Text = Convert.ToString(arrayfrequencyval[ComboBoxSampleAuswahl.SelectedIndex, colum]);
                        hScrollBarCombDelayValue.Value = arrayfrequencyval[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        if (arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, colum] > hScrollBarCombFeedbackValue.Maximum) arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, colum] = (short)hScrollBarCombFeedbackValue.Maximum;
                        if (arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, colum] < hScrollBarCombFeedbackValue.Minimum) arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, colum] = (short)hScrollBarCombFeedbackValue.Minimum;
                        TextBoxCombFeedbackValue.Text = Convert.ToString(arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, colum]);
                        hScrollBarCombFeedbackValue.Value = arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        TextBoxCombGainValue.Text = Convert.ToString(arraygainval[ComboBoxSampleAuswahl.SelectedIndex, colum]);
                        hScrollBarCombGainValue.Value = arraygainval[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        break;
                    }
                case 13: // reverb
                    {
                        GroupBoxReverb.Visible = true;
                        ComboBoxReverbValue.SelectedIndex = arrayval1[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        ComboBoxReverbFeedback.SelectedIndex = arrayval2[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        ComboBoxReverbGain.SelectedIndex = arraygain[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        if (arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, colum] > hScrollBarReverbFeedbackValue.Maximum) arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, colum] = (short)hScrollBarReverbFeedbackValue.Maximum;
                        if (arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, colum] < hScrollBarReverbFeedbackValue.Minimum) arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, colum] = (short)hScrollBarReverbFeedbackValue.Minimum;
                        TextBoxReverbFeedbackValue.Text = Convert.ToString(arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, colum]);
                        hScrollBarReverbFeedbackValue.Value = arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        TextBoxReverbGainValue.Text = Convert.ToString(arraygainval[ComboBoxSampleAuswahl.SelectedIndex, colum]);
                        hScrollBarReverbGainValue.Value = arraygainval[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        break;
                    }
                case 14: // ctrl
                    {
                        GroupBoxCtrl.Visible = true;
                        ComboBoxCtrlValue.SelectedIndex = arrayval1[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        break;
                    }
                case 15: // sv filter
                    {
                        GroupBoxFilter.Visible = true;
                        ComboBoxFilterValue.SelectedIndex = arrayval1[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        ComboBoxFilterCutoff.SelectedIndex = arrayfrequency[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        ComboBoxFilterResonance.SelectedIndex = arrayval2[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        ComboBoxFilterMode.SelectedIndex = arraygain[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        if (arrayfrequencyval[ComboBoxSampleAuswahl.SelectedIndex, colum] > hScrollBarFilterCutoffValue.Maximum) arrayfrequencyval[ComboBoxSampleAuswahl.SelectedIndex, colum] = (short)hScrollBarFilterCutoffValue.Maximum;
                        TextBoxFilterCutoffValue.Text = Convert.ToString(arrayfrequencyval[ComboBoxSampleAuswahl.SelectedIndex, colum]);
                        hScrollBarFilterCutoffValue.Value = arrayfrequencyval[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        if (arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, colum] > hScrollBarFilterResonanceValue.Maximum) arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, colum] = (short)hScrollBarFilterResonanceValue.Maximum;
                        if (arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, colum] < hScrollBarFilterResonanceValue.Minimum) arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, colum] = (short)hScrollBarFilterResonanceValue.Minimum;
                        TextBoxFilterResonanceValue.Text = Convert.ToString(arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, colum]);
                        hScrollBarFilterResonanceValue.Value = arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        break;
                    }
                case 16: // distortion
                    {
                        GroupBoxDistortion.Visible = true;
                        ComboBoxDistortionVal.SelectedIndex = arrayval1[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        ComboBoxDistortionGain.SelectedIndex = arraygain[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        TextBoxDistortionGainValue.Text = Convert.ToString(arraygainval[ComboBoxSampleAuswahl.SelectedIndex, colum]);
                        hScrollBarDistortionGainValue.Value = arraygainval[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        break;
                    }
                case 17: // clone sample
                    {
                        if (selectedindex > 0)
                        {

                            GroupBoxClone.Visible = true;
                            ComboBoxCloneTranspose.SelectedIndex = arrayfrequency[ComboBoxSampleAuswahl.SelectedIndex, colum];

                            if (arraygain[ComboBoxSampleAuswahl.SelectedIndex, colum] <= ComboBoxCloneSamplenr.Items.Count) ComboBoxCloneSamplenr.SelectedIndex = arraygain[ComboBoxSampleAuswahl.SelectedIndex, colum];
                            ComboBoxCloneReverse.SelectedIndex = arraygainval[ComboBoxSampleAuswahl.SelectedIndex, colum];

                            int temp = samplelength[arraygain[ComboBoxSampleAuswahl.SelectedIndex, colum]] - 1;  // aus dem array
                            if (temp < 0) temp = 0;
                            hScrollBarCloneOffsetValue.Maximum = temp;
                            if (hScrollBarCloneOffsetValue.Value > temp)
                            {
                                hScrollBarCloneOffsetValue.Value = temp;
                                // arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, colum] = 0;
                            }
                            hScrollBarCloneOffsetValue.PerformLayout(); //redraw
                            TextBoxCloneOffsetValue.Text = Convert.ToString(hScrollBarCloneOffsetValue.Value);


                            TextBoxCloneOffsetValue.Text = Convert.ToString((ushort)arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, colum]);
                            if ((ushort)arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, colum] < hScrollBarCloneOffsetValue.Maximum)
                            {
                                hScrollBarCloneOffsetValue.Value = (ushort)arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, colum];
                            }
                            else
                            {
                                hScrollBarCloneOffsetValue.Value = temp;
                                arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, colum] = (short)temp;
                            }
                            // show selected sample name
                            labelCloneName.Text = ComboBoxSampleAuswahl.Items[ComboBoxCloneSamplenr.SelectedIndex].ToString();
                        }
                        TextBoxCloneTransposeValue.Text = Convert.ToString(arrayfrequencyval[ComboBoxSampleAuswahl.SelectedIndex, colum]);
                        hScrollBarCloneTransposeValue.Value = arrayfrequencyval[ComboBoxSampleAuswahl.SelectedIndex, colum];

                        break;
                    }
                case 18: // chord generator
                    {
                        if (selectedindex > 0)
                        {
                            GroupBoxChord.Visible = true;
                            if (arraygain[ComboBoxSampleAuswahl.SelectedIndex, colum] <= ComboBoxChordSamplenr.Items.Count) ComboBoxChordSamplenr.SelectedIndex = arraygain[ComboBoxSampleAuswahl.SelectedIndex, colum];

                            ComboBoxChordNote1.SelectedIndex = arrayfrequency[ComboBoxSampleAuswahl.SelectedIndex, colum];
                            ComboBoxChordNote2.SelectedIndex = arraywidth[ComboBoxSampleAuswahl.SelectedIndex, colum];
                            ComboBoxChordNote3.SelectedIndex = arrayval1[ComboBoxSampleAuswahl.SelectedIndex, colum];
                            ComboBoxChordShift.SelectedIndex = arrayval2[ComboBoxSampleAuswahl.SelectedIndex, colum];
                            if (arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, colum] > hScrollBarChordShiftValue.Maximum) arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, colum] = (short)hScrollBarChordShiftValue.Maximum;
                            if (arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, colum] < hScrollBarChordShiftValue.Minimum) arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, colum] = (short)hScrollBarChordShiftValue.Minimum;
                            TextBoxChordShiftValue.Text = Convert.ToString(arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, colum]);
                            hScrollBarChordShiftValue.Value = arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, colum];
                            // show selected sample name
                            labelChordName.Text = ComboBoxSampleAuswahl.Items[ComboBoxChordSamplenr.SelectedIndex].ToString();
                        }
                        break;
                    }
                case 19: // sample and hold
                    {
                        GroupBoxSh.Visible = true;
                        ComboBoxShVal.SelectedIndex = arrayval1[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        ComboBoxShStep.SelectedIndex = arraygain[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        TextBoxShStepValue.Text = Convert.ToString(arraygainval[ComboBoxSampleAuswahl.SelectedIndex, colum]);
                        hScrollBarShStepValue.Value = arraygainval[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        break;
                    }
                case 20: // import sample 
                    {
                        GroupBoxImport.Visible = true;
                        ComboBoxImportNumber.SelectedIndex = arraygain[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        // show selected sample name
                        labelImportedName.Text = comboBoxSelectImport.Items[ComboBoxImportNumber.SelectedIndex].ToString();
                        break;
                    }

                case 21: // 1-pole filter
                    {
                        GroupBoxOnepole.Visible = true;
                        ComboBoxOnepoleValue.SelectedIndex = arrayval1[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        ComboBoxOnepoleCutoff.SelectedIndex = arrayfrequency[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        ComboBoxOnepoleMode.SelectedIndex = arraygain[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        if (arrayfrequencyval[ComboBoxSampleAuswahl.SelectedIndex, colum] > hScrollBarOnepoleCutoffValue.Maximum) arrayfrequencyval[ComboBoxSampleAuswahl.SelectedIndex, colum] = (short)hScrollBarOnepoleCutoffValue.Maximum;
                        TextBoxOnepoleCutoffValue.Text = Convert.ToString(arrayfrequencyval[ComboBoxSampleAuswahl.SelectedIndex, colum]);
                        hScrollBarOnepoleCutoffValue.Value = arrayfrequencyval[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        break;
                    }

                case 22: // loop generator
                    {
                        //if (colum == 15) // only in the last colum
                        //{
                            GroupBoxLoop.Visible = true;

                            int temp = samplelength[selectedindex] - 2;
                            if (temp < 0) temp = 0;
                            hScrollBarLoopOffsetValue.Maximum = temp;

                            if (loopoffset[selectedindex] > hScrollBarLoopOffsetValue.Maximum) loopoffset[selectedindex] = hScrollBarLoopOffsetValue.Maximum;
                            if (loopoffset[selectedindex] < hScrollBarLoopOffsetValue.Minimum) loopoffset[selectedindex] = hScrollBarLoopOffsetValue.Minimum;
                            //TextBoxLoopOffsetValue.Text = Convert.ToString(loopoffset[ComboBoxSampleAuswahl.SelectedIndex, colum]);
                            hScrollBarLoopOffsetValue.Value = loopoffset[ComboBoxSampleAuswahl.SelectedIndex];
                            TextBoxLoopOffsetValue.Text = hScrollBarLoopOffsetValue.Value.ToString("X");
                            TextBoxLoopLengthValue.Text = (samplelength[ComboBoxSampleAuswahl.SelectedIndex] - hScrollBarLoopOffsetValue.Value).ToString("X");
                        //}
                        break;
                    }

                case 23: // envelope
                    {
                        GroupBoxEnvelope.Visible = true;
                        TextBoxEnvelopeAttackValue.Text = Convert.ToString(arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, colum]);
                        hScrollBarEnvelopeAttackValue.Value = arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, colum];

                        TextBoxEnvelopeDecayValue.Text = Convert.ToString(arrayval1value[ComboBoxSampleAuswahl.SelectedIndex, colum]);
                        hScrollBarEnvelopeDecayValue.Value = arrayval1value[ComboBoxSampleAuswahl.SelectedIndex, colum];

                        if (arraywidthval[ComboBoxSampleAuswahl.SelectedIndex, colum] > hScrollBarEnvelopeSustainValue.Maximum) arraywidthval[ComboBoxSampleAuswahl.SelectedIndex, colum] = (byte)hScrollBarEnvelopeSustainValue.Maximum;
                        TextBoxEnvelopeSustainValue.Text = Convert.ToString(arraywidthval[ComboBoxSampleAuswahl.SelectedIndex, colum]);
                        hScrollBarEnvelopeSustainValue.Value = arraywidthval[ComboBoxSampleAuswahl.SelectedIndex, colum];

                        if (arrayfrequencyval[ComboBoxSampleAuswahl.SelectedIndex, colum] > hScrollBarEnvelopeReleaseValue.Maximum) arrayfrequencyval[ComboBoxSampleAuswahl.SelectedIndex, colum] = (short)hScrollBarEnvelopeReleaseValue.Maximum;
                        TextBoxEnvelopeReleaseValue.Text = Convert.ToString(arrayfrequencyval[ComboBoxSampleAuswahl.SelectedIndex, colum]);
                        hScrollBarEnvelopeReleaseValue.Value = arrayfrequencyval[ComboBoxSampleAuswahl.SelectedIndex, colum];

                        TextBoxEnvelopeGainValue.Text = Convert.ToString(arraygainval[ComboBoxSampleAuswahl.SelectedIndex, colum]);
                        hScrollBarEnvelopeGainValue.Value = arraygainval[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        break;
                    }

                case 24: // vocoder
                    {
                        GroupBoxVocoder.Visible = true;
                        ComboBoxVocoderModulator.SelectedIndex = arrayval1[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        ComboBoxVocoderCarrier.SelectedIndex = arrayval2[ComboBoxSampleAuswahl.SelectedIndex, colum];
                        break;
                    }



            }
        }

        // trashcan
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure?", "Delete Instrument", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                int j = ComboBoxSampleAuswahl.SelectedIndex;
                samplelength[j] = 2;
                loopoffset[j] = 0;
                looplength[j] = 2;
                for (int i = 0; i < 20; i++)
                {
                    arrayvar[j, i] = 0;
                    arrayfunction[j, i] = 0;
                    arrayinstance[j, i] = 0;
                    arrayfrequency[j, i] = 0;
                    arrayfrequencyval[j, i] = 0;
                    arraygain[j, i] = 0;
                    arraygainval[j, i] = 0;
                    arraywidth[j, i] = 0;
                    arraywidthval[j, i] = 0;
                    arrayval1[j, i] = 0;
                    arrayval1value[j, i] = 0;
                    arrayval2[j, i] = 0;
                    arrayval2value[j, i] = 0;
                }
                string s = "Instrument_" + (ComboBoxSampleAuswahl.SelectedIndex + 1).ToString();
                if (!ComboBoxSampleAuswahl.Items.Contains(s))
                {
                    ComboBoxSampleAuswahl.Items.Remove(selecteditem);
                    ComboBoxSampleAuswahl.Items.Insert(selectedindex, s);
                    ComboBoxSampleAuswahl.SelectedIndex = selectedindex;
                }
                ComboBoxSampleAuswahl_SelectedIndexChanged(sender, e); // trigger sampleauswahl to update screen
                updatetotalsize();
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }

            // replace name in combobox

        }

        private void clearAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure?", "Delete all Instruments", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                for (int j = 0; j < 31; j++)
                {
                    ComboBoxSampleAuswahl.SelectedIndex = j;
                    string s = "Instrument_" + (ComboBoxSampleAuswahl.SelectedIndex + 1).ToString();
                    if (!ComboBoxSampleAuswahl.Items.Contains(s))
                    {
                        ComboBoxSampleAuswahl.Items.Remove(selecteditem);
                        ComboBoxSampleAuswahl.Items.Insert(selectedindex, s);
                        ComboBoxSampleAuswahl.SelectedIndex = selectedindex;
                    }

                    samplelength[j] = 2;
                    loopoffset[j] = 0;
                    looplength[j] = 2;
                    for (int i = 0; i < 20; i++)
                    {
                        arrayvar[j, i] = 0;
                        arrayfunction[j, i] = 0;
                        arrayinstance[j, i] = 0;
                        arrayfrequency[j, i] = 0;
                        arrayfrequencyval[j, i] = 0;
                        arraygain[j, i] = 0;
                        arraygainval[j, i] = 0;
                        arraywidth[j, i] = 0;
                        arraywidthval[j, i] = 0;
                        arrayval1[j, i] = 0;
                        arrayval1value[j, i] = 0;
                        arrayval2[j, i] = 0;
                        arrayval2value[j, i] = 0;
                    }
                }
                for (int j = 0; j < 8; j++)
                {
                    importedlength[j] = 0;
                    comboBoxSelectImport.SelectedIndex = j;
                    string s = "Sample_" + (comboBoxSelectImport.SelectedIndex + 1).ToString();
                    if (!comboBoxSelectImport.Items.Contains(s))
                    {
                        comboBoxSelectImport.Items.Remove(selecteditem2);
                        comboBoxSelectImport.Items.Insert(selectedindex2, s);
                        comboBoxSelectImport.SelectedIndex = selectedindex2;
                    }
                    comboBoxSelectImport_SelectedIndexChanged(sender, e);
                }
                //ComboBoxSampleAuswahl_SelectedIndexChanged(sender, e); // trigger sampleauswahl to update screen
                //comboBoxSelectImport_SelectedIndexChanged(sender, e); // trigger selectedindex to update screen
                ComboBoxSampleAuswahl.SelectedIndex = 0;
                comboBoxSelectImport.SelectedIndex = 0;
                updatetotalsize();
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }

        }


        // Button edit click
        private void ButtonEdit1_Click(object sender, EventArgs e)
        {
            hideGroupBoxes();
            selectGroupBox(ComboBoxFunction1.SelectedIndex, 0);
            update_visibility();
            edit = 0;
        }
        private void ButtonEdit2_Click(object sender, EventArgs e)
        {
            hideGroupBoxes();
            selectGroupBox(ComboBoxFunction2.SelectedIndex, 1);
            update_visibility();
            edit = 1;
        }
        private void ButtonEdit3_Click(object sender, EventArgs e)
        {
            hideGroupBoxes();
            selectGroupBox(ComboBoxFunction3.SelectedIndex, 2);
            update_visibility();
            edit = 2;
        }
        private void ButtonEdit4_Click(object sender, EventArgs e)
        {
            hideGroupBoxes();
            selectGroupBox(ComboBoxFunction4.SelectedIndex, 3);
            update_visibility();
            edit = 3;
        }
        private void ButtonEdit5_Click(object sender, EventArgs e)
        {
            hideGroupBoxes();
            selectGroupBox(ComboBoxFunction5.SelectedIndex, 4);
            update_visibility();
            edit = 4;
        }
        private void ButtonEdit6_Click(object sender, EventArgs e)
        {
            hideGroupBoxes();
            selectGroupBox(ComboBoxFunction6.SelectedIndex, 5);
            update_visibility();
            edit = 5;
        }
        private void ButtonEdit7_Click(object sender, EventArgs e)
        {
            hideGroupBoxes();
            selectGroupBox(ComboBoxFunction7.SelectedIndex, 6);
            update_visibility();
            edit = 6;
        }
        private void ButtonEdit8_Click(object sender, EventArgs e)
        {
            hideGroupBoxes();
            selectGroupBox(ComboBoxFunction8.SelectedIndex, 7);
            update_visibility();
            edit = 7;
        }
        private void ButtonEdit9_Click(object sender, EventArgs e)
        {
            hideGroupBoxes();
            selectGroupBox(ComboBoxFunction9.SelectedIndex, 8);
            update_visibility();
            edit = 8;
        }
        private void ButtonEdit10_Click(object sender, EventArgs e)
        {
            hideGroupBoxes();
            selectGroupBox(ComboBoxFunction10.SelectedIndex, 9);
            update_visibility();
            edit = 9;
        }
        private void ButtonEdit11_Click(object sender, EventArgs e)
        {
            hideGroupBoxes();
            selectGroupBox(ComboBoxFunction11.SelectedIndex, 10);
            update_visibility();
            edit = 10;
        }
        private void ButtonEdit12_Click(object sender, EventArgs e)
        {
            hideGroupBoxes();
            selectGroupBox(ComboBoxFunction12.SelectedIndex, 11);
            update_visibility();
            edit = 11;
        }
        private void ButtonEdit13_Click(object sender, EventArgs e)
        {
            hideGroupBoxes();
            selectGroupBox(ComboBoxFunction13.SelectedIndex, 12);
            update_visibility();
            edit = 12;
        }
        private void ButtonEdit14_Click(object sender, EventArgs e)
        {
            hideGroupBoxes();
            selectGroupBox(ComboBoxFunction14.SelectedIndex, 13);
            update_visibility();
            edit = 13;
        }
        private void ButtonEdit15_Click(object sender, EventArgs e)
        {
            hideGroupBoxes();
            selectGroupBox(ComboBoxFunction15.SelectedIndex, 14);
            update_visibility();
            edit = 14;
        }
        private void ButtonEdit16_Click(object sender, EventArgs e)
        {
            hideGroupBoxes();
            selectGroupBox(ComboBoxFunction16.SelectedIndex, 15);
            update_visibility();
            edit = 15;
        }

        // set arrayvar when selecting var
        private void ComboBoxVar1_SelectedIndexChanged(object sender, EventArgs e)
        {
            arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 0] = ComboBoxVar1.SelectedIndex;
            CheckError();
        }
        private void ComboBoxVar2_SelectedIndexChanged(object sender, EventArgs e)
        {
            arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 1] = ComboBoxVar2.SelectedIndex;
            CheckError();
        }
        private void ComboBoxVar3_SelectedIndexChanged(object sender, EventArgs e)
        {
            arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 2] = ComboBoxVar3.SelectedIndex;
            CheckError();
        }
        private void ComboBoxVar4_SelectedIndexChanged(object sender, EventArgs e)
        {
            arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 3] = ComboBoxVar4.SelectedIndex;
            CheckError();
        }
        private void ComboBoxVar5_SelectedIndexChanged(object sender, EventArgs e)
        {
            arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 4] = ComboBoxVar5.SelectedIndex;
            CheckError();
        }
        private void ComboBoxVar6_SelectedIndexChanged(object sender, EventArgs e)
        {
            arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 5] = ComboBoxVar6.SelectedIndex;
            CheckError();
        }
        private void ComboBoxVar7_SelectedIndexChanged(object sender, EventArgs e)
        {
            arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 6] = ComboBoxVar7.SelectedIndex;
            CheckError();
        }
        private void ComboBoxVar8_SelectedIndexChanged(object sender, EventArgs e)
        {
            arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 7] = ComboBoxVar8.SelectedIndex;
            CheckError();
        }
        private void ComboBoxVar9_SelectedIndexChanged(object sender, EventArgs e)
        {
            arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 8] = ComboBoxVar9.SelectedIndex;
            CheckError();
        }
        private void ComboBoxVar10_SelectedIndexChanged(object sender, EventArgs e)
        {
            arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 9] = ComboBoxVar10.SelectedIndex;
            CheckError();
        }
        private void ComboBoxVar11_SelectedIndexChanged(object sender, EventArgs e)
        {
            arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 10] = ComboBoxVar11.SelectedIndex;
            CheckError();
        }
        private void ComboBoxVar12_SelectedIndexChanged(object sender, EventArgs e)
        {
            arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 11] = ComboBoxVar12.SelectedIndex;
            CheckError();
        }
        private void ComboBoxVar13_SelectedIndexChanged(object sender, EventArgs e)
        {
            arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 12] = ComboBoxVar13.SelectedIndex;
            CheckError();
        }
        private void ComboBoxVar14_SelectedIndexChanged(object sender, EventArgs e)
        {
            arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 13] = ComboBoxVar14.SelectedIndex;
            CheckError();
        }
        private void ComboBoxVar15_SelectedIndexChanged(object sender, EventArgs e)
        {
            arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 14] = ComboBoxVar15.SelectedIndex;
            CheckError();
        }
        private void ComboBoxVar16_SelectedIndexChanged(object sender, EventArgs e)
        {
            arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 15] = ComboBoxVar16.SelectedIndex;
            CheckError();
        }



        // set arrayfunction when selecting function
        private void ComboBoxFunction1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (ComboBoxFunction1.SelectedIndex == 22) ComboBoxFunction1.SelectedIndex = 0; // no loop gen here!
            arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 0] = ComboBoxFunction1.SelectedIndex;
            hideGroupBoxes();
            clearslot(ComboBoxSampleAuswahl.SelectedIndex, 0);
            selectGroupBox(ComboBoxFunction1.SelectedIndex, 0);
            update_visibility();
            edit = 0;
            CheckError();
        }
        private void ComboBoxFunction2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (ComboBoxFunction2.SelectedIndex == 22) ComboBoxFunction2.SelectedIndex = 0; // no loop gen here!
            arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 1] = ComboBoxFunction2.SelectedIndex;
            hideGroupBoxes();
            clearslot(ComboBoxSampleAuswahl.SelectedIndex, 1);
            selectGroupBox(ComboBoxFunction2.SelectedIndex, 1);
            update_visibility();
            edit = 1;
            CheckError();
        }
        private void ComboBoxFunction3_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (ComboBoxFunction3.SelectedIndex == 22) ComboBoxFunction3.SelectedIndex = 0; // no loop gen here!
            arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 2] = ComboBoxFunction3.SelectedIndex;
            hideGroupBoxes();
            clearslot(ComboBoxSampleAuswahl.SelectedIndex, 2);
            selectGroupBox(ComboBoxFunction3.SelectedIndex, 2);
            update_visibility();
            edit = 2;
            CheckError();
        }
        private void ComboBoxFunction4_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (ComboBoxFunction4.SelectedIndex == 22) ComboBoxFunction4.SelectedIndex = 0; // no loop gen here!
            arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 3] = ComboBoxFunction4.SelectedIndex;
            hideGroupBoxes();
            clearslot(ComboBoxSampleAuswahl.SelectedIndex, 3);
            selectGroupBox(ComboBoxFunction4.SelectedIndex, 3);
            update_visibility();
            edit = 3;
            CheckError();
        }
        private void ComboBoxFunction5_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (ComboBoxFunction5.SelectedIndex == 22) ComboBoxFunction5.SelectedIndex = 0; // no loop gen here!
            arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 4] = ComboBoxFunction5.SelectedIndex;
            hideGroupBoxes();
            clearslot(ComboBoxSampleAuswahl.SelectedIndex, 4);
            selectGroupBox(ComboBoxFunction5.SelectedIndex, 4);
            update_visibility();
            edit = 4;
            CheckError();
        }
        private void ComboBoxFunction6_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (ComboBoxFunction6.SelectedIndex == 22) ComboBoxFunction6.SelectedIndex = 0; // no loop gen here!
            arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 5] = ComboBoxFunction6.SelectedIndex;
            hideGroupBoxes();
            clearslot(ComboBoxSampleAuswahl.SelectedIndex, 5);
            selectGroupBox(ComboBoxFunction6.SelectedIndex, 5);
            update_visibility();
            edit = 5;
            CheckError();
        }
        private void ComboBoxFunction7_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (ComboBoxFunction7.SelectedIndex == 22) ComboBoxFunction7.SelectedIndex = 0; // no loop gen here!
            arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 6] = ComboBoxFunction7.SelectedIndex;
            hideGroupBoxes();
            clearslot(ComboBoxSampleAuswahl.SelectedIndex, 6);
            update_visibility();
            selectGroupBox(ComboBoxFunction7.SelectedIndex, 6);
            edit = 6;
            CheckError();
        }
        private void ComboBoxFunction8_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (ComboBoxFunction8.SelectedIndex == 22) ComboBoxFunction8.SelectedIndex = 0; // no loop gen here!
            arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 7] = ComboBoxFunction8.SelectedIndex;
            hideGroupBoxes();
            clearslot(ComboBoxSampleAuswahl.SelectedIndex, 7);
            selectGroupBox(ComboBoxFunction8.SelectedIndex, 7);
            update_visibility();
            edit = 7;
            CheckError();
        }
        private void ComboBoxFunction9_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (ComboBoxFunction9.SelectedIndex == 22) ComboBoxFunction9.SelectedIndex = 0; // no loop gen here!
            arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 8] = ComboBoxFunction9.SelectedIndex;
            hideGroupBoxes();
            clearslot(ComboBoxSampleAuswahl.SelectedIndex, 8);
            selectGroupBox(ComboBoxFunction9.SelectedIndex, 8);
            update_visibility();
            edit = 8;
            CheckError();
        }
        private void ComboBoxFunction10_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (ComboBoxFunction10.SelectedIndex == 22) ComboBoxFunction10.SelectedIndex = 0; // no loop gen here!
            arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 9] = ComboBoxFunction10.SelectedIndex;
            hideGroupBoxes();
            clearslot(ComboBoxSampleAuswahl.SelectedIndex, 9);
            selectGroupBox(ComboBoxFunction10.SelectedIndex, 9);
            update_visibility();
            edit = 9;
            CheckError();
        }
        private void ComboBoxFunction11_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (ComboBoxFunction11.SelectedIndex == 22) ComboBoxFunction11.SelectedIndex = 0; // no loop gen here!
            arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 10] = ComboBoxFunction11.SelectedIndex;
            hideGroupBoxes();
            clearslot(ComboBoxSampleAuswahl.SelectedIndex, 10);
            selectGroupBox(ComboBoxFunction11.SelectedIndex, 10);
            update_visibility();
            edit = 10;
            CheckError();
        }
        private void ComboBoxFunction12_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (ComboBoxFunction12.SelectedIndex == 22) ComboBoxFunction12.SelectedIndex = 0; // no loop gen here!
            arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 11] = ComboBoxFunction12.SelectedIndex;
            hideGroupBoxes();
            clearslot(ComboBoxSampleAuswahl.SelectedIndex, 11);
            selectGroupBox(ComboBoxFunction12.SelectedIndex, 11);
            update_visibility();
            edit = 11;
            CheckError();
        }
        private void ComboBoxFunction13_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (ComboBoxFunction13.SelectedIndex == 22) ComboBoxFunction13.SelectedIndex = 0; // no loop gen here!
            arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 12] = ComboBoxFunction13.SelectedIndex;
            hideGroupBoxes();
            clearslot(ComboBoxSampleAuswahl.SelectedIndex, 12);
            selectGroupBox(ComboBoxFunction13.SelectedIndex, 12);
            update_visibility();
            edit = 12;
            CheckError();
        }
        private void ComboBoxFunction14_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (ComboBoxFunction14.SelectedIndex == 22) ComboBoxFunction14.SelectedIndex = 0; // no loop gen here!
            arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 13] = ComboBoxFunction14.SelectedIndex;
            hideGroupBoxes();
            clearslot(ComboBoxSampleAuswahl.SelectedIndex, 13);
            selectGroupBox(ComboBoxFunction14.SelectedIndex, 13);
            update_visibility();
            edit = 13;
            CheckError();
        }
        private void ComboBoxFunction15_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (ComboBoxFunction15.SelectedIndex == 22) ComboBoxFunction15.SelectedIndex = 0; // no loop gen here!
            arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 14] = ComboBoxFunction15.SelectedIndex;
            hideGroupBoxes();
            clearslot(ComboBoxSampleAuswahl.SelectedIndex, 14);
            selectGroupBox(ComboBoxFunction15.SelectedIndex, 14);
            update_visibility();
            edit = 14;
            CheckError();
        }
        private void ComboBoxFunction16_SelectionChangeCommitted(object sender, EventArgs e)
        {
            // loopgen only allowed here!
            arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 15] = ComboBoxFunction16.SelectedIndex;
            hideGroupBoxes();
            clearslot(ComboBoxSampleAuswahl.SelectedIndex, 15);
            selectGroupBox(ComboBoxFunction16.SelectedIndex, 15);
            update_visibility();
            edit = 15;
            CheckError();
        }





        // debug
        private void buttonStopSample_Click(object sender, EventArgs e)
        {
            waveout.Stop();
            buttonStopSample.Visible = false;
            //System.Console.WriteLine(arrayinstance[ComboBoxSampleAuswahl.SelectedIndex, 0]);

        }

        private void update_visibility()
        {
            if (ComboBoxOscsawFreq.SelectedIndex > 0) hScrollBarOscsawFreqValue.Visible = false; else hScrollBarOscsawFreqValue.Visible = true;
            if (ComboBoxOscsawFreq.SelectedIndex > 0) TextBoxOscsawFreqValue.Visible = false; else TextBoxOscsawFreqValue.Visible = true;
            if (ComboBoxOscsawGain.SelectedIndex > 0) hScrollBarOscsawGainValue.Visible = false; else hScrollBarOscsawGainValue.Visible = true;
            if (ComboBoxOscsawGain.SelectedIndex > 0) TextBoxOscsawGainValue.Visible = false; else TextBoxOscsawGainValue.Visible = true;
            if (ComboBoxOsctriFreq.SelectedIndex > 0) hScrollBarOsctriFreqValue.Visible = false; else hScrollBarOsctriFreqValue.Visible = true;
            if (ComboBoxOsctriFreq.SelectedIndex > 0) TextBoxOsctriFreqValue.Visible = false; else TextBoxOsctriFreqValue.Visible = true;
            if (ComboBoxOsctriGain.SelectedIndex > 0) hScrollBarOsctriGainValue.Visible = false; else hScrollBarOsctriGainValue.Visible = true;
            if (ComboBoxOsctriGain.SelectedIndex > 0) TextBoxOsctriGainValue.Visible = false; else TextBoxOsctriGainValue.Visible = true;
            if (ComboBoxOscsineFreq.SelectedIndex > 0) hScrollBarOscsineFreqValue.Visible = false; else hScrollBarOscsineFreqValue.Visible = true;
            if (ComboBoxOscsineFreq.SelectedIndex > 0) TextBoxOscsineFreqValue.Visible = false; else TextBoxOscsineFreqValue.Visible = true;
            if (ComboBoxOscsineGain.SelectedIndex > 0) hScrollBarOscsineGainValue.Visible = false; else hScrollBarOscsineGainValue.Visible = true;
            if (ComboBoxOscsineGain.SelectedIndex > 0) TextBoxOscsineGainValue.Visible = false; else TextBoxOscsineGainValue.Visible = true;
            if (ComboBoxOscpulseFreq.SelectedIndex > 0) hScrollBarOscpulseFreqValue.Visible = false; else hScrollBarOscpulseFreqValue.Visible = true;
            if (ComboBoxOscpulseFreq.SelectedIndex > 0) TextBoxOscpulseFreqValue.Visible = false; else TextBoxOscpulseFreqValue.Visible = true;
            if (ComboBoxOscpulseGain.SelectedIndex > 0) hScrollBarOscpulseGainValue.Visible = false; else hScrollBarOscpulseGainValue.Visible = true;
            if (ComboBoxOscpulseGain.SelectedIndex > 0) TextBoxOscpulseGainValue.Visible = false; else TextBoxOscpulseGainValue.Visible = true;
            if (ComboBoxOscnoiseGain.SelectedIndex > 0) hScrollBarOscnoiseGainValue.Visible = false; else hScrollBarOscnoiseGainValue.Visible = true;
            if (ComboBoxOscnoiseGain.SelectedIndex > 0) TextBoxOscnoiseGainValue.Visible = false; else TextBoxOscnoiseGainValue.Visible = true;
            if (ComboBoxEnvaGain.SelectedIndex > 0) hScrollBarEnvaGainValue.Visible = false; else hScrollBarEnvaGainValue.Visible = true;
            if (ComboBoxEnvaGain.SelectedIndex > 0) TextBoxEnvaGainValue.Visible = false; else TextBoxEnvaGainValue.Visible = true;
            if (ComboBoxEnvdGain.SelectedIndex > 0) hScrollBarEnvdGainValue.Visible = false; else hScrollBarEnvdGainValue.Visible = true;
            if (ComboBoxEnvdGain.SelectedIndex > 0) TextBoxEnvdGainValue.Visible = false; else TextBoxEnvdGainValue.Visible = true;
            if (ComboBoxAddVal2.SelectedIndex > 0) hScrollBarAddVal2Value.Visible = false; else hScrollBarAddVal2Value.Visible = true;
            if (ComboBoxAddVal2.SelectedIndex > 0) TextBoxAddVal2Value.Visible = false; else TextBoxAddVal2Value.Visible = true;
            if (ComboBoxMulVal2.SelectedIndex > 0) hScrollBarMulVal2Value.Visible = false; else hScrollBarMulVal2Value.Visible = true;
            if (ComboBoxMulVal2.SelectedIndex > 0) TextBoxMulVal2Value.Visible = false; else TextBoxMulVal2Value.Visible = true;
            if (ComboBoxMulVal2.SelectedIndex > 0) TextBoxMulVal2ValueFloat.Visible = false; else TextBoxMulVal2ValueFloat.Visible = true;
            if (ComboBoxDelayDelay.SelectedIndex > 0) hScrollBarDelayDelayValue.Visible = false; else hScrollBarDelayDelayValue.Visible = true;
            if (ComboBoxDelayDelay.SelectedIndex > 0) TextBoxDelayDelayValue.Visible = false; else TextBoxDelayDelayValue.Visible = true;
            if (ComboBoxDelayGain.SelectedIndex > 0) hScrollBarDelayGainValue.Visible = false; else hScrollBarDelayGainValue.Visible = true;
            if (ComboBoxDelayGain.SelectedIndex > 0) TextBoxDelayGainValue.Visible = false; else TextBoxDelayGainValue.Visible = true;
            if (ComboBoxCombDelay.SelectedIndex > 0) hScrollBarCombDelayValue.Visible = false; else hScrollBarCombDelayValue.Visible = true;
            if (ComboBoxCombDelay.SelectedIndex > 0) TextBoxCombDelayValue.Visible = false; else TextBoxCombDelayValue.Visible = true;
            if (ComboBoxCombFeedback.SelectedIndex > 0) hScrollBarCombFeedbackValue.Visible = false; else hScrollBarCombFeedbackValue.Visible = true;
            if (ComboBoxCombFeedback.SelectedIndex > 0) TextBoxCombFeedbackValue.Visible = false; else TextBoxCombFeedbackValue.Visible = true;
            if (ComboBoxCombGain.SelectedIndex > 0) hScrollBarCombGainValue.Visible = false; else hScrollBarCombGainValue.Visible = true;
            if (ComboBoxCombGain.SelectedIndex > 0) TextBoxCombGainValue.Visible = false; else TextBoxCombGainValue.Visible = true;
            if (ComboBoxReverbFeedback.SelectedIndex > 0) hScrollBarReverbFeedbackValue.Visible = false; else hScrollBarReverbFeedbackValue.Visible = true;
            if (ComboBoxReverbFeedback.SelectedIndex > 0) TextBoxReverbFeedbackValue.Visible = false; else TextBoxReverbFeedbackValue.Visible = true;
            if (ComboBoxReverbGain.SelectedIndex > 0) hScrollBarReverbGainValue.Visible = false; else hScrollBarReverbGainValue.Visible = true;
            if (ComboBoxReverbGain.SelectedIndex > 0) TextBoxReverbGainValue.Visible = false; else TextBoxReverbGainValue.Visible = true;
            if (ComboBoxFilterCutoff.SelectedIndex > 0) hScrollBarFilterCutoffValue.Visible = false; else hScrollBarFilterCutoffValue.Visible = true;
            if (ComboBoxFilterCutoff.SelectedIndex > 0) TextBoxFilterCutoffValue.Visible = false; else TextBoxFilterCutoffValue.Visible = true;
            if (ComboBoxFilterResonance.SelectedIndex > 0) hScrollBarFilterResonanceValue.Visible = false; else hScrollBarFilterResonanceValue.Visible = true;
            if (ComboBoxFilterResonance.SelectedIndex > 0) TextBoxFilterResonanceValue.Visible = false; else TextBoxFilterResonanceValue.Visible = true;
            if (ComboBoxVolGain.SelectedIndex > 0) hScrollBarVolGainValue.Visible = false; else hScrollBarVolGainValue.Visible = true;
            if (ComboBoxVolGain.SelectedIndex > 0) TextBoxVolGainValue.Visible = false; else TextBoxVolGainValue.Visible = true;
            if (ComboBoxDistortionGain.SelectedIndex > 0) hScrollBarDistortionGainValue.Visible = false; else hScrollBarDistortionGainValue.Visible = true;
            if (ComboBoxDistortionGain.SelectedIndex > 0) TextBoxDistortionGainValue.Visible = false; else TextBoxDistortionGainValue.Visible = true;
            if (ComboBoxChordShift.SelectedIndex > 0) hScrollBarChordShiftValue.Visible = false; else hScrollBarChordShiftValue.Visible = true;
            if (ComboBoxChordShift.SelectedIndex > 0) TextBoxChordShiftValue.Visible = false; else TextBoxChordShiftValue.Visible = true;
            if (ComboBoxOscpulseWidth.SelectedIndex > 0) hScrollBarOscpulseWidthValue.Visible = false; else hScrollBarOscpulseWidthValue.Visible = true;
            if (ComboBoxOscpulseWidth.SelectedIndex > 0) TextBoxOscpulseWidthValue.Visible = false; else TextBoxOscpulseWidthValue.Visible = true;
            if (ComboBoxShStep.SelectedIndex > 0) hScrollBarShStepValue.Visible = false; else hScrollBarShStepValue.Visible = true;
            if (ComboBoxShStep.SelectedIndex > 0) TextBoxShStepValue.Visible = false; else TextBoxShStepValue.Visible = true;
            if (ComboBoxOnepoleCutoff.SelectedIndex > 0) hScrollBarOnepoleCutoffValue.Visible = false; else hScrollBarOnepoleCutoffValue.Visible = true;
            if (ComboBoxOnepoleCutoff.SelectedIndex > 0) TextBoxOnepoleCutoffValue.Visible = false; else TextBoxOnepoleCutoffValue.Visible = true;
            if (clonewithtranspose == true)
            {
                if (ComboBoxCloneTranspose.SelectedIndex > 0) hScrollBarCloneTransposeValue.Visible = false; else hScrollBarCloneTransposeValue.Visible = true;
                if (ComboBoxCloneTranspose.SelectedIndex > 0) TextBoxCloneTransposeValue.Visible = false; else TextBoxCloneTransposeValue.Visible = true;
            }
           
        }


        // Osc_saw control elements
        // set arrayfreq when selecting oscsaw freq
        private void ComboBoxOscsawFreq_SelectionChangeCommitted(object sender, EventArgs e)
        {
            arrayfrequency[ComboBoxSampleAuswahl.SelectedIndex, edit] = (short)ComboBoxOscsawFreq.SelectedIndex;
            if (ComboBoxOscsawFreq.SelectedIndex > 0) hScrollBarOscsawFreqValue.Visible = false; else hScrollBarOscsawFreqValue.Visible = true;
            if (ComboBoxOscsawFreq.SelectedIndex > 0) TextBoxOscsawFreqValue.Visible = false; else TextBoxOscsawFreqValue.Visible = true;
            Refresh();
        }
        // set arraygain when selecting oscsaw gain
        private void ComboBoxOscsawGain_SelectionChangeCommitted(object sender, EventArgs e)
        {
            arraygain[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)ComboBoxOscsawGain.SelectedIndex;
            if (ComboBoxOscsawGain.SelectedIndex > 0) hScrollBarOscsawGainValue.Visible = false; else hScrollBarOscsawGainValue.Visible = true;
            if (ComboBoxOscsawGain.SelectedIndex > 0) TextBoxOscsawGainValue.Visible = false; else TextBoxOscsawGainValue.Visible = true;
            Refresh();
        }
        // set arrayfreqval when scrolling oscsaw freq value
        private void hScrollBarOscsawFreqValue_Scroll(object sender, ScrollEventArgs e)
        {
            arrayfrequencyval[ComboBoxSampleAuswahl.SelectedIndex, edit] = (short)hScrollBarOscsawFreqValue.Value;
            TextBoxOscsawFreqValue.Text = Convert.ToString(hScrollBarOscsawFreqValue.Value);
            //  buttonGenerateSingleSample.PerformClick();
        }
        // set arraygainval when scrolling oscsaw gain value
        private void hScrollBarOscsawGainValue_Scroll(object sender, ScrollEventArgs e)
        {
            arraygainval[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarOscsawGainValue.Value;
            TextBoxOscsawGainValue.Text = Convert.ToString(hScrollBarOscsawGainValue.Value);
        }

        // Osc_tri control elements
        // set arrayfreq when selecting osctri freq
        private void ComboBoxOsctriFreq_SelectionChangeCommitted(object sender, EventArgs e)
        {
            arrayfrequency[ComboBoxSampleAuswahl.SelectedIndex, edit] = (short)ComboBoxOsctriFreq.SelectedIndex;
            if (ComboBoxOsctriFreq.SelectedIndex > 0) hScrollBarOsctriFreqValue.Visible = false; else hScrollBarOsctriFreqValue.Visible = true;
            if (ComboBoxOsctriFreq.SelectedIndex > 0) TextBoxOsctriFreqValue.Visible = false; else TextBoxOsctriFreqValue.Visible = true;
            Refresh();
        }
        // set arraygain when selecting osctri gain
        private void ComboBoxOsctriGain_SelectionChangeCommitted(object sender, EventArgs e)
        {
            arraygain[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)ComboBoxOsctriGain.SelectedIndex;
            if (ComboBoxOsctriGain.SelectedIndex > 0) hScrollBarOsctriGainValue.Visible = false; else hScrollBarOsctriGainValue.Visible = true;
            if (ComboBoxOsctriGain.SelectedIndex > 0) TextBoxOsctriGainValue.Visible = false; else TextBoxOsctriGainValue.Visible = true;
            Refresh();
        }
        // set arrayfreqval when scrolling osctri freq value
        private void hScrollBarOsctriFreqValue_Scroll(object sender, ScrollEventArgs e)
        {
            arrayfrequencyval[ComboBoxSampleAuswahl.SelectedIndex, edit] = (short)hScrollBarOsctriFreqValue.Value;
            TextBoxOsctriFreqValue.Text = Convert.ToString(hScrollBarOsctriFreqValue.Value);
        }
        // set arraygainval when scrolling osctri gain value
        private void hScrollBarOsctriGainValue_Scroll(object sender, ScrollEventArgs e)
        {
            arraygainval[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarOsctriGainValue.Value;
            TextBoxOsctriGainValue.Text = Convert.ToString(hScrollBarOsctriGainValue.Value);
        }

        // osc_sine control elements
        // set arrayfreq when selecting oscsine freq
        private void ComboBoxOscsineFreq_SelectionChangeCommitted(object sender, EventArgs e)
        {
            arrayfrequency[ComboBoxSampleAuswahl.SelectedIndex, edit] = (short)ComboBoxOscsineFreq.SelectedIndex;
            if (ComboBoxOscsineFreq.SelectedIndex > 0) hScrollBarOscsineFreqValue.Visible = false; else hScrollBarOscsineFreqValue.Visible = true;
            if (ComboBoxOscsineFreq.SelectedIndex > 0) TextBoxOscsineFreqValue.Visible = false; else TextBoxOscsineFreqValue.Visible = true;
            Refresh();
        }
        // set arraygain when selecting oscsine gain
        private void ComboBoxOscsineGain_SelectionChangeCommitted(object sender, EventArgs e)
        {
            arraygain[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)ComboBoxOscsineGain.SelectedIndex;
            if (ComboBoxOscsineGain.SelectedIndex > 0) hScrollBarOscsineGainValue.Visible = false; else hScrollBarOscsineGainValue.Visible = true;
            if (ComboBoxOscsineGain.SelectedIndex > 0) TextBoxOscsineGainValue.Visible = false; else TextBoxOscsineGainValue.Visible = true;
            Refresh();
        }
        // set arrayfreqval when scrolling oscsine freq value
        private void hScrollBarOscsineFreqValue_Scroll(object sender, ScrollEventArgs e)
        {
            arrayfrequencyval[ComboBoxSampleAuswahl.SelectedIndex, edit] = (short)hScrollBarOscsineFreqValue.Value;
            TextBoxOscsineFreqValue.Text = Convert.ToString(hScrollBarOscsineFreqValue.Value);
        }
        // set arraygainval when scrolling oscsine gain value
        private void hScrollBarOscsineGainValue_Scroll(object sender, ScrollEventArgs e)
        {
            arraygainval[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarOscsineGainValue.Value;
            TextBoxOscsineGainValue.Text = Convert.ToString(hScrollBarOscsineGainValue.Value);
        }

        // osc_pulse control elements
        // set arrayfreq when selecting oscpulse freq
        private void ComboBoxOscpulseFreq_SelectionChangeCommitted(object sender, EventArgs e)
        {
            arrayfrequency[ComboBoxSampleAuswahl.SelectedIndex, edit] = (short)ComboBoxOscpulseFreq.SelectedIndex;
            if (ComboBoxOscpulseFreq.SelectedIndex > 0) hScrollBarOscpulseFreqValue.Visible = false; else hScrollBarOscpulseFreqValue.Visible = true;
            if (ComboBoxOscpulseFreq.SelectedIndex > 0) TextBoxOscpulseFreqValue.Visible = false; else TextBoxOscpulseFreqValue.Visible = true;
            Refresh();
        }
        // set arraygain when selecting oscpulse gain
        private void ComboBoxOscpulseGain_SelectionChangeCommitted(object sender, EventArgs e)
        {
            arraygain[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)ComboBoxOscpulseGain.SelectedIndex;
            if (ComboBoxOscpulseGain.SelectedIndex > 0) hScrollBarOscpulseGainValue.Visible = false; else hScrollBarOscpulseGainValue.Visible = true;
            if (ComboBoxOscpulseGain.SelectedIndex > 0) TextBoxOscpulseGainValue.Visible = false; else TextBoxOscpulseGainValue.Visible = true;
            Refresh();
        }
        // set arraywidth when selecting oscpulse width
        private void ComboBoxOscpulseWidth_SelectionChangeCommitted(object sender, EventArgs e)
        {
            arraywidth[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)ComboBoxOscpulseWidth.SelectedIndex;
            if (ComboBoxOscpulseWidth.SelectedIndex > 0) hScrollBarOscpulseWidthValue.Visible = false; else hScrollBarOscpulseWidthValue.Visible = true;
            if (ComboBoxOscpulseWidth.SelectedIndex > 0) TextBoxOscpulseWidthValue.Visible = false; else TextBoxOscpulseWidthValue.Visible = true;
            Refresh();
        }
        // set arrayfreqval when scrolling oscpulse freq value
        private void hScrollBarOscpulseFreqValue_Scroll(object sender, ScrollEventArgs e)
        {
            arrayfrequencyval[ComboBoxSampleAuswahl.SelectedIndex, edit] = (short)hScrollBarOscpulseFreqValue.Value;
            TextBoxOscpulseFreqValue.Text = Convert.ToString(hScrollBarOscpulseFreqValue.Value);
        }
        // set arraygainval when scrolling oscpulse gain value
        private void hScrollBarOscpulseGainValue_Scroll(object sender, ScrollEventArgs e)
        {
            arraygainval[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarOscpulseGainValue.Value;
            TextBoxOscpulseGainValue.Text = Convert.ToString(hScrollBarOscpulseGainValue.Value);
        }
        // set arraywidthval when scrolling oscpulse width value
        private void hScrollBarOscpulseWidthValue_Scroll(object sender, ScrollEventArgs e)
        {
            arraywidthval[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarOscpulseWidthValue.Value;
            TextBoxOscpulseWidthValue.Text = Convert.ToString(hScrollBarOscpulseWidthValue.Value);
        }

        // osc_noise control elements
        // set arraygain when selecting oscnoise gain
        private void ComboBoxOscnoiseGain_SelectionChangeCommitted(object sender, EventArgs e)
        {
            arraygain[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)ComboBoxOscnoiseGain.SelectedIndex;
            if (ComboBoxOscnoiseGain.SelectedIndex > 0) hScrollBarOscnoiseGainValue.Visible = false; else hScrollBarOscnoiseGainValue.Visible = true;
            if (ComboBoxOscnoiseGain.SelectedIndex > 0) TextBoxOscnoiseGainValue.Visible = false; else TextBoxOscnoiseGainValue.Visible = true;
            Refresh();
        }
        // set arraygainval when scrolling oscnoise gain value
        private void hScrollBarOscnoiseGainValue_Scroll(object sender, ScrollEventArgs e)
        {
            arraygainval[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarOscnoiseGainValue.Value;
            TextBoxOscnoiseGainValue.Text = Convert.ToString(hScrollBarOscnoiseGainValue.Value);
        }
        // set arraywidthval when scrolling oscnoise seed value
        private void hScrollBarOscnoiseSeedValue_Scroll(object sender, ScrollEventArgs e)
        {
            arraywidthval[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarOscnoiseSeedValue.Value;
            TextBoxOscnoiseSeedValue.Text = Convert.ToString(hScrollBarOscnoiseSeedValue.Value);
        }

        // Enva control elements
        // set arrayval1value when scrolling enva attack value
        private void hScrollBarEnvaAttackValue_Scroll(object sender, ScrollEventArgs e)
        {
            arrayval1value[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarEnvaAttackValue.Value;
            TextBoxEnvaAttackValue.Text = Convert.ToString(hScrollBarEnvaAttackValue.Value);
        }
        // set arraygain when selecting enva gain
        private void ComboBoxEnvaGain_SelectionChangeCommitted(object sender, EventArgs e)
        {
            arraygain[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)ComboBoxEnvaGain.SelectedIndex;
            if (ComboBoxEnvaGain.SelectedIndex > 0) hScrollBarEnvaGainValue.Visible = false; else hScrollBarEnvaGainValue.Visible = true;
            if (ComboBoxEnvaGain.SelectedIndex > 0) TextBoxEnvaGainValue.Visible = false; else TextBoxEnvaGainValue.Visible = true;
            Refresh();
        }
        // set arraygainval when scrolling enva gain value
        private void hScrollBarEnvaGainValue_Scroll(object sender, ScrollEventArgs e)
        {
            arraygainval[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarEnvaGainValue.Value;
            TextBoxEnvaGainValue.Text = Convert.ToString(hScrollBarEnvaGainValue.Value);
        }

        // Envd control elements
        // set arrayval1value when scrolling envd decay value
        private void hScrollBarEnvdDecayValue_Scroll(object sender, ScrollEventArgs e)
        {
            arrayval1value[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarEnvdDecayValue.Value;
            TextBoxEnvdDecayValue.Text = Convert.ToString(hScrollBarEnvdDecayValue.Value);
        }
        // set arrayval2value when scrolling envd sustain value
        private void hScrollBarEnvdSustainValue_Scroll(object sender, ScrollEventArgs e)
        {
            arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarEnvdSustainValue.Value;
            TextBoxEnvdSustainValue.Text = Convert.ToString(hScrollBarEnvdSustainValue.Value);
        }
        // set arraygain when selecting envd gain
        private void ComboBoxEnvdGain_SelectionChangeCommitted(object sender, EventArgs e)
        {
            arraygain[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)ComboBoxEnvdGain.SelectedIndex;
            if (ComboBoxEnvdGain.SelectedIndex > 0) hScrollBarEnvdGainValue.Visible = false; else hScrollBarEnvdGainValue.Visible = true;
            if (ComboBoxEnvdGain.SelectedIndex > 0) TextBoxEnvdGainValue.Visible = false; else TextBoxEnvdGainValue.Visible = true;
            Refresh();
        }
        // set arraygainval when scrolling envd gain value
        private void hScrollBarEnvdGainValue_Scroll(object sender, ScrollEventArgs e)
        {
            arraygainval[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarEnvdGainValue.Value;
            TextBoxEnvdGainValue.Text = Convert.ToString(hScrollBarEnvdGainValue.Value);
        }

        // Add control elements
        // set arrayval1 when selecting addval1
        private void ComboBoxAddVal1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            arrayval1[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)ComboBoxAddVal1.SelectedIndex;
            CheckError();
            Refresh();
        }
        // set arrayval2 when selecting addval2
        private void ComboBoxAddVal2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            arrayval2[ComboBoxSampleAuswahl.SelectedIndex, edit] = (short)ComboBoxAddVal2.SelectedIndex;
            if (ComboBoxAddVal2.SelectedIndex > 0) hScrollBarAddVal2Value.Visible = false; else hScrollBarAddVal2Value.Visible = true;
            if (ComboBoxAddVal2.SelectedIndex > 0) TextBoxAddVal2Value.Visible = false; else TextBoxAddVal2Value.Visible = true;
            Refresh();
        }
        // set arrayval2value when scrolling addval2 value
        private void hScrollBarAddVal2Value_Scroll(object sender, ScrollEventArgs e)
        {
            arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, edit] = (short)hScrollBarAddVal2Value.Value;
            TextBoxAddVal2Value.Text = Convert.ToString(hScrollBarAddVal2Value.Value);
        }

        // Mul control elements
        // set arrayval1 when selecting mulval1
        private void ComboBoxMulVal1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            arrayval1[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)ComboBoxMulVal1.SelectedIndex;
            CheckError();
            Refresh();
        }
        // set arrayval2 when selecting mulval2
        private void ComboBoxMulVal2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            arrayval2[ComboBoxSampleAuswahl.SelectedIndex, edit] = (short)ComboBoxMulVal2.SelectedIndex;
            if (ComboBoxMulVal2.SelectedIndex > 0) hScrollBarMulVal2Value.Visible = false; else hScrollBarMulVal2Value.Visible = true;
            if (ComboBoxMulVal2.SelectedIndex > 0) TextBoxMulVal2Value.Visible = false; else TextBoxMulVal2Value.Visible = true;
            if (ComboBoxMulVal2.SelectedIndex > 0) TextBoxMulVal2ValueFloat.Visible = false; else TextBoxMulVal2ValueFloat.Visible = true;
            Refresh();
        }
        // set arrayval2value when scrolling mulval2 value
        private void hScrollBarMulVal2Value_Scroll(object sender, ScrollEventArgs e)
        {
            arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, edit] = (short)hScrollBarMulVal2Value.Value;
            TextBoxMulVal2Value.Text = Convert.ToString(hScrollBarMulVal2Value.Value);
            float tempfloat = (float)hScrollBarMulVal2Value.Value;
            tempfloat = (1.0f / 32767.0f) * tempfloat;
            TextBoxMulVal2ValueFloat.Text = tempfloat.ToString("F4");
        }

        // Delay control elements
        // set arrayval1 when selecting delayvalue
        private void ComboBoxDelayValue_SelectionChangeCommitted(object sender, EventArgs e)
        {
            arrayval1[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)ComboBoxDelayValue.SelectedIndex;
            CheckError();
            Refresh();
        }
        // set arrayfrequency when selecting delaydelay
        private void ComboBoxDelayDelay_SelectionChangeCommitted(object sender, EventArgs e)
        {
            arrayfrequency[ComboBoxSampleAuswahl.SelectedIndex, edit] = (short)ComboBoxDelayDelay.SelectedIndex;
            if (ComboBoxDelayDelay.SelectedIndex > 0) hScrollBarDelayDelayValue.Visible = false; else hScrollBarDelayDelayValue.Visible = true;
            if (ComboBoxDelayDelay.SelectedIndex > 0) TextBoxDelayDelayValue.Visible = false; else TextBoxDelayDelayValue.Visible = true;
            Refresh();
        }
        // set arraygain when selecting delaygain
        private void ComboBoxDelayGain_SelectionChangeCommitted(object sender, EventArgs e)
        {
            arraygain[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)ComboBoxDelayGain.SelectedIndex;
            if (ComboBoxDelayGain.SelectedIndex > 0) hScrollBarDelayGainValue.Visible = false; else hScrollBarDelayGainValue.Visible = true;
            if (ComboBoxDelayGain.SelectedIndex > 0) TextBoxDelayGainValue.Visible = false; else TextBoxDelayGainValue.Visible = true;
            Refresh();
        }
        // set arrayfreqencyvalue when scrolling delaydelay value
        private void hScrollBarDelayDelayValue_Scroll(object sender, ScrollEventArgs e)
        {
            arrayfrequencyval[ComboBoxSampleAuswahl.SelectedIndex, edit] = (short)hScrollBarDelayDelayValue.Value;
            TextBoxDelayDelayValue.Text = Convert.ToString(hScrollBarDelayDelayValue.Value);
        }
        // set arraygainvalue when scrolling delaygain value
        private void hScrollBarDelayGainValue_Scroll(object sender, ScrollEventArgs e)
        {
            arraygainval[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarDelayGainValue.Value;
            TextBoxDelayGainValue.Text = Convert.ToString(hScrollBarDelayGainValue.Value);
        }

        // Comb control elements
        // set arrayval1 when selecting combvalue
        private void ComboBoxCombValue_SelectionChangeCommitted(object sender, EventArgs e)
        {
            arrayval1[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)ComboBoxCombValue.SelectedIndex;
            CheckError();
            Refresh();
        }
        // set arrayfrequency when selecting combdelay
        private void ComboBoxCombDelay_SelectionChangeCommitted(object sender, EventArgs e)
        {
            arrayfrequency[ComboBoxSampleAuswahl.SelectedIndex, edit] = (short)ComboBoxCombDelay.SelectedIndex;
            if (ComboBoxCombDelay.SelectedIndex > 0) hScrollBarCombDelayValue.Visible = false; else hScrollBarCombDelayValue.Visible = true;
            if (ComboBoxCombDelay.SelectedIndex > 0) TextBoxCombDelayValue.Visible = false; else TextBoxCombDelayValue.Visible = true;
            Refresh();
        }
        // set arrayval2 when selecting combfeedback
        private void ComboBoxCombFeedback_SelectionChangeCommitted(object sender, EventArgs e)
        {
            arrayval2[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)ComboBoxCombFeedback.SelectedIndex;
            if (ComboBoxCombFeedback.SelectedIndex > 0) hScrollBarCombFeedbackValue.Visible = false; else hScrollBarCombFeedbackValue.Visible = true;
            if (ComboBoxCombFeedback.SelectedIndex > 0) TextBoxCombFeedbackValue.Visible = false; else TextBoxCombFeedbackValue.Visible = true;
            Refresh();
        }
        // set arraygain when selecting combgain
        private void ComboBoxCombGain_SelectionChangeCommitted(object sender, EventArgs e)
        {
            arraygain[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)ComboBoxCombGain.SelectedIndex;
            if (ComboBoxCombGain.SelectedIndex > 0) hScrollBarCombGainValue.Visible = false; else hScrollBarCombGainValue.Visible = true;
            if (ComboBoxCombGain.SelectedIndex > 0) TextBoxCombGainValue.Visible = false; else TextBoxCombGainValue.Visible = true;
            Refresh();
        }
        // set arrayfreqvalue when scrolling combdelay value
        private void hScrollBarCombDelayValue_Scroll(object sender, ScrollEventArgs e)
        {
            arrayfrequencyval[ComboBoxSampleAuswahl.SelectedIndex, edit] = (short)hScrollBarCombDelayValue.Value;
            TextBoxCombDelayValue.Text = Convert.ToString(hScrollBarCombDelayValue.Value);
        }
        // set arrayval2value when scrolling delayfeedback value
        private void hScrollBarCombFeedbackValue_Scroll(object sender, ScrollEventArgs e)
        {
            arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarCombFeedbackValue.Value;
            TextBoxCombFeedbackValue.Text = Convert.ToString(hScrollBarCombFeedbackValue.Value);
        }
        // set arraygainvalue when scrolling combgain value
        private void hScrollBarCombGainValue_Scroll(object sender, ScrollEventArgs e)
        {
            arraygainval[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarCombGainValue.Value;
            TextBoxCombGainValue.Text = Convert.ToString(hScrollBarCombGainValue.Value);
        }

        // Reverb control elements
        // set arrayval1 when selecting reverbvalue
        private void ComboBoxReverbValue_SelectionChangeCommitted(object sender, EventArgs e)
        {
            arrayval1[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)ComboBoxReverbValue.SelectedIndex;
            CheckError();
            Refresh();
        }
        // set arrayval2 when selecting reverbfeedback
        private void ComboBoxReverbFeedback_SelectionChangeCommitted(object sender, EventArgs e)
        {
            arrayval2[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)ComboBoxReverbFeedback.SelectedIndex;
            if (ComboBoxReverbFeedback.SelectedIndex > 0) hScrollBarReverbFeedbackValue.Visible = false; else hScrollBarReverbFeedbackValue.Visible = true;
            if (ComboBoxReverbFeedback.SelectedIndex > 0) TextBoxReverbFeedbackValue.Visible = false; else TextBoxReverbFeedbackValue.Visible = true;
            Refresh();
        }
        // set arraygain when selecting reverbgain
        private void ComboBoxReverbGain_SelectionChangeCommitted(object sender, EventArgs e)
        {
            arraygain[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)ComboBoxReverbGain.SelectedIndex;
            if (ComboBoxReverbGain.SelectedIndex > 0) hScrollBarReverbGainValue.Visible = false; else hScrollBarReverbGainValue.Visible = true;
            if (ComboBoxReverbGain.SelectedIndex > 0) TextBoxReverbGainValue.Visible = false; else TextBoxReverbGainValue.Visible = true;
            Refresh();
        }
        // set arrayval2value when scrolling reverbfeedback value
        private void hScrollBarReverbFeedbackValue_Scroll(object sender, ScrollEventArgs e)
        {
            arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarReverbFeedbackValue.Value;
            TextBoxReverbFeedbackValue.Text = Convert.ToString(hScrollBarReverbFeedbackValue.Value);
        }
        // set arraygainvalue when scrolling reverbgain value
        private void hScrollBarReverbGainValue_Scroll(object sender, ScrollEventArgs e)
        {
            arraygainval[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarReverbGainValue.Value;
            TextBoxReverbGainValue.Text = Convert.ToString(hScrollBarReverbGainValue.Value);
        }

        // Ctrl control elements
        // set arrayval1 when selecting ctrlvalue
        private void ComboBoxCtrlValue_SelectionChangeCommitted(object sender, EventArgs e)
        {
            arrayval1[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)ComboBoxCtrlValue.SelectedIndex;
            CheckError();
            Refresh();
        }

        // Filter control elements
        // set arrayval1 when selecting filtervalue
        private void ComboBoxFilterValue_SelectionChangeCommitted(object sender, EventArgs e)
        {
            arrayval1[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)ComboBoxFilterValue.SelectedIndex;
            CheckError();
            Refresh();
        }
        // set arrayfrequency when selecting filtercutoff
        private void ComboBoxFilterCutoff_SelectionChangeCommitted(object sender, EventArgs e)
        {
            arrayfrequency[ComboBoxSampleAuswahl.SelectedIndex, edit] = (short)ComboBoxFilterCutoff.SelectedIndex;
            if (ComboBoxFilterCutoff.SelectedIndex > 0) hScrollBarFilterCutoffValue.Visible = false; else hScrollBarFilterCutoffValue.Visible = true;
            if (ComboBoxFilterCutoff.SelectedIndex > 0) TextBoxFilterCutoffValue.Visible = false; else TextBoxFilterCutoffValue.Visible = true;
            Refresh();
        }
        // set arrayval2 when selecting filterresonance
        private void ComboBoxFilterResonance_SelectionChangeCommitted(object sender, EventArgs e)
        {
            arrayval2[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)ComboBoxFilterResonance.SelectedIndex;
            if (ComboBoxFilterResonance.SelectedIndex > 0) hScrollBarFilterResonanceValue.Visible = false; else hScrollBarFilterResonanceValue.Visible = true;
            if (ComboBoxFilterResonance.SelectedIndex > 0) TextBoxFilterResonanceValue.Visible = false; else TextBoxFilterResonanceValue.Visible = true;
            Refresh();
        }
        // set arraygain when selecting filtermode
        private void ComboBoxFilterMode_SelectionChangeCommitted(object sender, EventArgs e)
        {
            arraygain[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)ComboBoxFilterMode.SelectedIndex;
            Refresh();
        }
        // set filterfrequencyvalue when scrolling filtercutoff value
        private void hScrollBarFilterCutoffValue_Scroll(object sender, ScrollEventArgs e)
        {
            arrayfrequencyval[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarFilterCutoffValue.Value;
            TextBoxFilterCutoffValue.Text = Convert.ToString(hScrollBarFilterCutoffValue.Value);
        }
        // set arrayval2value when scrolling filterresonance value
        private void hScrollBarFilterResonanceValue_Scroll(object sender, ScrollEventArgs e)
        {
            arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarFilterResonanceValue.Value;
            TextBoxFilterResonanceValue.Text = Convert.ToString(hScrollBarFilterResonanceValue.Value);
        }

        // Volume control elements
        // set arrayval1 when selecting volval
        private void ComboBoxVolVal_SelectionChangeCommitted(object sender, EventArgs e)
        {
            arrayval1[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)ComboBoxVolVal.SelectedIndex;
            CheckError();
            Refresh();
        }
        // set arraygain when selecting volgain
        private void ComboBoxVolGain_SelectionChangeCommitted(object sender, EventArgs e)
        {
            arraygain[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)ComboBoxVolGain.SelectedIndex;
            if (ComboBoxVolGain.SelectedIndex > 0) hScrollBarVolGainValue.Visible = false; else hScrollBarVolGainValue.Visible = true;
            if (ComboBoxVolGain.SelectedIndex > 0) TextBoxVolGainValue.Visible = false; else TextBoxVolGainValue.Visible = true;
            Refresh();
        }
        // set arraygainvalue when scrolling volgain value
        private void hScrollBarVolGainValue_Scroll(object sender, ScrollEventArgs e)
        {
            arraygainval[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarVolGainValue.Value;
            TextBoxVolGainValue.Text = Convert.ToString(hScrollBarVolGainValue.Value);
        }

        // Distortion control elements
        // set arrayval1 when selecting distortionvalue
        private void ComboBoxDistortionVal_SelectionChangeCommitted(object sender, EventArgs e)
        {
            arrayval1[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)ComboBoxDistortionVal.SelectedIndex;
            CheckError();
            Refresh();
        }
        // set arraygain when selecting distortiongain
        private void ComboBoxDistortionGain_SelectionChangeCommitted(object sender, EventArgs e)
        {
            arraygain[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)ComboBoxDistortionGain.SelectedIndex;
            if (ComboBoxDistortionGain.SelectedIndex > 0) hScrollBarDistortionGainValue.Visible = false; else hScrollBarDistortionGainValue.Visible = true;
            if (ComboBoxDistortionGain.SelectedIndex > 0) TextBoxDistortionGainValue.Visible = false; else TextBoxDistortionGainValue.Visible = true;
            Refresh();
        }
        // set arraygainvalue when scrolling distortiongain value
        private void hScrollBarDistortionGainValue_Scroll(object sender, ScrollEventArgs e)
        {
            arraygainval[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarDistortionGainValue.Value;
            TextBoxDistortionGainValue.Text = Convert.ToString(hScrollBarDistortionGainValue.Value);
        }

        // Clone sample control elements
        // combobox clone sample - set number of items
        private void ComboBoxCloneSamplenr_DropDown(object sender, EventArgs e)
        {
            ComboBoxCloneSamplenr.Items.Clear();
            for (int i = 0; i < ComboBoxSampleAuswahl.SelectedIndex; i++)
            {
                ComboBoxCloneSamplenr.Items.Add(i + 1);
            }
            Refresh();
        }
        // set arraygain when selecting samplenumber
        private void ComboBoxCloneSamplenr_SelectionChangeCommitted(object sender, EventArgs e)
        {
            arraygain[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)ComboBoxCloneSamplenr.SelectedIndex;
            // show selected sample name
            labelCloneName.Text = ComboBoxSampleAuswahl.Items[ComboBoxCloneSamplenr.SelectedIndex].ToString();

            int temp = samplelength[arraygain[ComboBoxSampleAuswahl.SelectedIndex, edit]] - 1;  // aus dem array
            if (temp < 0) temp = 0;
            hScrollBarCloneOffsetValue.Maximum = temp;
            if (hScrollBarCloneOffsetValue.Value > temp) hScrollBarCloneOffsetValue.Value = temp;
            hScrollBarCloneOffsetValue.PerformLayout(); //redraw
            TextBoxCloneOffsetValue.Text = Convert.ToString(hScrollBarCloneOffsetValue.Value);
            Refresh();
        }
        // set arraygainval when selecting reverse
        private void ComboBoxCloneReverse_SelectionChangeCommitted(object sender, EventArgs e)
        {
            arraygainval[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)ComboBoxCloneReverse.SelectedIndex;
            Refresh();
        }
        // set arrayfrequency when selecting transpose
        private void ComboBoxCloneTranspose_SelectionChangeCommitted(object sender, EventArgs e)
        {
            arrayfrequency[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)ComboBoxCloneTranspose.SelectedIndex;
            if (ComboBoxCloneTranspose.SelectedIndex > 0) hScrollBarCloneTransposeValue.Visible = false; else hScrollBarCloneTransposeValue.Visible = true;
            if (ComboBoxCloneTranspose.SelectedIndex > 0) TextBoxCloneTransposeValue.Visible = false; else TextBoxCloneTransposeValue.Visible = true;
            Refresh();
        }
        // set arrayfrequencyval when scrolling transpose
        private void hScrollBarCloneTransposeValue_Scroll(object sender, ScrollEventArgs e)
        {
            arrayfrequencyval[ComboBoxSampleAuswahl.SelectedIndex, edit] = (short)hScrollBarCloneTransposeValue.Value;
            TextBoxCloneTransposeValue.Text = Convert.ToString(hScrollBarCloneTransposeValue.Value);
        }
        // set arrayval2value when scrolling offset
        private void hScrollBarCloneOffsetValue_Scroll(object sender, ScrollEventArgs e)
        {
            //   int temp = samplelength[ComboBoxCloneSamplenr.SelectedIndex] - 1;  // hmm, besser aus dem array holen
            int temp = samplelength[arraygain[ComboBoxSampleAuswahl.SelectedIndex, edit]] - 1;  // aus dem array
            if (temp < 0) temp = 0;
            hScrollBarCloneOffsetValue.Maximum = temp;
            if (hScrollBarCloneOffsetValue.Value > temp) hScrollBarCloneOffsetValue.Value = temp;
            // only even values
            if (hScrollBarCloneOffsetValue.Value % 2 == 1) hScrollBarCloneOffsetValue.Value -= 1;
            arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, edit] = (short)hScrollBarCloneOffsetValue.Value;
            TextBoxCloneOffsetValue.Text = Convert.ToString(hScrollBarCloneOffsetValue.Value);
            //TextBoxCloneOffsetValue.Text = hScrollBarCloneOffsetValue.Value.ToString("X");
        }

        // Chord generator control elements
        // combobox clone sample - set number of items
        private void ComboBoxChordSamplenr_DropDown(object sender, EventArgs e)
        {
            ComboBoxChordSamplenr.Items.Clear();
            for (int i = 0; i < ComboBoxSampleAuswahl.SelectedIndex; i++)
            {
                ComboBoxChordSamplenr.Items.Add(i + 1);
            }
            Refresh();
        }
        // set arraygain when selecting samplenumber
        private void ComboBoxChordSamplenr_SelectionChangeCommitted(object sender, EventArgs e)
        {
            arraygain[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)ComboBoxChordSamplenr.SelectedIndex;
            // show selected sample name
            labelChordName.Text = ComboBoxSampleAuswahl.Items[ComboBoxChordSamplenr.SelectedIndex].ToString();
            Refresh();
        }
        // set arrayfrequency when selecting note1
        private void ComboBoxChordNote1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            arrayfrequency[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)ComboBoxChordNote1.SelectedIndex;
            Refresh();
        }
        // set arraywidth when selecting note2
        private void ComboBoxChordNote2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            arraywidth[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)ComboBoxChordNote2.SelectedIndex;
            Refresh();
        }
        // set arrayval1 when selecting note3
        private void ComboBoxChordNote3_SelectionChangeCommitted(object sender, EventArgs e)
        {
            arrayval1[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)ComboBoxChordNote3.SelectedIndex;
            Refresh();
        }
        // set arrayval2 when selecting shift
        private void ComboBoxChordShift_SelectionChangeCommitted(object sender, EventArgs e)
        {
            arrayval2[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)ComboBoxChordShift.SelectedIndex;
            if (ComboBoxChordShift.SelectedIndex > 0) hScrollBarChordShiftValue.Visible = false; else hScrollBarChordShiftValue.Visible = true;
            if (ComboBoxChordShift.SelectedIndex > 0) TextBoxChordShiftValue.Visible = false; else TextBoxChordShiftValue.Visible = true;
            Refresh();
        }
        // set arrayval2value when scrolling shiftvalue
        private void hScrollBarChordShiftValue_Scroll(object sender, ScrollEventArgs e)
        {
            arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarChordShiftValue.Value;
            TextBoxChordShiftValue.Text = Convert.ToString(hScrollBarChordShiftValue.Value);
        }


        // sample and hold control elements
        // set arrayval1 when selecting shval
        private void ComboBoxShVal_SelectionChangeCommitted(object sender, EventArgs e)
        {
            arrayval1[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)ComboBoxShVal.SelectedIndex;
            CheckError();
            Refresh();
        }
        // set arraygein when selecting shstep
        private void ComboBoxShStep_SelectionChangeCommitted(object sender, EventArgs e)
        {
            arraygain[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)ComboBoxShStep.SelectedIndex;
            if (ComboBoxShStep.SelectedIndex > 0) hScrollBarShStepValue.Visible = false; else hScrollBarShStepValue.Visible = true;
            if (ComboBoxShStep.SelectedIndex > 0) TextBoxShStepValue.Visible = false; else TextBoxShStepValue.Visible = true;
            Refresh();
        }
        // set arraygainvalue when scrolling shstep value
        private void hScrollBarShStepValue_Scroll(object sender, ScrollEventArgs e)
        {
            arraygainval[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarShStepValue.Value;
            TextBoxShStepValue.Text = Convert.ToString(hScrollBarShStepValue.Value);
        }

        // Import sample control elements
        // set arraygain when selecting importnumber
        private void ComboBoxImportNumber_SelectionChangeCommitted(object sender, EventArgs e)
        {
            arraygain[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)ComboBoxImportNumber.SelectedIndex;
            // show selected sample name
            labelImportedName.Text = comboBoxSelectImport.Items[ComboBoxImportNumber.SelectedIndex].ToString();
            Refresh();
        }

        // 1-Pole Filter control elements
        // set arrayval1 when selecting filtervalue
        private void ComboBoxOnepoleValue_SelectionChangeCommitted(object sender, EventArgs e)
        {
            arrayval1[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)ComboBoxOnepoleValue.SelectedIndex;
            CheckError();
            Refresh();
        }
        // set arrayfrequency when selecting filtercutoff
        private void ComboBoxOnepoleCutoff_SelectionChangeCommitted(object sender, EventArgs e)
        {
            arrayfrequency[ComboBoxSampleAuswahl.SelectedIndex, edit] = (short)ComboBoxOnepoleCutoff.SelectedIndex;
            if (ComboBoxOnepoleCutoff.SelectedIndex > 0) hScrollBarOnepoleCutoffValue.Visible = false; else hScrollBarOnepoleCutoffValue.Visible = true;
            if (ComboBoxOnepoleCutoff.SelectedIndex > 0) TextBoxOnepoleCutoffValue.Visible = false; else TextBoxOnepoleCutoffValue.Visible = true;
            Refresh();
        }
        // set arraygain when selecting filtermode
        private void ComboBoxOnepoleMode_SelectionChangeCommitted(object sender, EventArgs e)
        {
            arraygain[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)ComboBoxOnepoleMode.SelectedIndex;
            Refresh();
        }
        // set filterfrequencyvalue when scrolling filtercutoff value
        private void hScrollBarOnepoleCutoffValue_Scroll(object sender, ScrollEventArgs e)
        {
            arrayfrequencyval[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarOnepoleCutoffValue.Value;
            TextBoxOnepoleCutoffValue.Text = Convert.ToString(hScrollBarOnepoleCutoffValue.Value);
        }


        // Loop generator control elements
        private void hScrollBarLoopOffsetValue_Scroll(object sender, ScrollEventArgs e)
        {
            // only even values
            // if (hScrollBarLoopOffsetValue.Value % 2 == 1) hScrollBarLoopOffsetValue.Value += 1;
            // set max length for loop repeat and offset bars
            int temp = samplelength[selectedindex] - 2;
            if (temp < 0) temp = 0;
            hScrollBarLoopOffsetValue.Maximum = temp;

            if (hScrollBarLoopOffsetValue.Value < (samplelength[selectedindex]) / 2)
                hScrollBarLoopOffsetValue.Value = ((samplelength[selectedindex]) / 2) - 1;
            // only even values
            if (hScrollBarLoopOffsetValue.Value % 2 == 1) hScrollBarLoopOffsetValue.Value += 1;

            looplength[ComboBoxSampleAuswahl.SelectedIndex] = (samplelength[ComboBoxSampleAuswahl.SelectedIndex] - hScrollBarLoopOffsetValue.Value);
            loopoffset[ComboBoxSampleAuswahl.SelectedIndex] = hScrollBarLoopOffsetValue.Value;
            TextBoxLoopOffsetValue.Text = hScrollBarLoopOffsetValue.Value.ToString("X");
            TextBoxLoopLengthValue.Text = (samplelength[ComboBoxSampleAuswahl.SelectedIndex] - hScrollBarLoopOffsetValue.Value).ToString("X");
            hScrollBarLoopOffsetValue.Minimum = ((samplelength[selectedindex]) / 2) - 1;
            pictureBox1.Invalidate();
        }


        // Envelope control elements
        // set arrayval2value when scrolling envelope attack value
        private void hScrollBarEnvelopeAttackValue_Scroll(object sender, ScrollEventArgs e)
        {
            arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarEnvelopeAttackValue.Value;
            TextBoxEnvelopeAttackValue.Text = Convert.ToString(hScrollBarEnvelopeAttackValue.Value);
        }

        // set arrayval1value when scrolling envelope decay value
        private void hScrollBarEnvelopeDecayValue_Scroll(object sender, ScrollEventArgs e)
        {
            arrayval1value[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarEnvelopeDecayValue.Value;
            TextBoxEnvelopeDecayValue.Text = Convert.ToString(hScrollBarEnvelopeDecayValue.Value);
        }
        // set arraywidthval when scrolling envelope sustain level value
        private void hScrollBarEnvelopeSustainValue_Scroll(object sender, ScrollEventArgs e)
        {
            arraywidthval[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarEnvelopeSustainValue.Value;
            TextBoxEnvelopeSustainValue.Text = Convert.ToString(hScrollBarEnvelopeSustainValue.Value);
        }
        // set arrayfrequencyval when scrolling envelope release value
        private void hScrollBarEnvelopeReleaseValue_Scroll(object sender, ScrollEventArgs e)
        {
            arrayfrequencyval[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarEnvelopeReleaseValue.Value;
            TextBoxEnvelopeReleaseValue.Text = Convert.ToString(hScrollBarEnvelopeReleaseValue.Value);
        }
        // set arraygainval when scrolling envelope gain value
        private void hScrollBarEnvelopeGainValue_Scroll(object sender, ScrollEventArgs e)
        {
            arraygainval[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarEnvelopeGainValue.Value;
            TextBoxEnvelopeGainValue.Text = Convert.ToString(hScrollBarEnvelopeGainValue.Value);
        }

        // Vocoder control elements
        // set arrayval1 when selecting modulator
        private void ComboBoxVocoderModulator_SelectionChangeCommitted(object sender, EventArgs e)
        {
            arrayval1[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)ComboBoxVocoderModulator.SelectedIndex;
            CheckError();
            Refresh();
        }
        // set arrayval2 when selecting carrier
        private void ComboBoxVocoderCarrier_SelectionChangeCommitted(object sender, EventArgs e)
        {
            arrayval2[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)ComboBoxVocoderCarrier.SelectedIndex;
            CheckError();
            Refresh();
        }



        // edit text in sampleauswahl
        private void ComboBoxSampleAuswahl_KeyDown(object sender, KeyEventArgs e)
        {
            string s = ComboBoxSampleAuswahl.Text;
            if (e.KeyCode == Keys.Enter)
            {
                // if item exists, select it. if it does not exist, add it.
                if (!ComboBoxSampleAuswahl.Items.Contains(s))
                {
                    ComboBoxSampleAuswahl.Items.Remove(selecteditem);
                    ComboBoxSampleAuswahl.Items.Insert(selectedindex, s);
                    ComboBoxSampleAuswahl.SelectedIndex = selectedindex;
                }
            }
        }
        // wiggle with selected index error
        private void ComboBoxSampleAuswahl_Leave(object sender, EventArgs e)
        {
            ComboBoxSampleAuswahl.SelectedIndex = selectedindex;
        }


        // edit text in selectimport
        private void comboBoxSelectImport_KeyDown(object sender, KeyEventArgs e)
        {
            string s = comboBoxSelectImport.Text;
            if (e.KeyCode == Keys.Enter)
            {
                // if item exists, select it. if it does not exist, add it.
                if (!comboBoxSelectImport.Items.Contains(s))
                {
                    comboBoxSelectImport.Items.Remove(selecteditem2);
                    comboBoxSelectImport.Items.Insert(selectedindex2, s);
                    comboBoxSelectImport.SelectedIndex = selectedindex2;
                }
            }
        }
        // wiggle with selected index error
        private void comboBoxSelectImport_Leave(object sender, EventArgs e)
        {
            comboBoxSelectImport.SelectedIndex = selectedindex2;
        }


        // samplerate 
        private void ComboBoxNotes_SelectedIndexChanged(object sender, EventArgs e)
        {
            calcsamplerate();
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            calcsamplerate();
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            calcsamplerate();
        }
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            calcsamplerate();
        }


        // ********************** play sample *********************************************************************************************

        // button play selected sample
        private void buttonPlaySample_Click(object sender, EventArgs e)
        {
            // move data from 2d array to a new 1d array

            buttonStopSample.Visible = true;
            buttonPlaySample.Visible = false;
            //  Cursor.Current = Cursors.WaitCursor;
            if (ComboBoxSampleAuswahl.SelectedIndex >= 0 && ComboBoxSampleAuswahl.SelectedIndex <= 0x1f)
            {
                int temp = (looplength[ComboBoxSampleAuswahl.SelectedIndex] + loopoffset[ComboBoxSampleAuswahl.SelectedIndex]);
                // if (looplength[ComboBoxSampleAuswahl.SelectedIndex] > 2)
                if (arrayfunction[selectedindex, 15] == 22&&radioButtonOutput.Checked==true)  // is loopgen used ?
                {
                    for (int i = 0; i < temp; i++)
                    {
                        actsample[i] = sample[i, ComboBoxSampleAuswahl.SelectedIndex];
                    }

                    for (int i = 0; i < looplength[ComboBoxSampleAuswahl.SelectedIndex]; i++)
                    {
                        actsample[i + temp] = sample[i + loopoffset[ComboBoxSampleAuswahl.SelectedIndex], ComboBoxSampleAuswahl.SelectedIndex];
                    }



                    Class1.PlayPCM(actsample, temp + looplength[ComboBoxSampleAuswahl.SelectedIndex], samplerate);
                }
                else  // without loop
                {
                    for (int i = 0; i < samplelength[ComboBoxSampleAuswahl.SelectedIndex]; i++)
                    {
                        actsample[i] = visuakt[i];
                        //actsample[i] = sample[i, ComboBoxSampleAuswahl.SelectedIndex];
                    }
                    Class1.PlayPCM(actsample, samplelength[ComboBoxSampleAuswahl.SelectedIndex], samplerate);
                }
            }
            // Cursor.Current = Cursors.Default;
        }





        /*
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                Cursor.Current = Cursors.WaitCursor;
                if (ComboBoxSampleAuswahl.SelectedIndex >= 0 && ComboBoxSampleAuswahl.SelectedIndex <= 0x1f)
                {
                    for (int i = 0; i < 0xffff; i++) // move data from 2d array to a new 1d array
                    {
                        actsample[i] = sample[i, ComboBoxSampleAuswahl.SelectedIndex];
                    }

                    Class1.PlayPCM(actsample, samplelength[ComboBoxSampleAuswahl.SelectedIndex], samplerate);
                }

            }
            Cursor.Current = Cursors.Default;
        }
        */

        // ********************** Menu ***************************************************************************************************

        // info
        private void toolStripMenuItemInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Alcatraz AmigaKlangGUI \r\n\r\n(C) Jochen ´Virgill´ Feldkötter 2022\r\nwww.soundcloud.com/virgill\r\n\r\nWouldn´t have been possible without:\r\nDan,Hellfire,Gopher,Bartman,Antiriad\r\nLeonard\r\n\r\nThanks for nice tipps:\r\nSoundy, Leonard, H0ffman, Bifat,\r\nStingray, Aceman, Juice, Tecon\r\n\r\nShrinkler compressor by Blueberry\r\nAklang2ASM by Dan/Lemon.\r\nLSP, Atari playback STrinkler\r\nby Leonard/Oxygene");
        }


        // save raw sample dialog
        private void toolStripMenuItemSaveSample_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = ComboBoxSampleAuswahl.Text + ".raw";
            saveFileDialog1.Filter = "raw (*.raw)|*.raw|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Title = "Save actual sample";

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Stream myStream;
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    BinaryWriter writer = new BinaryWriter(myStream);
                    for (int i = 0; i < samplelength[ComboBoxSampleAuswahl.SelectedIndex]; i++)
                    {
                        writer.Write(sample[i, ComboBoxSampleAuswahl.SelectedIndex]);
                    }
                    myStream.Close();
                }
            }
        }


        // import sample dialog (wav or raw)
        private void buttonImportSample_Click(object sender, EventArgs e)
        {

            openFileDialog1.Filter = "wav (*.wav)|*.wav|raw (*.raw)|*.raw";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Title = "Import sample";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (openFileDialog1.FilterIndex == 1) // open wav
                {
                    Stream myStream;
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        if (myStream.Length > 65535)
                        {
                            MessageBox.Show("Error, sample too big!");
                            return;
                        }
                        WaveFileReader reader = new WaveFileReader(myStream);
                        int SampleRate = reader.WaveFormat.SampleRate; // get samplerate from the stream
                        WaveFormat newFormat = new WaveFormat();
                        WaveFormat newFormat2 = new WaveFormat(SampleRate, 8, 1);
                        WaveFormatConversionStream str = new WaveFormatConversionStream(newFormat, reader);
                        WaveFormatConversionStream str2 = new WaveFormatConversionStream(newFormat2, str);  // convert 2 times..  Needed..
                        for (int i = 0; i < (int)str2.Length; i++)
                        {
                            byte temp = (byte)str2.ReadByte();
                            temp += 128; //convert to sbyte amiga format
                            importedsample[i, comboBoxSelectImport.SelectedIndex] = temp;
                        }
                        importedlength[comboBoxSelectImport.SelectedIndex] = (int)str2.Length;
                        TextBoxImportSampleLength.Text = str2.Length.ToString("X");
                        // replace name in combobox
                        string s = System.IO.Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
                        if (!comboBoxSelectImport.Items.Contains(s))
                        {
                            comboBoxSelectImport.Items.Remove(selecteditem2);
                            comboBoxSelectImport.Items.Insert(selectedindex2, s);
                            comboBoxSelectImport.SelectedIndex = selectedindex2;
                        }
                        myStream.Close();
                    }
                }

                if (openFileDialog1.FilterIndex == 2) // open raw
                {
                    Stream myStream;
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        if (myStream.Length > 65535)
                        {
                            MessageBox.Show("Error, sample too big!");
                            return;
                        }
                        BinaryReader reader = new BinaryReader(myStream);
                        for (int i = 0; i < (int)myStream.Length; i++)
                        {
                            importedsample[i, comboBoxSelectImport.SelectedIndex] = reader.ReadByte();
                        }
                        importedlength[comboBoxSelectImport.SelectedIndex] = (int)myStream.Length;
                        TextBoxImportSampleLength.Text = myStream.Length.ToString("X");
                        // replace name in combobox
                        string s = System.IO.Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
                        if (!comboBoxSelectImport.Items.Contains(s))
                        {
                            comboBoxSelectImport.Items.Remove(selecteditem2);
                            comboBoxSelectImport.Items.Insert(selectedindex2, s);
                            comboBoxSelectImport.SelectedIndex = selectedindex2;
                        }
                        myStream.Close();
                    }
                }
            }
            updatetotalsize();
        }

        //************************************************************************************************************************************


        // clear imported sample
        private void buttonClearImportSample_Click(object sender, EventArgs e)
        {
            importedlength[selectedindex2] = 0;
            // replace name in combobox
            string s = "Sample_" + (comboBoxSelectImport.SelectedIndex + 1).ToString();
            if (!comboBoxSelectImport.Items.Contains(s))
            {
                comboBoxSelectImport.Items.Remove(selecteditem2);
                comboBoxSelectImport.Items.Insert(selectedindex2, s);
                comboBoxSelectImport.SelectedIndex = selectedindex2;
            }
            comboBoxSelectImport_SelectedIndexChanged(sender, e);
            //updatetotalsize();
        }


        // save instrument dialog
        private void toolStripMenuItemSaveInstrument_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = ComboBoxSampleAuswahl.Text + ".aki";
            saveFileDialog1.Filter = "aki (*.aki)|*.aki|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Title = "Save actual instrument";
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Stream myStream;
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    BinaryWriter writer = new BinaryWriter(myStream);
                    writer.Write((int)47110816); // write magic number
                    //int j = ComboBoxSampleAuswahl.SelectedIndex;
                    int j = selectedindex;
                    writer.Write(samplelength[j]);
                    for (int i = 0; i < 20; i++)
                    {
                        writer.Write(arrayvar[j, i]);
                        writer.Write(arrayfunction[j, i]);
                        writer.Write(arrayinstance[j, i]);
                        writer.Write(arrayfrequency[j, i]);
                        writer.Write(arrayfrequencyval[j, i]);
                        writer.Write(arraygain[j, i]);
                        writer.Write(arraygainval[j, i]);
                        writer.Write(arraywidth[j, i]);
                        writer.Write(arraywidthval[j, i]);
                        writer.Write(arrayval1[j, i]);
                        writer.Write(arrayval1value[j, i]);
                        writer.Write(arrayval2[j, i]);
                        writer.Write(arrayval2value[j, i]);
                        writer.Write(loopoffset[j]);
                        writer.Write(looplength[j]);
                    }
                    myStream.Close();
                }
            }
        }

        // save patch dialog
        private void toolStripMenuItemSavePatch_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "patch.akp";
            saveFileDialog1.Filter = "akp (*.akp)|*.akp|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Title = "Save whole patch";

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Stream myStream;
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    BinaryWriter writer = new BinaryWriter(myStream);
                    writer.Write((int)47110815); // write magic number
                    for (int j = 0; j < 31; j++)
                    {
                        // write samplenames
                        //ComboBoxSampleAuswahl.SelectedIndex = j;
                        //string selectedItem = ComboBoxSampleAuswahl.SelectedItem.ToString();

                        string selectedItem = ComboBoxSampleAuswahl.Items[j].ToString();
                        writer.Write(selectedItem);

                        writer.Write(samplelength[j]);
                        for (int i = 0; i < 20; i++)
                        {
                            writer.Write(arrayvar[j, i]);
                            writer.Write(arrayfunction[j, i]);
                            writer.Write(arrayinstance[j, i]);
                            writer.Write(arrayfrequency[j, i]);
                            writer.Write(arrayfrequencyval[j, i]);
                            writer.Write(arraygain[j, i]);
                            writer.Write(arraygainval[j, i]);
                            writer.Write(arraywidth[j, i]);
                            writer.Write(arraywidthval[j, i]);
                            writer.Write(arrayval1[j, i]);
                            writer.Write(arrayval1value[j, i]);
                            writer.Write(arrayval2[j, i]);
                            writer.Write(arrayval2value[j, i]);
                            writer.Write(loopoffset[j]);
                            writer.Write(looplength[j]);
                        }
                    }
                    //*************************************************************
                    // write imported samples ... seems to work
                    //*************************************************************
                    for (int j = 0; j < 8; j++)
                    {
                        // write samplenames
                        comboBoxSelectImport.SelectedIndex = j;
                        string selectedItem = comboBoxSelectImport.SelectedItem.ToString();
                        writer.Write(selectedItem);
                        writer.Write(importedlength[j]);
                        for (int smp = 0; smp < importedlength[j]; smp++)
                        {
                            writer.Write(importedsample[smp, j]);
                        }
                    }
                    //*************************************************************
                    myStream.Close();
                    ComboBoxSampleAuswahl.SelectedIndex = 0;
                    comboBoxSelectImport.SelectedIndex = 0;
                }
            }
        }



        // load patch dialog
        private void toolStripMenuItemLoadPatch_Click(object sender, EventArgs e)
        {
            radioButtonOutput.Checked = true;
            hideGroupBoxes();
            openFileDialog1.Filter = "akp (*.akp)|*.akp|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Title = "Load whole patch";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Stream myStream;
                if ((myStream = openFileDialog1.OpenFile()) != null)
                {
                    BinaryReader reader = new BinaryReader(myStream);

                    int test = reader.ReadInt32(); // read magic number
                    if (test != 47110815)
                    {
                        MessageBox.Show("Error, wrong file"); return;
                    }
                    ComboBoxSampleAuswahl.Items.Clear();

                    for (int j = 0; j < 31; j++)
                    {
                        ComboBoxSampleAuswahl.Items.Add(reader.ReadString());

                        samplelength[j] = reader.ReadInt32();
                        for (int i = 0; i < 20; i++)
                        {
                            arrayvar[j, i] = reader.ReadInt32();
                            arrayfunction[j, i] = reader.ReadInt32();
                            arrayinstance[j, i] = reader.ReadByte();
                            arrayfrequency[j, i] = reader.ReadInt16();
                            arrayfrequencyval[j, i] = reader.ReadInt16();
                            arraygain[j, i] = reader.ReadByte();
                            arraygainval[j, i] = reader.ReadByte();
                            arraywidth[j, i] = reader.ReadByte();
                            arraywidthval[j, i] = reader.ReadByte();
                            arrayval1[j, i] = reader.ReadInt16();
                            arrayval1value[j, i] = reader.ReadInt16();
                            arrayval2[j, i] = reader.ReadInt16();
                            arrayval2value[j, i] = reader.ReadInt16();
                            loopoffset[j] = reader.ReadInt32();
                            looplength[j] = reader.ReadInt32();
                        }
                    }

                    //*************************************************************
                    // read imported samples ... test
                    //*************************************************************
                    if (myStream.Position != myStream.Length) // check if it´s old patch data (without imported samples)
                    {
                        comboBoxSelectImport.Items.Clear();

                        for (int j = 0; j < 8; j++)
                        {
                            comboBoxSelectImport.Items.Add(reader.ReadString());
                            importedlength[j] = reader.ReadInt32();

                            for (int smp = 0; smp < importedlength[j]; smp++)
                            {
                                importedsample[smp, j] = reader.ReadByte();
                            }
                        }
                    }
                    //*************************************************************

                    myStream.Close();
                    checkloopparams();
                    ComboBoxSampleAuswahl.SelectedIndex = 0;
                    comboBoxSelectImport.SelectedIndex = 0;
                }
            }
            //  ComboBoxSampleAuswahl_SelectedIndexChanged(sender, e); // trigger sampleauswahl to update screen
            updatetotalsize();
            CheckError();
        }


        // load instrument dialog
        private void toolStripMenuItemLoadInstrument_Click(object sender, EventArgs e)
        {
            hideGroupBoxes();
            openFileDialog1.Filter = "aki (*.aki)|*.aki|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Title = "Load instrument";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Stream myStream;
                if ((myStream = openFileDialog1.OpenFile()) != null)
                {
                    BinaryReader reader = new BinaryReader(myStream);
                    var size = new FileInfo(openFileDialog1.FileName).Length;
                    if (size != 668)
                    {
                        MessageBox.Show("Error, wrong file or corrupted"); return;
                    }

                    int test = reader.ReadInt32(); // read magic number
                    if (test != 47110816)
                    {
                        MessageBox.Show("Error, wrong file"); return;
                    }

                    int j = selectedindex;
                    ComboBoxSampleAuswahl.SelectedIndex = j;
                    samplelength[j] = reader.ReadInt32();
                    for (int i = 0; i < 20; i++)
                    {
                        arrayvar[j, i] = reader.ReadInt32();
                        arrayfunction[j, i] = reader.ReadInt32();
                        arrayinstance[j, i] = reader.ReadByte();
                        arrayfrequency[j, i] = reader.ReadInt16();
                        arrayfrequencyval[j, i] = reader.ReadInt16();
                        arraygain[j, i] = reader.ReadByte();
                        arraygainval[j, i] = reader.ReadByte();
                        arraywidth[j, i] = reader.ReadByte();
                        arraywidthval[j, i] = reader.ReadByte();
                        arrayval1[j, i] = reader.ReadInt16();
                        arrayval1value[j, i] = reader.ReadInt16();
                        arrayval2[j, i] = reader.ReadInt16();
                        arrayval2value[j, i] = reader.ReadInt16();
                        loopoffset[j] = reader.ReadInt32();
                        looplength[j] = reader.ReadInt32();
                    }
                    checkloopparams();
                    // replace name in combobox
                    string s = System.IO.Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
                    if (!ComboBoxSampleAuswahl.Items.Contains(s))
                    {
                        ComboBoxSampleAuswahl.Items.Remove(selecteditem);
                        ComboBoxSampleAuswahl.Items.Insert(selectedindex, s);
                        ComboBoxSampleAuswahl.SelectedIndex = selectedindex;
                    }
                    myStream.Close();
                }
            }
            ComboBoxSampleAuswahl_SelectedIndexChanged(sender, e); // trigger sampleauswahl to update screen
            updatetotalsize();
            CheckError();
        }


        // export to mod dialog
        private void toolStripMenuItemExport_Click(object sender, EventArgs e)
        {
            MessageBox.Show("choose a mod file.   Warning!!! samples will be overwritten!!!");
            long filelength = 0;
            string filename;
            hideGroupBoxes();
            openFileDialog1.Filter = "mod (*.mod)|*.mod|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Title = "Export samples to amiga mod";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Stream myStream;
                if ((myStream = openFileDialog1.OpenFile()) != null)
                {
                    filename = openFileDialog1.FileName;
                    filelength = myStream.Length;
                    BinaryReader reader = new BinaryReader(myStream);

                    for (int i = 0; i < filelength; i++)
                    {
                        amigamod[i] = reader.ReadByte();
                    }
                    myStream.Close();

                    // check if really a mod file (M.K.) or (M!K!) <- for mods with more than 64 pattern
                    if (!(amigamod[1080] == 77 && amigamod[1081] == 46 && amigamod[1082] == 75 && amigamod[1083] == 46))
                    {
                        if (!(amigamod[1080] == 77 && amigamod[1081] == 33 && amigamod[1082] == 75 && amigamod[1083] == 33))
                        {
                            MessageBox.Show("Not an Amiga mod file!!"); return;
                        }
                    }

                    // find out highest pattern number
                    byte maxpattern = 0;
                    for (int i = 952; i <= 1079; i++)
                    {
                        if (amigamod[i] > maxpattern) maxpattern = amigamod[i];
                    }
                    maxpattern += 1;
                    // calculate position where samples start
                    int sampleposition = 1084 + (maxpattern * 1024);

                    long newlength = sampleposition;
                    // write samplelength and loop data
                    byte[] temp = new byte[2];
                    for (int i = 0; i < 31; i++)
                    {
                        // samplelength
                        temp = BitConverter.GetBytes((UInt16)(samplelength[i] >> 1));
                        amigamod[42 + (30 * i)] = temp[1];
                        amigamod[43 + (30 * i)] = temp[0];
                        // loop offset
                        if (arrayfunction[i, 15] == 22)  // is loopgen usen ?
                        {
                            temp = BitConverter.GetBytes((UInt16)(loopoffset[i] >> 1));
                            amigamod[46 + (30 * i)] = temp[1];
                            amigamod[47 + (30 * i)] = temp[0];
                            // loop length
                            temp = BitConverter.GetBytes((UInt16)(looplength[i] >> 1));
                            amigamod[48 + (30 * i)] = temp[1];
                            amigamod[49 + (30 * i)] = temp[0];
                        }
                        // sample
                        for (int j = 0; j < samplelength[i]; j++)
                        {
                            amigamod[newlength + j] = sample[j, i];
                        }
                        // length of mod + sampledata
                        newlength += samplelength[i];
                    }

                    using (BinaryWriter writer = new BinaryWriter(File.Open(filename, FileMode.Create)))
                    {
                        for (int i = 0; i < newlength; i++)
                        {
                            writer.Write(amigamod[i]);
                        }
                    }
                }
            }
        }


        //****************************************************************************************************************
        // export executable
        //****************************************************************************************************************

        private void exportGeneratorFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CheckErrorSave() == true) { MessageBox.Show("Error in instruments"); return; }

            char temp = ' ';
            // make ilen table
            TextBoxTranslateIlen.Clear();
            for (int i = 0; i < getnumberofhighestinstrument(); i++)
            {
                TextBoxTranslateIlen.Text = TextBoxTranslateIlen.Text + "// " + ComboBoxSampleAuswahl.Items[i].ToString() + "\r\n";    // instrument name
                TextBoxTranslateIlen.Text = TextBoxTranslateIlen.Text + "SmpLength[" + i.ToString() + "] = 0x" + samplelength[i].ToString("X") + ";\r\n";   // sample length
                TextBoxTranslateIlen.Text = TextBoxTranslateIlen.Text + "repeat_offset[" + i.ToString() + "] = 0x" + loopoffset[i].ToString("X") + ";\r\n"; // loop offset
                TextBoxTranslateIlen.Text = TextBoxTranslateIlen.Text + "repeat_length[" + i.ToString() + "] = 0x" + looplength[i].ToString("X") + ";\r\n"; // loop length
                if (arrayfunction[i, 15] == 22) temp = 'l'; else temp = ' '; // is loopgen usen ?
                TextBoxTranslateIlen.Text = TextBoxTranslateIlen.Text + "samplename_flag[" + i.ToString() + "] = '" + temp + "';\r\n"; // loop flag (loopgen used)
                TextBoxTranslateIlen.Text = TextBoxTranslateIlen.Text + "\r\n";
            }
            // write imported length
            for (int i = 0; i < 8; i++)
            {
                TextBoxTranslateIlen.Text = TextBoxTranslateIlen.Text + "ImpLength[" + i.ToString() + "] = 0x" + importedlength[i].ToString("X") + ";\r\n";   // imported length
                TextBoxTranslateIlen.Text = TextBoxTranslateIlen.Text + "\r\n";
            }

            // make inst table
            TextBoxTranslateInst.Clear();
            for (int i = 0; i < 31; i++)
            {
                if (samplelength[i] > 2)
                {
                    TextBoxTranslateInst.Text = TextBoxTranslateInst.Text + "// " + ComboBoxSampleAuswahl.Items[i].ToString() + "\r\n";    // instrument name
                    TextBoxTranslateInst.Text = TextBoxTranslateInst.Text + "if (instrument == " + i.ToString() + ") {\r\n";
                    for (int j = 0; j < 16; j++)
                    {
                        if (arrayvar[i, j] != 0 && arrayfunction[i, j] != 22)
                        {
                            TextBoxTranslateInst.Text = TextBoxTranslateInst.Text + arrayvartext[arrayvar[i, j]] + " = " + arrayfunctiontext[arrayfunction[i, j]] + "(";
                            switch (arrayfunction[i, j])
                            {
                                case 0: break;
                                case 1: // vol
                                    {
                                        TextBoxTranslateInst.Text += arrayvartext[arrayval1[i, j]] + ", ";
                                        //if (arrayval1[i, j] == 0) { MessageBox.Show("Error in instrument " + (i + 1).ToString()); return; }
                                        if (arraygain[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arraygain[i, j]]; else TextBoxTranslateInst.Text += arraygainval[i, j].ToString();
                                        TextBoxTranslateInst.Text += ");" + "\r\n";
                                        break;
                                    }
                                case 2: // osc saw
                                    {
                                        TextBoxTranslateInst.Text += j.ToString() + ", "; // instance
                                        if (arrayfrequency[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arrayfrequency[i, j]]; else TextBoxTranslateInst.Text += arrayfrequencyval[i, j].ToString();
                                        TextBoxTranslateInst.Text += ", ";
                                        if (arraygain[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arraygain[i, j]]; else TextBoxTranslateInst.Text += arraygainval[i, j].ToString();
                                        TextBoxTranslateInst.Text += ");" + "\r\n";
                                        break;
                                    }
                                case 3: // osc tri
                                    {
                                        TextBoxTranslateInst.Text += j.ToString() + ", "; // instance
                                        if (arrayfrequency[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arrayfrequency[i, j]]; else TextBoxTranslateInst.Text += arrayfrequencyval[i, j].ToString();
                                        TextBoxTranslateInst.Text += ", ";
                                        if (arraygain[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arraygain[i, j]]; else TextBoxTranslateInst.Text += arraygainval[i, j].ToString();
                                        TextBoxTranslateInst.Text += ");" + "\r\n";
                                        break;
                                    }
                                case 4: // osc sine
                                    {
                                        TextBoxTranslateInst.Text += j.ToString() + ", "; // instance
                                        if (arrayfrequency[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arrayfrequency[i, j]]; else TextBoxTranslateInst.Text += arrayfrequencyval[i, j].ToString();
                                        TextBoxTranslateInst.Text += ", ";
                                        if (arraygain[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arraygain[i, j]]; else TextBoxTranslateInst.Text += arraygainval[i, j].ToString();
                                        TextBoxTranslateInst.Text += ");" + "\r\n";
                                        break;
                                    }
                                case 5: // osc pulse
                                    {
                                        TextBoxTranslateInst.Text += j.ToString() + ", "; // instance
                                        if (arrayfrequency[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arrayfrequency[i, j]]; else TextBoxTranslateInst.Text += arrayfrequencyval[i, j].ToString();
                                        TextBoxTranslateInst.Text += ", ";
                                        if (arraygain[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arraygain[i, j]]; else TextBoxTranslateInst.Text += arraygainval[i, j].ToString();
                                        TextBoxTranslateInst.Text += ", ";
                                        if (arraywidth[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arraywidth[i, j]]; else TextBoxTranslateInst.Text += arraywidthval[i, j].ToString();
                                        TextBoxTranslateInst.Text += ");" + "\r\n";
                                        break;
                                    }
                                case 6: // osc noise
                                    {
                                        TextBoxTranslateInst.Text += "smp" + ", "; // smp
                                        if (arraygain[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arraygain[i, j]]; else TextBoxTranslateInst.Text += arraygainval[i, j].ToString();
                                        TextBoxTranslateInst.Text += ");" + "\r\n";
                                        break;
                                    }
                                case 7: // enva
                                    {
                                        TextBoxTranslateInst.Text += "smp" + ", "; // smp
                                        if (arrayval1[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arrayval1[i, j]]; else TextBoxTranslateInst.Text += arrayval1value[i, j].ToString();
                                        TextBoxTranslateInst.Text += ", ";
                                        TextBoxTranslateInst.Text += "0, ";
                                        if (arraygain[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arraygain[i, j]]; else TextBoxTranslateInst.Text += arraygainval[i, j].ToString();
                                        TextBoxTranslateInst.Text += ");" + "\r\n";
                                        break;
                                    }
                                case 8: // envd
                                    {
                                        TextBoxTranslateInst.Text += "smp" + ", "; // smp
                                        if (arrayval1[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arrayval1[i, j]]; else TextBoxTranslateInst.Text += arrayval1value[i, j].ToString();
                                        TextBoxTranslateInst.Text += ", ";
                                        if (arrayval2[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arrayval2[i, j]]; else TextBoxTranslateInst.Text += arrayval2value[i, j].ToString();
                                        TextBoxTranslateInst.Text += ", ";
                                        if (arraygain[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arraygain[i, j]]; else TextBoxTranslateInst.Text += arraygainval[i, j].ToString();
                                        TextBoxTranslateInst.Text += ");" + "\r\n";
                                        break;
                                    }
                                case 9: // add
                                    {
                                        TextBoxTranslateInst.Text += arrayvartext[arrayval1[i, j]];
                                        //if (arrayval1[i, j] == 0) { MessageBox.Show("Error in instrument " + (i + 1).ToString()); return; }
                                        TextBoxTranslateInst.Text += ", ";
                                        if (arrayval2[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arrayval2[i, j]]; else TextBoxTranslateInst.Text += arrayval2value[i, j].ToString();
                                        TextBoxTranslateInst.Text += ");" + "\r\n";
                                        break;
                                    }
                                case 10: // mul
                                    {
                                        TextBoxTranslateInst.Text += arrayvartext[arrayval1[i, j]];
                                        //if (arrayval1[i, j] == 0) { MessageBox.Show("Error in instrument " + (i + 1).ToString()); return; }
                                        TextBoxTranslateInst.Text += ", ";
                                        if (arrayval2[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arrayval2[i, j]]; else TextBoxTranslateInst.Text += arrayval2value[i, j].ToString();
                                        TextBoxTranslateInst.Text += ");" + "\r\n";
                                        break;
                                    }
                                case 11: // delay
                                    {
                                        TextBoxTranslateInst.Text += j.ToString() + ", "; // instance
                                        TextBoxTranslateInst.Text += arrayvartext[arrayval1[i, j]];
                                        if (arrayval1[i, j] == 0) { MessageBox.Show("Error in instrument " + (i + 1).ToString()); return; }
                                        TextBoxTranslateInst.Text += ", ";
                                        if (arrayfrequency[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arrayfrequency[i, j]]; else TextBoxTranslateInst.Text += arrayfrequencyval[i, j].ToString();
                                        TextBoxTranslateInst.Text += ", ";
                                        if (arraygain[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arraygain[i, j]]; else TextBoxTranslateInst.Text += arraygainval[i, j].ToString();
                                        TextBoxTranslateInst.Text += ");" + "\r\n";
                                        break;
                                    }
                                case 12: // comb
                                    {
                                        TextBoxTranslateInst.Text += j.ToString() + ", "; // instance
                                        TextBoxTranslateInst.Text += arrayvartext[arrayval1[i, j]];
                                        //if (arrayval1[i, j] == 0) { MessageBox.Show("Error in instrument " + (i + 1).ToString()); return; }
                                        TextBoxTranslateInst.Text += ", ";
                                        if (arrayfrequency[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arrayfrequency[i, j]]; else TextBoxTranslateInst.Text += arrayfrequencyval[i, j].ToString();
                                        TextBoxTranslateInst.Text += ", ";
                                        if (arrayval2[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arrayval2[i, j]]; else TextBoxTranslateInst.Text += arrayval2value[i, j].ToString();
                                        TextBoxTranslateInst.Text += ", ";
                                        if (arraygain[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arraygain[i, j]]; else TextBoxTranslateInst.Text += arraygainval[i, j].ToString();
                                        TextBoxTranslateInst.Text += ");" + "\r\n";
                                        break;
                                    }
                                case 13: // reverb
                                    {
                                        TextBoxTranslateInst.Text += arrayvartext[arrayval1[i, j]];
                                        if (arrayval1[i, j] == 0) { MessageBox.Show("Error in instrument " + (i + 1).ToString()); return; }
                                        TextBoxTranslateInst.Text += ", ";
                                        if (arrayval2[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arrayval2[i, j]]; else TextBoxTranslateInst.Text += arrayval2value[i, j].ToString();
                                        TextBoxTranslateInst.Text += ", ";
                                        if (arraygain[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arraygain[i, j]]; else TextBoxTranslateInst.Text += arraygainval[i, j].ToString();
                                        TextBoxTranslateInst.Text += ");" + "\r\n";
                                        break;
                                    }
                                case 14: // ctrl
                                    {
                                        TextBoxTranslateInst.Text += arrayvartext[arrayval1[i, j]];
                                        //if (arrayval1[i, j] == 0) { MessageBox.Show("Error in instrument " + (i + 1).ToString()); return; }
                                        TextBoxTranslateInst.Text += ");" + "\r\n";
                                        break;
                                    }
                                case 15: // filter
                                    {
                                        TextBoxTranslateInst.Text += j.ToString() + ", "; // instance
                                        TextBoxTranslateInst.Text += arrayvartext[arrayval1[i, j]];
                                        if (arrayval1[i, j] == 0) { MessageBox.Show("Error in instrument " + (i + 1).ToString()); return; }
                                        TextBoxTranslateInst.Text += ", ";
                                        if (arrayfrequency[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arrayfrequency[i, j]]; else TextBoxTranslateInst.Text += arrayfrequencyval[i, j].ToString();
                                        TextBoxTranslateInst.Text += ", ";
                                        if (arrayval2[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arrayval2[i, j]]; else TextBoxTranslateInst.Text += arrayval2value[i, j].ToString();
                                        TextBoxTranslateInst.Text += ", ";
                                        TextBoxTranslateInst.Text += arraygain[i, j].ToString();
                                        TextBoxTranslateInst.Text += ");" + "\r\n";
                                        break;
                                    }
                                case 16: // distortion
                                    {
                                        TextBoxTranslateInst.Text += arrayvartext[arrayval1[i, j]] + ", ";
                                        //if (arrayval1[i, j] == 0) { MessageBox.Show("Error in instrument " + (i + 1).ToString()); return; }
                                        if (arraygain[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arraygain[i, j]]; else TextBoxTranslateInst.Text += arraygainval[i, j].ToString();
                                        TextBoxTranslateInst.Text += ");" + "\r\n";
                                        break;
                                    }
                                /*
                            case 17: // clone sample old
                                {
                                    TextBoxTranslateInst.Text += "(smp+" + arrayval2value[i, j].ToString() + ")";
                                    TextBoxTranslateInst.Text += "< SmpLength[" + arraygain[i, j].ToString() + "] ? ";
                                    if (arraygainval[i, j] == 0)
                                    {
                                        TextBoxTranslateInst.Text += "*(BYTE*)(BaseAdr[" + arraygain[i, j].ToString() + "]+smp+" + arrayval2value[i, j].ToString() + ")<<8 : 0";
                                    }
                                    else
                                    {
                                        TextBoxTranslateInst.Text += "*(BYTE*)(BaseAdr[" + (arraygain[i, j] + 1).ToString() + "]-(smp+" + arrayval2value[i, j].ToString() + "))<<8 : 0";
                                    }
                                    TextBoxTranslateInst.Text += ");" + "\r\n";
                                    break;
                                }
                                */
                                
                                case 17: // clone sample with transpose
                                    {
                                        int transpose = arrayfrequencyval[i, j];
                                        if (arrayfrequency[i, j] > 0) transpose = variable[arrayfrequency[i, j]]; //get transpose value
                                        transpose += 32768; //add to shift to normal

                                        TextBoxTranslateInst.Text += "(((smp*(";
                                        if (arrayfrequency[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arrayfrequency[i, j]]; else TextBoxTranslateInst.Text += arrayfrequencyval[i, j].ToString();
                                        TextBoxTranslateInst.Text += "+32768))>>15)";
                                        TextBoxTranslateInst.Text += "+" + arrayval2value[i, j].ToString() + ")";
                                        TextBoxTranslateInst.Text += "< SmpLength[" + arraygain[i, j].ToString() + "] ? ";
                                        if (arraygainval[i, j] == 0)
                                        {
                                            TextBoxTranslateInst.Text += "*(BYTE*)(BaseAdr[" + arraygain[i, j].ToString() + "]+((smp*(";
                                            if (arrayfrequency[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arrayfrequency[i, j]]; else TextBoxTranslateInst.Text += arrayfrequencyval[i, j].ToString();
                                            TextBoxTranslateInst.Text += "+32768))>>15)";
                                            TextBoxTranslateInst.Text += "+" + arrayval2value[i, j].ToString() + ")<<8 : 0";
                                        }
                                        else
                                        {
                                            TextBoxTranslateInst.Text += "*(BYTE*)(BaseAdr[" + (arraygain[i, j] + 1).ToString() + "]-(((smp*(";
                                            if (arrayfrequency[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arrayfrequency[i, j]]; else TextBoxTranslateInst.Text += arrayfrequencyval[i, j].ToString();
                                            TextBoxTranslateInst.Text += "+32768))>>15)";
                                            TextBoxTranslateInst.Text += "+" + arrayval2value[i, j].ToString() + "))<<8 : 0";
                                        }
                                        TextBoxTranslateInst.Text += ");" + "\r\n";
                                        break;
                                    }
                                // ...   clone sample
                                // *(BYTE*)(BaseAdr[x]+smp)<<8 forward
                                // *(BYTE*)(BaseAdr[x-1]-smp)<<8 backward

                                case 18: // chordgen
                                    {
                                        TextBoxTranslateInst.Text += "smp" + ", ";
                                        TextBoxTranslateInst.Text += "BaseAdr[" + arraygain[i, j].ToString() + "]";
                                        TextBoxTranslateInst.Text += ", ";
                                        TextBoxTranslateInst.Text += arrayfrequency[i, j].ToString();
                                        TextBoxTranslateInst.Text += ", ";
                                        TextBoxTranslateInst.Text += arraywidth[i, j].ToString();
                                        TextBoxTranslateInst.Text += ", ";
                                        TextBoxTranslateInst.Text += arrayval1[i, j].ToString();
                                        TextBoxTranslateInst.Text += ", ";
                                        if (arrayval2[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arrayval2[i, j]]; else TextBoxTranslateInst.Text += arrayval2value[i, j].ToString();
                                        TextBoxTranslateInst.Text += ");" + "\r\n";
                                        break;
                                    }
                                case 19: // sample and hold
                                    {
                                        TextBoxTranslateInst.Text += j.ToString() + ", "; // instance
                                        TextBoxTranslateInst.Text += arrayvartext[arrayval1[i, j]] + ", ";
                                        if (arrayval1[i, j] == 0) { MessageBox.Show("Error in instrument " + (i + 1).ToString()); return; }
                                        if (arraygain[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arraygain[i, j]]; else TextBoxTranslateInst.Text += arraygainval[i, j].ToString();
                                        TextBoxTranslateInst.Text += ");" + "\r\n";
                                        break;
                                    }
                                case 20: // imported sample
                                    {
                                        // ternary operation
                                        TextBoxTranslateInst.Text += "smp < ImpLength[" + arraygain[i, j].ToString() + "] ? ";
                                        TextBoxTranslateInst.Text += "*(BYTE*)(BaseImpAdr[" + arraygain[i, j].ToString() + "]+smp)<<8 : 0";
                                        TextBoxTranslateInst.Text += ");" + "\r\n";
                                        break;
                                    }
                                case 21: // 1-pole filter
                                    {
                                        TextBoxTranslateInst.Text += j.ToString() + ", "; // instance
                                        TextBoxTranslateInst.Text += arrayvartext[arrayval1[i, j]];
                                        //if (arrayval1[i, j] == 0) { MessageBox.Show("Error in instrument " + (i + 1).ToString()); return; }
                                        TextBoxTranslateInst.Text += ", ";
                                        if (arrayfrequency[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arrayfrequency[i, j]]; else TextBoxTranslateInst.Text += arrayfrequencyval[i, j].ToString();
                                        TextBoxTranslateInst.Text += ", ";
                                        TextBoxTranslateInst.Text += arraygain[i, j].ToString();
                                        TextBoxTranslateInst.Text += ");" + "\r\n";
                                        break;
                                    }
                                case 23: // adsr envelope
                                    {
                                        // OPERATOR IN THE GUI HAS:
                                        int attackLength = (arrayval2value[i, j] << 8) + 1;             
                                        int decayLength = (arrayval1value[i,j] << 8) + 1;              
                                        int releaseLength = (arrayfrequencyval[i,j] << 8) + 1;     
                                        int sustainLength = samplelength[i] - attackLength - decayLength - releaseLength;
                                        byte gain = (arraygainval[i,j]);                         
                                        short sustainValue = (short)(arraywidthval[i,j] << 8);        //

                                        int peak = (32767 * gain) << 1;
                                        int sustainLevel = (sustainValue * gain) << 1;
                                        int attackAmount = peak / attackLength;
                                        int decayAmount = (peak - sustainLevel) / decayLength;
                                        int releaseAmount = sustainLevel / releaseLength;

                                        TextBoxTranslateInst.Text += j.ToString() + ", "; // instance
                                        //attack
                                        TextBoxTranslateInst.Text += attackAmount.ToString();
                                        TextBoxTranslateInst.Text += ", ";
                                        //decay
                                        TextBoxTranslateInst.Text += decayAmount.ToString();
                                        TextBoxTranslateInst.Text += ", ";
                                        //sustain level
                                        TextBoxTranslateInst.Text += sustainLevel.ToString();
                                        TextBoxTranslateInst.Text += ", ";
                                        //sustain
                                        TextBoxTranslateInst.Text += sustainLength.ToString();
                                        TextBoxTranslateInst.Text += ", ";
                                        //release
                                        TextBoxTranslateInst.Text += releaseAmount.ToString();
                                        TextBoxTranslateInst.Text += ", ";
                                        //peak
                                        TextBoxTranslateInst.Text += peak.ToString();
                                        TextBoxTranslateInst.Text += ");" + "\r\n";
                                        break;
                                    }

                            }
                        }
                    }
                    TextBoxTranslateInst.Text = TextBoxTranslateInst.Text + "}\r\n";
                }
            }

            DialogResult dialogResult = MessageBox.Show("Are you sure?", "Save header files", MessageBoxButtons.OKCancel);
            if (dialogResult == DialogResult.OK)
            {
                // save files
                string root = Application.StartupPath;
                string filename = root + "/exe_creator/ilen.h";
                using (TextWriter writer = new StreamWriter(File.Open(filename, FileMode.Create)))
                {
                    writer.Write(TextBoxTranslateIlen.Text); // save ilen.h
                    writer.Close();
                }

                filename = root + "/exe_creator/inst.h";
                using (TextWriter writer = new StreamWriter(File.Open(filename, FileMode.Create)))
                {
                    writer.Write(TextBoxTranslateInst.Text); // save inst.h
                    writer.Close();
                }

            }
            else return;

            {
                MessageBox.Show("choose your mod file!");
                long filelength = 0;
                string filename;
                hideGroupBoxes();
                openFileDialog1.Filter = "mod (*.mod)|*.mod|All files (*.*)|*.*";
                openFileDialog1.FilterIndex = 1;
                openFileDialog1.RestoreDirectory = true;
                openFileDialog1.Title = "Create exe from Amiga mod";

                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Stream myStream;
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        filename = openFileDialog1.FileName;
                        filelength = myStream.Length;
                        BinaryReader reader = new BinaryReader(myStream);

                        for (int i = 0; i < filelength; i++)
                        {
                            amigamod[i] = reader.ReadByte();
                        }
                        myStream.Close();

                        // check if really a mod file (M.K.)
                        if (!(amigamod[1080] == 77 && amigamod[1081] == 46 && amigamod[1082] == 75 && amigamod[1083] == 46))
                        {
                            MessageBox.Show("Not an Amiga mod file!!"); return;
                        }

                        // find out highest pattern number
                        byte maxpattern = 0;
                        for (int i = 952; i <= 1079; i++)
                        {
                            if (amigamod[i] > maxpattern) maxpattern = amigamod[i];
                        }
                        maxpattern += 1;
                        // calculate mod length without samples
                        int modlength = 1084 + (maxpattern * 1024);
                        string root = Application.StartupPath;
                        TextBoxTranslateIset.Clear();
                        int instrumentcount = getnumberofhighestinstrument();
                        TextBoxTranslateIset.Text += "#define executable\r\n";
                        TextBoxTranslateIset.Text += "#define numinstruments " + instrumentcount.ToString() + "\r\n";
                        TextBoxTranslateIset.Text += "const void * protrackermod;\r\n";
                        TextBoxTranslateIset.Text += "INCBIN(protrackermod, \"empty.mod\");\r\n";
                        TextBoxTranslateIset.Text += "const void * importedsamples;\r\n";
                        TextBoxTranslateIset.Text += "INCBIN(importedsamples, \"Isamp.raw\");\r\n";
                        TextBoxTranslateIset.Text += "int mod_length_empty = " + modlength.ToString() + ";\r\n";
                        // calc length of all imported samples
                        int implength = 0;
                        for (int s = 0; s < 8; s++) { implength += importedlength[s]; }
                        TextBoxTranslateIset.Text += "int imp_length = " + implength.ToString() + ";\r\n";
                        // calc length of all generated samples
                        updatetotalsize();
                        TextBoxTranslateIset.Text += "long gen_length = " + labelTotalSize.Text + ";\r\n";


                        filename = root + "/exe_creator/Iset.h";
                        using (TextWriter writer = new StreamWriter(File.Open(filename, FileMode.Create)))
                        {
                            writer.Write(TextBoxTranslateIset.Text); // save Iset.h
                            writer.Close();
                        }

                        TextBoxTranslateIswitch.Clear();
                        TextBoxTranslateIswitch.Text += "#define executable\r\n";

                        filename = root + "/exe_creator/support/Iswitch.h";
                        using (TextWriter writer = new StreamWriter(File.Open(filename, FileMode.Create)))
                        {
                            writer.Write(TextBoxTranslateIswitch.Text); // save Iswitch.h
                            writer.Close();
                        }



                        // neu für delta encoding
                        //*********************************************************************************************************

                        // transfer imp samples from 2d to 1d array
                        int smp2 = 0;
                        for (int j = 0; j < 8; j++)
                        {
                            for (int smp = 0; smp < importedlength[j]; smp++)
                            {
                                importedsample1d[smp2] = importedsample[smp, j];
                                smp2++;
                            }
                        }

                        // delta encode
                        delta_encode(importedsample1d, implength);

                        // save delta encoded imported samples
                        filename = root + "/exe_creator/Isamp.raw";
                        using (BinaryWriter writer = new BinaryWriter(File.Open(filename, FileMode.Create)))
                        {
                            for (int smp = 0; smp < implength; smp++)
                            {
                                writer.Write(importedsample1d[smp]);
                            }
                            writer.Close();
                        }

                        //********************************************************************************************************


                        /*
                                                // save imported samples
                                                filename = root + "/exe_creator/Isamp.raw";
                                                using (BinaryWriter writer = new BinaryWriter(File.Open(filename, FileMode.Create)))
                                                {
                                                    for (int j = 0; j < 8; j++)
                                                    {
                                                        for (int smp = 0; smp < importedlength[j]; smp++)
                                                        {
                                                            writer.Write(importedsample[smp, j]);
                                                        }
                                                    }
                                                    writer.Close();
                                                }
                        */

                        // write samplelength and loop params to mod
                        byte[] temp2 = new byte[2];
                        for (int i = 0; i < 31; i++)
                        {
                            // samplelength
                            temp2 = BitConverter.GetBytes((UInt16)(samplelength[i] >> 1));
                            amigamod[42 + (30 * i)] = temp2[1];
                            amigamod[43 + (30 * i)] = temp2[0];
                            // loop offset
                            if (arrayfunction[i, 15] == 22)  // is loopgen usen ?
                            {
                                temp2 = BitConverter.GetBytes((UInt16)(loopoffset[i] >> 1));
                                amigamod[46 + (30 * i)] = temp2[1];
                                amigamod[47 + (30 * i)] = temp2[0];
                                // loop length
                                temp2 = BitConverter.GetBytes((UInt16)(looplength[i] >> 1));
                                amigamod[48 + (30 * i)] = temp2[1];
                                amigamod[49 + (30 * i)] = temp2[0];
                            }
                        }


                        // save empty mod 
                        filename = "exe_creator/empty.mod";

                        using (BinaryWriter writer = new BinaryWriter(File.Open(filename, FileMode.Create)))
                        {
                            for (int i = 0; i < modlength; i++)
                            {
                                writer.Write(amigamod[i]);
                            }
                        }
                    }

                    // execute bat
                    string path = Application.StartupPath;
                    Process.Start(path + "/exe_creator/exe_creator.bat");
                }
                else return;
            }
            MessageBox.Show("exporting exemusic.exe to folder exe_creator");
        }


        //****************************************************************************************************************
        // export amiga binary
        //****************************************************************************************************************

        private void exportBinaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CheckErrorSave() == true) { MessageBox.Show("Error in instruments"); return; }

            char temp = ' ';
            // make ilen table
            TextBoxTranslateIlen.Clear();
            for (int i = 0; i < getnumberofhighestinstrument(); i++)
            {
                TextBoxTranslateIlen.Text = TextBoxTranslateIlen.Text + "// " + ComboBoxSampleAuswahl.Items[i].ToString() + "\r\n";    // instrument name
                TextBoxTranslateIlen.Text = TextBoxTranslateIlen.Text + "SmpLength[" + i.ToString() + "] = 0x" + samplelength[i].ToString("X") + ";\r\n";   // sample length
                TextBoxTranslateIlen.Text = TextBoxTranslateIlen.Text + "repeat_offset[" + i.ToString() + "] = 0x" + loopoffset[i].ToString("X") + ";\r\n"; // loop offset
                TextBoxTranslateIlen.Text = TextBoxTranslateIlen.Text + "repeat_length[" + i.ToString() + "] = 0x" + looplength[i].ToString("X") + ";\r\n"; // loop length
                if (arrayfunction[i, 15] == 22) temp = 'l'; else temp = ' '; // is loopgen usen ?
                TextBoxTranslateIlen.Text = TextBoxTranslateIlen.Text + "samplename_flag[" + i.ToString() + "] = '" + temp + "';\r\n"; // loop flag (loopgen used)
                TextBoxTranslateIlen.Text = TextBoxTranslateIlen.Text + "\r\n";
            }
            // write imported length
            for (int i = 0; i < 8; i++)
            {
                TextBoxTranslateIlen.Text = TextBoxTranslateIlen.Text + "ImpLength[" + i.ToString() + "] = 0x" + importedlength[i].ToString("X") + ";\r\n";   // imported length
                TextBoxTranslateIlen.Text = TextBoxTranslateIlen.Text + "\r\n";
            }

            // make inst table
            TextBoxTranslateInst.Clear();
            for (int i = 0; i < 31; i++)
            {
                if (samplelength[i] > 2)
                {
                    TextBoxTranslateInst.Text = TextBoxTranslateInst.Text + "// " + ComboBoxSampleAuswahl.Items[i].ToString() + "\r\n";    // instrument name
                    TextBoxTranslateInst.Text = TextBoxTranslateInst.Text + "if (instrument == " + i.ToString() + ") {\r\n";
                    for (int j = 0; j < 16; j++)
                    {
                        if (arrayvar[i, j] != 0 && arrayfunction[i, j] != 22)
                        {
                            TextBoxTranslateInst.Text = TextBoxTranslateInst.Text + arrayvartext[arrayvar[i, j]] + " = " + arrayfunctiontext[arrayfunction[i, j]] + "(";
                            switch (arrayfunction[i, j])
                            {
                                case 0: break;
                                case 1: // vol
                                    {
                                        TextBoxTranslateInst.Text += arrayvartext[arrayval1[i, j]] + ", ";
                                        //if (arrayval1[i, j] == 0) { MessageBox.Show("Error in instrument " + (i + 1).ToString()); return; }
                                        if (arraygain[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arraygain[i, j]]; else TextBoxTranslateInst.Text += arraygainval[i, j].ToString();
                                        TextBoxTranslateInst.Text += ");" + "\r\n";
                                        break;
                                    }
                                case 2: // osc saw
                                    {
                                        TextBoxTranslateInst.Text += j.ToString() + ", "; // instance
                                        if (arrayfrequency[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arrayfrequency[i, j]]; else TextBoxTranslateInst.Text += arrayfrequencyval[i, j].ToString();
                                        TextBoxTranslateInst.Text += ", ";
                                        if (arraygain[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arraygain[i, j]]; else TextBoxTranslateInst.Text += arraygainval[i, j].ToString();
                                        TextBoxTranslateInst.Text += ");" + "\r\n";
                                        break;
                                    }
                                case 3: // osc tri
                                    {
                                        TextBoxTranslateInst.Text += j.ToString() + ", "; // instance
                                        if (arrayfrequency[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arrayfrequency[i, j]]; else TextBoxTranslateInst.Text += arrayfrequencyval[i, j].ToString();
                                        TextBoxTranslateInst.Text += ", ";
                                        if (arraygain[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arraygain[i, j]]; else TextBoxTranslateInst.Text += arraygainval[i, j].ToString();
                                        TextBoxTranslateInst.Text += ");" + "\r\n";
                                        break;
                                    }
                                case 4: // osc sine
                                    {
                                        TextBoxTranslateInst.Text += j.ToString() + ", "; // instance
                                        if (arrayfrequency[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arrayfrequency[i, j]]; else TextBoxTranslateInst.Text += arrayfrequencyval[i, j].ToString();
                                        TextBoxTranslateInst.Text += ", ";
                                        if (arraygain[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arraygain[i, j]]; else TextBoxTranslateInst.Text += arraygainval[i, j].ToString();
                                        TextBoxTranslateInst.Text += ");" + "\r\n";
                                        break;
                                    }
                                case 5: // osc pulse
                                    {
                                        TextBoxTranslateInst.Text += j.ToString() + ", "; // instance
                                        if (arrayfrequency[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arrayfrequency[i, j]]; else TextBoxTranslateInst.Text += arrayfrequencyval[i, j].ToString();
                                        TextBoxTranslateInst.Text += ", ";
                                        if (arraygain[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arraygain[i, j]]; else TextBoxTranslateInst.Text += arraygainval[i, j].ToString();
                                        TextBoxTranslateInst.Text += ", ";
                                        if (arraywidth[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arraywidth[i, j]]; else TextBoxTranslateInst.Text += arraywidthval[i, j].ToString();
                                        TextBoxTranslateInst.Text += ");" + "\r\n";
                                        break;
                                    }
                                case 6: // osc noise
                                    {
                                        TextBoxTranslateInst.Text += "smp" + ", "; // smp
                                        if (arraygain[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arraygain[i, j]]; else TextBoxTranslateInst.Text += arraygainval[i, j].ToString();
                                        TextBoxTranslateInst.Text += ");" + "\r\n";
                                        break;
                                    }
                                case 7: // enva
                                    {
                                        TextBoxTranslateInst.Text += "smp" + ", "; // smp
                                        if (arrayval1[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arrayval1[i, j]]; else TextBoxTranslateInst.Text += arrayval1value[i, j].ToString();
                                        TextBoxTranslateInst.Text += ", ";
                                        TextBoxTranslateInst.Text += "0, ";
                                        if (arraygain[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arraygain[i, j]]; else TextBoxTranslateInst.Text += arraygainval[i, j].ToString();
                                        TextBoxTranslateInst.Text += ");" + "\r\n";
                                        break;
                                    }
                                case 8: // envd
                                    {
                                        TextBoxTranslateInst.Text += "smp" + ", "; // smp
                                        if (arrayval1[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arrayval1[i, j]]; else TextBoxTranslateInst.Text += arrayval1value[i, j].ToString();
                                        TextBoxTranslateInst.Text += ", ";
                                        if (arrayval2[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arrayval2[i, j]]; else TextBoxTranslateInst.Text += arrayval2value[i, j].ToString();
                                        TextBoxTranslateInst.Text += ", ";
                                        if (arraygain[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arraygain[i, j]]; else TextBoxTranslateInst.Text += arraygainval[i, j].ToString();
                                        TextBoxTranslateInst.Text += ");" + "\r\n";
                                        break;
                                    }
                                case 9: // add
                                    {
                                        TextBoxTranslateInst.Text += arrayvartext[arrayval1[i, j]];
                                        //if (arrayval1[i, j] == 0) { MessageBox.Show("Error in instrument " + (i + 1).ToString()); return; }
                                        TextBoxTranslateInst.Text += ", ";
                                        if (arrayval2[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arrayval2[i, j]]; else TextBoxTranslateInst.Text += arrayval2value[i, j].ToString();
                                        TextBoxTranslateInst.Text += ");" + "\r\n";
                                        break;
                                    }
                                case 10: // mul
                                    {
                                        TextBoxTranslateInst.Text += arrayvartext[arrayval1[i, j]];
                                        //if (arrayval1[i, j] == 0) { MessageBox.Show("Error in instrument " + (i + 1).ToString()); return; }
                                        TextBoxTranslateInst.Text += ", ";
                                        if (arrayval2[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arrayval2[i, j]]; else TextBoxTranslateInst.Text += arrayval2value[i, j].ToString();
                                        TextBoxTranslateInst.Text += ");" + "\r\n";
                                        break;
                                    }
                                case 11: // delay
                                    {
                                        TextBoxTranslateInst.Text += j.ToString() + ", "; // instance
                                        TextBoxTranslateInst.Text += arrayvartext[arrayval1[i, j]];
                                        //if (arrayval1[i, j] == 0) { MessageBox.Show("Error in instrument " + (i + 1).ToString()); return; }
                                        TextBoxTranslateInst.Text += ", ";
                                        if (arrayfrequency[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arrayfrequency[i, j]]; else TextBoxTranslateInst.Text += arrayfrequencyval[i, j].ToString();
                                        TextBoxTranslateInst.Text += ", ";
                                        if (arraygain[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arraygain[i, j]]; else TextBoxTranslateInst.Text += arraygainval[i, j].ToString();
                                        TextBoxTranslateInst.Text += ");" + "\r\n";
                                        break;
                                    }
                                case 12: // comb
                                    {
                                        TextBoxTranslateInst.Text += j.ToString() + ", "; // instance
                                        TextBoxTranslateInst.Text += arrayvartext[arrayval1[i, j]];
                                        //if (arrayval1[i, j] == 0) { MessageBox.Show("Error in instrument " + (i + 1).ToString()); return; }
                                        TextBoxTranslateInst.Text += ", ";
                                        if (arrayfrequency[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arrayfrequency[i, j]]; else TextBoxTranslateInst.Text += arrayfrequencyval[i, j].ToString();
                                        TextBoxTranslateInst.Text += ", ";
                                        if (arrayval2[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arrayval2[i, j]]; else TextBoxTranslateInst.Text += arrayval2value[i, j].ToString();
                                        TextBoxTranslateInst.Text += ", ";
                                        if (arraygain[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arraygain[i, j]]; else TextBoxTranslateInst.Text += arraygainval[i, j].ToString();
                                        TextBoxTranslateInst.Text += ");" + "\r\n";
                                        break;
                                    }
                                case 13: // reverb
                                    {
                                        TextBoxTranslateInst.Text += arrayvartext[arrayval1[i, j]];
                                        if (arrayval1[i, j] == 0) { MessageBox.Show("Error in instrument " + (i + 1).ToString()); return; }
                                        TextBoxTranslateInst.Text += ", ";
                                        if (arrayval2[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arrayval2[i, j]]; else TextBoxTranslateInst.Text += arrayval2value[i, j].ToString();
                                        TextBoxTranslateInst.Text += ", ";
                                        if (arraygain[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arraygain[i, j]]; else TextBoxTranslateInst.Text += arraygainval[i, j].ToString();
                                        TextBoxTranslateInst.Text += ");" + "\r\n";
                                        break;
                                    }
                                case 14: // ctrl
                                    {
                                        TextBoxTranslateInst.Text += arrayvartext[arrayval1[i, j]];
                                        //if (arrayval1[i, j] == 0) { MessageBox.Show("Error in instrument " + (i + 1).ToString()); return; }
                                        TextBoxTranslateInst.Text += ");" + "\r\n";
                                        break;
                                    }
                                case 15: // filter
                                    {
                                        TextBoxTranslateInst.Text += j.ToString() + ", "; // instance
                                        TextBoxTranslateInst.Text += arrayvartext[arrayval1[i, j]];
                                        if (arrayval1[i, j] == 0) { MessageBox.Show("Error in instrument " + (i + 1).ToString()); return; }
                                        TextBoxTranslateInst.Text += ", ";
                                        if (arrayfrequency[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arrayfrequency[i, j]]; else TextBoxTranslateInst.Text += arrayfrequencyval[i, j].ToString();
                                        TextBoxTranslateInst.Text += ", ";
                                        if (arrayval2[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arrayval2[i, j]]; else TextBoxTranslateInst.Text += arrayval2value[i, j].ToString();
                                        TextBoxTranslateInst.Text += ", ";
                                        TextBoxTranslateInst.Text += arraygain[i, j].ToString();
                                        TextBoxTranslateInst.Text += ");" + "\r\n";
                                        break;
                                    }
                                case 16: // clamp
                                    {
                                        TextBoxTranslateInst.Text += arrayvartext[arrayval1[i, j]] + ", ";
                                        //if (arrayval1[i, j] == 0) { MessageBox.Show("Error in instrument " + (i + 1).ToString()); return; }
                                        if (arraygain[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arraygain[i, j]]; else TextBoxTranslateInst.Text += arraygainval[i, j].ToString();
                                        TextBoxTranslateInst.Text += ");" + "\r\n";
                                        break;
                                    }
                                /*
                            case 17: // clone
                                {
                                    // ternary operation
                                    TextBoxTranslateInst.Text += "(smp+" + arrayval2value[i, j].ToString() + ")";
                                    TextBoxTranslateInst.Text += "< SmpLength[" + arraygain[i, j].ToString() + "] ? ";
                                    if (arraygainval[i, j] == 0)
                                    {
                                        TextBoxTranslateInst.Text += "*(BYTE*)(BaseAdr[" + arraygain[i, j].ToString() + "]+smp+" + arrayval2value[i, j].ToString() + ")<<8 : 0";
                                    }
                                    else
                                    {
                                        TextBoxTranslateInst.Text += "*(BYTE*)(BaseAdr[" + (arraygain[i, j] + 1).ToString() + "]-(smp+" + arrayval2value[i, j].ToString() + "))<<8 : 0";
                                    }
                                    TextBoxTranslateInst.Text += ");" + "\r\n";
                                    break;
                                }
                                */
                                case 17: // clone sample with transpose
                                    {
                                        int transpose = arrayfrequencyval[i, j];
                                        if (arrayfrequency[i, j] > 0) transpose = variable[arrayfrequency[i, j]]; //get transpose value
                                        transpose += 32768; //add to shift to normal

                                        TextBoxTranslateInst.Text += "(((smp*(";
                                        if (arrayfrequency[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arrayfrequency[i, j]]; else TextBoxTranslateInst.Text += arrayfrequencyval[i, j].ToString();
                                        TextBoxTranslateInst.Text += "+32768))>>15)";
                                        TextBoxTranslateInst.Text += "+" + arrayval2value[i, j].ToString() + ")";
                                        TextBoxTranslateInst.Text += "< SmpLength[" + arraygain[i, j].ToString() + "] ? ";
                                        if (arraygainval[i, j] == 0)
                                        {
                                            TextBoxTranslateInst.Text += "*(BYTE*)(BaseAdr[" + arraygain[i, j].ToString() + "]+((smp*(";
                                            if (arrayfrequency[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arrayfrequency[i, j]]; else TextBoxTranslateInst.Text += arrayfrequencyval[i, j].ToString();
                                            TextBoxTranslateInst.Text += "+32768))>>15)";
                                            TextBoxTranslateInst.Text += "+" + arrayval2value[i, j].ToString() + ")<<8 : 0";
                                        }
                                        else
                                        {
                                            TextBoxTranslateInst.Text += "*(BYTE*)(BaseAdr[" + (arraygain[i, j] + 1).ToString() + "]-(((smp*(";
                                            if (arrayfrequency[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arrayfrequency[i, j]]; else TextBoxTranslateInst.Text += arrayfrequencyval[i, j].ToString();
                                            TextBoxTranslateInst.Text += "+32768))>>15)";
                                            TextBoxTranslateInst.Text += "+" + arrayval2value[i, j].ToString() + "))<<8 : 0";
                                        }
                                        TextBoxTranslateInst.Text += ");" + "\r\n";
                                        break;
                                    }

                                case 18: // chordgen
                                    {
                                        TextBoxTranslateInst.Text += "smp" + ", ";
                                        TextBoxTranslateInst.Text += "BaseAdr[" + arraygain[i, j].ToString() + "]";
                                        TextBoxTranslateInst.Text += ", ";
                                        TextBoxTranslateInst.Text += arrayfrequency[i, j].ToString();
                                        TextBoxTranslateInst.Text += ", ";
                                        TextBoxTranslateInst.Text += arraywidth[i, j].ToString();
                                        TextBoxTranslateInst.Text += ", ";
                                        TextBoxTranslateInst.Text += arrayval1[i, j].ToString();
                                        TextBoxTranslateInst.Text += ", ";
                                        if (arrayval2[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arrayval2[i, j]]; else TextBoxTranslateInst.Text += arrayval2value[i, j].ToString();
                                        TextBoxTranslateInst.Text += ");" + "\r\n";
                                        break;
                                    }
                                case 19: // sample and hold
                                    {
                                        TextBoxTranslateInst.Text += j.ToString() + ", "; // instance
                                        TextBoxTranslateInst.Text += arrayvartext[arrayval1[i, j]] + ", ";
                                        if (arrayval1[i, j] == 0) { MessageBox.Show("Error in instrument " + (i + 1).ToString()); return; }
                                        if (arraygain[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arraygain[i, j]]; else TextBoxTranslateInst.Text += arraygainval[i, j].ToString();
                                        TextBoxTranslateInst.Text += ");" + "\r\n";
                                        break;
                                    }
                                case 20: // imported sample
                                    {
                                        // ternary operation
                                        TextBoxTranslateInst.Text += "smp < ImpLength[" + arraygain[i, j].ToString() + "] ? ";
                                        TextBoxTranslateInst.Text += "*(BYTE*)(BaseImpAdr[" + arraygain[i, j].ToString() + "]+smp)<<8 : 0";
                                        TextBoxTranslateInst.Text += ");" + "\r\n";
                                        break;
                                    }
                                case 21: // 1-pole filter
                                    {
                                        TextBoxTranslateInst.Text += j.ToString() + ", "; // instance
                                        TextBoxTranslateInst.Text += arrayvartext[arrayval1[i, j]];
                                        //if (arrayval1[i, j] == 0) { MessageBox.Show("Error in instrument " + (i + 1).ToString()); return; }
                                        TextBoxTranslateInst.Text += ", ";
                                        if (arrayfrequency[i, j] > 0) TextBoxTranslateInst.Text += arrayvartext[arrayfrequency[i, j]]; else TextBoxTranslateInst.Text += arrayfrequencyval[i, j].ToString();
                                        TextBoxTranslateInst.Text += ", ";
                                        TextBoxTranslateInst.Text += arraygain[i, j].ToString();
                                        TextBoxTranslateInst.Text += ");" + "\r\n";
                                        break;
                                    }
                                case 23: // adsr envelope
                                    {
                                        // OPERATOR IN THE GUI HAS:
                                        int attackLength = (arrayval2value[i, j] << 8) + 1;
                                        int decayLength = (arrayval1value[i, j] << 8) + 1;
                                        int releaseLength = (arrayfrequencyval[i, j] << 8) + 1;
                                        int sustainLength = samplelength[i] - attackLength - decayLength - releaseLength;
                                        byte gain = (arraygainval[i, j]);
                                        short sustainValue = (short)(arraywidthval[i, j] << 8);        //

                                        int peak = (32767 * gain) << 1;
                                        int sustainLevel = (sustainValue * gain) << 1;
                                        int attackAmount = peak / attackLength;
                                        int decayAmount = (peak - sustainLevel) / decayLength;
                                        int releaseAmount = sustainLevel / releaseLength;

                                        TextBoxTranslateInst.Text += j.ToString() + ", "; // instance
                                        //attack
                                        TextBoxTranslateInst.Text += attackAmount.ToString();
                                        TextBoxTranslateInst.Text += ", ";
                                        //decay
                                        TextBoxTranslateInst.Text += decayAmount.ToString();
                                        TextBoxTranslateInst.Text += ", ";
                                        //sustain level
                                        TextBoxTranslateInst.Text += sustainLevel.ToString();
                                        TextBoxTranslateInst.Text += ", ";
                                        //sustain
                                        TextBoxTranslateInst.Text += sustainLength.ToString();
                                        TextBoxTranslateInst.Text += ", ";
                                        //release
                                        TextBoxTranslateInst.Text += releaseAmount.ToString();
                                        TextBoxTranslateInst.Text += ", ";
                                        //peak
                                        TextBoxTranslateInst.Text += peak.ToString();
                                        TextBoxTranslateInst.Text += ");" + "\r\n";
                                        break;
                                    }

                            }
                        }
                    }
                    TextBoxTranslateInst.Text = TextBoxTranslateInst.Text + "}\r\n";
                }
            }

            DialogResult dialogResult = MessageBox.Show("Are you sure?", "Save header files", MessageBoxButtons.OKCancel);
            if (dialogResult == DialogResult.OK)
            {
                // save files
                string root = Application.StartupPath;
                string filename = root + "/exe_creator/ilen.h";
                using (TextWriter writer = new StreamWriter(File.Open(filename, FileMode.Create)))
                {
                    writer.Write(TextBoxTranslateIlen.Text); // save ilen.h
                    writer.Close();
                }

                filename = root + "/exe_creator/inst.h";
                using (TextWriter writer = new StreamWriter(File.Open(filename, FileMode.Create)))
                {
                    writer.Write(TextBoxTranslateInst.Text); // save inst.h
                    writer.Close();
                }

                root = Application.StartupPath;
                TextBoxTranslateIset.Clear();
                int instrumentcount = getnumberofhighestinstrument();
                TextBoxTranslateIset.Text += "#define binary\r\n";
                TextBoxTranslateIset.Text += "#define numinstruments " + instrumentcount.ToString() + "\r\n";
                // calc length of all imported samples
                int implength = 0;
                for (int s = 0; s < 8; s++) { implength += importedlength[s]; }
                TextBoxTranslateIset.Text += "int imp_length = " + implength.ToString() + ";\r\n";

                filename = root + "/exe_creator/Iset.h";
                using (TextWriter writer = new StreamWriter(File.Open(filename, FileMode.Create)))
                {
                    writer.Write(TextBoxTranslateIset.Text); // save Iset.h
                    writer.Close();
                }

                TextBoxTranslateIswitch.Clear();
                TextBoxTranslateIswitch.Text += "#define binary\r\n";

                filename = root + "/exe_creator/support/Iswitch.h";
                using (TextWriter writer = new StreamWriter(File.Open(filename, FileMode.Create)))
                {
                    writer.Write(TextBoxTranslateIswitch.Text); // save Iswitch.h
                    writer.Close();
                }


                // neu für delta encoding
                //*********************************************************************************************************

                // transfer imp samples from 2d to 1d array
                int smp2 = 0;
                for (int j = 0; j < 8; j++)
                {
                    for (int smp = 0; smp < importedlength[j]; smp++)
                    {
                        importedsample1d[smp2] = importedsample[smp, j];
                        smp2++;
                    }
                }

                // delta encode
                delta_encode(importedsample1d, implength);

                // save delta encoded imported samples
                filename = root + "/exe_creator/Isamp.raw";
                using (BinaryWriter writer = new BinaryWriter(File.Open(filename, FileMode.Create)))
                {
                    for (int smp = 0; smp < implength; smp++)
                    {
                        writer.Write(importedsample1d[smp]);
                    }
                    writer.Close();
                }

                //********************************************************************************************************
                /*
                                // save imported samples
                                filename = root + "/exe_creator/Isamp.raw";
                                using (BinaryWriter writer = new BinaryWriter(File.Open(filename, FileMode.Create)))
                                {
                                    for (int j = 0; j < 8; j++)
                                    {
                                        for (int smp = 0; smp < importedlength[j]; smp++)
                                        {
                                            writer.Write(importedsample[smp, j]);
                                        }
                                    }
                                    writer.Close();
                                }
                */

            }
            else return; // clicked on no..  leave!

            // execute bat
            string path = Application.StartupPath;
            Process.Start(path + "/exe_creator/bin_creator.bat");

            MessageBox.Show("exporting exemusic.bin to folder exe_creator");

        }
        //****************************************************************************************************************
        // export and translate ASM
        //****************************************************************************************************************

        
        private void exportScriptToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (CheckErrorSave() == true) { MessageBox.Show("Error in instruments"); return; }

            char temp = ' ';
            // make dan table
            TextBoxTranslateDan.Clear();

            // write imported sample lengths with comma seperation
            TextBoxTranslateDan.Text = TextBoxTranslateDan.Text + importedlength[0].ToString() + ", ";
            TextBoxTranslateDan.Text = TextBoxTranslateDan.Text + importedlength[1].ToString() + ", ";
            TextBoxTranslateDan.Text = TextBoxTranslateDan.Text + importedlength[2].ToString() + ", ";
            TextBoxTranslateDan.Text = TextBoxTranslateDan.Text + importedlength[3].ToString() + ", ";
            TextBoxTranslateDan.Text = TextBoxTranslateDan.Text + importedlength[4].ToString() + ", ";
            TextBoxTranslateDan.Text = TextBoxTranslateDan.Text + importedlength[5].ToString() + ", ";
            TextBoxTranslateDan.Text = TextBoxTranslateDan.Text + importedlength[6].ToString() + ", ";
            TextBoxTranslateDan.Text = TextBoxTranslateDan.Text + importedlength[7].ToString() + "\r\n";


            // make inst table
            TextBoxTranslateInst.Clear();
            for (int i = 0; i < getnumberofhighestinstrument(); i++)
            {

                TextBoxTranslateDan.Text = TextBoxTranslateDan.Text + "$ " + ComboBoxSampleAuswahl.Items[i].ToString() + ", ";    // instrument name
                TextBoxTranslateDan.Text = TextBoxTranslateDan.Text + samplelength[i].ToString() + ", ";   // sample length
                TextBoxTranslateDan.Text = TextBoxTranslateDan.Text + loopoffset[i].ToString() + ", "; // loop offset
                TextBoxTranslateDan.Text = TextBoxTranslateDan.Text + looplength[i].ToString() + ", "; // loop length
                if (arrayfunction[i, 15] == 22) temp = 'Y'; else temp = 'N'; // is loopgen usen ?
                TextBoxTranslateDan.Text = TextBoxTranslateDan.Text + temp + "\r\n#\r\n"; // loop flag (loopgen used)

                for (int j = 0; j < 16; j++)
                {
                    if (arrayvar[i, j] != 0 && arrayfunction[i, j] != 22)
                    {
                        TextBoxTranslateDan.Text = TextBoxTranslateDan.Text + arrayvartext[arrayvar[i, j]] + " = " + arrayfunctiontext[arrayfunction[i, j]];
                        switch (arrayfunction[i, j])
                        {
                            case 0: break;
                            case 1: // vol
                                {
                                    TextBoxTranslateDan.Text += "(" + arrayvartext[arrayval1[i, j]] + ", ";
                                    //if (arrayval1[i, j] == 0) { MessageBox.Show("Error in instrument " + (i + 1).ToString()); return; }
                                    if (arraygain[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arraygain[i, j]]; else TextBoxTranslateDan.Text += arraygainval[i, j].ToString();
                                    TextBoxTranslateDan.Text += ");" + "\r\n";
                                    break;
                                }
                            case 2: // osc saw
                                {
                                    TextBoxTranslateDan.Text += "(" + j.ToString() + ", "; // instance
                                    if (arrayfrequency[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arrayfrequency[i, j]]; else TextBoxTranslateDan.Text += arrayfrequencyval[i, j].ToString();
                                    TextBoxTranslateDan.Text += ", ";
                                    if (arraygain[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arraygain[i, j]]; else TextBoxTranslateDan.Text += arraygainval[i, j].ToString();
                                    TextBoxTranslateDan.Text += ");" + "\r\n";
                                    break;
                                }
                            case 3: // osc tri
                                {
                                    TextBoxTranslateDan.Text += "(" + j.ToString() + ", "; // instance
                                    if (arrayfrequency[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arrayfrequency[i, j]]; else TextBoxTranslateDan.Text += arrayfrequencyval[i, j].ToString();
                                    TextBoxTranslateDan.Text += ", ";
                                    if (arraygain[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arraygain[i, j]]; else TextBoxTranslateDan.Text += arraygainval[i, j].ToString();
                                    TextBoxTranslateDan.Text += ");" + "\r\n";
                                    break;
                                }
                            case 4: // osc sine
                                {
                                    TextBoxTranslateDan.Text += "(" + j.ToString() + ", "; // instance
                                    if (arrayfrequency[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arrayfrequency[i, j]]; else TextBoxTranslateDan.Text += arrayfrequencyval[i, j].ToString();
                                    TextBoxTranslateDan.Text += ", ";
                                    if (arraygain[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arraygain[i, j]]; else TextBoxTranslateDan.Text += arraygainval[i, j].ToString();
                                    TextBoxTranslateDan.Text += ");" + "\r\n";
                                    break;
                                }
                            case 5: // osc pulse
                                {
                                    TextBoxTranslateDan.Text += "(" + j.ToString() + ", "; // instance
                                    if (arrayfrequency[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arrayfrequency[i, j]]; else TextBoxTranslateDan.Text += arrayfrequencyval[i, j].ToString();
                                    TextBoxTranslateDan.Text += ", ";
                                    if (arraygain[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arraygain[i, j]]; else TextBoxTranslateDan.Text += arraygainval[i, j].ToString();
                                    TextBoxTranslateDan.Text += ", ";
                                    if (arraywidth[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arraywidth[i, j]]; else TextBoxTranslateDan.Text += arraywidthval[i, j].ToString();
                                    TextBoxTranslateDan.Text += ");" + "\r\n";
                                    break;
                                }
                            case 6: // osc noise
                                {
                                    TextBoxTranslateDan.Text += "(";
                                    if (arraygain[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arraygain[i, j]]; else TextBoxTranslateDan.Text += arraygainval[i, j].ToString();
                                    TextBoxTranslateDan.Text += ");" + "\r\n";
                                    break;
                                }
                            case 7: // enva
                                {
                                    TextBoxTranslateDan.Text += "(" + j.ToString() + ", "; // instance
                                    if (arrayval1[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arrayval1[i, j]]; else TextBoxTranslateDan.Text += arrayval1value[i, j].ToString();
                                    TextBoxTranslateDan.Text += ", ";
                                    TextBoxTranslateDan.Text += "0, ";
                                    if (arraygain[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arraygain[i, j]]; else TextBoxTranslateDan.Text += arraygainval[i, j].ToString();
                                    TextBoxTranslateDan.Text += ");" + "\r\n";
                                    break;
                                }
                            case 8: // envd
                                {
                                    TextBoxTranslateDan.Text += "(" + j.ToString() + ", "; // instance
                                    if (arrayval1[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arrayval1[i, j]]; else TextBoxTranslateDan.Text += arrayval1value[i, j].ToString();
                                    TextBoxTranslateDan.Text += ", ";
                                    if (arrayval2[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arrayval2[i, j]]; else TextBoxTranslateDan.Text += arrayval2value[i, j].ToString();
                                    TextBoxTranslateDan.Text += ", ";
                                    if (arraygain[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arraygain[i, j]]; else TextBoxTranslateDan.Text += arraygainval[i, j].ToString();
                                    TextBoxTranslateDan.Text += ");" + "\r\n";
                                    break;
                                }
                            case 9: // add
                                {
                                    TextBoxTranslateDan.Text += "(" + arrayvartext[arrayval1[i, j]];
                                    //if (arrayval1[i, j] == 0) { MessageBox.Show("Error in instrument " + (i + 1).ToString()); return; }
                                    TextBoxTranslateDan.Text += ", ";
                                    if (arrayval2[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arrayval2[i, j]]; else TextBoxTranslateDan.Text += arrayval2value[i, j].ToString();
                                    TextBoxTranslateDan.Text += ");" + "\r\n";
                                    break;
                                }
                            case 10: // mul
                                {
                                    TextBoxTranslateDan.Text += "(" + arrayvartext[arrayval1[i, j]];
                                    //if (arrayval1[i, j] == 0) { MessageBox.Show("Error in instrument " + (i + 1).ToString()); return; }
                                    TextBoxTranslateDan.Text += ", ";
                                    if (arrayval2[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arrayval2[i, j]]; else TextBoxTranslateDan.Text += arrayval2value[i, j].ToString();
                                    TextBoxTranslateDan.Text += ");" + "\r\n";
                                    break;
                                }
                            case 11: // delay
                                {
                                    TextBoxTranslateDan.Text += "(" + j.ToString() + ", "; // instance
                                    TextBoxTranslateDan.Text += arrayvartext[arrayval1[i, j]];
                                    if (arrayval1[i, j] == 0) { MessageBox.Show("Error in instrument " + (i + 1).ToString()); return; }
                                    TextBoxTranslateDan.Text += ", ";
                                    if (arrayfrequency[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arrayfrequency[i, j]]; else TextBoxTranslateDan.Text += arrayfrequencyval[i, j].ToString();
                                    TextBoxTranslateDan.Text += ", ";
                                    if (arraygain[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arraygain[i, j]]; else TextBoxTranslateDan.Text += arraygainval[i, j].ToString();
                                    TextBoxTranslateDan.Text += ");" + "\r\n";
                                    break;
                                }
                            case 12: // comb
                                {
                                    TextBoxTranslateDan.Text += "(" + j.ToString() + ", "; // instance
                                    TextBoxTranslateDan.Text += arrayvartext[arrayval1[i, j]];
                                    //if (arrayval1[i, j] == 0) { MessageBox.Show("Error in instrument " + (i + 1).ToString()); return; }
                                    TextBoxTranslateDan.Text += ", ";
                                    if (arrayfrequency[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arrayfrequency[i, j]]; else TextBoxTranslateDan.Text += arrayfrequencyval[i, j].ToString();
                                    TextBoxTranslateDan.Text += ", ";
                                    if (arrayval2[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arrayval2[i, j]]; else TextBoxTranslateDan.Text += arrayval2value[i, j].ToString();
                                    TextBoxTranslateDan.Text += ", ";
                                    if (arraygain[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arraygain[i, j]]; else TextBoxTranslateDan.Text += arraygainval[i, j].ToString();
                                    TextBoxTranslateDan.Text += ");" + "\r\n";
                                    break;
                                }
                            case 13: // reverb
                                {
                                    TextBoxTranslateDan.Text += "(" + arrayvartext[arrayval1[i, j]];
                                    if (arrayval1[i, j] == 0) { MessageBox.Show("Error in instrument " + (i + 1).ToString()); return; }
                                    TextBoxTranslateDan.Text += ", ";
                                    if (arrayval2[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arrayval2[i, j]]; else TextBoxTranslateDan.Text += arrayval2value[i, j].ToString();
                                    TextBoxTranslateDan.Text += ", ";
                                    if (arraygain[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arraygain[i, j]]; else TextBoxTranslateDan.Text += arraygainval[i, j].ToString();
                                    TextBoxTranslateDan.Text += ");" + "\r\n";
                                    break;
                                }
                            case 14: // ctrl
                                {
                                    TextBoxTranslateDan.Text += "(" + arrayvartext[arrayval1[i, j]];
                                    //if (arrayval1[i, j] == 0) { MessageBox.Show("Error in instrument " + (i + 1).ToString()); return; }
                                    TextBoxTranslateDan.Text += ");" + "\r\n";
                                    break;
                                }
                            case 15: // filter
                                {
                                    TextBoxTranslateDan.Text += "(" + j.ToString() + ", "; // instance
                                    TextBoxTranslateDan.Text += arrayvartext[arrayval1[i, j]];
                                    if (arrayval1[i, j] == 0) { MessageBox.Show("Error in instrument " + (i + 1).ToString()); return; }
                                    TextBoxTranslateDan.Text += ", ";
                                    if (arrayfrequency[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arrayfrequency[i, j]]; else TextBoxTranslateDan.Text += arrayfrequencyval[i, j].ToString();
                                    TextBoxTranslateDan.Text += ", ";
                                    if (arrayval2[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arrayval2[i, j]]; else TextBoxTranslateDan.Text += arrayval2value[i, j].ToString();
                                    TextBoxTranslateDan.Text += ", ";
                                    TextBoxTranslateDan.Text += arraygain[i, j].ToString();
                                    TextBoxTranslateDan.Text += ");" + "\r\n";
                                    break;
                                }
                            case 16: // clamp
                                {
                                    // TextBoxTranslateDan.Text += "(" + arrayvartext[arrayval1[i, j]];
                                    // if (arrayval1[i, j] == 0) { MessageBox.Show("Error in instrument " + (i + 1).ToString()); return; }
                                    // TextBoxTranslateDan.Text += ");" + "\r\n";
                                    // break;
                                    TextBoxTranslateDan.Text += "(" + arrayvartext[arrayval1[i, j]] + ", ";
                                    //if (arrayval1[i, j] == 0) { MessageBox.Show("Error in instrument " + (i + 1).ToString()); return; }
                                    if (arraygain[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arraygain[i, j]]; else TextBoxTranslateDan.Text += arraygainval[i, j].ToString();
                                    TextBoxTranslateDan.Text += ");" + "\r\n";
                                    break;
                                }
                            case 17: // clone TO BE DONE FOR DAN

                                {
                                    if (arraygainval[i, j] == 0) // forward
                                    {
                                        TextBoxTranslateDan.Text += "clone(smp," + arraygain[i, j].ToString() + ", " + arrayval2value[i, j].ToString();
                                    }
                                    else
                                    {
                                        TextBoxTranslateDan.Text += "clone_reverse(smp," + arraygain[i, j].ToString() + ", " + arrayval2value[i, j].ToString();
                                    }
                                    TextBoxTranslateDan.Text += ");" + "\r\n";
                                    break;
                                }

                            case 18: // chordgen
                                {
                                    TextBoxTranslateDan.Text += "(" + j.ToString() + ", "; // instance (dan wanted it instead of smp)
                                    //TextBoxTranslateDan.Text += "(" + "smp" + ", ";
                                    TextBoxTranslateDan.Text += arraygain[i, j].ToString();
                                    TextBoxTranslateDan.Text += ", ";
                                    TextBoxTranslateDan.Text += arrayfrequency[i, j].ToString();
                                    TextBoxTranslateDan.Text += ", ";
                                    TextBoxTranslateDan.Text += arraywidth[i, j].ToString();
                                    TextBoxTranslateDan.Text += ", ";
                                    TextBoxTranslateDan.Text += arrayval1[i, j].ToString();
                                    TextBoxTranslateDan.Text += ", ";
                                    if (arrayval2[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arrayval2[i, j]]; else TextBoxTranslateDan.Text += arrayval2value[i, j].ToString();
                                    TextBoxTranslateDan.Text += ");" + "\r\n";
                                    break;
                                }
                            case 19: // sample and hold
                                {
                                    TextBoxTranslateDan.Text += "(" + j.ToString() + ", "; // instance
                                    TextBoxTranslateDan.Text += arrayvartext[arrayval1[i, j]] + ", ";
                                    //if (arrayval1[i, j] == 0) { MessageBox.Show("Error in instrument " + (i + 1).ToString()); return; }
                                    if (arraygain[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arraygain[i, j]]; else TextBoxTranslateDan.Text += arraygainval[i, j].ToString();
                                    TextBoxTranslateDan.Text += ");" + "\r\n";
                                    break;
                                }
                            case 20: // imported sample
                                {
                                    TextBoxTranslateDan.Text += "imported_sample(smp," + arraygain[i, j].ToString() + ");\r\n";
                                    // ternary operation
                                    break;
                                }
                            case 21: // one pole filter
                                {
                                    TextBoxTranslateDan.Text += "(" + j.ToString() + ", "; // instance
                                    TextBoxTranslateDan.Text += arrayvartext[arrayval1[i, j]];
                                    //if (arrayval1[i, j] == 0) { MessageBox.Show("Error in instrument " + (i + 1).ToString()); return; }
                                    TextBoxTranslateDan.Text += ", ";
                                    if (arrayfrequency[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arrayfrequency[i, j]]; else TextBoxTranslateDan.Text += arrayfrequencyval[i, j].ToString();
                                    TextBoxTranslateDan.Text += ", ";
                                    TextBoxTranslateDan.Text += arraygain[i, j].ToString();
                                    TextBoxTranslateDan.Text += ");" + "\r\n";
                                    break;
                                }
                            case 23: // adsr envelope
                                {
                                    // OPERATOR IN THE GUI HAS:
                                    int attackLength = (arrayval2value[i, j] << 8) + 1;
                                    int decayLength = (arrayval1value[i, j] << 8) + 1;
                                    int releaseLength = (arrayfrequencyval[i, j] << 8) + 1;
                                    int sustainLength = samplelength[i] - attackLength - decayLength - releaseLength;
                                    byte gain = (arraygainval[i, j]);
                                    short sustainValue = (short)(arraywidthval[i, j] << 8);        //

                                    int peak = (32767 * gain) << 1;
                                    int sustainLevel = (sustainValue * gain) << 1;
                                    int attackAmount = peak / attackLength;
                                    int decayAmount = (peak - sustainLevel) / decayLength;
                                    int releaseAmount = sustainLevel / releaseLength;

                                    TextBoxTranslateDan.Text += "(" + j.ToString() + ", "; // instance
                                    //attack
                                    TextBoxTranslateDan.Text += attackAmount.ToString();
                                    TextBoxTranslateDan.Text += ", ";
                                    //decay
                                    TextBoxTranslateDan.Text += decayAmount.ToString();
                                    TextBoxTranslateDan.Text += ", ";
                                    //sustain level
                                    TextBoxTranslateDan.Text += sustainLevel.ToString();
                                    TextBoxTranslateDan.Text += ", ";
                                    //sustain
                                    TextBoxTranslateDan.Text += sustainLength.ToString();
                                    TextBoxTranslateDan.Text += ", ";
                                    //release
                                    TextBoxTranslateDan.Text += releaseAmount.ToString();
                                    TextBoxTranslateDan.Text += ", ";
                                    //peak
                                    TextBoxTranslateDan.Text += peak.ToString();
                                    TextBoxTranslateDan.Text += ");" + "\r\n";
                                    break;
                                }
                        }
                    }
                }
                TextBoxTranslateDan.Text = TextBoxTranslateDan.Text + "\r\n";
            }


            DialogResult dialogResult = MessageBox.Show("Are you sure?", "export ASM file", MessageBoxButtons.OKCancel);
            if (dialogResult == DialogResult.OK)
            {
                // save files
                string root = Application.StartupPath;
                string filename = root + "/exe_creator/script.txt";
                using (TextWriter writer = new StreamWriter(File.Open(filename, FileMode.Create)))
                {
                    writer.Write(TextBoxTranslateDan.Text); // save script.txt
                    writer.Close();
                }
                MessageBox.Show("exporting exemusic.asm to folder exe_creator");

                // transfer imp samples from 2d to 1d array
                int smp2 = 0;
                for (int j = 0; j < 8; j++)
                {
                    for (int smp = 0; smp < importedlength[j]; smp++)
                    {
                        importedsample1d[smp2] = importedsample[smp, j];
                        smp2++;
                    }
                }

                // delta encode
                // calc length of all imported samples
                int implength = 0;
                for (int s = 0; s < 8; s++) { implength += importedlength[s]; }
                delta_encode(importedsample1d, implength);

                // save delta encoded imported samples
                filename = root + "/exe_creator/Isamp.raw";
                using (BinaryWriter writer = new BinaryWriter(File.Open(filename, FileMode.Create)))
                {
                    for (int smp = 0; smp < implength; smp++)
                    {
                        writer.Write(importedsample1d[smp]);
                    }
                    writer.Close();
                }



                // execute bat
                string path = Application.StartupPath;
                Process.Start(path + "/exe_creator/aklang2asm.bat");
            }
        }



        // ****************************************************************************
        // Export Atari prg 
        // ****************************************************************************

        private void exportAtariExeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CheckErrorSave() == true) { MessageBox.Show("Error in instruments"); return; }

            char temp = ' ';
            // make dan table
            TextBoxTranslateDan.Clear();

            // write imported sample lengths with comma seperation
            TextBoxTranslateDan.Text = TextBoxTranslateDan.Text + importedlength[0].ToString() + ", ";
            TextBoxTranslateDan.Text = TextBoxTranslateDan.Text + importedlength[1].ToString() + ", ";
            TextBoxTranslateDan.Text = TextBoxTranslateDan.Text + importedlength[2].ToString() + ", ";
            TextBoxTranslateDan.Text = TextBoxTranslateDan.Text + importedlength[3].ToString() + ", ";
            TextBoxTranslateDan.Text = TextBoxTranslateDan.Text + importedlength[4].ToString() + ", ";
            TextBoxTranslateDan.Text = TextBoxTranslateDan.Text + importedlength[5].ToString() + ", ";
            TextBoxTranslateDan.Text = TextBoxTranslateDan.Text + importedlength[6].ToString() + ", ";
            TextBoxTranslateDan.Text = TextBoxTranslateDan.Text + importedlength[7].ToString() + "\r\n";


            // make inst table
            TextBoxTranslateInst.Clear();
            for (int i = 0; i < getnumberofhighestinstrument(); i++)
            {

                TextBoxTranslateDan.Text = TextBoxTranslateDan.Text + "$ " + ComboBoxSampleAuswahl.Items[i].ToString() + ", ";    // instrument name
                TextBoxTranslateDan.Text = TextBoxTranslateDan.Text + samplelength[i].ToString() + ", ";   // sample length
                TextBoxTranslateDan.Text = TextBoxTranslateDan.Text + loopoffset[i].ToString() + ", "; // loop offset
                TextBoxTranslateDan.Text = TextBoxTranslateDan.Text + looplength[i].ToString() + ", "; // loop length
                if (arrayfunction[i, 15] == 22) temp = 'Y'; else temp = 'N'; // is loopgen usen ?
                TextBoxTranslateDan.Text = TextBoxTranslateDan.Text + temp + "\r\n#\r\n"; // loop flag (loopgen used)

                for (int j = 0; j < 16; j++)
                {
                    if (arrayvar[i, j] != 0 && arrayfunction[i, j] != 22)
                    {
                        TextBoxTranslateDan.Text = TextBoxTranslateDan.Text + arrayvartext[arrayvar[i, j]] + " = " + arrayfunctiontext[arrayfunction[i, j]];
                        switch (arrayfunction[i, j])
                        {
                            case 0: break;
                            case 1: // vol
                                {
                                    TextBoxTranslateDan.Text += "(" + arrayvartext[arrayval1[i, j]] + ", ";
                                    //if (arrayval1[i, j] == 0) { MessageBox.Show("Error in instrument " + (i + 1).ToString()); return; }
                                    if (arraygain[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arraygain[i, j]]; else TextBoxTranslateDan.Text += arraygainval[i, j].ToString();
                                    TextBoxTranslateDan.Text += ");" + "\r\n";
                                    break;
                                }
                            case 2: // osc saw
                                {
                                    TextBoxTranslateDan.Text += "(" + j.ToString() + ", "; // instance
                                    if (arrayfrequency[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arrayfrequency[i, j]]; else TextBoxTranslateDan.Text += arrayfrequencyval[i, j].ToString();
                                    TextBoxTranslateDan.Text += ", ";
                                    if (arraygain[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arraygain[i, j]]; else TextBoxTranslateDan.Text += arraygainval[i, j].ToString();
                                    TextBoxTranslateDan.Text += ");" + "\r\n";
                                    break;
                                }
                            case 3: // osc tri
                                {
                                    TextBoxTranslateDan.Text += "(" + j.ToString() + ", "; // instance
                                    if (arrayfrequency[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arrayfrequency[i, j]]; else TextBoxTranslateDan.Text += arrayfrequencyval[i, j].ToString();
                                    TextBoxTranslateDan.Text += ", ";
                                    if (arraygain[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arraygain[i, j]]; else TextBoxTranslateDan.Text += arraygainval[i, j].ToString();
                                    TextBoxTranslateDan.Text += ");" + "\r\n";
                                    break;
                                }
                            case 4: // osc sine
                                {
                                    TextBoxTranslateDan.Text += "(" + j.ToString() + ", "; // instance
                                    if (arrayfrequency[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arrayfrequency[i, j]]; else TextBoxTranslateDan.Text += arrayfrequencyval[i, j].ToString();
                                    TextBoxTranslateDan.Text += ", ";
                                    if (arraygain[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arraygain[i, j]]; else TextBoxTranslateDan.Text += arraygainval[i, j].ToString();
                                    TextBoxTranslateDan.Text += ");" + "\r\n";
                                    break;
                                }
                            case 5: // osc pulse
                                {
                                    TextBoxTranslateDan.Text += "(" + j.ToString() + ", "; // instance
                                    if (arrayfrequency[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arrayfrequency[i, j]]; else TextBoxTranslateDan.Text += arrayfrequencyval[i, j].ToString();
                                    TextBoxTranslateDan.Text += ", ";
                                    if (arraygain[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arraygain[i, j]]; else TextBoxTranslateDan.Text += arraygainval[i, j].ToString();
                                    TextBoxTranslateDan.Text += ", ";
                                    if (arraywidth[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arraywidth[i, j]]; else TextBoxTranslateDan.Text += arraywidthval[i, j].ToString();
                                    TextBoxTranslateDan.Text += ");" + "\r\n";
                                    break;
                                }
                            case 6: // osc noise
                                {
                                    TextBoxTranslateDan.Text += "(";
                                    if (arraygain[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arraygain[i, j]]; else TextBoxTranslateDan.Text += arraygainval[i, j].ToString();
                                    TextBoxTranslateDan.Text += ");" + "\r\n";
                                    break;
                                }
                            case 7: // enva
                                {
                                    TextBoxTranslateDan.Text += "(" + j.ToString() + ", "; // instance
                                    if (arrayval1[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arrayval1[i, j]]; else TextBoxTranslateDan.Text += arrayval1value[i, j].ToString();
                                    TextBoxTranslateDan.Text += ", ";
                                    TextBoxTranslateDan.Text += "0, ";
                                    if (arraygain[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arraygain[i, j]]; else TextBoxTranslateDan.Text += arraygainval[i, j].ToString();
                                    TextBoxTranslateDan.Text += ");" + "\r\n";
                                    break;
                                }
                            case 8: // envd
                                {
                                    TextBoxTranslateDan.Text += "(" + j.ToString() + ", "; // instance
                                    if (arrayval1[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arrayval1[i, j]]; else TextBoxTranslateDan.Text += arrayval1value[i, j].ToString();
                                    TextBoxTranslateDan.Text += ", ";
                                    if (arrayval2[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arrayval2[i, j]]; else TextBoxTranslateDan.Text += arrayval2value[i, j].ToString();
                                    TextBoxTranslateDan.Text += ", ";
                                    if (arraygain[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arraygain[i, j]]; else TextBoxTranslateDan.Text += arraygainval[i, j].ToString();
                                    TextBoxTranslateDan.Text += ");" + "\r\n";
                                    break;
                                }
                            case 9: // add
                                {
                                    TextBoxTranslateDan.Text += "(" + arrayvartext[arrayval1[i, j]];
                                    //if (arrayval1[i, j] == 0) { MessageBox.Show("Error in instrument " + (i + 1).ToString()); return; }
                                    TextBoxTranslateDan.Text += ", ";
                                    if (arrayval2[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arrayval2[i, j]]; else TextBoxTranslateDan.Text += arrayval2value[i, j].ToString();
                                    TextBoxTranslateDan.Text += ");" + "\r\n";
                                    break;
                                }
                            case 10: // mul
                                {
                                    TextBoxTranslateDan.Text += "(" + arrayvartext[arrayval1[i, j]];
                                    //if (arrayval1[i, j] == 0) { MessageBox.Show("Error in instrument " + (i + 1).ToString()); return; }
                                    TextBoxTranslateDan.Text += ", ";
                                    if (arrayval2[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arrayval2[i, j]]; else TextBoxTranslateDan.Text += arrayval2value[i, j].ToString();
                                    TextBoxTranslateDan.Text += ");" + "\r\n";
                                    break;
                                }
                            case 11: // delay
                                {
                                    TextBoxTranslateDan.Text += "(" + j.ToString() + ", "; // instance
                                    TextBoxTranslateDan.Text += arrayvartext[arrayval1[i, j]];
                                    if (arrayval1[i, j] == 0) { MessageBox.Show("Error in instrument " + (i + 1).ToString()); return; }
                                    TextBoxTranslateDan.Text += ", ";
                                    if (arrayfrequency[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arrayfrequency[i, j]]; else TextBoxTranslateDan.Text += arrayfrequencyval[i, j].ToString();
                                    TextBoxTranslateDan.Text += ", ";
                                    if (arraygain[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arraygain[i, j]]; else TextBoxTranslateDan.Text += arraygainval[i, j].ToString();
                                    TextBoxTranslateDan.Text += ");" + "\r\n";
                                    break;
                                }
                            case 12: // comb
                                {
                                    TextBoxTranslateDan.Text += "(" + j.ToString() + ", "; // instance
                                    TextBoxTranslateDan.Text += arrayvartext[arrayval1[i, j]];
                                    //if (arrayval1[i, j] == 0) { MessageBox.Show("Error in instrument " + (i + 1).ToString()); return; }
                                    TextBoxTranslateDan.Text += ", ";
                                    if (arrayfrequency[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arrayfrequency[i, j]]; else TextBoxTranslateDan.Text += arrayfrequencyval[i, j].ToString();
                                    TextBoxTranslateDan.Text += ", ";
                                    if (arrayval2[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arrayval2[i, j]]; else TextBoxTranslateDan.Text += arrayval2value[i, j].ToString();
                                    TextBoxTranslateDan.Text += ", ";
                                    if (arraygain[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arraygain[i, j]]; else TextBoxTranslateDan.Text += arraygainval[i, j].ToString();
                                    TextBoxTranslateDan.Text += ");" + "\r\n";
                                    break;
                                }
                            case 13: // reverb
                                {
                                    TextBoxTranslateDan.Text += "(" + arrayvartext[arrayval1[i, j]];
                                    //if (arrayval1[i, j] == 0) { MessageBox.Show("Error in instrument " + (i + 1).ToString()); return; }
                                    TextBoxTranslateDan.Text += ", ";
                                    if (arrayval2[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arrayval2[i, j]]; else TextBoxTranslateDan.Text += arrayval2value[i, j].ToString();
                                    TextBoxTranslateDan.Text += ", ";
                                    if (arraygain[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arraygain[i, j]]; else TextBoxTranslateDan.Text += arraygainval[i, j].ToString();
                                    TextBoxTranslateDan.Text += ");" + "\r\n";
                                    break;
                                }
                            case 14: // ctrl
                                {
                                    TextBoxTranslateDan.Text += "(" + arrayvartext[arrayval1[i, j]];
                                    //if (arrayval1[i, j] == 0) { MessageBox.Show("Error in instrument " + (i + 1).ToString()); return; }
                                    TextBoxTranslateDan.Text += ");" + "\r\n";
                                    break;
                                }
                            case 15: // filter
                                {
                                    TextBoxTranslateDan.Text += "(" + j.ToString() + ", "; // instance
                                    TextBoxTranslateDan.Text += arrayvartext[arrayval1[i, j]];
                                    if (arrayval1[i, j] == 0) { MessageBox.Show("Error in instrument " + (i + 1).ToString()); return; }
                                    TextBoxTranslateDan.Text += ", ";
                                    if (arrayfrequency[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arrayfrequency[i, j]]; else TextBoxTranslateDan.Text += arrayfrequencyval[i, j].ToString();
                                    TextBoxTranslateDan.Text += ", ";
                                    if (arrayval2[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arrayval2[i, j]]; else TextBoxTranslateDan.Text += arrayval2value[i, j].ToString();
                                    TextBoxTranslateDan.Text += ", ";
                                    TextBoxTranslateDan.Text += arraygain[i, j].ToString();
                                    TextBoxTranslateDan.Text += ");" + "\r\n";
                                    break;
                                }
                            case 16: // clamp
                                {
                                    // TextBoxTranslateDan.Text += "(" + arrayvartext[arrayval1[i, j]];
                                    // if (arrayval1[i, j] == 0) { MessageBox.Show("Error in instrument " + (i + 1).ToString()); return; }
                                    // TextBoxTranslateDan.Text += ");" + "\r\n";
                                    // break;
                                    TextBoxTranslateDan.Text += "(" + arrayvartext[arrayval1[i, j]] + ", ";
                                    //if (arrayval1[i, j] == 0) { MessageBox.Show("Error in instrument " + (i + 1).ToString()); return; }
                                    if (arraygain[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arraygain[i, j]]; else TextBoxTranslateDan.Text += arraygainval[i, j].ToString();
                                    TextBoxTranslateDan.Text += ");" + "\r\n";
                                    break;
                                }
                            case 17: // clone TO BE DONE FOR DAN

                                {
                                    if (arraygainval[i, j] == 0) // forward
                                    {
                                        TextBoxTranslateDan.Text += "clone(smp," + arraygain[i, j].ToString() + ", " + arrayval2value[i, j].ToString();
                                    }
                                    else
                                    {
                                        TextBoxTranslateDan.Text += "clone_reverse(smp," + arraygain[i, j].ToString() + ", " + arrayval2value[i, j].ToString();
                                    }
                                    TextBoxTranslateDan.Text += ");" + "\r\n";
                                    break;
                                }

                            case 18: // chordgen
                                {
                                    TextBoxTranslateDan.Text += "(" + j.ToString() + ", "; // instance (dan wanted it instead of smp)
                                    //TextBoxTranslateDan.Text += "(" + "smp" + ", ";
                                    TextBoxTranslateDan.Text += arraygain[i, j].ToString();
                                    TextBoxTranslateDan.Text += ", ";
                                    TextBoxTranslateDan.Text += arrayfrequency[i, j].ToString();
                                    TextBoxTranslateDan.Text += ", ";
                                    TextBoxTranslateDan.Text += arraywidth[i, j].ToString();
                                    TextBoxTranslateDan.Text += ", ";
                                    TextBoxTranslateDan.Text += arrayval1[i, j].ToString();
                                    TextBoxTranslateDan.Text += ", ";
                                    if (arrayval2[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arrayval2[i, j]]; else TextBoxTranslateDan.Text += arrayval2value[i, j].ToString();
                                    TextBoxTranslateDan.Text += ");" + "\r\n";
                                    break;
                                }
                            case 19: // sample and hold
                                {
                                    TextBoxTranslateDan.Text += "(" + j.ToString() + ", "; // instance
                                    TextBoxTranslateDan.Text += arrayvartext[arrayval1[i, j]] + ", ";
                                    if (arrayval1[i, j] == 0) { MessageBox.Show("Error in instrument " + (i + 1).ToString()); return; }
                                    if (arraygain[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arraygain[i, j]]; else TextBoxTranslateDan.Text += arraygainval[i, j].ToString();
                                    TextBoxTranslateDan.Text += ");" + "\r\n";
                                    break;
                                }
                            case 20: // imported sample
                                {
                                    TextBoxTranslateDan.Text += "imported_sample(smp," + arraygain[i, j].ToString() + ");\r\n";
                                    // ternary operation
                                    break;
                                }
                            case 21: // one pole filter
                                {
                                    TextBoxTranslateDan.Text += "(" + j.ToString() + ", "; // instance
                                    TextBoxTranslateDan.Text += arrayvartext[arrayval1[i, j]];
                                    //if (arrayval1[i, j] == 0) { MessageBox.Show("Error in instrument " + (i + 1).ToString()); return; }
                                    TextBoxTranslateDan.Text += ", ";
                                    if (arrayfrequency[i, j] > 0) TextBoxTranslateDan.Text += arrayvartext[arrayfrequency[i, j]]; else TextBoxTranslateDan.Text += arrayfrequencyval[i, j].ToString();
                                    TextBoxTranslateDan.Text += ", ";
                                    TextBoxTranslateDan.Text += arraygain[i, j].ToString();
                                    TextBoxTranslateDan.Text += ");" + "\r\n";
                                    break;
                                }
                            case 23: // adsr envelope
                                {
                                    // OPERATOR IN THE GUI HAS:
                                    int attackLength = (arrayval2value[i, j] << 8) + 1;
                                    int decayLength = (arrayval1value[i, j] << 8) + 1;
                                    int releaseLength = (arrayfrequencyval[i, j] << 8) + 1;
                                    int sustainLength = samplelength[i] - attackLength - decayLength - releaseLength;
                                    byte gain = (arraygainval[i, j]);
                                    short sustainValue = (short)(arraywidthval[i, j] << 8);        //

                                    int peak = (32767 * gain) << 1;
                                    int sustainLevel = (sustainValue * gain) << 1;
                                    int attackAmount = peak / attackLength;
                                    int decayAmount = (peak - sustainLevel) / decayLength;
                                    int releaseAmount = sustainLevel / releaseLength;

                                    TextBoxTranslateDan.Text += "(" + j.ToString() + ", "; // instance
                                    //attack
                                    TextBoxTranslateDan.Text += attackAmount.ToString();
                                    TextBoxTranslateDan.Text += ", ";
                                    //decay
                                    TextBoxTranslateDan.Text += decayAmount.ToString();
                                    TextBoxTranslateDan.Text += ", ";
                                    //sustain level
                                    TextBoxTranslateDan.Text += sustainLevel.ToString();
                                    TextBoxTranslateDan.Text += ", ";
                                    //sustain
                                    TextBoxTranslateDan.Text += sustainLength.ToString();
                                    TextBoxTranslateDan.Text += ", ";
                                    //release
                                    TextBoxTranslateDan.Text += releaseAmount.ToString();
                                    TextBoxTranslateDan.Text += ", ";
                                    //peak
                                    TextBoxTranslateDan.Text += peak.ToString();
                                    TextBoxTranslateDan.Text += ");" + "\r\n";
                                    break;
                                }
                        }
                    }
                }
                TextBoxTranslateDan.Text = TextBoxTranslateDan.Text + "\r\n";
            }


            //DialogResult dialogResult = MessageBox.Show("Are you sure?", "export ASM file", MessageBoxButtons.OKCancel);
            //if (dialogResult == DialogResult.OK)
            {
                // save files
                string root = Application.StartupPath;
                string filename2 = root + "/exe_creator/script.txt";
                using (TextWriter writer = new StreamWriter(File.Open(filename2, FileMode.Create)))
                {
                    writer.Write(TextBoxTranslateDan.Text); // save script.txt
                    writer.Close();
                }
              //  MessageBox.Show("exporting exemusic.asm to folder exe_creator");

                // transfer imp samples from 2d to 1d array
                int smp2 = 0;
                for (int j = 0; j < 8; j++)
                {
                    for (int smp = 0; smp < importedlength[j]; smp++)
                    {
                        importedsample1d[smp2] = importedsample[smp, j];
                        smp2++;
                    }
                }

                // delta encode
                // calc length of all imported samples
                int implength = 0;
                for (int s = 0; s < 8; s++) { implength += importedlength[s]; }
                delta_encode(importedsample1d, implength);

                // save delta encoded imported samples
                filename2 = root + "/exe_creator/Isamp.raw";
                using (BinaryWriter writer = new BinaryWriter(File.Open(filename2, FileMode.Create)))
                {
                    for (int smp = 0; smp < implength; smp++)
                    {
                        writer.Write(importedsample1d[smp]);
                    }
                    writer.Close();
                }

                // execute bat
            //    string path = Application.StartupPath;
            //    Process.Start(path + "/exe_creator/aklang2asm.bat");
            }
            MessageBox.Show("choose your mod file!");
            long filelength = 0;
            string filename;
            hideGroupBoxes();
            openFileDialog1.Filter = "mod (*.mod)|*.mod|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Title = "Create exe from mod file";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Stream myStream;
                if ((myStream = openFileDialog1.OpenFile()) != null)
                {
                    filename = openFileDialog1.FileName;
                    filelength = myStream.Length;
                    BinaryReader reader = new BinaryReader(myStream);

                    for (int i = 0; i < filelength; i++)
                    {
                        amigamod[i] = reader.ReadByte();
                    }
                    myStream.Close();

                    // check if really a mod file (M.K.)
                    if (!(amigamod[1080] == 77 && amigamod[1081] == 46 && amigamod[1082] == 75 && amigamod[1083] == 46))
                    {
                        MessageBox.Show("Not a mod file!!"); return;
                    }


                    // save empty mod 
                    filename = "exe_creator/AtariKlang.mod";

                    using (BinaryWriter writer = new BinaryWriter(File.Open(filename, FileMode.Create)))
                    {
                        for (int i = 0; i < filelength; i++)
                        {
                            writer.Write(amigamod[i]);
                        }
                    }
                }

                // execute bat
                string path = Application.StartupPath;
                Process.Start(path + "/exe_creator/prg_creator.bat");
            }
            else return;



        }



        // swap slots for buttons up and down
        void swapslots(int instr, int slot1, int slot2)
        {
            int inttemp;
            short shorttemp;
            byte bytetemp;

            inttemp = arrayvar[instr, slot1];
            arrayvar[instr, slot1] = arrayvar[instr, slot2];
            arrayvar[instr, slot2] = inttemp;

            inttemp = arrayfunction[instr, slot1];
            arrayfunction[instr, slot1] = arrayfunction[instr, slot2];
            arrayfunction[instr, slot2] = inttemp;

            shorttemp = arrayfrequency[instr, slot1];
            arrayfrequency[instr, slot1] = arrayfrequency[instr, slot2];
            arrayfrequency[instr, slot2] = shorttemp;

            shorttemp = arrayfrequencyval[instr, slot1];
            arrayfrequencyval[instr, slot1] = arrayfrequencyval[instr, slot2];
            arrayfrequencyval[instr, slot2] = shorttemp;

            bytetemp = arraygain[instr, slot1];
            arraygain[instr, slot1] = arraygain[instr, slot2];
            arraygain[instr, slot2] = bytetemp;

            bytetemp = arraygainval[instr, slot1];
            arraygainval[instr, slot1] = arraygainval[instr, slot2];
            arraygainval[instr, slot2] = bytetemp;

            bytetemp = arraywidth[instr, slot1];
            arraywidth[instr, slot1] = arraywidth[instr, slot2];
            arraywidth[instr, slot2] = bytetemp;

            bytetemp = arraywidthval[instr, slot1];
            arraywidthval[instr, slot1] = arraywidthval[instr, slot2];
            arraywidthval[instr, slot2] = bytetemp;

            shorttemp = arrayval1[instr, slot1];
            arrayval1[instr, slot1] = arrayval1[instr, slot2];
            arrayval1[instr, slot2] = shorttemp;

            shorttemp = arrayval1value[instr, slot1];
            arrayval1value[instr, slot1] = arrayval1value[instr, slot2];
            arrayval1value[instr, slot2] = shorttemp;

            shorttemp = arrayval2[instr, slot1];
            arrayval2[instr, slot1] = arrayval2[instr, slot2];
            arrayval2[instr, slot2] = shorttemp;

            shorttemp = arrayval2value[instr, slot1];
            arrayval2value[instr, slot1] = arrayval2value[instr, slot2];
            arrayval2value[instr, slot2] = shorttemp;
        }


        // buttons up and down
        private void buttonDown1_Click(object sender, EventArgs e)
        {
            hideGroupBoxes();
            swapslots(ComboBoxSampleAuswahl.SelectedIndex, 0, 1);
            ComboBoxVar1.SelectedIndex = arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 0];
            ComboBoxVar2.SelectedIndex = arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 1];
            ComboBoxFunction1.SelectedIndex = arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 0];
            ComboBoxFunction2.SelectedIndex = arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 1];
            CheckError();
        }
        private void buttonDown2_Click(object sender, EventArgs e)
        {
            hideGroupBoxes();
            swapslots(ComboBoxSampleAuswahl.SelectedIndex, 1, 2);
            ComboBoxVar2.SelectedIndex = arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 1];
            ComboBoxVar3.SelectedIndex = arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 2];
            ComboBoxFunction2.SelectedIndex = arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 1];
            ComboBoxFunction3.SelectedIndex = arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 2];
            CheckError();
        }
        private void buttonDown3_Click(object sender, EventArgs e)
        {
            hideGroupBoxes();
            swapslots(ComboBoxSampleAuswahl.SelectedIndex, 2, 3);
            ComboBoxVar3.SelectedIndex = arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 2];
            ComboBoxVar4.SelectedIndex = arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 3];
            ComboBoxFunction3.SelectedIndex = arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 2];
            ComboBoxFunction4.SelectedIndex = arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 3];
            CheckError();
        }
        private void buttonDown4_Click(object sender, EventArgs e)
        {
            hideGroupBoxes();
            swapslots(ComboBoxSampleAuswahl.SelectedIndex, 3, 4);
            ComboBoxVar4.SelectedIndex = arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 3];
            ComboBoxVar5.SelectedIndex = arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 4];
            ComboBoxFunction4.SelectedIndex = arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 3];
            ComboBoxFunction5.SelectedIndex = arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 4];
            CheckError();
        }
        private void buttonDown5_Click(object sender, EventArgs e)
        {
            hideGroupBoxes();
            swapslots(ComboBoxSampleAuswahl.SelectedIndex, 4, 5);
            ComboBoxVar5.SelectedIndex = arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 4];
            ComboBoxVar6.SelectedIndex = arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 5];
            ComboBoxFunction5.SelectedIndex = arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 4];
            ComboBoxFunction6.SelectedIndex = arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 5];
            CheckError();
        }
        private void buttonDown6_Click(object sender, EventArgs e)
        {
            hideGroupBoxes();
            swapslots(ComboBoxSampleAuswahl.SelectedIndex, 5, 6);
            ComboBoxVar6.SelectedIndex = arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 5];
            ComboBoxVar7.SelectedIndex = arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 6];
            ComboBoxFunction6.SelectedIndex = arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 5];
            ComboBoxFunction7.SelectedIndex = arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 6];
            CheckError();
        }
        private void buttonDown7_Click(object sender, EventArgs e)
        {
            hideGroupBoxes();
            swapslots(ComboBoxSampleAuswahl.SelectedIndex, 6, 7);
            ComboBoxVar7.SelectedIndex = arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 6];
            ComboBoxVar8.SelectedIndex = arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 7];
            ComboBoxFunction7.SelectedIndex = arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 6];
            ComboBoxFunction8.SelectedIndex = arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 7];
            CheckError();
        }
        private void buttonDown8_Click(object sender, EventArgs e)
        {
            hideGroupBoxes();
            swapslots(ComboBoxSampleAuswahl.SelectedIndex, 7, 8);
            ComboBoxVar8.SelectedIndex = arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 7];
            ComboBoxVar9.SelectedIndex = arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 8];
            ComboBoxFunction8.SelectedIndex = arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 7];
            ComboBoxFunction9.SelectedIndex = arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 8];
            CheckError();
        }
        private void buttonDown9_Click(object sender, EventArgs e)
        {
            hideGroupBoxes();
            swapslots(ComboBoxSampleAuswahl.SelectedIndex, 8, 9);
            ComboBoxVar9.SelectedIndex = arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 8];
            ComboBoxVar10.SelectedIndex = arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 9];
            ComboBoxFunction9.SelectedIndex = arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 8];
            ComboBoxFunction10.SelectedIndex = arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 9];
            CheckError();
        }
        private void buttonDown10_Click(object sender, EventArgs e)
        {
            hideGroupBoxes();
            swapslots(ComboBoxSampleAuswahl.SelectedIndex, 9, 10);
            ComboBoxVar10.SelectedIndex = arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 9];
            ComboBoxVar11.SelectedIndex = arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 10];
            ComboBoxFunction10.SelectedIndex = arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 9];
            ComboBoxFunction11.SelectedIndex = arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 10];
            CheckError();
        }
        private void buttonDown11_Click(object sender, EventArgs e)
        {
            hideGroupBoxes();
            swapslots(ComboBoxSampleAuswahl.SelectedIndex, 10, 11);
            ComboBoxVar11.SelectedIndex = arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 10];
            ComboBoxVar12.SelectedIndex = arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 11];
            ComboBoxFunction11.SelectedIndex = arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 10];
            ComboBoxFunction12.SelectedIndex = arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 11];
            CheckError();
        }
        private void buttonDown12_Click(object sender, EventArgs e)
        {
            hideGroupBoxes();
            swapslots(ComboBoxSampleAuswahl.SelectedIndex, 11, 12);
            ComboBoxVar12.SelectedIndex = arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 11];
            ComboBoxVar13.SelectedIndex = arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 12];
            ComboBoxFunction12.SelectedIndex = arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 11];
            ComboBoxFunction13.SelectedIndex = arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 12];
            CheckError();
        }
        private void buttonDown13_Click(object sender, EventArgs e)
        {
            hideGroupBoxes();
            swapslots(ComboBoxSampleAuswahl.SelectedIndex, 12, 13);
            ComboBoxVar13.SelectedIndex = arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 12];
            ComboBoxVar14.SelectedIndex = arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 13];
            ComboBoxFunction13.SelectedIndex = arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 12];
            ComboBoxFunction14.SelectedIndex = arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 13];
            CheckError();
        }
        private void buttonDown14_Click(object sender, EventArgs e)
        {
            hideGroupBoxes();
            swapslots(ComboBoxSampleAuswahl.SelectedIndex, 13, 14);
            ComboBoxVar14.SelectedIndex = arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 13];
            ComboBoxVar15.SelectedIndex = arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 14];
            ComboBoxFunction14.SelectedIndex = arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 13];
            ComboBoxFunction15.SelectedIndex = arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 14];
            CheckError();
        }
        private void buttonDown15_Click(object sender, EventArgs e)
        {
            hideGroupBoxes();
            if (ComboBoxFunction16.SelectedIndex < 19)
            {
                swapslots(ComboBoxSampleAuswahl.SelectedIndex, 14, 15);
                ComboBoxVar15.SelectedIndex = arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 14];
                ComboBoxVar16.SelectedIndex = arrayvar[ComboBoxSampleAuswahl.SelectedIndex, 15];
                ComboBoxFunction15.SelectedIndex = arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 14];
                ComboBoxFunction16.SelectedIndex = arrayfunction[ComboBoxSampleAuswahl.SelectedIndex, 15];
                CheckError();
            }
        }
        private void buttonUp2_Click(object sender, EventArgs e)
        {
            buttonDown1_Click(sender, e);
        }
        private void buttonUp3_Click(object sender, EventArgs e)
        {
            buttonDown2_Click(sender, e);
        }
        private void buttonUp4_Click(object sender, EventArgs e)
        {
            buttonDown3_Click(sender, e);
        }
        private void buttonUp5_Click(object sender, EventArgs e)
        {
            buttonDown4_Click(sender, e);
        }
        private void buttonUp6_Click(object sender, EventArgs e)
        {
            buttonDown5_Click(sender, e);
        }
        private void buttonUp7_Click(object sender, EventArgs e)
        {
            buttonDown6_Click(sender, e);
        }
        private void buttonUp8_Click(object sender, EventArgs e)
        {
            buttonDown7_Click(sender, e);
        }
        private void buttonUp9_Click(object sender, EventArgs e)
        {
            buttonDown8_Click(sender, e);
        }
        private void buttonUp10_Click(object sender, EventArgs e)
        {
            buttonDown9_Click(sender, e);
        }
        private void buttonUp11_Click(object sender, EventArgs e)
        {
            buttonDown10_Click(sender, e);
        }
        private void buttonUp12_Click(object sender, EventArgs e)
        {
            buttonDown11_Click(sender, e);
        }
        private void buttonUp13_Click(object sender, EventArgs e)
        {
            buttonDown12_Click(sender, e);
        }
        private void buttonUp14_Click(object sender, EventArgs e)
        {
            buttonDown13_Click(sender, e);
        }
        private void buttonUp15_Click(object sender, EventArgs e)
        {
            buttonDown14_Click(sender, e);
        }
        private void buttonUp16_Click(object sender, EventArgs e)
        {
            buttonDown15_Click(sender, e);
        }

        // timer for playback / stop
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Form1.waveout.PlaybackState != PlaybackState.Playing)
            {
                buttonStopSample.Visible = false;
                buttonPlaySample.Visible = true;
            }
        }




        void delta_encode(byte[] buffer, int length)
        {

            byte last = 0;
            for (int i = 0; i < length; i++)
            {
                byte current = buffer[i];
                buffer[i] = (byte)(current - last);
                last = current;
            }
        }


        void delta_decode(byte[] buffer, int length)
        {
            byte last = 0;
            for (int i = 0; i < length; i++)
            {
                byte delta = buffer[i];
                buffer[i] = (byte)(delta + last);
                last = buffer[i];
            }
        }
/*
        // Array mit Fibonacci folge fehlt hier. Algo ist getestet 
        void fibonacci_decode(byte[] buffer, int length, byte[] dest)
        {
            int i, lim = 0;
            byte d = 0;
            int x = 0;
            //lim = length << 1;
            for (i = 0; i < length; i++)
            {
                // Decode high nybble, then low nybble
                d = buffer[i];
                x += fibonacci[d];
                if (x > 127) x = 127;
                if (x < -128) x = -128;
                dest[i] = (byte)x;
            }
        }

        void fibonacci_encode(byte[] buffer, int len, byte[] dest)
        {
            int x = 0;
            for (int i = 0; i < len; i++)
            {
                int pr = (int)((sbyte)(buffer[i]));
                int delta = pr - x;
                int j = 15;
                while (j > 0 && delta < fibonacci[j])
                {
                    j--;
                }
                dest[i] = (byte)(j);
                x += fibonacci[j];
            }
        }
*/

        void xor_encode(byte[] buffer, int length)
        {
            byte prev = buffer[0];
            byte t;
            for (int i = 1; i < length; ++i)
            {
                t = buffer[i];
                buffer[i] ^= prev;
                prev = t;
            }
        }


        // delta encode test
        private void button2_Click(object sender, EventArgs e)
        {

        }

        // delta decode test
        private void button3_Click(object sender, EventArgs e)
        {

        }

        // keypress F1 for Render and F2 for play/stop
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F1)
            {
                //Falls Combobox noch editiert wurde, gab es absturz
                //Deswegen, Combobox verlassen
                ComboBoxSampleAuswahl_Leave(this, EventArgs.Empty);


                buttonGenerateAll.PerformClick();
                return true;    // indicate that you handled this keystroke
            }

            if (keyData == Keys.F2)
            {

                if (buttonStopSample.Visible == true)
                    buttonStopSample.PerformClick();
                if (buttonPlaySample.Visible == true)
                    buttonPlaySample.PerformClick();


                return true;    // indicate that you handled this keystroke
            }

            // Call the base class
            return base.ProcessCmdKey(ref msg, keyData);
        }



        private void CheckError()
        {
            System.Drawing.SolidBrush BrushOrange = new System.Drawing.SolidBrush(colCirclesErr);
            System.Drawing.SolidBrush BrushBackground = new System.Drawing.SolidBrush(colCirclesBg);

            System.Drawing.Graphics formGraphics;
            formGraphics = this.CreateGraphics();
            int i = ComboBoxSampleAuswahl.SelectedIndex;
            for (int j = 0; j < 16; j++)
            {
                bool errortemp = false;
                switch (arrayfunction[i, j])
                {
                    case 0: break;
                    case 1: //vol
                        { if (arrayval1[i, j] == 0) errortemp = true; break; }
                    case 2: break;
                    case 3: break;
                    case 4: break;
                    case 5: break;
                    case 6: break;
                    case 7: break;
                    case 8: break;
                    case 9: //add
                        { if (arrayval1[i, j] == 0) errortemp = true; break; }
                    case 10: //mul
                        { if (arrayval1[i, j] == 0) errortemp = true; break; }
                    case 11: //delay
                        { if (arrayval1[i, j] == 0) errortemp = true; break; }
                    case 12: //comb
                        { if (arrayval1[i, j] == 0) errortemp = true; break; }
                    case 13: //reverb
                        { if (arrayval1[i, j] == 0) errortemp = true; break; }
                    case 14: //ctrl
                        { if (arrayval1[i, j] == 0) errortemp = true; break; }
                    case 15: //filter
                        { if (arrayval1[i, j] == 0) errortemp = true; break; }
                    case 16: //clamp
                        { if (arrayval1[i, j] == 0) errortemp = true; break; }
                    case 17: break;
                    case 18: break;
                    case 19: //s&h
                        { if (arrayval1[i, j] == 0) errortemp = true; break; }
                    case 20: break;
                    case 21: //onepole
                        { if (arrayval1[i, j] == 0) errortemp = true; break; }
                    case 22: break; 
                    case 23: break;
                }
                if (
                    ((((arrayvar[i, j] == 0) && (arrayfunction[i, j] != 0)) ||
                    ((arrayvar[i, j] != 0) && (arrayfunction[i, j] == 0))) &&
                    (arrayfunction[i, j] != 22)) ||
                    errortemp == true)

                    formGraphics.FillEllipse(BrushOrange, new Rectangle(359, 136 + (j * 27), 14, 14));

                else formGraphics.FillEllipse(BrushBackground, new Rectangle(359, 136 + (j * 27), 14, 14));

            }
            BrushOrange.Dispose();
            BrushBackground.Dispose();
            formGraphics.Dispose();
        }

         bool CheckErrorSave()
        {
            bool errortemp = false;
            for (int i = 0; i < 31; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    if (((((arrayvar[i, j] == 0) && (arrayfunction[i, j] != 0)) || ((arrayvar[i, j] != 0) && (arrayfunction[i, j] == 0))) && (arrayfunction[i, j] != 22)))
                        errortemp = true;
                    switch (arrayfunction[i, j])
                    {
                        case 0: break;
                        case 1: //vol
                            { if (arrayval1[i, j] == 0) errortemp = true; break; }
                        case 2: break;
                        case 3: break;
                        case 4: break;
                        case 5: break;
                        case 6: break;
                        case 7: break;
                        case 8: break;
                        case 9: //add
                            { if (arrayval1[i, j] == 0) errortemp = true; break; }
                        case 10: //mul
                            { if (arrayval1[i, j] == 0) errortemp = true; break; }
                        case 11: //delay
                            { if (arrayval1[i, j] == 0) errortemp = true; break; }
                        case 12: //comb
                            { if (arrayval1[i, j] == 0) errortemp = true; break; }
                        case 13: //reverb
                            { if (arrayval1[i, j] == 0) errortemp = true; break; }
                        case 14: //ctrl
                            { if (arrayval1[i, j] == 0) errortemp = true; break; }
                        case 15: //filter
                            { if (arrayval1[i, j] == 0) errortemp = true; break; }
                        case 16: //clamp
                            { if (arrayval1[i, j] == 0) errortemp = true; break; }
                        case 17: break;
                        case 18: break;
                        case 19: //s&h
                            { if (arrayval1[i, j] == 0) errortemp = true; break; }
                        case 20: break;
                        case 21: //onepole
                            { if (arrayval1[i, j] == 0) errortemp = true; break; }
                        case 22: break;
                        case 23: break;
                    }

                }

            }
            return errortemp;
        }


        /*
        // mousewheel
        protected override void OnMouseWheel(MouseEventArgs e)
        {
           if (this.VScroll && (Control.ModifierKeys & Keys.Shift) == Keys.Shift)
            {
                this.VScroll = false;
               base.OnMouseWheel(e);
               this.VScroll = true;
            }
            else
            {
                base.OnMouseWheel(e);
            }
        }
        */


        // check textboxes and parse for their wertebereich
        // volume gain
        private void TextBoxVolGainValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                short parsedValue;
                if (!short.TryParse(TextBoxVolGainValue.Text, out parsedValue)) { TextBoxVolGainValue.Text = "0"; parsedValue = 0; }
                if (parsedValue > hScrollBarVolGainValue.Maximum) { TextBoxVolGainValue.Text = hScrollBarVolGainValue.Maximum.ToString(); parsedValue = (short)hScrollBarVolGainValue.Maximum; }
                hScrollBarVolGainValue.Value = parsedValue;
                arraygainval[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarVolGainValue.Value;
                e.SuppressKeyPress = true;
                this.ActiveControl = label1; // remove focus
            }
        }
        // osc saw freq
        private void TextBoxOscsawFreqValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                short parsedValue;
                if (!short.TryParse(TextBoxOscsawFreqValue.Text, out parsedValue)) { TextBoxOscsawFreqValue.Text = "0"; parsedValue = 0; }
                if (parsedValue > hScrollBarOscsawFreqValue.Maximum) { TextBoxOscsawFreqValue.Text = hScrollBarOscsawFreqValue.Maximum.ToString(); parsedValue = (short)hScrollBarOscsawFreqValue.Maximum; }
                hScrollBarOscsawFreqValue.Value = parsedValue;
                arrayfrequencyval[ComboBoxSampleAuswahl.SelectedIndex, edit] = (short)hScrollBarOscsawFreqValue.Value;
                e.SuppressKeyPress = true;
                this.ActiveControl = label1; // remove focus
            }
        }
        // osc saw gain
        private void TextBoxOscsawGainValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                short parsedValue;
                if (!short.TryParse(TextBoxOscsawGainValue.Text, out parsedValue)) { TextBoxOscsawGainValue.Text = "0"; parsedValue = 0; }
                if (parsedValue > hScrollBarOscsawGainValue.Maximum) { TextBoxOscsawGainValue.Text = hScrollBarOscsawGainValue.Maximum.ToString(); parsedValue = (short)hScrollBarOscsawGainValue.Maximum; }
                hScrollBarOscsawGainValue.Value = parsedValue;
                arraygainval[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarOscsawGainValue.Value;
                e.SuppressKeyPress = true;
                this.ActiveControl = label1; // remove focus
            }
        }
        // osc tri freq
        private void TextBoxOsctriFreqValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                short parsedValue;
                if (!short.TryParse(TextBoxOsctriFreqValue.Text, out parsedValue)) { TextBoxOsctriFreqValue.Text = "0"; parsedValue = 0; }
                if (parsedValue > hScrollBarOsctriFreqValue.Maximum) { TextBoxOsctriFreqValue.Text = hScrollBarOsctriFreqValue.Maximum.ToString(); parsedValue = (short)hScrollBarOsctriFreqValue.Maximum; }
                hScrollBarOsctriFreqValue.Value = parsedValue;
                arrayfrequencyval[ComboBoxSampleAuswahl.SelectedIndex, edit] = (short)hScrollBarOsctriFreqValue.Value;
                e.SuppressKeyPress = true;
                this.ActiveControl = label1; // remove focus
            }
        }
        // osc tri gain
        private void TextBoxOsctriGainValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                short parsedValue;
                if (!short.TryParse(TextBoxOsctriGainValue.Text, out parsedValue)) { TextBoxOsctriGainValue.Text = "0"; parsedValue = 0; }
                if (parsedValue > hScrollBarOsctriGainValue.Maximum) { TextBoxOsctriGainValue.Text = hScrollBarOsctriGainValue.Maximum.ToString(); parsedValue = (short)hScrollBarOsctriGainValue.Maximum; }
                hScrollBarOsctriGainValue.Value = parsedValue;
                arraygainval[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarOsctriGainValue.Value;
                e.SuppressKeyPress = true;
                this.ActiveControl = label1; // remove focus
            }
        }
        // osc sine freq
        private void TextBoxOscsineFreqValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                short parsedValue;
                if (!short.TryParse(TextBoxOscsineFreqValue.Text, out parsedValue)) { TextBoxOscsineFreqValue.Text = "0"; parsedValue = 0; }
                if (parsedValue > hScrollBarOscsineFreqValue.Maximum) { TextBoxOscsineFreqValue.Text = hScrollBarOscsineFreqValue.Maximum.ToString(); parsedValue = (short)hScrollBarOscsineFreqValue.Maximum; }
                hScrollBarOscsineFreqValue.Value = parsedValue;
                arrayfrequencyval[ComboBoxSampleAuswahl.SelectedIndex, edit] = (short)hScrollBarOscsineFreqValue.Value;
                e.SuppressKeyPress = true;
                this.ActiveControl = label1; // remove focus
            }
        }
        // osc sine gain
        private void TextBoxOscsineGainValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                short parsedValue;
                if (!short.TryParse(TextBoxOscsineGainValue.Text, out parsedValue)) { TextBoxOscsineGainValue.Text = "0"; parsedValue = 0; }
                if (parsedValue > hScrollBarOscsineGainValue.Maximum) { TextBoxOscsineGainValue.Text = hScrollBarOscsineGainValue.Maximum.ToString(); parsedValue = (short)hScrollBarOscsineGainValue.Maximum; }
                hScrollBarOscsineGainValue.Value = parsedValue;
                arraygainval[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarOscsineGainValue.Value;
                e.SuppressKeyPress = true;
                this.ActiveControl = label1; // remove focus
            }
        }
        // osc pulse freq
        private void TextBoxOscpulseFreqValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                short parsedValue;
                if (!short.TryParse(TextBoxOscpulseFreqValue.Text, out parsedValue)) { TextBoxOscpulseFreqValue.Text = "0"; parsedValue = 0; }
                if (parsedValue > hScrollBarOscpulseFreqValue.Maximum) { TextBoxOscpulseFreqValue.Text = hScrollBarOscpulseFreqValue.Maximum.ToString(); parsedValue = (short)hScrollBarOscpulseFreqValue.Maximum; }
                hScrollBarOscpulseFreqValue.Value = parsedValue;
                arrayfrequencyval[ComboBoxSampleAuswahl.SelectedIndex, edit] = (short)hScrollBarOscpulseFreqValue.Value;
                e.SuppressKeyPress = true;
                this.ActiveControl = label1; // remove focus
            }
        }
        // osc pulse width
        private void TextBoxOscpulseWidthValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                short parsedValue;
                if (!short.TryParse(TextBoxOscpulseWidthValue.Text, out parsedValue)) { TextBoxOscpulseWidthValue.Text = "0"; parsedValue = 0; }
                if (parsedValue > hScrollBarOscpulseWidthValue.Maximum) { TextBoxOscpulseWidthValue.Text = hScrollBarOscpulseWidthValue.Maximum.ToString(); parsedValue = (short)hScrollBarOscpulseWidthValue.Maximum; }
                hScrollBarOscpulseWidthValue.Value = parsedValue;
                arraywidthval[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarOscpulseWidthValue.Value;
                e.SuppressKeyPress = true;
                this.ActiveControl = label1; // remove focus
            }
        }
        // osc pulse gain
        private void TextBoxOscpulseGainValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                short parsedValue;
                if (!short.TryParse(TextBoxOscpulseGainValue.Text, out parsedValue)) { TextBoxOscpulseGainValue.Text = "0"; parsedValue = 0; }
                if (parsedValue > hScrollBarOscpulseGainValue.Maximum) { TextBoxOscpulseGainValue.Text = hScrollBarOscpulseGainValue.Maximum.ToString(); parsedValue = (short)hScrollBarOscpulseGainValue.Maximum; }
                hScrollBarOscpulseGainValue.Value = parsedValue;
                arraygainval[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarOscpulseGainValue.Value;
                e.SuppressKeyPress = true;
                this.ActiveControl = label1; // remove focus
            }
        }
        // osc noise gain
        private void TextBoxOscnoiseGainValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                short parsedValue;
                if (!short.TryParse(TextBoxOscnoiseGainValue.Text, out parsedValue)) { TextBoxOscnoiseGainValue.Text = "0"; parsedValue = 0; }
                if (parsedValue > hScrollBarOscnoiseGainValue.Maximum) { TextBoxOscnoiseGainValue.Text = hScrollBarOscnoiseGainValue.Maximum.ToString(); parsedValue = (short)hScrollBarOscnoiseGainValue.Maximum; }
                hScrollBarOscnoiseGainValue.Value = parsedValue;
                arraygainval[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarOscnoiseGainValue.Value;
                e.SuppressKeyPress = true;
                this.ActiveControl = label1; // remove focus
            }
        }
        // enva attack
        private void TextBoxEnvaAttackValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                short parsedValue;
                if (!short.TryParse(TextBoxEnvaAttackValue.Text, out parsedValue)) { TextBoxEnvaAttackValue.Text = "0"; parsedValue = 0; }
                if (parsedValue > hScrollBarEnvaAttackValue.Maximum) { TextBoxEnvaAttackValue.Text = hScrollBarEnvaAttackValue.Maximum.ToString(); parsedValue = (short)hScrollBarEnvaAttackValue.Maximum; }
                hScrollBarEnvaAttackValue.Value = parsedValue;
                arrayval1value[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarEnvaAttackValue.Value;
                e.SuppressKeyPress = true;
                this.ActiveControl = label1; // remove focus
            }
        }
        // enva gain
        private void TextBoxEnvaGainValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                short parsedValue;
                if (!short.TryParse(TextBoxEnvaGainValue.Text, out parsedValue)) { TextBoxEnvaGainValue.Text = "0"; parsedValue = 0; }
                if (parsedValue > hScrollBarEnvaGainValue.Maximum) { TextBoxEnvaGainValue.Text = hScrollBarEnvaGainValue.Maximum.ToString(); parsedValue = (short)hScrollBarEnvaGainValue.Maximum; }
                hScrollBarEnvaGainValue.Value = parsedValue;
                arraygainval[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarEnvaGainValue.Value;
                e.SuppressKeyPress = true;
                this.ActiveControl = label1; // remove focus
            }
        }
        // envd decay
        private void TextBoxEnvdDecayValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                short parsedValue;
                if (!short.TryParse(TextBoxEnvdDecayValue.Text, out parsedValue)) { TextBoxEnvdDecayValue.Text = "0"; parsedValue = 0; }
                if (parsedValue > hScrollBarEnvdDecayValue.Maximum) { TextBoxEnvdDecayValue.Text = hScrollBarEnvdDecayValue.Maximum.ToString(); parsedValue = (short)hScrollBarEnvdDecayValue.Maximum; }
                hScrollBarEnvdDecayValue.Value = parsedValue;
                arrayval1value[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarEnvdDecayValue.Value;
                e.SuppressKeyPress = true;
                this.ActiveControl = label1; // remove focus
            }
        }
        // envd gain
        private void TextBoxEnvdGainValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                short parsedValue;
                if (!short.TryParse(TextBoxEnvdGainValue.Text, out parsedValue)) { TextBoxEnvdGainValue.Text = "0"; parsedValue = 0; }
                if (parsedValue > hScrollBarEnvdGainValue.Maximum) { TextBoxEnvdGainValue.Text = hScrollBarEnvdGainValue.Maximum.ToString(); parsedValue = (short)hScrollBarEnvdGainValue.Maximum; }
                hScrollBarEnvdGainValue.Value = parsedValue;
                arraygainval[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarEnvdGainValue.Value;
                e.SuppressKeyPress = true;
                this.ActiveControl = label1; // remove focus
            }
        }
        // envd sustain
        private void TextBoxEnvdSustainValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                short parsedValue;
                if (!short.TryParse(TextBoxEnvdSustainValue.Text, out parsedValue)) { TextBoxEnvdSustainValue.Text = "0"; parsedValue = 0; }
                if (parsedValue > hScrollBarEnvdSustainValue.Maximum) { TextBoxEnvdSustainValue.Text = hScrollBarEnvdSustainValue.Maximum.ToString(); parsedValue = (short)hScrollBarEnvdSustainValue.Maximum; }
                hScrollBarEnvdSustainValue.Value = parsedValue;
                arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarEnvdSustainValue.Value;
                e.SuppressKeyPress = true;
                this.ActiveControl = label1; // remove focus
            }
        }
        // delay value
        private void TextBoxDelayDelayValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                short parsedValue;
                if (!short.TryParse(TextBoxDelayDelayValue.Text, out parsedValue)) { TextBoxDelayDelayValue.Text = "0"; parsedValue = 0; }
                if (parsedValue > hScrollBarDelayDelayValue.Maximum) { TextBoxDelayDelayValue.Text = hScrollBarDelayDelayValue.Maximum.ToString(); parsedValue = (short)hScrollBarDelayDelayValue.Maximum; }
                hScrollBarDelayDelayValue.Value = parsedValue;
                arrayfrequencyval[ComboBoxSampleAuswahl.SelectedIndex, edit] = (short)hScrollBarDelayDelayValue.Value;
                e.SuppressKeyPress = true;
                this.ActiveControl = label1; // remove focus
            }
        }
        // delay gain
        private void TextBoxDelayGainValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                short parsedValue;
                if (!short.TryParse(TextBoxDelayGainValue.Text, out parsedValue)) { TextBoxDelayGainValue.Text = "0"; parsedValue = 0; }
                if (parsedValue > hScrollBarDelayGainValue.Maximum) { TextBoxDelayGainValue.Text = hScrollBarDelayGainValue.Maximum.ToString(); parsedValue = (short)hScrollBarDelayGainValue.Maximum; }
                hScrollBarDelayGainValue.Value = parsedValue;
                arraygainval[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarDelayGainValue.Value;
                e.SuppressKeyPress = true;
                this.ActiveControl = label1; // remove focus
            }
        }
        // comb value
        private void TextBoxCombDelayValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                short parsedValue;
                if (!short.TryParse(TextBoxCombDelayValue.Text, out parsedValue)) { TextBoxCombDelayValue.Text = "0"; parsedValue = 0; }
                if (parsedValue > hScrollBarCombDelayValue.Maximum) { TextBoxCombDelayValue.Text = hScrollBarCombDelayValue.Maximum.ToString(); parsedValue = (short)hScrollBarCombDelayValue.Maximum; }
                hScrollBarCombDelayValue.Value = parsedValue;
                arrayfrequencyval[ComboBoxSampleAuswahl.SelectedIndex, edit] = (short)hScrollBarCombDelayValue.Value;
                e.SuppressKeyPress = true;
                this.ActiveControl = label1; // remove focus
            }
        }
        // comb feedback
        private void TextBoxCombFeedbackValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                short parsedValue;
                if (!short.TryParse(TextBoxCombFeedbackValue.Text, out parsedValue)) { TextBoxCombFeedbackValue.Text = "0"; parsedValue = 0; }
                if (parsedValue > hScrollBarCombFeedbackValue.Maximum) { TextBoxCombFeedbackValue.Text = hScrollBarCombFeedbackValue.Maximum.ToString(); parsedValue = (short)hScrollBarCombFeedbackValue.Maximum; }
                hScrollBarCombFeedbackValue.Value = parsedValue;
                arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarCombFeedbackValue.Value;
                e.SuppressKeyPress = true;
                this.ActiveControl = label1; // remove focus
            }
        }
        // comb gain
        private void TextBoxCombGainValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                short parsedValue;
                if (!short.TryParse(TextBoxCombGainValue.Text, out parsedValue)) { TextBoxCombGainValue.Text = "0"; parsedValue = 0; }
                if (parsedValue > hScrollBarCombGainValue.Maximum) { TextBoxCombGainValue.Text = hScrollBarCombGainValue.Maximum.ToString(); parsedValue = (short)hScrollBarCombGainValue.Maximum; }
                hScrollBarCombGainValue.Value = parsedValue;
                arraygainval[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarCombGainValue.Value;
                e.SuppressKeyPress = true;
                this.ActiveControl = label1; // remove focus
            }
        }
        // reverb feedback
        private void TextBoxReverbFeedbackValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                short parsedValue;
                if (!short.TryParse(TextBoxReverbFeedbackValue.Text, out parsedValue)) { TextBoxReverbFeedbackValue.Text = "0"; parsedValue = 0; }
                if (parsedValue > hScrollBarReverbFeedbackValue.Maximum) { TextBoxReverbFeedbackValue.Text = hScrollBarReverbFeedbackValue.Maximum.ToString(); parsedValue = (short)hScrollBarReverbFeedbackValue.Maximum; }
                hScrollBarReverbFeedbackValue.Value = parsedValue;
                arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarReverbFeedbackValue.Value;
                e.SuppressKeyPress = true;
                this.ActiveControl = label1; // remove focus
            }
        }
        // reverb gain
        private void TextBoxReverbGainValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                short parsedValue;
                if (!short.TryParse(TextBoxReverbGainValue.Text, out parsedValue)) { TextBoxReverbGainValue.Text = "0"; parsedValue = 0; }
                if (parsedValue > hScrollBarReverbGainValue.Maximum) { TextBoxReverbGainValue.Text = hScrollBarReverbGainValue.Maximum.ToString(); parsedValue = (short)hScrollBarReverbGainValue.Maximum; }
                hScrollBarReverbGainValue.Value = parsedValue;
                arraygainval[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarReverbGainValue.Value;
                e.SuppressKeyPress = true;
                this.ActiveControl = label1; // remove focus
            }
        }
        // sv filter cutoff
        private void TextBoxFilterCutoffValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                short parsedValue;
                if (!short.TryParse(TextBoxFilterCutoffValue.Text, out parsedValue)) { TextBoxFilterCutoffValue.Text = "0"; parsedValue = 0; }
                if (parsedValue > hScrollBarFilterCutoffValue.Maximum) { TextBoxFilterCutoffValue.Text = hScrollBarFilterCutoffValue.Maximum.ToString(); parsedValue = (short)hScrollBarFilterCutoffValue.Maximum; }
                hScrollBarFilterCutoffValue.Value = parsedValue;
                arrayfrequencyval[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarFilterCutoffValue.Value;
                e.SuppressKeyPress = true;
                this.ActiveControl = label1; // remove focus
            }
        }
        // sv filter resonance
        private void TextBoxFilterResonanceValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                short parsedValue;
                if (!short.TryParse(TextBoxFilterResonanceValue.Text, out parsedValue)) { TextBoxFilterResonanceValue.Text = "0"; parsedValue = 0; }
                if (parsedValue > hScrollBarFilterResonanceValue.Maximum) { TextBoxFilterResonanceValue.Text = hScrollBarFilterResonanceValue.Maximum.ToString(); parsedValue = (short)hScrollBarFilterResonanceValue.Maximum; }
                hScrollBarFilterResonanceValue.Value = parsedValue;
                arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarFilterResonanceValue.Value;
                e.SuppressKeyPress = true;
                this.ActiveControl = label1; // remove focus
            }
        }
        // add value 
        private void TextBoxAddVal2Value_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                short parsedValue;
                if (!short.TryParse(TextBoxAddVal2Value.Text, out parsedValue)) { TextBoxAddVal2Value.Text = "0"; parsedValue = 0; }
                if (parsedValue > hScrollBarAddVal2Value.Maximum) { TextBoxAddVal2Value.Text = hScrollBarAddVal2Value.Maximum.ToString(); parsedValue = (short)hScrollBarAddVal2Value.Maximum; }
                hScrollBarAddVal2Value.Value = parsedValue;
                arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, edit] = (short)hScrollBarAddVal2Value.Value;
                e.SuppressKeyPress = true;
                this.ActiveControl = label1; // remove focus
            }
        }
        // mul value
        private void TextBoxMulVal2Value_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                short parsedValue;
                if (!short.TryParse(TextBoxMulVal2Value.Text, out parsedValue)) { TextBoxMulVal2Value.Text = "0"; parsedValue = 0; }
                if (parsedValue > hScrollBarMulVal2Value.Maximum) { TextBoxMulVal2Value.Text = hScrollBarMulVal2Value.Maximum.ToString(); parsedValue = (short)hScrollBarMulVal2Value.Maximum; }
                hScrollBarMulVal2Value.Value = parsedValue;
                arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, edit] = (short)hScrollBarMulVal2Value.Value;
                float tempfloat = (float)hScrollBarMulVal2Value.Value;
                tempfloat = (1.0f / 32767.0f) * tempfloat;
                TextBoxMulVal2ValueFloat.Text = tempfloat.ToString("F4");
                e.SuppressKeyPress = true;
                this.ActiveControl = label1; // remove focus
            }
        }
        // clone transpose
        private void TextBoxCloneTransposeValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                short parsedValue;
                if (!short.TryParse(TextBoxCloneTransposeValue.Text, out parsedValue)) { TextBoxCloneTransposeValue.Text = "0"; parsedValue = 0; }
                if (parsedValue > hScrollBarCloneTransposeValue.Maximum) { TextBoxCloneTransposeValue.Text = hScrollBarCloneTransposeValue.Maximum.ToString(); parsedValue = (short)hScrollBarCloneTransposeValue.Maximum; }
                if (parsedValue < hScrollBarCloneTransposeValue.Minimum) { TextBoxCloneTransposeValue.Text = hScrollBarCloneTransposeValue.Minimum.ToString(); parsedValue = (short)hScrollBarCloneTransposeValue.Minimum; }
                hScrollBarCloneTransposeValue.Value = parsedValue;
                arrayfrequencyval[ComboBoxSampleAuswahl.SelectedIndex, edit] = (short)hScrollBarCloneTransposeValue.Value;
                e.SuppressKeyPress = true;
                this.ActiveControl = label1; // remove focus
            }
        }
        // chord gen shift
        private void TextBoxChordShiftValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                short parsedValue;
                if (!short.TryParse(TextBoxChordShiftValue.Text, out parsedValue)) { TextBoxChordShiftValue.Text = "0"; parsedValue = 0; }
                if (parsedValue > hScrollBarChordShiftValue.Maximum) { TextBoxChordShiftValue.Text = hScrollBarChordShiftValue.Maximum.ToString(); parsedValue = (short)hScrollBarChordShiftValue.Maximum; }
                hScrollBarChordShiftValue.Value = parsedValue;
                arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarChordShiftValue.Value;
                e.SuppressKeyPress = true;
                this.ActiveControl = label1; // remove focus
            }
        }
        // s&h step
        private void TextBoxShStepValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                short parsedValue;
                if (!short.TryParse(TextBoxShStepValue.Text, out parsedValue)) { TextBoxShStepValue.Text = "0"; parsedValue = 0; }
                if (parsedValue > hScrollBarShStepValue.Maximum) { TextBoxShStepValue.Text = hScrollBarShStepValue.Maximum.ToString(); parsedValue = (short)hScrollBarShStepValue.Maximum; }
                hScrollBarShStepValue.Value = parsedValue;
                arraygainval[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarShStepValue.Value;
                e.SuppressKeyPress = true;
                this.ActiveControl = label1; // remove focus
            }
        }
        // distortion gain
        private void TextBoxDistortionGainValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                short parsedValue;
                if (!short.TryParse(TextBoxDistortionGainValue.Text, out parsedValue)) { TextBoxDistortionGainValue.Text = "0"; parsedValue = 0; }
                if (parsedValue > hScrollBarDistortionGainValue.Maximum) { TextBoxDistortionGainValue.Text = hScrollBarDistortionGainValue.Maximum.ToString(); parsedValue = (short)hScrollBarDistortionGainValue.Maximum; }
                hScrollBarDistortionGainValue.Value = parsedValue;
                arraygainval[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarDistortionGainValue.Value;
                e.SuppressKeyPress = true;
                this.ActiveControl = label1; // remove focus
            }
        }
        // one pole filter cutoff
        private void TextBoxOnepoleCutoffValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                short parsedValue;
                if (!short.TryParse(TextBoxOnepoleCutoffValue.Text, out parsedValue)) { TextBoxOnepoleCutoffValue.Text = "0"; parsedValue = 0; }
                if (parsedValue > hScrollBarOnepoleCutoffValue.Maximum) { TextBoxOnepoleCutoffValue.Text = hScrollBarOnepoleCutoffValue.Maximum.ToString(); parsedValue = (short)hScrollBarOnepoleCutoffValue.Maximum; }
                hScrollBarOnepoleCutoffValue.Value = parsedValue;
                arrayfrequencyval[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarOnepoleCutoffValue.Value;
                e.SuppressKeyPress = true;
                this.ActiveControl = label1;
            }
        }

        // adsr envelope attack
        private void TextBoxEnvelopeAttackValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                short parsedValue;
                if (!short.TryParse(TextBoxEnvelopeAttackValue.Text, out parsedValue)) { TextBoxEnvelopeAttackValue.Text = "0"; parsedValue = 0; }
                if (parsedValue > hScrollBarEnvelopeAttackValue.Maximum) { TextBoxEnvelopeAttackValue.Text = hScrollBarEnvelopeAttackValue.Maximum.ToString(); parsedValue = (short)hScrollBarEnvelopeAttackValue.Maximum; }
                hScrollBarEnvelopeAttackValue.Value = parsedValue;
                arrayval2value[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarEnvelopeAttackValue.Value;
                e.SuppressKeyPress = true;
                this.ActiveControl = label1; // remove focus
            }
        }
        // adsr envelope decay
        private void TextBoxEnvelopeDecayValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                short parsedValue;
                if (!short.TryParse(TextBoxEnvelopeDecayValue.Text, out parsedValue)) { TextBoxEnvelopeDecayValue.Text = "0"; parsedValue = 0; }
                if (parsedValue > hScrollBarEnvelopeDecayValue.Maximum) { TextBoxEnvelopeDecayValue.Text = hScrollBarEnvelopeDecayValue.Maximum.ToString(); parsedValue = (short)hScrollBarEnvelopeDecayValue.Maximum; }
                hScrollBarEnvelopeDecayValue.Value = parsedValue;
                arrayval1value[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarEnvelopeDecayValue.Value;
                e.SuppressKeyPress = true;
                this.ActiveControl = label1; // remove focus
            }
        }
        // adsr envelope sustain
        private void TextBoxEnvelopeSustainValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                short parsedValue;
                if (!short.TryParse(TextBoxEnvelopeSustainValue.Text, out parsedValue)) { TextBoxEnvelopeSustainValue.Text = "0"; parsedValue = 0; }
                if (parsedValue > hScrollBarEnvelopeSustainValue.Maximum) { TextBoxEnvelopeSustainValue.Text = hScrollBarEnvelopeSustainValue.Maximum.ToString(); parsedValue = (short)hScrollBarEnvelopeSustainValue.Maximum; }
                hScrollBarEnvelopeSustainValue.Value = parsedValue;
                arraywidthval[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarEnvelopeSustainValue.Value;
                e.SuppressKeyPress = true;
                this.ActiveControl = label1; // remove focus
            }
        }
        // adsr envelope release
        private void TextBoxEnvelopeReleaseValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                short parsedValue;
                if (!short.TryParse(TextBoxEnvelopeReleaseValue.Text, out parsedValue)) { TextBoxEnvelopeReleaseValue.Text = "0"; parsedValue = 0; }
                if (parsedValue > hScrollBarEnvelopeReleaseValue.Maximum) { TextBoxEnvelopeReleaseValue.Text = hScrollBarEnvelopeReleaseValue.Maximum.ToString(); parsedValue = (short)hScrollBarEnvelopeReleaseValue.Maximum; }
                hScrollBarEnvelopeReleaseValue.Value = parsedValue;
                arrayfrequencyval[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarEnvelopeReleaseValue.Value;
                e.SuppressKeyPress = true;
                this.ActiveControl = label1; // remove focus
            }
        }
        // adsr envelope gain
        private void TextBoxEnvelopeGainValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                short parsedValue;
                if (!short.TryParse(TextBoxEnvelopeGainValue.Text, out parsedValue)) { TextBoxEnvelopeGainValue.Text = "0"; parsedValue = 0; }
                if (parsedValue > hScrollBarEnvelopeGainValue.Maximum) { TextBoxEnvelopeGainValue.Text = hScrollBarEnvelopeGainValue.Maximum.ToString(); parsedValue = (short)hScrollBarEnvelopeGainValue.Maximum; }
                hScrollBarEnvelopeGainValue.Value = parsedValue;
                arraygainval[ComboBoxSampleAuswahl.SelectedIndex, edit] = (byte)hScrollBarEnvelopeGainValue.Value;
                e.SuppressKeyPress = true;
                this.ActiveControl = label1; // remove focus
            }
        }

        // samplelength decimal
        private void textBoxSamplelengthDec_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int parsedValue;
                if (!int.TryParse(textBoxSamplelengthDec.Text, out parsedValue)) { textBoxSamplelengthDec.Text = "2"; parsedValue = 2; }
                if (parsedValue > 65534) { textBoxSamplelengthDec.Text = "65534"; parsedValue = 65534; }
                if (parsedValue % 2 == 1) { parsedValue += 1; textBoxSamplelengthDec.Text = parsedValue.ToString(); }
                if (parsedValue < 2) { textBoxSamplelengthDec.Text = "2"; parsedValue = 2; }
                hScrollBarSamplelength.Value = parsedValue;
                samplelength[ComboBoxSampleAuswahl.SelectedIndex] = parsedValue;
                textBox3.Text = parsedValue.ToString("X");
                e.SuppressKeyPress = true;
                this.ActiveControl = label1;
                updateloopbars();
            }
        }
        // samplelength hex
        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int parsedValue;
                if (!int.TryParse(textBox3.Text, System.Globalization.NumberStyles.HexNumber, null, out parsedValue)) { textBox3.Text = "2"; parsedValue = 2; }
                if (parsedValue > 65534) { textBox3.Text = "FFFE"; parsedValue = 65534; }
                if (parsedValue % 2 == 1) { parsedValue += 1; textBox3.Text = parsedValue.ToString("x"); }
                if (parsedValue < 2) { textBox3.Text = "2"; parsedValue = 2; }
                hScrollBarSamplelength.Value = parsedValue;
                samplelength[ComboBoxSampleAuswahl.SelectedIndex] = parsedValue;
                textBoxSamplelengthDec.Text = parsedValue.ToString();
                textBox3.Text = parsedValue.ToString("X"); // again, to display only uppercase letters
                e.SuppressKeyPress = true;
                this.ActiveControl = label1;
                updateloopbars();
            }
        }
        // sampleloop offset hex
        private void TextBoxLoopOffsetValue_KeyDown(object sender, KeyEventArgs e)
        {
            /*
            if (e.KeyCode == Keys.Enter)
            {
                int parsedValue;
                if (!int.TryParse(TextBoxLoopOffsetValue.Text, System.Globalization.NumberStyles.HexNumber, null, out parsedValue)) { TextBoxLoopOffsetValue.Text = "2"; parsedValue = 2; }
                if (parsedValue > 65534) { TextBoxLoopOffsetValue.Text = "FFFE"; parsedValue = 65534; }
                if (parsedValue % 2 == 1) { parsedValue += 1; TextBoxLoopOffsetValue.Text = parsedValue.ToString("x"); }
                if (parsedValue < 2) { TextBoxLoopOffsetValue.Text = "2"; parsedValue = 2; }
                hScrollBarLoopOffsetValue.Value = parsedValue;
                samplelength[ComboBoxSampleAuswahl.SelectedIndex] = parsedValue;
                TextBoxLoopOffsetValue.Text = parsedValue.ToString("X"); // again, to display only uppercase letters
                e.SuppressKeyPress = true;
                this.ActiveControl = label1;
            }
            */
        }



        // selects all text in a clicked textbox
        private void TextBoxOnepoleCutoffValue_Click(object sender, EventArgs e)
        {
            TextBoxOnepoleCutoffValue.SelectAll();
        }
        private void TextBoxFilterCutoffValue_Click(object sender, EventArgs e)
        {
            TextBoxFilterCutoffValue.SelectAll();
        }
        private void TextBoxReverbFeedbackValue_Click(object sender, EventArgs e)
        {
            TextBoxReverbFeedbackValue.SelectAll();
        }
        private void TextBoxReverbGainValue_Click(object sender, EventArgs e)
        {
            TextBoxReverbGainValue.SelectAll();
        }
        private void TextBoxCombDelayValue_Click(object sender, EventArgs e)
        {
            TextBoxCombDelayValue.SelectAll();
        }
        private void TextBoxCombFeedbackValue_Click(object sender, EventArgs e)
        {
            TextBoxCombFeedbackValue.SelectAll();
        }
        private void TextBoxCombGainValue_Click(object sender, EventArgs e)
        {
            TextBoxCombGainValue.SelectAll();
        }
        private void TextBoxEnvaAttackValue_Click(object sender, EventArgs e)
        {
            TextBoxEnvaAttackValue.SelectAll();
        }
        private void TextBoxEnvaGainValue_Click(object sender, EventArgs e)
        {
            TextBoxEnvaGainValue.SelectAll();
        }
        private void TextBoxDelayDelayValue_Click(object sender, EventArgs e)
        {
            TextBoxDelayDelayValue.SelectAll();
        }
        private void TextBoxDelayGainValue_Click(object sender, EventArgs e)
        {
            TextBoxDelayGainValue.SelectAll();
        }
        private void TextBoxMulVal2Value_Click(object sender, EventArgs e)
        {
            TextBoxMulVal2Value.SelectAll();
        }
        private void TextBoxEnvdDecayValue_Click(object sender, EventArgs e)
        {
            TextBoxEnvdDecayValue.SelectAll();
        }
        private void TextBoxEnvdSustainValue_Click(object sender, EventArgs e)
        {
            TextBoxEnvdSustainValue.SelectAll();
        }
        private void TextBoxEnvdGainValue_Click(object sender, EventArgs e)
        {
            TextBoxEnvdGainValue.SelectAll();
        }
        private void TextBoxOscnoiseGainValue_Click(object sender, EventArgs e)
        {
            TextBoxOscnoiseGainValue.SelectAll();
        }
        private void TextBoxOsctriFreqValue_Click(object sender, EventArgs e)
        {
            TextBoxOsctriFreqValue.SelectAll();
        }
        private void TextBoxOsctriGainValue_Click(object sender, EventArgs e)
        {
            TextBoxOsctriGainValue.SelectAll();
        }
        private void TextBoxOscsineFreqValue_Click(object sender, EventArgs e)
        {
            TextBoxOscsineFreqValue.SelectAll();
        }
        private void TextBoxOscsineGainValue_Click(object sender, EventArgs e)
        {
            TextBoxOscsineGainValue.SelectAll();
        }
        private void TextBoxOscpulseFreqValue_Click(object sender, EventArgs e)
        {
            TextBoxOscpulseFreqValue.SelectAll();
        }
        private void TextBoxOscpulseWidthValue_Click(object sender, EventArgs e)
        {
            TextBoxOscpulseWidthValue.SelectAll();
        }
        private void TextBoxOscpulseGainValue_Click(object sender, EventArgs e)
        {
            TextBoxOscpulseGainValue.SelectAll();
        }
        private void TextBoxAddVal2Value_Click(object sender, EventArgs e)
        {
            TextBoxAddVal2Value.SelectAll();
        }
        private void TextBoxVolGainValue_Click(object sender, EventArgs e)
        {
            TextBoxVolGainValue.SelectAll();
        }
        private void TextBoxDistortionGainValue_Click(object sender, EventArgs e)
        {
            TextBoxDistortionGainValue.SelectAll();
        }
        private void TextBoxShStepValue_Click(object sender, EventArgs e)
        {
            TextBoxShStepValue.SelectAll();
        }
        private void TextBoxOscsawFreqValue_Click(object sender, EventArgs e)
        {
            TextBoxOscsawFreqValue.SelectAll();
        }
        private void TextBoxOscsawGainValue_Click(object sender, EventArgs e)
        {
            TextBoxOscsawGainValue.SelectAll();
        }
        private void TextBoxCloneTransposeValue_Click(object sender, EventArgs e)
        {
            TextBoxCloneTransposeValue.SelectAll();
        }
        private void TextBoxChordShiftValue_Click(object sender, EventArgs e)
        {
            TextBoxChordShiftValue.SelectAll();
        }
        private void TextBoxFilterResonanceValue_Click(object sender, EventArgs e)
        {
            TextBoxFilterResonanceValue.SelectAll();
        }
        private void textBox3_Click(object sender, EventArgs e)
        {
            textBox3.SelectAll();
        }
        private void textBoxSamplelengthDec_Click(object sender, EventArgs e)
        {
            textBoxSamplelengthDec.SelectAll();
        }
        private void TextBoxEnvelopeAttackValue_Click(object sender, EventArgs e)
        {
            TextBoxEnvelopeAttackValue.SelectAll();
        }
        private void TextBoxEnvelopeDecayValue_Click(object sender, EventArgs e)
        {
            TextBoxEnvelopeDecayValue.SelectAll();
        }
        private void TextBoxEnvelopeSustainValue_Click(object sender, EventArgs e)
        {
            TextBoxEnvelopeSustainValue.SelectAll();
        }
        private void TextBoxEnvelopeReleaseValue_Click(object sender, EventArgs e)
        {
            TextBoxEnvelopeReleaseValue.SelectAll();
        }
        private void TextBoxEnvelopeGainValue_Click(object sender, EventArgs e)
        {
            TextBoxEnvelopeGainValue.SelectAll();
        }




        private void radioButtonColum1_CheckedChanged(object sender, EventArgs e)
        {
            updateVisu(ComboBoxSampleAuswahl.SelectedIndex);
            ButtonEdit1.PerformClick();
        }

        private void radioButtonColum2_CheckedChanged(object sender, EventArgs e)
        {
            updateVisu(ComboBoxSampleAuswahl.SelectedIndex);
            ButtonEdit2.PerformClick();
        }

        private void radioButtonColum3_CheckedChanged(object sender, EventArgs e)
        {
            updateVisu(ComboBoxSampleAuswahl.SelectedIndex);
            ButtonEdit3.PerformClick();
        }

        private void radioButtonColum4_CheckedChanged(object sender, EventArgs e)
        {
            updateVisu(ComboBoxSampleAuswahl.SelectedIndex);
            ButtonEdit4.PerformClick();
        }

        private void radioButtonColum5_CheckedChanged(object sender, EventArgs e)
        {
            updateVisu(ComboBoxSampleAuswahl.SelectedIndex);
            ButtonEdit5.PerformClick();
        }

        private void radioButtonColum6_CheckedChanged(object sender, EventArgs e)
        {
            updateVisu(ComboBoxSampleAuswahl.SelectedIndex);
            ButtonEdit6.PerformClick();
        }

        private void radioButtonColum7_CheckedChanged(object sender, EventArgs e)
        {
            updateVisu(ComboBoxSampleAuswahl.SelectedIndex);
            ButtonEdit7.PerformClick();
        }

        private void radioButtonColum8_CheckedChanged(object sender, EventArgs e)
        {
            updateVisu(ComboBoxSampleAuswahl.SelectedIndex);
            ButtonEdit8.PerformClick();
        }

        private void radioButtonColum9_CheckedChanged(object sender, EventArgs e)
        {
            updateVisu(ComboBoxSampleAuswahl.SelectedIndex);
            ButtonEdit9.PerformClick();
        }

        private void radioButtonColum10_CheckedChanged(object sender, EventArgs e)
        {
            updateVisu(ComboBoxSampleAuswahl.SelectedIndex);
            ButtonEdit10.PerformClick();
        }

        private void radioButtonColum11_CheckedChanged(object sender, EventArgs e)
        {
            updateVisu(ComboBoxSampleAuswahl.SelectedIndex);
            ButtonEdit11.PerformClick();
        }

        private void radioButtonColum12_CheckedChanged(object sender, EventArgs e)
        {
            updateVisu(ComboBoxSampleAuswahl.SelectedIndex);
            ButtonEdit12.PerformClick();
        }

        private void radioButtonColum13_CheckedChanged(object sender, EventArgs e)
        {
            updateVisu(ComboBoxSampleAuswahl.SelectedIndex);
            ButtonEdit13.PerformClick();
        }

        private void radioButtonColum14_CheckedChanged(object sender, EventArgs e)
        {
            updateVisu(ComboBoxSampleAuswahl.SelectedIndex);
            ButtonEdit14.PerformClick();
        }

        private void radioButtonColum15_CheckedChanged(object sender, EventArgs e)
        {
            updateVisu(ComboBoxSampleAuswahl.SelectedIndex);
            ButtonEdit15.PerformClick();
        }

        private void radioButtonColum16_CheckedChanged(object sender, EventArgs e)
        {
            updateVisu(ComboBoxSampleAuswahl.SelectedIndex);
            ButtonEdit16.PerformClick();
        }

        private void radioButtonOutput_CheckedChanged(object sender, EventArgs e)
        {
            updateVisu(ComboBoxSampleAuswahl.SelectedIndex);
        }
      
        // Atari design
        private void ToolStripMenuItemAtari_Click(object sender, EventArgs e)
        {
            foreach (Control x in this.Controls)
            {
                if (x is MyGroupBox)
                {
                    ((MyGroupBox)x).ForeColor = Color.Black;
                    ((MyGroupBox)x).BorderColor = Color.Black;
                    foreach (Control y in ((MyGroupBox)x).Controls)
                    {
                        if (y is Label)
                        {
                            ((Label)y).ForeColor = Color.Black;
                        }
                    //    if (y is Button)
                    //    {
                    //        ((Button)y).FlatStyle = FlatStyle.Flat;
                    //    }
                    }
                }
                if (x is Label)
                {
                    ((Label)x).ForeColor = Color.Black;
                }

                //if (x is Button)
                //{
                //    ((Button)x).FlatStyle = FlatStyle.Flat;
                //}
            }
            checkBoxSampleview.ForeColor = Color.Black;
            toolStripMenuItem1.ForeColor = Color.Black;
            toolStripMenuItem2.ForeColor = Color.Black;
            toolStripMenuItem3.ForeColor = Color.Black;
            this.Cursor = CursorAtari;
            colSamplebox = col2Atari;
            colCirclesBg = col1Atari;
            colCirclesErr = col2Atari;
            ToolStripMenuItemAmiga.Checked = false;
            ToolStripMenuItemAtari.Checked = true;
            this.BackColor = col1Atari;
            pictureBox2.BackColor = col1Atari;
            pictureBox2.Image = AmigaKlangGUI.Properties.Resources.st_trash2;
            menuStrip1.BackgroundImage = AmigaKlangGUI.Properties.Resources.stbar800;
            colLines = Color.Black;

            buttonGenerateAll.PerformClick();
        }

        // Amiga design
        private void ToolStripMenuItemAmiga_Click(object sender, EventArgs e)
        {
            foreach (Control x in this.Controls)
            {
                if (x is MyGroupBox)
                {
                    ((MyGroupBox)x).ForeColor = Color.White;
                    ((MyGroupBox)x).BorderColor = Color.White;
                    foreach (Control y in ((MyGroupBox)x).Controls)
                    {
                        if (y is Label)
                        {
                            ((Label)y).ForeColor = Color.White;
                        }
                        //    if (y is Button)
                        //    {
                        //        ((Button)y).FlatStyle = FlatStyle.Flat;
                        //    }
                    }
                }
                if (x is Label)
                {
                    ((Label)x).ForeColor = Color.White;
                }

                //if (x is Button)
                //{
                //    ((Button)x).FlatStyle = FlatStyle.Flat;
                //}
            }
            checkBoxSampleview.ForeColor = Color.White;
            toolStripMenuItem1.ForeColor = Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(85)))), ((int)(((byte)(173)))));      //blue
            toolStripMenuItem2.ForeColor = Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(85)))), ((int)(((byte)(173)))));      //blue
            toolStripMenuItem3.ForeColor = Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(85)))), ((int)(((byte)(173)))));      //blue
            this.Cursor = CursorAmiga;
            colSamplebox = col2Amiga;
            colCirclesBg = col1Amiga;
            colCirclesErr = col2Amiga;
            ToolStripMenuItemAmiga.Checked = true;
            ToolStripMenuItemAtari.Checked = false;
            this.BackColor = col1Amiga;
            pictureBox2.BackColor = col1Amiga;
            pictureBox2.Image = AmigaKlangGUI.Properties.Resources.amiga_trash;
            menuStrip1.BackgroundImage = AmigaKlangGUI.Properties.Resources.Unbenannt;
            colLines = Color.White;

            buttonGenerateAll.PerformClick();
        }


    }
}




