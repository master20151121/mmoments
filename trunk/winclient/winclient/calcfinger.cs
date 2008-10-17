#define verbose

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace winclient
{
    class calcfinger
    {
        // Magic numbers
        static int TAM_FRAME = 16384;
        static int FRAME_OVERLAP = 2;
        static int FRAME_RATE = 441100;
        static int BANDS = 24;
        static double THREASHOLD = 10000; // magic threashold.

        class cola
        {
            public int[] data = new int[TAM_FRAME];
            public int front, rear;
        }

        static string wav_infile_; // args[0]
        static char plot_; //args[1]
        //static string sig_outfile_; //args[2]
        static double dMax_; // args[3]

        static public string generate(string wavinurl)
        {
#if verbose
            Console.Write("starting calcfinger on {0}", wavinurl);
#endif
            wav_infile_ = wavinurl;
            plot_ = 's'; // e|s|S|h
            dMax_ = System.Math.PI / THREASHOLD;

            string finger = run();
            Console.WriteLine(finger.Length);

            return finger;
        }

        static string run()
        {
#if verbose
            Console.WriteLine("in file = {0}", wav_infile_);
            //Console.WriteLine("sig file = {0}", sig_outfile_);
#endif

            // load the wav file.            
            wavesimple wavin = new wavesimple(wav_infile_);

#if verbose
            Console.WriteLine("no channels {0}", wavin.get_no_channels().ToString());
            Console.WriteLine("frame rate = {0}", wavin.get_frame_rate().ToString());
            Console.WriteLine("bps = {0}", wavin.get_bits_pr_sample().ToString());
#endif

            TextWriter tw = new StringWriter();

            int frame_size;
            int sample_size_bits;
            uint sample_rate = wavin.get_frame_rate();// frame rate == samplerate?
            // im supposed to do a check here.
            leeWav(wavin, sample_rate, tw);

            return tw.ToString();
        }

        static void SWAP(float[] data, int i, int j)
        {
            float f = data[i];
            data[i] = data[j];
            data[j] = f;
        }

        static void leeWav(wavesimple wavf, uint samplerate, TextWriter sig_out)
        {
            //Console.WriteLine("starting leeWav");
            int i, j, jj, p;
            float[] signal = new float[TAM_FRAME];
            float[] reband = new float[2 * TAM_FRAME]; //rebannda

            cola buffer = new cola();
            int cont = 0;

            float[] window = new float[TAM_FRAME];

            double[] parteReal = new double[TAM_FRAME];
            double[] parteImag = new double[TAM_FRAME];
            double[] entropias = new double[BANDS];    // 24 bandas de 1 bark
            double[] entropiasAnt = new double[BANDS]; // 24 signos

            buffer.front = 0;
            buffer.rear = TAM_FRAME - 1;


            float wss = hanning(window, TAM_FRAME); // correct


            //try??
            for (i = 0; i < TAM_FRAME; i++)
            { // does this work prop
                byte[] byte_buffer = new byte[4];
                wavf.read(ref byte_buffer);

                short firstb = (short)(byte_buffer[0] & 0X00FF); // ushort vs short = no change.
                short secondb = (short)(byte_buffer[1] << 8 & 0XFF00);
                short thirdb = (short)(byte_buffer[2] & 0X00FF);
                short fourthb = (short)(byte_buffer[3] << 8 & 0XFF00);

                short sampleleft = (short)(firstb | secondb); //same
                short sampleright = (short)(thirdb | fourthb); //same
                agrega(buffer, (sampleleft / 2 + sampleright / 2));
            }

            //Console.WriteLine("i got here");

            toarray(buffer, signal); //ref?? //same

            for (i = 0; i < TAM_FRAME; i++)
            {
                signal[i] *= window[i]; //apply hanning windows
            } //same

            for (j = 0, jj = 1; j < TAM_FRAME; j++, jj += 2)
            {
                reband[jj - 1] = signal[j];
                reband[jj] = 0; //imaginary part
            } //same11

            //calculate fast fourier transform
            four1(reband, TAM_FRAME, -1); //used to be  rebanada-1 ? //different.

            for (j = 0, jj = 1; j < TAM_FRAME; j++, jj += 2)
            {
                parteReal[j] = reband[jj - 1];
                parteImag[j] = reband[jj];
            }


            entropiaXbandas(parteReal, parteImag, entropiasAnt, samplerate, TAM_FRAME);
            code(entropiasAnt, 3, BANDS, sig_out);

            cont = 0;
            //Console.WriteLine("starting the loop");
            while (true)
            {
                byte[] byte_buffer = new byte[4];
                int num_bytes = wavf.read(ref byte_buffer);
                if (num_bytes != 4)
                {
                    break;
                }
                // little endian => JRE always big endian
                short firstByte = (short)(byte_buffer[0] & 0x00FF);
                short secondByte = (short)((byte_buffer[1] << 8) & 0xFF00);
                short thirdByte = (short)(byte_buffer[2] & 0x00FF);
                short fourthByte = (short)((byte_buffer[3] << 8) & 0xFF00);

                short sampleLeft = (short)(firstByte | secondByte);
                short sampleRight = (short)(thirdByte | fourthByte);

                agrega(buffer, (sampleLeft / 2 + sampleRight / 2));

                cont++;

                if (cont == TAM_FRAME / FRAME_OVERLAP)
                {
                    cont = 0;
                    toarray(buffer, signal);

                    for (i = 0; i < TAM_FRAME; i++)
                    {
                        signal[i] *= window[i]; // apply Hanning window
                    }

                    for (j = 0, jj = 1; j < TAM_FRAME; j++, jj += 2)
                    {
                        reband[jj - 1] = signal[j];
                        reband[jj] = 0; // PARTE IMAGINARIA
                    }

                    // Calculate Fast Fourier Transform
                    four1(reband, TAM_FRAME, -1); // **** this used to be rebanada-1 ?!?

                    for (j = 0, jj = 1; j < TAM_FRAME; j++, jj += 2)
                    {
                        parteReal[j] = reband[jj - 1];
                        parteImag[j] = reband[jj];
                    }

                    entropiaXbandas(parteReal, parteImag, entropias, samplerate, TAM_FRAME);
                    code(entropias, 3, BANDS, sig_out);
                }
            }
        }

        static float hanning(float[] x, int n)
        {
            int n1 = n - 1;
            float wss = 0;
            for (int i = 0; i < n; i++)
            {
                x[i] = 0.5f - 0.5f * (float)(Math.Cos(2 * Math.PI * i / n1));
                wss += x[i] * x[i];
            }
            return wss;
        }

        static void agrega(cola c, int x)
        {
            c.data[c.rear] = x;
            c.rear++;
            c.rear %= TAM_FRAME;
        }

        static void toarray(cola c, float[] a) // just to be confused with builtin toarray.
        {
            int i = c.front;
            int j = 0;
            do
            {
                if (i == TAM_FRAME)
                {
                    i = 0;
                }
                else
                {
                    i++;
                }
                a[j++] = c.data[i];
            } while (i != c.rear);
        }

        #region // four1_orig
        // java copy. useless?
        /* four1
        RECIBE:
        data.- UN ARREGLO DE TAMA¥O 2*nn DE LA FUNCION A TRANSFORMAR,
        EN LAS LOCALIDADES PARES (0,2,...) ESTAN LAS PARTES REALES Y
        EN LAS LOCALIDADES IMPARES (1,3,...) ESTAN LAS PARTES IMAGINARIAS.
        nn.- EL NUMERO DE ELEMENTOS DATOS COMPLEJOS DEL ARREGLO, DEBE SER
        POTENCIA DE DOS (EJ 512, 1024, 4096) ­ESTO NO SE VERIFICA!.
        isign.- UN -1 SI SE DESEA LA TRANSFORMADA DE FOURIER
        UN 1 SI SE DESEA LA TRANSFORMADA INVERSA DE FOURIER.

        REGRESA:
        data.- UN ARREGLO DE TAMA¥O 2*nn CON LA FUNCION TRANSFORMADA,
        EN LAS LOCALIDADES PARES (0,2,...) ESTAN LAS PARTES REALES Y
        EN LAS LOCALIDADES IMPARES (1,3,...) ESTAN LAS PARTES IMAGINARIAS.
     */
        //protected void four1_orig(float[] data, int nn, int isign)
        //{
        //    int n, mmax, m, j, istep, i;
        //    double wtemp, wr, wpr, wpi, wi, teta;
        //    // DOBLE PRECISION PARA LA RECURRENCIA TRIGONOMETRICA
        //    float tempr, tempi;

        //    n = nn << 1;
        //    j = 1;
        //    for (i = 1; i < n; i += 2)
        //    {   // ESTA PARTE HACE EL BIT-REVERSE
        //        if (j > i)
        //        {
        //            SWAP(data, j, i);    // INTERCAMBIA LOS 2 NUMEROS COMPLEJOS
        //            SWAP(data, j + 1, i + 1);
        //        }
        //        m = n >> 1;
        //        while (m >= 2 && j > m)
        //        {
        //            j -= m;
        //            m >>= 1;
        //        }
        //        j += m;
        //    }

        //    mmax = 2;          // AQUI EMPIEZA LA SECCION DE DANIEL-LANCZOS
        //    while (n > mmax)
        //    { // EL CICLO EXTERNO SE EJECUTA LOG(nn) BASE 2 VECES 
        //        istep = 2 * mmax;
        //        teta = 6.28318530717959 / (isign * mmax); //INICIALIZA LA RECURRENCIA TRIG.
        //        wtemp = Math.sin(0.5 * teta);
        //        wpr = -2.0 * wtemp * wtemp;
        //        wpi = Math.sin(teta);
        //        wr = 1.0;
        //        wi = 0.0;
        //        for (m = 1; m < mmax; m += 2)
        //        {   // LOS 2 CICLOS INTERNOS ANIDADOS
        //            for (i = m; i <= n; i += istep)
        //            {
        //                j = i + mmax;       // ESTA ES LA FORMULA DE DANIEL-LANCZOS
        //                tempr = ((float)wr) * data[j] - ((float)wi) * data[j + 1];
        //                tempi = ((float)wr) * data[j + 1] + ((float)wi) * data[j];
        //                data[j] = data[i] - tempr;
        //                data[j + 1] = data[i + 1] - tempi;
        //                data[i] += tempr;
        //                data[i + 1] += tempi;
        //            }  // RECURRENCIA TRIGONOMETRICA
        //            wr = (wtemp = wr) * wpr - wi * wpi + wr;
        //            wi = wi * wpr + wtemp * wpi + wi;
        //        }
        //        mmax = istep;
        //    }
        //}
        #endregion

        static void four1(float[] data, int nn, int isign)
        {
            int n, mmax, m, j = 0, i;
            double wtemp, wr, wpr, wpi, wi, theta, wpin = 0;
            float tempr, tempi, datar, datai;
            float data1r, data1i;

            n = nn * 2;
            j = 0;
            for (i = 0; i < n; i += 2)
            {
                if (j > i)
                {
                    SWAP(data, j, i);
                    SWAP(data, j + 1, i + 1);
                }
                m = nn;
                while (m >= 2 && j >= m)
                {
                    j -= m;
                    m >>= 1;
                }
                j += m;
            }

            theta = Math.PI * 0.5;

            if (isign < 0)
                theta = -theta;

            for (mmax = 2; n > mmax; mmax *= 2)
            {
                wpi = wpin;
                wpin = Math.Sin(theta);
                wpr = 1 - wpin * wpin - wpin * wpin;    // cos(theta*2)
                theta *= .5;
                wr = 1;
                wi = 0;

                for (m = 0; m < mmax; m += 2)
                {
                    j = m + mmax;
                    tempr = (float)wr * (data1r = data[j]);
                    tempi = (float)wi * (data1i = data[j + 1]);
                    for (i = m; i < n - mmax * 2; i += mmax * 2)
                    {
                        tempr -= tempi;
                        tempi = (float)wr * data1i + (float)wi * data1r;
                        data1r = data[j + mmax * 2];
                        data1i = data[j + mmax * 2 + 1];
                        data[i] = (datar = data[i]) + tempr;
                        data[i + 1] = (datai = data[i + 1]) + tempi;
                        data[j] = datar - tempr;
                        data[j + 1] = datai - tempi;
                        tempr = (float)wr * data1r;
                        tempi = (float)wi * data1i;
                        j += mmax * 2;
                    }
                    tempr -= tempi;
                    tempi = (float)wr * data1i + (float)wi * data1r;
                    data[i] = (datar = data[i]) + tempr;
                    data[i + 1] = (datai = data[i + 1]) + tempi;
                    data[j] = datar - tempr;
                    data[j + 1] = datai - tempi;
                    wr = (wtemp = wr) * wpr - wi * wpi;
                    wi = wtemp * wpi + wi * wpr;
                }
            }
        }

        static void entropiaXbandas(double[] re, double[] im, double[] entropias,
               float sampleRate, int N)
        {
            int k, p = 0;
            int n1, n2, n;
            double f, b;
            int k1, k2;
            double b1 = 0;
            double sum = 0.0;

            k1 = Convert.ToInt32(Math.Round(20 * N / sampleRate));    // desde 20 Hz;
            n1 = n2 = k1;
            k2 = (int)Math.Round(22050 * N / sampleRate); // hasta 22 KHz

            for (k = k1; k < k2; k++)
            {
                f = (sampleRate * k) / N; // frec en Hertz
                b = 13 * Math.Atan(0.00076 * f) + 3.5 * Math.Atan((f / 7500) * (f / 7500)); // frec en Barks

                n2++;
                if ((b - b1) >= 1.0)
                { // ancho de 1 bark para 25 bandas
                    double[] real = new double[(n2 - n1)];
                    double[] imag = new double[(n2 - n1)];
                    //                System.out.printf("BARK=%5.2f\n",b);
                    for (n = n1; n < n2; n++)
                    {
                        real[n - n1] = re[n];
                        imag[n - n1] = im[n];
                        // System.out.printf("%5.2f %5.2f\n",real[n-n1],imag[n-n1]);
                    }
                    sum += entropias[p] = DeterminaEntropiaGauss(real, 0, imag, 0, n2 - n1);
                    p++;
                    b1 = b;
                    n1 = n2;
                }
            }
            if (sum > 0.0)
            {
                for (p--; p >= 0; p--)
                {
                    entropias[p] /= sum;
                }
            }
        }

        static double DeterminaEntropiaGauss(double[] x, int base_x,
                    double[] y, int base_y, int n)
        {
            double entropia;
            int i;
            double varx = 0, vary = 0, varxy = 0, promx = 0, promy = 0;
            for (i = 0; i < n; i++)
            {
                promx += x[base_x + i];
                promy += y[base_y + i];
            }
            promx /= n;
            promy /= n;
            for (i = 0; i < n; i++)
            {
                varx += (x[base_x + i] - promx) * (x[base_x + i] - promx);
                vary += (y[base_y + i] - promy) * (y[base_y + i] - promy);
                varxy += (x[base_x + i] - promx) * (y[base_y + i] - promy);
            }
            varx /= n;
            vary /= n;
            varxy /= n;
            double det = varx * vary - varxy * varxy;
            entropia = log(2 * Math.PI) + log(det) / 2;
            return entropia;
        }


        static double log(double x)
        {
            return (x > 0.0) ? Math.Log(x) : 0;
        }

        static float log(float x)
        {
            return (x > 0.0) ? (float)Math.Log(x) : 0;
        }

        #region morevars
        static double[,] s = {
	{0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0},
	{0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0},
	{0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0},
	{0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0},
	{0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0},
	{0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0},
	{0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0},
	{0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0},
	{0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0},
	{0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0},
	{0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0},
	{0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0},
	{0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0},
	{0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0},
	{0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0},
	{0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0},
	{0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0},
	{0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0},
	{0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0},
	{0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0},
	{0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0},
	{0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0},
	{0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0},
	{0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0} };

        // array dim: 24
        static double[] sum = {
	0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,
	0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,
	0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0 };

        static int code_i = 0, code_j = 1;

        #endregion

        static void code(double[] y, int n, int bands, TextWriter sp)
        {
            for (int k = 0; k < bands; k++)
            {
                sum[k] -= s[k, code_i];
                sum[k] += (s[k, code_i] = (y[k] / (float)n));
            }
            code_i++;

            if ((code_j % (n + 1)) == 0)
            {
                code_i = 0;
            }

            code_j++;

            if (code_j >= n)
            {
                tcode(sum, bands, sp);
            }
        }

        static double[] tcode_y = new double[BANDS];
        static double tcode_x = 0;
        static double tcode_i = 0;

        static void tcode(double[] y1, int bands, TextWriter sp)
        {
            int j, k;

            char codebuff = ' '; // ** used to be unsigned, but Java doesn't have tihs
            double Dt, Frac;

            Dt = (((float)TAM_FRAME / (float)FRAME_OVERLAP) / (float)FRAME_RATE);
            Frac = 0.4375 * Dt;

            if (tcode_i == 0)
            {
                tcode_i++;
                for (j = 0; j < bands; j++)
                {
                    tcode_y[j] = y1[j];
                }
                return;
            }

            for (j = 0; j < bands; j++)
            {
                switch (plot_)
                {
                    case 'e': // entropy
                        //sp.printf("%3.5f ",tcode_y[j]);                        
                        //sp.print(tcode_y[j] + " ");
                        sp.Write(tcode_y[j] + " ");
                        break;
                    case 'h': // Hex signature
                        tcode_y[j] = Math.Atan((y1[j] - tcode_y[j]) / (Frac));
                        k = 2 * (j % 4);
                        int cbuff = (int)codebuff;
                        cbuff |= tcode_y[j] > dMax_ ? 0x80 >> k : ((tcode_y[j] < -dMax_) ? 0x40 >> k : 0xC0 >> k);
                        codebuff = (char)cbuff;
                        if (k == 6)
                        {
                            //sp.printf("%02X",codebuff);
                            //sp.print(codebuff);
                            sp.Write(codebuff);
                            codebuff = ' ';
                        }
                        break;
                    case 's': // Binary signature
                        tcode_y[j] = Math.Atan((y1[j] - tcode_y[j]) / (Frac));
                        char c = (tcode_y[j] > dMax_) ? '1' : ((tcode_y[j] < -dMax_) ? '0' : '*');

                        //sp.print(c);
                        sp.Write(c);
                        break;
                    case 'S': // slope
                        //sp.printf("%3.5f ",Math.atan((y1[j]-tcode_y[j])/(Frac)));
                        //sp.print(Math.Atan((y1[j] - tcode_y[j]) / (Frac)));
                        sp.Write(Math.Atan((y1[j] - tcode_y[j]) / (Frac)));
                        //sp.print(" ");
                        sp.Write(" ");
                        break;
                }
                tcode_y[j] = y1[j];
            }
            //    sp.printf("\n");
            //sp.print(" ");
            sp.Write(" ");
            tcode_x += Dt;
        }


    }
}
