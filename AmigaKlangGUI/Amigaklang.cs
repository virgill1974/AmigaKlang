using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AmigaKlangGUI
{


    class Amigaklang
    {
        // **************************************** nodes ******************************************
        // all audio signals are short integers (-32768 .. +32767)
        // all contol signals are bytes (-128 .. 0 .. +127)
        // *****************************************************************************************

        // bytebeat test ***************************************************************************
        public static short osc_bytebeat(int sample, byte gain)
        {
             long t = (long)sample;
            //short t = (short)sample;
            // int t = sample;
           
            //t= ((t<<1)^((t<<1)+(t>>7)&t>>12))|t>>(4-(1^7&(t>>19)))|t>>7;
         //   t= t * (t >> (int)((t>>11) & 15)) * (t>>9&1)<<2;
            t = (t & t >> (int)(t >> 11)) * (t >> 11 & 3) << 1;
            return vol((short)(t<<8), gain);

        }
        
        
        // vocoder test ****************************************************************************
        public static short vocoder(short val1, short val2,short band1cut,short band1reso, short band2cut, short band2reso, short band3cut, short band3reso, short band4cut, short band4reso, short band5cut, short band5reso)
        {
            //Modulator:
             
            short band1 = sv_flt_n(0, val1, band1cut, band1reso, 0); //lowpass
            band1 = mul(band1, band1);
            band1 = onepole_flt(0, band1, 1, 0); // lowpass env follower
            band1 = mul(band1, 256);

            short band2 = sv_flt_n(1, val1, band2cut, band2reso, 2); //bandpass
            band2 = mul(band2, band2);
            band2 = onepole_flt(1, band2, 1, 0); // lowpass env follower
            band2 = mul(band2, 256);

            short band3 = sv_flt_n(2, val1, band3cut, band3reso, 2); //bandpass
            band3 = mul(band3, band3);
            band3 = onepole_flt(2, band3, 1, 0); // lowpass env follower
            band3 = mul(band3, 256);

            short band4 = sv_flt_n(3, val1, band4cut, band4reso, 2); //bandpass
            band4 = mul(band4, band4);
            band4 = onepole_flt(3, band4, 1, 0); // lowpass env follower
            band4 = mul(band4, 256);

            short band5 = sv_flt_n(4, val1, band5cut, band5reso, 1); //highpass
            band5 = mul(band5, band5);
            band5 = onepole_flt(4, band5, 1, 0); // lowpass env follower
            band5 = mul(band5, 256);

            // Carrier:

            short out1 = sv_flt_n(5, val2, band1cut, band1reso, 0);
            out1 = vol(out1, (byte)band1);

            short out2 = sv_flt_n(6, val2, band2cut, band2reso, 2);
            out2 = vol(out2, (byte)band2);

            short out3 = sv_flt_n(7, val2, band3cut, band3reso, 2);
            out3 = vol(out3, (byte)band3);

            short out4 = sv_flt_n(8, val2, band4cut, band4reso, 2);
            out4 = vol(out4, (byte)band4);

            short out5 = sv_flt_n(9, val2, band5cut, band5reso, 1);
            out5 = vol(out5, (byte)band5);


            return (short)(out1+out2+out3+out4+out5);
                                          
        }
        

        // clamp to short **************************************************************************
        public static short clamp(int val)
        {
            val = val > 32767 ? 32767 : val;
            val = val < -32768 ? -32768 : val;
            return (short)val;
        }

        // distortion ******************************************************************************
        public static short distort(short val, byte gain)
        {
            int temp;
            short res;


            val = clamp((int)(val * gain >> 5));
            val >>= 1;                                    // division by 2
            temp = val * (32767 - Math.Abs(-val));        // make it sine
            res = (short)(temp >> 16);
            res <<= 3;
            return res;
        }

        // volume **********************************************************************************
        public static short vol(short val, byte gain)
        {
            return (short)((int)val * gain >> 7);
        }

        // saw oscillator **************************************************************************
        public static short[] counter_saw = new short[16];
        public static short osc_saw(byte instance, short freq, byte gain)
        {
            counter_saw[instance] += freq;              // sawtooth
            return vol(counter_saw[instance], gain);    // gain
        }

        // sample and hold *************************************************************************
        public static short[] counter_sh = new short[16];
        public static short[] buffer_sh = new short[16];
        public static short sh(byte instance, short val1, byte step)
        {
            short step2 = (short)((step*step)>>2); 
            if (counter_sh[instance] == 0 ) buffer_sh[instance] = val1;
            if (counter_sh[instance] == step2) counter_sh[instance] = -1;
            counter_sh[instance] += 1;
            return buffer_sh[instance];
        }
    

        // triangle oscillator *********************************************************************
        public static short[] counter_tri = new short[16];
        public static short osc_tri(byte instance, short freq, byte gain)
        {
            short buf = counter_tri[instance] += freq;  // sawtooth
            if (buf < 0) buf = (short)~buf;         // mirror lower half
            //if (buf < 0) buf = (short)(65535 - buf);         // mirror lower half
            buf -= 16384;                           // offset to bring it in the middle again
            buf <<= 1;                              // double it to have max gain
            return vol(buf, gain);
        }


        // sine oscillator *************************************************************************
        public static short[] counter_sine = new short[16];
        public static short osc_sine(byte instance, short freq, byte gain)
        {
            int temp;
            short res;
            short buf = counter_sine[instance] += freq;  // sawtooth
            buf -= 16384;                           // phase offset to make cos instead of sin
            temp = buf * (32767 - Math.Abs(-buf));        // make it sine
            res = (short)(temp >> 16);
            res <<= 3;
            return vol(res, gain);
        }


        // pulse oscillator ************************************************************************
        public static short[] counter_pulse = new short[16];
        public static short osc_pulse(byte instance, short freq, byte gain, byte dutycycle)
        {
            short buf = counter_pulse[instance] += freq;  // sawtooth
            if (buf < (dutycycle - 63) << 9) buf = -32768;  // check duty cycle
            else buf = 32767;                       //
            return vol(buf, gain);
        }

        // noise oscillator ************************************************************************
        public static uint g_x1 = 0x67452301;           // random seeds
        public static uint g_x2 = 0xEFCDAB89;
        public static uint g_x3 = 0;
        public static short osc_noise(int sample, byte gain) // int sample not needed
        {
            g_x1 ^= g_x2;
            g_x3 += g_x2;
            g_x2 += g_x1;
            short buf = (short)g_x3;
            return vol(buf, gain);
        }

        static short[] decayTable = new short[]  {32767,32767,32767,16384,10922, 8192, 6553, 4681,
    3640, 2978, 2520, 2048, 1724, 1489, 1310, 1129, 992, 885, 799, 712, 642, 585, 537,
    489, 448, 414, 385, 356, 330, 309, 289, 270, 254, 239, 225, 212, 201, 190, 181,
    171, 163, 155, 148, 141, 134, 129, 123, 118, 113, 108, 104, 100, 96, 93, 89, 86,
    83, 80, 77, 75, 72, 70, 68, 65, 63, 61, 60, 58, 56, 54, 53, 51, 50, 49, 47, 46,
    45, 44, 43, 41, 40, 39, 38, 38, 37, 36, 35, 34, 33, 33, 32, 31, 30, 30, 29, 29,
    28, 27, 27, 26, 26, 25, 25, 24, 24, 23, 23, 22, 22, 22, 21, 21, 20, 20, 19, 19,
    19, 18, 18, 18, 17, 17, 17, 17, 16, 16, 16, 16};

        // attack envelope *************************************************************************
        public static short enva(int sample, byte attack, byte sustain, byte gain)  // sustain not needed
        {
            short t = decayTable[attack];
            int buf = (sample * t) >> 8;
            if (buf > 32767) buf = 32767;
            return vol((short)buf, gain);
        }


        // decay envelope **************************************************************************
        public static short envd(int sample, byte decay, byte sustain, byte gain)
        {
            short sustain16 = (short)(sustain << 8);
            short t = decayTable[decay];
            int buf = 32767 - ((sample * t) >> 8);
            if (buf < sustain16) buf = sustain16;
            return vol((short)buf, gain);
        }

        // add (with clamping) *********************************************************************
        public static short add(short val1, short val2)
        {
            return clamp(val1 + val2);
        }

        // mul *************************************************************************************
        public static short mul(short val1, short val2)  // both values to 8 bit and mul
        {
            return (short)((val1 * val2) >> 15);
        }


        // single delay cyclic buffer ***************************************************************
        static short[] dci = new short[16];
        public static short[,] buffercyc = new short[16, 2048];               // create buffer with delay length.  Max 2048 (16k)
        public static short dly_cyc(byte instance, short val, short delay, byte gain)
        {
            if (delay > 2047) delay = 2047;
            buffercyc[instance, dci[instance]] = vol(val, gain);
            if (++dci[instance] >= delay) dci[instance] = 0;    // wrap circular buffer
            return buffercyc[instance, dci[instance]];
        }


        // feedback comb filter (for reverb) new with clamp ****************************************
        public static short[,] buffern = new short[24, 2048];               // create buffer with delay length.  Max 2048 (16k)
        static short[] cfi = new short[24];                          // index variable
        public static short cmb_flt_n(byte instance, short val, int delay, byte feedback, byte gain)
        {
            if (delay > 2047) delay = 2047;
            buffern[instance, cfi[instance]] = add(val, (vol(buffern[instance, cfi[instance]], feedback)));
            short output = buffern[instance, cfi[instance]];
            if (++cfi[instance] >= delay) cfi[instance] = 0;            // wrap circular buffer
            return vol(output, gain);
        }

        // reverb **********************************************************************************
        public static short reverb(short val, byte feedback, byte gain)  // used pime dly lengths
        {
           int buf = cmb_flt_n(16, val, 557, feedback, gain);
            buf += cmb_flt_n(17, val, 593, feedback, gain);
            buf += cmb_flt_n(18, val, 641, feedback, gain);
            buf += cmb_flt_n(19, val, 677, feedback, gain);
            buf += cmb_flt_n(20, val, 709, feedback, gain);
            buf += cmb_flt_n(21, val, 743, feedback, gain);
            buf += cmb_flt_n(22, val, 787, feedback, gain);
            buf += cmb_flt_n(23, val, 809, feedback, gain);
            return clamp(buf);
        }

        // ctrl (make control value out of bigger values) ******************************************
        public static byte ctrl(short val)     // return only positive values from 0..127
        {
            return (byte)((val >> 9) + 64);
        }



        // state variable filter new ***************************************************************
        //public static short[] filterBuffer = new short[8 * 4];
        public static short[] lpf = new short[16];
        public static short[] hpf = new short[16];
        public static short[] bpf = new short[16];
        public static short sv_flt_n(byte instance, short val, short cutoff, short resonance, byte mode)
        {
            lpf[instance] = clamp(lpf[instance] + ((bpf[instance] >> 7) * cutoff));
            hpf[instance] = clamp(val - lpf[instance] - ((bpf[instance] >> 7) * resonance));
            bpf[instance] = clamp(bpf[instance] + ((hpf[instance] >> 7) * cutoff));

            switch (mode)
            {
                case 0: { return lpf[instance]; break; }
                case 1: { return hpf[instance]; break; }
                case 2: { return bpf[instance]; break; }
                case 3: { return clamp(hpf[instance] + bpf[instance]); break; } // probably needs clamp
            }
            return 0;
        }

        // one pole ***************************************************************
        public static short[] pole = new short[16];
        public static short onepole_flt(byte instance, short val, byte cutoff, byte mode)
        {
            pole[instance] = clamp(pole[instance] - cutoff * (pole[instance] >> 7) + cutoff * (val >> 7));
            
            switch (mode)
            {
                case 0: { return pole[instance]; break; }  // lpf
                case 1: { return clamp(val - pole[instance]); break; } // hpf  (was (short) then changed to clamp (10.10.2023)
            }
            return 0;

        }


        // chord generator **************************************************************************

        public static short chordgen(int sample, byte inst, byte[,] BaseAdr, byte n1, byte n2, byte n3, byte shift)
        {
            int buf = (sbyte)BaseAdr[sample, inst] << 7;              // 1/1		Prime
            if (n1 == 1 || n2 == 1 || n3 == 1) buf += (sbyte)BaseAdr[((sample * 271) >> 8) + shift, inst] << 7;             // 16/15	kl Sekunde
            if (n1 == 2 || n2 == 2 || n3 == 2) buf += (sbyte)BaseAdr[((sample * 287) >> 8) + shift, inst] << 7;             // 9/8		gr Sekunde
            if (n1 == 3 || n2 == 3 || n3 == 3) buf += (sbyte)BaseAdr[((sample * 304) >> 8) + shift, inst] << 7;             // 6/5		kl Terz
            if (n1 == 4 || n2 == 4 || n3 == 4) buf += (sbyte)BaseAdr[((sample * 322) >> 8) + shift, inst] << 7;             // 5/4		gr Terz
            if (n1 == 5 || n2 == 5 || n3 == 5) buf += (sbyte)BaseAdr[((sample * 342) >> 8) + shift, inst] << 7;             // 4/3		Quarte
            if (n1 == 6 || n2 == 6 || n3 == 6) buf += (sbyte)BaseAdr[((sample * 362) >> 8) + shift, inst] << 7;             // 45/32	Tritonus
            if (n1 == 7 || n2 == 7 || n3 == 7) buf += (sbyte)BaseAdr[((sample * 383) >> 8) + shift, inst] << 7;             // 3/2		Quinte
            if (n1 == 8 || n2 == 8 || n3 == 8) buf += (sbyte)BaseAdr[((sample * 203) >> 7) + shift, inst] << 7;             // 8/5		kl Sexte
            if (n1 == 9 || n2 == 9 || n3 == 9) buf += (sbyte)BaseAdr[((sample * 215) >> 7) + shift, inst] << 7;             // 5/3		gr Sexte
            if (n1 == 10 || n2 == 10 || n3 == 10) buf += (sbyte)BaseAdr[((sample * 228) >> 7) + shift, inst] << 7;          // 16/9		kl Septime
            if (n1 == 11 || n2 == 11 || n3 == 11) buf += (sbyte)BaseAdr[((sample * 483) >> 8) + shift, inst] << 7;          // 15/8		gr Septime
            if (n1 == 12 || n2 == 12 || n3 == 12) buf += (sbyte)BaseAdr[((sample * 1) << 1) + shift, inst] << 7;            // 2/1		Oktave
            return clamp(buf);
        }

        /*
        // clone sample with transpose ****************************************************************

        public static short clone(int sample, byte inst, byte[,] BaseAdr, ushort transpose, ushort offset, byte mode, int length)
        {
            uint pointer = (uint)((sample * transpose) >> 15) ;
            int buf = 0;
            
            if ((pointer+offset) < length)
            {
                if (mode == 0)
                {
                    buf = (sbyte)BaseAdr[pointer+offset, inst] << 8;
                }
                else
                {
                    buf = (sbyte)BaseAdr[length-(pointer+offset), inst] << 8;
                }
            }
           // TextBoxTranslateInst.Text += "*(BYTE*)(BaseAdr[" +  arraygain[i, j]     .ToString() + "] +  smp + offset   )<<8 : 0";
           // TextBoxTranslateInst.Text += "*(BYTE*)(BaseAdr[" + (arraygain[i, j] + 1).ToString() + "] - (smp + offset") )<<8 : 0";

            return (short)buf;
        }
        */

        // adsr envelope *******************************************************************************

        // instances 
        public static short[] ADSR_Mode = new short[16];            // mode instances (clear to zero)
        public static int[] ADSR_Value = new int[16];               // value instance (clear to zero)
        public static int[] ADSR_SustainCounter= new int[16];	    // sustain counter (clear to zero)

        // operator function
        public static short adsr(byte instance, int attackAmount, int decayAmount, int sustainLevel, int sustainLength, int releaseAmount, int peak)
        {
            int val = ADSR_Value[instance];

            switch (ADSR_Mode[instance])
            {
                case 0:         // Attack
                    val += attackAmount;
                    if (val >= peak)
                    {
                        val = peak;
                        ADSR_Mode[instance] = 1;
                    }
                    break;

                case 1:         // Decay
                    val -= decayAmount;
                    if (val <= sustainLevel)
                    {
                        val = sustainLevel;
                        ADSR_Mode[instance] = 2;
                    }
                    break;

                case 2:         // Sustain
                    ADSR_SustainCounter[instance]++;
                    if (ADSR_SustainCounter[instance] > sustainLength)
                    {
                        ADSR_Mode[instance] = 3;
                    }
                    break;

                case 3:         // Release
                    val -= releaseAmount;
                    if (val < 0)
                    {
                        val = 0;
                    }
                    break;

                default:
                    break;
            }

            ADSR_Value[instance] = val;

            return (short)(val >> 8);
        }



        /*
                // perfect loop generator ******************************************************************
                void loopgen(WORD repeat_length, WORD repeat_offset, void* BaseAdr)
                {

                    short v1, v2, v3, v4 = 0;
                    int smp;
                    for (smp = 0; smp < repeat_length; smp++)
                    {
                        v4 = 32767 - (32767 / (repeat_length >> 8) * (smp >> 8));               // falling ramp
                        v3 = mul(v4, *(BYTE*)(BaseAdr + repeat_offset + smp) << 8);             // forward sample	
                        v2 = (32767 / (repeat_length >> 8) * (smp >> 8));                       // rising ramp	
                        v1 = mul(v2, *(BYTE*)(BaseAdr + repeat_offset + repeat_length - smp) << 8); // backward sample
                        v1 = add(v3, v1);
                        *(BYTE*)(BaseAdr + repeat_offset + smp) = v1 >> 8;
                    }

                }

         */




    }

}

